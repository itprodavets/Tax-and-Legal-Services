namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class Summary : BaseDto
    {
        public MonAmnt Unrelated { get; set; } = new MonAmnt();
        public MonAmnt Related { get; set; } = new MonAmnt();
        public MonAmnt Total { get; set; } = new MonAmnt();
        public MonAmnt ProfitOrLoss { get; set; } = new MonAmnt();
        public MonAmnt TaxPaid { get; set; } = new MonAmnt();
        public MonAmnt TaxAccrued { get; set; } = new MonAmnt();
        public MonAmnt Capital { get; set; } = new MonAmnt();
        public MonAmnt Earnings { get; set; } = new MonAmnt();
        public string NbEmployees { get; set; } = string.Empty;
        public MonAmnt Assets { get; set; } = new MonAmnt();
    }
}