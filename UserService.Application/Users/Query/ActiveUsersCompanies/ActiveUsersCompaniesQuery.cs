using MediatR;
using UserService.Application.Common.DTOs.User;

namespace UserService.Application.Users.Query.ActiveUsersCompanies
{
	public class ActiveUsersCompaniesQuery : IRequest<ActiveUsersCompaniesDTO>
	{
	}
}
