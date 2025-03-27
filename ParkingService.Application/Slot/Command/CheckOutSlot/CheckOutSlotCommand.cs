using MediatR;
using ParkingService.Application.Common.DTO.Slot;

namespace ParkingService.Application.Slot.Command.CheckOutSlot
{
	public class CheckOutSlotCommand : IRequest<bool>
	{
		public string UserId {  get; set; }
		public CheckOutSlotDTO CheckOutSlotDto { get; set; }
        public CheckOutSlotCommand(string userId, CheckOutSlotDTO checkOutSlotDto)
        {
            this.UserId = userId;
            this.CheckOutSlotDto = checkOutSlotDto;
        }
    }
}
