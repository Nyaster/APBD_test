using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("/api/flights")]
[ApiController]
public class FlightsController : ControllerBase
{
    private FlightsService _flights;

    public FlightsController(FlightsService flights)
    {
        _flights = flights;
    }

    [HttpGet("/users/{id:int}")]
    public async Task<IActionResult> GetFlightsForPerson(int id)
    {
        try
        {
            var flightsForPassanger = await _flights.GetFlightsForPassanger(id);
            return Ok(flightsForPassanger);
        }
        catch (Exception e)
        {
            return NotFound();
        }
       
    }
    [HttpPost("{id:int}")]
    public async Task<IActionResult> AddPersonToFlight(int id,FlightDepartureDTO flightDepartureDto)
    {
        try
        {
            var addUserToFlight = await _flights.AddUserToFlight(id, flightDepartureDto);
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}