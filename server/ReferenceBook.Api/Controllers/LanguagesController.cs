using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReferenceBook.Api.Application.Queries.Interfaces;
using ReferenceBook.Api.ViewModels.Languages;

namespace ReferenceBook.Api.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class LanguagesController : ControllerBase
	{
		private readonly ILanguageQueries _languageQueries;
		private readonly ILogger<CountriesController> _logger;

		public LanguagesController(ILanguageQueries languageQueries, ILogger<CountriesController> logger)
		{
			_languageQueries = languageQueries ?? throw new ArgumentNullException(nameof(languageQueries));
			_logger = logger;
		}
		
		[HttpGet("list")]
		[ProducesResponseType(typeof(IEnumerable<LanguageViewModel>), (int)HttpStatusCode.OK)]
		public async Task<ActionResult<IEnumerable<LanguageViewModel>>> GetLanguagesAsync()
		{
			var countries = await _languageQueries.GetLanguagesAsync();
			return Ok(countries);
		}
	}
}