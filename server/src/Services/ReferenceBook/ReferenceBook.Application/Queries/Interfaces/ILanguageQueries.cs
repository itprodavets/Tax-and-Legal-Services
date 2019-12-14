using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceBook.Application.Dto.Languages;

namespace ReferenceBook.Application.Queries.Interfaces
{
	public interface ILanguageQueries
	{
		/// <summary>
		///  Get Languages
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<LanguageDto>> GetLanguagesAsync();
	}
}