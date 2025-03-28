using MediatR;
using UserService.Application.Common.DTOs.User;

namespace UserService.Application.Users.Query.GetUserById
{
	public class GetUserByIdQuery : IRequest<UserByIdResDTO>
	{ 
		public string UserId { get; set; }
	}
}
