namespace UserService.Application.Common.DTOs.Company
{
	public class CompanyPageResAdminDTO
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public List<CompanyResForAdminDTO> Companies { get; set; }
	}
}
