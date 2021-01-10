using CodeGenerator.Commands;
using CodeGenerator.Configurations;
using CodeGenerator.Generators;
using CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeGenerator.CompositeCommands
{
    public class RenameCompositeCommand : CompositeCommand<string>
    {
        private TemplateConfiguration TemplateConfiguration => Startup.TemplateConfiguration;
        private Query QueryConfig => Startup.QueryConfigBuilder.GetConfig().Query;

        public override void Invoke()
        {
            var queryName = GetInvokeResult(0);
            var newName = GetInvokeResult(1);
            var dataBase = GetInvokeResult(2) ?? QueryConfig.DataBase;
            var classProjectQueryDirectory = GetInvokeResult(3) ?? QueryConfig.ClassQueryDirectory;
            var sqlProjectQueryDirectory = GetInvokeResult(4) ?? QueryConfig.SqlQueryDirectory;
            var includeDataBasePath = GetInvokeResult(5);

            var classPojectName = QueryConfig.ClassQueryProject;
            var sqlProjectName = QueryConfig.SqlQueryProject;

            var solutionDirectory = QueryConfig.SolutionDirectory;

            var classTemplatePath = TemplateConfiguration.ClassTemplate;
            var sqlTemplatePath = TemplateConfiguration.SqlTemplate;

            new ClassQueryGenerator(queryName, dataBase, includeDataBasePath, classPojectName, classProjectQueryDirectory, solutionDirectory, classTemplatePath).Rename(newName);
            new SqlQueryGenerator(queryName, dataBase, sqlProjectName, sqlProjectQueryDirectory, solutionDirectory, sqlTemplatePath).Rename(newName);
        }
    }
}
