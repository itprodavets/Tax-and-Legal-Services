using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Collections.Extensions;
using Common.Extensions;
using Core.Domain.Enums.Countries;
using Core.Domain.Enums.Currencies;
using Core.Domain.Enums.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReferenceBook.Domain.AggregatesModel.CountryAggregate;
using ReferenceBook.Domain.AggregatesModel.CurrencyAggregate;
using ReferenceBook.Domain.AggregatesModel.LanguageAggregate;
using ReferenceBook.Infrastructure;
using Serilog;

namespace ReferenceBook.Api.Infrastructure
{
    public static class MigrationManager
    {
        public static async Task MigrateDatabase(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<ReferenceBookContext>();
            var env = scope.ServiceProvider.GetService<IHostEnvironment>();

            context.Database.Migrate();

            if (!context.Countries.Any())
                context.Countries.AddRange(GetCountriesFromFile(env.ContentRootPath));

            if (!context.Currencies.Any())
                context.Currencies.AddRange(GetCurrenciesFromFile(env.ContentRootPath));

            if (!context.Languages.Any())
                context.Languages.AddRange(GetLanguagesFromFile(env.ContentRootPath));

            await context.SaveChangesAsync();
        }

        private static IEnumerable<Country> GetCountriesFromFile(string contentRootPath)
        {
            var file = Path.Combine(contentRootPath, "Setup", "Countries.tsv");
            if (!File.Exists(file))
                throw new Exception("File does not exist");

            string[] headers;
            try
            {
                string[] requiredHeaders = { "Name", "Alpha2Code", "Alpha3Code", "NumericCode", "Region", "SubRegion" };
                headers = GetHeaders(requiredHeaders, file);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return File.ReadAllLines(file)
                .Skip(1)
                .SelectTry(CreateCountry)
                .OnCaughtException(ex =>
                {
                    Log.Fatal(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                    return null!;
                })
                .OfType<Country>()
                .ToArray();
        }

        private static Country CreateCountry(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Country is null or empty");

            var list = value.Split('\t').ToArray();
            return new Country(list[0].ToString(),
                list[1].ToEnum<Alpha2Code>(),
                list[2].ToEnum<Alpha3Code>(),
                Convert.ToInt16(list[3]),
                list.Length > 4 ? list[4].ToEnum<Region>() : Region.None,
                list.Length > 5 ? list[5] : string.Empty);
        }

        private static IEnumerable<Currency> GetCurrenciesFromFile(string contentRootPath)
        {
            var file = Path.Combine(contentRootPath, "Setup", "Currencies.tsv");
            if (!File.Exists(file))
                throw new Exception("File does not exist");

            string[] headers;
            try
            {
                string[] requiredHeaders = { "Code", "Name", "Symbol" };
                headers = GetHeaders(requiredHeaders, file);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return File.ReadAllLines(file)
                .Skip(1)
                .SelectTry(CreateCurrency)
                .OnCaughtException(ex =>
                {
                    Log.Fatal(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                    return null!;
                })
                .OfType<Currency>()
                .ToArray();
        }

        private static Currency CreateCurrency(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Currency is null or empty");

            var list = value.Split('\t').ToArray();

            return new Currency(list[0].ToEnum<CurrencyCode>(), list[1], list[2]);
        }

        private static IEnumerable<Language> GetLanguagesFromFile(string contentRootPath)
        {
            var file = Path.Combine(contentRootPath, "Setup", "Languages.tsv");
            if (!File.Exists(file))
                throw new Exception("File does not exist");

            string[] headers;
            try
            {
                string[] requiredHeaders = { "Name", "NativeName", "Code" };
                headers = GetHeaders(requiredHeaders, file);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return File.ReadAllLines(file)
                .Skip(1)
                .SelectTry(CreateLanguage)
                .OnCaughtException(ex =>
                {
                    Log.Fatal(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                    return null!;
                })
                .OfType<Language>()
                .ToArray();
        }

        private static Language CreateLanguage(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Language is null or empty");

            var list = value.Split('\t').ToArray();
            return new Language(list[0].ToString(), list[1].ToString(), list[2].ToEnum<LanguageCode>());
        }

        private static string[] GetHeaders(string[] requiredHeaders, string file)
        {
            var headers = File.ReadLines(file).First().Split('\t');

            if (headers.Count() != requiredHeaders.Count())
                throw new Exception($"requiredHeader count '{requiredHeaders.Length}' is different then read header '{headers.Count()}'");

            foreach (var requiredHeader in requiredHeaders)
                if (!headers.Contains(requiredHeader))
                    throw new Exception($"does not contain required header '{requiredHeader}'");

            return headers;
        }
    }
}