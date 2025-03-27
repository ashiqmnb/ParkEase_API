using MediatR;
using ParkingService.Application.Common.DTO.Slot;

namespace ParkingService.Application.Slot.Query.GetSlotsCountByCompanyId
{
	public class GetSlotsCountByCompanyIdCommand : IRequest<SlotsCountResDTO>
	{
		public string CompanyId { get; set; }
	}
}
