using System;

namespace System.Runtime.Serialization
{
    internal class AttributeData
    {
        public string prefix;
        public string ns;
        public string localName;
        public string value;
    }

    internal class ElementData
    {
        public string localName;
        public string ns;
        public string prefix;
        public int attributeCount;
        public AttributeData[] attributes;
        public IDataNode dataNode;
        public int childElementIndex;

        public void AddAttribute(string prefix, string ns, string name, string value)
        {
            GrowAttributesIfNeeded();
            AttributeData attribute = attributes[attributeCount];
            if (attribute == null)
            {
                attributes[attributeCount] = attribute = new AttributeData();
            }

            attribute.prefix = prefix;
            attribute.ns = ns;
            attribute.localName = name;
            attribute.value = value;
            attributeCount++;
        }

        private void GrowAttributesIfNeeded()
        {
            if (attributes == null)
            {
                attributes = new AttributeData[4];
            }
            else if (attributes.Length == attributeCount)
            {
                AttributeData[] newAttributes = new AttributeData[attributes.Length * 2];
                Array.Copy(attributes, 0, newAttributes, 0, attributes.Length);
                attributes = newAttributes;
            }
        }
    }
}
