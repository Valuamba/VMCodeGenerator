using CodeGenerator.Commands;
using CodeGenerator.CompositeCommands;
using CodeGenerator.Localization;
using System;
using System.Collections.Generic;

namespace CodeGenerator.Utilities
{
    public class ArgumentUtility
    {
        private readonly MessageLocalization MessageLocalization = Startup.MessageLocalization;

        public ICompositeCommand<string> Parse(List<string> args, ICompositeCommand<string> compositeCommand = null, int index = 0)
        {
            if (compositeCommand == null)
            {
                compositeCommand = GetCompositeCommand(args[0]);
                index++;
            }
            if (args.Count > 1)
            {
                compositeCommand.Add(GetCommand(args, ref index));
                index++;
                if (index < args.Count)
                {
                    Parse(args, compositeCommand, index);
                }
            }

            return compositeCommand;
        }

        private ICompositeCommand<string> GetCompositeCommand(string arg)
        {
            switch (arg)
            {
                case "create": return (CreateCompositeCommand) Activator.CreateInstance(typeof(CreateCompositeCommand));
                case "remove": return (RemoveCompositeCommand) Activator.CreateInstance(typeof(RemoveCompositeCommand));
                case "rename": return (RenameCompositeCommand) Activator.CreateInstance(typeof(RenameCompositeCommand));
                case "global": return (GlobalConfigCompositeCommand) Activator.CreateInstance(typeof(GlobalConfigCompositeCommand));
                case "regenerate": return (RegenerateCompositeCommand)Activator.CreateInstance(typeof(RegenerateCompositeCommand));
                default: throw new ArgumentException(MessageLocalization.GetMessage("argument.incorrect.compositeCommand", arg));
            }
        }

        private ICommand<string> GetCommand(List<string> args, ref int index)
        {
            switch (args[index])
            {
                case "-n":
                case "--name":
                    return (NameCommand)Activator.CreateInstance(typeof(NameCommand), args[++index]);
                case "-db":
                case "--database":
                    return (DataBaseCommand)Activator.CreateInstance(typeof(DataBaseCommand), args[++index]);                  
                case "--path":
                    return (PathCommand)Activator.CreateInstance(typeof(PathCommand), args[++index]);
                case "-b":
                case "--base":
                    return (ParrentClassCommand)Activator.CreateInstance(typeof(ParrentClassCommand), args[++index]);
                case "--cpath":
                    return (ClassProjectPathCommand)Activator.CreateInstance(typeof(ClassProjectPathCommand), args[++index]);
                case "--spath":
                    return (SqlProjectPathCommand)Activator.CreateInstance(typeof(SqlProjectPathCommand), args[++index]);

                default: throw new ArgumentException(MessageLocalization.GetMessage("argument.incorrect.tag", args[index]));
            };
        }
    }
}
