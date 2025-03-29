using WebHost.Models;

namespace WebHost.Services.Contracts;

public interface IAcademyService
{
    Task<AcademyViewModel?> GetAcademyAsync(string id);
    Task<AcademyViewModel> CreateAcademyAsync(AcademyInputModel academy);
    Task<AcademyViewModel> UpdateAcademyAsync(AcademyInputModel academy);
    Task<bool> DeleteAcademyAsync(string id);
    
    Task<IEnumerable<AcademyViewModel>> GetAcademiesAsync();
}