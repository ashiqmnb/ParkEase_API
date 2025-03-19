using MediatR;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Repository;

namespace UserService.Application.Companies.Command.UpdateProfile
{
	public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, bool>
	{
		private readonly ICompanyRepo _companyRepo;
		private readonly ICloudinaryService _cloudinaryService;

		public UpdateProfileCommandHandler(ICompanyRepo companyRepo, ICloudinaryService cloudinaryService)
		{
			_companyRepo = companyRepo;
			_cloudinaryService = cloudinaryService;
		}

		public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.ProfileUpdateDto == null) throw new Exception("Update detsils not found");

				var company = await _companyRepo.GetCompanyById(request.CompanyId);
				if (company == null) throw new Exception("Company not found");

				if(request.ProfileUpdateDto.Name != null) company.Name = request.ProfileUpdateDto.Name;
				if(request.ProfileUpdateDto.Description != null) company.Description = request.ProfileUpdateDto.Description;
				if(request.ProfileUpdateDto.Phone != null) company.Phone = request.ProfileUpdateDto.Phone;
				if(request.ProfileUpdateDto.Profile != null)
				{
					var profileUrl = await _cloudinaryService.UploadProfileImageAsync(request.ProfileUpdateDto.Profile);
					if (profileUrl != null)
					{
						company.Profile = profileUrl;
					}
				}

				await _companyRepo.SaveChangesAsyncCustom();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
