using System;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class MessageSpec
    {
        public string SendingEntityIn { get; set; } = default!;
        public string TransmittingCountry { get; set; } = default!;
        public string[] ReceivingCountry { get; set; } = default!;
        public string MessageType { get; set; } = default!;
        public string Language { get; set; } = default!;
        public string Warning { get; set; } = default!;
        public string Contact { get; set; } = default!;
        public string MessageRefId { get; set; } = default!;
        public string MessageTypeIndic { get; set; } = default!;
        public string[] CorrMessageRefId { get; set; } = default!;
        public DateTime ReportingPeriod { get; set; } = default!;
        public DateTime Timestamp { get; set; } = default!;
    }
}