using System;
using System.Collections.Generic;

namespace CodeGenerator.Generators
{
    [Serializable]
    public abstract class BaseEngineHost
    {
        private readonly IList<string> assemblyLocationList;
        private readonly IList<string> namespaceList;

        public BaseEngineHost()
        {
            assemblyLocationList = new List<string>();
            namespaceList = new List<string>();
        }

        public IList<string> AssemblyLocationList
        {
            get { return assemblyLocationList;}
        }

        public void AddAssemblyLocation(string assemblyLocation)
        {
            if (assemblyLocationList.Contains(assemblyLocation)) return;
            assemblyLocationList.Add(assemblyLocation);
        }

        public IList<string> NamespaceList
        {
            get { return namespaceList; }
        }

        public void AddNamespace(string namespaceName)
        {
            if (namespaceList.Contains(namespaceName)) return;
            namespaceList.Add(namespaceName);
        }

        #region ExtendProperties

        Dictionary<string, object> _ExtendProperties = new Dictionary<string, object>();

        public void SetValue(string key, object value)
        {
            if (_ExtendProperties.ContainsKey(key))
            {
                _ExtendProperties[key] = value;
            }
            else
            {
                _ExtendProperties.Add(key, value);
            }
        }

        public object GetValue(string key)
        {
            if (_ExtendProperties.ContainsKey(key))
            {
                return _ExtendProperties[key];
            }
            else
            {
                return null;
            }
        }

        public string GetString(string key)
        {
            object obj = GetValue(key);
            if (obj == null) return string.Empty;
            else return obj.ToString();
        }

        //public int GetInt32(string key)
        //{
        //    return ConvertUtil.ToInt32(GetValue(key), 0);
        //}

        //public bool GetBoolean(string key)
        //{
        //    return ConvertUtil.ToBoolean(GetValue(key), false);
        //}

        //public DateTime GetDateTime(string key)
        //{
        //    return ConvertUtil.ToDateTime(GetValue(key), DateTime.MinValue);
        //}

        //public decimal GetDecimal(string key)
        //{
        //    return ConvertUtil.ToDecimal(GetValue(key), decimal.Zero);
        //}

        #endregion
    }
}
