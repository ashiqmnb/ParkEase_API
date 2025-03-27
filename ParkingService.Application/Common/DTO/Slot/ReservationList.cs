using MassTransit.Configuration;

namespace ParkingService.Application.Common.DTO.Slot
{
	public class ReservationList
	{
		public string SlotId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Status { get; set; }
		public string VehicleNumber { get; set; }

	}
}
