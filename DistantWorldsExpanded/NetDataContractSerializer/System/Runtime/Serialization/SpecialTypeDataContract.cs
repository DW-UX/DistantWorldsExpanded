using System;
using System.Xml;

namespace System.Runtime.Serialization
{
    internal sealed class SpecialTypeDataContract : DataContract
    {
        public SpecialTypeDataContract(Type type)
            : base(new SpecialTypeDataContractCriticalHelper(type))
        {
        }

        public SpecialTypeDataContract(Type type, XmlDictionaryString name, XmlDictionaryString ns)
            : base(new SpecialTypeDataContractCriticalHelper(type, name, ns))
        {
        }

        internal override bool IsBuiltInDataContract => true;

        private class SpecialTypeDataContractCriticalHelper : DataContract.DataContractCriticalHelper
        {
            internal SpecialTypeDataContractCriticalHelper(Type type)
                : base(type)
            {
            }

            internal SpecialTypeDataContractCriticalHelper(Type type, XmlDictionaryString name, XmlDictionaryString ns)
                : base(type)
            {
                SetDataContractName(name, ns);
            }
        }
    }
}
