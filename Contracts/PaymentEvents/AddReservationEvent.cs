namespace Contracts.PaymentEvents
{
	public class AddReservationEvent
	{
		public Guid TransactionId { get; set; }
		public string UserId { get; set; }
		public string CompanyId { get; set; }
		public int Coins { get; set; }
		public string SlotId { get; set; }
		public string VehicleNumber { get; set; }
	}
}
