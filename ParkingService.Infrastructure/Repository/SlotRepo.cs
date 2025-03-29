using Microsoft.EntityFrameworkCore;
using ParkingService.Domain.Entity;
using ParkingService.Domain.Repository;
using ParkingService.Infrastructure.Data;
using System.Reflection.Metadata.Ecma335;

namespace ParkingService.Infrastructure.Repository
{
	public class SlotRepo : ISlotRepo
	{
		private readonly ParkingDbContext _parkingDbContext;

		public SlotRepo(ParkingDbContext parkingDbContext)
		{
			_parkingDbContext = parkingDbContext;
		}

		public async Task<int> AddSlot(Slot slot)
		{
			try
			{
				await _parkingDbContext.Slots.AddAsync(slot);
				return await _parkingDbContext.SaveChangesAsync();
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<List<Slot>> GetSlotsByCompanyId(string companyId)
		{
			try
			{
				var slot = await _parkingDbContext.Slots
					.Where(s => s.CompanyId == companyId && s.IsDeleted == false)
					.ToListAsync();
				return slot;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<Slot> GetSlotById(string slotId)
		{
			try
			{
				var slot = await _parkingDbContext.Slots
					.FirstOrDefaultAsync(s => s.Id.ToString() == slotId);
				return slot;
				
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

		public async Task<Slot> GetSlotByUserIdAndSlotId(string userId, string slotId)
		{
			try
			{
				var slot = await _parkingDbContext.Slots
					.FirstOrDefaultAsync(s => s.Id.ToString() == slotId && s.UserId == userId);
				return slot;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<Slot> GetSlotByUserIdAndSlotIdAndHistoryId(string userId, string slotId, string historyId)
		{
			try
			{
				var slot = await _parkingDbContext.Slots
					.FirstOrDefaultAsync(s => s.Id.ToString() == slotId && s.UserId == userId && s.CurrentHistoryId == historyId);
				return slot;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<List<Slot>> GetSlotByUserId(string userId)
		{
			try
			{
				var slots = await _parkingDbContext.Slots
					.Where(s => s.UserId == userId).ToListAsync();
				return slots;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<List<Slot>> GetAllSlots()
		{
			try
			{
				var slots = await _parkingDbContext.Slots.ToListAsync();
				return slots;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException.Message ?? ex.Message);
			}
		}
	}
}
