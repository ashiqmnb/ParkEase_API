namespace Contracts.PaymentEvents
{
	public class UpdateTransactionStatusEvent
	{
		public Guid TransactionId { get; set; }
		public string Status { get; set; }
		public DateTime UpdatedOn { get; set; }
	}
}
