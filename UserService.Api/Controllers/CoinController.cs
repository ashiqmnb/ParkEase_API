using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Coin.Query.GetCoinStatus;
using UserService.Application.Coin.Query.GetCurrentCoins;
using UserService.Application.Common.ApiResponse;
using UserService.Application.Common.DTOs.Address;

namespace UserService.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoinController : ControllerBase
	{
		private readonly ISender _mediater;

		public CoinController(ISender mediater)
		{
			_mediater = mediater;
		}


		[HttpGet("coin-status")]
		public async Task<IActionResult> GetCoinStatus()
		{
			try
			{
				var res = await _mediater.Send(new GetCoinStatusCommand());
				if (res != null) return Ok(new ApiResponse<CoinResDTO>(200, "Success", res));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}


		[HttpGet("current-coins")]
		public async Task<IActionResult> GetCurrentCoins()
		{
			try
			{
				string id = HttpContext.Items["UserId"].ToString();
				var coins = await _mediater.Send(new GetCurrentCoinsCommand { Id = id});

				if (coins != null) return Ok(new ApiResponse<int>(200, "Success", coins));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));

			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}
	}
}
