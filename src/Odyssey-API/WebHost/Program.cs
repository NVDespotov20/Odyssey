using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebHost.Data;

namespace WebHost;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
		});

		// Add services to the container.

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services
			.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			});/*
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				var ed25519PublicKeyBytes = Convert.FromHexString("94439ed2475a9741eae7be4a4bb95d73c57eb10b7deec8d9ac4f57f4fc278dc3");

				var ed25519PublicKeyParameters = new Ed25519PublicKeyParameters(ed25519PublicKeyBytes, 0);
				var jsonWebKey = new JsonWebKey
				{
					Kty = "OKP",
					Crv = "Ed25519",
					X = Base64UrlEncoder.Encode(ed25519PublicKeyBytes)
				};

				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ClockSkew = TimeSpan.Zero,
					IssuerSigningKey = jsonWebKey
				};
			});*/


		var app = builder.Build();
		
		app.UseHttpsRedirection();
		app.UseSwagger();
		app.UseSwaggerUI();

		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();

		app.Run();
	}
}
