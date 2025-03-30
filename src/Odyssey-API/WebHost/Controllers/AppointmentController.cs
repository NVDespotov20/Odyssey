using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebHost.Entities;
using WebHost.Models;
using WebHost.Services.Implementations;

namespace WebHost.Controllers;

[ApiController]
[Route("/api/appointments")]
public class AppointmentController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly AppointmentService _appointmentService;

    public AppointmentController(UserManager<User> userManager, AppointmentService appointmentService)
    {
        _userManager = userManager;
        _appointmentService = appointmentService;
    }

    [HttpGet("User")]
    [Authorize]
    public async Task<IActionResult> GetUserAppointmentsAsync(string userId)
    {
        var appointments = await _appointmentService.GetUserAppointmentsAsync(userId);
        return Ok(appointments);
    }

    [HttpGet("getById")]
    [Authorize]
    public async Task<IActionResult> GetAppointmentByIdAsync(string appointmentId)
    {
        var appointment = await _appointmentService.GetAppointmentByIdAsync(appointmentId);
        if (appointment == null)
            return BadRequest();
        
        return Ok(appointment);
    }

    [HttpGet]
    [Authorize]
    public async Task<IEnumerable<Appointment>> GetUserInstructorAppointmentsAsync(string userId, string instructorId)
    {
        return await _appointmentService.GetUserInstructorAppointmentsAsync(userId, instructorId);
    }

    [HttpPost]
    [Authorize]
    public async Task<Appointment> CreateAppointment(AppointmentInputModel model)
    {
        return await _appointmentService.CreateAppointmentAsync(model);
    }

    [HttpPut("Update")]
    [Authorize]
    public async Task<Appointment> UpdateAppointment(Appointment model)
    {
        return await _appointmentService.UpdateAppointmentAsync(model);
    }
}