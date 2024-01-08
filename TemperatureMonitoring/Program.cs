using TemperatureMonitoring;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddEnvironmentVariables();

builder.Services
    .AddSingleton<TemperatureService>()
    .AddHostedService<HttpListenerService>();

using var host = builder.Build();

await host.RunAsync();
