using System.ComponentModel.DataAnnotations;

namespace ParkingService.Domain.Entity
{
	public class Slot : AuditableEntity
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "CompanyId is required")]
		public string CompanyId { get; set; }

		[Required(ErrorMessage = "Status is required")]
		public SlotStatus Status { get; set; } = SlotStatus.Available;

		[Required(ErrorMessage = "Type is required")]
		public SlotType Type { get; set; }

		public string? UserId { get; set; }
		public string? VehicleNumber { get; set; }


		public ICollection<History> Histories { get; set; } = new List<History>();

	}

	public enum SlotStatus
	{
		Available,
		Reserved,
		Parked
	}

	public enum SlotType
	{
		TwoWheeler,
		FourWheeler
	}
}
