using ParkingService.Domain.Entity;

namespace ParkingService.Domain.Repository
{
	public interface ISlotRepo
	{
		Task<int> AddSlot(Slot slot);
		Task<Slot> GetSlotById(string slotId);
		Task<List<Slot>> GetSlotsByCompanyId(string companyId);
		Task<int> SaveChangesAsyncCustom();
		Task<Slot> GetSlotByUserIdAndSlotId(string userId, string slotId);
		Task<Slot> GetSlotByUserIdAndSlotIdAndHistoryId(string userId, string slotId, string historyId);
		Task<List<Slot>> GetSlotByUserId(string userId);

	}
}
