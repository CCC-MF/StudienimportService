using System.Net;
using StudienFileConverter;

namespace StudienimportService.Services;

public class StudienUploadService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger _logger;
    private readonly Uri _uri;

    public StudienUploadService(ILogger logger, Uri uri)
    {
        _httpClient = new HttpClient();
        _uri = uri;
        _logger = logger;
    }

    public async void upload(List<Studie> studien)
    {
        var studienFileConverter = new StudienFileConverter.StudienFileConverter(_logger);
        var stream = studienFileConverter.Convert(studien);

        var byteArrayContent = new ByteArrayContent(stream.ToArray());

        var content = new MultipartFormDataContent();
        content.Add(byteArrayContent, "document", "upload.xlsx");

        var response = await _httpClient.PostAsync(_uri, content);

        if (response.StatusCode != HttpStatusCode.OK)
            _logger.LogCritical("Cannot update studies in Onkostar: {}", await response.Content.ReadAsStringAsync());
        else
            _logger.LogInformation("Studies updated in Onkostar: {}", await response.Content.ReadAsStringAsync());

        stream.Dispose();
    }
}