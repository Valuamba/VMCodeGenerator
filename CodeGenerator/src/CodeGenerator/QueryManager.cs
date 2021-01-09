using System;
using System.Linq;
using CodeGenerator.Utilities;
using CodeGenerator.CompositeCommands;

namespace CodeGenerator
{
    public class QueryManager
    {
        public void Execute(string[] args)
        {
            var compositeCommand = ArgumentUtility.Parse(args.ToList());
            Validate(compositeCommand, Startup.Rull[compositeCommand.GetType()]);
            compositeCommand.Invoke();
        }

        public void Validate(ICompositeCommand<string> command, Rule rule)
        {
            if (rule != null)
            {
                int i = 0;
                if (rule.OrderedList != null)
                {
                    for (i = 0; i < rule.OrderedList.Count; i++)
                    {
                        if (rule.OrderedList[i] != command.ChildCommands[i].GetType())
                        {
                            throw new ArgumentException("Incorrect object.");
                        }
                    }
                }
                if (rule.UnorderedList != null)
                {
                    int startIndex = i;
                    for(int j = startIndex; j < command.ChildCommands.Count; j++)
                    {
                        if(!rule.UnorderedList.Any(t => t == command.ChildCommands[j].GetType()))
                        {
                            throw new ArgumentException("Incorrect list commands");
                        }
                    }

                    for (int j = 0; j < rule.UnorderedList.Count; j++)
                    {
                        var index = command.ChildCommands.FindIndex(x => x != null && x.GetType() == rule.UnorderedList[j]);
                        if (index == -1)
                        {
                            command.ChildCommands.Insert(j + startIndex, null);
                        }
                        else if (command.ChildCommands[j + startIndex].GetType() != rule.UnorderedList[j])
                        {
                            var temp = command.ChildCommands[j + startIndex];
                            command.ChildCommands[j + startIndex] = command.ChildCommands[index];
                            command.ChildCommands[index] = temp;
                        }
                    }
                }
            }
        }
    }
}
