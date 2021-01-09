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
        private const string ProjectFileName = "Project1";
        private const string IncludePath = @"TestData\Query\SelectAllDeals.cs";
        private const string Content = "CONTENT";

        private readonly string SolutionPath = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), @"Resources\TestProject");

        private string FileDirectoryPath => Path.GetDirectoryName(FileFullPath);
        private string FileFullPath => Path.Combine(SolutionPath, ProjectFileName, IncludePath);
        private string ProjectPath => Path.Combine(SolutionPath, ProjectFileName, ProjectFileName + ".csproj");
        private Project CurrentProject => ProjectCollection.GlobalProjectCollection.GetLoadedProjects(ProjectPath).Single();
        private SolutionInfo SolutionInfo;
        private FileInfo fileInfo;

        [TestInitialize]
        public void TestInit()
        {
            GenerateDirectoryForTest();
            SolutionInfo = new SolutionInfo(SolutionPath);
        }

        [TestCleanup]
        public void CleanUp()
        {
            CurrentProject.RemoveItems(CurrentProject.GetItems("Compile"));
            CurrentProject.RemoveItems(CurrentProject.GetItems("Folder"));
            CurrentProject.RemoveItems(CurrentProject.GetItems("EmbeddedResource"));
            CurrentProject.Save();
            if (Directory.Exists(FileDirectoryPath))
            {
                Directory.Delete(FileDirectoryPath, true);
            }
            ProjectCollection.GlobalProjectCollection.UnloadAllProjects();
        }

        public void GenerateDirectoryForTest()
        {
            Directory.CreateDirectory(FileDirectoryPath);
        }

        public void Should_BeSolutionDirectory()
        {
            Assert.IsTrue(SolutionInfo.IsSolutionDirectory(SolutionPath, out fileInfo));
        }

        [TestMethod]
        public void Should_BeNotSolutionDirectory()
        {
            Assert.IsFalse(SolutionInfo.IsSolutionDirectory(FileDirectoryPath, out fileInfo));
        }

        [TestMethod]
        public void Should_FindProjects_WhenUseProjectsProperty()
        {
            Assert.AreEqual(1, SolutionInfo.Projects.Count);
        }

        [TestMethod]
        public void Should_BePossibleTo_AddCompileItem()
        {
            SolutionInfo.AddItem(ProjectFileName, IncludePath, Content, ItemType.Compile);
            var item = CurrentProject.GetItems(ItemType.Compile.ToString());

            Assert.IsTrue(File.Exists(FileFullPath));
            Assert.IsTrue(item.Count == 1);
            Assert.AreEqual(IncludePath, item.Single().EvaluatedInclude);
        }

        [TestMethod]
        public void Should_BePossibleTo_AddEmbeddedItem()
        {
            SolutionInfo.AddItem(ProjectFileName, IncludePath, Content, ItemType.EmbeddedResource);
            var item = CurrentProject.GetItems(ItemType.EmbeddedResource.ToString());

            Assert.IsTrue(File.Exists(FileFullPath));
            Assert.IsTrue(item.Count == 1);
            Assert.AreEqual(IncludePath, item.Single().EvaluatedInclude);
        }

        [TestMethod]
        public void Should_RenameItem()
        {
            var newName = "NewName.cs";
            SolutionInfo.AddItem(ProjectFileName, IncludePath, Content, ItemType.Compile);
            SolutionInfo.Rename(ProjectFileName, IncludePath, newName);
            var item = CurrentProject.GetItems(ItemType.Compile.ToString());

            Assert.IsTrue(File.Exists(Path.Combine(Path.GetDirectoryName(FileFullPath), newName)));
            Assert.IsTrue(item.Count == 1);
            Assert.AreEqual(Path.Combine(Path.GetDirectoryName(IncludePath), newName), item.Single().EvaluatedInclude);
        }

        [TestMethod]
        public void Should_DeleteItem()
        {
            SolutionInfo.AddItem(ProjectFileName, IncludePath, Content, ItemType.EmbeddedResource);

            Assert.IsTrue(File.Exists(FileFullPath));

            SolutionInfo.Delete(ProjectFileName, IncludePath);
            var item = CurrentProject.GetItems(ItemType.EmbeddedResource.ToString());

            Assert.IsTrue(!File.Exists(FileFullPath));
            Assert.IsTrue(item.Count == 0);
        }

        [TestMethod]
        public void Should_RegenerateItemContent()
        {
            var regeneratedContent = "REGENERATED CONTENT";
            SolutionInfo.AddItem(ProjectFileName, IncludePath, Content, ItemType.EmbeddedResource);
            var item = CurrentProject.GetItems(ItemType.EmbeddedResource.ToString());
            Assert.IsTrue(File.Exists(FileFullPath));
            Assert.IsTrue(item.Count == 1);
            Assert.AreEqual(IncludePath, item.Single().EvaluatedInclude);
            Assert.AreEqual(Content, File.ReadAllText(FileFullPath));

            SolutionInfo.Regenerate(ProjectFileName, IncludePath, regeneratedContent);
            Assert.AreEqual(regeneratedContent, File.ReadAllText(FileFullPath));
        }
    }
}
