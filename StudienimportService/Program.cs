using CliWrap;
using StudienimportService;

const string serviceName = "Studienimport Service";

if (args is { Length: 1 })
{
    string executablePath =
        Path.Combine(AppContext.BaseDirectory, "StudienimportService.exe");

    if (args[0] is "/Install")
    {
        await Cli.Wrap("sc")
            .WithArguments(new[] { "create", serviceName, $"binPath={executablePath}", "start=delayed-auto" })
            .ExecuteAsync();
    }
    else if (args[0] is "/Uninstall")
    {
        await Cli.Wrap("sc")
            .WithArguments(new[] { "stop", serviceName })
            .ExecuteAsync();

        await Cli.Wrap("sc")
            .WithArguments(new[] { "delete", serviceName })
            .ExecuteAsync();
    }

    return;
}

var host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options => { options.ServiceName = serviceName; })
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();

        services.AddLogging(builder =>
        {
            builder.AddConfiguration(
                context.Configuration.GetSection("Logging"));
        });
    })
    .Build();

await host.RunAsync();