using Core.Domain.Enums.Countries;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class Tidn : BaseDto
    {
        public Alpha2Code Jurisdiction { get; set; }
        public string Tin { get; set; } = string.Empty;
    }
}