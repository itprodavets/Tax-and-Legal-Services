using System;
using Autofac;
using TaxLegal.Cbc.Report.Application;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Services.Interfaces;

namespace TaxLegal.Cbc.Report.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        public ApplicationModule(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Reference)
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.RegisterInstance(typeof(Application.Schemas.V100.Models.Xml.CBC_OECD))
                .Keyed<Type>(SupportedSchema.OECD_100)
                .SingleInstance();
            builder.RegisterInstance(typeof(Application.Schemas.V101.Models.Xml.CBC_OECD))
                .Keyed<Type>(SupportedSchema.OECD_101)
                .SingleInstance();
            builder.RegisterInstance(typeof(Application.Schemas.V200.Models.Xml.CBC_OECD))
                .Keyed<Type>(SupportedSchema.OECD_200)
                .SingleInstance();

            builder.RegisterType<Application.Schemas.V100.Services.SchemaService>()
                .Keyed<ISchemaService>(SupportedSchema.OECD_100)
                .SingleInstance();
            builder.RegisterType<Application.Schemas.V101.Services.SchemaService>()
                .Keyed<ISchemaService>(SupportedSchema.OECD_101)
                .SingleInstance();
            builder.RegisterType<Application.Schemas.V200.Services.SchemaService>()
                .Keyed<ISchemaService>(SupportedSchema.OECD_200)
                .SingleInstance();
        }
    }
}