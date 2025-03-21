﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Common.ApiResponse;
using UserService.Application.Common.DTOs.Auth;
using UserService.Application.Common.DTOs.Company;
using UserService.Application.Companies.Command.AddImages;
using UserService.Application.Companies.Command.UpdateProfile;
using UserService.Application.Companies.Query;

namespace UserService.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompanyController : ControllerBase
	{
		private readonly ISender _mediater;

		public CompanyController(ISender mediater)
		{
			_mediater = mediater;
		}


		[Authorize]
		[HttpGet]
		public async Task<IActionResult> GetCompanyById([FromQuery] string? companyId)
		{
			try
			{
				if (companyId == null)
				{
					companyId = HttpContext.Items["UserId"]?.ToString();
				}

				var company = await _mediater.Send(new GetCompanyByIdQuery { companyId = companyId });
				if(company != null) return Ok(new ApiResponse<CompanyByIdResDTO>(200, "Success", company));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}


		[HttpPatch("add-images")]
		public async Task<IActionResult> AddImages(List<IFormFile> images)
		{
			try
			{
				var companyId = HttpContext.Items["UserId"]?.ToString();
				var res = await _mediater.Send(new AddImagesCommand
				{
					CompanyId = companyId,
					Images = images
				});

				if (res) return Ok(new ApiResponse<string>(200, "Success", "Images Added Successfully"));	
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}


		[HttpPatch("profile-update")]
		public async Task<IActionResult> UpdateProfile([FromForm] CompanyProfileUpdateDTO updateProfile)
		{
			try
			{
				var companyId = HttpContext.Items["UserId"]?.ToString();

				var res = await _mediater.Send(new UpdateProfileCommand(updateProfile, companyId));
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
