using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.Commands
{
    public interface ICommand
    {
        public void Invoke();
    }

    public interface ICommand<T> : ICommand
    {
        public new T Invoke();
    }
}
