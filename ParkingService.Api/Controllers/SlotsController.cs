using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingService.Application.Common.ApiResponse;
using ParkingService.Application.Common.DTO.Slot;
using ParkingService.Application.Slot.Command;
using ParkingService.Application.Slot.Command.AddSlot;
using ParkingService.Application.Slot.Command.ChackInSlot;
using ParkingService.Application.Slot.Command.CheckOutSlot;
using ParkingService.Application.Slot.Query.GetReservationsParkings;
using ParkingService.Application.Slot.Query.GetSlotsByCompanyId;
using ParkingService.Application.Slot.Query.GetSlotsCountByCompanyId;

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


		//[Authorize]
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


		[HttpGet("count")]
		public async Task<IActionResult> GetSlotsCountByCompanyId([FromQuery] string? companyId)
		{
			try
			{
				if (companyId == null)
				{
					companyId = HttpContext.Items["UserId"]?.ToString();
				}

				var res = await _mediater.Send(new GetSlotsCountByCompanyIdCommand { CompanyId = companyId });
				if (res != null) return Ok(new ApiResponse<SlotsCountResDTO>(200, "Success", res));
				return BadRequest(new ApiResponse<string>(400, "Failed", "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}


		[HttpPatch("check-in/{slotId}")]
		public async Task<IActionResult> CheckInSlot(string slotId)
		{
			try
			{
					var userId = HttpContext.Items["UserId"]?.ToString();
				var res = await _mediater.Send(new CheckInSlotCommand
				{
					SlotId = slotId,
					UserId = userId
				});

				if(res) return Ok(new ApiResponse<string>(200, "Success", "Checked in into slot successfull"));
				return BadRequest(new ApiResponse<string>(400, "Failed", "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}


		[HttpPatch("check-out")]
		public async Task<IActionResult> CheckOutSlot([FromBody]CheckOutSlotDTO checkOutSlotDto)
		{
			try
			{
				var userId = HttpContext.Items["UserId"]?.ToString();
				var res = await _mediater.Send(new CheckOutSlotCommand(userId, checkOutSlotDto));
				if (res) return Ok(new ApiResponse<string>(200, "Success", "Checked out slot successfull"));
				return BadRequest(new ApiResponse<string>(400, "Failed", "Something went wrong"));
			}
			catch(Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}


		[HttpGet("reserve-parking")]
		public async Task<IActionResult> GetReservationsParkings()
		{
			try
			{
				var userId = HttpContext.Items["UserId"]?.ToString();
				var res = await _mediater.Send(new GetReservationsParkingsCommand { UserId = userId });
				if(res != null) return Ok(new ApiResponse<ReservParkResDTO>(200, "Success", res));
				return BadRequest(new ApiResponse<string>(400, "Failed", "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}
	}
}
