using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
    internal class XmlSerializableReader : XmlReader, IXmlLineInfo, IXmlTextParser
    {
        private XmlReaderDelegator xmlReader;
        private int startDepth;
        private bool isRootEmptyElement;
        private XmlReader innerReader;

        private XmlReader InnerReader => innerReader;

        internal void BeginRead(XmlReaderDelegator xmlReader)
        {
            if (xmlReader.NodeType != XmlNodeType.Element)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.Element, xmlReader));
            }

            this.xmlReader = xmlReader;
            startDepth = xmlReader.Depth;
            innerReader = xmlReader.UnderlyingReader;
            isRootEmptyElement = InnerReader.IsEmptyElement;
        }

        internal void EndRead()
        {
            if (isRootEmptyElement)
            {
                xmlReader.Read();
            }
            else
            {
                if (xmlReader.IsStartElement() && xmlReader.Depth == startDepth)
                {
                    xmlReader.Read();
                }

                while (xmlReader.Depth > startDepth)
                {
                    if (!xmlReader.Read())
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializerReadContext.CreateUnexpectedStateException(XmlNodeType.EndElement, xmlReader));
                    }
                }
            }
        }

        public override bool Read()
        {
            XmlReader reader = InnerReader;
            if (reader.Depth == startDepth)
            {
                if (reader.NodeType == XmlNodeType.EndElement ||
                     (reader.NodeType == XmlNodeType.Element && reader.IsEmptyElement))
                {
                    return false;
                }
            }
            return reader.Read();
        }

        public override void Close()
        {
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.IXmlSerializableIllegalOperation));
        }

        public override XmlReaderSettings Settings => InnerReader.Settings;
        public override XmlNodeType NodeType => InnerReader.NodeType;
        public override string Name => InnerReader.Name;
        public override string LocalName => InnerReader.LocalName;
        public override string NamespaceURI => InnerReader.NamespaceURI;
        public override string Prefix => InnerReader.Prefix;
        public override bool HasValue => InnerReader.HasValue;
        public override string Value => InnerReader.Value;
        public override int Depth => InnerReader.Depth;
        public override string BaseURI => InnerReader.BaseURI;
        public override bool IsEmptyElement => InnerReader.IsEmptyElement;
        public override bool IsDefault => InnerReader.IsDefault;
        public override char QuoteChar => InnerReader.QuoteChar;
        public override XmlSpace XmlSpace => InnerReader.XmlSpace;
        public override string XmlLang => InnerReader.XmlLang;
        public override IXmlSchemaInfo SchemaInfo => InnerReader.SchemaInfo;
        public override Type ValueType => InnerReader.ValueType;
        public override int AttributeCount => InnerReader.AttributeCount;
        public override string this[int i] => InnerReader[i];
        public override string this[string name] => InnerReader[name];
        public override string this[string name, string namespaceURI] => InnerReader[name, namespaceURI];
        public override bool EOF => InnerReader.EOF;
        public override ReadState ReadState => InnerReader.ReadState;
        public override XmlNameTable NameTable => InnerReader.NameTable;
        public override bool CanResolveEntity => InnerReader.CanResolveEntity;
        public override bool CanReadBinaryContent => InnerReader.CanReadBinaryContent;
        public override bool CanReadValueChunk => InnerReader.CanReadValueChunk;
        public override bool HasAttributes => InnerReader.HasAttributes;

        public override string GetAttribute(string name) { return InnerReader.GetAttribute(name); }
        public override string GetAttribute(string name, string namespaceURI) { return InnerReader.GetAttribute(name, namespaceURI); }
        public override string GetAttribute(int i) { return InnerReader.GetAttribute(i); }
        public override bool MoveToAttribute(string name) { return InnerReader.MoveToAttribute(name); }
        public override bool MoveToAttribute(string name, string ns) { return InnerReader.MoveToAttribute(name, ns); }
        public override void MoveToAttribute(int i) { InnerReader.MoveToAttribute(i); }
        public override bool MoveToFirstAttribute() { return InnerReader.MoveToFirstAttribute(); }
        public override bool MoveToNextAttribute() { return InnerReader.MoveToNextAttribute(); }
        public override bool MoveToElement() { return InnerReader.MoveToElement(); }
        public override string LookupNamespace(string prefix) { return InnerReader.LookupNamespace(prefix); }
        public override bool ReadAttributeValue() { return InnerReader.ReadAttributeValue(); }
        public override void ResolveEntity() { InnerReader.ResolveEntity(); }
        public override bool IsStartElement() { return InnerReader.IsStartElement(); }
        public override bool IsStartElement(string name) { return InnerReader.IsStartElement(name); }
        public override bool IsStartElement(string localname, string ns) { return InnerReader.IsStartElement(localname, ns); }
        public override XmlNodeType MoveToContent() { return InnerReader.MoveToContent(); }

        public override object ReadContentAsObject() { return InnerReader.ReadContentAsObject(); }
        public override bool ReadContentAsBoolean() { return InnerReader.ReadContentAsBoolean(); }
        public override DateTime ReadContentAsDateTime() { return InnerReader.ReadContentAsDateTime(); }
        public override double ReadContentAsDouble() { return InnerReader.ReadContentAsDouble(); }
        public override int ReadContentAsInt() { return InnerReader.ReadContentAsInt(); }
        public override long ReadContentAsLong() { return InnerReader.ReadContentAsLong(); }
        public override string ReadContentAsString() { return InnerReader.ReadContentAsString(); }
        public override object ReadContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver) { return InnerReader.ReadContentAs(returnType, namespaceResolver); }
        public override int ReadContentAsBase64(byte[] buffer, int index, int count) { return InnerReader.ReadContentAsBase64(buffer, index, count); }
        public override int ReadContentAsBinHex(byte[] buffer, int index, int count) { return InnerReader.ReadContentAsBinHex(buffer, index, count); }
        public override int ReadValueChunk(char[] buffer, int index, int count) { return InnerReader.ReadValueChunk(buffer, index, count); }
        public override string ReadString() { return InnerReader.ReadString(); }

        // IXmlTextParser members
        bool IXmlTextParser.Normalized
        {
            get
            {
                IXmlTextParser xmlTextParser = InnerReader as IXmlTextParser;
                return (xmlTextParser == null) ? xmlReader.Normalized : xmlTextParser.Normalized;
            }
            set
            {
                IXmlTextParser xmlTextParser = InnerReader as IXmlTextParser;
                if (xmlTextParser == null)
                {
                    xmlReader.Normalized = value;
                }
                else
                {
                    xmlTextParser.Normalized = value;
                }
            }
        }

        WhitespaceHandling IXmlTextParser.WhitespaceHandling
        {
            get
            {
                IXmlTextParser xmlTextParser = InnerReader as IXmlTextParser;
                return (xmlTextParser == null) ? xmlReader.WhitespaceHandling : xmlTextParser.WhitespaceHandling;
            }
            set
            {
                IXmlTextParser xmlTextParser = InnerReader as IXmlTextParser;
                if (xmlTextParser == null)
                {
                    xmlReader.WhitespaceHandling = value;
                }
                else
                {
                    xmlTextParser.WhitespaceHandling = value;
                }
            }
        }

        // IXmlLineInfo members
        bool IXmlLineInfo.HasLineInfo()
        {
            IXmlLineInfo xmlLineInfo = InnerReader as IXmlLineInfo;
            return (xmlLineInfo == null) ? xmlReader.HasLineInfo() : xmlLineInfo.HasLineInfo();
        }

        int IXmlLineInfo.LineNumber
        {
            get
            {
                IXmlLineInfo xmlLineInfo = InnerReader as IXmlLineInfo;
                return (xmlLineInfo == null) ? xmlReader.LineNumber : xmlLineInfo.LineNumber;
            }
        }

        int IXmlLineInfo.LinePosition
        {
            get
            {
                IXmlLineInfo xmlLineInfo = InnerReader as IXmlLineInfo;
                return (xmlLineInfo == null) ? xmlReader.LinePosition : xmlLineInfo.LinePosition;
            }
        }
    }
}
