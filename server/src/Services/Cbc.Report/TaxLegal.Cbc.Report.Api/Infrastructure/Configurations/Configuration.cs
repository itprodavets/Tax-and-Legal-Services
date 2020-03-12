namespace TaxLegal.Cbc.Report.Api.Infrastructure.Configurations
{
	public class Configuration
	{
        public string StoredFilesPath { get; set; } = default!;
        public int FileSizeLimit { get; set; } = default!;
        public AppConfiguration App { get; set; } = default!;
		public DbConfiguration Db { get; set; } = default!;
    }
}