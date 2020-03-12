namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class AddressFix : BaseDto
    {
        public string Street { get; set; } = string.Empty;
        public string BuildingIdentifier { get; set; } = string.Empty;
        public string SuiteIdentifier { get; set; } = string.Empty;
        public string FloorIdentifier { get; set; } = string.Empty;
        public string Districtname { get; set; } = string.Empty;
        public string Pob { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string CountrySubentity { get; set; } = string.Empty;
    }
}