namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class Doc
    {
        public DocTypeEnum Type { get; set; }
        public string RefId { get; set; } = default!;
        public string CorrMessageRefId { get; set; } = default!;
        public string CorrDocRefId { get; set; } = default!;
    }
}