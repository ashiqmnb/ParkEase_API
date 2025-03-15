
namespace UserService.Application.Common.DTOs.Auth
{
	public class CompanyLoginResDTO
	{
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? Profile { get; set; }
		public string? Token { get; set; }
		public string? SubscriptionStatus { get; set; }
		public string? Type { get; set; }
		public int Coins { get; set; }
		public bool IsBlocked { get; set; } = false;
	}
}
