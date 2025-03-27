namespace ParkingService.Application.Common.DTO.History
{
	public class UserHistoryPageResDTO
	{
		public int CurrentPage { get; set; }
		public int TotalPages { get; set; }
		public List<UserHistoryResDTO> Histories { get; set; }
	}
}
