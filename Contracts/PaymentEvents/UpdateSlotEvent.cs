namespace Contracts.PaymentEvents
{
	public class UpdateSlotEvent
	{
		public string SlotId { get; set; }
		public string? UserId { get; set; }
		public string? UserName { get; set; }
		public string Status { get; set; }
		public string VehicleNumber { get; set; }
	}
}
