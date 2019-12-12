using System;

namespace ReferenceBook.Api.Infrastructure.Configurations
{
	public class DbConfiguration
	{
		public DbConfiguration(string host, string port, string name, string user, string password)
		{
			if (IsNotValid) throw new ArgumentNullException(nameof(DbConfiguration));

			Host = host;
			Port = port;
			Name = name;
			User = user;
			Password = password;
		}

		public string Host { get; }
		public string Port { get; }
		public string Name { get; }
		public string User { get; }
		public string Password { get; }

		private bool IsNotValid =>
			string.IsNullOrEmpty(Host) &&
			string.IsNullOrEmpty(Port) &&
			string.IsNullOrEmpty(Name) &&
			string.IsNullOrEmpty(User) &&
			string.IsNullOrEmpty(Password);
	}
}