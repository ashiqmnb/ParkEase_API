using MediatR;

namespace PaymentService.Application.Coin.Command.AddSubscription
{
	public class AddSubscriptionCommand : IRequest<bool>
	{
		public string CompanyId {  get; set; }
		public int Days { get; set; }
	}
}
