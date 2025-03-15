

using UserService.Domain.Entity;

namespace UserService.Domain.Repository
{
	public interface IAuthRepo
	{
		Task<List<Admin>> GetAdmin();
		Task<List<User>> GetUsers();
		Task<List<VerifyUser>> GetVerifyUsers();
		Task<int> RemoveVerifyUser(VerifyUser verifyUser);
		Task<int> AddVerifyUser(VerifyUser verifyUser);
		Task<int> AddUser(User user);
		Task<User> GetUserByEmail(string email);
		Task<int> SaveChangesAsyncCustom();
		Task<Company> GetCompanyByEmail(string email);
		Task<int> AddCompany(Company company);
	}
}
