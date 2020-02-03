namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class ValidationMessage
    {
        public ValidationSeverity Severity { get; }
        public string Message { get; }

        public ValidationMessage(
            ValidationSeverity severity,
            string message
        )
        {
            Severity = severity;
            Message = message;
        }
    }
}