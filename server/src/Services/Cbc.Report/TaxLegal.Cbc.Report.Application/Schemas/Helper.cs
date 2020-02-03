using System;

namespace TaxLegal.Cbc.Report.Application.Schemas
{
    public static class Helper
    {
        public static T Parse<T>(Enum value)
            where T : struct, Enum
        {
            return Enum.Parse<T>(value.ToString("G"));
        }
    }
}