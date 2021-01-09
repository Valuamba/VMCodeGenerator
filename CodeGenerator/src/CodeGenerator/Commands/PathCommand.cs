using System;

namespace CodeGenerator.Commands
{
    public class PathCommand : ICommand<string>
    {
        public readonly string Path;

        public PathCommand(string path)
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
