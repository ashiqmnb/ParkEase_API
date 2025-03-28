﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Common.ApiResponse;
using PaymentService.Application.Common.DTOs.Transaction;
using PaymentService.Application.Transaction.Query.GetPayments;
using PaymentService.Application.Transaction.Query.TransByCompanyId;

namespace PaymentService.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private readonly ISender _mediater;

		public TransactionController(ISender mediater)
		{
			_mediater = mediater;
		}


		[HttpGet("companyId/{pageNumber}/{pageSize}")]
		public async Task<IActionResult> TransByCompanyId(int pageNumber = 1, int pageSize = 10)
		{
			try
			{
				string companyId = HttpContext.Items["UserId"].ToString();
				var transactions = await _mediater.Send(new TransByCompanyIdQuery { 
					CompanyId = companyId,
					PageNumber = pageNumber,
					PageSize = pageSize
				});

				if (transactions != null) return Ok(new ApiResponse<CompanyPageTransResDTO>(200, "Success", transactions));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}


		[HttpGet("payments/{pageNumber}/{pageSize}")]
		//[Authorize]
		public async Task<IActionResult> GetPayments(int pageNumber = 1, int pageSize = 10)
		{
			try
			{
				var payments = await _mediater.Send(new GetPaymentsQuery
				{
					PageNumber = pageNumber,
					PageSize = pageSize
				});

				if (payments != null) return Ok(new ApiResponse<PaymentPageResDTO>(200, "Success", payments));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return StatusCode(500, new ApiResponse<string>(500, "Failed", null, ex.Message));
			}
		}

	}
}
