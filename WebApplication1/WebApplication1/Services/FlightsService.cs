using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public class FlightsService
{
    private FlightDbRepository _flightDbRepository;

    public FlightsService(FlightDbRepository flightDbRepository)
    {
        _flightDbRepository = flightDbRepository;
    }

    public async Task<List<Flight>> GetFlightsForPassanger(int id)
    {
        var flightsForPassanger = await _flightDbRepository.GetFlightsForPassanger(id);
        List<Flight> flights = new List<Flight>();
        if (flightsForPassanger.Count == 0)
        {
            throw new Exception("UserNotFound");
        }

        foreach (var flightFromDb in flightsForPassanger)
        {
            Flight flight = new Flight();
            flight.CityDict = await GetCity(flightFromDb.CityDictId);
            flight.plane = await GetPlane(flightFromDb.PlaneId);
            flight.Comments = flightFromDb.Comments;
            flight.FlightDate = flightFromDb.FlightDate;
            flights.Add(flight);
        }

        {
        }
        return flights;
    }

    public async Task<Plane> GetPlane(int id)
    {
        return await _flightDbRepository.GetPlane(id);
    }

    public async Task<CityDict> GetCity(int id)
    {
        return await _flightDbRepository.GetCity(id);
    }

    public async Task<int> AddUserToFlight(int id, FlightDepartureDTO flightDepartureDto)
    {
        var flightsForPassanger = await GetFlightsForPassanger(id);
        var flighto = await GetFlighto(id);
        var plane = await GetPlane(flighto.PlaneId);
        var usersToFlight = await GetUsersToFlight(id);
        if (usersToFlight >= plane.MaxSeat)
        {
            throw new Exception("Max seats is Allocated");
        }

        if (flighto.FlightDate < DateTime.Now)
        {
            throw new Exception("Flight is arleady flighted");
        }
        foreach (var flight in flightsForPassanger)
        {
            if (flight.IdFlight == id)
            {
                throw new Exception("Already allocated");
            }   
        }

        var addUserToflight = await _flightDbRepository.AddUserToflight(id, flightDepartureDto.PassangerID);
        if (addUserToflight == 0)
        {
            throw new Exception("Error while adding to fliths");
        }

        return addUserToflight;
    }

    public async Task<FlightFromDB> GetFlighto(int id)
    {
        var flight = await _flightDbRepository.GetFlight(id);
        return flight;
    }

    public async Task<int> GetUsersToFlight(int id)
    {
        return await _flightDbRepository.GetUsersToFlight(id);
    }
}