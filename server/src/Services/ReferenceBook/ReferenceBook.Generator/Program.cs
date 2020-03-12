using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReferenceBook.Generator
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            GenerateCountries(args[0], args[3]);
            GenerateCurrencies(args[1], args[4]);
            GenerateLanguages(args[2], args[5]);
        }

        private static void GenerateCountries(string tsvPath, string enumPath)
        {
            var dictionary = GenerateDictionary(tsvPath, enumPath, 1);

            WriteSource(tsvPath, dictionary.Values);

            var alpha2Codes = dictionary.ToDictionary(x => x.Key, x => x.Value[0]);
            GenerateCSharpEnum("country", "Alpha2Code", "Core.Domain.Enums.Countries", alpha2Codes, "ISO 3166-1 Alpha2 code");
            GenerateTypeScriptEnum(enumPath, "CountryEnum", alpha2Codes.Keys);

            var alpha3Codes = dictionary.ToDictionary(x => x.Value[2], x => x.Value[0]);
            GenerateCSharpEnum("country", "Alpha3Code", "Core.Domain.Enums.Countries", alpha3Codes, "ISO 3166-1 Alpha3 code");

            var regions = dictionary.Values
                .Select(x => x.Length > 4 ? x[4] : string.Empty)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Prepend("None")
                .Distinct()
                .ToDictionary(x => x, x => x);
            GenerateCSharpEnum("country", "Region", "Core.Domain.Enums.Countries", regions, "Region");
        }

        private static void GenerateCurrencies(string tsvPath, string enumPath)
        {
            var dictionary = GenerateDictionary(tsvPath, enumPath, 0);

            WriteSource(tsvPath, dictionary.Values);

            var currencyCodes = dictionary.ToDictionary(x => x.Key, x => x.Value[1]);
            GenerateCSharpEnum("currency", "CurrencyCode", "Core.Domain.Enums.Currencies", currencyCodes, "ISO 4217 currency code");
            GenerateTypeScriptEnum(enumPath, "CurrencyEnum", currencyCodes.Keys);
        }

        private static void GenerateLanguages(string tsvPath, string enumPath)
        {
            var dictionary = GenerateDictionary(tsvPath, enumPath, 2);

            WriteSource(tsvPath, dictionary.Values);

            var languageCodes = dictionary.ToDictionary(x => x.Key, x => x.Value[0]);
            GenerateCSharpEnum("language", "LanguageCode", "Core.Domain.Enums.Languages", languageCodes, "ISO 639-1 language code");
            GenerateTypeScriptEnum(enumPath, "LanguageEnum", languageCodes.Keys);
        }

        private static void GenerateCSharpEnum(string directory, string name, string ns, IReadOnlyDictionary<string, string> dictionary, string description)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"namespace {ns}");
            sb.AppendLine("{");
            sb.AppendLine("    /// <summary>");
            sb.AppendLine($"    /// {description}");
            sb.AppendLine("    /// </summary>");
            sb.AppendLine($"    public enum {name}");
            sb.AppendLine("    {");

            foreach (var (value, comment) in dictionary)
            {
                sb.AppendLine("        /// <summary>");
                sb.AppendLine($"        /// {comment}");
                sb.AppendLine("        /// </summary>");
                sb.AppendLine($"        {value},");
            }

            sb.AppendLine("    }");
            sb.AppendLine("}");

            Directory.CreateDirectory(directory);
            File.WriteAllText(Path.Combine(directory, $"{name}.cs"), sb.ToString());
        }

        private static void GenerateTypeScriptEnum(string path, string name, IReadOnlyCollection<string> values)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"export enum {name} {{");

            foreach (var value in values)
                sb.AppendLine($"    {value},");

            sb.AppendLine("}");

            File.WriteAllText(path, sb.ToString());
        }

        private static void WriteSource(string tsvPath, IReadOnlyCollection<string[]> data)
        {
            var lines = data.Select(x => string.Join('\t', x)).Prepend(File.ReadAllLines(tsvPath).First()).ToArray();
            File.WriteAllLines(tsvPath, lines);
        }

        private static Dictionary<string, string[]> GenerateDictionary(string tsvPath, string enumPath, int primaryIndex)
        {
            var (sources, enumeration) = ReadSources(tsvPath, enumPath, primaryIndex);
            var fieldsCount = sources.Max(x => x.Value.Length);

            return sources.Keys.Union(enumeration)
                .OrderBy(x => x)
                .ToDictionary(
                    x => x,
                    x => sources.ContainsKey(x) ? sources[x] : Enumerable.Repeat(x, fieldsCount).ToArray()
                );
        }

        private static (Dictionary<string, string[]>, string[]) ReadSources(string tsvPath, string enumPath, int primaryIndex)
        {
            var sources = File.ReadAllLines(tsvPath)
                .Skip(1)
                .Select(x => x.Split('\t'))
                .ToDictionary(x => x[primaryIndex], x => x);
            var enumeration = File.ReadAllLines(enumPath)
                .Skip(1).SkipLast(1)
                .Select(x => x.Trim(' ', '\t', ','))
                .Distinct()
                .ToArray();

            return (sources, enumeration);
        }
    }
}