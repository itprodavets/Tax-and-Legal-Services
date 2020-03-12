using System.Linq;
using Core.Domain.Enums.Countries;
using Core.Domain.Enums.Currencies;
using Core.Domain.Enums.Languages;
using TaxLegal.Cbc.Report.Application.Dto;
using static TaxLegal.Cbc.Report.Application.Schemas.Helper;

namespace TaxLegal.Cbc.Report.Application.Schemas.V100.Models.Xml
{
    public static class XmlToModel
    {
        public static ReportData Convert(CBC_OECD model)
        {
            return new ReportData
            {
                Version = SupportedSchema.OECD_101,
                Message = GetMessage(model.MessageSpec),
                Reports = model.CbcBody.SelectOrEmpty(GetReport).ToArray(),
            };
        }

        private static Message GetMessage(MessageSpec_Type e)
        {
            if (e is null)
                return new Message();

            return new Message
            {
                SendingEntityIn = e.SendingEntityIN,
                Jurisdiction = Parse<Alpha2Code>(e.TransmittingCountry),
                Jurisdictions = e.ReceivingCountry.SelectOrEmpty(x => Parse<Alpha2Code>(x)).ToArray(),
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
            if (e is null)
                return new Dto.Report();

            return new Dto.Report
            {
                ReportingEntity = GetReportingEntity(e.ReportingEntity),
                ConstituentEntities = e.CbcReports
                    .SelectManyOrEmpty(x => x.ConstEntities)
                    .SelectOrEmpty(GetConstituentEntity)
                    .ToArray(),
                AdditionalInfo = e.AdditionalInfo.SelectOrEmpty(GetAdditionalInfo).ToArray(),
                Reports = e.CbcReports.SelectOrEmpty(GetReportBody).ToArray(),
            };
        }

        private static ReportingEntity GetReportingEntity(CorrectableReportingEntity_Type e)
        {
            if (e is null)
                return new ReportingEntity();

            return new ReportingEntity
            {
                Doc = GetDoc(e.DocSpec),
                Organisation = GetOrganisation(e.Entity),
                NameMNEGroup = string.Empty,
                Role = Parse<ReportingRoleEnum>(e.ReportingRole),
                StartDate = default,
                EndDate = default,
            };
        }

        private static Doc GetDoc(DocSpec_Type e)
        {
            if (e is null)
                return new Doc();

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
            if (e is null)
                return new Organisation();

            var hasTin = e.TIN != null && !string.IsNullOrWhiteSpace(e.TIN.Value) && e.TIN.Value.ToUpper() != NoTin;

            return new Organisation
            {
                Jurisdictions = e.ResCountryCode.SelectOrEmpty(x => Parse<Alpha2Code>(x)).ToArray(),
                HasTin = hasTin,
                Tin = hasTin ? GetTin(e.TIN) : new Tidn { Tin = NoTin },
                In = e.IN.SelectOrEmpty(GetIn).ToArray(),
                Name = e.Name.SelectOrEmpty(x => x.Value).ToArray(),
                Address = e.Address.SelectOrEmpty(GetAddress).ToArray(),
            };
        }

        private static Tidn GetTin(TIN_Type e)
        {
            if (e is null)
                return new Tidn();

            return new Tidn
            {
                Jurisdiction = Parse<Alpha2Code>(e.issuedBy),
                Tin = e.Value,
            };
        }

        private static Idn GetIn(OrganisationIN_Type e)
        {
            if (e is null)
                return new Idn();

            return new Idn
            {
                Jurisdiction = Parse<Alpha2Code>(e.issuedBy),
                In = e.Value,
                Type = e.INType,
            };
        }

        private static AdditionalInfo GetAdditionalInfo(CorrectableAdditionalInfo_Type e)
        {
            if (e is null)
                return new AdditionalInfo();

            return new AdditionalInfo
            {
                Doc = GetDoc(e.DocSpec),
                OtherInfo = new[] { new OtherInfo { Info = e.OtherInfo } },
                Jurisdictions = e.ResCountryCode.SelectOrEmpty(x => Parse<Alpha2Code>(x)).ToArray(),
                SummaryTypes = e.SummaryRef.SelectOrEmpty(x => Parse<SummaryTypeEnum>(x)).ToArray(),
            };
        }

        private static ReportBody GetReportBody(CorrectableCbcReport_Type e)
        {
            if (e is null)
                return new ReportBody();

            return new ReportBody
            {
                Doc = GetDoc(e.DocSpec),
                Jurisdiction = Parse<Alpha2Code>(e.ResCountryCode),
                Summary = GetSummary(e.Summary),
            };
        }

        private static Summary GetSummary(CorrectableCbcReport_TypeSummary e)
        {
            if (e is null)
                return new Summary();

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
            if (e is null)
                return new MonAmnt();

            return new MonAmnt
            {
                Currency = Parse<CurrencyCode>(e.currCode),
                Value = e.Value,
            };
        }

        private static ConstituentEntity GetConstituentEntity(ConstituentEntity_Type e)
        {
            if (e is null)
                return new ConstituentEntity();

            return new ConstituentEntity
            {
                Organisation = GetOrganisation(e.ConstEntity),
                Jurisdiction = Parse<Alpha2Code>(e.IncorpCountryCode),
                BizActivities = e.BizActivities.SelectOrEmpty(x => Parse<BizActivityTypeEnum>(x)).ToArray(),
                OtherInfo = e.OtherEntityInfo,
            };
        }


        private static Address GetAddress(Address_Type e)
        {
            if (e is null)
                return new Address();

            var fix = e.Items.OfTypeOrEmpty<AddressFix_Type>().FirstOrDefault();
            var free = e.Items.OfTypeOrEmpty<string>().FirstOrDefault();

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
            if (e is null)
                return new AddressFix();

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