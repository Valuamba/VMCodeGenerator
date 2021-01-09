using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Commands
{
    public class NameCommand : ICommand<string>
    {
        public readonly string Name;

        public NameCommand(string name)
        {
            Name = name;
        }

        public string Invoke()
        {
            return Name;
        }

        void ICommand.Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
