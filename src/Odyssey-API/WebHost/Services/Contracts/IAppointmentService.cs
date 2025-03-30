using WebHost.Entities;
using WebHost.Models;

namespace WebHost.Services.Contracts;

public interface IAppointmentService
{
    Task<IEnumerable<Appointment>> GetAppointmentsAsync();
    Task<Appointment?> GetAppointmentByIdAsync(string id);
    Task<IEnumerable<Appointment>> GetUserAppointmentsAsync(string userId);
    Task<IEnumerable<Appointment>> GetUserInstructorAppointmentsAsync(string userId, string instructorId);
    Task<Appointment> CreateAppointmentAsync(AppointmentInputModel appointment);
    Task<Appointment> UpdateAppointmentAsync(Appointment appointment);
    Task<bool> DeleteAppointmentAsync(string id);
}