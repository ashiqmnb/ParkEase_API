using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using UserService.Domain.Entity;
using UserService.Domain.Repository;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repository
{
	public class CompanyRepo : ICompanyRepo
	{
		private readonly UserDbContext _userDbContext;

		public CompanyRepo(UserDbContext userDbContext)
		{
			_userDbContext = userDbContext;
		}

		public async Task<Company> GetCompanyById(string companyId)
		{
			try
			{
				var company = await _userDbContext.Companies
					.FirstOrDefaultAsync(c => c.Id.ToString() == companyId);
				return company;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<int> SaveChangesAsyncCustom()
		{
			try
			{
				return await _userDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
