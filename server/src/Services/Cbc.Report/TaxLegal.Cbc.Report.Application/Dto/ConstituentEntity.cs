using System;
using Core.Domain.Enums.Countries;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class ConstituentEntity : BaseDto
    {
        public Organisation Organisation { get; set; } = new Organisation();
        public UltimateParrentEntityRoleEnum? Role { get; set; }
        public Alpha2Code? Jurisdiction { get; set; }
        public BizActivityTypeEnum[] BizActivities { get; set; } = Array.Empty<BizActivityTypeEnum>();
        public string OtherInfo { get; set; } = string.Empty;
    }
}