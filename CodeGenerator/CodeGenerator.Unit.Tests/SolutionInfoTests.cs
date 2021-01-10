using System.Collections.Generic;
using System.IO;
using CodeGenerator.Commands;
using CodeGenerator.Generators;
using CodeGenerator.Utilities;
using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;

namespace CodeGenerator.Test
{
    [TestClass]
    public class SolutionInfoTests
    {
        private Project CurrentProject => ProjectCollection.GlobalProjectCollection.GetLoadedProjects(ProjectPath).Single();
        private SolutionInfo SolutionInfo;
        private FileInfo fileInfo;

        private const string QueryName = "SelectAllStocks";
        private const string DataBase = "DefaultBase";
        private const string ProjectName = "Project1";
        private const string IncludeQueryDirectory = @"TestData\Query\Class";
        private const string ParrentClassName = "BaseSelect";
        private const string IncludeDataBasePath = "Deals";
        private const string ClassFileExtension = ".cs";
        private const string Content = "CONTENT";

        public string IncludeDirectoryPath => Path.Combine(IncludeQueryDirectory, DataBase, IncludeDataBasePath);
        public string IncludeFilePath => Path.Combine(IncludeDirectoryPath, QueryName + ClassFileExtension);
        private string AssemblyPath => Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);
        private string SolutionPath => Path.Combine(AssemblyPath, @"Resources\TestProject");
        private string ProjectPath => Path.Combine(SolutionPath, ProjectName, ProjectName + ".csproj");
        private string TemplatePath => Path.Combine(AssemblyPath, @"Resources\Templates\ClassTemplate.tt");
        private string FullPath => Path.Combine(AssemblyPath, @"Resources\TestProject", ProjectName, IncludeDirectoryPath, QueryName + ClassFileExtension);

        [TestInitialize]
        public void TestInit()
        {
            GenerateDirectoryForTest();
            SolutionInfo = SolutionInfo.GetInstance(SolutionPath);
        }

        [TestCleanup]
        public void CleanUp()
        {
            CurrentProject.RemoveItems(CurrentProject.GetItems("Compile"));
            CurrentProject.RemoveItems(CurrentProject.GetItems("Folder"));
            CurrentProject.RemoveItems(CurrentProject.GetItems("EmbeddedResource"));
            CurrentProject.Save();
            var fileDirectoryPath = Path.GetDirectoryName(FullPath);
            if (Directory.Exists(fileDirectoryPath))
            {
                Directory.Delete(fileDirectoryPath, true);
            }
            ProjectCollection.GlobalProjectCollection.UnloadAllProjects();
        }

        public void GenerateDirectoryForTest()
        {
            var fileDirectoryPath = Path.GetDirectoryName(FullPath);
            Directory.CreateDirectory(fileDirectoryPath);
        }

        public void Should_BeSolutionDirectory()
        {
            Assert.IsTrue(SolutionInfo.IsSolutionDirectory(SolutionPath, out fileInfo));
        }

        [TestMethod]
        public void Should_BeNotSolutionDirectory()
        {
            Assert.IsFalse(SolutionInfo.IsSolutionDirectory("C:\\", out fileInfo));
        }

        [TestMethod]
        public void Should_FindProjects_WhenUseProjectsProperty()
        {
            Assert.AreEqual(1, SolutionInfo.Projects.Count);
        }

        [TestMethod]
        public void Should_BePossibleTo_AddCompileItem()
        {
            SolutionInfo.AddItem(ProjectName, IncludeDirectoryPath, QueryName + ClassFileExtension, Content, ItemType.Compile);
            var item = CurrentProject.GetItems(ItemType.Compile.ToString());

            Assert.IsTrue(File.Exists(FullPath));
            Assert.IsTrue(item.Count == 1);
            Assert.AreEqual(IncludeFilePath, item.Single().EvaluatedInclude);
        }

        [TestMethod]
        public void Should_BePossibleTo_AddEmbeddedItem()
        {
            SolutionInfo.AddItem(ProjectName, IncludeDirectoryPath, QueryName + ClassFileExtension, Content, ItemType.EmbeddedResource);
            var item = CurrentProject.GetItems(ItemType.EmbeddedResource.ToString());

            Assert.IsTrue(File.Exists(FullPath));
            Assert.IsTrue(item.Count == 1);
            Assert.AreEqual(IncludeFilePath, item.Single().EvaluatedInclude);
        }

        [TestMethod]
        public void Should_RenameItem()
        {
            var newName = "NewName" + ClassFileExtension;
            SolutionInfo.AddItem(ProjectName, IncludeDirectoryPath, QueryName + ClassFileExtension, Content, ItemType.Compile);
            SolutionInfo.Rename(ProjectName, IncludeDirectoryPath, QueryName + ClassFileExtension, newName);
            var item = CurrentProject.GetItems(ItemType.Compile.ToString());

            Assert.IsTrue(File.Exists(Path.Combine(Path.GetDirectoryName(FullPath), newName)));
            Assert.IsTrue(item.Count == 1);
            Assert.AreEqual(Path.Combine(IncludeDirectoryPath, newName), item.Single().EvaluatedInclude);
        }

        [TestMethod]
        public void Should_DeleteItem()
        {
            SolutionInfo.AddItem(ProjectName, IncludeDirectoryPath, QueryName + ClassFileExtension, Content, ItemType.Compile);

            Assert.IsTrue(File.Exists(FullPath));

            SolutionInfo.Delete(ProjectName, IncludeDirectoryPath, QueryName + ClassFileExtension);
            var item = CurrentProject.GetItems(ItemType.Compile.ToString());

            Assert.IsTrue(!File.Exists(FullPath));
            Assert.IsTrue(item.Count == 0);
        }

        [TestMethod]
        public void Should_RegenerateItemContent()
        {
            var regeneratedContent = "REGENERATED CONTENT";
            SolutionInfo.AddItem(ProjectName, IncludeDirectoryPath, QueryName + ClassFileExtension, Content, ItemType.Compile);
            var item = CurrentProject.GetItems(ItemType.Compile.ToString());

            Assert.IsTrue(File.Exists(FullPath));
            Assert.IsTrue(item.Count == 1);
            Assert.AreEqual(IncludeFilePath, item.Single().EvaluatedInclude);
            Assert.AreEqual(Content, File.ReadAllText(FullPath));

            SolutionInfo.Regenerate(ProjectName, IncludeDirectoryPath, QueryName + ClassFileExtension, regeneratedContent);
            Assert.AreEqual(regeneratedContent, File.ReadAllText(FullPath));
        }
    }
}
