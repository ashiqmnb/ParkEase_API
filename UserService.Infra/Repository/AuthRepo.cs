using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entity;
using UserService.Domain.Repository;
using UserService.Infrastructure.Data;


namespace UserService.Infrastructure.Repository
{
	public class AuthRepo : IAuthRepo
	{

		private readonly UserDbContext _userDbContext;

		public AuthRepo(UserDbContext userDbContext)
		{
			_userDbContext = userDbContext;
		}

		public async Task<int> AddCompany(Company company)
		{
			try
			{
				await _userDbContext.Companies.AddAsync(company);
				return await _userDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<int> AddUser(User user)
		{
			try
			{
				await _userDbContext.Users.AddAsync(user);
				return await _userDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<int> AddVerifyUser(VerifyUser verifyUser)
		{
			try
			{
				await _userDbContext.VerifyUsers.AddAsync(verifyUser);
				return await _userDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<List<Admin>> GetAdmin()
		{
			try
			{
				var admin = await _userDbContext.Admins.ToListAsync();
				return admin;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<Company> GetCompanyByEmail(string email)
		{
			try
			{
				var company = await _userDbContext.Companies.FirstOrDefaultAsync(c => c.Email == email);
				return company;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<User> GetUserByEmail(string email)
		{
			try
			{
				var user =  await _userDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
				return user;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<List<User>> GetUsers()
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

		public async Task<List<VerifyUser>> GetVerifyUsers()
		{
			try
			{
				var verifyUsers = await _userDbContext.VerifyUsers.ToListAsync();
				return verifyUsers;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<int> RemoveVerifyUser(VerifyUser verifyUser)
		{
			try
			{
				_userDbContext.VerifyUsers.Remove(verifyUser);
				return await _userDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<int> SaveChangesAsyncCustom()
		{
			return await _userDbContext.SaveChangesAsync();
		}
	}
}
