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

        public readonly DirectoryInfo SolutionDirectory;
        public readonly FileInfo SolutionFile;
        public readonly List<ProjectInfo> Projects;

        private SolutionInfo(string path)
        {
            if (!IsSolutionDirectory(path, out SolutionFile))
            {
                throw new ArgumentException($"Directory at path [{path}] does not directory of solution.");
            }
            Projects = GetProjects();
            SolutionDirectory = new DirectoryInfo(path);
        }

        private static SolutionInfo instance;
        public static SolutionInfo GetInstance(string path)
        {
            if (instance == null)
            {
                instance = new SolutionInfo(path);
            }
            return instance;
        }

        public ProjectInfo this[string projectPath] => Projects.GetProject(projectPath);

        public bool IsSolutionDirectory(string path, out FileInfo solutionFile)
        {
            solutionFile = new DirectoryInfo(path).GetFiles().SingleOrDefault(f => Path.GetExtension(f.FullName) == SolutionFileExtension);
            return solutionFile != null;
        }

        public void AddItem(string projectFileName, string includeDirectoryPath, string fileName, string content, ItemType itemType = ItemType.Compile)
        {
            this[projectFileName].AddItem(includeDirectoryPath, fileName, content, itemType);
        }

        public void Rename(string projectFileName, string includeDirectoryPath, string fileName, string newName)
        {
            this[projectFileName].Rename(includeDirectoryPath, fileName, newName);

        }

        public void Delete(string projectFileName, string includeDirectoryPath, string fileName)
        {
            this[projectFileName].Delete(includeDirectoryPath, fileName);
        }

        public void Regenerate(string projectFileName, string includeDirectoryPath, string fileName, string content)
        {
            this[projectFileName].Regenerate(includeDirectoryPath, fileName, content);
        }

        public List<ProjectInfo> GetProjects()
        {
            var projects = new List<ProjectInfo>();
            using (var fileStream = SolutionFile.OpenRead())
            {
                using (var stream = new StreamReader(fileStream))
                {
                    var matches = Regex.Matches(stream.ReadToEnd(), "(?<=Project\\(\"\\{[\\w\\d-]+\\}\"\\) = \"[\\w\\.]+\", \")([\\.\\w\\\\]+(cs)proj)");
                    foreach (Match match in matches)
                    {
                        projects.Add(new ProjectInfo(Path.Combine(SolutionFile.DirectoryName, match.Value)));
                    }
                }
            }

            return projects;
        }
    }
}
