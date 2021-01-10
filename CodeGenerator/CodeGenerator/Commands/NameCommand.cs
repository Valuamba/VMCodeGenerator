using CodeGenerator.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeGenerator.Commands
{
    public class NameCommand : BaseStringCommand, ICommand<string>
    {
        public readonly string Name;

        public NameCommand(string name)
        {
            Name = name;
        }
        
        public string Invoke()
        {
            return IsValid(Name);
        }

        public override string IsValid(string input)
        {
            return Regex.IsMatch(input, RegexHelper.NameValidRegex)
                 ? input
                 : throw new ArgumentException(MessageLocalization.GetMessage("message.error.isNotValid.name", "file"));
        }

        void ICommand.Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
