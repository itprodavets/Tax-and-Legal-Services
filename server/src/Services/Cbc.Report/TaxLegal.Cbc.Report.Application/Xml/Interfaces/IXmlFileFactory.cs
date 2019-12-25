using TaxLegal.Cbc.Report.Application.Dto;

namespace TaxLegal.Cbc.Report.Application.Xml.Interfaces
{
    public interface IXmlFileFactory
    {
        IXmlFile Create(SupportedSchema schema, ReportData data);
    }
}
