using System.ComponentModel;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public enum SupportedSchema
    {
        [Description("1.0.0")]
        OECD_100 = 1,
        [Description("1.0.1")]
        OECD_101 = 2,
        [Description("2.0.0")]
        OECD_200 = 3,
    }
}