using CodeGenerator.Localization;

namespace CodeGenerator.Commands
{
    public abstract class BaseStringCommand
    {
        protected readonly MessageLocalization MessageLocalization = Startup.MessageLocalization;

        public abstract string IsValid(string input);
    }
}
