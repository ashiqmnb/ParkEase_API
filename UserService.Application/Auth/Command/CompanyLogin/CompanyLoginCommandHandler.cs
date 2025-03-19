using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Application.Common.DTOs.Auth;
using UserService.Domain.Entity;
using UserService.Domain.Repository;

namespace UserService.Application.Auth.Command.CompanyLogin
{
	public class CompanyLoginCommandHandler : IRequestHandler<CompanyLoginCommand, CompanyLoginResDTO>
	{
		private readonly IAuthRepo _authRepo;

		public CompanyLoginCommandHandler(IAuthRepo authRepo)
		{
			_authRepo = authRepo;
		}
		async Task<CompanyLoginResDTO> IRequestHandler<CompanyLoginCommand, CompanyLoginResDTO>.Handle(CompanyLoginCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var company = await _authRepo.GetCompanyByEmail(request.LoginDto.Email);
				if (company == null)
				{
					throw new UnauthorizedAccessException("Comapany Not Found");
				}

				bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.LoginDto.Password, company.Password);
				if (!isValidPassword)
				{
					throw new UnauthorizedAccessException("Invalid Password");
				}

				if (company.IsBlocked == true)
				{
					throw new UnauthorizedAccessException("Company is Blocked");
				}

				var token = GenerateJwtToken(company);

				var loginRes = new CompanyLoginResDTO
				{
					Name = company.Name,
					Email = company.Email,
					Profile = company.Profile,
					Phone = company.Phone,
					Token = token,
					IsBlocked = company.IsBlocked,
					SubscriptionStatus = company.SubscriptionStatus.ToString(),
					Type = company.Type.ToString(),
					Coins = company.Coins
				};

				return loginRes;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ??  ex.Message);
			}
		}

		private string GenerateJwtToken(Company user)
		{
			if (user == null)
			{
				throw new UnauthorizedAccessException("Admin not found.");
			}
			var securityKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

			if (string.IsNullOrEmpty(securityKey))
			{
				throw new Exception("Jwt Secret key is Missing");
			}

			var key = Encoding.UTF8.GetBytes(securityKey);
			var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

			var claims = new[]
			   {
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.Email),
				new Claim(ClaimTypes.Role,"Company")
			};

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddHours(2),
				signingCredentials: credentials
			);

			string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

			return jwtToken;
		}
	}
}
