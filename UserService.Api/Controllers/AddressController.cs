using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Address.Command.AddAddress;
using UserService.Application.Address.Command.EditLatLong;
using UserService.Application.Common.ApiResponse;
using UserService.Application.Common.DTOs.Address;

namespace UserService.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AddressController : ControllerBase
	{
		private readonly ISender _mediater;

		public AddressController(ISender mediater)
		{
			_mediater = mediater;
		}


		[HttpPost]
		public async Task<IActionResult> AddAddress([FromBody] AddAddressDTO addAddressDto)
		{
			try
			{
				string companyId = HttpContext.Items["UserId"].ToString();
				var res = await _mediater.Send(new AddAddressCommand(addAddressDto, companyId));
				if (res) return Ok(new ApiResponse<string>(200, "Success", "Address Added Succesfully"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}

		[HttpPatch("{addressId}")]
		public async Task<IActionResult> EditLatLong(string addressId, double latitude, double longitude)
		{
			try
			{
				var res = await _mediater.Send(new EditLatLongCommand 
					{ 
						AddressId = addressId,
						Latitude = latitude,
						Longitude = longitude
					}
				);

				if (res) return Ok(new ApiResponse<string>(200, "Success", "Address Edited Succesfully"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}


		
	}
}
