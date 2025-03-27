using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entity;
using UserService.Domain.Repository;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repository
{
	public class UserRepo : IUserRepo
	{
		private readonly UserDbContext _userDbContext;

		public UserRepo(UserDbContext userDbContext)
		{
			_userDbContext = userDbContext;
		}

		public async Task<List<User>> GetAllUsers()
		{
			try
			{
				var users = await _userDbContext.Users.ToListAsync();
				return users;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<User> GetUserById(string userId)
		{
			try
			{
				var user = await _userDbContext.Users
					.FirstOrDefaultAsync(u => u.Id.ToString() == userId);
				return user;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<List<Admin>> GetAllAdmins()
		{
			try
			{
				var admins = await _userDbContext.Admins.ToListAsync();
				return admins;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

	}
}
