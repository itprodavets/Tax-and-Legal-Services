using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Schemas;

namespace TaxLegal.Cbc.Report.Application.Xml.Interfaces
{
    public interface IXmlFileBuilder
    {
        IXmlFile Build(SupportedSchema schema, ReportData data);
    }
}
