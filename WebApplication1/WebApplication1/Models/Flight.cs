namespace WebApplication1.Models;

public class Flight
{
    public int IdFlight { get; set; }
    public DateTime FlightDate { get; set; }
    public string Comments { get; set; }
    public Plane plane { get; set; }
    public CityDict CityDict { get; set; }
}