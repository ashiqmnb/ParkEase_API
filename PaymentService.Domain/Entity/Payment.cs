using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entity;

namespace PaymentService.Domain.Entity
{
	public class Payment : AuditableEntity
	{ 
		public Guid Id { get; set; }

		[Required(ErrorMessage = "TransactionId is required")]
		public string TransactionId { get; set; }

		[Required(ErrorMessage = "Status is required")]
		public PaymentStatus Status { get; set; }	

		[Required(ErrorMessage = "Amount is required")]
		public int Amount { get; set; }

		[Required(ErrorMessage = "Coin is required")]
		public int Coin { get; set; }

		[Required(ErrorMessage = "CustomerId is required")]
		public Guid CustomerId { get; set; }
	}


	public enum PaymentStatus
	{
		Pending,
		Success,
		Failed
	}
}
