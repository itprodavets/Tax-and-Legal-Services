using Core.Domain.Enums.Languages;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class OtherInfo : BaseDto
    {
        public LanguageCode? Language { get; set; }
        public string Info { get; set; } = string.Empty;
    }
}