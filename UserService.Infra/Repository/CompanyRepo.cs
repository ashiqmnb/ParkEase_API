using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using UserService.Domain.Entity;
using UserService.Domain.Repository;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.Migrations;

namespace UserService.Infrastructure.Repository
{
	public class CompanyRepo : ICompanyRepo
	{
		private readonly UserDbContext _userDbContext;

		public CompanyRepo(UserDbContext userDbContext)
		{
			_userDbContext = userDbContext;
		}

		public async Task<List<Company>> GetAllCompanies()
		{
			try
			{
				var companies = await _userDbContext.Companies.ToListAsync();
				return companies;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<List<Company>> GetAllCompaniesForAdmin()
		{
			try
			{
				var companies = await _userDbContext.Companies
					.Where(c => c.IsDeleted == false)
					.OrderByDescending(c => c.CreatedOn)
					.ToListAsync();
				return companies;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<List<Company>> GetAllCompaniesForUser()
		{
			try
			{
				var companies = await _userDbContext.Companies
					.Include(c => c.Address)
					.Where(c => c.IsDeleted == false && c.IsBlocked == false && c.SubscriptionStatus == SubscriptionStatus.Active)
					.OrderByDescending(c => c.CreatedOn)
					.ToListAsync();

				return companies;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
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
