using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaxLegal.Cbc.Report.Api.Infrastructure.Configurations;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Queries.Interfaces;
using TaxLegal.Cbc.Report.Application.Xml;

namespace TaxLegal.Cbc.Report.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CbCReportingController : ControllerBase
    {
        private readonly ICbCReportingQueries _cbCReporting;
        private readonly IConfiguration _configuration;

        public CbCReportingController(ICbCReportingQueries cbCReporting, IConfiguration configuration)
        {
            _cbCReporting = cbCReporting ?? throw new ArgumentNullException(nameof(cbCReporting));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet("XmlToModel")]
        [ProducesResponseType(typeof(ReportData), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ReportData>> XmlToModel()
        {
            var reportData = await _cbCReporting.XmlToModel();
            return Ok(reportData);
        }

        [HttpGet("ModelToXml")]
        [ProducesResponseType(typeof(FileResult), (int)HttpStatusCode.OK)]
        public async Task<FileResult> ModelToXml([FromQuery] SupportedSchema schema, [FromQuery] ReportData data)
        {
            var file = await _cbCReporting.ModelToXml(schema, data);
            return XmlFileResponse(schema, file.Content, file.FileName, file.Encoding);
        }

        [HttpGet("XmlValidate")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Stream>> XmlValidate()
        {
            var result = await _cbCReporting.XmlValidate();
            return Ok(result);
        }

        #region private
        private FileStreamResult XmlFileResponse(SupportedSchema schema, object content, string fileName, Encoding encoding)
        {
            var xmlSerializer = new XmlSerializer(content.GetType());
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream, encoding, 1024 * 512, true);
            try
            {
                xmlSerializer.Serialize(stream, content, XmlSerializerNamespaces(schema));
                stream.Position = 0;
                return File(stream, "text/xml", $"{fileName}.xml");
            }
            finally
            {
                streamWriter.Dispose();
            }
        }

        private XmlSerializerNamespaces XmlSerializerNamespaces(SupportedSchema schema)
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            var cfg = _configuration.Get<Configuration>();
            var appXmlFile = cfg.App.XmlFile.SingleOrDefault(x => x.SupportedSchema == schema);

            if (appXmlFile?.Namespaces == null) return ns;

            foreach (var (key, value) in appXmlFile.Namespaces)
                ns.Add(key, value);

            return ns;
        }
        #endregion
    }
}