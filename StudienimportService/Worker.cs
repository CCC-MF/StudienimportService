using StudienimportService.Services;

namespace StudienimportService;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly IConfiguration _configuration;
    
    public Worker(ILogger<Worker> logger, IHostApplicationLifetime lifetime, IConfiguration configuration)
    {
        _logger = logger;
        _lifetime = lifetime;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var requestUrl = _configuration.GetSection("App")["RequestUrl"];
            var postUrl = _configuration.GetSection("App")["PostUrl"];
            if (null == requestUrl || null == postUrl)
            {
                _logger.LogCritical("Required URLs not set. Exit.");
                _lifetime.StopApplication();
                break;
            }

            var taskDelay = _configuration.GetSection("App")["TaskDelay"];
            int delay;
            if (null == taskDelay || ! Int32.TryParse(taskDelay, out delay))
            {
                _logger.LogWarning("No TaskDelay given, using 7 days as default value.");
                delay = 7;
            }
            
            var studien = await new StudienRequestService(_logger, new Uri(requestUrl)).RequestStudien();
            new StudienUploadService(_logger, new Uri(postUrl)).Upload(studien);
            
            await Task.Delay(TimeSpan.FromDays(delay), stoppingToken);
        }
    }
}