using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebHost.Data;

namespace WebHost;


public class Program
{
	public static byte[] HexStringToByteArray(string hex)
	{
		int numberChars = hex.Length;
		byte[] bytes = new byte[numberChars / 2];
		for (int i = 0; i < numberChars; i += 2)
		{
			bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
		}
		return bytes;
	}
	
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
			})
			.AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				
				string publicKeyPEM = @"
-----BEGIN RSA PUBLIC KEY-----
MIIBCgKCAQEAxRXlDZXeYcDbIp8SFEiZoMs6kRFAHDKsWRp7vHY8ApoMAFyL3MBQ
PE0vygT4Y7c1BveeQaIs++53/Ehcwp3zinzQrol9ybWuEHdIq2N9TWaoBpOFLmKR
AUubCf/YG0pZ49aFZttudOHCYXKLuOeoBPJB0DVM3jdU1rNr+uF8Ep8r2srhjx4R
19KsqzM3fe4jdnJBGz83kQABCM57OCtwSSTZRJg0/mmLEmxgEIF2j02ME1v1uGV0
HCfIMUTU/LRrxz9ahaiD+EjU2Nt5VJ3zbpWUQ05PMU1ihPrWPUzYrFruzK7vcR8j
0mUyN8tYmuOTI+GFsoYsGxG0IoKGszGtOQIDAQAB
-----END RSA PUBLIC KEY-----
";

				// Create an RSA instance and import the PEM-formatted key.
				using RSA rsa = RSA.Create();
				rsa.ImportFromPem(publicKeyPEM.ToCharArray());

				// Wrap the RSA key in an RsaSecurityKey which is used by the JWT validation.
				var rsaKey = new RsaSecurityKey(rsa);
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ClockSkew = TimeSpan.Zero,
					IssuerSigningKey = rsaKey
				};
			});


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
