using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Entity
{
	public class Company : AuditableEntity
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is Required")]
		[StringLength(25, ErrorMessage = "Name cannot exceed 25 characters.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string? Email { get; set; }

		public string? Phone { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
		[RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
			ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
		public string? Password { get; set; }

		public string? Description { get; set; }

		public string? Profile { get; set; }

		public List<string>? Images { get; set; }

		public int Coins { get; set; } = 0;

		public SubscriptionStatus SubscriptionStatus { get; set; } = SubscriptionStatus.Inactive;

		[Required(ErrorMessage = "Type is required")]
		public Type Type { get; set; }

		public bool IsBlocked { get; set; } = false;


		// to manage subscriptions
		public DateTime? SubscriptionStartDate { get; set; }
		public DateTime? SubscriptionExpiryDate { get; set; }
		public int? SubscriptionDurationInDays { get; set; }


		public Guid? AddressId { get; set; }

		public virtual Address? Address { get; set; }
	}

	public enum SubscriptionStatus
	{
		Active,
		Inactive
	}

	public enum Type
	{
		Customer,
		Public
	}
}
