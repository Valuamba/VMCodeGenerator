using CodeGenerator.Commands;
using System.Collections.Generic;

namespace CodeGenerator.CompositeCommands
{
    public interface ICompositeCommand : ICommand
    {
        public List<ICommand> Components { get; }
        public void Add(ICommand component);
        public void Remove(ICommand component);
    }
}
