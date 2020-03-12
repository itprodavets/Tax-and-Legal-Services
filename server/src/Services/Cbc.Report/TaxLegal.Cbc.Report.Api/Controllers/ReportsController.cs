using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TaxLegal.Cbc.Report.Api.Infrastructure;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Services.Interfaces;

namespace TaxLegal.Cbc.Report.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(
            IReportService reportService
        )
        {
            _reportService = reportService;
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
                throw new ValidationException("Failed to deserialize file");

            return Ok(reportData);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ContentResult), (int) HttpStatusCode.OK)]
        public IActionResult Generate([FromBody] JsonElement raw)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = DeserializeReportData(raw);
            var xml = _reportService.Generate(data);

            Response.Headers.Add("Content-Disposition", $"attachment; filename={data.Message?.RefId}.xml");
            Response.Headers.Add("X-Content-Type-Options", "nosniff");

            return Content(xml, MediaTypeNames.Application.Xml);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ContentResult), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ReportData>> XmlModelXml([OpenApiFile] IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            await using var stream = System.IO.File.Create(filePath);
            await file.CopyToAsync(stream);
            stream.Position = 0;

            var reportData = _reportService.Parse(stream);
            if (reportData is null)
                return BadRequest(new[] { new ValidationMessage(ValidationSeverity.Error, "Failed to deserialize file") });

            var xml = _reportService.Generate(reportData);

            return Content(xml, MediaTypeNames.Application.Xml);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ReportData), (int) HttpStatusCode.OK)]
        public IActionResult ModelXmlModel([FromBody] JsonElement raw)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var data = DeserializeReportData(raw);

            var xml = _reportService.Generate(data);

            var ms = new MemoryStream();
            using var w = new StreamWriter(ms, Encoding.UTF8);
            w.Write(xml);
            w.Flush();
            ms.Position = 0;

            var reportData = _reportService.Parse(ms);
            if (reportData is null)
                return BadRequest(new[] { new ValidationMessage(ValidationSeverity.Error, "Failed to deserialize file") });

            return Ok(reportData);
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

        private ReportData DeserializeReportData(JsonElement raw)
        {
            try
            {
                var json = raw.GetRawText();
                var options = new JsonSerializerOptions
                {
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true,
                };
                return JsonSerializer.Deserialize<ReportData>(json, options);
            }
            catch (Exception exception)
            {
                throw new ValidationException(exception.Message);
            }
        }
    }
}