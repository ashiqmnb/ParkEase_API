namespace ParkingService.Application.Common.DTO.Slot
{
	public class SlotResDetailsDTO
	{
		public int Total { get; set; }
		public int TwoWheeler { get; set; }
		public int FourWheeler { get; set; }
		public int Available { get; set; }
		public int Reserved { get; set; }
		public int Parked { get; set; }
		public List<SlotResDTO> Slots { get; set; }
	}
}
