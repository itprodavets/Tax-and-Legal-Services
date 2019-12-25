using System.IO;
using System.Threading.Tasks;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Queries.Interfaces;
using TaxLegal.Cbc.Report.Application.Xml;
using TaxLegal.Cbc.Report.Application.Xml.Interfaces;

namespace TaxLegal.Cbc.Report.Application.Queries.Implementations
{
    public class CbCReportingQueries : ICbCReportingQueries
    {
        private readonly IXmlFileFactory _xmlFileFactory;

        public CbCReportingQueries(IXmlFileFactory xmlFileFactory)
        {
            _xmlFileFactory = xmlFileFactory;
        }

        public async Task<ReportData> XmlToModel()
        {
            return await Task.Run(() => new ReportData());
        }

        public async Task<IXmlFile> ModelToXml(SupportedSchema schema, ReportData data)
        {
            return await Task.Run(() => _xmlFileFactory.Create(schema, data));
        }

        public async Task<bool> XmlValidate()
        {
            return await Task.Run(() => true);
        }
    }
}