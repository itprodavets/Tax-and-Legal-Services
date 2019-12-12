using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using ReferenceBook.Api.Application.Queries.Interfaces;
using ReferenceBook.Api.ViewModels.Languages;

namespace ReferenceBook.Api.Application.Queries.Implementations
{
	public class LanguageQueries : ILanguageQueries
	{
		private readonly string _connectionString;

		public LanguageQueries(string connectionString)
		{
			_connectionString = !string.IsNullOrWhiteSpace(connectionString) ? connectionString : throw new ArgumentNullException(nameof(connectionString));
		}

        /// <summary>
        ///  Get Languages
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LanguageViewModel>> GetLanguagesAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            connection.Open();

            return await connection.QueryAsync<LanguageViewModel>(SQL_Resources.languages);
        }
	}
}