using MediatR;

namespace UserService.Application.Companies.Command.BlockUnblock
{
	public class BlockUnblockCommand : IRequest<string>
	{
		public string CompanyId { get; set; }
	}
}
