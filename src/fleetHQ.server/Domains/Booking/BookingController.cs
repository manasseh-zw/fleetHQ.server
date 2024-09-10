using System.Security.Claims;

using FleetHQ.Server.Shared;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetHQ.Server.Domains.Booking;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings()
    {
        var companyId = User.FindFirst(Constants.CompanyId)?.Value;
        if (companyId == null) return Unauthorized();

        var result = await _bookingService.GetBookings(Guid.Parse(companyId));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddBooking([FromBody] AddBookingDto dto)
    {
        var companyId = User.FindFirst(Constants.CompanyId)?.Value;
        if (companyId == null) return Unauthorized();

        var result = await _bookingService.AddBooking(Guid.Parse(companyId), dto);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPut("{bookingId}")]
    public async Task<IActionResult> UpdateBooking(Guid bookingId, [FromBody] UpdateBookingDto dto)
    {
        var result = await _bookingService.UpdateBooking(bookingId, dto);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("{bookingId}")]
    public async Task<IActionResult> DeleteBooking(Guid bookingId)
    {
        var result = await _bookingService.DeleteBooking(bookingId);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    [HttpPost("{bookingId}/assign")]
    public async Task<IActionResult> AssignDriverAndVehicle(Guid bookingId, [FromBody] AssignDriverAndVehicleDto dto)
    {
        var result = await _bookingService.AssignDriverAndVehicle(bookingId, dto.DriverId, dto.VehicleId);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}

