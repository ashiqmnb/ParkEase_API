using MediatR;
using PaymentService.Application.Common.DTOs;

namespace PaymentService.Application.Coin.Command.ReserveSlot
{
	public class ReserveSlotCommand : IRequest<bool>
	{
		public string UserId { get; set; }
		public ReserveSlotDTO ReserveSlotDto { get; set; }

        public ReserveSlotCommand(string UserId, ReserveSlotDTO reserveSlotDto)
        {
            this.UserId = UserId;
            this.ReserveSlotDto = reserveSlotDto;
        }
    }
}
