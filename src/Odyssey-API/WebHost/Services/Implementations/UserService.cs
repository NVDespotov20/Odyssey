using Microsoft.EntityFrameworkCore;
using WebHost.Data;
using WebHost.Entities;
using WebHost.Models;
using WebHost.Services.Contracts;

namespace WebHost.Services.Implementations;

public class UserService : IUserService
    {
        private readonly OdysseyDbContext _context;

        public UserService(OdysseyDbContext context)
        {
            _context = context;
        }

        public async Task<UserViewModel?> GetUser(string userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userId);

            if (user == null)
            {
                return null;
            }

            return new UserViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                AboutMe = user.AboutMe
            };
        }
       
        // Update user data
        public async Task<bool> UpdateUser(UserInputModel user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == user.Username);

            if (existingUser == null)
            {
                return false; // User not found
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            existingUser.AboutMe = user.AboutMe;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();

            return true; // Successful update
        }

        // Delete a user by their ID
        public async Task<bool> DeleteUser(string userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return false; 
            }

            user.Deleted = true;
            await _context.SaveChangesAsync();

            return true;
        }
    }