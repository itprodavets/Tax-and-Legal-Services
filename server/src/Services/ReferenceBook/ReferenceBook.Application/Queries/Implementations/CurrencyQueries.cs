using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using ReferenceBook.Application.Dto.Currencies;
using ReferenceBook.Application.Queries.Interfaces;

namespace ReferenceBook.Application.Queries.Implementations
{
    public class CurrencyQueries : ICurrencyQueries
    {
        private readonly string _connectionString;

        public CurrencyQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<CurrencyDto>> GetCurrenciesAsync()
        {
            await using var connection = new NpgsqlConnection(_connectionString);

            connection.Open();

            var currencies = await connection.QueryAsync<CurrencyDto>(SQL_Resources.currencies);
            return currencies;
        }
    }
}