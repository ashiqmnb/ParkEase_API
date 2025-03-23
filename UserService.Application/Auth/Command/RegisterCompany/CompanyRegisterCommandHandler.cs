using MediatR;
using UserService.Application.Common.Interfaces;
using UserService.Domain.Entity;
using UserService.Domain.Repository;

namespace UserService.Application.Auth.Command.RegisterCompany
{
	public class CompanyRegisterCommandHandler : IRequestHandler<CompanyRegisterCommand, bool>
	{
		private readonly IAuthRepo _authRepo;
		private readonly IEmailService _emailService;

		public CompanyRegisterCommandHandler(IAuthRepo authRepo, IEmailService emailService)
		{
			_authRepo = authRepo;
			_emailService = emailService;
		}

		public async Task<bool> Handle(CompanyRegisterCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var existingComapny = await _authRepo.GetCompanyByEmail(request.companyRegDto.Email);

				if (existingComapny != null) throw new Exception("Comapny with this email id is already exists");

				string salt = BCrypt.Net.BCrypt.GenerateSalt();
				string hashPassword = BCrypt.Net.BCrypt.HashPassword(request.companyRegDto.Password, salt);
				
				if(request.companyRegDto.Type.ToLower() != "customer" && request.companyRegDto.Type.ToLower() != "public")
				{
					throw new Exception("Invalid company type");
				}

				var companyType = UserService.Domain.Entity.Type.Public;

				if (request.companyRegDto.Type == "Customer")
				{
					companyType = UserService.Domain.Entity.Type.Customer;
				}

				var company = new Company
				{
					Name = request.companyRegDto.Name,
					Email = request.companyRegDto.Email,
					Password = hashPassword,
					Type = companyType,
					CreatedBy = "admin",
					CreatedOn = DateTime.UtcNow
				};

				await _authRepo.AddCompany(company);

				string subject = $"Welcome to ParkEase - Your Company Account Details";

				string body = $@"
					<!DOCTYPE html>
					<html>
					<head>
						<style>
							body {{ font-family: Arial, sans-serif; text-align: center; background-color: #f4f4f4; padding: 20px; }}
							.container {{ max-width: 500px; background: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); margin: auto; }}
							.login-info {{ font-weight: bold; color: #333; padding: 10px; background: #f4f4f4; display: inline-block; border-radius: 5px; }}
							.footer {{ margin-top: 20px; font-size: 12px; color: #666; }}
						</style>
					</head>
					<body>
						<div class='container'>
							<h2>Welcome to ParkEase!</h2>
							<p>Your company has been successfully registered on ParkEase.</p>

							<p>Your login credentials:</p>
							<p class='login-info'>Username: {request.companyRegDto.Email}</p>
							<p class='login-info'>Temporary Password: {request.companyRegDto.Password}</p>


							<p class='footer'>For security reasons, please change your password after logging in.</p>
							<p class='footer'>If you have any questions, contact our support team.</p>
						</div>
					</body>
					</html>";

				await _emailService.SendEmail(request.companyRegDto.Email, subject, body);

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.InnerException?.Message ?? ex.Message);
			}
		}
	}
}
