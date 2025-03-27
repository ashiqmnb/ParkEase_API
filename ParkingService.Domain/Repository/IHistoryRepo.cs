using ParkingService.Domain.Entity;

namespace ParkingService.Domain.Repository
{
	public interface IHistoryRepo
	{
		Task<int> AddHistory(History history);
		Task<History> GetHistoryById(string id);
		Task<List<History>> GetHistoryByUserId(string userId);
		Task<int> SaveChangesAsyncCustom();
	}
}
