
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaymentService.Api.Middlewares;
using PaymentService.Application;
using PaymentService.Application.Common.AppSettings;
using PaymentService.Infrastructure;
using System.Text;

namespace PaymentService.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			Env.Load();

			var appSettings = new AppSettings
			{
				DbConnectionString = Environment.GetEnvironmentVariable("DEFAULT_CONNECTION"),
				JwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY"),
				
				RazorPayKeyId = Environment.GetEnvironmentVariable("RAZORPAY_KEYID"),
				RazorPayKeySecret = Environment.GetEnvironmentVariable("RAZORPAY_KEYSECRET"),

				AdminId = Environment.GetEnvironmentVariable("ADMIN_ID"),
			};

			builder.Services.AddSingleton(appSettings);


			// add layers dependency
			builder.Services.AddApplicationService();
			builder.Services.AddInfrastructureServices(builder.Configuration,appSettings);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();

			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "ParkEase Payment Service API", Version = "v1" });

				// Add JWT Authentication in Swagger
				options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter 'Bearer' [space] and then your token"
				});
				options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
			});


			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(o =>
			{
				var jwtKey = appSettings.JwtSecretKey;

				if (string.IsNullOrEmpty(jwtKey))
				{
					throw new Exception("JWT key is missing from environment variables.");
				}

				o.TokenValidationParameters = new TokenValidationParameters
				{
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true
				};
			});


			// Cors Added
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("ReactPolicy", builder =>
				{
					builder.AllowAnyOrigin()
						   .AllowAnyMethod()
						   .AllowAnyHeader();
				});
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseCors("ReactPolicy");

			app.UseHttpsRedirection();

			app.UseAuthorization();
			app.UseAuthentication();

			app.UseMiddleware<UserIdMiddlware>();


			app.MapControllers();

			app.Run();
		}
	}
}
