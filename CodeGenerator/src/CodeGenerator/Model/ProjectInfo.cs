using Microsoft.Build.Evaluation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Generators
{
    public class ProjectInfo : Project
    {
        public ProjectInfo(string path) : base(path)
        {
        }

        private ProjectItem GetItemByEvaluatedInclude(string evaluatedInclude)
        {
            return GetItemsByEvaluatedInclude(evaluatedInclude).Single();
        }

        private void ProjectActionMethod(Action<string, string, string> action, string includeDirectoryPath, string fileName)
        {
            string fullDirectoryPath = Path.Combine(DirectoryPath, includeDirectoryPath);
            string fullPath = Path.Combine(fullDirectoryPath, fileName);
            string includePath = Path.Combine(includeDirectoryPath, fileName);

            action(fullDirectoryPath, fullPath, includePath);
            Save();
        }

        public void AddItem(string includeDirectoryPath, string fileName, string content, ItemType itemType)
        {
            ProjectActionMethod((fullDirectoryPath, fullPath, includePath) =>
            {
                Directory.CreateDirectory(fullDirectoryPath);
                File.WriteAllText(fullPath, content);
                AddItem(itemType.ToString(), includePath);

            }, includeDirectoryPath, fileName);
        }

        public void Rename(string includeDirectoryPath, string fileName, string newName)
        {
            ProjectActionMethod((fullDirectoryPath, fullPath, includePath) =>
            {
                var item = GetItemByEvaluatedInclude(includePath);
                var destFullPath = Path.Combine(fullDirectoryPath, newName);
                File.Move(fullPath, destFullPath);
                item.Rename(Path.Combine(includeDirectoryPath, newName));

            }, includeDirectoryPath, fileName);
        }

        public void Delete(string includeDirectoryPath, string fileName)
        {
            ProjectActionMethod((fullDirectoryPath, fullPath, includePath) =>
            {
                var item = GetItemByEvaluatedInclude(includePath);
                File.Delete(fullPath);
                RemoveItem(item);

            }, includeDirectoryPath, fileName);
        }

        public void Regenerate(string includeDirectoryPath, string fileName, string content)
        {
            ProjectActionMethod((fullDirectoryPath, fullPath, includePath) =>
            {
                File.WriteAllText(fullPath, content);

            }, includeDirectoryPath, fileName);
        }
    }
}
