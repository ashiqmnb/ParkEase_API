using MediatR;
using Microsoft.AspNetCore.Http;

namespace UserService.Application.Companies.Command.AddImages
{
	public class AddImagesCommand : IRequest<bool>
	{
		public string CompanyId { get; set; }
		public List<IFormFile> Images { get; set; }
	}
}
