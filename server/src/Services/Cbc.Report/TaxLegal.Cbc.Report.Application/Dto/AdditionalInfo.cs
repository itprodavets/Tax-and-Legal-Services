using System;
using Core.Domain.Enums.Countries;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class AdditionalInfo : BaseDto
    {
        public Doc Doc { get; set; } = default!;
        public OtherInfo[] OtherInfo { get; set; } = Array.Empty<OtherInfo>();
        public Alpha2Code[] Jurisdiction { get; set; } = Array.Empty<Alpha2Code>();
        public SummaryTypeEnum[] SummaryTypes { get; set; } = Array.Empty<SummaryTypeEnum>();
    }
}