namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class Summary
    {
        public MonAmnt Unrelated { get; set; } = default!;
        public MonAmnt Related { get; set; } = default!;
        public MonAmnt Total { get; set; } = default!;
        public MonAmnt ProfitOrLoss { get; set; } = default!;
        public MonAmnt TaxPaid { get; set; } = default!;
        public MonAmnt TaxAccrued { get; set; } = default!;
        public MonAmnt Capital { get; set; } = default!;
        public MonAmnt Earnings { get; set; } = default!;
        public string NbEmployees { get; set; } = default!;
        public MonAmnt Assets { get; set; } = default!;
    }
}