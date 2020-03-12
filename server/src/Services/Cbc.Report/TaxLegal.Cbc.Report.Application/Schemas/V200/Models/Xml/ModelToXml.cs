using System.Collections.Generic;
using System.Linq;
using Core.Domain.Enums.Countries;
using Core.Domain.Enums.Languages;
using TaxLegal.Cbc.Report.Application.Dto;
using static TaxLegal.Cbc.Report.Application.Schemas.Helper;

namespace TaxLegal.Cbc.Report.Application.Schemas.V200.Models.Xml
{
    public static class ModelToXml
    {
        public static CBC_OECD Convert(ReportData data)
        {
            return new CBC_OECD
            {
                MessageSpec = GetMessage(data.Message),
                CbcBody = data.Reports.SelectOrEmpty(GetCbcBody).ToArray(),
                version = data.Version.GetDescription(),
            };
        }

        private static MessageSpec_Type? GetMessage(Message e)
        {
            if (e is null)
                return default;

            return new MessageSpec_Type
            {
                SendingEntityIN = e.SendingEntityIn.StringOrNull(),
                TransmittingCountry = Parse<CountryCode_Type>(e.Jurisdiction),
                ReceivingCountry = e.Jurisdictions.SelectOrEmpty(x => Parse<CountryCode_Type>(x)).ToArray(),
                MessageType = Parse<MessageType_EnumType>(e.Type),
                Language = ParseOrDefault<LanguageCode, LanguageCode_Type>(e.Language),
                LanguageSpecified = e.Language.HasValue,
                Warning = e.Warning.StringOrNull(),
                Contact = e.Contact.StringOrNull(),
                MessageRefId = e.RefId.StringOrNull(),
                MessageTypeIndic = Parse<CbcMessageTypeIndic_EnumType>(e.TypeIndic.Value),
                CorrMessageRefId = e.CorrMessageRefId.SelectOrEmpty(x => x.StringOrNull()).ToArray(),
                ReportingPeriod = e.ReportingPeriod,
                Timestamp = e.Timestamp,
            };
        }

        private static CbcBody_Type? GetCbcBody(Dto.Report e)
        {
            if (e is null)
                return default;

            return new CbcBody_Type
            {
                ReportingEntity = GetReportingEntity(e.ReportingEntity),
                CbcReports = e.Reports
                    .SelectOrEmpty(x => GetCbcReport(
                        x, e.ConstituentEntities.Where(c => c.Jurisdiction == x.Jurisdiction).ToArray()
                    ))
                    .ToArray(),
                AdditionalInfo = e.AdditionalInfo.SelectOrEmpty(GetAdditionalInfo).ToArray(),
            };
        }

        private static CorrectableReportingEntity_Type? GetReportingEntity(ReportingEntity e)
        {
            if (e is null)
                return default;

            return new CorrectableReportingEntity_Type
            {
                Entity = GetOrganization(e.Organisation),
                NameMNEGroup = e.NameMNEGroup.StringOrNull(),
                ReportingRole = Parse<CbcReportingRole_EnumType>(e.Role),
                ReportingPeriod = new ReportingEntity_TypeReportingPeriod
                {
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                },
                DocSpec = GetDocSpec(e.Doc),
            };
        }

        private static OrganisationParty_Type? GetOrganization(Organisation e)
        {
            if (e is null)
                return default;

            return new OrganisationParty_Type
            {
                ResCountryCode = e.Jurisdictions.SelectOrEmpty(x => Parse<CountryCode_Type>(x)).ToArray(),
                TIN = e.HasTin ? GetTin(e.Tin) : GetDefaultTin(e.Jurisdictions.FirstOrDefault()),
                IN = e.In.SelectOrEmpty(GetIn).ToArray(),
                Name = e.Name.SelectOrEmpty(GetName).ToArray(),
                Address = e.Address.SelectOrEmpty(GetAddress).ToArray(),
            };
        }

        private static TIN_Type? GetTin(Tidn e)
        {
            if (e is null)
                return default;

            return new TIN_Type
            {
                issuedBy = ParseOrDefault<Alpha2Code, CountryCode_Type>(e.Jurisdiction),
                issuedBySpecified = e.Jurisdiction.HasValue,
                Value = e.Tin.StringOrNull(),
            };
        }

        private static TIN_Type GetDefaultTin(Alpha2Code e)
        {
            return new TIN_Type
            {
                issuedBy = Parse<CountryCode_Type>(e),
                issuedBySpecified = true,
                Value = NoTin,
            };
        }

        private static OrganisationIN_Type? GetIn(Idn e)
        {
            if (e is null)
                return default;

            return new OrganisationIN_Type
            {
                issuedBy = ParseOrDefault<Alpha2Code, CountryCode_Type>(e.Jurisdiction),
                issuedBySpecified = e.Jurisdiction.HasValue,
                INType = e.Type.StringOrNull(),
                Value = e.In.StringOrNull(),
            };
        }

        private static NameOrganisation_Type? GetName(string e)
        {
            return new NameOrganisation_Type { Value = e.StringOrNull() };
        }

        private static Address_Type? GetAddress(Address e)
        {
            if (e is null)
                return default;

            var items = new List<object>();
            if (e.IsFixed)
            {
                var fix = GetAddressFix(e.Fix);
                if (fix != null)
                    items.Add(fix);
            }

            if (!string.IsNullOrWhiteSpace(e.Free))
                items.Add(e.Free);

            return new Address_Type
            {
                CountryCode = Parse<CountryCode_Type>(e.Jurisdiction),
                Items = items
                    .SelectOrEmpty(x => x switch
                    {
                        string y => y.StringOrNull(),
                        _        => x,
                    })
                    .ToArray(),
                legalAddressType = ParseOrDefault<AddressTypeEnum, OECDLegalAddressType_EnumType>(e.Type),
                legalAddressTypeSpecified = e.Type.HasValue,
            };
        }

        private static AddressFix_Type? GetAddressFix(AddressFix e)
        {
            if (e is null)
                return default;

            return new AddressFix_Type
            {
                Street = e.Street.StringOrNull(),
                BuildingIdentifier = e.BuildingIdentifier.StringOrNull(),
                SuiteIdentifier = e.SuiteIdentifier.StringOrNull(),
                FloorIdentifier = e.FloorIdentifier.StringOrNull(),
                DistrictName = e.Districtname.StringOrNull(),
                POB = e.Pob.StringOrNull(),
                PostCode = e.PostCode.StringOrNull(),
                City = e.City.StringOrNull(),
                CountrySubentity = e.CountrySubentity.StringOrNull(),
            };
        }

        private static DocSpec_Type? GetDocSpec(Doc e)
        {
            if (e is null)
                return default;

            return new DocSpec_Type
            {
                DocTypeIndic = Parse<OECDDocTypeIndic_EnumType>(e.Type),
                DocRefId = e.RefId.StringOrNull(),
                CorrMessageRefId = e.CorrMessageRefId.StringOrNull(),
                CorrDocRefId = e.CorrDocRefId.StringOrNull(),
            };
        }

        private static CorrectableCbcReport_Type? GetCbcReport(ReportBody e, ConstituentEntity[] c)
        {
            if (e is null)
                return default;

            return new CorrectableCbcReport_Type
            {
                DocSpec = GetDocSpec(e.Doc),
                ResCountryCode = Parse<CountryCode_Type>(e.Jurisdiction),
                Summary = GetSummary(e.Summary),
                ConstEntities = c.SelectOrEmpty(GetConstituenEntity).ToArray(),
            };
        }

        private static ConstituentEntity_Type? GetConstituenEntity(ConstituentEntity e)
        {
            if (e is null)
                return default;

            return new ConstituentEntity_Type
            {
                ConstEntity = GetOrganization(e.Organisation),
                Role = ParseOrDefault<UltimateParrentEntityRoleEnum, UltimateParentEntityRole_EnumType>(e.Role),
                RoleSpecified = e.Role.HasValue,
                IncorpCountryCode = ParseOrDefault<Alpha2Code, CountryCode_Type>(e.Jurisdiction),
                IncorpCountryCodeSpecified = e.Jurisdiction.HasValue,
                BizActivities = e.BizActivities.SelectOrEmpty(x => Parse<CbcBizActivityType_EnumType>(x)).ToArray(),
                OtherEntityInfo = e.OtherInfo.StringOrNull(),
            };
        }

        private static CorrectableCbcReport_TypeSummary? GetSummary(Summary e)
        {
            if (e is null)
                return default;

            return new CorrectableCbcReport_TypeSummary
            {
                Revenues = new CorrectableCbcReport_TypeSummaryRevenues
                {
                    Unrelated = GetMonAmnt(e.Unrelated),
                    Related = GetMonAmnt(e.Related),
                    Total = GetMonAmnt(e.Total),
                },
                ProfitOrLoss = GetMonAmnt(e.ProfitOrLoss),
                TaxPaid = GetMonAmnt(e.TaxPaid),
                TaxAccrued = GetMonAmnt(e.TaxAccrued),
                Capital = GetMonAmnt(e.Capital),
                Earnings = GetMonAmnt(e.Earnings),
                NbEmployees = e.NbEmployees,
                Assets = GetMonAmnt(e.Assets),
            };
        }

        private static MonAmnt_Type? GetMonAmnt(MonAmnt e)
        {
            if (e is null)
                return default;

            return new MonAmnt_Type
            {
                currCode = Parse<currCode_Type>(e.Currency),
                Value = e.Value.StringOrNull(),
            };
        }

        private static CorrectableAdditionalInfo_Type? GetAdditionalInfo(AdditionalInfo e)
        {
            if (e is null)
                return default;

            return new CorrectableAdditionalInfo_Type
            {
                DocSpec = GetDocSpec(e.Doc),
                OtherInfo = e.OtherInfo.SelectOrEmpty(GetOtherInfo).ToArray(),
                ResCountryCode = e.Jurisdictions.SelectOrEmpty(x => Parse<CountryCode_Type>(x)).ToArray(),
                SummaryRef = e.SummaryTypes.SelectOrEmpty(x => Parse<CbcSummaryListElementsType_EnumType>(x)).ToArray(),
            };
        }

        private static StringMin1Max4000WithLang_Type? GetOtherInfo(OtherInfo e)
        {
            if (e is null)
                return default;

            return new StringMin1Max4000WithLang_Type
            {
                language = ParseOrDefault<LanguageCode, LanguageCode_Type>(e.Language),
                languageSpecified = e.Language.HasValue,
                Value = e.Info.StringOrNull(),
            };
        }
    }
}