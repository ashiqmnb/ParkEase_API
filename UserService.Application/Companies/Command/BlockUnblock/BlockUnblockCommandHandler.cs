using MediatR;
using UserService.Domain.Repository;

namespace UserService.Application.Companies.Command.BlockUnblock
{
	public class BlockUnblockCommandHandler : IRequestHandler<BlockUnblockCommand, string>
	{
		private readonly ICompanyRepo _companyRepo;

		public BlockUnblockCommandHandler(ICompanyRepo companyRepo)
		{
			_companyRepo = companyRepo;
		}

		public async Task<string> Handle(BlockUnblockCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var company = await _companyRepo.GetCompanyById(request.CompanyId);

				if (company == null) throw new Exception("Company not found");

				company.IsBlocked = !company.IsBlocked;
				await _companyRepo.SaveChangesAsyncCustom();

				if (company.IsBlocked == true) return "Blocked company successfully";
				return "Unblocked company successfully";
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ??  ex.Message);	
			}
		}
	}
}
