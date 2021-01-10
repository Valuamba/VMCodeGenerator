using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CodeGenerator.Document.Xml
{
    public abstract class XmlBuilder
    {
        protected abstract XmlBuilderCommonSettings BuilderSettings { get; }

        protected void Serialize<T>(T obj) where T : class
        {
            using (FileStream writer = File.Open(BuilderSettings.FilePath, FileMode.OpenOrCreate))
            {
                XmlSerializer x = new XmlSerializer(typeof(T));

                x.Serialize(writer, obj);
            }
        }

        protected T Deserialize<T>() where T : class
        {
            T obj;
            using (FileStream writer = File.Open(BuilderSettings.FilePath, FileMode.OpenOrCreate))
            {
                XmlSerializer x = new XmlSerializer(typeof(T));

                obj = x.Deserialize(writer) as T;
            }

            return obj;
        }

        protected void InitializeFile<T>(T rootObj = null) where T : class, new()
        {
            var fileInfo = new FileInfo(BuilderSettings.FilePath);
            //Directory.CreateDirectory(BuilderSettings.DirectoryPath);
            if (!fileInfo.Exists || fileInfo.Length == 0)
            {
                if (rootObj == null)
                {
                    rootObj = new T();
                }
                //Logger.Info($"Initialize file at path [{BuilderSettings.FilePath}]");
                Serialize(rootObj);
            }
        }
    }
}
