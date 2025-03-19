namespace UserService.Application.Common.DTOs.Company
{
	public class CompanyByIdResDTO
	{
		public string Name { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? Description { get; set; }
		public string? Profile { get; set; }
		public List<string>? Images { get; set; }
		public int Coins { get; set; }
		public string? SubscriptionStatus { get; set; }
		public string? Type { get; set; }
		public bool IsBlocked { get; set; }
		public Guid? AddressId { get; set; }
		public string? District { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public string? Place { get; set; }
		public int? PostalCode { get; set; }
		public string? State { get; set; }
	}
}
