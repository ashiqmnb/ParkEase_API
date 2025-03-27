using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Coin.Command.AddSubscription;
using PaymentService.Application.Coin.Command.ConfirmPayment;
using PaymentService.Application.Coin.Command.RazorOrderCreate;
using PaymentService.Application.Coin.Command.ReserveSlot;
using PaymentService.Application.Common.ApiResponse;
using PaymentService.Application.Common.DTOs;
using Razorpay.Api;

namespace PaymentService.Api.Controllers
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


		[HttpPost("create-payment")]
		public async Task<IActionResult> CreatePayment(int amount)
		{
			try
			{
				string userId = HttpContext.Items["UserId"].ToString();
				var orderId = await _mediater.Send(
					new CreatePaymentCommand
					{ 
						UserId = userId, 
						Amount = amount 
					});
				if (orderId != null) return Ok(new ApiResponse<string>(200, "Success", orderId));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}



		[HttpPost("confirm-payment")] 
		public async Task<IActionResult> ConfirmPayment([FromBody] PaymentDTO paymentDto)
		{
			try
			{
				var res = await _mediater.Send(new ConfirmPaymentCommand(paymentDto));
				if(res) return Ok(new ApiResponse<string>(200, "Success", "Payment confirmed successfully"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}


		[HttpPost("add-subscription")]
		//[Authorize(Roles = "Company")]
		public async Task<IActionResult> AddSubscription(int days)
		{
			try
			{
				var companyId = HttpContext.Items["UserId"].ToString();
				var res = await _mediater.Send(new AddSubscriptionCommand
				{
					CompanyId = companyId,
					Days = days
				});

				if (res) return Ok(new ApiResponse<string>(200, "Success", "Subscription added successfully"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}

		[HttpPost("reserve-slot")]
		//[Authorize(Roles = "User")]
		public async Task<IActionResult> ReserveSlot([FromBody] ReserveSlotDTO reserveSlotDto)
		{
			try
			{

				var userId = HttpContext.Items["UserId"].ToString();

				var res = await _mediater.Send(new ReserveSlotCommand(userId, reserveSlotDto));

				if (res) return Ok(new ApiResponse<string>(200, "Success", "Slot reserved successfully"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}
	}
}
