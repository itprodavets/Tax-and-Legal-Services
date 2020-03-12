using System;
using Core.Domain.Enums.Countries;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class Organisation : BaseDto
    {
        public Alpha2Code[] Jurisdictions { get; set; } = Array.Empty<Alpha2Code>();
        public bool HasTin { get; set; }
        public Tidn Tin { get; set; } = new Tidn();
        public Idn[] In { get; set; } = Array.Empty<Idn>();
        public string[] Name { get; set; } = Array.Empty<string>();
        public Address[] Address { get; set; } = Array.Empty<Address>();
    }
}