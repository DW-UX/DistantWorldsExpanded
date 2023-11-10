using System;
using System.Globalization;

namespace System.Runtime.Serialization
{
    internal class DataNode<T> : IDataNode
    {
        protected Type dataType;
        private T _value;
        private string _dataContractName;
        private string _dataContractNamespace;
        private string _clrTypeName;
        private string _clrAssemblyName;
        private string _id = Globals.NewObjectId;
        private bool _isFinalValue;

        internal DataNode()
        {
            dataType = typeof(T);
            _isFinalValue = true;
        }

        internal DataNode(T value)
            : this()
        {
            _value = value;
        }

        public Type DataType => dataType;

        public object Value
        {
            get => _value;
            set => _value = (T)value;
        }

        bool IDataNode.IsFinalValue
        {
            get => _isFinalValue;
            set => _isFinalValue = value;
        }

        public T GetValue()
        {
            return _value;
        }

        public string DataContractName
        {
            get => _dataContractName;
            set => _dataContractName = value;
        }

        public string DataContractNamespace
        {
            get => _dataContractNamespace;
            set => _dataContractNamespace = value;
        }

        public string ClrTypeName
        {
            get => _clrTypeName;
            set => _clrTypeName = value;
        }

        public string ClrAssemblyName
        {
            get => _clrAssemblyName;
            set => _clrAssemblyName = value;
        }

        public bool PreservesReferences => (Id != Globals.NewObjectId);

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public virtual void GetData(ElementData element)
        {
            element.dataNode = this;
            element.attributeCount = 0;
            element.childElementIndex = 0;

            if (DataContractName != null)
            {
                AddQualifiedNameAttribute(element, Globals.XsiPrefix, Globals.XsiTypeLocalName, Globals.SchemaInstanceNamespace, DataContractName, DataContractNamespace);
            }

            if (ClrTypeName != null)
            {
                element.AddAttribute(Globals.SerPrefix, Globals.SerializationNamespace, Globals.ClrTypeLocalName, ClrTypeName);
            }

            if (ClrAssemblyName != null)
            {
                element.AddAttribute(Globals.SerPrefix, Globals.SerializationNamespace, Globals.ClrAssemblyLocalName, ClrAssemblyName);
            }
        }

        public virtual void Clear()
        {
            // dataContractName not cleared because it is used when re-serializing from unknown data
            _clrTypeName = _clrAssemblyName = null;
        }

        internal void AddQualifiedNameAttribute(ElementData element, string elementPrefix, string elementName, string elementNs, string valueName, string valueNs)
        {
            string prefix = ExtensionDataReader.GetPrefix(valueNs);
            element.AddAttribute(elementPrefix, elementNs, elementName, string.Format(CultureInfo.InvariantCulture, "{0}:{1}", prefix, valueName));

            bool prefixDeclaredOnElement = false;
            if (element.attributes != null)
            {
                for (int i = 0; i < element.attributes.Length; i++)
                {
                    AttributeData attribute = element.attributes[i];
                    if (attribute != null && attribute.prefix == Globals.XmlnsPrefix && attribute.localName == prefix)
                    {
                        prefixDeclaredOnElement = true;
                        break;
                    }
                }
            }
            if (!prefixDeclaredOnElement)
            {
                element.AddAttribute(Globals.XmlnsPrefix, Globals.XmlnsNamespace, prefix, valueNs);
            }
        }
    }
}
