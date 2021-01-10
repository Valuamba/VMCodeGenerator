using CodeGenerator.Commands;
using CodeGenerator.CompositeCommands;
using CodeGenerator.Configurations;
using CodeGenerator.Document;
using CodeGenerator.Localization;
using CodeGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CodeGenerator
{
    public class Startup
    {
        private static JsonSettingsFile settingsFile;
        public static QueryConfigXmlBuilder QueryConfigBuilder => new QueryConfigXmlBuilder();
        public static TemplateConfiguration TemplateConfiguration => new TemplateConfiguration();
        public static MessageConfiguration MessageConfiguration => new MessageConfiguration();
        public static MessageLocalization MessageLocalization => new MessageLocalization();
        public static ConsoleRetrier ConsoleRetrier => new ConsoleRetrier();
        public static Dictionary<Type, Rule> Rull => new Dictionary<Type, Rule>
        {
            { typeof(CreateCompositeCommand), new Rule(orderedList: new List<Type> { typeof(NameCommand) }, 
                unorderedList: new List<Type> { typeof(ParrentClassCommand), typeof(DataBaseCommand), typeof(ClassProjectPathCommand), typeof(SqlProjectPathCommand), typeof(PathCommand) })},
            { typeof(RemoveCompositeCommand), new Rule(orderedList: new List<Type> { typeof(NameCommand), typeof(NameCommand) }, 
                unorderedList: new List<Type> { typeof(DataBaseCommand), typeof(ClassProjectPathCommand), typeof(SqlProjectPathCommand) }) },
            { typeof(RenameCompositeCommand), new Rule(orderedList: new List<Type> { typeof(NameCommand), typeof(NameCommand) }, 
                unorderedList: new List<Type> { typeof(DataBaseCommand) }) },
            { typeof(GlobalConfigCompositeCommand), null}
        };

        /// <summary>
        /// Provides a <see cref="ISettingsFile"/> with settings.
        /// Value is set in <see cref="ConfigureServices"/>
        /// Otherwise, will use default JSON settings file with name: "settings.{profile}.json".
        /// Default settings will look for the resource file (copied to binaries/Resources/ folder);
        /// If not found, will look for embedded resource in the calling assembly of this method
        /// </summary>
        /// <returns>An instance of settings</returns>
        public static JsonSettingsFile GetSettings()
        {
            if (settingsFile == null)
            {
                var settingsProfile = "settings.json";

                var jsonFile = FileReader.IsResourceFileExist(settingsProfile)
                    ? new JsonSettingsFile(settingsProfile)
                    : new JsonSettingsFile($"Resources.{settingsProfile}", Assembly.GetCallingAssembly());
                return jsonFile;
            }

            return settingsFile;
        }
    }
}