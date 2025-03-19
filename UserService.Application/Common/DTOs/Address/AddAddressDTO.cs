using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Common.DTOs.Address
{
	public class AddAddressDTO
	{
		[Required(ErrorMessage = "Place is Required")]
		public string? Place { get; set; }

		[Required(ErrorMessage = "District is Required")]
		public string? District { get; set; }


		[Required(ErrorMessage = "State is Required")]
		public string? State { get; set; }

		[Required(ErrorMessage = "Postalcode is Required")]
		public int PostalCode { get; set; }
	}
}
