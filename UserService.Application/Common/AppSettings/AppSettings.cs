namespace UserService.Application.Common.AppSettings
{
    public class AppSettings
    {
        public string DbConnectionString { get; set; }
        public string JwtSecretKey { get; set; }


        // For email services
        public string EmailHost { get; set; }
		public int EmailPort { get; set; }
		public string EmailUsername { get; set; }
		public string EmailPassword { get; set; }

	}
}
