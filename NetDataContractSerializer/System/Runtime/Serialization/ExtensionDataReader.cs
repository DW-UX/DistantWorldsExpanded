using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using SerializationException = System.Runtime.Serialization.SerializationException;

namespace System.Runtime.Serialization
{
    internal class ExtensionDataReader : XmlReader
    {
        private enum ExtensionDataNodeType
        {
            None,
            Element,
            EndElement,
            Text,
            Xml,
            ReferencedElement,
            NullElement,
        }

        private readonly Hashtable cache = new Hashtable();
        private ElementData[] elements;
        private ElementData element;
        private ElementData nextElement;
        private ReadState readState = ReadState.Initial;
        private ExtensionDataNodeType internalNodeType;
        private XmlNodeType nodeType;
        private int depth;
        private string localName;
        private string ns;
        private string prefix;
        private string value;
        private int attributeCount;
        private int attributeIndex;
        private XmlNodeReader xmlNodeReader;
        private Queue<IDataNode> deserializedDataNodes;
        private readonly XmlObjectSerializerReadContext context;

        private static readonly Dictionary<string, string> nsToPrefixTable;

        private static readonly Dictionary<string, string> prefixToNsTable;

        static ExtensionDataReader()
        {
            nsToPrefixTable = new Dictionary<string, string>();
            prefixToNsTable = new Dictionary<string, string>();
            AddPrefix(Globals.XsiPrefix, Globals.SchemaInstanceNamespace);
            AddPrefix(Globals.SerPrefix, Globals.SerializationNamespace);
            AddPrefix(string.Empty, string.Empty);
        }

        internal ExtensionDataReader(XmlObjectSerializerReadContext context)
        {
            attributeIndex = -1;
            this.context = context;
        }

        internal void SetDeserializedValue(object obj)
        {
            IDataNode deserializedDataNode = (deserializedDataNodes == null || deserializedDataNodes.Count == 0) ? null : deserializedDataNodes.Dequeue();
            if (deserializedDataNode != null && !(obj is IDataNode))
            {
                deserializedDataNode.Value = obj;
                deserializedDataNode.IsFinalValue = true;
            }
        }

        internal IDataNode GetCurrentNode()
        {
            IDataNode retVal = element.dataNode;
            Skip();
            return retVal;
        }

        internal void SetDataNode(IDataNode dataNode, string name, string ns)
        {
            SetNextElement(dataNode, name, ns, null);
            element = nextElement;
            nextElement = null;
            SetElement();
        }

        internal void Reset()
        {
            localName = null;
            ns = null;
            prefix = null;
            value = null;
            attributeCount = 0;
            attributeIndex = -1;
            depth = 0;
            element = null;
            nextElement = null;
            elements = null;
            deserializedDataNodes = null;
        }

        private bool IsXmlDataNode => (internalNodeType == ExtensionDataNodeType.Xml);

        public override XmlNodeType NodeType => IsXmlDataNode ? xmlNodeReader.NodeType : nodeType;
        public override string LocalName => IsXmlDataNode ? xmlNodeReader.LocalName : localName;
        public override string NamespaceURI => IsXmlDataNode ? xmlNodeReader.NamespaceURI : ns;
        public override string Prefix => IsXmlDataNode ? xmlNodeReader.Prefix : prefix;
        public override string Value => IsXmlDataNode ? xmlNodeReader.Value : value;
        public override int Depth => IsXmlDataNode ? xmlNodeReader.Depth : depth;
        public override int AttributeCount => IsXmlDataNode ? xmlNodeReader.AttributeCount : attributeCount;
        public override bool EOF => IsXmlDataNode ? xmlNodeReader.EOF : (readState == ReadState.EndOfFile);
        public override ReadState ReadState => IsXmlDataNode ? xmlNodeReader.ReadState : readState;
        public override bool IsEmptyElement => IsXmlDataNode ? xmlNodeReader.IsEmptyElement : false;
        public override bool IsDefault => IsXmlDataNode ? xmlNodeReader.IsDefault : base.IsDefault;
        public override char QuoteChar => IsXmlDataNode ? xmlNodeReader.QuoteChar : base.QuoteChar;
        public override XmlSpace XmlSpace => IsXmlDataNode ? xmlNodeReader.XmlSpace : base.XmlSpace;
        public override string XmlLang => IsXmlDataNode ? xmlNodeReader.XmlLang : base.XmlLang;
        public override string this[int i] => IsXmlDataNode ? xmlNodeReader[i] : GetAttribute(i);
        public override string this[string name] => IsXmlDataNode ? xmlNodeReader[name] : GetAttribute(name);
        public override string this[string name, string namespaceURI] => IsXmlDataNode ? xmlNodeReader[name, namespaceURI] : GetAttribute(name, namespaceURI);

        public override bool MoveToFirstAttribute()
        {
            if (IsXmlDataNode)
            {
                return xmlNodeReader.MoveToFirstAttribute();
            }

            if (attributeCount == 0)
            {
                return false;
            }

            MoveToAttribute(0);
            return true;
        }

        public override bool MoveToNextAttribute()
        {
            if (IsXmlDataNode)
            {
                return xmlNodeReader.MoveToNextAttribute();
            }

            if (attributeIndex + 1 >= attributeCount)
            {
                return false;
            }

            MoveToAttribute(attributeIndex + 1);
            return true;
        }

        public override void MoveToAttribute(int index)
        {
            if (IsXmlDataNode)
            {
                xmlNodeReader.MoveToAttribute(index);
            }
            else
            {
                if (index < 0 || index >= attributeCount)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.InvalidXmlDeserializingExtensionData));
                }

                nodeType = XmlNodeType.Attribute;
                AttributeData attribute = element.attributes[index];
                localName = attribute.localName;
                ns = attribute.ns;
                prefix = attribute.prefix;
                value = attribute.value;
                attributeIndex = index;
            }
        }

        public override string GetAttribute(string name, string namespaceURI)
        {
            if (IsXmlDataNode)
            {
                return xmlNodeReader.GetAttribute(name, namespaceURI);
            }

            for (int i = 0; i < element.attributeCount; i++)
            {
                AttributeData attribute = element.attributes[i];
                if (attribute.localName == name && attribute.ns == namespaceURI)
                {
                    return attribute.value;
                }
            }

            return null;
        }

        public override bool MoveToAttribute(string name, string namespaceURI)
        {
            if (IsXmlDataNode)
            {
                return xmlNodeReader.MoveToAttribute(name, ns);
            }

            for (int i = 0; i < element.attributeCount; i++)
            {
                AttributeData attribute = element.attributes[i];
                if (attribute.localName == name && attribute.ns == namespaceURI)
                {
                    MoveToAttribute(i);
                    return true;
                }
            }

            return false;
        }

        public override bool MoveToElement()
        {
            if (IsXmlDataNode)
            {
                return xmlNodeReader.MoveToElement();
            }

            if (nodeType != XmlNodeType.Attribute)
            {
                return false;
            }

            SetElement();
            return true;
        }

        private void SetElement()
        {
            nodeType = XmlNodeType.Element;
            localName = element.localName;
            ns = element.ns;
            prefix = element.prefix;
            value = string.Empty;
            attributeCount = element.attributeCount;
            attributeIndex = -1;
        }

        public override string LookupNamespace(string prefix)
        {
            if (IsXmlDataNode)
            {
                return xmlNodeReader.LookupNamespace(prefix);
            }

            if (!prefixToNsTable.TryGetValue(prefix, out string ns))
            {
                return null;
            }

            return ns;
        }

        public override void Skip()
        {
            if (IsXmlDataNode)
            {
                xmlNodeReader.Skip();
            }
            else
            {
                if (ReadState != ReadState.Interactive)
                {
                    return;
                }

                MoveToElement();
                if (IsElementNode(internalNodeType))
                {
                    int depth = 1;
                    while (depth != 0)
                    {
                        if (!Read())
                        {
                            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.InvalidXmlDeserializingExtensionData));
                        }

                        if (IsElementNode(internalNodeType))
                        {
                            depth++;
                        }
                        else if (internalNodeType == ExtensionDataNodeType.EndElement)
                        {
                            ReadEndElement();
                            depth--;
                        }
                    }
                }
                else
                {
                    Read();
                }
            }
        }

        private bool IsElementNode(ExtensionDataNodeType nodeType)
        {
            return (nodeType == ExtensionDataNodeType.Element ||
                nodeType == ExtensionDataNodeType.ReferencedElement ||
                nodeType == ExtensionDataNodeType.NullElement);
        }

        public override void Close()
        {
            if (IsXmlDataNode)
            {
                xmlNodeReader.Close();
            }
            else
            {
                Reset();
                readState = ReadState.Closed;
            }
        }

        public override bool Read()
        {
            if (nodeType == XmlNodeType.Attribute && MoveToNextAttribute())
            {
                return true;
            }

            MoveNext(element.dataNode);

            switch (internalNodeType)
            {
                case ExtensionDataNodeType.Element:
                case ExtensionDataNodeType.ReferencedElement:
                case ExtensionDataNodeType.NullElement:
                    PushElement();
                    SetElement();
                    break;

                case ExtensionDataNodeType.Text:
                    nodeType = XmlNodeType.Text;
                    prefix = string.Empty;
                    ns = string.Empty;
                    localName = string.Empty;
                    attributeCount = 0;
                    attributeIndex = -1;
                    break;

                case ExtensionDataNodeType.EndElement:
                    nodeType = XmlNodeType.EndElement;
                    prefix = string.Empty;
                    ns = string.Empty;
                    localName = string.Empty;
                    value = string.Empty;
                    attributeCount = 0;
                    attributeIndex = -1;
                    PopElement();
                    break;

                case ExtensionDataNodeType.None:
                    if (depth != 0)
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.InvalidXmlDeserializingExtensionData));
                    }

                    nodeType = XmlNodeType.None;
                    prefix = string.Empty;
                    ns = string.Empty;
                    localName = string.Empty;
                    value = string.Empty;
                    attributeCount = 0;
                    readState = ReadState.EndOfFile;
                    return false;

                case ExtensionDataNodeType.Xml:
                    // do nothing
                    break;

                default:
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.InvalidStateInExtensionDataReader));
            }
            readState = ReadState.Interactive;
            return true;
        }

        public override string Name
        {
            get
            {
                if (IsXmlDataNode)
                {
                    return xmlNodeReader.Name;
                }
                Fx.Assert("ExtensionDataReader Name property should only be called for IXmlSerializable");
                return string.Empty;
            }
        }

        public override bool HasValue
        {
            get
            {
                if (IsXmlDataNode)
                {
                    return xmlNodeReader.HasValue;
                }
                Fx.Assert("ExtensionDataReader HasValue property should only be called for IXmlSerializable");
                return false;
            }
        }

        public override string BaseURI
        {
            get
            {
                if (IsXmlDataNode)
                {
                    return xmlNodeReader.BaseURI;
                }
                Fx.Assert("ExtensionDataReader BaseURI property should only be called for IXmlSerializable");
                return string.Empty;
            }
        }

        public override XmlNameTable NameTable
        {
            get
            {
                if (IsXmlDataNode)
                {
                    return xmlNodeReader.NameTable;
                }
                Fx.Assert("ExtensionDataReader NameTable property should only be called for IXmlSerializable");
                return null;
            }
        }

        public override string GetAttribute(string name)
        {
            if (IsXmlDataNode)
            {
                return xmlNodeReader.GetAttribute(name);
            }
            Fx.Assert("ExtensionDataReader GetAttribute method should only be called for IXmlSerializable");
            return null;
        }

        public override string GetAttribute(int i)
        {
            if (IsXmlDataNode)
            {
                return xmlNodeReader.GetAttribute(i);
            }
            Fx.Assert("ExtensionDataReader GetAttribute method should only be called for IXmlSerializable");
            return null;
        }

        public override bool MoveToAttribute(string name)
        {
            if (IsXmlDataNode)
            {
                return xmlNodeReader.MoveToAttribute(name);
            }
            Fx.Assert("ExtensionDataReader MoveToAttribute method should only be called for IXmlSerializable");
            return false;
        }

        public override void ResolveEntity()
        {
            if (IsXmlDataNode)
            {
                xmlNodeReader.ResolveEntity();
            }
            else
            {
                Fx.Assert("ExtensionDataReader ResolveEntity method should only be called for IXmlSerializable");
            }
        }

        public override bool ReadAttributeValue()
        {
            if (IsXmlDataNode)
            {
                return xmlNodeReader.ReadAttributeValue();
            }
            Fx.Assert("ExtensionDataReader ReadAttributeValue method should only be called for IXmlSerializable");
            return false;
        }

        private void MoveNext(IDataNode dataNode)
        {
            switch (internalNodeType)
            {
                case ExtensionDataNodeType.Text:
                case ExtensionDataNodeType.ReferencedElement:
                case ExtensionDataNodeType.NullElement:
                    internalNodeType = ExtensionDataNodeType.EndElement;
                    return;
                default:
                    Type dataNodeType = dataNode.DataType;
                    if (dataNodeType == Globals.TypeOfClassDataNode)
                    {
                        MoveNextInClass((ClassDataNode)dataNode);
                    }
                    else if (dataNodeType == Globals.TypeOfCollectionDataNode)
                    {
                        MoveNextInCollection((CollectionDataNode)dataNode);
                    }
                    else if (dataNodeType == Globals.TypeOfISerializableDataNode)
                    {
                        MoveNextInISerializable((ISerializableDataNode)dataNode);
                    }
                    else if (dataNodeType == Globals.TypeOfXmlDataNode)
                    {
                        MoveNextInXml((XmlDataNode)dataNode);
                    }
                    else if (dataNode.Value != null)
                    {
                        MoveToDeserializedObject(dataNode);
                    }
                    else
                    {
                        Fx.Assert("Encountered invalid data node when deserializing unknown data");
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.InvalidStateInExtensionDataReader));
                    }
                    break;
            }
        }

        private void SetNextElement(IDataNode node, string name, string ns, string prefix)
        {
            internalNodeType = ExtensionDataNodeType.Element;
            nextElement = GetNextElement();
            nextElement.localName = name;
            nextElement.ns = ns;
            nextElement.prefix = prefix;
            if (node == null)
            {
                nextElement.attributeCount = 0;
                nextElement.AddAttribute(Globals.XsiPrefix, Globals.SchemaInstanceNamespace, Globals.XsiNilLocalName, Globals.True);
                internalNodeType = ExtensionDataNodeType.NullElement;
            }
            else if (!CheckIfNodeHandled(node))
            {
                AddDeserializedDataNode(node);
                node.GetData(nextElement);
                if (node is XmlDataNode)
                {
                    MoveNextInXml((XmlDataNode)node);
                }
            }
        }

        private void AddDeserializedDataNode(IDataNode node)
        {
            if (node.Id != Globals.NewObjectId && (node.Value == null || !node.IsFinalValue))
            {
                if (deserializedDataNodes == null)
                {
                    deserializedDataNodes = new Queue<IDataNode>();
                }

                deserializedDataNodes.Enqueue(node);
            }
        }

        private bool CheckIfNodeHandled(IDataNode node)
        {
            bool handled = false;
            if (node.Id != Globals.NewObjectId)
            {
                handled = (cache[node] != null);
                if (handled)
                {
                    if (nextElement == null)
                    {
                        nextElement = GetNextElement();
                    }

                    nextElement.attributeCount = 0;
                    nextElement.AddAttribute(Globals.SerPrefix, Globals.SerializationNamespace, Globals.RefLocalName, node.Id.ToString(NumberFormatInfo.InvariantInfo));
                    nextElement.AddAttribute(Globals.XsiPrefix, Globals.SchemaInstanceNamespace, Globals.XsiNilLocalName, Globals.True);
                    internalNodeType = ExtensionDataNodeType.ReferencedElement;
                }
                else
                {
                    cache.Add(node, node);
                }
            }
            return handled;
        }

        private void MoveNextInClass(ClassDataNode dataNode)
        {
            if (dataNode.Members != null && element.childElementIndex < dataNode.Members.Count)
            {
                if (element.childElementIndex == 0)
                {
                    context.IncrementItemCount(-dataNode.Members.Count);
                }

                ExtensionDataMember member = dataNode.Members[element.childElementIndex++];
                SetNextElement(member.Value, member.Name, member.Namespace, GetPrefix(member.Namespace));
            }
            else
            {
                internalNodeType = ExtensionDataNodeType.EndElement;
                element.childElementIndex = 0;
            }
        }

        private void MoveNextInCollection(CollectionDataNode dataNode)
        {
            if (dataNode.Items != null && element.childElementIndex < dataNode.Items.Count)
            {
                if (element.childElementIndex == 0)
                {
                    context.IncrementItemCount(-dataNode.Items.Count);
                }

                IDataNode item = dataNode.Items[element.childElementIndex++];
                SetNextElement(item, dataNode.ItemName, dataNode.ItemNamespace, GetPrefix(dataNode.ItemNamespace));
            }
            else
            {
                internalNodeType = ExtensionDataNodeType.EndElement;
                element.childElementIndex = 0;
            }
        }

        private void MoveNextInISerializable(ISerializableDataNode dataNode)
        {
            if (dataNode.Members != null && element.childElementIndex < dataNode.Members.Count)
            {
                if (element.childElementIndex == 0)
                {
                    context.IncrementItemCount(-dataNode.Members.Count);
                }

                ISerializableDataMember member = dataNode.Members[element.childElementIndex++];
                SetNextElement(member.Value, member.Name, string.Empty, string.Empty);
            }
            else
            {
                internalNodeType = ExtensionDataNodeType.EndElement;
                element.childElementIndex = 0;
            }
        }

        private void MoveNextInXml(XmlDataNode dataNode)
        {
            if (IsXmlDataNode)
            {
                xmlNodeReader.Read();
                if (xmlNodeReader.Depth == 0)
                {
                    internalNodeType = ExtensionDataNodeType.EndElement;
                    xmlNodeReader = null;
                }
            }
            else
            {
                internalNodeType = ExtensionDataNodeType.Xml;
                if (element == null)
                {
                    element = nextElement;
                }
                else
                {
                    PushElement();
                }

                XmlNode wrapperElement = XmlObjectSerializerReadContext.CreateWrapperXmlElement(dataNode.OwnerDocument,
                    dataNode.XmlAttributes, dataNode.XmlChildNodes, element.prefix, element.localName, element.ns);
                for (int i = 0; i < element.attributeCount; i++)
                {
                    AttributeData a = element.attributes[i];
                    XmlAttribute xmlAttr = dataNode.OwnerDocument.CreateAttribute(a.prefix, a.localName, a.ns);
                    xmlAttr.Value = a.value;
                    wrapperElement.Attributes.Append(xmlAttr);
                }
                xmlNodeReader = new XmlNodeReader(wrapperElement);
                xmlNodeReader.Read();
            }
        }

        private void MoveToDeserializedObject(IDataNode dataNode)
        {
            Type type = dataNode.DataType;
            bool isTypedNode = true;
            if (type == Globals.TypeOfObject)
            {
                type = dataNode.Value.GetType();
                if (type == Globals.TypeOfObject)
                {
                    internalNodeType = ExtensionDataNodeType.EndElement;
                    return;
                }
                isTypedNode = false;
            }

            if (!MoveToText(type, dataNode, isTypedNode))
            {
                if (dataNode.IsFinalValue)
                {
                    internalNodeType = ExtensionDataNodeType.EndElement;
                }
                else
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.Format(SR.InvalidDataNode, DataContract.GetClrTypeFullName(type))));
                }
            }
        }

        private bool MoveToText(Type type, IDataNode dataNode, bool isTypedNode)
        {
            bool handled = true;
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<bool>)dataNode).GetValue() : (bool)dataNode.Value);
                    break;
                case TypeCode.Char:
                    value = XmlConvert.ToString((int)(isTypedNode ? ((DataNode<char>)dataNode).GetValue() : (char)dataNode.Value));
                    break;
                case TypeCode.Byte:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<byte>)dataNode).GetValue() : (byte)dataNode.Value);
                    break;
                case TypeCode.Int16:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<short>)dataNode).GetValue() : (short)dataNode.Value);
                    break;
                case TypeCode.Int32:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<int>)dataNode).GetValue() : (int)dataNode.Value);
                    break;
                case TypeCode.Int64:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<long>)dataNode).GetValue() : (long)dataNode.Value);
                    break;
                case TypeCode.Single:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<float>)dataNode).GetValue() : (float)dataNode.Value);
                    break;
                case TypeCode.Double:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<double>)dataNode).GetValue() : (double)dataNode.Value);
                    break;
                case TypeCode.Decimal:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<decimal>)dataNode).GetValue() : (decimal)dataNode.Value);
                    break;
                case TypeCode.DateTime:
                    DateTime dateTime = isTypedNode ? ((DataNode<DateTime>)dataNode).GetValue() : (DateTime)dataNode.Value;
                    value = dateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffffffK", DateTimeFormatInfo.InvariantInfo);
                    break;
                case TypeCode.String:
                    value = isTypedNode ? ((DataNode<string>)dataNode).GetValue() : (string)dataNode.Value;
                    break;
                case TypeCode.SByte:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<sbyte>)dataNode).GetValue() : (sbyte)dataNode.Value);
                    break;
                case TypeCode.UInt16:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<ushort>)dataNode).GetValue() : (ushort)dataNode.Value);
                    break;
                case TypeCode.UInt32:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<uint>)dataNode).GetValue() : (uint)dataNode.Value);
                    break;
                case TypeCode.UInt64:
                    value = XmlConvert.ToString(isTypedNode ? ((DataNode<ulong>)dataNode).GetValue() : (ulong)dataNode.Value);
                    break;
                case TypeCode.Object:
                default:
                    if (type == Globals.TypeOfByteArray)
                    {
                        byte[] bytes = isTypedNode ? ((DataNode<byte[]>)dataNode).GetValue() : (byte[])dataNode.Value;
                        value = (bytes == null) ? string.Empty : Convert.ToBase64String(bytes);
                    }
                    else if (type == Globals.TypeOfTimeSpan)
                    {
                        value = XmlConvert.ToString(isTypedNode ? ((DataNode<TimeSpan>)dataNode).GetValue() : (TimeSpan)dataNode.Value);
                    }
                    else if (type == Globals.TypeOfGuid)
                    {
                        Guid guid = isTypedNode ? ((DataNode<Guid>)dataNode).GetValue() : (Guid)dataNode.Value;
                        value = guid.ToString();
                    }
                    else if (type == Globals.TypeOfUri)
                    {
                        Uri uri = isTypedNode ? ((DataNode<Uri>)dataNode).GetValue() : (Uri)dataNode.Value;
                        value = uri.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped);
                    }
                    else
                    {
                        handled = false;
                    }

                    break;
            }

            if (handled)
            {
                internalNodeType = ExtensionDataNodeType.Text;
            }

            return handled;
        }

        private void PushElement()
        {
            GrowElementsIfNeeded();
            elements[depth++] = element;
            if (nextElement == null)
            {
                element = GetNextElement();
            }
            else
            {
                element = nextElement;
                nextElement = null;
            }
        }

        private void PopElement()
        {
            prefix = element.prefix;
            localName = element.localName;
            ns = element.ns;

            if (depth == 0)
            {
                return;
            }

            depth--;

            if (elements != null)
            {
                element = elements[depth];
            }
        }

        private void GrowElementsIfNeeded()
        {
            if (elements == null)
            {
                elements = new ElementData[8];
            }
            else if (elements.Length == depth)
            {
                ElementData[] newElements = new ElementData[elements.Length * 2];
                Array.Copy(elements, 0, newElements, 0, elements.Length);
                elements = newElements;
            }
        }

        private ElementData GetNextElement()
        {
            int nextDepth = depth + 1;
            return (elements == null || elements.Length <= nextDepth || elements[nextDepth] == null)
                ? new ElementData() : elements[nextDepth];
        }

        internal static string GetPrefix(string ns)
        {
            ns = ns ?? string.Empty;
            if (!nsToPrefixTable.TryGetValue(ns, out string prefix))
            {
                lock (nsToPrefixTable)
                {
                    if (!nsToPrefixTable.TryGetValue(ns, out prefix))
                    {
                        prefix = (ns == null || ns.Length == 0) ? string.Empty : "p" + nsToPrefixTable.Count;
                        AddPrefix(prefix, ns);
                    }
                }
            }
            return prefix;
        }

        private static void AddPrefix(string prefix, string ns)
        {
            nsToPrefixTable.Add(ns, prefix);
            prefixToNsTable.Add(prefix, ns);
        }
    }
}
