using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebHost.Entities;
using WebHost.Models;
using WebHost.Services.Contracts;
using WebHost.Services.Implementations;

namespace WebHost.Controllers;

[ApiController]
[Route("/api/appointments")]
public class AppointmentController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly ICurrentUser _currentUser;
    private readonly AppointmentService _appointmentService;

    public AppointmentController(UserManager<User> userManager, AppointmentService appointmentService, ICurrentUser currentUser)
    {
        _userManager = userManager;
        _appointmentService = appointmentService;
        _currentUser = currentUser;
    }

    [HttpGet("User")]
    [Authorize]
    public async Task<IActionResult> GetUserAppointmentsAsync(string userId)
    {
        var appointments = await _appointmentService.GetUserAppointmentsAsync(userId);
        return Ok(appointments);
    }

    [HttpGet("getDay")]
    [Authorize]
    public async Task<IActionResult> GetAppointmentsForInstructorForDay(string userId, DateTime date)
    {
        var appointments = await _appointmentService.GetUserAppointmentsForDayAsync(userId, date);
        return Ok(appointments.Select(a => new { a.StartTime, a.EndTime, a.Status }).ToList());
    }
    [HttpGet("getMonth")]
    [Authorize]
    public async Task<IActionResult> GetAppointmentsForInstructorForMonth(string userId, DateTime date)
    {
        var appointments = await _appointmentService.GetUserAppointmentsForMonthAsync(userId, date);
        return Ok(appointments.Select(a => new { a.StartTime, a.EndTime, a.Status }).ToList());
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
    public async Task<IActionResult> CreateAppointment(AppointmentInputModel model)
    {
        model.StudentId = _currentUser.UserId;
        model.Status = "Pending";
        if(ModelState.IsValid)
            return Ok(await _appointmentService.CreateAppointmentAsync(model));
        return BadRequest();
    }

    [HttpPut("Update")]
    [Authorize]
    public async Task<Appointment> UpdateAppointment(Appointment model)
    {
        return await _appointmentService.UpdateAppointmentAsync(model);
    }
}