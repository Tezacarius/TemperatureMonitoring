namespace TemperatureMonitoring;

public class HttpListenerService : IHostedService
{
    private readonly HttpListener _listener = new();
    private readonly TemperatureService _temperatureService;

    public HttpListenerService(TemperatureService temperatureService)
    {
        _temperatureService = temperatureService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await ListenAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _listener.Close();

        return Task.CompletedTask;
    }

    public async Task ListenAsync()
    {
        _listener.Prefixes.Add("http://+:7860/");
        _listener.Start();
        while (true)
        {
            HttpListenerContext context = await _listener.GetContextAsync();
            var request = context.Request;
            var response = context.Response;
            var inputStream = request.InputStream;
            var encoding = request.ContentEncoding;
            var reader = new StreamReader(inputStream, encoding);
            var requestBody = reader.ReadToEnd();

            Console.WriteLine("{0} request was caught: {1}",
                request.HttpMethod, request.Url);

            var responseString = _temperatureService.GetData();
            var responseBody = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = responseBody.Length;
            response.OutputStream.Write(responseBody, 0, responseBody.Length);
            response.StatusCode = (int)HttpStatusCode.OK;
            response.Close();
        }
    }
}