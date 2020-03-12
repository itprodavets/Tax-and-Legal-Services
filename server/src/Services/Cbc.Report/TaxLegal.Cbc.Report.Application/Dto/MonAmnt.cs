using Core.Domain.Enums.Currencies;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class MonAmnt : BaseDto
    {
        public CurrencyCode Currency { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}