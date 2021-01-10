using CodeGenerator.Commands;
using CodeGenerator.Configurations;
using CodeGenerator.Generators;
using CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.CompositeCommands
{
    public class RemoveCompositeCommand : CompositeCommand<string>
    {
        private TemplateConfiguration TemplateConfiguration => Startup.TemplateConfiguration;
        private Query QueryConfig => Startup.QueryConfigBuilder.GetConfig().Query;

        public override void Invoke()
        {
            var queryName = GetInvokeResult(0);
            var dataBase = GetInvokeResult(1) ?? QueryConfig.DataBase;
            var classProjectQueryDirectory = GetInvokeResult(2) ?? QueryConfig.ClassQueryDirectory;
            var sqlProjectQueryDirectory = GetInvokeResult(3) ?? QueryConfig.SqlQueryDirectory;
            var includeDataBasePath = GetInvokeResult(4);

            var classPojectName = QueryConfig.ClassQueryProject;
            var sqlProjectName = QueryConfig.SqlQueryProject;

            var solutionDirectory = QueryConfig.SolutionDirectory;

            var classTemplatePath = TemplateConfiguration.ClassTemplate;
            var sqlTemplatePath = TemplateConfiguration.SqlTemplate;

            new ClassQueryGenerator(queryName, dataBase, includeDataBasePath, classPojectName, classProjectQueryDirectory, solutionDirectory, classTemplatePath).Delete();
            new SqlQueryGenerator(queryName, dataBase, sqlProjectName, sqlProjectQueryDirectory, solutionDirectory, sqlTemplatePath).Delete();
        }
    }
}
