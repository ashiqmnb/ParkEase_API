
using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Common.DTOs.Auth
{
	public class CompanyRegDTO
	{
		[Required(ErrorMessage = "Name is Required")]
		[StringLength(25, ErrorMessage = "Name cannot exceed 25 characters.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
		[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
			ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
		public string? Password { get; set; }

		[Required(ErrorMessage = "Type is required")]
		public string Type { get; set; }
	}
}
