using System;
using System.Xml;

namespace System.Runtime.Serialization
{
    using DataContractDictionary = System.Collections.Generic.Dictionary<XmlQualifiedName, DataContract>;

    internal struct ScopedKnownTypes
    {
        internal DataContractDictionary[] dataContractDictionaries;
        private int count;

        internal void Push(DataContractDictionary dataContractDictionary)
        {
            if (dataContractDictionaries == null)
            {
                dataContractDictionaries = new DataContractDictionary[4];
            }
            else if (count == dataContractDictionaries.Length)
            {
                Array.Resize<DataContractDictionary>(ref dataContractDictionaries, dataContractDictionaries.Length * 2);
            }

            dataContractDictionaries[count++] = dataContractDictionary;
        }

        internal void Pop()
        {
            count--;
        }

        internal DataContract GetDataContract(XmlQualifiedName qname)
        {
            for (int i = (count - 1); i >= 0; i--)
            {
                DataContractDictionary dataContractDictionary = dataContractDictionaries[i];
                if (dataContractDictionary.TryGetValue(qname, out DataContract dataContract))
                {
                    return dataContract;
                }
            }
            return null;
        }

    }
}
