using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceBook.Api.ViewModels.Countries;

namespace ReferenceBook.Api.Application.Queries.Interfaces
{
	public interface ICountryQueries
	{
		/// <summary>
		///  Get Countries
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<CountryViewModel>> GetCountriesAsync();
	}
}