
namespace Domain.DTO
{
    public record DeviceType
    {
        string Burner { get; set; }
        string Boiler { get; set; }
        string Valve { get; set; }
        string Pump { get; set; }
        string Alarm { get; set; }
        string WaterDevice { get; set; }
    }
}
