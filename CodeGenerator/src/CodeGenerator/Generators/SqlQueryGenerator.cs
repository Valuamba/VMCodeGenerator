namespace CodeGenerator.Generators
{
    public class SqlQueryGenerator : QueryGenerator
    {
        public SqlQueryGenerator(string queryName, string dataBase, string projectName, string projectQueryDirectory, string solutionPath, string templatePath)
            : base(queryName, dataBase, projectName, projectQueryDirectory, solutionPath, templatePath)
        {
        }

        protected override ItemType ItemType => ItemType.EmbeddedResource;

        protected override string FileExtension => ".sql";

        protected override TextTemplateEngineHost InitializeHost()
        {
            var host = new SqlHost();
            host.SetFileExtension(FileExtension);
            host.TemplateFileValue = TemplatePath;
            return host;
        }
    }
}
