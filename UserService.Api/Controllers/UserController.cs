using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Common.ApiResponse;

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


		//[Authorize]
		//[HttpGet]
		//public async Task<IActionResult> GetUserById([FromQuery] string? userId)
		//{
		//	try
		//	{
		//		if(userId == null)
		//		{
		//			userId = HttpContext.Items["UserId"]?.ToString();
		//		}

		//		return Ok(userId);
		//	}
		//	catch (Exception ex)
		//	{
		//		return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
		//	}
		//}

	}
}
