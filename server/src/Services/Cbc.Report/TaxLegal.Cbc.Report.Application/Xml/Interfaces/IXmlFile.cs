using System.Text;

namespace TaxLegal.Cbc.Report.Application.Xml.Interfaces
{
    public interface IXmlFile
    {
        string FileName { get; }
        Encoding Encoding { get; }
        object Content { get; }
    }
}
