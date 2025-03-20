using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingService.Application.Common.ApiResponse;
using ParkingService.Application.Common.DTO.Slot;
using ParkingService.Application.Slot.Command;
using ParkingService.Application.Slot.Query.GetSlotsByCompanyId;

namespace ParkingService.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SlotsController : ControllerBase
	{
		private readonly ISender _mediater;

		public SlotsController(ISender mediater)
		{
			_mediater = mediater;
		}

		//string userId = HttpContext.Items["UserId"].ToString();

		[HttpPost]
		[Authorize(Roles = "Company")]
		public async Task<IActionResult> AddSlot([FromBody]AddSlotDTO addSlotDto)
		{
			try
			{
				string companyId = HttpContext.Items["UserId"].ToString();

				var res	= await _mediater.Send(new AddSlotCommand(companyId, addSlotDto));
				if (res) return Ok(new ApiResponse<string>(200, "Success", "Slot added successfully"));
				return BadRequest(new ApiResponse<string>(400, "Failed", "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}


		[Authorize]
		[HttpGet]
		public async Task<IActionResult> GetSlotsByCompanyId([FromQuery] string? companyId)
		{
			try
			{
				if (companyId == null)
				{
					companyId = HttpContext.Items["UserId"]?.ToString();
				}

				var slots = await _mediater.Send(new GetSlotsByCompanyIdCommand { CompanyId = companyId });
				if(slots != null) return Ok(new ApiResponse<SlotResDetailsDTO>(200, "Success", slots));
				return BadRequest(new ApiResponse<string>(400, "Failed", "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}
	}
}
