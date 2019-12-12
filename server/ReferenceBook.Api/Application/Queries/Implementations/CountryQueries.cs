using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using ReferenceBook.Api.Application.Queries.Interfaces;
using ReferenceBook.Api.ViewModels.Countries;

namespace ReferenceBook.Api.Application.Queries.Implementations
{
	public class CountryQueries : ICountryQueries
	{
		private readonly string _connectionString;

		public CountryQueries(string connectionString)
		{
			_connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
		}

		/// <summary>
		///  Get Countries
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<CountryViewModel>> GetCountriesAsync()
		{
			await using var connection = new SqlConnection(_connectionString);

			connection.Open();

			return await connection.QueryAsync<CountryViewModel>(SQL_Resources.countries);
		}
	}
}