using CodeGenerator.Utilities;
using System;
using System.IO;

namespace CodeGenerator.Configurations
{
    public class TemplateConfiguration
    {
        private JsonSettingsFile settingsFile;

        public TemplateConfiguration()
        {
            settingsFile = Startup.GetSettings();
            ClassTemplate = GetPath(nameof(ClassTemplate));
            SqlTemplate = GetPath(nameof(SqlTemplate));
        }

        public readonly string ClassTemplate;
        public readonly string SqlTemplate;

        public string GetPath(string key)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", "Templates", settingsFile.GetValue<string>($".templates.{key.ToLowerFirstChar()}"));
        }
    }
}
