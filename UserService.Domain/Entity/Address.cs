using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Entity
{
	public class Address : AuditableEntity
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "District is Required")]
		public string? District { get; set; }
		[Required(ErrorMessage = "Latitude is Required")]
		public double? Latitude { get; set; }

		[Required(ErrorMessage = "Longitude is Required")]
		public double? Longitude { get; set; }

		[Required(ErrorMessage = "Place is Required")]
		public string? Place { get; set; }

		[Required(ErrorMessage = "Postalcode is Required")]
		public int PostalCode { get; set; }

		[Required(ErrorMessage = "State is Required")]
		public string? State { get; set; }

		public virtual Company Company { get; set; }

	}
}
