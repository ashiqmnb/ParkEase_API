using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using UserService.Application.Common.ApiResponse;
using UserService.Application.Common.DTOs.User;
using UserService.Application.Users.Command.BlockUnblock;
using UserService.Application.Users.Command.UpdateProfile;
using UserService.Application.Users.Query.GetUserById;
using UserService.Application.Users.Query.GetUsers;

namespace UserService.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly ISender _mediater;

		public UserController(ISender mediater)
		{
			_mediater = mediater;
		}


		[HttpGet]
		public async Task<IActionResult> GetUsers([FromQuery] UserQueryParams queryParams)
		{
			try
			{
				var users = await _mediater.Send(new GetUsersQuery
				{
					QueryParams = queryParams
				});

				if (users != null) return Ok(new ApiResponse<UserPageResDTO>(200, "Success", users));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}



		[HttpGet("userId")]
		[Authorize(Roles = "Admin, User")]
		public async Task<IActionResult> GetUserById([FromQuery] string? userId)
		{
			try
			{
				if(userId == null)
				{
					userId = HttpContext.Items["UserId"]?.ToString();
				}
				var user = await _mediater.Send(new GetUserByIdQuery { UserId = userId });
				if(user != null) return Ok(new ApiResponse<UserByIdResDTO>(200, "Success", user));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch(Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}



		[HttpPatch("blockUnblock/{userId}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> BlockUnblock(string userId)
		{
			try
			{
				var res = await _mediater.Send(new BlockUnblockCommand { UserId = userId });
				if (res != null) return Ok(new ApiResponse<string>(200, "Success", res));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}



		[HttpPatch("profile-update")]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> UpdateProfile([FromForm] UserProfileUpdateDTO updateProfileDto)
		{
			try
			{
				string userId = HttpContext.Items["UserId"]?.ToString();
				var res = await _mediater.Send(new UpdateProfileCommand(updateProfileDto, userId));
				if (res) return Ok(new ApiResponse<string>(200, "Success", "Profile Updated Successfully"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}
	}
}