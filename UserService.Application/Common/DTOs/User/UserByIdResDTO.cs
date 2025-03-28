namespace UserService.Application.Common.DTOs.User
{
	public class UserByIdResDTO
	{
		public string Id { get; set; }
		public string? Name { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? UserName { get; set; }
		public int Coins { get; set; } = 0;
		public bool IsBlocked { get; set; } = false;
		public string? Profile { get; set; }
	}
}
