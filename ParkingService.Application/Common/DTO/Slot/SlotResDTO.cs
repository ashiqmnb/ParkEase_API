using ParkingService.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace ParkingService.Application.Common.DTO.Slot
{
	public class SlotResDTO
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Status { get; set; }
		public string Type { get; set; }
		public string? UserId { get; set; }
		public string? VehicleNumber { get; set; }
	}
}
