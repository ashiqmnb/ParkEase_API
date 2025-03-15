using MediatR;
using UserService.Domain.Entity;
using UserService.Domain.Repository;

namespace UserService.Application.Auth.Command.VerifyEmail
{
	public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, bool>
	{
		private readonly IAuthRepo _authRepo;

		public VerifyEmailCommandHandler(IAuthRepo authRepo)
		{
			_authRepo = authRepo;
		}

		public async Task<bool> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var users = await _authRepo.GetVerifyUsers();

				var user = users.FirstOrDefault(vu => vu.Email == request.Email && vu.Otp == request.Otp);

				if (user == null)
				{
					throw new Exception("User not found");
				}

				TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
				if (currentTime <= user.Expire_time)
				{
					var verifiedUser = new User
					{
						Name = user.Name,
						Email = user.Email,
						Password = user.Password,
						Profile = null,
						UserName = $"{user.Name.Replace(" ", "").ToLower()}{new Random().Next(1000, 9999)}",
						CreatedBy = "Initial Create",
						CreatedOn = DateTime.UtcNow,
						UpdatedBy = "Initial Create",
						UpdatedOn = DateTime.UtcNow
					};

					await _authRepo.AddUser(verifiedUser);
					await _authRepo.RemoveVerifyUser(user);

					return true;
				}

				return false;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);	
			}
		}
	}
}
