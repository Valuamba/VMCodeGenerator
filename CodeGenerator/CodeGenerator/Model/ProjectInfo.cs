using CodeGenerator.Localization;
using CodeGenerator.Utilities.ExceptionUtilities;
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
        private readonly MessageLocalization MessageLocalization = Startup.MessageLocalization;

        public ProjectInfo(string path) : base(path)
        {
        }

        private ProjectItem GetItemByEvaluatedInclude(string evaluatedInclude)
        {
            return GetItemsByEvaluatedInclude(evaluatedInclude).SingleOrDefault();
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
                ThrowExceptionUtility.ThrowException<ArgumentException>(() => IsFileExist(fullPath) || IsItemExist(includePath),
                    message: MessageLocalization.GetMessage("project.error.file.isAlreadyExist", includePath));
                Directory.CreateDirectory(fullDirectoryPath);
                File.WriteAllText(fullPath, content);
                AddItem(itemType.ToString(), includePath);

            }, includeDirectoryPath, fileName);
        }

        public bool IsItemExist(string includePath)
        {
            return GetItemByEvaluatedInclude(includePath) != null;
        }

        public bool IsFileExist(string fullPath)
        {
            return File.Exists(fullPath);
        }

        public void Rename(string includeDirectoryPath, string fileName, string newName)
        {
            ProjectActionMethod((fullDirectoryPath, fullPath, includePath) =>
            {
                ThrowExceptionUtility.ThrowException<ArgumentException>(() => !IsFileExist(fullPath) || !IsItemExist(includePath),
                    message: MessageLocalization.GetMessage("project.error.file.doesNotExist", includePath));
                var item = GetItemByEvaluatedInclude(includePath);
                var destFullPath = Path.Combine(fullDirectoryPath, newName);
                var newIncludePath = Path.Combine(includeDirectoryPath, newName);
                ThrowExceptionUtility.ThrowException<ArgumentException>(() => IsFileExist(destFullPath) || IsItemExist(newIncludePath),
                    message: MessageLocalization.GetMessage("project.error.file.isAlreadyExist", newIncludePath));
                File.Move(fullPath, destFullPath);
                item.Rename(newIncludePath);

            }, includeDirectoryPath, fileName);
        }

        public void Delete(string includeDirectoryPath, string fileName)
        {
            ProjectActionMethod((fullDirectoryPath, fullPath, includePath) =>
            {
                ThrowExceptionUtility.ThrowException<ArgumentException>(() => !IsFileExist(fullPath) || !IsItemExist(includePath),
                    message: MessageLocalization.GetMessage("project.error.file.doesNotExist", includePath));
                var item = GetItemByEvaluatedInclude(includePath);
                File.Delete(fullPath);
                RemoveItem(item);

            }, includeDirectoryPath, fileName);
        }

        public void Regenerate(string includeDirectoryPath, string fileName, string content)
        {
            ProjectActionMethod((fullDirectoryPath, fullPath, includePath) =>
            {
                ThrowExceptionUtility.ThrowException<ArgumentException>(() => !IsFileExist(fullPath) || !IsItemExist(includePath),
                    message: MessageLocalization.GetMessage("project.error.file.doesNotExist", includePath));
                File.WriteAllText(fullPath, content);

            }, includeDirectoryPath, fileName);
        }
    }
}
