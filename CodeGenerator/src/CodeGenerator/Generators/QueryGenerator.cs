using CodeGenerator.Exceptions;
using CodeGenerator.Utilities.ExceptionUtilities;
using Microsoft.VisualStudio.TextTemplating;
using System.IO;

namespace CodeGenerator.Generators
{
    public abstract class QueryGenerator
    {
        protected abstract ItemType ItemType { get; }
        protected abstract string FileExtension { get; }
        protected TextTemplateEngineHost EngineHost;
        public readonly Engine Engine;
        protected SolutionInfo Solution;

        public readonly string Name;
        public readonly string ProjectName;
        public readonly string ProjectQueryDirectory;
        public readonly string DataBase;
        public readonly string TemplatePath;

        public QueryGenerator(string queryName, string dataBase, string projectName, string projectQueryDirectory, string solutionPath, string templatePath)
        {
            Engine = new Engine(); 
            Solution = new SolutionInfo(solutionPath);
            Name = queryName;
            DataBase = dataBase;
            ProjectQueryDirectory = projectQueryDirectory;
            TemplatePath = templatePath;
            ProjectName = projectName;
            EngineHost = InitializeHost();
        }

        public string GenerateContent()
        {
            return Engine.ProcessTemplate(File.ReadAllText(TemplatePath), EngineHost);
        }

        public string GeneratePathForProjectFile()
        {
            return Path.Combine(ProjectQueryDirectory, DataBase, Name + FileExtension);
        }

        public string GenerateNamespace()
        {
            return $"{ProjectName}.{Path.GetDirectoryName(GeneratePathForProjectFile()).Replace("\\", ".").Replace("/", ".")}";
        }

        protected abstract TextTemplateEngineHost InitializeHost();

        public void Generate()
        {
            var pathForProjectFile = GeneratePathForProjectFile();
            ThrowExceptionUtility.ThrowException<SolutionException>(() => Solution[ProjectName, pathForProjectFile].Count != 0,
                message: $"File at path [{pathForProjectFile}] already exists in [{ProjectName}] project file.");
            Solution.AddItem(ProjectName, pathForProjectFile, GenerateContent(), ItemType);
        }

        public void Regenerate()
        {
            var pathForProjectFile = GeneratePathForProjectFile();
            ThrowExceptionUtility.ThrowException<SolutionException>(() => Solution[ProjectName, pathForProjectFile].Count == 0,
                message: $"File at path [{pathForProjectFile}] does not exist in [{ProjectName}] project file.");
            Solution.Regenerate(ProjectName, pathForProjectFile, GenerateContent());
        }

        public void Rename(string newName)
        {
            var pathForProjectFile = GeneratePathForProjectFile();
            ThrowExceptionUtility.ThrowException<SolutionException>(() => Solution[ProjectName, pathForProjectFile].Count == 0,
                message: $"File at path [{pathForProjectFile}] does not exist in [{ProjectName}] project file.");
            Solution.Rename(ProjectName, pathForProjectFile, newName);
        }

        public void Delete()
        {
            var pathForProjectFile = GeneratePathForProjectFile();
            ThrowExceptionUtility.ThrowException<SolutionException>(() => Solution[ProjectName, pathForProjectFile].Count == 0,
                message: $"File at path [{pathForProjectFile}] does not exist in [{ProjectName}] project file.");
            Solution.Delete(ProjectName, pathForProjectFile);
        }
    }
}
