namespace ReferenceBook.Application.Dto.Countries
{
    public class CountryDto : BaseDto<short>
    {
        public string Name { get; private set; } = string.Empty;
        public string Alpha2Code { get; private set; } = string.Empty;
        public string Alpha3Code { get; private set; } = string.Empty;
        public short NumericCode { get; private set; } = 0;
        public string Region { get; private set; } = string.Empty;
        public string SubRegion { get; private set; } = string.Empty;
    }
}