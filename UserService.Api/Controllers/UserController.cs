using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Common.ApiResponse;
using UserService.Application.Common.DTOs.User;
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
		public async Task<IActionResult> GetUsers([FromQuery]UserQueryParams queryParams)
		{
			try
			{
				var users = await _mediater.Send(new GetUsersQuery
				{
					QueryParams = queryParams
				});

				if(users != null) return Ok(new ApiResponse<UserPageResDTO>(200, "Success", users));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}

	}
}