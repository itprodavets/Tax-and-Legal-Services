using System.Collections.Generic;
using TaxLegal.Cbc.Report.Application.Xml;

namespace TaxLegal.Cbc.Report.Api.Infrastructure.Configurations
{
    public class XmlFileConfiguration
    {
        public SupportedSchema SupportedSchema { get; set; } = default!;
        public Dictionary<string, string> Namespaces { get; set; } = default!;
    }
}