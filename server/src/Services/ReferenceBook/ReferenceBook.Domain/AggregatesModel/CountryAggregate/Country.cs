using System;
using System.Diagnostics.CodeAnalysis;
using Common.Domain;

namespace ReferenceBook.Domain.AggregatesModel.CountryAggregate
{
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Local")]
    public class Country : Entity<short>
    {
        private Country()
        {
        }
        public Country(string name, Alpha2Code alpha2Code, Alpha3Code alpha3Code, short numericCode, Region region, string subRegion)
        {
            Name = name;
            Alpha2Code = alpha2Code;
            Alpha3Code = alpha3Code;
            NumericCode = numericCode;
            Region = region;
            SubRegion = subRegion;
        }

        public string Name { get; private set; } = string.Empty;
        public Alpha2Code Alpha2Code { get; private set; }
        public Alpha3Code Alpha3Code { get; private set; }
        public short NumericCode { get; private set; }
        public Region Region { get; private set; }
        public string SubRegion { get; private set; }
    }
}