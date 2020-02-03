using System.Text;
using TaxLegal.Cbc.Report.Application.Xml.Interfaces;

namespace TaxLegal.Cbc.Report.Application.Xml.Implementation
{
    public class XmlFile : IXmlFile
    {
        public XmlFile(string fileName, object content, Encoding encoding = null!)
        {
            FileName = fileName;
            Encoding = encoding ?? Encoding.UTF8;
            Content = content;
        }

        public string FileName { get; }

        public Encoding Encoding { get; }

        public object Content { get; }
    }
}
