using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebHost.Data;
using WebHost.Entities;
using WebHost.Services.Contracts;
using WebHost.Services.Implementations;

namespace WebHost;


public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddDbContext<OdysseyDbContext>(options =>
		{
			options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
		});
		
		builder.Services.AddIdentity<User, IdentityRole>()
			.AddEntityFrameworkStores<OdysseyDbContext>()
			.AddDefaultTokenProviders();

		
		
		builder.Services.AddScoped<IAcademyService, AcademyService>();
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<IFileService, FileService>();
		builder.Services.AddScoped<IImageService, ImageService>();
		builder.Services.AddScoped<IEncryptionService, EncryptionService>();
		builder.Services.AddScoped<IAppointmentService, AppointmentService>();
		
		var rsa = RSA.Create();
		rsa.ImportFromPem(builder.Configuration["JWT:Public"]);
		var rsaKey = new RsaSecurityKey(rsa);
		
		builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = rsaKey,
				};
			});
		builder.Services.AddAuthorization(options =>
		{
			options.AddPolicy("InstructorOnly", policy => policy.RequireRole("Instructor"));
			options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
		});

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(options =>
		{
			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			{
				Name = "Authorization",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http,
				Scheme = "Bearer"
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
					Array.Empty<string>()
				}
			});
		});

		var app = builder.Build();
		
		using (var scope = app.Services.CreateScope())
		{
			var services = scope.ServiceProvider;
			var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = services.GetRequiredService<UserManager<User>>();
			var context = services.GetRequiredService<OdysseyDbContext>();
			
			await context.Database.EnsureCreatedAsync();
			await context.Database.MigrateAsync();
			await SeedRoles(roleManager, userManager);
		}
		
		app.UseHttpsRedirection();
		app.UseSwagger();
		app.UseSwaggerUI();

		app.UseAuthentication();
		app.UseAuthorization();
		app.MapControllers();

		app.Run();
	}

	static async Task SeedRoles(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
	{
		if (!await roleManager.RoleExistsAsync("User"))
		{
			await roleManager.CreateAsync(new IdentityRole("User"));
		}

		if (!await roleManager.RoleExistsAsync("Instructor"))
		{
			await roleManager.CreateAsync(new IdentityRole("Instructor"));
		}
	}
}
