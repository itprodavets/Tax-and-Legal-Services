using TaxLegal.Cbc.Report.Application.Dto;

namespace TaxLegal.Cbc.Report.Application.Xml.Interfaces
{
    public interface IXmlFileBuilder
    {
        IXmlFile Build(SupportedSchema schema, ReportData data);
    }
}
