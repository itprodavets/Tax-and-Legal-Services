using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ReferenceBook.Api.Infrastructure;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace ReferenceBook.Api
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(new CompactJsonFormatter())
                .CreateLogger();

            try
            {
                var host = CreateHostBuilder(args).Build();

                Log.Information("Starting migrate database");
                await host.MigrateDatabase();

                Log.Information("Starting web host");
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseKestrel(server =>
                        {
                            server.AddServerHeader = false;
                            server.ListenAnyIP(5000, listen =>
                            {
                                var certFile = Path.GetFullPath(Path.Combine("certs", "cert.pfx"));
                                if (File.Exists(certFile))
                                    listen.UseHttps(certFile);
                            });
                        })
                        .UseStartup<Startup>();
                })
                .UseSerilog();
    }
}