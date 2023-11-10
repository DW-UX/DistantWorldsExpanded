using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Runtime.Serialization
{
    internal class GenericInfo : IGenericNameProvider
    {
        private readonly string genericTypeName;
        private readonly XmlQualifiedName stableName;
        private List<GenericInfo> paramGenericInfos;
        private readonly List<int> nestedParamCounts;

        internal GenericInfo(XmlQualifiedName stableName, string genericTypeName)
        {
            this.stableName = stableName;
            this.genericTypeName = genericTypeName;
            nestedParamCounts = new List<int>
            {
                0
            };
        }

        internal void Add(GenericInfo actualParamInfo)
        {
            if (paramGenericInfos == null)
            {
                paramGenericInfos = new List<GenericInfo>();
            }

            paramGenericInfos.Add(actualParamInfo);
        }

        internal void AddToLevel(int level, int count)
        {
            if (level >= nestedParamCounts.Count)
            {
                do
                {
                    nestedParamCounts.Add((level == nestedParamCounts.Count) ? count : 0);
                } while (level >= nestedParamCounts.Count);
            }
            else
            {
                nestedParamCounts[level] = nestedParamCounts[level] + count;
            }
        }

        internal XmlQualifiedName GetExpandedStableName()
        {
            if (paramGenericInfos == null)
            {
                return stableName;
            }

            return new XmlQualifiedName(DataContract.EncodeLocalName(DataContract.ExpandGenericParameters(XmlConvert.DecodeName(stableName.Name), this)), stableName.Namespace);
        }

        internal string GetStableNamespace()
        {
            return stableName.Namespace;
        }

        internal XmlQualifiedName StableName => stableName;

        internal IList<GenericInfo> Parameters => paramGenericInfos;

        public int GetParameterCount()
        {
            return paramGenericInfos.Count;
        }

        public IList<int> GetNestedParameterCounts()
        {
            return nestedParamCounts;
        }

        public string GetParameterName(int paramIndex)
        {
            return paramGenericInfos[paramIndex].GetExpandedStableName().Name;
        }

        public string GetNamespaces()
        {
            StringBuilder namespaces = new StringBuilder();
            for (int j = 0; j < paramGenericInfos.Count; j++)
            {
                namespaces.Append(" ").Append(paramGenericInfos[j].GetStableNamespace());
            }

            return namespaces.ToString();
        }

        public string GetGenericTypeName()
        {
            return genericTypeName;
        }

        public bool ParametersFromBuiltInNamespaces
        {
            get
            {
                bool parametersFromBuiltInNamespaces = true;
                for (int j = 0; j < paramGenericInfos.Count; j++)
                {
                    if (parametersFromBuiltInNamespaces)
                    {
                        parametersFromBuiltInNamespaces = DataContract.IsBuiltInNamespace(paramGenericInfos[j].GetStableNamespace());
                    }
                    else
                    {
                        break;
                    }
                }
                return parametersFromBuiltInNamespaces;
            }
        }

    }
}
