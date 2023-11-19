using StudienimportService.Services;

namespace StudienimportService;

public class Worker(ILogger<Worker> logger, IHostApplicationLifetime lifetime, IConfiguration configuration)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var requestUrl = configuration.GetSection("App")["RequestUrl"];
            var postUrl = configuration.GetSection("App")["PostUrl"];
            if (null == requestUrl || null == postUrl)
            {
                logger.LogCritical("Required URLs not set. Exit.");
                lifetime.StopApplication();
                break;
            }

            var taskDelay = configuration.GetSection("App")["TaskDelay"];
            if (null == taskDelay || ! Int32.TryParse(taskDelay, out var delay))
            {
                logger.LogWarning("No TaskDelay given, using 7 days as default value.");
                delay = 7;
            }
            
            var studien = await new StudienRequestService(logger, new Uri(requestUrl)).RequestStudien();
            new StudienUploadService(logger, new Uri(postUrl)).Upload(studien);
            
            await Task.Delay(TimeSpan.FromDays(delay), stoppingToken);
        }
    }
}