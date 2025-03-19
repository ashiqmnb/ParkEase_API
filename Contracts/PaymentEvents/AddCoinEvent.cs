namespace Contracts.PaymentEvents
{
	public class AddCoinEvent
	{
		public string UserId { get; set; }
		public int Coins { get; set; }
	}
}
