using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization
{
    internal interface IGenericNameProvider
    {
        int GetParameterCount();
        IList<int> GetNestedParameterCounts();
        string GetParameterName(int paramIndex);
        string GetNamespaces();
        string GetGenericTypeName();
        bool ParametersFromBuiltInNamespaces { get; }
    }

    internal class GenericNameProvider : IGenericNameProvider
    {
        private readonly string _genericTypeName;
        private readonly object[] _genericParams; //Type or DataContract
        private readonly IList<int> _nestedParamCounts;

        internal GenericNameProvider(Type type)
            : this(DataContract.GetClrTypeFullName(type.GetGenericTypeDefinition()), type.GetGenericArguments())
        {
        }

        internal GenericNameProvider(string genericTypeName, object[] genericParams)
        {
            _genericTypeName = genericTypeName;
            _genericParams = new object[genericParams.Length];
            genericParams.CopyTo(_genericParams, 0);

            DataContract.GetClrNameAndNamespace(genericTypeName, out string name, out string ns);
            _nestedParamCounts = DataContract.GetDataContractNameForGenericName(name, null);
        }

        public int GetParameterCount()
        {
            return _genericParams.Length;
        }

        public IList<int> GetNestedParameterCounts()
        {
            return _nestedParamCounts;
        }

        public string GetParameterName(int paramIndex)
        {
            return GetStableName(paramIndex).Name;
        }

        public string GetNamespaces()
        {
            StringBuilder namespaces = new StringBuilder();
            for (int j = 0; j < GetParameterCount(); j++)
            {
                namespaces.Append(" ").Append(GetStableName(j).Namespace);
            }

            return namespaces.ToString();
        }

        public string GetGenericTypeName()
        {
            return _genericTypeName;
        }

        public bool ParametersFromBuiltInNamespaces
        {
            get
            {
                bool parametersFromBuiltInNamespaces = true;
                for (int j = 0; j < GetParameterCount(); j++)
                {
                    if (parametersFromBuiltInNamespaces)
                    {
                        parametersFromBuiltInNamespaces = DataContract.IsBuiltInNamespace(GetStableName(j).Namespace);
                    }
                    else
                    {
                        break;
                    }
                }
                return parametersFromBuiltInNamespaces;
            }
        }

        private XmlQualifiedName GetStableName(int i)
        {
            object o = _genericParams[i];
            XmlQualifiedName qname = o as XmlQualifiedName;
            if (qname == null)
            {
                Type paramType = o as Type;
                if (paramType != null)
                {
                    _genericParams[i] = qname = DataContract.GetStableName(paramType);
                }
                else
                {
                    _genericParams[i] = qname = ((DataContract)o).StableName;
                }
            }
            return qname;
        }
    }
}
