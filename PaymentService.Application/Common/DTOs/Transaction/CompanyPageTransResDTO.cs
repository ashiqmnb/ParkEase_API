namespace PaymentService.Application.Common.DTOs.Transaction
{
	public class CompanyPageTransResDTO
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public List<CompanyTransResDTO> Transactions { get; set; }
	}
}
