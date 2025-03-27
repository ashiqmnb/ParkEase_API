using MediatR;
using ParkingService.Domain.Entity;
using ParkingService.Domain.Repository;

namespace ParkingService.Application.Slot.Command.ChackInSlot
{
	public class CheckInSlotCommandHandler : IRequestHandler<CheckInSlotCommand, bool>
	{
		private readonly ISlotRepo _slotRepo;
		private readonly IHistoryRepo _historyRepo;

		public CheckInSlotCommandHandler(ISlotRepo slotRepo, IHistoryRepo historyRepo)
		{
			_slotRepo = slotRepo;
			_historyRepo = historyRepo;
		}

		public async Task<bool> Handle(CheckInSlotCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var slot = await _slotRepo.GetSlotByUserIdAndSlotId(request.UserId, request.SlotId);

				if (slot == null) throw new Exception("Slot not found with curresponding userId and slotId");
				if (slot.Status != Domain.Entity.SlotStatus.Reserved) throw new Exception("Slot currently not reserved");

				Guid historyId = Guid.NewGuid();

				var history = new Domain.Entity.History
				{
					Id = historyId,
					SlotId = slot.Id,
					SlotName = slot.Name,
					CompanyId = slot.CompanyId,
					UserId = request.UserId,
					CheckIn = DateTime.UtcNow,
					CreatedBy = request.UserId,
					CreatedOn = DateTime.UtcNow,
					VehicleNumber = slot.VehicleNumber,
				};

				await _historyRepo.AddHistory(history);

				slot.CurrentHistoryId = historyId.ToString();
				slot.Status = SlotStatus.Parked;
				await _historyRepo.SaveChangesAsyncCustom();

				return true;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
