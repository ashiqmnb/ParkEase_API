using MediatR;

namespace ParkingService.Application.Slot.Command.ChackInSlot
{
	public class CheckInSlotCommand : IRequest<bool>
	{
		public string UserId { get; set; }
		public string SlotId { get; set; }
	}
}
