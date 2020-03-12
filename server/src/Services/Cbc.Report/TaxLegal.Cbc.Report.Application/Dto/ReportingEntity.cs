using System;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class ReportingEntity : BaseDto
    {
        public Doc Doc { get; set; } = new Doc();
        public Organisation Organisation { get; set; } = new Organisation();
        public string NameMNEGroup { get; set; } = string.Empty;
        public ReportingRoleEnum Role { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}