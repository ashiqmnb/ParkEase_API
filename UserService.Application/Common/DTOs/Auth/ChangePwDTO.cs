using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Common.DTOs.Auth
{
	public class ChangePwDTO
	{
		[Required(ErrorMessage = "Password is required.")]
		[MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
		public string CurrentPassword { get; set; }

		[Required(ErrorMessage = "Password is required.")]
		[MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
		public string NewPassword { get; set; }
	}
}
