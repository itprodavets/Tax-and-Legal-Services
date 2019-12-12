using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceBook.Api.ViewModels.Languages;

namespace ReferenceBook.Api.Application.Queries.Interfaces
{
	public interface ILanguageQueries
	{
		/// <summary>
		/// 	Get Languages
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<LanguageViewModel>> GetLanguagesAsync();
	}
}