
using Contracts.PaymentEvents;
using MassTransit;
using MediatR;
using PaymentService.Application.Common.AppSettings;
using PaymentService.Domain.Entity;
using PaymentService.Domain.Repository;
using Razorpay.Api;

namespace PaymentService.Application.Coin.Command.ConfirmPayment
{
	public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand, bool>
	{

		private readonly AppSettings _appSettings;
		private readonly IPaymentRepo _paymentRepo;
		private readonly IPublishEndpoint _publishEndpoint;	


		public ConfirmPaymentCommandHandler(AppSettings appSettings, IPaymentRepo paymentRepo, IPublishEndpoint publishEndpoint)
		{
			_appSettings = appSettings;
			_paymentRepo = paymentRepo;
			_publishEndpoint = publishEndpoint;
		}
		public async Task<bool> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
		{
			try
			{
				string key = _appSettings.RazorPayKeyId;
				string secret = _appSettings.RazorPayKeySecret;

				RazorpayClient client = new RazorpayClient(key, secret);

				Dictionary<string, string> attributes = new Dictionary<string, string>
				{
					{ "razorpay_payment_id",request.paymentDto.razorpay_payment_id },
					{ "razorpay_order_id", request.paymentDto.razorpay_order_id },
					{ "razorpay_signature", request.paymentDto.razorpay_signature }
				};

				Utils.verifyPaymentSignature(attributes);

				var payment = await _paymentRepo.GetPaymentByTransId(request.paymentDto.razorpay_order_id);

				if(payment == null) throw new Exception("Payment record not found");


				// add rabbit mq for updating user coins in user seevice
				await _publishEndpoint.Publish(new AddCoinEvent
				{
					UserId = payment.CustomerId.ToString(),
					Coins = payment.Coin
				});

				payment.Status = PaymentStatus.Success;

				await _paymentRepo.SaveChangesAsyncCustom();
				return true;

			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
