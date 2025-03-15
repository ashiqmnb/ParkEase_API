using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Auth.Command.AdminLogin;
using UserService.Application.Auth.Command.ComapnyForgotPw;
using UserService.Application.Auth.Command.CompanyLogin;
using UserService.Application.Auth.Command.CompanyResetPw;
using UserService.Application.Auth.Command.RegisterCompany;
using UserService.Application.Auth.Command.UserForgotPw;
using UserService.Application.Auth.Command.UserLogin;
using UserService.Application.Auth.Command.UserRegister;
using UserService.Application.Auth.Command.UserResetPw;
using UserService.Application.Auth.Command.VerifyEmail;
using UserService.Application.Auth.Command.VerifyOtpForResetPw;
using UserService.Application.Common.ApiResponse;
using UserService.Application.Common.DTOs.Auth;


namespace UserService.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly ISender _mediater;

		public AuthController(ISender mediater)
		{
			_mediater = mediater;
		}


		[HttpPost("admin/login")]
		public async Task<IActionResult> AdminLogin([FromBody]LoginDTO loginDTO)
		{
			try
			{
				var res = await _mediater.Send(new AdminLoginCommand(loginDTO));
				if (res != null) return Ok(new ApiResponse<AdminLoginResDTO>(200, "Success", res));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
			}
			
		}


		[HttpPost("user/register")]
		public async Task<IActionResult> UserRegister([FromBody] UserRegDTO userRegDTO)
		{
			try
			{
				var res = await _mediater.Send(new UserRegisterCommand(userRegDTO));
				if (res) return Ok(new ApiResponse<string>(200, "Success", "User Registered Successfully"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
			}
		}


		[HttpPost("user/verify-email")]
		public async Task<IActionResult> VerifyEmail(string email, int otp)
		{
			try
			{
				var res = await _mediater.Send(new VerifyEmailCommand { Email = email, Otp = otp});
				if (res != null) return Ok(new ApiResponse<string>(200, "Success", "Email Verified Successfully"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
			}
		}


		[HttpPost("user/login")]
		public async Task<IActionResult> UserLogin([FromBody] LoginDTO loginDTO)
		{
			try
			{
				var loginRes = await _mediater.Send(new UserLoginCommand(loginDTO));
				if (loginRes != null) return Ok(new ApiResponse<UserLoginResDTO>(200, "Success", loginRes));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
			}
		}


		[HttpPost("user/forgot-password")]
		public async Task<IActionResult> UserForgotPw(string email)
		{
			try
			{
				var res = await _mediater.Send(new UserForgotPwCommand { Email = email });
				if (res) return Ok(new ApiResponse<string>(200, "Success", "OTP sended for reset password"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
			}
		}


		[HttpPost("user/verify-otp-reset-password")]
		public async Task<IActionResult> UserVerifyOtpForResetPw(string email, int otp)
		{
			try
			{
				var res = await _mediater.Send(new VerifyOtpForResetPwCommand { Email=email, Otp=otp });
				if (res) return Ok(new ApiResponse<string>(200, "Success", "OTP verified for reset password"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
			}
		}


		[HttpPatch("user/reset-password")]
		public async Task<IActionResult> UserResetPw([FromBody] ResetPwDTO userResetPwDto)
		{
			try
			{
				var res = await _mediater.Send(new UserResetPwCommand(userResetPwDto));
				if (res) return Ok(new ApiResponse<string>(200, "Success", "Password reset completed"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
			}
		}


		[HttpPost("comapny/register")]
		public async Task<IActionResult> ComapnyRegister([FromBody] CompanyRegDTO companyRegDto)
		{
			try
			{
				var res = await _mediater.Send(new CompanyRegisterCommand(companyRegDto));
				if (res) return Ok(new ApiResponse<string>(200, "Success", "Comapany added succssfully"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
			}
		}


		[HttpPost("comapny/login")]
		public async Task<IActionResult> ComapnyLogin([FromBody] LoginDTO loginDto)
		{
			try
			{
				var res = await _mediater.Send(new CompanyLoginCommand(loginDto));
				if (res != null) return Ok(new ApiResponse<CompanyLoginResDTO>(200, "Success", res));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
			}
		}


		[HttpPost("company/forgot-password")]
		public async Task<IActionResult> ComapanyForgotPw(string email)
		{
			try
			{
				var res = await _mediater.Send(new ComapnyForgotPwCommand { Email = email });
				if (res) return Ok(new ApiResponse<string>(200, "Success", "OTP sended for reset password"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
			}
		}


		[HttpPatch("company/reset-password")]
		public async Task<IActionResult> CompanyResetPw([FromBody] ResetPwDTO companyResetPwDto)
		{
			try
			{
				var res = await _mediater.Send(new CompanyResetPwCommand(companyResetPwDto));
				if (res) return Ok(new ApiResponse<string>(200, "Success", "Password reset completed"));
				return BadRequest(new ApiResponse<string>(400, "Failed", null, "Something went wrong"));
			}
			catch (Exception ex)
			{
				return BadRequest(new ApiResponse<string>(400, "Failed", null, ex.Message));
			}
		}
	}
}
