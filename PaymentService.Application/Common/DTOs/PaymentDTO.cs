using System.ComponentModel.DataAnnotations;

namespace PaymentService.Application.Common.DTOs
{
	public class PaymentDTO
	{
		[Required(ErrorMessage = "PaymentId is required")]
		public string? razorpay_payment_id { get; set; }

		[Required(ErrorMessage = "OrderId is required")]
		public string? razorpay_order_id { get; set; }

		[Required(ErrorMessage = "Signature is required")]
		public string? razorpay_signature { get; set; }
	}
}
