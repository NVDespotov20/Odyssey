using Microsoft.EntityFrameworkCore;
using WebHost.Data;
using WebHost.Entities;
using WebHost.Models;
using WebHost.Services.Contracts;

namespace WebHost.Services.Implementations;

public class AcademyService : IAcademyService
    {
        private readonly ApplicationDbContext _context; // Assuming you have a DbContext called ApplicationDbContext

        public AcademyService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch an academy by its ID
        public async Task<AcademyViewModel?> GetAcademy(string id)
        {
            var academy = await _context.Academies
                .FirstOrDefaultAsync(a => a.Id == id);

            if (academy == null)
            {
                return null;
            }

            return new AcademyViewModel
            {
                Id = academy.Id,
                Name = academy.Name,
                Price = academy.Price,
                Location = academy.Location
            };
        }

        // Create a new academy
        public async Task<AcademyViewModel> CreateAcademy(AcademyInputModel academy)
        {
            Academy? existingAcademy = await _context.Academies
                .FirstOrDefaultAsync(a => a.Id == academy.Id);

            if (existingAcademy != null)
            {
                return new();
            }

            var newAcademy = new Academy
            {
                Id = Guid.NewGuid().ToString(), // Use GUID or other strategy for unique IDs
                Name = academy.Name,
                Price = academy.Price,
                Location = academy.Location
            };

            _context.Academies.Add(newAcademy);
            await _context.SaveChangesAsync();

            return new()
            {
                Id = newAcademy.Id,
                Name = newAcademy.Name,
                Price = newAcademy.Price,
                Location = newAcademy.Location
            };
        }

        // Update an existing academy
        public async Task<AcademyViewModel> UpdateAcademy(AcademyInputModel academy)
        {
            Academy? existingAcademy = await _context.Academies
                .FirstOrDefaultAsync(a => a.Id == academy.Id);

            if (existingAcademy == null)
            {
                return new(); // Academy not found
            }

            existingAcademy.Name = academy.Name;
            existingAcademy.Price = academy.Price;
            existingAcademy.Location = academy.Location;

            _context.Academies.Update(existingAcademy);
            await _context.SaveChangesAsync();

            return new AcademyViewModel
            {
                Name = existingAcademy.Name,
                Price = existingAcademy.Price,
                Location = existingAcademy.Location
                
            };
        }

        // Delete an academy by its ID
        public async Task<bool> DeleteAcademy(string id)
        {
            var academy = await _context.Academies
                .FirstOrDefaultAsync(a => a.Id == id);

            if (academy == null)
            {
                return false; // Academy not found
            }

            _context.Academies.Remove(academy);
            await _context.SaveChangesAsync();

            return true; // Successful deletion
        }
    }