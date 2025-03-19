using System.ComponentModel.DataAnnotations;
using UserService.Domain.Entity;

namespace PaymentService.Domain.Entity
{
	public class Transaction : AuditableEntity
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "SenderId is required")]
		public Guid SenderId { get; set; }

		[Required(ErrorMessage = "ReceiverId is required")]
		public Guid ReceiverId { get; set; }

		[Required(ErrorMessage = "Coin is required")]
		public int Coin { get; set; }


		[Required(ErrorMessage = "Status is required")]
		public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
	}

	public enum TransactionStatus
	{
		Pending,
		Success,
		Failed
	}

}
