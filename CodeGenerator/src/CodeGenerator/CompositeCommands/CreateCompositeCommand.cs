using CodeGenerator.Configurations;
using CodeGenerator.Generators;
using CodeGenerator.Document;

namespace CodeGenerator.CompositeCommands
{
    public class CreateCompositeCommand : CompositeCommand<string>
    {
        public override void Invoke()
        {
            TemplateConfiguration template = new TemplateConfiguration();
            var queryConfig = new QueryConfigXmlBuilder().GetConfig().Query;


            var queryName = GetInvokeResult(0);
            var parrentClassName = GetInvokeResult(1) ?? queryConfig.ParentClass;
            var dataBase = GetInvokeResult(2) ?? queryConfig.DataBase;
            var classProjectQueryDirectory = GetInvokeResult(3) ?? queryConfig.ClassQueryDirectory;

            var classPojectName = queryConfig.ClassQueryProject;
            var sqlProjectName = queryConfig.SqlQueryProject;

            var solutionDirectory = queryConfig.SolutionDirectory;
            var sqlProjectQueryDirectory = queryConfig.SqlQueryDirectory;

            var classTemplatePath = template.ClassTemplatePath;
            var sqlTemplatePath = template.SqlTemplatePath;

           new ClassQueryGenerator(queryName, dataBase, classPojectName, classProjectQueryDirectory, solutionDirectory, classTemplatePath, parrentClassName).Generate();
           new SqlQueryGenerator(queryName, dataBase, sqlProjectName, sqlProjectQueryDirectory, solutionDirectory, sqlTemplatePath).Generate();
        }
    }
}
