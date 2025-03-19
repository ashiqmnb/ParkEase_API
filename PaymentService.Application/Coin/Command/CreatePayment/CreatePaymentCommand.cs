using MediatR;

namespace PaymentService.Application.Coin.Command.RazorOrderCreate
{
	public class CreatePaymentCommand : IRequest<string>
	{
		public string UserId { get; set; }
		public int Amount { get; set; }
	}
}
