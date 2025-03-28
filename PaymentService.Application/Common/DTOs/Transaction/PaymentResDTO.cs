using PaymentService.Domain.Entity;
using System.ComponentModel.DataAnnotations;

namespace PaymentService.Application.Common.DTOs.Transaction
{
	public class PaymentResDTO
	{
		public string TransactionId { get; set; }
		public string CustomerId { get; set; }
		public int Amount { get; set; }
		public int Coin { get; set; }
		public string Status { get; set; }
		public DateTime Date { get; set; }
	}
}
