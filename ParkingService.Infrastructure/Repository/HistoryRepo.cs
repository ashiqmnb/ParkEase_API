using Microsoft.EntityFrameworkCore;
using ParkingService.Domain.Entity;
using ParkingService.Domain.Repository;
using ParkingService.Infrastructure.Data;

namespace ParkingService.Infrastructure.Repository
{
	public class HistoryRepo : IHistoryRepo
	{
		private readonly ParkingDbContext _parkingDbContext;

		public HistoryRepo(ParkingDbContext parkingDbContext)
		{
			_parkingDbContext = parkingDbContext;
		}

		public async Task<int> AddHistory(History history)
		{
			try
			{
				await _parkingDbContext.AddAsync(history);
				return await _parkingDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ??  ex.Message);	
			}
		}

		public async Task<History> GetHistoryById(string id)
		{
			try
			{
				var history = await _parkingDbContext.Histories
					.FirstOrDefaultAsync(h => h.Id.ToString() == id);
				return history;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<List<History>> GetHistoryByUserId(string userId)
		{
			try
			{
				var histories = await _parkingDbContext.Histories
					.Where(h => h.UserId == userId)
					.ToListAsync();
				return histories;
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
				return await _parkingDbContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
