using CodeGenerator.Utilities;

namespace CodeGenerator.Configurations
{
    public class MessageConfiguration : IMessageConfiguration
    {
        private const string DefaultLanguage = "ru";

        public MessageConfiguration()
        {
            var settingsFile = Startup.GetSettings();
            Language = settingsFile.GetValueOrDefault(".message.language", DefaultLanguage);
        }

        public string Language { get; }
    }
}
