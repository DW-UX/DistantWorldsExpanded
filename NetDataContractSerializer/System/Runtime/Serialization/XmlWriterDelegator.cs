using System;
using System.Globalization;
using System.Xml;

namespace System.Runtime.Serialization
{
    internal class XmlWriterDelegator
    {
        protected XmlWriter _writer;
        protected XmlDictionaryWriter _dictionaryWriter;
        internal int _depth;
        private int _prefixes;

        public XmlWriterDelegator(XmlWriter writer)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _dictionaryWriter = writer as XmlDictionaryWriter;
        }

        internal XmlWriter Writer => _writer;

        internal void Flush()
        {
            _writer.Flush();
        }

        internal string LookupPrefix(string ns)
        {
            return _writer.LookupPrefix(ns);
        }

        private void WriteEndAttribute()
        {
            _writer.WriteEndAttribute();
        }

        public void WriteEndElement()
        {
            _writer.WriteEndElement();
            _depth--;
        }

        internal void WriteRaw(char[] buffer, int index, int count)
        {
            _writer.WriteRaw(buffer, index, count);
        }

        internal void WriteRaw(string data)
        {
            _writer.WriteRaw(data);
        }


        internal void WriteXmlnsAttribute(XmlDictionaryString ns)
        {
            if (_dictionaryWriter != null)
            {
                if (ns != null)
                {
                    _dictionaryWriter.WriteXmlnsAttribute(null, ns);
                }
            }
            else
            {
                WriteXmlnsAttribute(ns.Value);
            }
        }

        internal void WriteXmlnsAttribute(string ns)
        {
            if (ns != null)
            {
                if (ns.Length == 0)
                {
                    _writer.WriteAttributeString("xmlns", string.Empty, null, ns);
                }
                else
                {
                    if (_dictionaryWriter != null)
                    {
                        _dictionaryWriter.WriteXmlnsAttribute(null, ns);
                    }
                    else
                    {
                        string prefix = _writer.LookupPrefix(ns);
                        if (prefix == null)
                        {
                            prefix = string.Format(CultureInfo.InvariantCulture, "d{0}p{1}", _depth, _prefixes);
                            _prefixes++;
                            _writer.WriteAttributeString("xmlns", prefix, null, ns);
                        }
                    }
                }
            }
        }

        internal void WriteXmlnsAttribute(string prefix, XmlDictionaryString ns)
        {
            if (_dictionaryWriter != null)
            {
                _dictionaryWriter.WriteXmlnsAttribute(prefix, ns);
            }
            else
            {
                _writer.WriteAttributeString("xmlns", prefix, null, ns.Value);
            }
        }

        private void WriteStartAttribute(string prefix, string localName, string ns)
        {
            _writer.WriteStartAttribute(prefix, localName, ns);
        }

        private void WriteStartAttribute(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
        {
            if (_dictionaryWriter != null)
            {
                _dictionaryWriter.WriteStartAttribute(prefix, localName, namespaceUri);
            }
            else
            {
                _writer.WriteStartAttribute(prefix,
                    (localName == null ? null : localName.Value),
                    (namespaceUri == null ? null : namespaceUri.Value));
            }
        }

        internal void WriteAttributeString(string prefix, string localName, string ns, string value)
        {
            WriteStartAttribute(prefix, localName, ns);
            WriteAttributeStringValue(value);
            WriteEndAttribute();
        }

        internal void WriteAttributeString(string prefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, string value)
        {
            WriteStartAttribute(prefix, attrName, attrNs);
            WriteAttributeStringValue(value);
            WriteEndAttribute();
        }

        private void WriteAttributeStringValue(string value)
        {
            _writer.WriteValue(value);
        }

        internal void WriteAttributeString(string prefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, XmlDictionaryString value)
        {
            WriteStartAttribute(prefix, attrName, attrNs);
            WriteAttributeStringValue(value);
            WriteEndAttribute();
        }

        private void WriteAttributeStringValue(XmlDictionaryString value)
        {
            if (_dictionaryWriter == null)
            {
                _writer.WriteString(value.Value);
            }
            else
            {
                _dictionaryWriter.WriteString(value);
            }
        }

        internal void WriteAttributeInt(string prefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, int value)
        {
            WriteStartAttribute(prefix, attrName, attrNs);
            WriteAttributeIntValue(value);
            WriteEndAttribute();
        }

        private void WriteAttributeIntValue(int value)
        {
            _writer.WriteValue(value);
        }

        internal void WriteAttributeBool(string prefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, bool value)
        {
            WriteStartAttribute(prefix, attrName, attrNs);
            WriteAttributeBoolValue(value);
            WriteEndAttribute();
        }

        private void WriteAttributeBoolValue(bool value)
        {
            _writer.WriteValue(value);
        }

        internal void WriteAttributeQualifiedName(string attrPrefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, string name, string ns)
        {
            WriteXmlnsAttribute(ns);
            WriteStartAttribute(attrPrefix, attrName, attrNs);
            WriteAttributeQualifiedNameValue(name, ns);
            WriteEndAttribute();
        }

        private void WriteAttributeQualifiedNameValue(string name, string ns)
        {
            _writer.WriteQualifiedName(name, ns);
        }

        internal void WriteAttributeQualifiedName(string attrPrefix, XmlDictionaryString attrName, XmlDictionaryString attrNs, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteXmlnsAttribute(ns);
            WriteStartAttribute(attrPrefix, attrName, attrNs);
            WriteAttributeQualifiedNameValue(name, ns);
            WriteEndAttribute();
        }

        private void WriteAttributeQualifiedNameValue(XmlDictionaryString name, XmlDictionaryString ns)
        {
            if (_dictionaryWriter == null)
            {
                _writer.WriteQualifiedName(name.Value, ns.Value);
            }
            else
            {
                _dictionaryWriter.WriteQualifiedName(name, ns);
            }
        }

        internal void WriteStartElement(string localName, string ns)
        {
            WriteStartElement(null, localName, ns);
        }

        internal virtual void WriteStartElement(string prefix, string localName, string ns)
        {
            _writer.WriteStartElement(prefix, localName, ns);
            _depth++;
            _prefixes = 1;
        }

        public void WriteStartElement(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
        {
            WriteStartElement(null, localName, namespaceUri);
        }

        internal void WriteStartElement(string prefix, XmlDictionaryString localName, XmlDictionaryString namespaceUri)
        {
            if (_dictionaryWriter != null)
            {
                _dictionaryWriter.WriteStartElement(prefix, localName, namespaceUri);
            }
            else
            {
                _writer.WriteStartElement(prefix, (localName == null ? null : localName.Value), (namespaceUri == null ? null : namespaceUri.Value));
            }

            _depth++;
            _prefixes = 1;
        }

        internal void WriteStartElementPrimitive(XmlDictionaryString localName, XmlDictionaryString namespaceUri)
        {
            if (_dictionaryWriter != null)
            {
                _dictionaryWriter.WriteStartElement(null, localName, namespaceUri);
            }
            else
            {
                _writer.WriteStartElement(null, (localName == null ? null : localName.Value), (namespaceUri == null ? null : namespaceUri.Value));
            }
        }

        internal void WriteEndElementPrimitive()
        {
            _writer.WriteEndElement();
        }

        internal WriteState WriteState => _writer.WriteState;

        internal string XmlLang => _writer.XmlLang;

        internal XmlSpace XmlSpace => _writer.XmlSpace;

        public void WriteNamespaceDecl(XmlDictionaryString ns)
        {
            WriteXmlnsAttribute(ns);
        }

        private Exception CreateInvalidPrimitiveTypeException(Type type)
        {
            return new System.Runtime.Serialization.InvalidDataContractException(SR.Format(SR.InvalidPrimitiveType, DataContract.GetClrTypeFullName(type)));
        }

        internal void WriteAnyType(object value)
        {
            WriteAnyType(value, value.GetType());
        }

        internal void WriteAnyType(object value, Type valueType)
        {
            bool handled = true;
            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Boolean:
                    WriteBoolean((bool)value);
                    break;
                case TypeCode.Char:
                    WriteChar((char)value);
                    break;
                case TypeCode.Byte:
                    WriteUnsignedByte((byte)value);
                    break;
                case TypeCode.Int16:
                    WriteShort((short)value);
                    break;
                case TypeCode.Int32:
                    WriteInt((int)value);
                    break;
                case TypeCode.Int64:
                    WriteLong((long)value);
                    break;
                case TypeCode.Single:
                    WriteFloat((float)value);
                    break;
                case TypeCode.Double:
                    WriteDouble((double)value);
                    break;
                case TypeCode.Decimal:
                    WriteDecimal((decimal)value);
                    break;
                case TypeCode.DateTime:
                    WriteDateTime((DateTime)value);
                    break;
                case TypeCode.String:
                    WriteString((string)value);
                    break;
                case TypeCode.SByte:
                    WriteSignedByte((sbyte)value);
                    break;
                case TypeCode.UInt16:
                    WriteUnsignedShort((ushort)value);
                    break;
                case TypeCode.UInt32:
                    WriteUnsignedInt((uint)value);
                    break;
                case TypeCode.UInt64:
                    WriteUnsignedLong((ulong)value);
                    break;
                case TypeCode.Empty:
                case TypeCode.DBNull:
                case TypeCode.Object:
                default:
                    if (valueType == Globals.TypeOfByteArray)
                    {
                        WriteBase64((byte[])value);
                    }
                    else if (valueType == Globals.TypeOfObject)
                    {
                        //Write Nothing
                    }
                    else if (valueType == Globals.TypeOfTimeSpan)
                    {
                        WriteTimeSpan((TimeSpan)value);
                    }
                    else if (valueType == Globals.TypeOfGuid)
                    {
                        WriteGuid((Guid)value);
                    }
                    else if (valueType == Globals.TypeOfUri)
                    {
                        WriteUri((Uri)value);
                    }
                    else if (valueType == Globals.TypeOfXmlQualifiedName)
                    {
                        WriteQName((XmlQualifiedName)value);
                    }
                    else
                    {
                        handled = false;
                    }

                    break;
            }
            if (!handled)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateInvalidPrimitiveTypeException(valueType));
            }
        }

        internal void WriteExtensionData(IDataNode dataNode)
        {
            bool handled = true;
            Type valueType = dataNode.DataType;
            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Boolean:
                    WriteBoolean(((DataNode<bool>)dataNode).GetValue());
                    break;
                case TypeCode.Char:
                    WriteChar(((DataNode<char>)dataNode).GetValue());
                    break;
                case TypeCode.Byte:
                    WriteUnsignedByte(((DataNode<byte>)dataNode).GetValue());
                    break;
                case TypeCode.Int16:
                    WriteShort(((DataNode<short>)dataNode).GetValue());
                    break;
                case TypeCode.Int32:
                    WriteInt(((DataNode<int>)dataNode).GetValue());
                    break;
                case TypeCode.Int64:
                    WriteLong(((DataNode<long>)dataNode).GetValue());
                    break;
                case TypeCode.Single:
                    WriteFloat(((DataNode<float>)dataNode).GetValue());
                    break;
                case TypeCode.Double:
                    WriteDouble(((DataNode<double>)dataNode).GetValue());
                    break;
                case TypeCode.Decimal:
                    WriteDecimal(((DataNode<decimal>)dataNode).GetValue());
                    break;
                case TypeCode.DateTime:
                    WriteDateTime(((DataNode<DateTime>)dataNode).GetValue());
                    break;
                case TypeCode.String:
                    WriteString(((DataNode<string>)dataNode).GetValue());
                    break;
                case TypeCode.SByte:
                    WriteSignedByte(((DataNode<sbyte>)dataNode).GetValue());
                    break;
                case TypeCode.UInt16:
                    WriteUnsignedShort(((DataNode<ushort>)dataNode).GetValue());
                    break;
                case TypeCode.UInt32:
                    WriteUnsignedInt(((DataNode<uint>)dataNode).GetValue());
                    break;
                case TypeCode.UInt64:
                    WriteUnsignedLong(((DataNode<ulong>)dataNode).GetValue());
                    break;
                case TypeCode.Empty:
                case TypeCode.DBNull:
                case TypeCode.Object:
                default:
                    if (valueType == Globals.TypeOfByteArray)
                    {
                        WriteBase64(((DataNode<byte[]>)dataNode).GetValue());
                    }
                    else if (valueType == Globals.TypeOfObject)
                    {
                        object obj = dataNode.Value;
                        if (obj != null)
                        {
                            WriteAnyType(obj);
                        }
                    }
                    else if (valueType == Globals.TypeOfTimeSpan)
                    {
                        WriteTimeSpan(((DataNode<TimeSpan>)dataNode).GetValue());
                    }
                    else if (valueType == Globals.TypeOfGuid)
                    {
                        WriteGuid(((DataNode<Guid>)dataNode).GetValue());
                    }
                    else if (valueType == Globals.TypeOfUri)
                    {
                        WriteUri(((DataNode<Uri>)dataNode).GetValue());
                    }
                    else if (valueType == Globals.TypeOfXmlQualifiedName)
                    {
                        WriteQName(((DataNode<XmlQualifiedName>)dataNode).GetValue());
                    }
                    else
                    {
                        handled = false;
                    }

                    break;
            }
            if (!handled)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateInvalidPrimitiveTypeException(valueType));
            }
        }

        internal void WriteString(string value)
        {
            _writer.WriteValue(value);
        }

        internal virtual void WriteBoolean(bool value)
        {
            _writer.WriteValue(value);
        }
        public void WriteBoolean(bool value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteBoolean(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteDateTime(DateTime value)
        {
            _writer.WriteValue(value);
        }

        public void WriteDateTime(DateTime value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteDateTime(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteDecimal(decimal value)
        {
            _writer.WriteValue(value);
        }
        public void WriteDecimal(decimal value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteDecimal(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteDouble(double value)
        {
            _writer.WriteValue(value);
        }
        public void WriteDouble(double value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteDouble(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteInt(int value)
        {
            _writer.WriteValue(value);
        }
        public void WriteInt(int value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteInt(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteLong(long value)
        {
            _writer.WriteValue(value);
        }
        public void WriteLong(long value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteLong(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteFloat(float value)
        {
            _writer.WriteValue(value);
        }
        public void WriteFloat(float value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteFloat(value);
            WriteEndElementPrimitive();
        }

        private const int CharChunkSize = 76;
        private const int ByteChunkSize = CharChunkSize / 4 * 3;

        internal virtual void WriteBase64(byte[] bytes)
        {
            if (bytes == null)
            {
                return;
            }

            _writer.WriteBase64(bytes, 0, bytes.Length);
        }

        internal virtual void WriteShort(short value)
        {
            _writer.WriteValue(value);
        }
        public void WriteShort(short value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteShort(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteUnsignedByte(byte value)
        {
            _writer.WriteValue(value);
        }
        public void WriteUnsignedByte(byte value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteUnsignedByte(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteSignedByte(sbyte value)
        {
            _writer.WriteValue(value);
        }
        public void WriteSignedByte(sbyte value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteSignedByte(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteUnsignedInt(uint value)
        {
            _writer.WriteValue(value);
        }
        public void WriteUnsignedInt(uint value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteUnsignedInt(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteUnsignedLong(ulong value)
        {
            _writer.WriteRaw(XmlConvert.ToString(value));
        }
        public void WriteUnsignedLong(ulong value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteUnsignedLong(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteUnsignedShort(ushort value)
        {
            _writer.WriteValue(value);
        }
        public void WriteUnsignedShort(ushort value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteUnsignedShort(value);
            WriteEndElementPrimitive();
        }

        internal virtual void WriteChar(char value)
        {
            _writer.WriteValue(value);
        }
        public void WriteChar(char value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteChar(value);
            WriteEndElementPrimitive();
        }

        internal void WriteTimeSpan(TimeSpan value)
        {
            _writer.WriteRaw(XmlConvert.ToString(value));
        }
        public void WriteTimeSpan(TimeSpan value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteTimeSpan(value);
            WriteEndElementPrimitive();
        }

        internal void WriteGuid(Guid value)
        {
            _writer.WriteRaw(value.ToString());
        }
        public void WriteGuid(Guid value, XmlDictionaryString name, XmlDictionaryString ns)
        {
            WriteStartElementPrimitive(name, ns);
            WriteGuid(value);
            WriteEndElementPrimitive();
        }

        internal void WriteUri(Uri value)
        {
            _writer.WriteString(value.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped));
        }

        internal virtual void WriteQName(XmlQualifiedName value)
        {
            if (value != XmlQualifiedName.Empty)
            {
                WriteXmlnsAttribute(value.Namespace);
                WriteQualifiedName(value.Name, value.Namespace);
            }
        }

        internal void WriteQualifiedName(string localName, string ns)
        {
            _writer.WriteQualifiedName(localName, ns);
        }

        internal void WriteQualifiedName(XmlDictionaryString localName, XmlDictionaryString ns)
        {
            if (_dictionaryWriter == null)
            {
                _writer.WriteQualifiedName(localName.Value, ns.Value);
            }
            else
            {
                _dictionaryWriter.WriteQualifiedName(localName, ns);
            }
        }

        public void WriteBooleanArray(bool[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
        {
            if (_dictionaryWriter == null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteBoolean(value[i], itemName, itemNamespace);
                }
            }
            else
            {
                _dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
            }
        }

        public void WriteDateTimeArray(DateTime[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
        {
            if (_dictionaryWriter == null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteDateTime(value[i], itemName, itemNamespace);
                }
            }
            else
            {
                _dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
            }
        }

        public void WriteDecimalArray(decimal[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
        {
            if (_dictionaryWriter == null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteDecimal(value[i], itemName, itemNamespace);
                }
            }
            else
            {
                _dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
            }
        }

        public void WriteInt32Array(int[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
        {
            if (_dictionaryWriter == null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteInt(value[i], itemName, itemNamespace);
                }
            }
            else
            {
                _dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
            }
        }

        public void WriteInt64Array(long[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
        {
            if (_dictionaryWriter == null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteLong(value[i], itemName, itemNamespace);
                }
            }
            else
            {
                _dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
            }
        }

        public void WriteSingleArray(float[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
        {
            if (_dictionaryWriter == null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteFloat(value[i], itemName, itemNamespace);
                }
            }
            else
            {
                _dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
            }
        }

        public void WriteDoubleArray(double[] value, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
        {
            if (_dictionaryWriter == null)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    WriteDouble(value[i], itemName, itemNamespace);
                }
            }
            else
            {
                _dictionaryWriter.WriteArray(null, itemName, itemNamespace, value, 0, value.Length);
            }
        }

    }
}
