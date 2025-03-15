using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Entity
{
	public class User : AuditableEntity
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is Required")]
		[StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string? Email { get; set; }

		public string? Phone {  get; set; }

		[Required(ErrorMessage = "Password is required")]
		[MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
		[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
			ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
		public string? Password { get; set; }

		[Required(ErrorMessage = "UserName is required")]
		public string? UserName { get; set; }

		public int Coins { get; set; } = 0;

		public bool IsBlocked { get; set; } = false;

		public string? Profile { get; set; }
	}
}
