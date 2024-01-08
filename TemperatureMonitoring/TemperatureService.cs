namespace TemperatureMonitoring;
public class TemperatureService
{
    private readonly IConfiguration _config;
    private readonly string _sensorIds;
    private string _data = string.Empty;
    private readonly HttpClient _httpClient = new HttpClient();
    public TemperatureService(IConfiguration config)
    {
        _config = config;
        _sensorIds = _config["SENSOR_ID"];

        var clt = new CancellationTokenSource();
        var token = clt.Token;
        _ = Run(UpdateTemperature, TimeSpan.FromMinutes(5), token);
    }
    private async Task Run(Action action, TimeSpan period, CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(period, cancellationToken);

            if (!cancellationToken.IsCancellationRequested)
            {
                action();
            }
        }
    }

    private void UpdateTemperature()
    {
        try
        {
            var content = JsonContent.Create(new SensorDataRequest() { DeviceIds = _sensorIds });
            var response = _httpClient.PostAsync("https://www.data199.com/api/pv1/device/lastmeasurement/", content)
                .Result;
            var sensorData = response.Content.ReadFromJsonAsync<SensorDataResponse>().Result!;

            var time = DateTimeOffset.FromUnixTimeSeconds(sensorData.Devices.First().Measurement.TimeStamp).UtcDateTime
                .AddHours(3);

            _data =
                $"Time: {time.Hour}:{time.Minute}\nTemperature: {sensorData.Devices.First().Measurement.Temperature}C";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public string GetData()
    {
        return _data;
    }
}