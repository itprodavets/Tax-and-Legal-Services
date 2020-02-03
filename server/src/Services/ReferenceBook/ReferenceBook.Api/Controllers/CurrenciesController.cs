using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReferenceBook.Application.Dto.Currencies;
using ReferenceBook.Application.Queries.Interfaces;

namespace ReferenceBook.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ICurrencyQueries _currencyQueries;

        public CurrenciesController(ICurrencyQueries currencyQueries)
        {
            _currencyQueries = currencyQueries ?? throw new ArgumentNullException(nameof(currencyQueries));
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<CurrencyDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CurrencyDto>>> GetCurrenciesAsync()
        {
            var countries = await _currencyQueries.GetCurrenciesAsync();
            return Ok(countries);
        }
    }
}