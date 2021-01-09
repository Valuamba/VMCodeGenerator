using System.IO;

namespace CodeGenerator.Generators
{
    public class ClassQueryGenerator : QueryGenerator
    {
        public readonly string IncludeDataBasePath;
        public readonly string ParrentClassName;
        protected override ItemType ItemType => ItemType.Compile;
        protected override string FileExtension => ".cs";

        public ClassQueryGenerator(string queryName, string dataBase, string includeDataBasePath, string projectName, string includeQueryDirectory, string solutionPath, string templatePath, string parrentClassName)
            : base(queryName, dataBase, projectName, includeQueryDirectory, solutionPath, templatePath)
        {
            ParrentClassName = parrentClassName;
            IncludeDataBasePath = GetNullablePath(includeDataBasePath);
        }

        public override string IncludeDirectoryPath => Path.Combine(IncludeQueryDirectory, DataBase, IncludeDataBasePath);

        public string Namespace => $"{ProjectName}.{IncludeDirectoryPath.Replace("\\", ".").Replace("/", ".")}";

        protected override TextTemplateEngineHost InitializeHost()
        {
            var host = new ClassHost();
            host.SetFileExtension(FileExtension);

            host.SetValue("NameSpace", Namespace);
            host.SetValue("ClassName", Name);
            host.SetValue("BaseClass", ParrentClassName);

            host.AddAssemblyLocation(typeof(ClassHost).Assembly.Location);
            host.AddAssemblyLocation(typeof(BaseEngineHost).Assembly.Location);

            host.AddNamespace("CodeGenerator.Generator");

            host.TemplateFileValue = TemplatePath;
            return host;
        }

        private string GetNullablePath(string path)
        {
            return path ?? string.Empty;
        }
    }
}
