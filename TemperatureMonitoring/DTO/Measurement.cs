namespace TemperatureMonitoring.DTO;
public class Measurement
{
    [JsonPropertyName("idx")]
    public int Id { get; set; }
    [JsonPropertyName("ts")]
    public long TimeStamp { get; set; }
    [JsonPropertyName("c")]
    public long ReceivedByServer { get; set; }
    [JsonPropertyName("lb")]
    public bool IsBatteryLow { get; set; }
    [JsonPropertyName("t1")]
    public float Temperature { get; set; }
}