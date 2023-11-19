using CliWrap;
using StudienimportService;

const string serviceName = "Studienimport Service";

if (args is { Length: 1 })
{
    var executablePath =
        Path.Combine(AppContext.BaseDirectory, "StudienimportService.exe");

    switch (args[0])
    {
        case "/Install":
            await Cli.Wrap("sc")
                .WithArguments(new[] { "create", serviceName, $"binPath={executablePath}", "start=delayed-auto" })
                .ExecuteAsync();
            break;
        case "/Uninstall":
            await Cli.Wrap("sc")
                .WithArguments(new[] { "stop", serviceName })
                .ExecuteAsync();

            await Cli.Wrap("sc")
                .WithArguments(new[] { "delete", serviceName })
                .ExecuteAsync();
            break;
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