using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CodeGenerator.Utilities;

namespace CodeGenerator.Generators
{
    //Should be singleton
    public class SolutionInfo
    {
        private const string SolutionFileExtension = ".sln";
        private const string ProjectFileExtension = ".csproj";

        public readonly DirectoryInfo SolutionDirectory;
        public readonly FileInfo SolutionFile;
        public readonly List<Project> Projects;

        public SolutionInfo(string path)
        {
            if(!IsSolutionDirectory(path, out SolutionFile))
            {
                throw new ArgumentException($"Directory at path [{path}] does not directory of solution.");
            }
            Projects = GetProjects();
            SolutionDirectory = new DirectoryInfo(path);
        }

        public Project this[string projectPath]
        {
            get
            {
                return Projects.GetProject(projectPath);
            }
        }

        public ICollection<ProjectItem> this[string projectPath, string evaluatedInclude]
        {
            get
            {
                return Projects.Single(x => x.FullPath.Contains(projectPath)).GetItemsByEvaluatedInclude(evaluatedInclude);
            }
        }

        public bool IsSolutionDirectory(string path, out FileInfo solutionFile)
        {
            solutionFile = new DirectoryInfo(path).GetFiles().SingleOrDefault(f => Path.GetExtension(f.FullName) == SolutionFileExtension);
            return solutionFile != null;
        }

        public void AddItem(string projectFileName, string includePath, string content, ItemType itemType = ItemType.Compile)
        {
            var proj = this[projectFileName];
            Directory.CreateDirectory(Path.Combine(proj.DirectoryPath, Path.GetDirectoryName(includePath)));
            File.WriteAllText(Path.Combine(proj.DirectoryPath, includePath), content);
            proj.AddItem(itemType.ToString(), includePath);
            proj.Save();
        }

        public void Rename(string projectFileName, string includePath, string newName)
        {
            var proj = this[projectFileName];
            var item = proj.GetItemsByEvaluatedInclude(includePath).Single();
            var sourceFullPath = Path.Combine(proj.DirectoryPath, includePath);
            var destFullPath = Path.Combine(Path.GetDirectoryName(sourceFullPath), newName);
            File.Move(sourceFullPath, destFullPath);
            item.Rename(Path.Combine(Path.GetDirectoryName(includePath), newName));
            proj.Save();
        }

        public void Delete(string projectFileName, string includePath)
        {
            var proj = this[projectFileName];
            var item = proj.GetItemsByEvaluatedInclude(includePath).Single();
            var sourceFullPath = Path.Combine(proj.DirectoryPath, includePath);
            File.Delete(sourceFullPath);
            proj.RemoveItem(item);
            proj.Save();
        }

        public void Regenerate(string projectFileName, string includePath, string content)
        {
            var proj = this[projectFileName];
            File.WriteAllText(Path.Combine(proj.DirectoryPath, includePath), content);
        }

        public List<Project> GetProjects()
        {
            var projects = new List<Project>();
            using (var fileStream = SolutionFile.OpenRead())
            {
                using (var stream = new StreamReader(fileStream))
                {
                    var matches = Regex.Matches(stream.ReadToEnd(), "(?<=Project\\(\"\\{[\\w\\d-]+\\}\"\\) = \"[\\w\\.]+\", \")([\\.\\w\\\\]+(cs)proj)");
                    foreach (Match match in matches)
                    {
                        projects.Add(new Project(Path.Combine(SolutionFile.DirectoryName, match.Value)));
                    }
                }
            }

            return projects;
        }

        public string GetProjectFile(DirectoryInfo includedDirectory)
        {
            if (SolutionDirectory == includedDirectory)
            {
                return null;
            }

            if (includedDirectory.Exists)
            {
                var file = includedDirectory.GetFiles().ToList().SingleOrDefault(f => Path.GetExtension(f.FullName) == ProjectFileExtension)?.FullName;
                return file ?? GetProjectFile(includedDirectory.Parent);
            }
            else return GetProjectFile(includedDirectory.Parent);
        }
    }
}
