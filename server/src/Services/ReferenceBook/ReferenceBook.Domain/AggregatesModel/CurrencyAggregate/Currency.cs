using System.Diagnostics.CodeAnalysis;
using Common.Domain;
using Core.Domain.Enums.Currencies;

namespace ReferenceBook.Domain.AggregatesModel.CurrencyAggregate
{
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    public class Currency : Entity<short>
    {
        private Currency()
        {
        }

        public Currency(CurrencyCode code, string name, string symbol)
        {
            Code = code;
            Name = name;
            Symbol = symbol;
        }

        public CurrencyCode Code { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Symbol { get; private set; } = string.Empty;
    }
}