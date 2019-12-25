namespace ReferenceBook.Application.Dto.Countries
{
	public class CountryDto
	{
		public string Name { get; } = string.Empty;
		public string Alpha2Code { get; } = string.Empty;
		public string Alpha3Code { get; } = string.Empty;
		public short NumericCode { get; } = 0;
		public string Region { get; } = string.Empty;
		public string SubRegion { get; } = string.Empty;
	}
}