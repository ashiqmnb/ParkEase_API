using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingService.Application.Common.ApiResponse;
using ParkingService.Application.Common.DTO.History;
using ParkingService.Application.History.Query.GetHistoryByUserId;

namespace ParkingService.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HistoryController : ControllerBase
	{
		private readonly ISender _mediater;

		public HistoryController(ISender mediater)
		{
			_mediater = mediater;
		}

		[HttpGet("userId/{pageNumber}/{pageSize}")]
		public async Task<IActionResult> GetHistoryByUserId(int pageNumber = 1, int pageSize = 10)
		{
			try
			{
				string userId = HttpContext.Items["UserId"].ToString();
				var histories = await _mediater.Send(new GetHistoryByUserIdQuery { 
					UserID = userId, 
					PageNumber = pageNumber,
					PageSize = pageSize
				});
				if (histories != null) return Ok(new ApiResponse<UserHistoryPageResDTO>(200, "Success", histories));
					return BadRequest(new ApiResponse<string>(400, "Failed", "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}

		}
	}
}