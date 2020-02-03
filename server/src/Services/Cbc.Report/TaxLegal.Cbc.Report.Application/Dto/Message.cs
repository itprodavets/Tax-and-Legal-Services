using System;
using Core.Domain.Enums.Countries;
using Core.Domain.Enums.Languages;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class Message
    {
        public string SendingEntityIn { get; set; } = default!;
        public Alpha2Code Jurisdiction { get; set; }
        public Alpha2Code[] Jurisdictions { get; set; } = Array.Empty<Alpha2Code>();
        public MessageTypeEnum Type { get; set; }
        public LanguageCode Language { get; set; }
        public string Warning { get; set; } = default!;
        public string Contact { get; set; } = default!;
        public string RefId { get; set; } = default!;
        public MessageTypeIndicEnum TypeIndic { get; set; }
        public string[] CorrMessageRefId { get; set; } = default!;
        public DateTime ReportingPeriod { get; set; } = default!;
        public DateTime Timestamp { get; set; } = default!;
    }
}