namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class Doc : BaseDto
    {
        public DocTypeEnum Type { get; set; }
        public string RefId { get; set; } = string.Empty;
        public string CorrMessageRefId { get; set; } = string.Empty;
        public string CorrDocRefId { get; set; } = string.Empty;
    }
}