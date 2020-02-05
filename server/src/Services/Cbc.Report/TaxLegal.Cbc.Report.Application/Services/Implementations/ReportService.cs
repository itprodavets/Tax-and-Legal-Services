using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Autofac.Features.Indexed;
using Microsoft.Extensions.Logging;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Services.Interfaces;

namespace TaxLegal.Cbc.Report.Application.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IIndex<SupportedSchema, ISchemaService> _schemaServices;
        private readonly IIndex<SupportedSchema, Type> _schemaModels;
        private readonly ILogger<ReportService> _logger;

        public ReportService(
            IIndex<SupportedSchema, ISchemaService> schemaServices,
            IIndex<SupportedSchema, Type> schemaModels,
            ILogger<ReportService> logger
        )
        {
            _schemaServices = schemaServices;
            _schemaModels = schemaModels;
            _logger = logger;
        }

        public ReportData? Parse(Stream stream)
        {
            var schemas = Enum.GetValues(typeof(SupportedSchema)).OfType<SupportedSchema>().OrderByDescending(x => x).ToArray();
            foreach (var schema in schemas)
            {
                var schemaModel = _schemaModels[schema];
                var schemaService = _schemaServices[schema];

                var serializer = new XmlSerializer(schemaModel);

                object raw;
                try
                {
                    stream.Position = 0;
                    raw = serializer.Deserialize(stream);
                }
                catch
                {
                    continue;
                }

                try
                {
                    return schemaService.Parse(raw);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Failed to parse document for {schema} schema");
                }
            }

            return null;
        }

        public string Generate(ReportData data, SupportedSchema schema)
        {
            var schemaModel = _schemaModels[schema];
            var schemaService = _schemaServices[schema];

            var sb = new StringBuilder();
            var serializer = new XmlSerializer(schemaModel);

            using var writer = new StringWriter(sb);
            var raw = schemaService.Generate(data);
            serializer.Serialize(writer, raw);

            return sb.ToString();
        }

        public IReadOnlyCollection<ValidationMessage> Validate(SupportedSchema schema, Stream file)
        {
            var schemaService = _schemaServices[schema];

            return schemaService.Validate(file);
        }
    }
}