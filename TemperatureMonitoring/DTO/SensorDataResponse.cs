namespace TemperatureMonitoring.DTO;
public class SensorDataResponse
{
    [JsonPropertyName("devices")]
    public List<Device> Devices { get; set; } = new List<Device>();
    [JsonPropertyName("success")]
    public bool IsSuccess;
}