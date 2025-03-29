using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebHost.Entities;
using WebHost.Models;
using WebHost.Services.Contracts;

namespace WebHost.Controllers;

/// <summary>
/// Controller for managing academies
/// </summary>
[ApiController]
[Route("[controller]")]
public class AcademiesController : ControllerBase
{
    private readonly IAcademyService academyService;
    private readonly IConfiguration configuration;
    private readonly string AESKey;
    
    public AcademiesController(IAcademyService academyService, IConfiguration configuration)
    {
        this.academyService = academyService;
        this.configuration = configuration;
        this.AESKey = this.configuration["Encryption:AESKey"]!;
    }
    
    /// <summary>
    /// Get all academies
    /// </summary>
    /// <returns>All academies</returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAcademies()
    {
        try
        {
            IEnumerable<AcademyViewModel> academies = await this.academyService.GetAcademiesAsync();
            return Ok(academies);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /// <summary>
    /// Get academy by id
    /// </summary>
    /// <param name="id">Academy id</param>
    /// <returns>Academy</returns>
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetAcademy(string id)
    {
        try
        {
            AcademyViewModel? academy = await this.academyService.GetAcademyAsync(id);
            if (academy == null)
            {
                return NotFound();
            }
            return Ok(academy);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /// <summary>
    /// Create academy
    /// </summary>
    /// <param name="academy">Academy data</param>
    /// <returns>Created academy</returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAcademy(AcademyInputModel academy)
    {
        try
        {
            AcademyViewModel newAcademy = await this.academyService.CreateAcademyAsync(academy);
            return Ok(newAcademy);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /// <summary>
    /// Update academy
    /// </summary>
    /// <param name="academy">Academy data</param>
    /// <returns>Updated academy</returns>
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateAcademy(AcademyInputModel academy)
    {
        try
        {
            AcademyViewModel updatedAcademy = await this.academyService.UpdateAcademyAsync(academy);
            return Ok(updatedAcademy);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    /// <summary>
    /// Delete academy
    /// </summary>
    /// <param name="id">Academy id</param>
    /// <returns>Success</returns>
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteAcademy(string id)
    {
        try
        {
            bool result = await this.academyService.DeleteAcademyAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    // TODO: Implement enroll
}