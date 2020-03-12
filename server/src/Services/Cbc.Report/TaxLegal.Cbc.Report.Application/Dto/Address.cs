using Core.Domain.Enums.Countries;

namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class Address : BaseDto
    {
        public Alpha2Code Jurisdiction { get; set; }
        public AddressTypeEnum? Type { get; set; }
        public bool IsFixed { get; set; }
        public AddressFix Fix { get; set; } = new AddressFix();
        public string Free { get; set; } = string.Empty;
    }
}