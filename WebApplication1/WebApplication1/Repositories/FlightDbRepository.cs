using System.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Repositories;

public class FlightDbRepository
{
    private string _connectionString =
        "Server=db-mssql;Database=Server=db-mssql ;Database=2019SBD;Trusted_Connection=True;Trusted_Connection=True;";

    public async Task<List<FlightFromDB>> GetFlightsForPassanger(int id)
    {
        List<FlightFromDB> flights = new List<FlightFromDB>();

        using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
        {
            SqlCommand sqlCommand = new SqlCommand();
            await sqlConnection.OpenAsync();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText =
                "select Flight.IdFlight,FlightDate,Comments,IdPlane,IdCityDict,FirstName,LastName from Flight,Flight_Passenger,Passenger where Flight.IdFlight = Flight_Passenger.IdFlight and Flight_Passenger.IdPassenger = Passenger.IdPassenger and Passenger.IdPassenger = @IdPassanger";
            sqlCommand.Parameters.AddWithValue("@IdPassanger", id);
            using (var sqlDataReader = await sqlCommand.ExecuteReaderAsync())
            {
                while (await sqlDataReader.ReadAsync())
                {
                    FlightFromDB flight = new FlightFromDB();
                    flight.IdFlight = (int)sqlDataReader["IdFlight"];
                    flight.FlightDate = (DateTime)sqlDataReader["FlightDate"];
                    flight.Comments = sqlDataReader["Comments"].ToString();
                    flight.PlaneId = (int)sqlDataReader["IdPlane"];
                    flight.CityDictId = (int)sqlDataReader["IdCityDict"];
                    flights.Add(flight);
                }
            }
        }

        return flights;
    }

    public async Task<Plane> GetPlane(int id)
    {
        Plane plane = new Plane();
        using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
        {
            SqlCommand sqlCommand = new SqlCommand();
            await sqlConnection.OpenAsync();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText =
                "select IdPlane,Name,MaxSeats from Plane where IdPlane = @IdPlane";
            sqlCommand.Parameters.AddWithValue("@IdPlane", id);
            using (var sqlDataReader = await sqlCommand.ExecuteReaderAsync())
            {
                while (await sqlDataReader.ReadAsync())
                {
                    plane.IdPlane = (int)sqlDataReader["Idplane"];
                    plane.Name = sqlDataReader["Name"].ToString();
                    plane.MaxSeat = (int)sqlDataReader["MaxSeats"];
                }
            }
        }

        return plane;
    }

    public async Task<CityDict> GetCity(int id)
    {
        CityDict cityDict = new CityDict();
        using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
        {
            SqlCommand sqlCommand = new SqlCommand();
            await sqlConnection.OpenAsync();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText =
                "select IdCityDict, City from CityDict where IdCityDict = @IdCityDict;";
            sqlCommand.Parameters.AddWithValue("@IdCityDict", id);
            using (var sqlDataReader = await sqlCommand.ExecuteReaderAsync())
            {
                while (await sqlDataReader.ReadAsync())
                {
                    cityDict.IdCityDi = (int)sqlDataReader["IdCityDict"];
                    cityDict.City = sqlDataReader["City"].ToString();
                }
            }
        }

        return cityDict;
    }

    public async Task<FlightFromDB> GetFlight(int id)
    {
        FlightFromDB flight = new FlightFromDB();
        using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
        {
            SqlCommand sqlCommand = new SqlCommand();
            await sqlConnection.OpenAsync();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText =
                "select IdFlight,FlightDate,Comments,IdPlane,IdCityDict  from Flight where IdFlight = @IdFlight;";
            sqlCommand.Parameters.AddWithValue("@IdFlight", id);
            using (var sqlDataReader = await sqlCommand.ExecuteReaderAsync())
            {
                while (await sqlDataReader.ReadAsync())
                {
                    flight.IdFlight = (int)sqlDataReader["IdFlight"];
                    flight.FlightDate = (DateTime)sqlDataReader["FlightDate"];
                    flight.Comments = sqlDataReader["Comments"].ToString();
                    flight.PlaneId = (int)sqlDataReader["IdPlane"];
                    flight.CityDictId = (int)sqlDataReader["IdCityDict"];
                }
            }
        }

        return flight;
    }

    public async Task<int> GetUsersToFlight(int id)
    {
        int passangers = 0;
        using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
        {
            SqlCommand sqlCommand = new SqlCommand();
            await sqlConnection.OpenAsync();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText =
                "select count(*) as Passangers from Flight_Passenger where IdFlight = @flightId;";
            sqlCommand.Parameters.AddWithValue("@flightId", id);
            using (var sqlDataReader = await sqlCommand.ExecuteReaderAsync())
            {
                while (await sqlDataReader.ReadAsync())
                {
                    passangers = (int)sqlDataReader["Passangers"];
                }
            }
        }

        return passangers;
    }

    public async Task<int> AddUserToflight(int flightId, int userId)
    {
        int returnval = 0;
        using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
        {
            SqlCommand sqlCommand = new SqlCommand();
            await sqlConnection.OpenAsync();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.CommandText =
                "INSERT INTO Flight_Passenger VALUES ("+flightId+","+userId+");";
            returnval = sqlCommand.ExecuteNonQuery();
        }

        return returnval;
    }
}