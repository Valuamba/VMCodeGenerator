using System;

namespace CodeGenerator.Commands
{
    public class SqlProjectPathCommand : ICommand<string>
    {
        public readonly string Path;

        public SqlProjectPathCommand(string path)
        {
            Path = path;
        }

        public string Invoke()
        {
            return Path;
        }

        void ICommand.Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
