using MediatR;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Entity;
using UserService.Domain.Repository;

namespace UserService.Application.Auth.Command.UserForgotPw
{
	public class UserForgotPwCommandHandler : IRequestHandler<UserForgotPwCommand, bool>
	{
		private readonly IAuthRepo _authRepo;
		private readonly IEmailService _emailService;

		public UserForgotPwCommandHandler(IAuthRepo authRepo, IEmailService emailService)
		{
			_authRepo = authRepo;
			_emailService = emailService;
		}

		public async Task<bool> Handle(UserForgotPwCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _authRepo.GetUserByEmail(request.Email);
				if (user == null) throw new Exception("User not found");

				var verifyUsers = await _authRepo.GetVerifyUsers();
				var existinVerifyUser = verifyUsers.FirstOrDefault(vu => vu.Email == request.Email);

				if (existinVerifyUser != null)
				{
					await _authRepo.RemoveVerifyUser(existinVerifyUser);
				}

				// Generate otp 
				Random random = new Random();
				int otp = random.Next(100000, 1000000);

				string subject = "Your OTP for Email Verification";
				var body = $@"
				<!DOCTYPE html>
				<html>
				<head>
					<style>
						body {{ font-family: Arial, sans-serif; text-align: center; background-color: #f4f4f4; padding: 20px; }}
						.container {{ max-width: 500px; background: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); margin: auto; }}
						.otp {{ font-size: 24px; font-weight: bold; color: #333; padding: 10px; background: #f4f4f4; display: inline-block; border-radius: 5px; }}
						.footer {{ margin-top: 20px; font-size: 12px; color: #666; }}
					</style>
				</head>
				<body class='container'>
					<h2>ParkEase Email Verification</h2>
					<p>Use the following OTP to verify your email address:</p>
					<p class='otp'>{otp}</p>
					<p>This OTP is valid for 5 minutes. Do not share it with anyone.</p>
					<p class='footer'>If you didn’t request this, you can ignore this email.</p>
				</body>
				</html>";


				//sending email
				var res = await _emailService.SendEmail(request.Email, subject, body);

				if (res)
				{
					var currentTime = TimeOnly.FromDateTime(DateTime.Now);
					var expiryTime = currentTime.AddMinutes(5);

					// store data in verify user
					var userEntity = new VerifyUser();
					userEntity.Name = user.Name;
					userEntity.Email = user.Email;
					userEntity.Password = user.Password;
					userEntity.Otp = otp;
					userEntity.Expire_time = expiryTime;

					await _authRepo.AddVerifyUser(userEntity);

					return true;
				}

				return false;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
