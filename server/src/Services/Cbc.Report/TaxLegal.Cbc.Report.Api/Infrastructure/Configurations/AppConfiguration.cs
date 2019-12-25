using System.Collections.Generic;

namespace TaxLegal.Cbc.Report.Api.Infrastructure.Configurations
{
    public class AppConfiguration
    {
        public List<XmlFileConfiguration> XmlFile { get; set; } = default!;
    }
}