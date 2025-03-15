using MediatR;
using UserService.Domain.Repository;

namespace UserService.Application.Auth.Command.UserResetPw
{
	public class UserResetPwCommandHandler : IRequestHandler<UserResetPwCommand, bool>
	{
		private readonly IAuthRepo _authRepo;

		public UserResetPwCommandHandler(IAuthRepo authRepo)
		{
			_authRepo = authRepo;
		}

		public async Task<bool> Handle(UserResetPwCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _authRepo.GetUserByEmail(request.userResetPwDto.Email);
				if (user == null) throw new Exception("User not found");

				// hash new password
				string salt = BCrypt.Net.BCrypt.GenerateSalt();
				string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.userResetPwDto.NewPassword, salt);

				// update password
				user.Password = hashPassword;
				await _authRepo.SaveChangesAsyncCustom();

				return true;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ??  ex.Message);
			}
		}
	}
}
