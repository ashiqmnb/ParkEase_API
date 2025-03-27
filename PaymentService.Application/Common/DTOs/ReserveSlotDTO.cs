namespace PaymentService.Application.Common.DTOs
{
	public class ReserveSlotDTO
	{
		public string CompanyId { get; set; }
		public string SlotId{ get; set; }
		public string Type { get; set; }
		public string VehicleNumber { get; set; }

	}
}
