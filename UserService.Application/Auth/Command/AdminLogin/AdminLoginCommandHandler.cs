using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Application.Common.DTOs.Auth;
using UserService.Domain.Entity;
using UserService.Domain.Repository;

namespace UserService.Application.Auth.Command.AdminLogin
{
	public class AdminLoginCommandHandler : IRequestHandler<AdminLoginCommand, AdminLoginResDTO>
	{
		private readonly IAuthRepo _authRepo;

		public AdminLoginCommandHandler(IAuthRepo authRepo)
		{
			_authRepo = authRepo;
		}

		public async Task<AdminLoginResDTO> Handle(AdminLoginCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var admins = await _authRepo.GetAdmin();

				var admin = admins.FirstOrDefault( a => a.Email == request.loginDto.Email);

				if (admin == null)
				{
					throw new Exception("Admin Not Found");
				}

				bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.loginDto.Password, admin.Password);
				if (!isValidPassword)
				{
					throw new Exception("Invalid Password");
				}

				string jwtToken = GenerateJwtToken(admin);

				var loginRes = new AdminLoginResDTO
				{
					Token = jwtToken,
					Coins = admin.Coins,
				};

				return loginRes;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		private string GenerateJwtToken(Admin admin)
		{
			if (admin == null)
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
					new Claim(ClaimTypes.NameIdentifier, admin.Id.ToString()),
					new Claim(ClaimTypes.Name, admin.Email),
					new Claim(ClaimTypes.Role,"Admin")
			   };

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.UtcNow.AddHours(1),
				signingCredentials: credentials
			);

			string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

			return jwtToken;
		}
	}
}
