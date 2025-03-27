namespace UserService.Application.Common.DTOs.User
{
	public class UserQueryParams
	{
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 8;
		public string? Search { get; set; }
	}
}
