using System;
using System.Linq;
using Core.Domain.Enums.Countries;
using Core.Domain.Enums.Currencies;
using Core.Domain.Enums.Languages;
using TaxLegal.Cbc.Report.Application.Dto;
using static TaxLegal.Cbc.Report.Application.Schemas.Helper;

namespace TaxLegal.Cbc.Report.Application.Schemas.V200.Models.Xml
{
    public static class XmlToModel
    {
        public static ReportData Convert(CBC_OECD model)
        {
            return new ReportData
            {
                Version = SupportedSchema.OECD_200,
                Message = GetMessage(model.MessageSpec),
                Reports = model.CbcBody.Select(GetReport).ToArray(),
            };
        }

        private static Message GetMessage(MessageSpec_Type e)
        {
            return new Message
            {
                SendingEntityIn = e.SendingEntityIN,
                Jurisdiction = Parse<Alpha2Code>(e.TransmittingCountry),
                Jurisdictions = e.ReceivingCountry?.Select(x => Parse<Alpha2Code>(x)).ToArray() ?? Array.Empty<Alpha2Code>(),
                Type = Parse<MessageTypeEnum>(e.MessageType),
                Language = Parse<LanguageCode>(e.Language),
                Warning = e.Warning,
                Contact = e.Contact,
                RefId = e.MessageRefId,
                TypeIndic = Parse<MessageTypeIndicEnum>(e.MessageTypeIndic),
                CorrMessageRefId = e.CorrMessageRefId,
                ReportingPeriod = e.ReportingPeriod,
                Timestamp = e.Timestamp,
            };
        }

        private static Dto.Report GetReport(CbcBody_Type e)
        {
            return new Dto.Report
            {
                ReportingEntity = GetReportingEntity(e.ReportingEntity),
                ConstituentEntities = e.CbcReports?.SelectMany(x => x.ConstEntities).Select(GetConstituentEntity).ToArray() ?? Array.Empty<ConstituentEntity>(),
                AdditionalInfo = e.AdditionalInfo?.Select(GetAdditionalInfo).ToArray() ?? Array.Empty<AdditionalInfo>(),
                Reports = e.CbcReports?.Select(GetReportBody).ToArray() ?? Array.Empty<ReportBody>(),
            };
        }

        private static ReportingEntity GetReportingEntity(CorrectableReportingEntity_Type e)
        {
            return new ReportingEntity
            {
                Doc = GetDoc(e.DocSpec),
                Organisation = GetOrganisation(e.Entity),
                NameMNEGroup = e.NameMNEGroup,
                Role = Parse<ReportingRoleEnum>(e.ReportingRole),
                StartDate = e.ReportingPeriod.StartDate,
                EndDate = e.ReportingPeriod.EndDate,
            };
        }

        private static Doc GetDoc(DocSpec_Type e)
        {
            return new Doc
            {
                Type = Parse<DocTypeEnum>(e.DocTypeIndic),
                RefId = e.DocRefId,
                CorrDocRefId = e.CorrMessageRefId,
                CorrMessageRefId = e.CorrMessageRefId,
            };
        }

        private static Organisation GetOrganisation(OrganisationParty_Type e)
        {
            var hasTin = e.TIN != null && !string.IsNullOrWhiteSpace(e.TIN.Value) && e.TIN.Value.ToUpper() != NoTin;

            return new Organisation
            {
                Jurisdictions = e.ResCountryCode?.Select(x => Parse<Alpha2Code>(x)).ToArray() ?? Array.Empty<Alpha2Code>(),
                HasTin = hasTin,
                Tin = hasTin ? GetTin(e.TIN) : new Tidn { Tin = NoTin },
                In = e.IN?.Select(GetIn).ToArray() ?? Array.Empty<Idn>(),
                Name = e.Name?.Select(x => x.Value).ToArray() ?? Array.Empty<string>(),
                Address = e.Address?.Select(GetAddress).ToArray() ?? Array.Empty<Address>(),
            };
        }

        private static Tidn GetTin(TIN_Type e)
        {
            return new Tidn
            {
                Jurisdiction = Parse<Alpha2Code>(e.issuedBy),
                Tin = e.Value,
            };
        }

        private static Idn GetIn(OrganisationIN_Type e)
        {
            return new Idn
            {
                Jurisdiction = Parse<Alpha2Code>(e.issuedBy),
                In = e.Value,
                Type = e.INType,
            };
        }

        private static AdditionalInfo GetAdditionalInfo(CorrectableAdditionalInfo_Type e)
        {
            return new AdditionalInfo
            {
                Doc = GetDoc(e.DocSpec),
                OtherInfo = e.OtherInfo.Select(GetOtherInfo).ToArray(),
                Jurisdictions = e.ResCountryCode?.Select(x => Parse<Alpha2Code>(x)).ToArray() ?? Array.Empty<Alpha2Code>(),
                SummaryTypes = e.SummaryRef?.Select(x => Parse<SummaryTypeEnum>(x)).ToArray() ?? Array.Empty<SummaryTypeEnum>(),
            };
        }

        private static OtherInfo GetOtherInfo(StringMin1Max4000WithLang_Type e)
        {
            return new OtherInfo
            {
                Info = e.Value,
                Language = Parse<LanguageCode>(e.language),
            };
        }

        private static ReportBody GetReportBody(CorrectableCbcReport_Type e)
        {
            return new ReportBody
            {
                Doc = GetDoc(e.DocSpec),
                Jurisdiction = Parse<Alpha2Code>(e.ResCountryCode),
                Summary = GetSummary(e.Summary),
            };
        }

        private static Summary GetSummary(CorrectableCbcReport_TypeSummary e)
        {
            return new Summary
            {
                Unrelated = GetMonAmnt(e.Revenues.Unrelated),
                Related = GetMonAmnt(e.Revenues.Related),
                Total = GetMonAmnt(e.Revenues.Total),
                ProfitOrLoss = GetMonAmnt(e.ProfitOrLoss),
                TaxPaid = GetMonAmnt(e.TaxPaid),
                TaxAccrued = GetMonAmnt(e.TaxAccrued),
                Capital = GetMonAmnt(e.Capital),
                Earnings = GetMonAmnt(e.Earnings),
                NbEmployees = e.NbEmployees,
                Assets = GetMonAmnt(e.Assets),
            };
        }

        private static MonAmnt GetMonAmnt(MonAmnt_Type e)
        {
            return new MonAmnt
            {
                Currency = Parse<CurrencyCode>(e.currCode),
                Value = e.Value,
            };
        }

        private static ConstituentEntity GetConstituentEntity(ConstituentEntity_Type e)
        {
            return new ConstituentEntity
            {
                Organisation = GetOrganisation(e.ConstEntity),
                Role = Parse<UltimateParrentEntityRoleEnum>(e.Role),
                Jurisdiction = Parse<Alpha2Code>(e.IncorpCountryCode),
                BizActivities = e.BizActivities.Select(x => Parse<BizActivityTypeEnum>(x)).ToArray(),
                OtherInfo = e.OtherEntityInfo,
            };
        }


        private static Address GetAddress(Address_Type e)
        {
            var fix = e.Items.OfType<AddressFix_Type>().FirstOrDefault();
            var free = e.Items.OfType<string>().FirstOrDefault();

            return new Address
            {
                Jurisdiction = Parse<Alpha2Code>(e.CountryCode),
                Type = Parse<AddressTypeEnum>(e.legalAddressType),
                IsFixed = fix != null,
                Fix = fix is null ? default! : GetAddressFix(fix),
                Free = free,
            };
        }

        private static AddressFix GetAddressFix(AddressFix_Type e)
        {
            return new AddressFix
            {
                Street = e.Street,
                BuildingIdentifier = e.BuildingIdentifier,
                SuiteIdentifier = e.SuiteIdentifier,
                FloorIdentifier = e.FloorIdentifier,
                Districtname = e.DistrictName,
                Pob = e.POB,
                PostCode = e.PostCode,
                City = e.City,
                CountrySubentity = e.CountrySubentity,
            };
        }
    }
}