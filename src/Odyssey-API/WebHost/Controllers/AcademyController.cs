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
// [Authorize(Policy = "UserOnly")]
[Route("[controller]")]
public class AcademiesController : ControllerBase
{
    private readonly IAcademyService _academyService;

    public AcademiesController(IAcademyService academyService, IConfiguration configuration)
    {
        this._academyService = academyService;
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
            IEnumerable<AcademyViewModel> academies = await this._academyService.GetAcademiesAsync();
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
            AcademyViewModel? academy = await this._academyService.GetAcademyAsync(id);
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
    [Authorize(Policy = "InstructorOnly")]
    public async Task<IActionResult> CreateAcademy(AcademyInputModel academy)
    {
        try
        {
            AcademyViewModel newAcademy = await this._academyService.CreateAcademyAsync(academy);
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
    public async Task<IActionResult> UpdateAcademy(Academy academy)
    {
        try
        {
            AcademyViewModel updatedAcademy = await this._academyService.UpdateAcademyAsync(academy);
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
    public async Task<IActionResult> DeleteAcademy(string id)
    {
        try
        {
            bool result = await this._academyService.DeleteAcademyAsync(id);
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
}