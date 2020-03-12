using Core.Domain.Enums.Countries;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class ReportBody : BaseDto
    {
        public Doc Doc { get; set; } = new Doc();
        public Alpha2Code Jurisdiction { get; set; }
        public Summary Summary { get; set; } = new Summary();
    }
}