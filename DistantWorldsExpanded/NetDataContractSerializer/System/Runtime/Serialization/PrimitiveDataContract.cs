using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace System.Runtime.Serialization
{
    internal abstract class PrimitiveDataContract : DataContract
    {
        private readonly PrimitiveDataContractCriticalHelper _helper;

        protected PrimitiveDataContract(Type type, XmlDictionaryString name, XmlDictionaryString ns)
            : base(new PrimitiveDataContractCriticalHelper(type, name, ns))
        {
            _helper = base.Helper as PrimitiveDataContractCriticalHelper;
        }

        internal static PrimitiveDataContract GetPrimitiveDataContract(Type type)
        {
            return DataContract.GetBuiltInDataContract(type) as PrimitiveDataContract;
        }

        internal static PrimitiveDataContract GetPrimitiveDataContract(string name, string ns)
        {
            return DataContract.GetBuiltInDataContract(name, ns) as PrimitiveDataContract;
        }

        internal abstract string WriteMethodName { get; }

        internal abstract string ReadMethodName { get; }

        internal override XmlDictionaryString TopLevelElementNamespace
        {
            get => DictionaryGlobals.SerializationNamespace;
            set { }
        }

        internal override bool CanContainReferences => false;

        internal override bool IsPrimitive => true;

        internal override bool IsBuiltInDataContract => true;

        internal MethodInfo XmlFormatWriterMethod
        {
            get
            {
                if (_helper.XmlFormatWriterMethod == null)
                {
                    if (UnderlyingType.IsValueType)
                    {
                        _helper.XmlFormatWriterMethod = typeof(XmlWriterDelegator).GetMethod(WriteMethodName, Globals.ScanAllMembers, null, new Type[] { UnderlyingType, typeof(XmlDictionaryString), typeof(XmlDictionaryString) }, null);
                    }
                    else
                    {
                        _helper.XmlFormatWriterMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(WriteMethodName, Globals.ScanAllMembers, null, new Type[] { typeof(XmlWriterDelegator), UnderlyingType, typeof(XmlDictionaryString), typeof(XmlDictionaryString) }, null);
                    }
                }
                return _helper.XmlFormatWriterMethod;
            }
        }

        internal MethodInfo XmlFormatContentWriterMethod
        {
            get
            {
                if (_helper.XmlFormatContentWriterMethod == null)
                {
                    if (UnderlyingType.IsValueType)
                    {
                        _helper.XmlFormatContentWriterMethod = typeof(XmlWriterDelegator).GetMethod(WriteMethodName, Globals.ScanAllMembers, null, new Type[] { UnderlyingType }, null);
                    }
                    else
                    {
                        _helper.XmlFormatContentWriterMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(WriteMethodName, Globals.ScanAllMembers, null, new Type[] { typeof(XmlWriterDelegator), UnderlyingType }, null);
                    }
                }
                return _helper.XmlFormatContentWriterMethod;
            }
        }

        internal MethodInfo XmlFormatReaderMethod
        {
            get
            {
                if (_helper.XmlFormatReaderMethod == null)
                {
                    _helper.XmlFormatReaderMethod = typeof(XmlReaderDelegator).GetMethod(ReadMethodName, Globals.ScanAllMembers);
                }
                return _helper.XmlFormatReaderMethod;
            }
        }

        public override void WriteXmlValue(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context)
        {
            xmlWriter.WriteAnyType(obj);
        }

        protected object HandleReadValue(object obj, XmlObjectSerializerReadContext context)
        {
            context.AddNewObject(obj);
            return obj;
        }

        protected bool TryReadNullAtTopLevel(XmlReaderDelegator reader)
        {
            Attributes attributes = new Attributes();
            attributes.Read(reader);
            if (attributes.Ref != Globals.NewObjectId)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                    XmlObjectSerializer2.CreateSerializationException(
                        SR.Format(SR.CannotDeserializeRefAtTopLevel, attributes.Ref)));
            }

            if (attributes.XsiNil)
            {
                reader.Skip();
                return true;
            }
            return false;
        }

        internal override bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
        {
            PrimitiveDataContract dataContract = other as PrimitiveDataContract;
            if (dataContract != null)
            {
                Type thisType = GetType();
                Type otherType = other.GetType();
                return (thisType.Equals(otherType) || thisType.IsSubclassOf(otherType) || otherType.IsSubclassOf(thisType));
            }
            return false;
        }

        private class PrimitiveDataContractCriticalHelper : DataContract.DataContractCriticalHelper
        {
            private MethodInfo xmlFormatWriterMethod;
            private MethodInfo xmlFormatContentWriterMethod;
            private MethodInfo xmlFormatReaderMethod;

            internal PrimitiveDataContractCriticalHelper(Type type, XmlDictionaryString name, XmlDictionaryString ns)
                : base(type)
            {
                SetDataContractName(name, ns);
            }

            internal MethodInfo XmlFormatWriterMethod
            {
                get => xmlFormatWriterMethod;
                set => xmlFormatWriterMethod = value;
            }

            internal MethodInfo XmlFormatContentWriterMethod
            {
                get => xmlFormatContentWriterMethod;
                set => xmlFormatContentWriterMethod = value;
            }

            internal MethodInfo XmlFormatReaderMethod
            {
                get => xmlFormatReaderMethod;
                set => xmlFormatReaderMethod = value;
            }
        }

    }

    internal class CharDataContract : PrimitiveDataContract
    {
        internal CharDataContract()
            : this(DictionaryGlobals.CharLocalName, DictionaryGlobals.SerializationNamespace)
        {
        }

        internal CharDataContract(XmlDictionaryString name, XmlDictionaryString ns)
            : base(typeof(char), name, ns)
        {
        }

        internal override string WriteMethodName => "WriteChar";
        internal override string ReadMethodName => "ReadElementContentAsChar";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteChar((char)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsChar()
                : HandleReadValue(reader.ReadElementContentAsChar(), context);
        }
    }

    internal class AsmxCharDataContract : CharDataContract
    {
        internal AsmxCharDataContract() : base(DictionaryGlobals.CharLocalName, DictionaryGlobals.AsmxTypesNamespace) { }
    }

    internal class BooleanDataContract : PrimitiveDataContract
    {
        internal BooleanDataContract()
            : base(typeof(bool), DictionaryGlobals.BooleanLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteBoolean";
        internal override string ReadMethodName => "ReadElementContentAsBoolean";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteBoolean((bool)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsBoolean()
                : HandleReadValue(reader.ReadElementContentAsBoolean(), context);
        }
    }

    internal class SignedByteDataContract : PrimitiveDataContract
    {
        internal SignedByteDataContract()
            : base(typeof(sbyte), DictionaryGlobals.SignedByteLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteSignedByte";
        internal override string ReadMethodName => "ReadElementContentAsSignedByte";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteSignedByte((sbyte)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsSignedByte()
                : HandleReadValue(reader.ReadElementContentAsSignedByte(), context);
        }
    }

    internal class UnsignedByteDataContract : PrimitiveDataContract
    {
        internal UnsignedByteDataContract()
            : base(typeof(byte), DictionaryGlobals.UnsignedByteLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteUnsignedByte";
        internal override string ReadMethodName => "ReadElementContentAsUnsignedByte";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteUnsignedByte((byte)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsUnsignedByte()
                : HandleReadValue(reader.ReadElementContentAsUnsignedByte(), context);
        }
    }

    internal class ShortDataContract : PrimitiveDataContract
    {
        internal ShortDataContract()
            : base(typeof(short), DictionaryGlobals.ShortLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteShort";
        internal override string ReadMethodName => "ReadElementContentAsShort";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteShort((short)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsShort()
                : HandleReadValue(reader.ReadElementContentAsShort(), context);
        }
    }

    internal class UnsignedShortDataContract : PrimitiveDataContract
    {
        internal UnsignedShortDataContract()
            : base(typeof(ushort), DictionaryGlobals.UnsignedShortLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteUnsignedShort";
        internal override string ReadMethodName => "ReadElementContentAsUnsignedShort";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteUnsignedShort((ushort)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsUnsignedShort()
                : HandleReadValue(reader.ReadElementContentAsUnsignedShort(), context);
        }
    }

    internal class IntDataContract : PrimitiveDataContract
    {
        internal IntDataContract()
            : base(typeof(int), DictionaryGlobals.IntLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteInt";
        internal override string ReadMethodName => "ReadElementContentAsInt";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteInt((int)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsInt()
                : HandleReadValue(reader.ReadElementContentAsInt(), context);
        }
    }

    internal class UnsignedIntDataContract : PrimitiveDataContract
    {
        internal UnsignedIntDataContract()
            : base(typeof(uint), DictionaryGlobals.UnsignedIntLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteUnsignedInt";
        internal override string ReadMethodName => "ReadElementContentAsUnsignedInt";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteUnsignedInt((uint)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsUnsignedInt()
                : HandleReadValue(reader.ReadElementContentAsUnsignedInt(), context);
        }
    }

    internal class LongDataContract : PrimitiveDataContract
    {
        internal LongDataContract()
            : this(DictionaryGlobals.LongLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal LongDataContract(XmlDictionaryString name, XmlDictionaryString ns)
            : base(typeof(long), name, ns)
        {
        }

        internal override string WriteMethodName => "WriteLong";
        internal override string ReadMethodName => "ReadElementContentAsLong";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteLong((long)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsLong()
                : HandleReadValue(reader.ReadElementContentAsLong(), context);
        }
    }
    internal class IntegerDataContract : LongDataContract
    {
        internal IntegerDataContract() : base(DictionaryGlobals.integerLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class PositiveIntegerDataContract : LongDataContract
    {
        internal PositiveIntegerDataContract() : base(DictionaryGlobals.positiveIntegerLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class NegativeIntegerDataContract : LongDataContract
    {
        internal NegativeIntegerDataContract() : base(DictionaryGlobals.negativeIntegerLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class NonPositiveIntegerDataContract : LongDataContract
    {
        internal NonPositiveIntegerDataContract() : base(DictionaryGlobals.nonPositiveIntegerLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class NonNegativeIntegerDataContract : LongDataContract
    {
        internal NonNegativeIntegerDataContract() : base(DictionaryGlobals.nonNegativeIntegerLocalName, DictionaryGlobals.SchemaNamespace) { }
    }

    internal class UnsignedLongDataContract : PrimitiveDataContract
    {
        internal UnsignedLongDataContract()
            : base(typeof(ulong), DictionaryGlobals.UnsignedLongLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteUnsignedLong";
        internal override string ReadMethodName => "ReadElementContentAsUnsignedLong";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteUnsignedLong((ulong)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsUnsignedLong()
                : HandleReadValue(reader.ReadElementContentAsUnsignedLong(), context);
        }
    }

    internal class FloatDataContract : PrimitiveDataContract
    {
        internal FloatDataContract()
            : base(typeof(float), DictionaryGlobals.FloatLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteFloat";
        internal override string ReadMethodName => "ReadElementContentAsFloat";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteFloat((float)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsFloat()
                : HandleReadValue(reader.ReadElementContentAsFloat(), context);
        }
    }

    internal class DoubleDataContract : PrimitiveDataContract
    {
        internal DoubleDataContract()
            : base(typeof(double), DictionaryGlobals.DoubleLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteDouble";
        internal override string ReadMethodName => "ReadElementContentAsDouble";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteDouble((double)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsDouble()
                : HandleReadValue(reader.ReadElementContentAsDouble(), context);
        }
    }

    internal class DecimalDataContract : PrimitiveDataContract
    {
        internal DecimalDataContract()
            : base(typeof(decimal), DictionaryGlobals.DecimalLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteDecimal";
        internal override string ReadMethodName => "ReadElementContentAsDecimal";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteDecimal((decimal)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsDecimal()
                : HandleReadValue(reader.ReadElementContentAsDecimal(), context);
        }
    }

    internal class DateTimeDataContract : PrimitiveDataContract
    {
        internal DateTimeDataContract()
            : base(typeof(DateTime), DictionaryGlobals.DateTimeLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteDateTime";
        internal override string ReadMethodName => "ReadElementContentAsDateTime";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteDateTime((DateTime)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsDateTime()
                : HandleReadValue(reader.ReadElementContentAsDateTime(), context);
        }
    }

    internal class StringDataContract : PrimitiveDataContract
    {
        internal StringDataContract()
            : this(DictionaryGlobals.StringLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal StringDataContract(XmlDictionaryString name, XmlDictionaryString ns)
            : base(typeof(string), name, ns)
        {
        }

        internal override string WriteMethodName => "WriteString";
        internal override string ReadMethodName => "ReadElementContentAsString";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteString((string)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            if (context == null)
            {
                return TryReadNullAtTopLevel(reader) ? null : reader.ReadElementContentAsString();
            }
            else
            {
                return HandleReadValue(reader.ReadElementContentAsString(), context);
            }
        }
    }
    internal class TimeDataContract : StringDataContract
    {
        internal TimeDataContract() : base(DictionaryGlobals.timeLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class DateDataContract : StringDataContract
    {
        internal DateDataContract() : base(DictionaryGlobals.dateLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class HexBinaryDataContract : StringDataContract
    {
        internal HexBinaryDataContract() : base(DictionaryGlobals.hexBinaryLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class GYearMonthDataContract : StringDataContract
    {
        internal GYearMonthDataContract() : base(DictionaryGlobals.gYearMonthLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class GYearDataContract : StringDataContract
    {
        internal GYearDataContract() : base(DictionaryGlobals.gYearLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class GMonthDayDataContract : StringDataContract
    {
        internal GMonthDayDataContract() : base(DictionaryGlobals.gMonthDayLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class GDayDataContract : StringDataContract
    {
        internal GDayDataContract() : base(DictionaryGlobals.gDayLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class GMonthDataContract : StringDataContract
    {
        internal GMonthDataContract() : base(DictionaryGlobals.gMonthLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class NormalizedStringDataContract : StringDataContract
    {
        internal NormalizedStringDataContract() : base(DictionaryGlobals.normalizedStringLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class TokenDataContract : StringDataContract
    {
        internal TokenDataContract() : base(DictionaryGlobals.tokenLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class LanguageDataContract : StringDataContract
    {
        internal LanguageDataContract() : base(DictionaryGlobals.languageLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class NameDataContract : StringDataContract
    {
        internal NameDataContract() : base(DictionaryGlobals.NameLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class NCNameDataContract : StringDataContract
    {
        internal NCNameDataContract() : base(DictionaryGlobals.NCNameLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class IDDataContract : StringDataContract
    {
        internal IDDataContract() : base(DictionaryGlobals.XSDIDLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class IDREFDataContract : StringDataContract
    {
        internal IDREFDataContract() : base(DictionaryGlobals.IDREFLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class IDREFSDataContract : StringDataContract
    {
        internal IDREFSDataContract() : base(DictionaryGlobals.IDREFSLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class ENTITYDataContract : StringDataContract
    {
        internal ENTITYDataContract() : base(DictionaryGlobals.ENTITYLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class ENTITIESDataContract : StringDataContract
    {
        internal ENTITIESDataContract() : base(DictionaryGlobals.ENTITIESLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class NMTOKENDataContract : StringDataContract
    {
        internal NMTOKENDataContract() : base(DictionaryGlobals.NMTOKENLocalName, DictionaryGlobals.SchemaNamespace) { }
    }
    internal class NMTOKENSDataContract : StringDataContract
    {
        internal NMTOKENSDataContract() : base(DictionaryGlobals.NMTOKENSLocalName, DictionaryGlobals.SchemaNamespace) { }
    }

    internal class ByteArrayDataContract : PrimitiveDataContract
    {
        internal ByteArrayDataContract()
            : base(typeof(byte[]), DictionaryGlobals.ByteArrayLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteBase64";
        internal override string ReadMethodName => "ReadElementContentAsBase64";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteBase64((byte[])obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            if (context == null)
            {
                return TryReadNullAtTopLevel(reader) ? null : reader.ReadElementContentAsBase64();
            }
            else
            {
                return HandleReadValue(reader.ReadElementContentAsBase64(), context);
            }
        }
    }

    internal class ObjectDataContract : PrimitiveDataContract
    {
        internal ObjectDataContract()
            : base(typeof(object), DictionaryGlobals.ObjectLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteAnyType";
        internal override string ReadMethodName => "ReadElementContentAsAnyType";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            // write nothing
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            object obj;
            if (reader.IsEmptyElement)
            {
                reader.Skip();
                obj = new object();
            }
            else
            {
                string localName = reader.LocalName;
                string ns = reader.NamespaceURI;
                reader.Read();
                try
                {
                    reader.ReadEndElement();
                    obj = new object();
                }
                catch (XmlException xes)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.XmlForObjectCannotHaveContent, localName, ns), xes));
                }
            }
            return (context == null) ? obj : HandleReadValue(obj, context);
        }

        internal override bool CanContainReferences => true;

        internal override bool IsPrimitive => false;

    }

    internal class TimeSpanDataContract : PrimitiveDataContract
    {
        internal TimeSpanDataContract()
            : this(DictionaryGlobals.TimeSpanLocalName, DictionaryGlobals.SerializationNamespace)
        {
        }

        internal TimeSpanDataContract(XmlDictionaryString name, XmlDictionaryString ns)
            : base(typeof(TimeSpan), name, ns)
        {
        }

        internal override string WriteMethodName => "WriteTimeSpan";
        internal override string ReadMethodName => "ReadElementContentAsTimeSpan";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteTimeSpan((TimeSpan)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsTimeSpan()
                : HandleReadValue(reader.ReadElementContentAsTimeSpan(), context);
        }
    }

    internal class XsDurationDataContract : TimeSpanDataContract
    {
        internal XsDurationDataContract() : base(DictionaryGlobals.TimeSpanLocalName, DictionaryGlobals.SchemaNamespace) { }
    }

    internal class GuidDataContract : PrimitiveDataContract
    {
        internal GuidDataContract()
            : this(DictionaryGlobals.GuidLocalName, DictionaryGlobals.SerializationNamespace)
        {
        }

        internal GuidDataContract(XmlDictionaryString name, XmlDictionaryString ns)
            : base(typeof(Guid), name, ns)
        {
        }

        internal override string WriteMethodName => "WriteGuid";
        internal override string ReadMethodName => "ReadElementContentAsGuid";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteGuid((Guid)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            return (context == null) ? reader.ReadElementContentAsGuid()
                : HandleReadValue(reader.ReadElementContentAsGuid(), context);
        }
    }

    internal class AsmxGuidDataContract : GuidDataContract
    {
        internal AsmxGuidDataContract() : base(DictionaryGlobals.GuidLocalName, DictionaryGlobals.AsmxTypesNamespace) { }
    }

    internal class UriDataContract : PrimitiveDataContract
    {
        internal UriDataContract()
            : base(typeof(Uri), DictionaryGlobals.UriLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteUri";
        internal override string ReadMethodName => "ReadElementContentAsUri";

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteUri((Uri)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            if (context == null)
            {
                return TryReadNullAtTopLevel(reader) ? null : reader.ReadElementContentAsUri();
            }
            else
            {
                return HandleReadValue(reader.ReadElementContentAsUri(), context);
            }
        }
    }

    internal class QNameDataContract : PrimitiveDataContract
    {
        internal QNameDataContract()
            : base(typeof(XmlQualifiedName), DictionaryGlobals.QNameLocalName, DictionaryGlobals.SchemaNamespace)
        {
        }

        internal override string WriteMethodName => "WriteQName";
        internal override string ReadMethodName => "ReadElementContentAsQName";

        internal override bool IsPrimitive => false;

        public override void WriteXmlValue(XmlWriterDelegator writer, object obj, XmlObjectSerializerWriteContext context)
        {
            writer.WriteQName((XmlQualifiedName)obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator reader, XmlObjectSerializerReadContext context)
        {
            if (context == null)
            {
                return TryReadNullAtTopLevel(reader) ? null : reader.ReadElementContentAsQName();
            }
            else
            {
                return HandleReadValue(reader.ReadElementContentAsQName(), context);
            }
        }

        internal override void WriteRootElement(XmlWriterDelegator writer, XmlDictionaryString name, XmlDictionaryString ns)
        {
            if (object.ReferenceEquals(ns, DictionaryGlobals.SerializationNamespace))
            {
                writer.WriteStartElement(Globals.SerPrefix, name, ns);
            }
            else if (ns != null && ns.Value != null && ns.Value.Length > 0)
            {
                writer.WriteStartElement(Globals.ElementPrefix, name, ns);
            }
            else
            {
                writer.WriteStartElement(name, ns);
            }
        }
    }
}
