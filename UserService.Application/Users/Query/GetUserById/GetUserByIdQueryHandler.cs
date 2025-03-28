using MediatR;
using UserService.Application.Common.DTOs.User;
using UserService.Domain.Repository;

namespace UserService.Application.Users.Query.GetUserById
{
	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserByIdResDTO>
	{
		private readonly IUserRepo _userRepo;

		public GetUserByIdQueryHandler(IUserRepo userRepo)
		{
			_userRepo = userRepo;
		}

		public async Task<UserByIdResDTO> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _userRepo.GetUserById(request.UserId);

				if (user == null) throw new Exception("User not found in this User Id");

				var userRes = new UserByIdResDTO
				{
					Id = user.Id.ToString(),
					Name = user.Name,
					UserName = user.Name,
					Coins = user.Coins,
					Email = user.Email,
					Phone = user.Phone,
					IsBlocked = user.IsBlocked,
					Profile = user.Profile,
				};

				return userRes;
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ??  ex.Message);
			}
		}
	}
}
