using System.Net;
using StudienFileConverter;

namespace StudienimportService.Services;

public class StudienUploadService(ILogger logger, Uri uri)
{
    private readonly HttpClient _httpClient = new();

    public async void Upload(List<Studie> studien)
    {
        var studienFileConverter = new StudienFileConverter.StudienFileConverter(logger);
        var stream = studienFileConverter.Convert(studien);

        var byteArrayContent = new ByteArrayContent(stream.ToArray());

        var content = new MultipartFormDataContent();
        content.Add(byteArrayContent, "document", "upload.xlsx");

        var response = await _httpClient.PostAsync(uri, content);

        if (response.StatusCode != HttpStatusCode.OK)
            logger.LogCritical("Cannot update studies in Onkostar: {}", await response.Content.ReadAsStringAsync());
        else
            logger.LogInformation("Studies updated in Onkostar: {}", await response.Content.ReadAsStringAsync());

        stream.Dispose();
    }
}