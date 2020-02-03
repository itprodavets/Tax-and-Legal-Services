// using System;
// using System.IO;
// using System.Threading.Tasks;
// using System.Xml.Serialization;
// using TaxLegal.Cbc.Report.Application.Dto;
// using TaxLegal.Cbc.Report.Application.Queries.Interfaces;
// using TaxLegal.Cbc.Report.Application.Schemas;
// using TaxLegal.Cbc.Report.Application.Schemas.V100.Models.Xml;
// using TaxLegal.Cbc.Report.Application.Xml;
// using TaxLegal.Cbc.Report.Application.Xml.Interfaces;
// using TaxLegal.Cbc.Report.Application.Xml.Schemas;
//
// namespace TaxLegal.Cbc.Report.Application.Queries.Implementations
// {
//     public class CbCReportingQueries : ICbCReportingQueries
//     {
//         private readonly IXmlFileFactory _xmlFileFactory;
//
//         public CbCReportingQueries(IXmlFileFactory xmlFileFactory)
//         {
//             _xmlFileFactory = xmlFileFactory;
//         }
//
//         public async Task<ReportData> XmlToModel(SupportedSchema schema, FileStream stream)
//         {
//             var type = schema switch
//             {
//                 SupportedSchema.OECD_100 => typeof(CBC_OECD),
//                 SupportedSchema.OECD_101 => typeof(Xml.Schemas.V101.CBC_OECD),
//                 SupportedSchema.OECD_200 => typeof(Xml.Schemas.V200.CBC_OECD),
//                 _ => throw new ArgumentOutOfRangeException(nameof(schema), schema, null)
//             };
//             var serializer = new XmlSerializer(type);
//             var cbc = (ICBC_OECD) serializer.Deserialize(stream);
//
//             if (cbc == null) throw new ArgumentNullException(nameof(cbc));
//             return await Task.Run(() => cbc.Parse());
//         }
//
//         public async Task<IXmlFile> ModelToXml(SupportedSchema schema, ReportData data)
//         {
//             return await Task.Run(() => _xmlFileFactory.Create(schema, data));
//         }
//     }
// }