namespace GarageManager.Api.Models;

public class ServiceRecord
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = "";
    public decimal Cost { get; set; }
    public int MileageKm { get; set; }
}