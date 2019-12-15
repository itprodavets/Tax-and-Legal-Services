using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceBook.Application.Dto.Countries;

namespace ReferenceBook.Application.Queries.Interfaces
{
	public interface ICountryQueries
	{
        Task<IEnumerable<CountryDto>> GetCountriesAsync();
	}
}