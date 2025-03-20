using MediatR;
using ParkingService.Application.Common.DTO.Slot;

namespace ParkingService.Application.Slot.Query.GetSlotsByCompanyId
{
	public class GetSlotsByCompanyIdCommand : IRequest<SlotResDetailsDTO>
	{
		public string CompanyId { get; set; }
	}
}
