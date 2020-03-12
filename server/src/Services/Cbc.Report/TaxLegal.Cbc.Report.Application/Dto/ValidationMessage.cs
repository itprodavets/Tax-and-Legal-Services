namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class ValidationMessage
    {
        public ValidationMessage(
            ValidationSeverity severity,
            string message
        )
        {
            Severity = severity;
            Message = message;
        }

        public ValidationSeverity Severity { get; }
        public string Message { get; }
    }
}