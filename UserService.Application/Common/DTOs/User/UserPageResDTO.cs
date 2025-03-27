using UserService.Application.Common.DTOs.Company;

namespace UserService.Application.Common.DTOs.User
{
	public class UserPageResDTO
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public List<UserResDTO> Users{ get; set; }
	}
}
