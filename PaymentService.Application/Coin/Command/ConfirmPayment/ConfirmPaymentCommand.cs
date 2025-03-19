using MediatR;
using PaymentService.Application.Common.DTOs;

namespace PaymentService.Application.Coin.Command.ConfirmPayment
{
	public class ConfirmPaymentCommand : IRequest<bool>
	{
		public PaymentDTO paymentDto { get; set; }

		public ConfirmPaymentCommand(PaymentDTO paymentDto)
		{
			this.paymentDto = paymentDto;
		}
	}
}
