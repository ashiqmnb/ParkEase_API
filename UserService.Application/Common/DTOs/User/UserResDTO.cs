namespace UserService.Application.Common.DTOs.User
{
	public class UserResDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public int coins { get; set; }
		public DateTime AddedDate { get; set; }
		public bool IsBlocked { get; set; }
	}
}
