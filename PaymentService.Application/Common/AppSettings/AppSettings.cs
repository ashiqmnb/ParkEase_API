namespace PaymentService.Application.Common.AppSettings
{
	public class AppSettings
	{
		public string DbConnectionString { get; set; }
		public string JwtSecretKey { get; set; }


		// Razorpay Config
		public string RazorPayKeyId { get; set; }
		public string RazorPayKeySecret { get;	set; }

	}
}
