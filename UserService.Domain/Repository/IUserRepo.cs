using UserService.Domain.Entity;

namespace UserService.Domain.Repository
{
	public interface IUserRepo
	{
		Task<User> GetUserById(string userId);
	}
}
