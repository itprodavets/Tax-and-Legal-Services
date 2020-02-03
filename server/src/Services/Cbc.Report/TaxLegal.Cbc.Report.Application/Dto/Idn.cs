using Core.Domain.Enums.Countries;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class Idn : BaseDto
    {
        public Alpha2Code Jurisdiction { get; set; }
        public string In { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}