using UserService.Domain.Entity;

namespace UserService.Domain.Repository
{
	public interface ICompanyRepo
	{
		Task<List<Company>> GetAllCompaniesForAdmin();
		Task<List<Company>> GetAllCompaniesForUser();
		Task<Company> GetCompanyById(string companyId);
		Task<int> SaveChangesAsyncCustom();
	}
}
