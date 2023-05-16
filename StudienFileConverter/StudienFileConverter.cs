using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Extensions.Logging;

namespace StudienFileConverter;

public class StudienFileConverter
{
    private readonly ILogger _logger;

    public StudienFileConverter(ILogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Converts input stream of CSV file to list of studies
    /// </summary>
    /// <param name="inputStream">Input stream of CSV file</param>
    /// <returns>List of studies</returns>
    public List<Studie> Convert(Stream inputStream)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            Quote = '"',
            MissingFieldFound = null
        };

        try
        {
            var streamReader = new StreamReader(inputStream);
            var csvReader = new CsvReader(streamReader, config);

            return csvReader.GetRecords<Studie>().ToList();
        }
        catch (Exception e)
        {
            _logger.LogCritical("Error: {}", e);
        }

        return new List<Studie>();
    }

    /// <summary>
    ///     Converts a list of studies to a stream containing Excel file
    /// </summary>
    /// <param name="studien">The list of studies</param>
    /// <returns>The data stream containing an excel file</returns>
    public MemoryStream Convert(List<Studie> studien)
    {
        var outStream = new MemoryStream();

        var spreadsheetDocument = SpreadsheetDocument.Create(outStream, SpreadsheetDocumentType.Workbook);

        var workbookPart = spreadsheetDocument.AddWorkbookPart();
        workbookPart.Workbook = new Workbook();

        var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        var sheetData = new SheetData();
        worksheetPart.Worksheet = new Worksheet(sheetData);

        var sheets = workbookPart.Workbook.AppendChild(new Sheets());
        var sheet = new Sheet
        {
            Id = workbookPart.GetIdOfPart(worksheetPart),
            SheetId = 1,
            Name = "Sheet0"
        };

        sheets.Append(sheet);

        sheetData.AppendChild(Studie.HeaderRow());

        foreach (var studie in studien) sheetData.AppendChild(studie.ToRow());

        spreadsheetDocument.Dispose();
        return outStream;
    }
}