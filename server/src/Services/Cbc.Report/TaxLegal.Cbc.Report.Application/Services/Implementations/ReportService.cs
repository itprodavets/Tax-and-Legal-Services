using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public ReportData? Parse(Stream stream)
        {
            var schemas = Enum.GetValues(typeof(SupportedSchema)).OfType<SupportedSchema>().OrderByDescending(x => x).ToArray();
            foreach (var schema in schemas)
            {
                var schemaModel = _schemaModels[schema];
                var schemaService = _schemaServices[schema];

                var serializer = new XmlSerializer(schemaModel);
                try
                {
                    stream.Position = 0;
                    var raw = serializer.Deserialize(stream);

                    return schemaService.Parse(raw);
                }
                catch
                {
                    // ignored
                }
            }

            return null;
        }

        public IReadOnlyCollection<ValidationMessage> Validate(SupportedSchema schema, Stream file)
        {
            var schemaService = _schemaServices[schema];

            return schemaService.Validate(file);
        }
    }
}