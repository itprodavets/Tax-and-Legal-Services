using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReferenceBook.Application.Dto.Countries;
using ReferenceBook.Application.Queries.Interfaces;

namespace ReferenceBook.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryQueries _countryQueries;

        public CountriesController(ICountryQueries countryQueries)
        {
            _countryQueries = countryQueries ?? throw new ArgumentNullException(nameof(countryQueries));
        }

        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<CountryDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountriesAsync()
        {
            var countries = await _countryQueries.GetCountriesAsync();
            return Ok(countries);
        }
    }
}