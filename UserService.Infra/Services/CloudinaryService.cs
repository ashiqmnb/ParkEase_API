using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using UserService.Application.Common.AppSettings;
using UserService.Application.Common.Interfaces;

namespace UserService.Infrastructure.Services
{
	public class CloudinaryService : ICloudinaryService
	{

		private readonly Cloudinary _cloudinary;

		public CloudinaryService(IConfiguration configuration, AppSettings appSettings)
		{

			var cloudName = appSettings.CloudinaryCloudName;
			var apiKey = appSettings.CloudinaryApiKey;
			var apiSecret = appSettings.CloudinaryApiSecrut;

			if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
			{
				throw new Exception("Cloudinary configuration is missing or incomplete");
			}

			var account = new Account(cloudName, apiKey, apiSecret);
			_cloudinary = new Cloudinary(account);
		}


		public async Task<List<string>> UploadCompanyImagesAsync(List<IFormFile> files)
		{
			try
			{
				if (files == null || files.Count == 0)
				{
					throw new Exception("File Is Null or Empty");

				}

				var imageUrls = new List<string>();

				foreach (var file in files)
				{
					if (file.Length > 0)
					{
						using (var stream = file.OpenReadStream())
						{
							var uploadParams = new ImageUploadParams
							{
								File = new FileDescription(file.FileName, stream),
								Transformation = new Transformation().AspectRatio("4:3").Crop("fill")
							};

							var uploadResult = await _cloudinary.UploadAsync(uploadParams);


							if (uploadResult.Error != null)
							{
								throw new Exception($"Cloudinary upload error: {uploadResult.Error.Message}");
							}

							imageUrls.Add(uploadResult.SecureUrl?.ToString());
						}

					}
				}
				return imageUrls;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}

		public async Task<string> UploadProfileImageAsync(IFormFile file)
		{
			try
			{
				if (file == null || file.Length == 0) return null;

				using (var stream = file.OpenReadStream())
				{
					var uploadParams = new ImageUploadParams
					{
						File = new FileDescription(file.FileName, stream),
						Transformation = new Transformation().AspectRatio("1:1").Crop("fill")
					};

					var uploadResult = await _cloudinary.UploadAsync(uploadParams);
					return uploadResult.SecureUrl.ToString();
				}
			}
			catch(Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
