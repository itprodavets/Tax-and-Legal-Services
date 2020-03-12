using System.Collections.Generic;
using System.IO;
using TaxLegal.Cbc.Report.Application.Dto;

namespace TaxLegal.Cbc.Report.Application.Services.Interfaces
{
    public interface ISchemaService
    {
        ReportData Parse(object raw);
        object Generate(ReportData data);
        IReadOnlyCollection<ValidationMessage> Validate(Stream file);
    }
}