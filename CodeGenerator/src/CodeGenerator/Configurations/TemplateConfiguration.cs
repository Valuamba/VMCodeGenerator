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
            ClassTemplatePath = GetPath(nameof(ClassTemplatePath));
            SqlTemplatePath = GetPath(nameof(SqlTemplatePath));
        }

        public readonly string ClassTemplatePath;
        public readonly string SqlTemplatePath;

        public string GetPath(string key)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", "Template", settingsFile.GetValue<string>($".template.{key.ToLowerFirstChar()}"));
        }
    }
}
