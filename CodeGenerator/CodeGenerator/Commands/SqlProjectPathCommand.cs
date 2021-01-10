using CodeGenerator.Helper;
using CodeGenerator.Localization;
using System;
using System.Text.RegularExpressions;

namespace CodeGenerator.Commands
{
    public class SqlProjectPathCommand : BaseStringCommand, ICommand<string>
    {
        public readonly string Path;

        public SqlProjectPathCommand(string path)
        {
            Path = path;
        }

        public string Invoke()
        {
            return IsValid(Path);
        }

        public override string IsValid(string input)
        {
            return Regex.IsMatch(input, RegexHelper.DirectoryPathRegex)
                 ? input
                 : throw new ArgumentException(MessageLocalization.GetMessage("message.error.isNotValid.includeDirectoryPath", "sql project"));
        }

        void ICommand.Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
