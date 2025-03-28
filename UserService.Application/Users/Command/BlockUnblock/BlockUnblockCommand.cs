using MediatR;

namespace UserService.Application.Users.Command.BlockUnblock
{
	public class BlockUnblockCommand : IRequest<string>
	{
		public string UserId { get; set; }
	}
}
