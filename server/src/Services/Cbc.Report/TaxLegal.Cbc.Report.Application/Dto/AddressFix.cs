namespace TaxLegal.Cbc.Report.Application.Dto
{
    public class AddressFix
    {
        public string Street { get; set; } = default!;
        public string BuildingIdentifier { get; set; } = default!;
        public string SuiteIdentifier { get; set; } = default!;
        public string FloorIdentifier { get; set; } = default!;
        public string Districtname { get; set; } = default!;
        public string Pob { get; set; } = default!;
        public string PostCode { get; set; } = default!;
        public string City { get; set; } = default!;
        public string CountrySubentity { get; set; } = default!;
    }
}