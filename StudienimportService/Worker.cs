using StudienimportService.Services;

namespace StudienimportService;

public class Worker : BackgroundService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var requestUrl = _configuration.GetSection("App")["RequestUrl"];
            var postUrl = _configuration.GetSection("App")["PostUrl"];

            if (null != requestUrl && null != postUrl)
            {
                var studien = await new StudienRequestService(_logger, new Uri(requestUrl)).RequestStudien();
                new StudienUploadService(_logger, new Uri(postUrl)).upload(studien);
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}