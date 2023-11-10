using System.Collections.Generic;
using System.Xml;

namespace System.Runtime.Serialization
{
    internal class XmlDataNode : DataNode<object>
    {
        private IList<XmlAttribute> _xmlAttributes;
        private IList<XmlNode> _xmlChildNodes;
        private XmlDocument _ownerDocument;

        internal XmlDataNode()
        {
            dataType = Globals.TypeOfXmlDataNode;
        }

        internal IList<XmlAttribute> XmlAttributes
        {
            get => _xmlAttributes;
            set => _xmlAttributes = value;
        }

        internal IList<XmlNode> XmlChildNodes
        {
            get => _xmlChildNodes;
            set => _xmlChildNodes = value;
        }

        internal XmlDocument OwnerDocument
        {
            get => _ownerDocument;
            set => _ownerDocument = value;
        }

        public override void Clear()
        {
            base.Clear();
            _xmlAttributes = null;
            _xmlChildNodes = null;
            _ownerDocument = null;
        }
    }
}
