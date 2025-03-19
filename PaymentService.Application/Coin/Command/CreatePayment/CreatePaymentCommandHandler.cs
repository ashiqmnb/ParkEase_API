using MediatR;
using PaymentService.Application.Common.AppSettings;
using PaymentService.Domain.Repository;
using Razorpay.Api;

namespace PaymentService.Application.Coin.Command.RazorOrderCreate
{
	public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, string>
	{
		private readonly AppSettings _appSettings;
		private readonly IPaymentRepo _paymentRepo;


		public CreatePaymentCommandHandler(AppSettings appSettings, IPaymentRepo paymentRepo)
		{
			_appSettings = appSettings;
			_paymentRepo = paymentRepo;
		}

		public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
		{
			try
			{
				if(request.Amount < 20)
				{
					throw new Exception("Invalid amout");
				}

				decimal amount = request.Amount * 10;

				Dictionary<string, object> input = new Dictionary<string, object>();

				input.Add("amount", amount * 100);
				input.Add("currency", "INR");
				input.Add("receipt", request.UserId);
				input.Add("payment_capture", 1);

				string key = _appSettings.RazorPayKeyId;
				string secret = _appSettings.RazorPayKeySecret;

				RazorpayClient client = new RazorpayClient(key, secret);
				Razorpay.Api.Order order = client.Order.Create(input);

				var OrderId = order["id"].ToString();

				// convert userId from string to guid
				Guid guidCustomerId = Guid.Parse(request.UserId);

				var payment = new Domain.Entity.Payment
				{
					TransactionId = OrderId,
					Amount = request.Amount,
					Coin = request.Amount / 2,
					CustomerId = guidCustomerId,
					Status = Domain.Entity.PaymentStatus.Pending,
					CreatedBy = request.UserId,
					CreatedOn = DateTime.UtcNow
				};

				await _paymentRepo.AddPayment(payment);
				return OrderId;

			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
