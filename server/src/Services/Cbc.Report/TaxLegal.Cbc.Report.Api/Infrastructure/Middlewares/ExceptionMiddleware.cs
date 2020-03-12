using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TaxLegal.Cbc.Report.Application.Dto;

namespace TaxLegal.Cbc.Report.Api.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var (status, messages) = HandleException(exception);

                foreach (var error in messages.Where(x => x.Severity == ValidationSeverity.Error))
                    _logger.LogError(error.Message);

                context.Response.StatusCode = (int) status;
                context.Response.ContentType = MediaTypeNames.Application.Json;

                await context.Response.WriteAsync(JsonSerializer.Serialize(messages));
            }
        }

        private (HttpStatusCode, IReadOnlyCollection<ValidationMessage>) HandleException(Exception exception) => exception switch
        {
            ValidationException ex => (HttpStatusCode.BadRequest, new[] { new ValidationMessage(ValidationSeverity.Error, ex.Message) }),
            _                      => (HttpStatusCode.InternalServerError, new[] { new ValidationMessage(ValidationSeverity.Error, exception.Message) })
        };
    }
}