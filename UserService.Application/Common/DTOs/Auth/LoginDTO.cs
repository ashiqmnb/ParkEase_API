﻿

using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Common.DTOs.Auth
{
	public class LoginDTO
	{
		[Required(ErrorMessage = "Email is required.")]
		[EmailAddress(ErrorMessage = "Invalid email address.")]
		public string Email { get; set; }


		[Required(ErrorMessage = "Password is required.")]
		[MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
		public string Password { get; set; }
	}
}
