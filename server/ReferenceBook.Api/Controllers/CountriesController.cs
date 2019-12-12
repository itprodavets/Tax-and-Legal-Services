using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReferenceBook.Api.Application.Queries.Interfaces;
using ReferenceBook.Api.ViewModels.Countries;

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
		[ProducesResponseType(typeof(IEnumerable<CountryViewModel>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<CountryViewModel>>> GetCountriesAsync()
		{
			var countries = await _countryQueries.GetCountriesAsync();
			return Ok(countries);
		}
	}
}