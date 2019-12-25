using System.Linq;
using System.Text;
using Common.Extensions;
using TaxLegal.Cbc.Report.Application.Dto;
using TaxLegal.Cbc.Report.Application.Xml.Interfaces;
using V100 = TaxLegal.Cbc.Report.Application.Xml.Schemas.V100;
using V101 = TaxLegal.Cbc.Report.Application.Xml.Schemas.V101;
using V200 = TaxLegal.Cbc.Report.Application.Xml.Schemas.V200;

namespace TaxLegal.Cbc.Report.Application.Xml.Implementation.Builders
{
    public class CbcOecdBuilder : IXmlFileBuilder
    {
        public IXmlFile Build(SupportedSchema schema, ReportData data)
        {
            switch (schema)
            {
                case SupportedSchema.CBC_OECD_100:
                    return BuildSchema100(data);
                case SupportedSchema.CBC_OECD_101:
                    return BuildSchema101(data);
                case SupportedSchema.CBC_OECD_200:
                    return BuildSchema200(data);
                default: return BuildSchema200(data);
            }
        }

        #region Build Schema
        private XmlFile BuildSchema100(ReportData data)
        {
            var content = new V100.CBC_OECD()
            {
                MessageSpec  = new V100.MessageSpec_Type()
                {
                    SendingEntityIN = data.MessageSpec.SendingEntityIn,
                    TransmittingCountry = data.MessageSpec.TransmittingCountry.ToEnum<V100.CountryCode_Type>(),
                    ReceivingCountry = data.MessageSpec.ReceivingCountry.Select(code => code.ToEnum<V100.CountryCode_Type>()).ToArray(),
                    MessageType = data.MessageSpec.MessageType.ToEnum<V100.MessageType_EnumType>(),
                    Language = data.MessageSpec.Language.ToEnum<V100.LanguageCode_Type>(),
                    LanguageSpecified = true,
                    Warning = string.IsNullOrEmpty(data.MessageSpec.Warning) ? null : data.MessageSpec.Warning,
                    Contact = string.IsNullOrEmpty(data.MessageSpec.Contact) ? null : data.MessageSpec.Contact,
                    MessageRefId = data.MessageSpec.MessageRefId,
                    MessageTypeIndic = data.MessageSpec.MessageTypeIndic.ToEnum<V100.CbcMessageTypeIndic_EnumType>(),
                    MessageTypeIndicSpecified = true,
                    CorrMessageRefId = data.MessageSpec.CorrMessageRefId.Length == 0 ? null : data.MessageSpec.CorrMessageRefId,
                    ReportingPeriod = data.MessageSpec.ReportingPeriod,
                    Timestamp = data.MessageSpec.Timestamp
                },
                version = "1.0.0"
            };
            var fileName = content.MessageSpec.MessageRefId;

            return new XmlFile(fileName, content, Encoding.GetEncoding(1251));
        }

        private XmlFile BuildSchema101(ReportData data)
        {
            var content = new V101.CBC_OECD()
            {
                MessageSpec  = new V101.MessageSpec_Type()
                {
                    SendingEntityIN = data.MessageSpec.SendingEntityIn,
                    TransmittingCountry = data.MessageSpec.TransmittingCountry.ToEnum<V101.CountryCode_Type>(),
                    ReceivingCountry = data.MessageSpec.ReceivingCountry.Select(code => code.ToEnum<V101.CountryCode_Type>()).ToArray(),
                    MessageType = data.MessageSpec.MessageType.ToEnum<V101.MessageType_EnumType>(),
                    Language = data.MessageSpec.Language.ToEnum<V101.LanguageCode_Type>(),
                    LanguageSpecified = true,
                    Warning = string.IsNullOrEmpty(data.MessageSpec.Warning) ? null : data.MessageSpec.Warning,
                    Contact = string.IsNullOrEmpty(data.MessageSpec.Contact) ? null : data.MessageSpec.Contact,
                    MessageRefId = data.MessageSpec.MessageRefId,
                    MessageTypeIndic = data.MessageSpec.MessageTypeIndic.ToEnum<V101.CbcMessageTypeIndic_EnumType>(),
                    MessageTypeIndicSpecified = true,
                    CorrMessageRefId = data.MessageSpec.CorrMessageRefId.Length == 0 ? null : data.MessageSpec.CorrMessageRefId,
                    ReportingPeriod = data.MessageSpec.ReportingPeriod,
                    Timestamp = data.MessageSpec.Timestamp
                },
                version = "1.0.1"
            };
            var fileName = content.MessageSpec.MessageRefId;
            return new XmlFile(fileName, content, Encoding.GetEncoding(1251));
        }

        private XmlFile BuildSchema200(ReportData data)
        {
            var content = new V200.CBC_OECD()
            {
                MessageSpec  = new V200.MessageSpec_Type()
                {
                    SendingEntityIN = data.MessageSpec.SendingEntityIn,
                    TransmittingCountry = data.MessageSpec.TransmittingCountry.ToEnum<V200.CountryCode_Type>(),
                    ReceivingCountry = data.MessageSpec.ReceivingCountry.Select(code => code.ToEnum<V200.CountryCode_Type>()).ToArray(),
                    MessageType = data.MessageSpec.MessageType.ToEnum<V200.MessageType_EnumType>(),
                    Language = data.MessageSpec.Language.ToEnum<V200.LanguageCode_Type>(),
                    LanguageSpecified = true,
                    Warning = string.IsNullOrEmpty(data.MessageSpec.Warning) ? null : data.MessageSpec.Warning,
                    Contact = string.IsNullOrEmpty(data.MessageSpec.Contact) ? null : data.MessageSpec.Contact,
                    MessageRefId = data.MessageSpec.MessageRefId,
                    MessageTypeIndic = data.MessageSpec.MessageTypeIndic.ToEnum<V200.CbcMessageTypeIndic_EnumType>(),
                    CorrMessageRefId = data.MessageSpec.CorrMessageRefId.Length == 0 ? null : data.MessageSpec.CorrMessageRefId,
                    ReportingPeriod = data.MessageSpec.ReportingPeriod,
                    Timestamp = data.MessageSpec.Timestamp
                },
                version = "2.0.0"
            };
            var fileName = content.MessageSpec.MessageRefId;
            return new XmlFile(fileName, content, Encoding.GetEncoding(1251));
        }
        #endregion
    }
}