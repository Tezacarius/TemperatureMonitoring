namespace TemperatureMonitoring.DTO;
public class Device
{
    [JsonPropertyName("deviceid")]
    public string DeviceId { get; set; } = null!;
    [JsonPropertyName("lastseen")]
    public long LastSeen { get; set; }
    [JsonPropertyName("lowbattery")]
    public bool IsBatteryLow { get; set; }
    [JsonPropertyName("measurement")]

    public Measurement Measurement { get; set; } = null!;
}