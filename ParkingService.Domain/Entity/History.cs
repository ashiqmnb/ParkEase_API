using System.ComponentModel.DataAnnotations;

namespace ParkingService.Domain.Entity
{
	public class History : AuditableEntity
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "SlotId is required")]
		public Guid SlotId { get; set; }

		[Required(ErrorMessage = "SlotName is required")]
		public string SlotName { get; set; }

		[Required(ErrorMessage = "CompanyId is required")]
		public string CompanyId { get; set; }

		[Required(ErrorMessage = "UserId is required")]
		public string UserId { get; set; }

		[Required(ErrorMessage = "CheckIn is required")]
		public DateTime CheckIn { get; set; }


		public DateTime? CheckOut { get; set; }

		[Required(ErrorMessage = "VehicleNumber is required")]
		public string? VehicleNumber { get; set; }


		public Slot Slot { get; set; }
	}
}
