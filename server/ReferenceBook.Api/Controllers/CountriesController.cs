using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReferenceBook.Application.Dto.Countries;
using ReferenceBook.Application.Queries.Interfaces;

namespace ReferenceBook.Api.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class CountriesController : ControllerBase
	{
		private readonly ICountryQueries _countryQueries;
		private readonly ILogger<CountriesController> _logger;

		public CountriesController(ICountryQueries countryQueries, ILogger<CountriesController> logger)
		{
			_countryQueries = countryQueries ?? throw new ArgumentNullException(nameof(countryQueries));
			_logger = logger;
		}

		[HttpGet("list")]
		[ProducesResponseType(typeof(IEnumerable<CountryDto>), (int) HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountriesAsync()
		{
			var countries = await _countryQueries.GetCountriesAsync();
			return Ok(countries);
		}
	}
}