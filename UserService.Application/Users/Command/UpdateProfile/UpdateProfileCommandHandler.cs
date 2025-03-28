using MediatR;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Entity;
using UserService.Domain.Repository;

namespace UserService.Application.Users.Command.UpdateProfile
{
	public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, bool>
	{
		private readonly IUserRepo _userRepo;
		private readonly ICloudinaryService _cloudinaryService;

		public UpdateProfileCommandHandler(IUserRepo userRepo, ICloudinaryService cloudinaryService)
		{
			_userRepo = userRepo;
			_cloudinaryService = cloudinaryService;
		}

		public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.ProfileUpdateDto == null) throw new Exception("Update detsils not found");

				var user = await _userRepo.GetUserById(request.UserId);
				if (user == null) throw new Exception("User not found");

				if(request.ProfileUpdateDto.Name != null) user.Name = request.ProfileUpdateDto.Name;
				if(request.ProfileUpdateDto.Phone != null) user.Phone = request.ProfileUpdateDto.Phone;
				if(request.ProfileUpdateDto.Profile != null)
				{
					var profiileUrl = await _cloudinaryService.UploadProfileImageAsync(request.ProfileUpdateDto.Profile);
					if(profiileUrl != null) user.Profile = profiileUrl;
				}
				await _userRepo.SaveChangesAsyncCustom();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException.Message ?? ex.Message);
			}
		}
	}
}
