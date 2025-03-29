namespace UserService.Application.Common.DTOs.Company
{
	public class SubscriptionSummaryDTO
	{
		public string Status { get; set; }
		public DateTime? SubscriptionStartDate { get; set; }
		public DateTime? SubscriptionExpiryDate { get; set; }
		public int? SubscriptionDurationInDays { get; set; }
	}
}
