using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using InvalidDataContractException = System.Runtime.Serialization.InvalidDataContractException;

namespace System.Runtime.Serialization
{
    internal class XmlReaderDelegator
    {
        private readonly XmlReader _reader;
        private readonly XmlDictionaryReader _dictionaryReader;
        private bool _isEndOfEmptyElement = false;

        public XmlReaderDelegator(XmlReader reader)
        {
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
            _dictionaryReader = reader as XmlDictionaryReader;
        }

        internal XmlReader UnderlyingReader => _reader;

        internal ExtensionDataReader UnderlyingExtensionDataReader => _reader as ExtensionDataReader;

        internal int AttributeCount => _isEndOfEmptyElement ? 0 : _reader.AttributeCount;

        internal string GetAttribute(string name)
        {
            return _isEndOfEmptyElement ? null : _reader.GetAttribute(name);
        }

        internal string GetAttribute(string name, string namespaceUri)
        {
            return _isEndOfEmptyElement ? null : _reader.GetAttribute(name, namespaceUri);
        }

        internal string GetAttribute(int i)
        {
            if (_isEndOfEmptyElement)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException(nameof(i), SR.XmlElementAttributes));
            }

            return _reader.GetAttribute(i);
        }

        internal bool IsEmptyElement => false;

        internal bool IsNamespaceURI(string ns)
        {
            if (_dictionaryReader == null)
            {
                return ns == _reader.NamespaceURI;
            }
            else
            {
                return _dictionaryReader.IsNamespaceUri(ns);
            }
        }

        internal bool IsLocalName(string localName)
        {
            if (_dictionaryReader == null)
            {
                return localName == _reader.LocalName;
            }
            else
            {
                return _dictionaryReader.IsLocalName(localName);
            }
        }

        internal bool IsNamespaceUri(XmlDictionaryString ns)
        {
            if (_dictionaryReader == null)
            {
                return ns.Value == _reader.NamespaceURI;
            }
            else
            {
                return _dictionaryReader.IsNamespaceUri(ns);
            }
        }

        internal bool IsLocalName(XmlDictionaryString localName)
        {
            if (_dictionaryReader == null)
            {
                return localName.Value == _reader.LocalName;
            }
            else
            {
                return _dictionaryReader.IsLocalName(localName);
            }
        }

        internal int IndexOfLocalName(XmlDictionaryString[] localNames, XmlDictionaryString ns)
        {
            if (_dictionaryReader != null)
            {
                return _dictionaryReader.IndexOfLocalName(localNames, ns);
            }

            if (_reader.NamespaceURI == ns.Value)
            {
                string localName = LocalName;
                for (int i = 0; i < localNames.Length; i++)
                {
                    if (localName == localNames[i].Value)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public bool IsStartElement()
        {
            return !_isEndOfEmptyElement && _reader.IsStartElement();
        }

        internal bool IsStartElement(string localname, string ns)
        {
            return !_isEndOfEmptyElement && _reader.IsStartElement(localname, ns);
        }

        public bool IsStartElement(XmlDictionaryString localname, XmlDictionaryString ns)
        {
            if (_dictionaryReader == null)
            {
                return !_isEndOfEmptyElement && _reader.IsStartElement(localname.Value, ns.Value);
            }
            else
            {
                return !_isEndOfEmptyElement && _dictionaryReader.IsStartElement(localname, ns);
            }
        }

        internal bool MoveToAttribute(string name)
        {
            return _isEndOfEmptyElement ? false : _reader.MoveToAttribute(name);
        }

        internal bool MoveToAttribute(string name, string ns)
        {
            return _isEndOfEmptyElement ? false : _reader.MoveToAttribute(name, ns);
        }

        internal void MoveToAttribute(int i)
        {
            if (_isEndOfEmptyElement)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException(nameof(i), SR.XmlElementAttributes));
            }

            _reader.MoveToAttribute(i);
        }

        internal bool MoveToElement()
        {
            return _isEndOfEmptyElement ? false : _reader.MoveToElement();
        }

        internal bool MoveToFirstAttribute()
        {
            return _isEndOfEmptyElement ? false : _reader.MoveToFirstAttribute();
        }

        internal bool MoveToNextAttribute()
        {
            return _isEndOfEmptyElement ? false : _reader.MoveToNextAttribute();
        }

        public XmlNodeType NodeType => _isEndOfEmptyElement ? XmlNodeType.EndElement : _reader.NodeType;

        internal bool Read()
        {
            _reader.MoveToElement();
            if (!_reader.IsEmptyElement)
            {
                return _reader.Read();
            }

            if (_isEndOfEmptyElement)
            {
                _isEndOfEmptyElement = false;
                return _reader.Read();
            }
            _isEndOfEmptyElement = true;
            return true;
        }

        internal XmlNodeType MoveToContent()
        {
            if (_isEndOfEmptyElement)
            {
                return XmlNodeType.EndElement;
            }

            return _reader.MoveToContent();
        }

        internal bool ReadAttributeValue()
        {
            return _isEndOfEmptyElement ? false : _reader.ReadAttributeValue();
        }

        public void ReadEndElement()
        {
            if (_isEndOfEmptyElement)
            {
                Read();
            }
            else
            {
                _reader.ReadEndElement();
            }
        }

        private Exception CreateInvalidPrimitiveTypeException(Type type)
        {
            return new InvalidDataContractException(SR.Format(
                type.IsInterface ? SR.InterfaceTypeCannotBeCreated : SR.InvalidPrimitiveType,
                DataContract.GetClrTypeFullName(type)));
        }

        public object ReadElementContentAsAnyType(Type valueType)
        {
            Read();
            object o = ReadContentAsAnyType(valueType);
            ReadEndElement();
            return o;
        }

        internal object ReadContentAsAnyType(Type valueType)
        {
            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Boolean:
                    return ReadContentAsBoolean();
                case TypeCode.Char:
                    return ReadContentAsChar();
                case TypeCode.Byte:
                    return ReadContentAsUnsignedByte();
                case TypeCode.Int16:
                    return ReadContentAsShort();
                case TypeCode.Int32:
                    return ReadContentAsInt();
                case TypeCode.Int64:
                    return ReadContentAsLong();
                case TypeCode.Single:
                    return ReadContentAsSingle();
                case TypeCode.Double:
                    return ReadContentAsDouble();
                case TypeCode.Decimal:
                    return ReadContentAsDecimal();
                case TypeCode.DateTime:
                    return ReadContentAsDateTime();
                case TypeCode.String:
                    return ReadContentAsString();

                case TypeCode.SByte:
                    return ReadContentAsSignedByte();
                case TypeCode.UInt16:
                    return ReadContentAsUnsignedShort();
                case TypeCode.UInt32:
                    return ReadContentAsUnsignedInt();
                case TypeCode.UInt64:
                    return ReadContentAsUnsignedLong();
                case TypeCode.Empty:
                case TypeCode.DBNull:
                case TypeCode.Object:
                default:
                    if (valueType == Globals.TypeOfByteArray)
                    {
                        return ReadContentAsBase64();
                    }
                    else if (valueType == Globals.TypeOfObject)
                    {
                        return new object();
                    }
                    else if (valueType == Globals.TypeOfTimeSpan)
                    {
                        return ReadContentAsTimeSpan();
                    }
                    else if (valueType == Globals.TypeOfGuid)
                    {
                        return ReadContentAsGuid();
                    }
                    else if (valueType == Globals.TypeOfUri)
                    {
                        return ReadContentAsUri();
                    }
                    else if (valueType == Globals.TypeOfXmlQualifiedName)
                    {
                        return ReadContentAsQName();
                    }

                    break;
            }
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateInvalidPrimitiveTypeException(valueType));
        }

        internal IDataNode ReadExtensionData(Type valueType)
        {
            switch (Type.GetTypeCode(valueType))
            {
                case TypeCode.Boolean:
                    return new DataNode<bool>(ReadContentAsBoolean());
                case TypeCode.Char:
                    return new DataNode<char>(ReadContentAsChar());
                case TypeCode.Byte:
                    return new DataNode<byte>(ReadContentAsUnsignedByte());
                case TypeCode.Int16:
                    return new DataNode<short>(ReadContentAsShort());
                case TypeCode.Int32:
                    return new DataNode<int>(ReadContentAsInt());
                case TypeCode.Int64:
                    return new DataNode<long>(ReadContentAsLong());
                case TypeCode.Single:
                    return new DataNode<float>(ReadContentAsSingle());
                case TypeCode.Double:
                    return new DataNode<double>(ReadContentAsDouble());
                case TypeCode.Decimal:
                    return new DataNode<decimal>(ReadContentAsDecimal());
                case TypeCode.DateTime:
                    return new DataNode<DateTime>(ReadContentAsDateTime());
                case TypeCode.String:
                    return new DataNode<string>(ReadContentAsString());
                case TypeCode.SByte:
                    return new DataNode<sbyte>(ReadContentAsSignedByte());
                case TypeCode.UInt16:
                    return new DataNode<ushort>(ReadContentAsUnsignedShort());
                case TypeCode.UInt32:
                    return new DataNode<uint>(ReadContentAsUnsignedInt());
                case TypeCode.UInt64:
                    return new DataNode<ulong>(ReadContentAsUnsignedLong());
                case TypeCode.Empty:
                case TypeCode.DBNull:
                case TypeCode.Object:
                default:
                    if (valueType == Globals.TypeOfByteArray)
                    {
                        return new DataNode<byte[]>(ReadContentAsBase64());
                    }
                    else if (valueType == Globals.TypeOfObject)
                    {
                        return new DataNode<object>(new object());
                    }
                    else if (valueType == Globals.TypeOfTimeSpan)
                    {
                        return new DataNode<TimeSpan>(ReadContentAsTimeSpan());
                    }
                    else if (valueType == Globals.TypeOfGuid)
                    {
                        return new DataNode<Guid>(ReadContentAsGuid());
                    }
                    else if (valueType == Globals.TypeOfUri)
                    {
                        return new DataNode<Uri>(ReadContentAsUri());
                    }
                    else if (valueType == Globals.TypeOfXmlQualifiedName)
                    {
                        return new DataNode<XmlQualifiedName>(ReadContentAsQName());
                    }

                    break;
            }
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateInvalidPrimitiveTypeException(valueType));
        }

        private void ThrowConversionException(string value, string type)
        {
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(XmlObjectSerializer2.TryAddLineInfo(this, SR.Format(SR.XmlInvalidConversion, value, type))));
        }

        private void ThrowNotAtElement()
        {
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.Format(SR.XmlStartElementExpected, "EndElement")));
        }

        internal virtual char ReadElementContentAsChar()
        {
            return ToChar(ReadElementContentAsInt());
        }

        internal virtual char ReadContentAsChar()
        {
            return ToChar(ReadContentAsInt());
        }

        private char ToChar(int value)
        {
            if (value < char.MinValue || value > char.MaxValue)
            {
                ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "Char");
            }
            return (char)value;
        }

        public string ReadElementContentAsString()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            return _reader.ReadElementContentAsString();
        }

        internal string ReadContentAsString()
        {
            return _isEndOfEmptyElement ? string.Empty : _reader.ReadContentAsString();
        }

        public bool ReadElementContentAsBoolean()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            return _reader.ReadElementContentAsBoolean();
        }

        internal bool ReadContentAsBoolean()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowConversionException(string.Empty, "Boolean");
            }

            return _reader.ReadContentAsBoolean();
        }

        public float ReadElementContentAsFloat()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            return _reader.ReadElementContentAsFloat();
        }

        internal float ReadContentAsSingle()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowConversionException(string.Empty, "Float");
            }

            return _reader.ReadContentAsFloat();
        }

        public double ReadElementContentAsDouble()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            return _reader.ReadElementContentAsDouble();
        }

        internal double ReadContentAsDouble()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowConversionException(string.Empty, "Double");
            }

            return _reader.ReadContentAsDouble();
        }

        public decimal ReadElementContentAsDecimal()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            return _reader.ReadElementContentAsDecimal();
        }

        internal decimal ReadContentAsDecimal()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowConversionException(string.Empty, "Decimal");
            }

            return _reader.ReadContentAsDecimal();
        }

        internal virtual byte[] ReadElementContentAsBase64()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            if (_dictionaryReader == null)
            {
                return ReadContentAsBase64(_reader.ReadElementContentAsString());
            }
            else
            {
                return _dictionaryReader.ReadElementContentAsBase64();
            }
        }

        internal virtual byte[] ReadContentAsBase64()
        {
            if (_isEndOfEmptyElement)
            {
                return new byte[0];
            }

            if (_dictionaryReader == null)
            {
                return ReadContentAsBase64(_reader.ReadContentAsString());
            }
            else
            {
                return _dictionaryReader.ReadContentAsBase64();
            }
        }

        internal byte[] ReadContentAsBase64(string str)
        {
            if (str == null)
            {
                return null;
            }

            str = str.Trim();
            if (str.Length == 0)
            {
                return new byte[0];
            }

            try
            {
                return Convert.FromBase64String(str);
            }
            catch (ArgumentException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "byte[]", exception));
            }
            catch (FormatException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "byte[]", exception));
            }
        }

        internal virtual DateTime ReadElementContentAsDateTime()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            return _reader.ReadElementContentAsDateTime();
        }

        internal virtual DateTime ReadContentAsDateTime()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowConversionException(string.Empty, "DateTime");
            }

            return _reader.ReadContentAsDateTime();
        }

        public int ReadElementContentAsInt()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            return _reader.ReadElementContentAsInt();
        }

        internal int ReadContentAsInt()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowConversionException(string.Empty, "Int32");
            }

            return _reader.ReadContentAsInt();
        }

        public long ReadElementContentAsLong()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            return _reader.ReadElementContentAsLong();
        }

        internal long ReadContentAsLong()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowConversionException(string.Empty, "Int64");
            }

            return _reader.ReadContentAsLong();
        }

        public short ReadElementContentAsShort()
        {
            return ToShort(ReadElementContentAsInt());
        }

        internal short ReadContentAsShort()
        {
            return ToShort(ReadContentAsInt());
        }

        private short ToShort(int value)
        {
            if (value < short.MinValue || value > short.MaxValue)
            {
                ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "Int16");
            }
            return (short)value;
        }

        public byte ReadElementContentAsUnsignedByte()
        {
            return ToByte(ReadElementContentAsInt());
        }

        internal byte ReadContentAsUnsignedByte()
        {
            return ToByte(ReadContentAsInt());
        }

        private byte ToByte(int value)
        {
            if (value < byte.MinValue || value > byte.MaxValue)
            {
                ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "Byte");
            }
            return (byte)value;
        }

        public sbyte ReadElementContentAsSignedByte()
        {
            return ToSByte(ReadElementContentAsInt());
        }

        internal sbyte ReadContentAsSignedByte()
        {
            return ToSByte(ReadContentAsInt());
        }

        private sbyte ToSByte(int value)
        {
            if (value < sbyte.MinValue || value > sbyte.MaxValue)
            {
                ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "SByte");
            }
            return (sbyte)value;
        }

        public uint ReadElementContentAsUnsignedInt()
        {
            return ToUInt32(ReadElementContentAsLong());
        }

        internal uint ReadContentAsUnsignedInt()
        {
            return ToUInt32(ReadContentAsLong());
        }

        private uint ToUInt32(long value)
        {
            if (value < uint.MinValue || value > uint.MaxValue)
            {
                ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "UInt32");
            }
            return (uint)value;
        }

        internal virtual ulong ReadElementContentAsUnsignedLong()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            string str = _reader.ReadElementContentAsString();

            if (str == null || str.Length == 0)
            {
                ThrowConversionException(string.Empty, "UInt64");
            }

            return XmlConverter.ToUInt64(str);
        }

        internal virtual ulong ReadContentAsUnsignedLong()
        {
            string str = _reader.ReadContentAsString();

            if (str == null || str.Length == 0)
            {
                ThrowConversionException(string.Empty, "UInt64");
            }

            return XmlConverter.ToUInt64(str);
        }

        public ushort ReadElementContentAsUnsignedShort()
        {
            return ToUInt16(ReadElementContentAsInt());
        }

        internal ushort ReadContentAsUnsignedShort()
        {
            return ToUInt16(ReadContentAsInt());
        }

        private ushort ToUInt16(int value)
        {
            if (value < ushort.MinValue || value > ushort.MaxValue)
            {
                ThrowConversionException(value.ToString(NumberFormatInfo.CurrentInfo), "UInt16");
            }
            return (ushort)value;
        }

        public TimeSpan ReadElementContentAsTimeSpan()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            string str = _reader.ReadElementContentAsString();
            return XmlConverter.ToTimeSpan(str);
        }

        internal TimeSpan ReadContentAsTimeSpan()
        {
            string str = _reader.ReadContentAsString();
            return XmlConverter.ToTimeSpan(str);
        }

        public Guid ReadElementContentAsGuid()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            string str = _reader.ReadElementContentAsString();
            try
            {
                return Guid.Parse(str);
            }
            catch (ArgumentException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "Guid", exception));
            }
            catch (FormatException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "Guid", exception));
            }
            catch (OverflowException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "Guid", exception));
            }
        }

        internal Guid ReadContentAsGuid()
        {
            string str = _reader.ReadContentAsString();
            try
            {
                return Guid.Parse(str);
            }
            catch (ArgumentException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "Guid", exception));
            }
            catch (FormatException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "Guid", exception));
            }
            catch (OverflowException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "Guid", exception));
            }
        }

        public Uri ReadElementContentAsUri()
        {
            if (_isEndOfEmptyElement)
            {
                ThrowNotAtElement();
            }

            string str = ReadElementContentAsString();
            try
            {
                return new Uri(str, UriKind.RelativeOrAbsolute);
            }
            catch (ArgumentException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "Uri", exception));
            }
            catch (FormatException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "Uri", exception));
            }
        }

        internal Uri ReadContentAsUri()
        {
            string str = ReadContentAsString();
            try
            {
                return new Uri(str, UriKind.RelativeOrAbsolute);
            }
            catch (ArgumentException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "Uri", exception));
            }
            catch (FormatException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(str, "Uri", exception));
            }
        }

        public XmlQualifiedName ReadElementContentAsQName()
        {
            Read();
            XmlQualifiedName obj = ReadContentAsQName();
            ReadEndElement();
            return obj;
        }

        internal virtual XmlQualifiedName ReadContentAsQName()
        {
            return ParseQualifiedName(ReadContentAsString());
        }

        private XmlQualifiedName ParseQualifiedName(string str)
        {
            string name, ns;
            if (str == null || str.Length == 0)
            {
                name = ns = string.Empty;
            }
            else
            {
                XmlObjectSerializerReadContext.ParseQualifiedName(str, this, out name, out ns, out string prefix);
            }

            return new XmlQualifiedName(name, ns);
        }

        private void CheckExpectedArrayLength(XmlObjectSerializerReadContext context, int arrayLength)
        {
            context.IncrementItemCount(arrayLength);
        }

        protected int GetArrayLengthQuota(XmlObjectSerializerReadContext context)
        {
            if (_dictionaryReader.Quotas == null)
            {
                return context.RemainingItemCount;
            }

            return Math.Min(context.RemainingItemCount, _dictionaryReader.Quotas.MaxArrayLength);
        }

        private void CheckActualArrayLength(int expectedLength, int actualLength, XmlDictionaryString itemName, XmlDictionaryString itemNamespace)
        {
            if (expectedLength != actualLength)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.ArrayExceededSizeAttribute, expectedLength, itemName.Value, itemNamespace.Value)));
            }
        }

        internal bool TryReadBooleanArray(XmlObjectSerializerReadContext context,
            XmlDictionaryString itemName, XmlDictionaryString itemNamespace,
            int arrayLength, out bool[] array)
        {
            if (_dictionaryReader == null)
            {
                array = null;
                return false;
            }

            if (arrayLength != -1)
            {
                CheckExpectedArrayLength(context, arrayLength);
                array = new bool[arrayLength];
                int read = 0, offset = 0;
                while ((read = _dictionaryReader.ReadArray(itemName, itemNamespace, array, offset, arrayLength - offset)) > 0)
                {
                    offset += read;
                }
                CheckActualArrayLength(arrayLength, offset, itemName, itemNamespace);
            }
            else
            {
                array = BooleanArrayHelperWithDictionaryString.Instance.ReadArray(
                    _dictionaryReader, itemName, itemNamespace, GetArrayLengthQuota(context));
                context.IncrementItemCount(array.Length);
            }
            return true;
        }

        internal bool TryReadDateTimeArray(XmlObjectSerializerReadContext context,
            XmlDictionaryString itemName, XmlDictionaryString itemNamespace,
            int arrayLength, out DateTime[] array)
        {
            if (_dictionaryReader == null)
            {
                array = null;
                return false;
            }

            if (arrayLength != -1)
            {
                CheckExpectedArrayLength(context, arrayLength);
                array = new DateTime[arrayLength];
                int read = 0, offset = 0;
                while ((read = _dictionaryReader.ReadArray(itemName, itemNamespace, array, offset, arrayLength - offset)) > 0)
                {
                    offset += read;
                }
                CheckActualArrayLength(arrayLength, offset, itemName, itemNamespace);
            }
            else
            {
                array = DateTimeArrayHelperWithDictionaryString.Instance.ReadArray(
                    _dictionaryReader, itemName, itemNamespace, GetArrayLengthQuota(context));
                context.IncrementItemCount(array.Length);
            }
            return true;
        }

        internal bool TryReadDecimalArray(XmlObjectSerializerReadContext context,
            XmlDictionaryString itemName, XmlDictionaryString itemNamespace,
            int arrayLength, out decimal[] array)
        {
            if (_dictionaryReader == null)
            {
                array = null;
                return false;
            }

            if (arrayLength != -1)
            {
                CheckExpectedArrayLength(context, arrayLength);
                array = new decimal[arrayLength];
                int read = 0, offset = 0;
                while ((read = _dictionaryReader.ReadArray(itemName, itemNamespace, array, offset, arrayLength - offset)) > 0)
                {
                    offset += read;
                }
                CheckActualArrayLength(arrayLength, offset, itemName, itemNamespace);
            }
            else
            {
                array = DecimalArrayHelperWithDictionaryString.Instance.ReadArray(
                    _dictionaryReader, itemName, itemNamespace, GetArrayLengthQuota(context));
                context.IncrementItemCount(array.Length);
            }
            return true;
        }

        internal bool TryReadInt32Array(XmlObjectSerializerReadContext context,
            XmlDictionaryString itemName, XmlDictionaryString itemNamespace,
            int arrayLength, out int[] array)
        {
            if (_dictionaryReader == null)
            {
                array = null;
                return false;
            }

            if (arrayLength != -1)
            {
                CheckExpectedArrayLength(context, arrayLength);
                array = new int[arrayLength];
                int read = 0, offset = 0;
                while ((read = _dictionaryReader.ReadArray(itemName, itemNamespace, array, offset, arrayLength - offset)) > 0)
                {
                    offset += read;
                }
                CheckActualArrayLength(arrayLength, offset, itemName, itemNamespace);
            }
            else
            {
                array = Int32ArrayHelperWithDictionaryString.Instance.ReadArray(
                    _dictionaryReader, itemName, itemNamespace, GetArrayLengthQuota(context));
                context.IncrementItemCount(array.Length);
            }
            return true;
        }

        internal bool TryReadInt64Array(XmlObjectSerializerReadContext context,
            XmlDictionaryString itemName, XmlDictionaryString itemNamespace,
            int arrayLength, out long[] array)
        {
            if (_dictionaryReader == null)
            {
                array = null;
                return false;
            }

            if (arrayLength != -1)
            {
                CheckExpectedArrayLength(context, arrayLength);
                array = new long[arrayLength];
                int read = 0, offset = 0;
                while ((read = _dictionaryReader.ReadArray(itemName, itemNamespace, array, offset, arrayLength - offset)) > 0)
                {
                    offset += read;
                }
                CheckActualArrayLength(arrayLength, offset, itemName, itemNamespace);
            }
            else
            {
                array = Int64ArrayHelperWithDictionaryString.Instance.ReadArray(
                    _dictionaryReader, itemName, itemNamespace, GetArrayLengthQuota(context));
                context.IncrementItemCount(array.Length);
            }
            return true;
        }

        internal bool TryReadSingleArray(XmlObjectSerializerReadContext context,
            XmlDictionaryString itemName, XmlDictionaryString itemNamespace,
            int arrayLength, out float[] array)
        {
            if (_dictionaryReader == null)
            {
                array = null;
                return false;
            }

            if (arrayLength != -1)
            {
                CheckExpectedArrayLength(context, arrayLength);
                array = new float[arrayLength];
                int read = 0, offset = 0;
                while ((read = _dictionaryReader.ReadArray(itemName, itemNamespace, array, offset, arrayLength - offset)) > 0)
                {
                    offset += read;
                }
                CheckActualArrayLength(arrayLength, offset, itemName, itemNamespace);
            }
            else
            {
                array = SingleArrayHelperWithDictionaryString.Instance.ReadArray(
                    _dictionaryReader, itemName, itemNamespace, GetArrayLengthQuota(context));
                context.IncrementItemCount(array.Length);
            }
            return true;
        }

        internal bool TryReadDoubleArray(XmlObjectSerializerReadContext context,
            XmlDictionaryString itemName, XmlDictionaryString itemNamespace,
            int arrayLength, out double[] array)
        {
            if (_dictionaryReader == null)
            {
                array = null;
                return false;
            }

            if (arrayLength != -1)
            {
                CheckExpectedArrayLength(context, arrayLength);
                array = new double[arrayLength];
                int read = 0, offset = 0;
                while ((read = _dictionaryReader.ReadArray(itemName, itemNamespace, array, offset, arrayLength - offset)) > 0)
                {
                    offset += read;
                }
                CheckActualArrayLength(arrayLength, offset, itemName, itemNamespace);
            }
            else
            {
                array = DoubleArrayHelperWithDictionaryString.Instance.ReadArray(
                    _dictionaryReader, itemName, itemNamespace, GetArrayLengthQuota(context));
                context.IncrementItemCount(array.Length);
            }
            return true;
        }

        internal IDictionary<string, string> GetNamespacesInScope(XmlNamespaceScope scope)
        {
            return (_reader is IXmlNamespaceResolver) ? ((IXmlNamespaceResolver)_reader).GetNamespacesInScope(scope) : null;
        }

        // IXmlLineInfo members
        internal bool HasLineInfo()
        {
            IXmlLineInfo iXmlLineInfo = _reader as IXmlLineInfo;
            return (iXmlLineInfo == null) ? false : iXmlLineInfo.HasLineInfo();
        }

        internal int LineNumber
        {
            get
            {
                IXmlLineInfo iXmlLineInfo = _reader as IXmlLineInfo;
                return (iXmlLineInfo == null) ? 0 : iXmlLineInfo.LineNumber;
            }
        }

        internal int LinePosition
        {
            get
            {
                IXmlLineInfo iXmlLineInfo = _reader as IXmlLineInfo;
                return (iXmlLineInfo == null) ? 0 : iXmlLineInfo.LinePosition;
            }
        }

        // IXmlTextParser members
        internal bool Normalized
        {
            get
            {
                XmlTextReader xmlTextReader = _reader as XmlTextReader;
                if (xmlTextReader == null)
                {
                    IXmlTextParser xmlTextParser = _reader as IXmlTextParser;
                    return (xmlTextParser == null) ? false : xmlTextParser.Normalized;
                }
                else
                {
                    return xmlTextReader.Normalization;
                }
            }
            set
            {
                XmlTextReader xmlTextReader = _reader as XmlTextReader;
                if (xmlTextReader == null)
                {
                    IXmlTextParser xmlTextParser = _reader as IXmlTextParser;
                    if (xmlTextParser != null)
                    {
                        xmlTextParser.Normalized = value;
                    }
                }
                else
                {
                    xmlTextReader.Normalization = value;
                }
            }
        }

        internal WhitespaceHandling WhitespaceHandling
        {
            get
            {
                XmlTextReader xmlTextReader = _reader as XmlTextReader;
                if (xmlTextReader == null)
                {
                    IXmlTextParser xmlTextParser = _reader as IXmlTextParser;
                    return (xmlTextParser == null) ? WhitespaceHandling.None : xmlTextParser.WhitespaceHandling;
                }
                else
                {
                    return xmlTextReader.WhitespaceHandling;
                }
            }
            set
            {
                XmlTextReader xmlTextReader = _reader as XmlTextReader;
                if (xmlTextReader == null)
                {
                    IXmlTextParser xmlTextParser = _reader as IXmlTextParser;
                    if (xmlTextParser != null)
                    {
                        xmlTextParser.WhitespaceHandling = value;
                    }
                }
                else
                {
                    xmlTextReader.WhitespaceHandling = value;
                }
            }
        }

        // delegating properties and methods
        internal string Name => _reader.Name;

        public string LocalName => _reader.LocalName;

        internal string NamespaceURI => _reader.NamespaceURI;
        internal string Value => _reader.Value;
        internal Type ValueType => _reader.ValueType;
        internal int Depth => _reader.Depth;
        internal string LookupNamespace(string prefix) { return _reader.LookupNamespace(prefix); }
        internal bool EOF => _reader.EOF;

        internal void Skip()
        {
            _reader.Skip();
            _isEndOfEmptyElement = false;
        }
    }
}
