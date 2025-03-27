namespace ParkingService.Application.Common.DTO.History
{
	public class UserHistoryResDTO
	{
		public string SlotName { get; set; }
		public string CompanyId { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime? CheckOut { get; set; }
		public string? VehicleNumber { get; set; }
	}
}
