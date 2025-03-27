using MediatR;
using UserService.Application.Common.DTOs.User;
using UserService.Domain.Repository;

namespace UserService.Application.Users.Query.GetUsers
{
	public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, UserPageResDTO>
	{
		private readonly IUserRepo _userRepo;

		public GetUsersQueryHandler(IUserRepo userRepo)
		{
			_userRepo = userRepo;
		}

		public async Task<UserPageResDTO> Handle(GetUsersQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var users = await _userRepo.GetAllUsers();

				if (!string.IsNullOrEmpty(request.QueryParams.Search))
				{
					users = users
						.Where(u => u.Name.ToLower().Contains(request.QueryParams.Search, StringComparison.OrdinalIgnoreCase))
						.ToList();
				}

				int totalPage = (int)Math.Ceiling((double)users.Count / request.QueryParams.PageSize);

				users = users
					.Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize)
					.Take(request.QueryParams.PageSize)
					.ToList();

				var usersRes = users.Select(u => new UserResDTO
				{
					Id = u.Id.ToString(),
					Name = u.Name,
					Email = u.Email,
					coins = u.Coins,
					AddedDate = u.CreatedOn,
					IsBlocked = u.IsBlocked
				}).ToList();

				var res = new UserPageResDTO
				{
					CurrentPage = request.QueryParams.PageNumber,
					TotalPages = totalPage,
					Users = usersRes
				};

				return res;

			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException.Message ?? ex.Message);
			}
		}
	}
}
