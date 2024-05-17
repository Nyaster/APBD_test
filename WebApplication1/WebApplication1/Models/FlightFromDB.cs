namespace WebApplication1.Models;

public class FlightFromDB
{
    public int IdFlight { get; set; }
    public DateTime FlightDate { get; set; }
    public string Comments { get; set; }
    public int PlaneId { get; set; }
    public int CityDictId { get; set; }
}