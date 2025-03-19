namespace Contracts.PaymentEvents
{
	public class AddSubscriptionEvent
	{
		public Guid TransactionId { get; set; }
		public string CompanyId { get; set; }
		public int Days { get; set; }
		public int Coins { get; set; }
	}
}
