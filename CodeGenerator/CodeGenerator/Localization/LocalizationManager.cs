using CodeGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Localization
{
    public class LocalizationManager
    {
        private const string LangResource = "Resources.Localization.{0}.json";
        private readonly JsonSettingsFile localizationFile;
        private readonly JsonSettingsFile coreLocalizationFile;

        public LocalizationManager(string language, Assembly assembly = null)
        {
            localizationFile = GetLocalizationFile(language, assembly ?? Assembly.GetExecutingAssembly());
            coreLocalizationFile = GetLocalizationFile(language, Assembly.GetExecutingAssembly());
        }

        private static JsonSettingsFile GetLocalizationFile(string language, Assembly assembly)
        {
            var embeddedResourceName = string.Format(LangResource, language.ToLower());
            var assemblyToUse = assembly.GetManifestResourceNames().Any(name => name.Contains(embeddedResourceName))
                ? assembly
                : Assembly.GetExecutingAssembly();
            return new JsonSettingsFile(embeddedResourceName, assemblyToUse);
        }

        public string GetMessage(string messageKey, params object[] args)
        {
            var jsonKey = $"$['{messageKey}']";
            var localizationFileToUse = localizationFile.IsValuePresent(jsonKey) ? localizationFile : coreLocalizationFile;
            if (localizationFileToUse.IsValuePresent(jsonKey))
            {
                return string.Format(localizationFileToUse.GetValue<string>(jsonKey), args);
            }

            return messageKey;
        }
    }
}
