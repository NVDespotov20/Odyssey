using WebHost.Models;

namespace WebHost.Services.Contracts;

public interface IUserService
{
    Task<UserViewModel?> GetUser(string userId);
    Task<bool> UpdateUser(UserInputModel user);
    Task<bool> DeleteUser(string userId);
    
    Task<IEnumerable<UserViewModel>> GetUsers();
}