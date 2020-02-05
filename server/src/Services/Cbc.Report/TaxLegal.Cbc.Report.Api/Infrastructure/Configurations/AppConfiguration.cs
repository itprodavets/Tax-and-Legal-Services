using System.Collections.Generic;
using TaxLegal.Cbc.Report.Application.Configurations;

namespace TaxLegal.Cbc.Report.Api.Infrastructure.Configurations
{
    public class AppConfiguration
    {
        public List<XmlFileConfiguration> XmlFile { get; set; } = default!;
    }
}