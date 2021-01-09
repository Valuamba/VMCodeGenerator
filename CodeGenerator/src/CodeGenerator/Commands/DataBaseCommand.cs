using System;

namespace CodeGenerator.Commands
{
    public class DataBaseCommand : ICommand<string>
    {
        public readonly string DataBaseName;

        public DataBaseCommand(string dataBase)
        {
            DataBaseName = dataBase;
        }

        public string Invoke()
        {
            return DataBaseName;
        }

        void ICommand.Invoke()
        {
            throw new NotImplementedException();
        }
    }
}
