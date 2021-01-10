using System.Xml.Serialization;

namespace CodeGenerator.Model
{
    public class Query
    {
        [XmlElement]
        public string SolutionDirectory { get; set; }
        [XmlElement]
        public string ClassQueryProject { get; set; }
        [XmlElement]
        public string SqlQueryProject { get; set; }
        [XmlElement]
        public string ClassQueryDirectory { get; set; }
        [XmlElement]
        public string SqlQueryDirectory { get; set; }
        [XmlElement]
        public string DataBase { get; set; }
        [XmlElement]
        public string ParentClass { get; set; }
    }
}
