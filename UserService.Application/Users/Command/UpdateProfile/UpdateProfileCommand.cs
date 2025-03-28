using MediatR;
using UserService.Application.Common.DTOs.User;

namespace UserService.Application.Users.Command.UpdateProfile
{
	public class UpdateProfileCommand : IRequest<bool>
	{
		public string UserId { get; set; }
		public UserProfileUpdateDTO ProfileUpdateDto { get; set; }

        public UpdateProfileCommand(UserProfileUpdateDTO profileUpdateDto, string userId)
        {
            UserId = userId;
            ProfileUpdateDto = profileUpdateDto;
        }
    }
}
