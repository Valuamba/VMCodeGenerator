using CodeGenerator.Commands;
using System.Collections.Generic;

namespace CodeGenerator.CompositeCommands
{
    public interface ICompositeCommand<T> : ICommand
    {
        public List<ICommand<T>> ChildCommands { get; }
        public void Add(ICommand<T> component);
        public void Remove(ICommand<T> component);
    }
}
