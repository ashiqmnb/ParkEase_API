namespace UserService.Application.Common.DTOs.Company
{
	public class CompanyPageResUserDTO
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public List<CompanyResForUserDTO> Companies { get; set; }
	}
}
