// using System.Linq;
// using TaxLegal.Cbc.Report.Application.Dto;
//
// namespace TaxLegal.Cbc.Report.Application.Schemas.V101.Models.Xml
// {
//     public class XmlToModel
//     {
//         public ReportData Parse()
//         {
//             var data = new ReportData
//             {
//                 MessageSpec = CreateMessageSpec()
//             };
//             return data;
//         }
//
//         private MessageSpec CreateMessageSpec()
//         {
//             var messageSpec = new MessageSpec()
//             {
//                 SendingEntityIn = MessageSpec.SendingEntityIN,
//                 TransmittingCountry = MessageSpec.TransmittingCountry.ToString("G"),
//                 ReceivingCountry = MessageSpec.ReceivingCountry.Select<Application.Xml.Schemas.V101.CountryCode_Type, string>(x => x.ToString("G")).ToArray(),
//                 MessageType = MessageSpec.MessageType.ToString("G"),
//                 Language = MessageSpec.Language.ToString("G"),
//                 Warning = MessageSpec.Warning,
//                 Contact = MessageSpec.Contact,
//                 MessageRefId = MessageSpec.MessageRefId,
//                 MessageTypeIndic = MessageSpec.MessageTypeIndic.ToString("G"),
//                 CorrMessageRefId = MessageSpec.CorrMessageRefId,
//                 ReportingPeriod = MessageSpec.ReportingPeriod,
//                 Timestamp = MessageSpec.Timestamp
//             };
//             return messageSpec;
//         }
//     }
// }