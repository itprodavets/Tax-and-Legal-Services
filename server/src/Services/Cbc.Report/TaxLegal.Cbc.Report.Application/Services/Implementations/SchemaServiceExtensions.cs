using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Services.Interfaces;

namespace TaxLegal.Cbc.Report.Application.Services.Implementations
{
    public static class SchemaServiceExtensions
    {
        public static IReadOnlyCollection<ValidationMessage> Validate(
            this ISchemaService service,
            Stream file,
            string schemaRoot
        )
        {
            var settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

            foreach (var path in Directory.GetFiles(schemaRoot, "*.xsd"))
            {
                using var stream = new StreamReader(path);
                settings.Schemas.Add(XmlSchema.Read(stream, null));
            }

            var errors = new List<ValidationMessage>();
            settings.ValidationEventHandler += (_, e) =>
            {
                if (e.Message != null)
                    errors.Add(new ValidationMessage((ValidationSeverity) e.Severity, e.Message));
            };

            using var reader = XmlReader.Create(file, settings);

            while (reader.Read())
            {
            }

            return errors;
        }
    }
}