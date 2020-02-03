using System;
namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class BaseDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}