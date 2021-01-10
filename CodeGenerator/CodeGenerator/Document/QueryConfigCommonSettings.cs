using System;
using System.IO;
using CodeGenerator.Document.Xml;
using CodeGenerator.Utilities;

namespace CodeGenerator.Document
{
    public class QueryConfigCommonSettings : XmlBuilderCommonSettings
    {
        private readonly JsonSettingsFile settingsFile;

        public QueryConfigCommonSettings()
        {
            settingsFile = Startup.GetSettings();
        }

        public override string FilePath => GetPath("Name");

        public string GetPath(string key)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", "Config", settingsFile.GetValue<string>($".config.{key.ToLowerFirstChar()}"));
        }
    }
}
