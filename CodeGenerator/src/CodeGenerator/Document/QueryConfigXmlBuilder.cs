using System;
using CodeGenerator.Document.Xml;
using CodeGenerator.Model;

namespace CodeGenerator.Document
{
    public class QueryConfigXmlBuilder : XmlBuilder
    {
        public QueryConfigXmlBuilder()
        {
            Config config = new Config();
            InitializeFile(config);
        }

        protected override XmlBuilderCommonSettings BuilderSettings => new QueryConfigCommonSettings();

        public void SetPath(string sqlDirectoryPath, string sqlClassDirectoryPath)
        {
            //var config = Deserialize<Config>();
            //config.ClassDirectoryPath = sqlClassDirectoryPath;
            //config.SqlDirectoryPath = sqlDirectoryPath;
        }

        public Config GetConfig()
        {
            return Deserialize<Config>();
        }
    }
}
