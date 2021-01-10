using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CodeGenerator.Commands;
using CodeGenerator.Generators;
using CodeGenerator.Utilities;
using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeGenerator.Test
{
    [TestClass]
    public class QueryGeneratorTest
    {
        private const string QueryName = "SelectAllStocks";
        private const string DataBase = "DefaultBase";
        private const string ProjectName = "Project1";
        private const string IncludeQueryDirectory = @"TestData\Query\Class";
        private const string ParrentClassName = "BaseSelect";
        private const string IncludeDataBasePath = "Deals";

        private string AssemblyPath => Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);

        private string SolutionPath => Path.Combine(AssemblyPath, @"Resources\TestProject");
        private string TemplatePath => Path.Combine(AssemblyPath, @"Resources\Templates\ClassTemplate.tt");

        private ClassQueryGenerator Generator;

        [TestInitialize]
        public void TestInit()
        {
            Generator = new ClassQueryGenerator(QueryName, DataBase, IncludeDataBasePath, ProjectName, IncludeQueryDirectory, SolutionPath, TemplatePath, ParrentClassName);
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            ProjectCollection.GlobalProjectCollection.UnloadAllProjects();
        }

        [TestMethod]
        public void Should_GetCorrectNamespace()
        {
            var nameSpace = $"{ProjectName}.{IncludeQueryDirectory.Replace("\\", ".").Replace("/", ".")}.{DataBase}.{IncludeDataBasePath}";
            Assert.AreEqual(nameSpace, Generator.Namespace);
        }

        [TestMethod]
        public void Should_GetCorrectPathForProjectFile()
        {
            var includeDirectoryPath = Path.Combine(IncludeQueryDirectory, DataBase, IncludeDataBasePath);
            Assert.AreEqual(includeDirectoryPath, Generator.IncludeDirectoryPath);
        }
    }
}
