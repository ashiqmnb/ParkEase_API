using MediatR;
using ParkingService.Application.Common.DTO.Slot;

namespace ParkingService.Application.Slot.Command
{
	public class AddSlotCommand : IRequest<bool>
	{
		public string CompanyId {  get; set; }
		public AddSlotDTO AddSlotDto { get; set; }

        public AddSlotCommand(string companyId, AddSlotDTO addSlotDto)
		{
            CompanyId = companyId;
			this.AddSlotDto = addSlotDto;
        }
    }
}
