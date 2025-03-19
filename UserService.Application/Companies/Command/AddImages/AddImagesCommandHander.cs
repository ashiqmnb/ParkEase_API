using MediatR;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Repository;

namespace UserService.Application.Companies.Command.AddImages
{
	public class AddImagesCommandHander : IRequestHandler<AddImagesCommand, bool>
	{
		private readonly ICompanyRepo _companyRepo;
		private readonly ICloudinaryService _cloudinaryService;


		public AddImagesCommandHander(ICompanyRepo companyRepo, ICloudinaryService cloudinaryService)
		{
			_companyRepo = companyRepo;
			_cloudinaryService = cloudinaryService;
		}
		public async Task<bool> Handle(AddImagesCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var company = await _companyRepo.GetCompanyById(request.CompanyId);
				if (company == null) throw new Exception("Company not found");

				var imageUrls = await _cloudinaryService.UploadCompanyImagesAsync(request.Images);

				company.Images = imageUrls;
				await _companyRepo.SaveChangesAsyncCustom();
				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ??  ex.Message);	
			}
			throw new NotImplementedException();

		}
	}
}
