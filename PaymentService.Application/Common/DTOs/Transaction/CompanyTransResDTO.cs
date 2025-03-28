using System.ComponentModel.DataAnnotations;

namespace PaymentService.Application.Common.DTOs.Transaction
{
	public class CompanyTransResDTO
	{
		public string TransactionId { get; set; }
		public Guid SenderId { get; set; }
		public Guid ReceiverId { get; set; }
		public int Coin { get; set; }
		public string Description { get; set; }
		public string Status { get; set; }
		public DateTime Date { get; set; }
	}
}
