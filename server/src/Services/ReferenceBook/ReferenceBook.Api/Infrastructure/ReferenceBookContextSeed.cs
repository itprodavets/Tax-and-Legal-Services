using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Collections.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReferenceBook.Domain.AggregatesModel.CountryAggregate;
using ReferenceBook.Domain.AggregatesModel.LanguageAggregate;
using ReferenceBook.Domain.Enums;
using ReferenceBook.Infrastructure;

namespace ReferenceBook.Api.Infrastructure
{
	public class ReferenceBookContextSeed
	{
		public async Task SeedAsync(ReferenceBookContext context, IWebHostEnvironment env, ILogger<ReferenceBookContextSeed> logger)
		{
			await using (context)
			{
				context.Database.Migrate();

				if (!context.Countries.Any()) GetCountriesFromFile(env.ContentRootPath, logger);

				if (!context.Languages.Any()) GetLanguagesFromFile(env.ContentRootPath, logger);

				await context.SaveChangesAsync();
			}
		}

		private static IEnumerable<Country> GetCountriesFromFile(string contentRootPath, ILogger logger)
		{
			var csvFileCountries = Path.Combine(contentRootPath, "Setup", "Countries.csv");
			if (!File.Exists(csvFileCountries)) throw new Exception("File is not exists");

			string[] csvheaders;
			try
			{
				string[] requiredHeaders = {"Name", "Alpha2Code", "Alpha3Code", "NumericCode", "Region", "SubRegion"};
				csvheaders = GetHeaders(requiredHeaders, csvFileCountries);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return File.ReadAllLines(csvFileCountries)
			           .Skip(1)
			           .SelectTry(CreateCountry)
			           .OnCaughtException(ex =>
			            {
				            logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
				            return null;
			            })
			           .Where(x => x != null);
		}

		private static Country CreateCountry(string value)
		{
			if (string.IsNullOrEmpty(value))
				throw new Exception("Country is null or empty");

			return new Country("", Alpha2Code.AD, Alpha3Code.ABW, 0, Region.Africa, SubRegion.Caribbean);
		}

		private static IEnumerable<Language> GetLanguagesFromFile(string contentRootPath, ILogger logger)
		{
			var csvFileLanguages = Path.Combine(contentRootPath, "Setup", "Languages.csv");
			if (!File.Exists(csvFileLanguages)) throw new Exception("File is not exists");

			string[] csvheaders;
			try
			{
				string[] requiredHeaders = {"Name", "NativeName", "Code"};
				csvheaders = GetHeaders(requiredHeaders, csvFileLanguages);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return File.ReadAllLines(csvFileLanguages)
			           .Skip(1)
			           .SelectTry(CreateLanguage)
			           .OnCaughtException(ex =>
			            {
				            logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
				            return null;
			            })
			           .Where(x => x != null);
		}

		private static Language CreateLanguage(string value)
		{
			if (string.IsNullOrEmpty(value))
				throw new Exception("Language is null or empty");

			return new Language("", "", LanguageCode.AA);
		}


		private static string[] GetHeaders(string[] requiredHeaders, string csvFile)
		{
			var csvHeaders = File.ReadLines(csvFile).First().ToLowerInvariant().Split(',');

			if (csvHeaders.Count() != requiredHeaders.Count())
				throw new Exception($"requiredHeader count '{requiredHeaders.Count()}' is different then read header '{csvHeaders.Count()}'");

			foreach (var requiredHeader in requiredHeaders)
				if (!csvHeaders.Contains(requiredHeader))
					throw new Exception($"does not contain required header '{requiredHeader}'");

			return csvHeaders;
		}
	}
}