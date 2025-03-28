namespace PaymentService.Application.Common.DTOs.Transaction
{
	public class PaymentPageResDTO
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public List<PaymentResDTO> Payments { get; set; }
	}
}
