using CodeGenerator.Helper;
using System;
using System.Text.RegularExpressions;

namespace CodeGenerator.Commands
{
    public class DataBaseCommand : BaseStringCommand, ICommand<string>
    {
        public readonly string DataBaseName;

        public DataBaseCommand(string dataBase)
        {
            DataBaseName = dataBase;
        }

        public string Invoke()
        {
            return IsValid(DataBaseName);
        }

        public override string IsValid(string input)
        {
            return Regex.IsMatch(input, RegexHelper.NameValidRegex)
                 ? input
                 : throw new ArgumentException(MessageLocalization.GetMessage("message.error.isNotValid.name", "database"));
        }

        void ICommand.Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
