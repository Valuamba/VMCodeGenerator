namespace CodeGenerator.Generators
{
    public class ClassQueryGenerator : QueryGenerator
    {
        public readonly string ParrentClassName;
        protected override ItemType ItemType => ItemType.Compile;
        protected override string FileExtension => ".cs";

        public ClassQueryGenerator(string queryName, string dataBase, string projectName, string projectQueryDirectory, string solutionPath, string templatePath, string parrentClassName)
            : base(queryName, dataBase, projectName, projectQueryDirectory, solutionPath, templatePath)
        {
            ParrentClassName = parrentClassName;
        }

        protected override TextTemplateEngineHost InitializeHost()
        {
            var host = new ClassHost();
            host.SetFileExtension(FileExtension);

            host.SetValue("NameSpace", GenerateNamespace());
            host.SetValue("ClassName", Name);
            host.SetValue("BaseClass", ParrentClassName);

            host.AddAssemblyLocation(typeof(ClassHost).Assembly.Location);
            host.AddAssemblyLocation(typeof(BaseEngineHost).Assembly.Location);

            host.AddNamespace("CodeGenerator.Generator");

            host.TemplateFileValue = TemplatePath;
            return host;
        }
    }
}
