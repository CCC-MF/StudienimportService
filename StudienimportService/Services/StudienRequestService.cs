using System.Net;
using StudienFileConverter;

namespace StudienimportService.Services;

public class StudienRequestService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly Uri _uri;

    public StudienRequestService(ILogger logger, Uri uri)
    {
        _httpClient = new HttpClient();
        _uri = uri;
        _logger = logger;
    }

    public async Task<List<Studie>> RequestStudien()
    {
        var studienFileConverter = new StudienFileConverter.StudienFileConverter(_logger);

        var response = await _httpClient.GetAsync(_uri);

        if (response.StatusCode == HttpStatusCode.OK)
            return studienFileConverter.Convert(response.Content.ReadAsStream());
        _logger.LogError("Error: HTTP-Response {}", response.StatusCode);
        return new List<Studie>();
    }
}