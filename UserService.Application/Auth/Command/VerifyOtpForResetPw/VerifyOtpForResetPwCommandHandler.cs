using MediatR;
using UserService.Domain.Repository;

namespace UserService.Application.Auth.Command.VerifyOtpForResetPw
{
	public class VerifyOtpForResetPwCommandHandler : IRequestHandler<VerifyOtpForResetPwCommand, bool>
	{
		private readonly IAuthRepo _authRepo;

		public VerifyOtpForResetPwCommandHandler(IAuthRepo authRepo)
		{
			_authRepo = authRepo;
		}

		public async Task<bool> Handle(VerifyOtpForResetPwCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var verifyUsers = await _authRepo.GetVerifyUsers();
				var existinVerifyUser = verifyUsers.FirstOrDefault(vu => vu.Email == request.Email && vu.Otp == request.Otp);

				if (existinVerifyUser == null) throw new Exception("User not found");

				TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
				if (currentTime <= existinVerifyUser.Expire_time)
				{
					await _authRepo.RemoveVerifyUser(existinVerifyUser);
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
