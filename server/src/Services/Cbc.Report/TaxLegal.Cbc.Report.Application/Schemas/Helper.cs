using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace TaxLegal.Cbc.Report.Application.Schemas
{
    public static class Helper
    {
        public const string NoTin = "NOTIN";

        public static T Parse<T>(Enum value)
            where T : struct, Enum
        {
            return Enum.Parse<T>(value.ToString("G"));
        }

        public static TResult ParseOrDefault<TSource, TResult>(Nullable<TSource> value)
            where TSource : struct, Enum
            where TResult : struct, Enum
        {
            return value.HasValue ? Parse<TResult>(value.Value) : default;
        }

        public static string GetDescription<T>(this T value)
            where T : struct, Enum
        {
            return typeof(T)
                .GetMember(value.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description ?? value.ToString();
        }

        public static IEnumerable<TResult> SelectManyOrEmpty<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TResult>> selector
        )
        {
            if (source is null)
                return Array.Empty<TResult>();

            return source.SelectMany(x => selector(x) ?? Array.Empty<TResult>()).OfType<TResult>();
        }

        public static IEnumerable<TResult> SelectOrEmpty<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector
        )
        {
            if (source is null)
                return Array.Empty<TResult>();

            return source.Select(selector).OfType<TResult>();
        }

        public static IEnumerable<TResult> OfTypeOrEmpty<TResult>(
            this IEnumerable source
        )
        {
            if (source is null)
                return Array.Empty<TResult>();

            return source.OfType<TResult>();
        }

        public static string? StringOrNull(this string? source) => string.IsNullOrWhiteSpace(source) ? null : source;
    }
}