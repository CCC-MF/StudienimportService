using System.Net;
using StudienFileConverter;

namespace StudienimportService.Services;

public class StudienRequestService(ILogger logger, Uri uri)
{
    private readonly HttpClient _httpClient = new();

    public async Task<List<Studie>> RequestStudien()
    {
        var studienFileConverter = new StudienFileConverter.StudienFileConverter(logger);

        var response = await _httpClient.GetAsync(uri);

        if (response.StatusCode == HttpStatusCode.OK)
            return studienFileConverter.Convert(await response.Content.ReadAsStreamAsync());
        logger.LogCritical("Error: HTTP-Response {}", response.StatusCode);
        return new List<Studie>();
    }
}