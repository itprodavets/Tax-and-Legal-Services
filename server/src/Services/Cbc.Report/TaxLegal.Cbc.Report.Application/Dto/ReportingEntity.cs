using System;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class ReportingEntity : BaseDto
    {
        public Doc Doc { get; set; } = default!;
        public Organisation Organisation { get; set; } = default!;
        public string NameMNEGroup { get; set; } = default!;
        public ReportingRoleEnum Role { get; set; }
        public DateTime StartDate { get; set; } = default!;
        public DateTime EndDate { get; set; } = default!;
    }
}