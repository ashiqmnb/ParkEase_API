using MediatR;
using UserService.Application.Common.DTOs.User;
using UserService.Domain.Repository;

namespace UserService.Application.Users.Query.ActiveUsersCompanies
{
	public class ActiveUsersCompaniesQueryHandler : IRequestHandler<ActiveUsersCompaniesQuery, ActiveUsersCompaniesDTO>
	{
		private readonly IUserRepo _userRepo;
		private readonly ICompanyRepo _companyRepo;
		public ActiveUsersCompaniesQueryHandler(IUserRepo userRepo, ICompanyRepo companyRepo)
		{
			_userRepo = userRepo;
			_companyRepo = companyRepo;
		}

		public async Task<ActiveUsersCompaniesDTO> Handle(ActiveUsersCompaniesQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var companies = await _companyRepo.GetAllCompanies();
				var users = await _userRepo.GetAllUsers();

				users = users.Where(u => u.IsDeleted == false).ToList();
				companies = companies.Where(c => c.IsDeleted == false).ToList();

				var res = new ActiveUsersCompaniesDTO
				{
					Companies = companies.Count,
					Users = users.Count,
				};

				return res;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
