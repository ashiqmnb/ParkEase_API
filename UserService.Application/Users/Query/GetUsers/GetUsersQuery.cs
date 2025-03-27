using MediatR;
using UserService.Application.Common.DTOs.User;

namespace UserService.Application.Users.Query.GetUsers
{
	public class GetUsersQuery : IRequest<UserPageResDTO>
	{
		public UserQueryParams QueryParams { get; set; }
	}
}
