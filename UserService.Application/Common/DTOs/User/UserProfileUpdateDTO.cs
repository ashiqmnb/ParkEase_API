using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace UserService.Application.Common.DTOs.User
{
	public class UserProfileUpdateDTO
	{
		[StringLength(25, ErrorMessage = "Name cannot exceed 25 characters.")]
		public string? Name { get; set; }
		public IFormFile? Profile { get; set; }
		public string? Phone { get; set; }
	}
}
