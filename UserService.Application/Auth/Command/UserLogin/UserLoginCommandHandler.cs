using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Application.Common.DTOs.Auth;
using UserService.Domain.Entity;
using UserService.Domain.Repository;

namespace UserService.Application.Auth.Command.UserLogin
{
	public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, UserLoginResDTO>
	{
		private readonly IAuthRepo _authRepo;

		public UserLoginCommandHandler(IAuthRepo authRepo)
		{
			_authRepo = authRepo;
		}

		public async Task<UserLoginResDTO> Handle(UserLoginCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _authRepo.GetUserByEmail(request.loginDTO.Email);
				if (user == null)
				{
					throw new UnauthorizedAccessException("User Not Found");
				}

				bool isValidPassword = BCrypt.Net.BCrypt.Verify(request.loginDTO.Password, user.Password);
				if (!isValidPassword)
				{
					throw new UnauthorizedAccessException("Invalid Password");
				}

				if (user.IsBlocked == true)
				{
					throw new UnauthorizedAccessException("User is Blocked");
				}

				string jwtToken = GenerateJwtToken(user);

				var loginRes = new UserLoginResDTO
				{
					Name = user.Name,
					UserName = user.UserName,
					Email = user.Email,
					Token = jwtToken,
					Profile = user.Profile,
					Coins = user.Coins,
					Phone = user.Phone,
					IsBlocked = user.IsBlocked,
				};

				return loginRes;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		private string GenerateJwtToken(User user)
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
				new Claim(ClaimTypes.Role,"User")
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
