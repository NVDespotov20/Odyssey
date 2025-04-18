using Microsoft.EntityFrameworkCore;
using WebHost.Data;
using WebHost.Entities;
using WebHost.Models;
using WebHost.Services.Contracts;

namespace WebHost.Services.Implementations;

public class AppointmentService : IAppointmentService
{
    private readonly OdysseyDbContext context;

    public AppointmentService(OdysseyDbContext context)
    {
        this.context = context;
    }

    public async Task<string> CreateAppointmentAsync(AppointmentInputModel appointment)
    {
        var newAppointment = new Appointment()
        {
            InstructorId = appointment.InstructorId,
            StudentId = appointment.StudentId,
            StartTime = appointment.StartTime,
            EndTime = appointment.EndTime,
            Status = appointment.Status,
        };
        await context.Appointments.AddAsync(newAppointment);
        
        await context.SaveChangesAsync();
        return newAppointment.Id;
    }

    public async Task<Appointment> UpdateAppointmentAsync(Appointment appointment)
    {
        Appointment? existingAppointment = await context.Appointments.FirstOrDefaultAsync(
            a => a.Id == appointment.Id);

        if (existingAppointment == null)
        {
            return new();
        }
        
        existingAppointment.EndTime = appointment.EndTime;
        existingAppointment.StartTime = appointment.StartTime;
        context.Entry(existingAppointment).State = EntityState.Modified;
        await context.SaveChangesAsync();
        
        return existingAppointment;
    }

    public async Task<bool> DeleteAppointmentAsync(string id)
    {
        Appointment? existingAppointment = await context.Appointments.FirstOrDefaultAsync(
            a => a.Id == id);

        if (existingAppointment == null)
        {
            return false;
        }
        
        context.Appointments.Remove(existingAppointment);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
    {
        return await context.Appointments.ToListAsync();
    }

    public async Task<Appointment?> GetAppointmentByIdAsync(string id)
    {
        return await context.Appointments.FirstOrDefaultAsync(
            a => a.Id == id); 
    }
    public async Task<IEnumerable<Appointment>> GetUserAppointmentsAsync(string userId)
    {
        return await context.Appointments.Where(a => a.StudentId == userId).ToListAsync();  
    }
    public async Task<IEnumerable<Appointment>> GetUserAppointmentsForDayAsync(string userId, DateTime date)
    {
        return await context.Appointments.Where(a =>
                a.InstructorId == userId &&
                a.StartTime.DayOfYear == date.DayOfYear &&
                a.EndTime.DayOfYear == date.DayOfYear &&
                a.StartTime.Year == date.Year &&
                a.EndTime.Year == date.Year)
            .ToListAsync();  
    }
    public async Task<IEnumerable<Appointment>> GetUserAppointmentsForMonthAsync(string userId, DateTime date)
    {
        return await context.Appointments.Where(a =>
                a.InstructorId == userId &&
                a.StartTime.Month == date.Month &&
                a.EndTime.Month == date.Month &&
                a.StartTime.Year == date.Year &&
                a.EndTime.Year == date.Year)
            .ToListAsync();  
    }

    public async Task<IEnumerable<Appointment>> GetUserInstructorAppointmentsAsync(string userId, string instructorId)
    {
        return await context.Appointments.Where(a => a.StudentId == userId && a.InstructorId == instructorId).ToListAsync();
    }
}