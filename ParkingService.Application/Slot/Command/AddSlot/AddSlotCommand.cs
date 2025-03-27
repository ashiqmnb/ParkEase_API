using MediatR;
using ParkingService.Application.Common.DTO.Slot;

namespace ParkingService.Application.Slot.Command.AddSlot
{
    public class AddSlotCommand : IRequest<bool>
    {
        public string CompanyId { get; set; }
        public AddSlotDTO AddSlotDto { get; set; }

        public AddSlotCommand(string companyId, AddSlotDTO addSlotDto)
        {
            CompanyId = companyId;
            AddSlotDto = addSlotDto;
        }
    }
}
