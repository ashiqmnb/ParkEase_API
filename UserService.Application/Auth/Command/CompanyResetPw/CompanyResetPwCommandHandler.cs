using MediatR;
using UserService.Domain.Repository;

namespace UserService.Application.Auth.Command.CompanyResetPw
{
	public class CompanyResetPwCommandHandler : IRequestHandler<CompanyResetPwCommand, bool>
	{
		private readonly IAuthRepo _authRepo;

		public CompanyResetPwCommandHandler(IAuthRepo authRepo)
		{
			_authRepo = authRepo;
		}
		public async Task<bool> Handle(CompanyResetPwCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var company = await _authRepo.GetCompanyByEmail(request.companyResetPwDto.Email);
				if (company == null) throw new Exception("Comapny not found");

				// hash new password
				string salt = BCrypt.Net.BCrypt.GenerateSalt();
				string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.companyResetPwDto.NewPassword, salt);

				// update password
				company.Password = hashPassword;
				await _authRepo.SaveChangesAsyncCustom();

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
