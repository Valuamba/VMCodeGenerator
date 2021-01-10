using CodeGenerator.Configurations;
using CodeGenerator.Generators;
using CodeGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.CompositeCommands
{
    public class RegenerateCompositeCommand : CompositeCommand<string>
    {
        private TemplateConfiguration TemplateConfiguration => Startup.TemplateConfiguration;
        private Query QueryConfig => Startup.QueryConfigBuilder.GetConfig().Query;

        public override void Invoke()
        {
            var queryName = GetInvokeResult(0);
            var parrentClassName = GetInvokeResult(1) ?? QueryConfig.ParentClass;
            var dataBase = GetInvokeResult(2) ?? QueryConfig.DataBase;
            var classProjectQueryDirectory = GetInvokeResult(3) ?? QueryConfig.ClassQueryDirectory;
            var sqlProjectQueryDirectory = GetInvokeResult(4) ?? QueryConfig.SqlQueryDirectory;
            var includeDataBasePath = GetInvokeResult(5);

            var classPojectName = QueryConfig.ClassQueryProject;
            var sqlProjectName = QueryConfig.SqlQueryProject;

            var solutionDirectory = QueryConfig.SolutionDirectory;

            var classTemplatePath = TemplateConfiguration.ClassTemplate;
            var sqlTemplatePath = TemplateConfiguration.SqlTemplate;

            new ClassQueryGenerator(queryName, dataBase, includeDataBasePath, classPojectName, classProjectQueryDirectory, solutionDirectory, classTemplatePath, parrentClassName).Regenerate();
            new SqlQueryGenerator(queryName, dataBase, sqlProjectName, sqlProjectQueryDirectory, solutionDirectory, sqlTemplatePath).Regenerate();
        }
    }
}
