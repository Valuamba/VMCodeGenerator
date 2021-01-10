using CodeGenerator.Helper;
using CodeGenerator.Localization;
using CodeGenerator.Model;
using CodeGenerator.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CodeGenerator.CompositeCommands
{
    public class GlobalConfigCompositeCommand : CompositeCommand<string>
    {
        private readonly MessageLocalization MessageLocalization = Startup.MessageLocalization;
        private readonly Query QueryConfig = Startup.QueryConfigBuilder.GetConfig().Query ?? new Query();

        public Dictionary<string, string> ConsoleIteractionMap => new Dictionary<string, string>
        {
            {"message.write.directory.solution", nameof(QueryConfig.SolutionDirectory) },
            {"message.write.class.project", nameof(QueryConfig.ClassQueryProject) },
            {"message.write.sql.project",nameof(QueryConfig.SqlQueryProject) },
            {"message.write.class.includeDirectory", nameof(QueryConfig.ClassQueryDirectory) },
            {"message.write.sql.includeDirectory", nameof(QueryConfig.SqlQueryDirectory) },
            {"message.write.database", nameof(QueryConfig.DataBase) },
            {"message.write.parrentClass", nameof(QueryConfig.ParentClass) },
        };

        public override void Invoke()
        {
            foreach(var iteraction in ConsoleIteractionMap)
            {
                Startup.ConsoleRetrier.DoWithRetry(() =>
                {
                    Console.WriteLine(MessageLocalization.GetMessage(iteraction.Key));
                    var output = Console.ReadLine();
                    if(!output.IsNullOrEmpty())
                    {
                        SetPropertyValue(iteraction.Value, output);
                    }
                    return output.IsNullOrEmpty() && GetProperty(iteraction.Value).IsNullOrEmpty() ? false : true;
                }, MessageLocalization.GetMessage("message.error.outputValue.isNullOrEmpty"));
            }
            Startup.QueryConfigBuilder.SetQueryConfig(QueryConfig);
        }

        private string GetProperty(string name)
        {
            return QueryConfig.GetType().GetProperty(name).GetValue(QueryConfig) as string;
        }

        private void SetPropertyValue(string name, string value)
        {
            QueryConfig.GetType().GetProperty(name).SetValue(QueryConfig, value);
        }
    }
}
