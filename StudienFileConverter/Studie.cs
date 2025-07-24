using System.Globalization;
using CsvHelper.Configuration.Attributes;
using DocumentFormat.OpenXml.Spreadsheet;

namespace StudienFileConverter;

public class Studie
{
    [Name("Nummer/Kennung")] public string Id { get; set; }
    [Name("Studiennummer")] public string StudyId { get; set; }
    [Name("Studientitel")] public string StudyTitle { get; set; }
    [Name("Kurztitel")] public string ShortTitle { get; set; }
    [Name("Hilfetext")] public string HelpText { get; set; }
    [Name("Geplanter Studienbeginn")] public DateOnly? Begin { get; set; }
    [Name("Ende der Studie")] public DateOnly? End { get; set; }
    [Name("Pruefarzt - Name")] public string PruefarztName { get; set; }
    [Name("Pruefarzt - E-Mail")] public string PruefarztEMail { get; set; }
    [Name("Pruefarzt - Telefon")] public string PruefarztTelefon { get; set; }
    [Name("Pruefarzt - Notfall-Nr")] public string PruefarztNotfallNr { get; set; }
    [Name("Stellvertreter - Name")] public string StellvertreterName { get; set; }
    [Name("Stellvertreter - E-Mail")] public string StellvertreterEMail { get; set; }
    [Name("Stellvertreter - Telefon")] public string StellvertreterTelefon { get; set; }
    [Name("Stellvertreter - Notfall-Nr")] public string StellvertreterNotfallNr { get; set; }
    [Name("Studienassistent - Name")] public string StudienassistentName { get; set; }
    [Name("Studienassistent - E-Mail")] public string StudienassistentEMail { get; set; }
    [Name("Studienassistent - Telefon")] public string StudienassistentTelefon { get; set; }

    [Name("Studienassistent - Notfall-Nr")]
    public string StudienassistentNotfallNr { get; set; }

    [Name("Studientyp")] public string Studientyp { get; set; }
    [Name("AMG Phase")] public string AmgPhase { get; set; }
    [Name("Protokoll-Nr")] public string ProtokollNr { get; set; }
    [Name("Studienausrichtung")] public string Studienausrichtung { get; set; }
    [Name("Studienart")] public string Studienart { get; set; }
    [Name("Angaben zum PI")] public string Pi { get; set; }
    [Name("Ethikvotum vorhanden")] public string Ethikvorum { get; set; }
    [Name("Interventionell")] public string Interventionell { get; set; }

    [Name("Akkreditierte Studie der DKG fuer Darmkrebszentren")]
    public string Akkreditiert { get; set; }

    [Name("Therapieoptimierungsstudie")] public string Therapieoptimierung { get; set; }
    [Name("GPOH Register")] public string Gpoh { get; set; }
    [Name("Durchfuehrende Abteilung")] public string Abteilung { get; set; }
    [Name("ICD-10 Eingabe")] public string Icd10 { get; set; }
    [Name("Notiz")] public string Notiz { get; set; }
    [Name("DKH-Typ"), Default(""), Optional] public string DKHTyp { get; set; }
    [Name("Studienbeginn"), Default(""), Optional] public string Studienbeginn { get; set; }
    [Name("Rekrutierungsstart"), Default(""), Optional] public string Rekrutierungsstart { get; set; }
    [Name("Rekrutierungsende"), Default(""), Optional] public string Rekrutierungsende { get; set; }
    [Name("Prospektiv/Retrospektiv"), Default(""), Optional] public string ProspektivRetrospektiv { get; set; }


    public static Row HeaderRow()
    {
        var headers = new List<string>
        {
            "Nummer/Kennung",
            "Studiennummer",
            "Studientitel",
            "Kurztitel",
            "Aktiv",
            "Hilfetext",
            "Geplanter Studienbeginn",
            "Ende der Studie",
            "Pruefarzt - Name",
            "Pruefarzt - E-Mail",
            "Pruefarzt - Telefon",
            "Pruefarzt - Notfall-Nr",
            "Stellvertreter - Name",
            "Stellvertreter - E-Mail",
            "Stellvertreter - Telefon",
            "Stellvertreter - Notfall-Nr",
            "Studienassistent - Name",
            "Studienassistent - E-Mail",
            "Studienassistent - Telefon",
            "Studienassistent - Notfall-Nr",
            "Studientyp",
            "AMG Phase",
            "Protokoll-Nr",
            "Studienausrichtung",
            "Studienart",
            "Angaben zum PI",
            "Ethikvotum vorhanden",
            "Interventionell",
            "Akkreditierte Studie der DKG fuer Darmkrebszentren",
            "Therapieoptimierungsstudie",
            "GPOH Register",
            "Durchfuehrende Abteilung",
            "ICD-10 Eingabe",
            "Notiz",
            "DKH-Typ",
            "Studienbeginn",
            "Rekrutierungsstart",
            "Rekrutierungsende",
            "Prospektiv/Retrospektiv",
        };

        var row = new Row();
        foreach (var header in headers)
        {
            var cell = new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(header)
            };
            row.AppendChild(cell);
        }

        return row;
    }

    public Row ToRow()
    {
        var row = new Row();
        row.AppendChild(StringCell(Id));
        row.AppendChild(StringCell(StudyId));
        row.AppendChild(StringCell(StudyTitle));
        row.AppendChild(StringCell(ShortTitle));
        row.AppendChild(StringCell("1"));
        row.AppendChild(StringCell(HelpText));
        row.AppendChild(DateCell(Begin));
        row.AppendChild(DateCell(End));
        row.AppendChild(StringCell(PruefarztName));
        row.AppendChild(StringCell(PruefarztEMail));
        row.AppendChild(StringCell(PruefarztTelefon));
        row.AppendChild(StringCell(PruefarztNotfallNr));
        row.AppendChild(StringCell(StellvertreterName));
        row.AppendChild(StringCell(StellvertreterEMail));
        row.AppendChild(StringCell(StellvertreterTelefon));
        row.AppendChild(StringCell(StellvertreterNotfallNr));
        row.AppendChild(StringCell(StudienassistentName));
        row.AppendChild(StringCell(StudienassistentEMail));
        row.AppendChild(StringCell(StudienassistentTelefon));
        row.AppendChild(StringCell(StudienassistentNotfallNr));
        row.AppendChild(StringCell(Studientyp));
        row.AppendChild(StringCell(AmgPhase.Equals("0") ? "" : AmgPhase));
        row.AppendChild(StringCell(ProtokollNr));
        row.AppendChild(StringCell(Studienausrichtung));
        row.AppendChild(StringCell(Studienart));
        row.AppendChild(StringCell(Pi));
        row.AppendChild(StringCell(Ethikvorum));
        row.AppendChild(StringCell(Interventionell));
        row.AppendChild(StringCell(Akkreditiert));
        row.AppendChild(StringCell(Therapieoptimierung));
        row.AppendChild(StringCell(Gpoh));
        row.AppendChild(StringCell(Abteilung));
        row.AppendChild(StringCell(Icd10));
        row.AppendChild(StringCell(Notiz));
        row.AppendChild(StringCell(DKHTyp));
        row.AppendChild(StringCell(Studienbeginn));
        row.AppendChild(StringCell(Rekrutierungsstart));
        row.AppendChild(StringCell(Rekrutierungsende));
        row.AppendChild(StringCell(ProspektivRetrospektiv));
        return row;
    }

    private static Cell DateCell(DateOnly? value)
    {
        var cell = new Cell
        {
            DataType = CellValues.String
        };
        if (value.HasValue)
            cell.CellValue = new CellValue(value.Value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo));
        return cell;
    }

    private static Cell StringCell(string value)
    {
        var cell = new Cell
        {
            DataType = CellValues.String,
            CellValue = new CellValue(value)
        };
        return cell;
    }
}