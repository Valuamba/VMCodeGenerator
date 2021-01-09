using System;

namespace CodeGenerator.Commands
{
    public class ClassProjectPathCommand : ICommand<string>
    {
        public readonly string Path;

        public ClassProjectPathCommand(string path)
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
