using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Common.DTOs.Company
{
	public class CompanyResForUserDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Profile { get; set; }
		public string Type { get; set; }
		public string? District { get; set; }
		public string? Place { get; set; }
		public string? State { get; set; }
		public int PostalCode { get; set; }
	}
}
