using System.Collections.Generic;
using System.Threading.Tasks;
using ReferenceBook.Application.Dto.Languages;

namespace ReferenceBook.Application.Queries.Interfaces
{
    public interface ILanguageQueries
    {
        Task<IEnumerable<LanguageDto>> GetLanguagesAsync();
    }
}