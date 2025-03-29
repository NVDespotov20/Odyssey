using WebHost.Models;

namespace WebHost.Services.Contracts;

public interface IAcademyService
{
    Task<AcademyViewModel?> GetAcademy(string id);
    Task<AcademyViewModel> CreateAcademy(AcademyInputModel academy);
    Task<AcademyViewModel> UpdateAcademy(AcademyInputModel academy);
    Task<bool> DeleteAcademy(string id);
}