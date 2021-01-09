using CodeGenerator.Exceptions;
using CodeGenerator.Utilities.ExceptionUtilities;
using Microsoft.VisualStudio.TextTemplating;
using System;
using System.CodeDom.Compiler;
using System.IO;

namespace CodeGenerator.Generators
{
    public abstract class QueryGenerator
    {
        protected abstract ItemType ItemType { get; }
        protected abstract string FileExtension { get; }
        private TextTemplateEngineHost textTemplateEngineHost;
        protected TextTemplateEngineHost EngineHost
        {
            get
            {
                if(textTemplateEngineHost == null)
                {
                    textTemplateEngineHost = InitializeHost();
                }
                return textTemplateEngineHost;
            }
        }
        public readonly Engine Engine;
        protected SolutionInfo Solution;

        public readonly string Name;
        public readonly string ProjectName;
        public readonly string IncludeQueryDirectory;
        public readonly string DataBase;
        public readonly string TemplatePath;

        public QueryGenerator(string queryName, string dataBase, string projectName, string includeQueryDirectory, string solutionPath, string templatePath)
        {
            Engine = new Engine();
            Solution = SolutionInfo.GetInstance(solutionPath);
            Name = queryName;
            DataBase = dataBase;
            IncludeQueryDirectory = includeQueryDirectory;
            TemplatePath = templatePath;
            ProjectName = projectName;
        }
        protected abstract TextTemplateEngineHost InitializeHost();

        public virtual string IncludeDirectoryPath => Path.Combine(IncludeQueryDirectory, DataBase);

        public virtual string GenerateContent()
        {
            var content = Engine.ProcessTemplate(File.ReadAllText(TemplatePath), EngineHost);
            foreach (CompilerError error in EngineHost.Errors)
            {
                Console.WriteLine(error.ErrorText);
            }
            return content;
        }

        public void Generate()
        {
            Solution.AddItem(ProjectName, IncludeDirectoryPath, Name + FileExtension, GenerateContent(), ItemType);
        }

        //Chquery create -n SelectAllSocks –parrent SelectBaseDeal -db AtonBase –cpath Query\Class\ --spath TestData\Query
        public void Regenerate()
        {
            Solution.Regenerate(ProjectName, IncludeDirectoryPath, Name + FileExtension, GenerateContent());
        }

        public void Rename(string newName)
        {
            Solution.Rename(ProjectName, IncludeDirectoryPath, Name + FileExtension, newName);
        }

        public void Delete()
        {
            Solution.Delete(ProjectName, IncludeDirectoryPath, Name + FileExtension);
        }
    }
}
