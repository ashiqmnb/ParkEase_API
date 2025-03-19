using Microsoft.AspNetCore.Http;

namespace UserService.Application.Common.Interfaces
{
	public interface ICloudinaryService
	{
		Task<List<string>> UploadCompanyImagesAsync(List<IFormFile> files);
		Task<string> UploadProfileImageAsync(IFormFile file);
	}
}
