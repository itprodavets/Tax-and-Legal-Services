using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using ReferenceBook.Application.Dto.Countries;
using ReferenceBook.Application.Queries.Interfaces;

namespace ReferenceBook.Application.Queries.Implementations
{
    public class CountryQueries : ICountryQueries
    {
        private readonly string _connectionString;

        public CountryQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<CountryDto>> GetCountriesAsync()
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            connection.Open();

            return await connection.QueryAsync<CountryDto>(SQL_Resources.countries);
        }
    }
}