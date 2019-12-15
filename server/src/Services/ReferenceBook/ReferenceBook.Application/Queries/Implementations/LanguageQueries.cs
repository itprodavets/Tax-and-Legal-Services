using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using ReferenceBook.Api.Application;
using ReferenceBook.Application.Dto.Languages;
using ReferenceBook.Application.Queries.Interfaces;

namespace ReferenceBook.Application.Queries.Implementations
{
    public class LanguageQueries : ILanguageQueries
    {
        private readonly string _connectionString;

        public LanguageQueries(string connectionString)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<IEnumerable<LanguageDto>> GetLanguagesAsync()
        {
            await using var connection = new SqlConnection(_connectionString);

            connection.Open();

            return await connection.QueryAsync<LanguageDto>(SQL_Resources.languages);
        }
    }
}