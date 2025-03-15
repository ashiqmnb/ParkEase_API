using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Domain.Entity
{
	public class VerifyUser
	{
		public Guid Id { get; set; }

		[Required(ErrorMessage = "Name is Required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage = "Invalid Email Format")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Passsword is Required")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Otp is Required")]
		public int Otp { get; set; }

		[Required(ErrorMessage = "Expiry time is Required")]
		public TimeOnly Expire_time { get; set; }
	}
}
