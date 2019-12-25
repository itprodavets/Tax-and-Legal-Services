using System;
using Autofac.Features.Indexed;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Xml.Interfaces;

namespace TaxLegal.Cbc.Report.Application.Xml.Implementation
{
    public class XmlFileFactory : IXmlFileFactory
    {
        private readonly IIndex<SupportedSchema, IXmlFileBuilder> _builders;

        public XmlFileFactory(IIndex<SupportedSchema, IXmlFileBuilder> builders)
        {
            _builders = builders;
        }

        public IXmlFile Create(SupportedSchema schema, ReportData data)
        {
            if (_builders.TryGetValue(schema, out var builder))
            {
                return builder.Build(schema, data);
            }
            throw new NotSupportedException($"XML document builder for {schema:G} schema not found");
        }
    }
}
