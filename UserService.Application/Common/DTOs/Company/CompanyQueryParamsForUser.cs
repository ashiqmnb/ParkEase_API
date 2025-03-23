namespace UserService.Application.Common.DTOs.Company
{
	public class CompanyQueryParamsForUser
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 8;
		public string? Type { get; set; }
		public string? Search { get; set; }
	}
}
