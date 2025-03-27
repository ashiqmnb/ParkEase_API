using Contracts.PaymentEvents;
using MassTransit;
using ParkingService.Domain.Entity;
using ParkingService.Domain.Repository;

namespace ParkingService.Infrastructure.Consumer
{
	public class UpdateSlotEventConsumer : IConsumer<UpdateSlotEvent>
	{
		private readonly ISlotRepo _slotRepo;

		public UpdateSlotEventConsumer(ISlotRepo slotRepo)
		{
			_slotRepo = slotRepo;
		}

		public async Task Consume(ConsumeContext<UpdateSlotEvent> context)
		{
			try
			{
				var slot = await _slotRepo.GetSlotById(context.Message.SlotId);
				if (slot == null) throw new Exception("Slot not found");

				slot.Status = Domain.Entity.SlotStatus.Reserved;
				slot.UserId = context.Message.UserId;
				slot.UserName = context.Message.UserName;
				slot.VehicleNumber = context.Message.VehicleNumber;
				slot.UpdatedBy = context.Message.UserId;
				slot.UpdatedOn = DateTime.UtcNow;

				await _slotRepo.SaveChangesAsyncCustom();

			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
