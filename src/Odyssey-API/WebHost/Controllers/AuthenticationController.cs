using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebHost.Entities;
using WebHost.Models;

namespace WebHost.Controllers;

[Route("/api/auth")]
public class AuthenticationController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticationController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterInputModel model)
    {
        var user = new User
        {
            UserName = model.Username,
            Email = model.Email,
            AccessFailedCount = 0,
            FirstName = model.FirstName,
            LastName = model.LastName,
            AboutMe = "",
            Experience = "",
            AcademyId = null,
            Academy = null,

        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        if (model.Role == "null" || model.Role == "")
        {
            await _userManager.AddToRoleAsync(user, "user");
        }
        else if (!(await _roleManager.RoleExistsAsync(model.Role)))
        {
            return BadRequest("Invalid role");
        }
        else
        {
            await _userManager.AddToRoleAsync(user, model.Role);
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginInputModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null) return Unauthorized();

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if (!result.Succeeded) return Unauthorized();

        var token = GenerateJwtToken(user);
        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(User user)
    {
        var rsa = RSA.Create();
        rsa.ImportFromPem(_configuration["JWT:Private"]);
        var rsaKey = new RsaSecurityKey(rsa);

        // Retrieve the user's roles
        var roles = _userManager.GetRolesAsync(user).Result;

        // Create claims, including role claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

        // Add role claims
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var credentials = new SigningCredentials(rsaKey, SecurityAlgorithms.RsaSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddYears(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
