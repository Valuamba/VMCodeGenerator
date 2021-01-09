using System.Xml.Serialization;

namespace CodeGenerator.Model
{
    [XmlRoot]
    public class Config
    {
        [XmlElement]
        public Query Query { get; set; }
    }
}
