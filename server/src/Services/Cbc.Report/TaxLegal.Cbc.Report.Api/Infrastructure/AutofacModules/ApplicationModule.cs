using Autofac;
using TaxLegal.Cbc.Report.Application.Queries.Implementations;
using TaxLegal.Cbc.Report.Application.Queries.Interfaces;
using TaxLegal.Cbc.Report.Application.Xml;
using TaxLegal.Cbc.Report.Application.Xml.Implementation;
using TaxLegal.Cbc.Report.Application.Xml.Implementation.Builders;
using TaxLegal.Cbc.Report.Application.Xml.Interfaces;

namespace TaxLegal.Cbc.Report.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {
        public ApplicationModule(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CbCReportingQueries>()
                   .As<ICbCReportingQueries>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<XmlFileFactory>()
                   .As<IXmlFileFactory>()
                   .SingleInstance();

            builder.RegisterType<CbcOecdBuilder>()
                   .Keyed<IXmlFileBuilder>(SupportedSchema.CBC_OECD_100)
                   .SingleInstance();

            builder.RegisterType<CbcOecdBuilder>()
                   .Keyed<IXmlFileBuilder>(SupportedSchema.CBC_OECD_101)
                   .SingleInstance();

            builder.RegisterType<CbcOecdBuilder>()
                   .Keyed<IXmlFileBuilder>(SupportedSchema.CBC_OECD_200)
                   .SingleInstance();
        }
    }
}