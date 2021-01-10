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

        public void SetQueryConfig(Query queryConfig)
        {
            var config = Deserialize<Config>();
            config.Query = queryConfig;
            Serialize(config);
        }

        public Config GetConfig()
        {
            return Deserialize<Config>();
        }
    }
}
