using MediatR;
using UserService.Domain.Repository;

namespace UserService.Application.Users.Command.BlockUnblock
{
	public class BlockUnblockCommandHandler : IRequestHandler<BlockUnblockCommand, string>
	{
		private readonly IUserRepo _userRepo;

		public BlockUnblockCommandHandler(IUserRepo userRepo)
		{
			_userRepo = userRepo;
		}

		public async Task<string> Handle(BlockUnblockCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _userRepo.GetUserById(request.UserId);
				if (user == null) throw new Exception("User not found");

				user.IsBlocked = !user.IsBlocked;
				await _userRepo.SaveChangesAsyncCustom();

				if (user.IsBlocked == true) return "Blocked user successfully";
				return "Unblocked user successfully";

			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
