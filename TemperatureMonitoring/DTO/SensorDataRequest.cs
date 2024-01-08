namespace TemperatureMonitoring.DTO;
public class SensorDataRequest
{
    [JsonPropertyName("deviceids")] 
    public string DeviceIds { get; set; } = null!;
}
