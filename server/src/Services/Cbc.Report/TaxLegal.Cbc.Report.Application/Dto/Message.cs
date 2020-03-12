using System;
using Core.Domain.Enums.Countries;
using Core.Domain.Enums.Languages;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class Message : BaseDto
    {
        public string SendingEntityIn { get; set; } = string.Empty;
        public Alpha2Code Jurisdiction { get; set; }
        public Alpha2Code[] Jurisdictions { get; set; } = Array.Empty<Alpha2Code>();
        public MessageTypeEnum Type { get; set; }
        public LanguageCode? Language { get; set; }
        public string Warning { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string RefId { get; set; } = string.Empty;
        public MessageTypeIndicEnum? TypeIndic { get; set; }
        public string[] CorrMessageRefId { get; set; } = Array.Empty<string>();
        public DateTime ReportingPeriod { get; set; }
        public DateTime Timestamp { get; set; }
    }
}