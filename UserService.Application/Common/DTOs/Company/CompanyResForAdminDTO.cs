namespace UserService.Application.Common.DTOs.Company
{
	public class CompanyResForAdminDTO
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Type { get; set; }
		public string Status { get; set; }
		public DateTime AddedDate { get; set; }
		public bool IsBlocked { get; set; }
	}
}
