using CodeGenerator.Helper;
using System;

namespace CodeGenerator.CompositeCommands
{
    public class GlobalConfigCompositeCommand : CompositeCommand<string>
    {
        public override void Invoke()
        {
            Console.WriteLine(MessageHelper.WriteSqlDirectory);
            var sqlDirectory = Console.ReadLine();
            Console.WriteLine(MessageHelper.WriteQueryClassDirectory);
            var classDirectory = Console.ReadLine();
            Console.WriteLine(MessageHelper.WriteBaseDataBase);
            var baseDataBase = Console.ReadLine();
        }
    }
}
