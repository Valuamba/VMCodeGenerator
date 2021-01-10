using CodeGenerator.Helper;
using System;
using System.Text.RegularExpressions;

namespace CodeGenerator.Commands
{
    public class ParrentClassCommand : BaseStringCommand, ICommand<string>
    {
        private readonly string BaseParrent;

        public ParrentClassCommand(string baseParrentName)
        {
            BaseParrent = baseParrentName;
        }

        public string Invoke()
        {
            return IsValid(BaseParrent);
        }

        public override string IsValid(string input)
        {
            return Regex.IsMatch(input, RegexHelper.NameValidRegex)
                 ? input
                 : throw new ArgumentException(MessageLocalization.GetMessage("message.error.isNotValid.name", "parrent class"));
        }

        void ICommand.Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
