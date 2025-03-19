using UserService.Domain.Entity;

namespace UserService.Domain.Repository
{
	public interface ICompanyRepo
	{
		Task<Company> GetCompanyById(string companyId);
		Task<int> SaveChangesAsyncCustom();
	}
}
