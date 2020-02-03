using Core.Domain.Enums.Currencies;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class MonAmnt
    {
        public CurrencyCode Currency { get; set; }
        public string Value { get; set; } = default!;
    }
}