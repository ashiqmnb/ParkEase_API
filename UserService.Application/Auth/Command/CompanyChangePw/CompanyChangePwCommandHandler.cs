using MediatR;
using UserService.Domain.Entity;
using UserService.Domain.Repository;

namespace UserService.Application.Auth.Command.CompanyChangePw
{
	public class CompanyChangePwCommandHandler : IRequestHandler<CompanyChangePwCommand, bool>
	{
		private readonly ICompanyRepo _companyRepo;

		public CompanyChangePwCommandHandler(ICompanyRepo companyRepo)
		{
			_companyRepo = companyRepo;
		}

		public async Task<bool> Handle(CompanyChangePwCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var company = await _companyRepo.GetCompanyById(request.CompanyId);

				if (company == null) throw new Exception("Company not found");

				bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.ChangePwDto.CurrentPassword, company.Password);
				if (!isValidPassword)
				{
					throw new UnauthorizedAccessException("Invalid Password");
				}

				string salt = BCrypt.Net.BCrypt.GenerateSalt();
				string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.ChangePwDto.NewPassword, salt);

				company.Password = hashPassword;
				company.UpdatedOn = DateTime.UtcNow;
				company.UpdatedBy = request.CompanyId;

				await _companyRepo.SaveChangesAsyncCustom();

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
