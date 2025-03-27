using MediatR;
using ParkingService.Domain.Repository;

namespace ParkingService.Application.Slot.Command.CheckOutSlot
{
	public class CheckOutSlotCommandHandler : IRequestHandler<CheckOutSlotCommand, bool>
	{
		private readonly ISlotRepo _slotRepo;
		private readonly IHistoryRepo _historyRepo;

		public CheckOutSlotCommandHandler(ISlotRepo slotRepo, IHistoryRepo historyRepo)
		{
			_slotRepo = slotRepo;
			_historyRepo = historyRepo;
		}

		public async Task<bool> Handle(CheckOutSlotCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var slot = await _slotRepo.GetSlotByUserIdAndSlotIdAndHistoryId(request.UserId, request.CheckOutSlotDto.SlotId, request.CheckOutSlotDto.HistoryId);
				if (slot == null) throw new Exception("Slot not found with current credentials");

				slot.Status = Domain.Entity.SlotStatus.Available;
				slot.VehicleNumber = null;
				slot.UserId = null;
				slot.UserName = null;
				slot.CurrentHistoryId = null;

			    var history = await _historyRepo.GetHistoryById(request.CheckOutSlotDto.HistoryId);
				if (history == null) throw new Exception("History not found");

				history.CheckOut = DateTime.UtcNow;

				await _slotRepo.SaveChangesAsyncCustom();

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message); 
			}
		}
	}
}
