using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NSwag.Annotations;
using TaxLegal.Cbc.Report.Api.Infrastructure.Configurations;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Services.Interfaces;

namespace TaxLegal.Cbc.Report.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IConfiguration _configuration;

        public ReportsController(
            IReportService reportService,
            IConfiguration configuration
        )
        {
            _reportService = reportService;
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ReportData), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ReportData>> Parse([OpenApiFile] IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            await using var stream = System.IO.File.Create(filePath);
            await file.CopyToAsync(stream);
            stream.Position = 0;

            var reportData = _reportService.Parse(stream);
            if (reportData is null)
                return BadRequest(new ValidationMessage[] { new ValidationMessage(ValidationSeverity.Error, "Failed to deserialize file") });

            return Ok(reportData);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ContentResult), (int) HttpStatusCode.OK)]
        public IActionResult Generate([FromQuery] SupportedSchema schema, [FromBody] JsonElement raw)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (schema == 0)
                return BadRequest("Schema missing");

            var json = raw.GetRawText();
            var options = new JsonSerializerOptions
            {
                DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
            };
            var data = JsonSerializer.Deserialize<ReportData>(json, options);
            var xml = _reportService.Generate(data, schema);

            return Content(xml, MediaTypeNames.Application.Xml);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ActionResult<IReadOnlyCollection<ValidationMessage>>), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<ValidationMessage>>> Validate([FromQuery] SupportedSchema schema, [OpenApiFile] IFormFile file)
        {
            await using var stream = System.IO.File.Create(Path.GetTempFileName());
            await file.CopyToAsync(stream);
            stream.Position = 0;

            var result = _reportService.Validate(schema, stream);

            return Ok(result);
        }

        #region private

        private FileStreamResult XmlFileResponse(SupportedSchema schema, object content, string fileName, Encoding encoding)
        {
            var serializer = new XmlSerializer(content.GetType());
            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream, encoding, 1024 * 512, true);
            try
            {
                serializer.Serialize(stream, content, XmlSerializerNamespaces(schema));
                stream.Position = 0;
                return File(stream, "text/file", $"{fileName}.file");
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