using MediatR;
using ParkingService.Application.Common.DTO.Slot;

namespace ParkingService.Application.Slot.Query.GetReservationsParkings
{
	public class GetReservationsParkingsCommand : IRequest<ReservParkResDTO>
	{
		public string UserId { get; set; }
	}
}
