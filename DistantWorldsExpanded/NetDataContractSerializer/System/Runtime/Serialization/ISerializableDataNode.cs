using System.Collections.Generic;

namespace System.Runtime.Serialization
{
    internal class ISerializableDataNode : DataNode<object>
    {
        private string _factoryTypeName;
        private string _factoryTypeNamespace;
        private IList<ISerializableDataMember> _members;

        internal ISerializableDataNode()
        {
            dataType = Globals.TypeOfISerializableDataNode;
        }

        internal string FactoryTypeName
        {
            get => _factoryTypeName;
            set => _factoryTypeName = value;
        }

        internal string FactoryTypeNamespace
        {
            get => _factoryTypeNamespace;
            set => _factoryTypeNamespace = value;
        }

        internal IList<ISerializableDataMember> Members
        {
            get => _members;
            set => _members = value;
        }

        public override void GetData(ElementData element)
        {
            base.GetData(element);

            if (FactoryTypeName != null)
            {
                AddQualifiedNameAttribute(element, Globals.SerPrefix, Globals.ISerializableFactoryTypeLocalName, Globals.SerializationNamespace, FactoryTypeName, FactoryTypeNamespace);
            }
        }

        public override void Clear()
        {
            base.Clear();
            _members = null;
            _factoryTypeName = _factoryTypeNamespace = null;
        }
    }
}
