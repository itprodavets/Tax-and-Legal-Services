using System;

namespace TaxLegal.Cbc.Report.Api.Infrastructure
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}