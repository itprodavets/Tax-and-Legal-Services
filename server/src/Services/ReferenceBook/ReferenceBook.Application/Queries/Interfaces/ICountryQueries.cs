using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceBook.Application.Dto.Countries;

namespace ReferenceBook.Application.Queries.Interfaces
{
	public interface ICountryQueries
	{
		/// <summary>
		///  Get Countries
		/// </summary>
		/// <returns></returns>
		Task<IEnumerable<CountryDto>> GetCountriesAsync();
	}
}