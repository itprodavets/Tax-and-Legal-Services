using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceBook.Application.Dto.Currencies;

namespace ReferenceBook.Application.Queries.Interfaces
{
    public interface ICurrencyQueries
    {
        Task<IEnumerable<CurrencyDto>> GetCurrenciesAsync();
    }
}