namespace UserService.Application.Common.AppSettings
{
    public class AppSettings
    {
        public string DbConnectionString { get; set; }
        public string JwtSecretKey { get; set; }


		// SMTP mail configuration

		public string EmailHost { get; set; }
		public int EmailPort { get; set; }
		public string EmailUsername { get; set; }
		public string EmailPassword { get; set; }


		// Cloudinary configuration\

		public string CloudinaryCloudName { get; set; }
		public string CloudinaryApiKey { get; set; }
		public string CloudinaryApiSecrut { get; set; }

	}
}
