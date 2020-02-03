using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Autofac.Features.Indexed;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Services.Interfaces;

namespace TaxLegal.Cbc.Report.Application.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IIndex<SupportedSchema, ISchemaService> _schemaServices;
        private readonly IIndex<SupportedSchema, Type> _schemaModels;

        public ReportService(
            IIndex<SupportedSchema, ISchemaService> schemaServices,
            IIndex<SupportedSchema, Type> schemaModels
        )
        {
            _schemaServices = schemaServices;
            _schemaModels = schemaModels;
        }

        public ReportData Parse(SupportedSchema schema, Stream file)
        {
            var schemaModel = _schemaModels[schema];

            var serializer = new XmlSerializer(schemaModel);
            var raw = serializer.Deserialize(file);
            if (raw is null)
                throw new ArgumentException($"Failed to deserialize XML into object of {schema} version");

            var schemaService = _schemaServices[schema];

            return schemaService.Parse(raw);
        }

        public IReadOnlyCollection<ValidationMessage> Validate(SupportedSchema schema, Stream file)
        {
            var schemaService = _schemaServices[schema];

            return schemaService.Validate(file);
        }
    }
}