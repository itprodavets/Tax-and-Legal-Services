namespace TaxLegal.Cbc.Report.Api.Infrastructure.Configurations
{
	public class Configuration
	{
        public AppConfiguration App { get; set; } = default!;
		public DbConfiguration Db { get; set; } = default!;
    }
}