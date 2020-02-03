using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Schemas;

namespace TaxLegal.Cbc.Report.Application.Xml.Interfaces
{
    public interface IXmlFileFactory
    {
        IXmlFile Create(SupportedSchema schema, ReportData data);
    }
}
