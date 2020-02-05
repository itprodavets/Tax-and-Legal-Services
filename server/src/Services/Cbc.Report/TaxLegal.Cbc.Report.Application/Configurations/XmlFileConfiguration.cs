using System.Collections.Generic;
using TaxLegal.Cbc.Report.Application.Dto;

namespace TaxLegal.Cbc.Report.Application.Configurations
{
    public class XmlFileConfiguration
    {
        public SupportedSchema SupportedSchema { get; set; } = default!;
        public Dictionary<string, string> Namespaces { get; set; } = default!;
    }
}