using System.Collections.Generic;
using System.IO;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Schemas.V200.Models.Xml;
using TaxLegal.Cbc.Report.Application.Services.Implementations;
using TaxLegal.Cbc.Report.Application.Services.Interfaces;

namespace TaxLegal.Cbc.Report.Application.Schemas.V200.Services
{
    public class SchemaService : ISchemaService
    {
        public ReportData Parse(object raw)
        {
            return XmlToModel.Convert((CBC_OECD) raw);
        }

        public IReadOnlyCollection<ValidationMessage> Validate(Stream file)
        {
            return this.Validate(file, Path.Combine("Xsd", "2.0"));
        }
    }
}