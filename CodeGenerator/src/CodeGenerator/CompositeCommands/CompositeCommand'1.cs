using CodeGenerator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeGenerator.CompositeCommands
{
    public abstract class CompositeCommand<T> : ICompositeCommand<T>
    {
        public List<ICommand<T>> ChildCommands { get; }

        public CompositeCommand()
        {
            ChildCommands = new List<ICommand<T>>();
        }

        public virtual T GetInvokeResult(int index)
        {
            return ChildCommands.ElementAt(index).Invoke();
        }

        public virtual void Add(ICommand<T> command)
        {
            ChildCommands.Add(command);
        }

        public virtual void Remove(ICommand<T> command)
        {
            ChildCommands.Remove(command);
        }

        public abstract void Invoke();
    }
}
