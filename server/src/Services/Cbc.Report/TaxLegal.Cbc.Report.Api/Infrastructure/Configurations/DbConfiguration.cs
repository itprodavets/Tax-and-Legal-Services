namespace TaxLegal.Cbc.Report.Api.Infrastructure.Configurations
{
    public class DbConfiguration
    {
        // public DbConfiguration(string host, string port, string name, string user, string password)
        // {
        //     if (IsNotValid) throw new ArgumentNullException(nameof(DbConfiguration));

        //     Host = host;
        //     Port = port;
        //     Name = name;
        //     User = user;
        //     Password = password;
        // }

        public string Host { get; set; } = default!;
        public int Port { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string User { get; set; } = default!;
        public string Password { get; set; } = default!;

        public string ToConnectionString() => string.Join(';',
            $"Host={Host}",
            $"Port={Port}",
            $"Database={Name}",
            $"Username={User}",
            $"Password={Password}");

        private bool IsNotValid =>
            string.IsNullOrEmpty(Host) &&
            Port > 0 &&
            string.IsNullOrEmpty(Name) &&
            string.IsNullOrEmpty(User) &&
            string.IsNullOrEmpty(Password);
    }
}