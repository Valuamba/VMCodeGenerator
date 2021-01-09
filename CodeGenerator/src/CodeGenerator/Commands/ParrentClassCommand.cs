using System;

namespace CodeGenerator.Commands
{
    public class ParrentClassCommand : ICommand<string>
    {
        public ParrentClassCommand(string baseParrentName)
        {
        }

        public string Invoke()
        {
            return null;
        }

        void ICommand.Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
