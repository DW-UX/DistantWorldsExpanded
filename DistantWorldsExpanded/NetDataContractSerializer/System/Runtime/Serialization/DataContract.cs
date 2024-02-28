using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using CollectionDataContractAttribute = System.Runtime.Serialization.CollectionDataContractAttribute;
using ContractNamespaceAttribute = System.Runtime.Serialization.ContractNamespaceAttribute;
using DataContractAttribute = System.Runtime.Serialization.DataContractAttribute;
using InvalidDataContractException = System.Runtime.Serialization.InvalidDataContractException;
using KnownTypeAttribute = System.Runtime.Serialization.KnownTypeAttribute;
using SerializationException = System.Runtime.Serialization.SerializationException;

namespace System.Runtime.Serialization
{
    using DataContractDictionary = Dictionary<XmlQualifiedName, DataContract>;

    internal abstract class DataContract
    {
        private readonly XmlDictionaryString _name;

        private readonly XmlDictionaryString _ns;

        private readonly DataContractCriticalHelper _helper;

        protected DataContract(DataContractCriticalHelper helper)
        {
            _helper = helper;
            _name = helper.Name;
            _ns = helper.Namespace;
        }

        internal static DataContract GetDataContract(Type type)
        {
            return GetDataContract(type.TypeHandle, type, SerializationMode.SharedContract);
        }

        internal static DataContract GetDataContract(RuntimeTypeHandle typeHandle, Type type, SerializationMode mode)
        {
            int id = GetId(typeHandle);
            return GetDataContract(id, typeHandle, mode);
        }

        internal static DataContract GetDataContract(int id, RuntimeTypeHandle typeHandle, SerializationMode mode)
        {
            DataContract dataContract = GetDataContractSkipValidation(id, typeHandle, null);
            dataContract = dataContract.GetValidContract(mode);

            return dataContract;
        }

        internal static DataContract GetDataContractSkipValidation(int id, RuntimeTypeHandle typeHandle, Type type)
        {
            return DataContractCriticalHelper.GetDataContractSkipValidation(id, typeHandle, type);
        }

        internal static DataContract GetGetOnlyCollectionDataContract(int id, RuntimeTypeHandle typeHandle, Type type, SerializationMode mode)
        {
            DataContract dataContract = GetGetOnlyCollectionDataContractSkipValidation(id, typeHandle, type);
            dataContract = dataContract.GetValidContract(mode);
            if (dataContract is ClassDataContract)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.Format(SR.ClassDataContractReturnedForGetOnlyCollection, DataContract.GetClrTypeFullName(dataContract.UnderlyingType))));
            }
            return dataContract;
        }

        internal static DataContract GetGetOnlyCollectionDataContractSkipValidation(int id, RuntimeTypeHandle typeHandle, Type type)
        {
            return DataContractCriticalHelper.GetGetOnlyCollectionDataContractSkipValidation(id, typeHandle, type);
        }

        internal static DataContract GetDataContractForInitialization(int id)
        {
            return DataContractCriticalHelper.GetDataContractForInitialization(id);
        }

        internal static int GetIdForInitialization(ClassDataContract classContract)
        {
            return DataContractCriticalHelper.GetIdForInitialization(classContract);
        }

        internal static int GetId(RuntimeTypeHandle typeHandle)
        {
            return DataContractCriticalHelper.GetId(typeHandle);
        }

        public static DataContract GetBuiltInDataContract(Type type)
        {
            return DataContractCriticalHelper.GetBuiltInDataContract(type);
        }

        public static DataContract GetBuiltInDataContract(string name, string ns)
        {
            return DataContractCriticalHelper.GetBuiltInDataContract(name, ns);
        }

        public static DataContract GetBuiltInDataContract(string typeName)
        {
            return DataContractCriticalHelper.GetBuiltInDataContract(typeName);
        }

        internal static string GetNamespace(string key)
        {
            return DataContractCriticalHelper.GetNamespace(key);
        }

        internal static XmlDictionaryString GetClrTypeString(string key)
        {
            return DataContractCriticalHelper.GetClrTypeString(key);
        }

        internal static void ThrowInvalidDataContractException(string message, Type type)
        {
            DataContractCriticalHelper.ThrowInvalidDataContractException(message, type);
        }

        protected DataContractCriticalHelper Helper => _helper;

        internal Type UnderlyingType => _helper.UnderlyingType;

        internal Type OriginalUnderlyingType => _helper.OriginalUnderlyingType;


        internal virtual bool IsBuiltInDataContract => _helper.IsBuiltInDataContract;

        internal Type TypeForInitialization => _helper.TypeForInitialization;

        public virtual void WriteXmlValue(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context)
        {
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.UnexpectedContractType, DataContract.GetClrTypeFullName(GetType()), DataContract.GetClrTypeFullName(UnderlyingType))));
        }

        public virtual object ReadXmlValue(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context)
        {
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.UnexpectedContractType, DataContract.GetClrTypeFullName(GetType()), DataContract.GetClrTypeFullName(UnderlyingType))));
        }

        internal bool IsValueType
        {
            get => _helper.IsValueType;
            set => _helper.IsValueType = value;
        }

        internal bool IsReference
        {
            get => _helper.IsReference;
            set => _helper.IsReference = value;
        }

        internal XmlQualifiedName StableName
        {
            get => _helper.StableName;
            set => _helper.StableName = value;
        }

        internal GenericInfo GenericInfo
        {
            get => _helper.GenericInfo;
            set => _helper.GenericInfo = value;
        }

        internal virtual DataContractDictionary KnownDataContracts
        {
            get => _helper.KnownDataContracts;
            set => _helper.KnownDataContracts = value;
        }

        internal virtual bool IsISerializable
        {
            get => _helper.IsISerializable;
            set => _helper.IsISerializable = value;
        }

        internal XmlDictionaryString Name => _name;

        public virtual XmlDictionaryString Namespace => _ns;

        internal virtual bool HasRoot
        {
            get => true;
            set { }
        }

        internal virtual XmlDictionaryString TopLevelElementName
        {
            get => _helper.TopLevelElementName;
            set => _helper.TopLevelElementName = value;
        }

        internal virtual XmlDictionaryString TopLevelElementNamespace
        {
            get => _helper.TopLevelElementNamespace;
            set => _helper.TopLevelElementNamespace = value;
        }

        internal virtual bool CanContainReferences => true;

        internal virtual bool IsPrimitive => false;

        internal virtual void WriteRootElement(XmlWriterDelegator writer, XmlDictionaryString name, XmlDictionaryString ns)
        {
            if (object.ReferenceEquals(ns, DictionaryGlobals.SerializationNamespace) && !IsPrimitive)
            {
                writer.WriteStartElement(Globals.SerPrefix, name, ns);
            }
            else
            {
                writer.WriteStartElement(name, ns);
            }
        }

        internal virtual DataContract BindGenericParameters(DataContract[] paramContracts, Dictionary<DataContract, DataContract> boundContracts)
        {
            return this;
        }

        internal virtual DataContract GetValidContract(SerializationMode mode)
        {
            return this;
        }

        internal virtual DataContract GetValidContract()
        {
            return this;
        }

        internal virtual bool IsValidContract(SerializationMode mode)
        {
            return true;
        }

        internal MethodInfo ParseMethod => _helper.ParseMethod;

        protected class DataContractCriticalHelper
        {
            private static readonly Dictionary<TypeHandleRef, IntRef> typeToIDCache;
            private static DataContract[] dataContractCache;
            private static int dataContractID;
            private static Dictionary<Type, DataContract> typeToBuiltInContract;
            private static Dictionary<XmlQualifiedName, DataContract> nameToBuiltInContract;
            private static Dictionary<string, DataContract> typeNameToBuiltInContract;
            private static Dictionary<string, string> namespaces;
            private static Dictionary<string, XmlDictionaryString> clrTypeStrings;
            private static XmlDictionary clrTypeStringsDictionary;
            private static readonly TypeHandleRef typeHandleRef = new TypeHandleRef();
            private static readonly object cacheLock = new object();
            private static readonly object createDataContractLock = new object();
            private static readonly object initBuiltInContractsLock = new object();
            private static readonly object namespacesLock = new object();
            private static readonly object clrTypeStringsLock = new object();
            private readonly Type underlyingType;
            private Type originalUnderlyingType;
            private bool isReference;
            private bool isValueType;
            private XmlQualifiedName stableName;
            private GenericInfo genericInfo;
            private XmlDictionaryString name;
            private XmlDictionaryString ns;

            private Type typeForInitialization;
            private MethodInfo parseMethod;
            private bool parseMethodSet;

            static DataContractCriticalHelper()
            {
                typeToIDCache = new Dictionary<TypeHandleRef, IntRef>(new TypeHandleRefEqualityComparer());
                dataContractCache = new DataContract[32];
                dataContractID = 0;
            }

            internal static DataContract GetDataContractSkipValidation(int id, RuntimeTypeHandle typeHandle, Type type)
            {
                DataContract dataContract = dataContractCache[id];
                if (dataContract == null)
                {
                    dataContract = CreateDataContract(id, typeHandle, type);
                }
                else
                {
                    return dataContract.GetValidContract();
                }
                return dataContract;
            }

            internal static DataContract GetGetOnlyCollectionDataContractSkipValidation(int id, RuntimeTypeHandle typeHandle, Type type)
            {
                DataContract dataContract = dataContractCache[id];
                if (dataContract == null)
                {
                    dataContract = CreateGetOnlyCollectionDataContract(id, typeHandle, type);

                    AssignDataContractToId(dataContract, id);
                }
                return dataContract;
            }

            internal static DataContract GetDataContractForInitialization(int id)
            {
                DataContract dataContract = dataContractCache[id];
                if (dataContract == null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.DataContractCacheOverflow));
                }
                return dataContract;
            }

            internal static int GetIdForInitialization(ClassDataContract classContract)
            {
                int id = DataContract.GetId(classContract.TypeForInitialization.TypeHandle);
                if (id < dataContractCache.Length && ContractMatches(classContract, dataContractCache[id]))
                {
                    return id;
                }

                int currentDataContractId = DataContractCriticalHelper.dataContractID;

                for (int i = 0; i < currentDataContractId; i++)
                {
                    if (ContractMatches(classContract, dataContractCache[i]))
                    {
                        return i;
                    }
                }
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.DataContractCacheOverflow));
            }

            private static bool ContractMatches(DataContract contract, DataContract cachedContract)
            {
                return (cachedContract != null && cachedContract.UnderlyingType == contract.UnderlyingType);
            }

            internal static int GetId(RuntimeTypeHandle typeHandle)
            {
                lock (cacheLock)
                {
                    typeHandle = GetDataContractAdapterTypeHandle(typeHandle);
                    typeHandleRef.Value = typeHandle;
                    if (!typeToIDCache.TryGetValue(typeHandleRef, out IntRef id))
                    {
                        id = GetNextId();
                        try
                        {
                            typeToIDCache.Add(new TypeHandleRef(typeHandle), id);
                        }
                        catch (Exception ex)
                        {
                            if (Fx.IsFatal(ex))
                            {
                                throw;
                            }
                            throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.ToString(), ex);
                        }
                    }
                    return id.Value;
                }
            }

            // Assumed that this method is called under a lock
            private static IntRef GetNextId()
            {
                int value = dataContractID++;
                if (value >= dataContractCache.Length)
                {
                    int newSize = (value < int.MaxValue / 2) ? value * 2 : int.MaxValue;
                    if (newSize <= value)
                    {
                        Fx.Assert("DataContract cache overflow");
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SerializationException(SR.DataContractCacheOverflow));
                    }
                    Array.Resize<DataContract>(ref dataContractCache, newSize);
                }
                return new IntRef(value);
            }

            // check whether a corresponding update is required in ClassDataContract.IsNonAttributedTypeValidForSerialization
            private static DataContract CreateDataContract(int id, RuntimeTypeHandle typeHandle, Type type)
            {
                DataContract dataContract = dataContractCache[id];

                if (dataContract == null)
                {
                    lock (createDataContractLock)
                    {
                        dataContract = dataContractCache[id];

                        if (dataContract == null)
                        {
                            if (type == null)
                            {
                                type = Type.GetTypeFromHandle(typeHandle);
                            }

                            type = UnwrapNullableType(type);
                            type = GetDataContractAdapterType(type);
                            dataContract = GetBuiltInDataContract(type);
                            if (dataContract == null)
                            {
                                if (type.IsArray)
                                {
                                    dataContract = new CollectionDataContract(type);
                                }
                                else if (type.IsEnum)
                                {
                                    dataContract = new EnumDataContract(type);
                                }
                                else if (type.IsGenericParameter)
                                {
                                    dataContract = new GenericParameterDataContract(type);
                                }
                                else if (Globals.TypeOfIXmlSerializable.IsAssignableFrom(type))
                                {
                                    dataContract = new XmlDataContract(type);
                                }
                                else
                                {
                                    //if (type.ContainsGenericParameters)
                                    //    ThrowInvalidDataContractException(SR.Format(SR.TypeMustNotBeOpenGeneric, type), type);
                                    if (type.IsPointer)
                                    {
                                        type = Globals.TypeOfReflectionPointer;
                                    }

                                    if (!CollectionDataContract.TryCreate(type, out dataContract))
                                    {
                                        if (type.IsSerializable || type.IsDefined(Globals.TypeOfDataContractAttribute, false) || ClassDataContract.IsNonAttributedTypeValidForSerialization(type))
                                        {
                                            dataContract = new ClassDataContract(type);
                                        }
                                        else
                                        {
                                            ThrowInvalidDataContractException(SR.Format(SR.TypeNotSerializable, type), type);
                                        }
                                    }
                                }
                            }

                            AssignDataContractToId(dataContract, id);
                        }
                    }
                }

                return dataContract;
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            private static void AssignDataContractToId(DataContract dataContract, int id)
            {
                lock (cacheLock)
                {
                    dataContractCache[id] = dataContract;
                }
            }

            private static DataContract CreateGetOnlyCollectionDataContract(int id, RuntimeTypeHandle typeHandle, Type type)
            {
                DataContract dataContract = null;
                lock (createDataContractLock)
                {
                    dataContract = dataContractCache[id];
                    if (dataContract == null)
                    {
                        if (type == null)
                        {
                            type = Type.GetTypeFromHandle(typeHandle);
                        }

                        type = UnwrapNullableType(type);
                        type = GetDataContractAdapterType(type);
                        if (!CollectionDataContract.TryCreateGetOnlyCollectionDataContract(type, out dataContract))
                        {
                            ThrowInvalidDataContractException(SR.Format(SR.TypeNotSerializable, type), type);
                        }
                    }
                }
                return dataContract;
            }

            // Any change to this method should be reflected in GetDataContractOriginalType
            internal static Type GetDataContractAdapterType(Type type)
            {
                // Replace the DataTimeOffset ISerializable type passed in with the internal DateTimeOffsetAdapter DataContract type.
                // DateTimeOffsetAdapter is used for serialization/deserialization purposes to bypass the ISerializable implementation
                // on DateTimeOffset; which does not work in partial trust and to ensure correct schema import/export scenarios.
                if (type == Globals.TypeOfDateTimeOffset)
                {
                    return Globals.TypeOfDateTimeOffsetAdapter;
                }
                return type;
            }

            // Maps adapted types back to the original type
            // Any change to this method should be reflected in GetDataContractAdapterType
            internal static Type GetDataContractOriginalType(Type type)
            {
                if (type == Globals.TypeOfDateTimeOffsetAdapter)
                {
                    return Globals.TypeOfDateTimeOffset;
                }
                return type;
            }

            private static RuntimeTypeHandle GetDataContractAdapterTypeHandle(RuntimeTypeHandle typeHandle)
            {
                if (Globals.TypeOfDateTimeOffset.TypeHandle.Equals(typeHandle))
                {
                    return Globals.TypeOfDateTimeOffsetAdapter.TypeHandle;
                }
                return typeHandle;
            }

            public static DataContract GetBuiltInDataContract(Type type)
            {
                if (type.IsInterface && !CollectionDataContract.IsCollectionInterface(type))
                {
                    type = Globals.TypeOfObject;
                }

                lock (initBuiltInContractsLock)
                {
                    if (typeToBuiltInContract == null)
                    {
                        typeToBuiltInContract = new Dictionary<Type, DataContract>();
                    }

                    if (!typeToBuiltInContract.TryGetValue(type, out DataContract dataContract))
                    {
                        TryCreateBuiltInDataContract(type, out dataContract);
                        typeToBuiltInContract.Add(type, dataContract);
                    }
                    return dataContract;
                }
            }

            public static DataContract GetBuiltInDataContract(string name, string ns)
            {
                lock (initBuiltInContractsLock)
                {
                    if (nameToBuiltInContract == null)
                    {
                        nameToBuiltInContract = new Dictionary<XmlQualifiedName, DataContract>();
                    }

                    XmlQualifiedName qname = new XmlQualifiedName(name, ns);
                    if (!nameToBuiltInContract.TryGetValue(qname, out DataContract dataContract))
                    {
                        if (TryCreateBuiltInDataContract(name, ns, out dataContract))
                        {
                            nameToBuiltInContract.Add(qname, dataContract);
                        }
                    }
                    return dataContract;
                }
            }

            public static DataContract GetBuiltInDataContract(string typeName)
            {
                if (!typeName.StartsWith("System.", StringComparison.Ordinal))
                {
                    return null;
                }

                lock (initBuiltInContractsLock)
                {
                    if (typeNameToBuiltInContract == null)
                    {
                        typeNameToBuiltInContract = new Dictionary<string, DataContract>();
                    }

                    if (!typeNameToBuiltInContract.TryGetValue(typeName, out DataContract dataContract))
                    {
                        Type type = null;
                        string name = typeName.Substring(7);
                        if (name == "Char")
                        {
                            type = typeof(char);
                        }
                        else if (name == "Boolean")
                        {
                            type = typeof(bool);
                        }
                        else if (name == "SByte")
                        {
                            type = typeof(sbyte);
                        }
                        else if (name == "Byte")
                        {
                            type = typeof(byte);
                        }
                        else if (name == "Int16")
                        {
                            type = typeof(short);
                        }
                        else if (name == "UInt16")
                        {
                            type = typeof(ushort);
                        }
                        else if (name == "Int32")
                        {
                            type = typeof(int);
                        }
                        else if (name == "UInt32")
                        {
                            type = typeof(uint);
                        }
                        else if (name == "Int64")
                        {
                            type = typeof(long);
                        }
                        else if (name == "UInt64")
                        {
                            type = typeof(ulong);
                        }
                        else if (name == "Single")
                        {
                            type = typeof(float);
                        }
                        else if (name == "Double")
                        {
                            type = typeof(double);
                        }
                        else if (name == "Decimal")
                        {
                            type = typeof(decimal);
                        }
                        else if (name == "DateTime")
                        {
                            type = typeof(DateTime);
                        }
                        else if (name == "String")
                        {
                            type = typeof(string);
                        }
                        else if (name == "Byte[]")
                        {
                            type = typeof(byte[]);
                        }
                        else if (name == "Object")
                        {
                            type = typeof(object);
                        }
                        else if (name == "TimeSpan")
                        {
                            type = typeof(TimeSpan);
                        }
                        else if (name == "Guid")
                        {
                            type = typeof(Guid);
                        }
                        else if (name == "Uri")
                        {
                            type = typeof(Uri);
                        }
                        else if (name == "Xml.XmlQualifiedName")
                        {
                            type = typeof(XmlQualifiedName);
                        }
                        else if (name == "Enum")
                        {
                            type = typeof(Enum);
                        }
                        else if (name == "ValueType")
                        {
                            type = typeof(ValueType);
                        }
                        else if (name == "Array")
                        {
                            type = typeof(Array);
                        }
                        else if (name == "Xml.XmlElement")
                        {
                            type = typeof(XmlElement);
                        }
                        else if (name == "Xml.XmlNode[]")
                        {
                            type = typeof(XmlNode[]);
                        }

                        if (type != null)
                        {
                            TryCreateBuiltInDataContract(type, out dataContract);
                        }

                        typeNameToBuiltInContract.Add(typeName, dataContract);
                    }
                    return dataContract;
                }
            }

            public static bool TryCreateBuiltInDataContract(Type type, out DataContract dataContract)
            {
                if (type.IsEnum) // Type.GetTypeCode will report Enums as TypeCode.IntXX
                {
                    dataContract = null;
                    return false;
                }
                dataContract = null;
                switch (Type.GetTypeCode(type))
                {
                    case TypeCode.Boolean:
                        dataContract = new BooleanDataContract();
                        break;
                    case TypeCode.Byte:
                        dataContract = new UnsignedByteDataContract();
                        break;
                    case TypeCode.Char:
                        dataContract = new CharDataContract();
                        break;
                    case TypeCode.DateTime:
                        dataContract = new DateTimeDataContract();
                        break;
                    case TypeCode.Decimal:
                        dataContract = new DecimalDataContract();
                        break;
                    case TypeCode.Double:
                        dataContract = new DoubleDataContract();
                        break;
                    case TypeCode.Int16:
                        dataContract = new ShortDataContract();
                        break;
                    case TypeCode.Int32:
                        dataContract = new IntDataContract();
                        break;
                    case TypeCode.Int64:
                        dataContract = new LongDataContract();
                        break;
                    case TypeCode.SByte:
                        dataContract = new SignedByteDataContract();
                        break;
                    case TypeCode.Single:
                        dataContract = new FloatDataContract();
                        break;
                    case TypeCode.String:
                        dataContract = new StringDataContract();
                        break;
                    case TypeCode.UInt16:
                        dataContract = new UnsignedShortDataContract();
                        break;
                    case TypeCode.UInt32:
                        dataContract = new UnsignedIntDataContract();
                        break;
                    case TypeCode.UInt64:
                        dataContract = new UnsignedLongDataContract();
                        break;
                    default:
                        if (type == typeof(byte[]))
                        {
                            dataContract = new ByteArrayDataContract();
                        }
                        else if (type == typeof(object))
                        {
                            dataContract = new ObjectDataContract();
                        }
                        else if (type == typeof(Uri))
                        {
                            dataContract = new UriDataContract();
                        }
                        else if (type == typeof(XmlQualifiedName))
                        {
                            dataContract = new QNameDataContract();
                        }
                        else if (type == typeof(TimeSpan))
                        {
                            dataContract = new TimeSpanDataContract();
                        }
                        else if (type == typeof(Guid))
                        {
                            dataContract = new GuidDataContract();
                        }
                        else if (type == typeof(Enum) || type == typeof(ValueType))
                        {
                            dataContract = new SpecialTypeDataContract(type, DictionaryGlobals.ObjectLocalName, DictionaryGlobals.SchemaNamespace);
                        }
                        else if (type == typeof(Array))
                        {
                            dataContract = new CollectionDataContract(type);
                        }
                        else if (type == typeof(XmlElement) || type == typeof(XmlNode[]))
                        {
                            dataContract = new XmlDataContract(type);
                        }

                        break;
                }
                return dataContract != null;
            }

            public static bool TryCreateBuiltInDataContract(string name, string ns, out DataContract dataContract)
            {
                dataContract = null;
                if (ns == DictionaryGlobals.SchemaNamespace.Value)
                {
                    if (DictionaryGlobals.BooleanLocalName.Value == name)
                    {
                        dataContract = new BooleanDataContract();
                    }
                    else if (DictionaryGlobals.SignedByteLocalName.Value == name)
                    {
                        dataContract = new SignedByteDataContract();
                    }
                    else if (DictionaryGlobals.UnsignedByteLocalName.Value == name)
                    {
                        dataContract = new UnsignedByteDataContract();
                    }
                    else if (DictionaryGlobals.ShortLocalName.Value == name)
                    {
                        dataContract = new ShortDataContract();
                    }
                    else if (DictionaryGlobals.UnsignedShortLocalName.Value == name)
                    {
                        dataContract = new UnsignedShortDataContract();
                    }
                    else if (DictionaryGlobals.IntLocalName.Value == name)
                    {
                        dataContract = new IntDataContract();
                    }
                    else if (DictionaryGlobals.UnsignedIntLocalName.Value == name)
                    {
                        dataContract = new UnsignedIntDataContract();
                    }
                    else if (DictionaryGlobals.LongLocalName.Value == name)
                    {
                        dataContract = new LongDataContract();
                    }
                    else if (DictionaryGlobals.integerLocalName.Value == name)
                    {
                        dataContract = new IntegerDataContract();
                    }
                    else if (DictionaryGlobals.positiveIntegerLocalName.Value == name)
                    {
                        dataContract = new PositiveIntegerDataContract();
                    }
                    else if (DictionaryGlobals.negativeIntegerLocalName.Value == name)
                    {
                        dataContract = new NegativeIntegerDataContract();
                    }
                    else if (DictionaryGlobals.nonPositiveIntegerLocalName.Value == name)
                    {
                        dataContract = new NonPositiveIntegerDataContract();
                    }
                    else if (DictionaryGlobals.nonNegativeIntegerLocalName.Value == name)
                    {
                        dataContract = new NonNegativeIntegerDataContract();
                    }
                    else if (DictionaryGlobals.UnsignedLongLocalName.Value == name)
                    {
                        dataContract = new UnsignedLongDataContract();
                    }
                    else if (DictionaryGlobals.FloatLocalName.Value == name)
                    {
                        dataContract = new FloatDataContract();
                    }
                    else if (DictionaryGlobals.DoubleLocalName.Value == name)
                    {
                        dataContract = new DoubleDataContract();
                    }
                    else if (DictionaryGlobals.DecimalLocalName.Value == name)
                    {
                        dataContract = new DecimalDataContract();
                    }
                    else if (DictionaryGlobals.DateTimeLocalName.Value == name)
                    {
                        dataContract = new DateTimeDataContract();
                    }
                    else if (DictionaryGlobals.StringLocalName.Value == name)
                    {
                        dataContract = new StringDataContract();
                    }
                    else if (DictionaryGlobals.timeLocalName.Value == name)
                    {
                        dataContract = new TimeDataContract();
                    }
                    else if (DictionaryGlobals.dateLocalName.Value == name)
                    {
                        dataContract = new DateDataContract();
                    }
                    else if (DictionaryGlobals.hexBinaryLocalName.Value == name)
                    {
                        dataContract = new HexBinaryDataContract();
                    }
                    else if (DictionaryGlobals.gYearMonthLocalName.Value == name)
                    {
                        dataContract = new GYearMonthDataContract();
                    }
                    else if (DictionaryGlobals.gYearLocalName.Value == name)
                    {
                        dataContract = new GYearDataContract();
                    }
                    else if (DictionaryGlobals.gMonthDayLocalName.Value == name)
                    {
                        dataContract = new GMonthDayDataContract();
                    }
                    else if (DictionaryGlobals.gDayLocalName.Value == name)
                    {
                        dataContract = new GDayDataContract();
                    }
                    else if (DictionaryGlobals.gMonthLocalName.Value == name)
                    {
                        dataContract = new GMonthDataContract();
                    }
                    else if (DictionaryGlobals.normalizedStringLocalName.Value == name)
                    {
                        dataContract = new NormalizedStringDataContract();
                    }
                    else if (DictionaryGlobals.tokenLocalName.Value == name)
                    {
                        dataContract = new TokenDataContract();
                    }
                    else if (DictionaryGlobals.languageLocalName.Value == name)
                    {
                        dataContract = new LanguageDataContract();
                    }
                    else if (DictionaryGlobals.NameLocalName.Value == name)
                    {
                        dataContract = new NameDataContract();
                    }
                    else if (DictionaryGlobals.NCNameLocalName.Value == name)
                    {
                        dataContract = new NCNameDataContract();
                    }
                    else if (DictionaryGlobals.XSDIDLocalName.Value == name)
                    {
                        dataContract = new IDDataContract();
                    }
                    else if (DictionaryGlobals.IDREFLocalName.Value == name)
                    {
                        dataContract = new IDREFDataContract();
                    }
                    else if (DictionaryGlobals.IDREFSLocalName.Value == name)
                    {
                        dataContract = new IDREFSDataContract();
                    }
                    else if (DictionaryGlobals.ENTITYLocalName.Value == name)
                    {
                        dataContract = new ENTITYDataContract();
                    }
                    else if (DictionaryGlobals.ENTITIESLocalName.Value == name)
                    {
                        dataContract = new ENTITIESDataContract();
                    }
                    else if (DictionaryGlobals.NMTOKENLocalName.Value == name)
                    {
                        dataContract = new NMTOKENDataContract();
                    }
                    else if (DictionaryGlobals.NMTOKENSLocalName.Value == name)
                    {
                        dataContract = new NMTOKENDataContract();
                    }
                    else if (DictionaryGlobals.ByteArrayLocalName.Value == name)
                    {
                        dataContract = new ByteArrayDataContract();
                    }
                    else if (DictionaryGlobals.ObjectLocalName.Value == name)
                    {
                        dataContract = new ObjectDataContract();
                    }
                    else if (DictionaryGlobals.TimeSpanLocalName.Value == name)
                    {
                        dataContract = new XsDurationDataContract();
                    }
                    else if (DictionaryGlobals.UriLocalName.Value == name)
                    {
                        dataContract = new UriDataContract();
                    }
                    else if (DictionaryGlobals.QNameLocalName.Value == name)
                    {
                        dataContract = new QNameDataContract();
                    }
                }
                else if (ns == DictionaryGlobals.SerializationNamespace.Value)
                {
                    if (DictionaryGlobals.TimeSpanLocalName.Value == name)
                    {
                        dataContract = new TimeSpanDataContract();
                    }
                    else if (DictionaryGlobals.GuidLocalName.Value == name)
                    {
                        dataContract = new GuidDataContract();
                    }
                    else if (DictionaryGlobals.CharLocalName.Value == name)
                    {
                        dataContract = new CharDataContract();
                    }
                    else if ("ArrayOfanyType" == name)
                    {
                        dataContract = new CollectionDataContract(typeof(Array));
                    }
                }
                else if (ns == DictionaryGlobals.AsmxTypesNamespace.Value)
                {
                    if (DictionaryGlobals.CharLocalName.Value == name)
                    {
                        dataContract = new AsmxCharDataContract();
                    }
                    else if (DictionaryGlobals.GuidLocalName.Value == name)
                    {
                        dataContract = new AsmxGuidDataContract();
                    }
                }
                else if (ns == Globals.DataContractXmlNamespace)
                {
                    if (name == "XmlElement")
                    {
                        dataContract = new XmlDataContract(typeof(XmlElement));
                    }
                    else if (name == "ArrayOfXmlNode")
                    {
                        dataContract = new XmlDataContract(typeof(XmlNode[]));
                    }
                }
                return dataContract != null;
            }

            internal static string GetNamespace(string key)
            {
                lock (namespacesLock)
                {
                    if (namespaces == null)
                    {
                        namespaces = new Dictionary<string, string>();
                    }

                    if (namespaces.TryGetValue(key, out string value))
                    {
                        return value;
                    }

                    try
                    {
                        namespaces.Add(key, key);
                    }
                    catch (Exception ex)
                    {
                        if (Fx.IsFatal(ex))
                        {
                            throw;
                        }
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.ToString(), ex);
                    }
                    return key;
                }
            }

            internal static XmlDictionaryString GetClrTypeString(string key)
            {
                lock (clrTypeStringsLock)
                {
                    if (clrTypeStrings == null)
                    {
                        clrTypeStringsDictionary = new XmlDictionary();
                        clrTypeStrings = new Dictionary<string, XmlDictionaryString>();
                        try
                        {
                            clrTypeStrings.Add(Globals.TypeOfInt.Assembly.FullName, clrTypeStringsDictionary.Add(Globals.MscorlibAssemblyName));
                        }
                        catch (Exception ex)
                        {
                            if (Fx.IsFatal(ex))
                            {
                                throw;
                            }
                            throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.ToString(), ex);
                        }
                    }
                    if (clrTypeStrings.TryGetValue(key, out XmlDictionaryString value))
                    {
                        return value;
                    }

                    value = clrTypeStringsDictionary.Add(key);
                    try
                    {
                        clrTypeStrings.Add(key, value);
                    }
                    catch (Exception ex)
                    {
                        if (Fx.IsFatal(ex))
                        {
                            throw;
                        }
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.ToString(), ex);
                    }
                    return value;
                }
            }

            internal static void ThrowInvalidDataContractException(string message, Type type)
            {
                if (type != null)
                {
                    lock (cacheLock)
                    {
                        typeHandleRef.Value = GetDataContractAdapterTypeHandle(type.TypeHandle);
                        try
                        {
                            typeToIDCache.Remove(typeHandleRef);
                        }
                        catch (Exception ex)
                        {
                            if (Fx.IsFatal(ex))
                            {
                                throw;
                            }
                            throw DiagnosticUtility.ExceptionUtility.ThrowHelperFatal(ex.ToString(), ex);
                        }
                    }
                }

                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(message));
            }

            internal DataContractCriticalHelper()
            {
            }

            internal DataContractCriticalHelper(Type type)
            {
                underlyingType = type;
                SetTypeForInitialization(type);
                isValueType = type.IsValueType;
            }

            internal Type UnderlyingType => underlyingType;

            internal Type OriginalUnderlyingType
            {
                get
                {
                    if (originalUnderlyingType == null)
                    {
                        originalUnderlyingType = GetDataContractOriginalType(underlyingType);
                    }
                    return originalUnderlyingType;
                }
            }

            internal virtual bool IsBuiltInDataContract => false;

            internal Type TypeForInitialization => typeForInitialization;

            private void SetTypeForInitialization(Type classType)
            {
                if (classType.IsSerializable || classType.IsDefined(Globals.TypeOfDataContractAttribute, false))
                {
                    typeForInitialization = classType;
                }
            }

            internal bool IsReference
            {
                get => isReference;
                set => isReference = value;
            }

            internal bool IsValueType
            {
                get => isValueType;
                set => isValueType = value;
            }

            internal XmlQualifiedName StableName
            {
                get => stableName;
                set => stableName = value;
            }

            internal GenericInfo GenericInfo
            {
                get => genericInfo;
                set => genericInfo = value;
            }

            internal virtual DataContractDictionary KnownDataContracts
            {
                get => null;
                set { /* do nothing */ }
            }

            internal virtual bool IsISerializable
            {
                get => false;
                set => ThrowInvalidDataContractException(SR.RequiresClassDataContractToSetIsISerializable);
            }

            internal XmlDictionaryString Name
            {
                get => name;
                set => name = value;
            }

            public XmlDictionaryString Namespace
            {
                get => ns;
                set => ns = value;
            }

            internal virtual bool HasRoot
            {
                get => true;
                set { }
            }

            internal virtual XmlDictionaryString TopLevelElementName
            {
                get => name;
                set => name = value;
            }

            internal virtual XmlDictionaryString TopLevelElementNamespace
            {
                get => ns;
                set => ns = value;
            }

            internal virtual bool CanContainReferences => true;

            internal virtual bool IsPrimitive => false;

            internal MethodInfo ParseMethod
            {
                get
                {
                    if (!parseMethodSet)
                    {
                        MethodInfo method = UnderlyingType.GetMethod(Globals.ParseMethodName, BindingFlags.Public | BindingFlags.Static, null, new Type[] { Globals.TypeOfString }, null);

                        if (method != null && method.ReturnType == UnderlyingType)
                        {
                            parseMethod = method;
                        }

                        parseMethodSet = true;
                    }
                    return parseMethod;
                }
            }

            internal virtual void WriteRootElement(XmlWriterDelegator writer, XmlDictionaryString name, XmlDictionaryString ns)
            {
                if (object.ReferenceEquals(ns, DictionaryGlobals.SerializationNamespace) && !IsPrimitive)
                {
                    writer.WriteStartElement(Globals.SerPrefix, name, ns);
                }
                else
                {
                    writer.WriteStartElement(name, ns);
                }
            }

            internal void SetDataContractName(XmlQualifiedName stableName)
            {
                XmlDictionary dictionary = new XmlDictionary(2);
                Name = dictionary.Add(stableName.Name);
                Namespace = dictionary.Add(stableName.Namespace);
                StableName = stableName;
            }

            internal void SetDataContractName(XmlDictionaryString name, XmlDictionaryString ns)
            {
                Name = name;
                Namespace = ns;
                StableName = CreateQualifiedName(name.Value, ns.Value);
            }

            internal void ThrowInvalidDataContractException(string message)
            {
                ThrowInvalidDataContractException(message, UnderlyingType);
            }
        }

        internal static bool IsTypeSerializable(Type type)
        {
            return IsTypeSerializable(type, new Dictionary<Type, object>());
        }

        private static bool IsTypeSerializable(Type type, Dictionary<Type, object> previousCollectionTypes)
        {
            if (type.IsSerializable ||
                type.IsDefined(Globals.TypeOfDataContractAttribute, false) ||
                type.IsInterface ||
                type.IsPointer ||
                Globals.TypeOfIXmlSerializable.IsAssignableFrom(type))
            {
                return true;
            }
            if (CollectionDataContract.IsCollection(type, out Type itemType))
            {
                ValidatePreviousCollectionTypes(type, itemType, previousCollectionTypes);
                if (IsTypeSerializable(itemType, previousCollectionTypes))
                {
                    return true;
                }
            }
            return (DataContract.GetBuiltInDataContract(type) != null || ClassDataContract.IsNonAttributedTypeValidForSerialization(type));
        }

        private static void ValidatePreviousCollectionTypes(Type collectionType, Type itemType, Dictionary<Type, object> previousCollectionTypes)
        {
            previousCollectionTypes.Add(collectionType, collectionType);
            while (itemType.IsArray)
            {
                itemType = itemType.GetElementType();
            }

            // Do a breadth first traversal of the generic type tree to 
            // produce the closure of all generic argument types and
            // check that none of these is in the previousCollectionTypes            

            List<Type> itemTypeClosure = new List<Type>();
            Queue<Type> itemTypeQueue = new Queue<Type>();

            itemTypeQueue.Enqueue(itemType);
            itemTypeClosure.Add(itemType);

            while (itemTypeQueue.Count > 0)
            {
                itemType = itemTypeQueue.Dequeue();
                if (previousCollectionTypes.ContainsKey(itemType))
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.RecursiveCollectionType, DataContract.GetClrTypeFullName(itemType))));
                }
                if (itemType.IsGenericType)
                {
                    foreach (Type argType in itemType.GetGenericArguments())
                    {
                        if (!itemTypeClosure.Contains(argType))
                        {
                            itemTypeQueue.Enqueue(argType);
                            itemTypeClosure.Add(argType);
                        }
                    }
                }
            }
        }

        internal static Type UnwrapRedundantNullableType(Type type)
        {
            Type nullableType = type;
            while (type.IsGenericType && type.GetGenericTypeDefinition() == Globals.TypeOfNullable)
            {
                nullableType = type;
                type = type.GetGenericArguments()[0];
            }
            return nullableType;
        }

        internal static Type UnwrapNullableType(Type type)
        {
            while (type.IsGenericType && type.GetGenericTypeDefinition() == Globals.TypeOfNullable)
            {
                type = type.GetGenericArguments()[0];
            }

            return type;
        }

        private static bool IsAlpha(char ch)
        {
            return (ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z');
        }

        private static bool IsDigit(char ch)
        {
            return (ch >= '0' && ch <= '9');
        }

        private static bool IsAsciiLocalName(string localName)
        {
            if (localName.Length == 0)
            {
                return false;
            }

            if (!IsAlpha(localName[0]))
            {
                return false;
            }

            for (int i = 1; i < localName.Length; i++)
            {
                char ch = localName[i];
                if (!IsAlpha(ch) && !IsDigit(ch))
                {
                    return false;
                }
            }
            return true;
        }

        internal static string EncodeLocalName(string localName)
        {
            if (IsAsciiLocalName(localName))
            {
                return localName;
            }

            if (IsValidNCName(localName))
            {
                return localName;
            }

            return XmlConvert.EncodeLocalName(localName);
        }

        internal static bool IsValidNCName(string name)
        {
            try
            {
                XmlConvert.VerifyNCName(name);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        internal static XmlQualifiedName GetStableName(Type type)
        {
            return GetStableName(type, out bool hasDataContract);
        }

        internal static XmlQualifiedName GetStableName(Type type, out bool hasDataContract)
        {
            return GetStableName(type, new Dictionary<Type, object>(), out hasDataContract);
        }

        private static XmlQualifiedName GetStableName(Type type, Dictionary<Type, object> previousCollectionTypes, out bool hasDataContract)
        {
            type = UnwrapRedundantNullableType(type);
            if (TryGetBuiltInXmlAndArrayTypeStableName(type, previousCollectionTypes, out XmlQualifiedName stableName))
            {
                hasDataContract = false;
            }
            else
            {
                if (TryGetDCAttribute(type, out DataContractAttribute dataContractAttribute))
                {
                    stableName = GetDCTypeStableName(type, dataContractAttribute);
                    hasDataContract = true;
                }
                else
                {
                    stableName = GetNonDCTypeStableName(type, previousCollectionTypes);
                    hasDataContract = false;
                }
            }

            return stableName;
        }

        private static XmlQualifiedName GetDCTypeStableName(Type type, DataContractAttribute dataContractAttribute)
        {
            string name = null, ns = null;
            if (dataContractAttribute.IsNameSetExplicitly)
            {
                name = dataContractAttribute.Name;
                if (name == null || name.Length == 0)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.InvalidDataContractName, DataContract.GetClrTypeFullName(type))));
                }

                if (type.IsGenericType && !type.IsGenericTypeDefinition)
                {
                    name = ExpandGenericParameters(name, type);
                }

                name = DataContract.EncodeLocalName(name);
            }
            else
            {
                name = GetDefaultStableLocalName(type);
            }

            if (dataContractAttribute.IsNamespaceSetExplicitly)
            {
                ns = dataContractAttribute.Namespace;
                if (ns == null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.InvalidDataContractNamespace, DataContract.GetClrTypeFullName(type))));
                }

                CheckExplicitDataContractNamespaceUri(ns, type);
            }
            else
            {
                ns = GetDefaultDataContractNamespace(type);
            }

            return CreateQualifiedName(name, ns);
        }

        private static XmlQualifiedName GetNonDCTypeStableName(Type type, Dictionary<Type, object> previousCollectionTypes)
        {
            string name = null, ns = null;

            if (CollectionDataContract.IsCollection(type, out Type itemType))
            {
                ValidatePreviousCollectionTypes(type, itemType, previousCollectionTypes);
                return GetCollectionStableName(type, itemType, previousCollectionTypes, out CollectionDataContractAttribute collectionContractAttribute);
            }
            name = GetDefaultStableLocalName(type);

            // ensures that ContractNamespaceAttribute is honored when used with non-attributed types
            if (ClassDataContract.IsNonAttributedTypeValidForSerialization(type))
            {
                ns = GetDefaultDataContractNamespace(type);
            }
            else
            {
                ns = GetDefaultStableNamespace(type);
            }
            return CreateQualifiedName(name, ns);
        }

        private static bool TryGetBuiltInXmlAndArrayTypeStableName(Type type, Dictionary<Type, object> previousCollectionTypes, out XmlQualifiedName stableName)
        {
            stableName = null;

            DataContract builtInContract = GetBuiltInDataContract(type);
            if (builtInContract != null)
            {
                stableName = builtInContract.StableName;
            }
            else if (Globals.TypeOfIXmlSerializable.IsAssignableFrom(type))
            {
                SchemaExporter.GetXmlTypeInfo(type, out XmlQualifiedName xmlTypeStableName, out XmlSchemaType xsdType, out bool hasRoot);
                stableName = xmlTypeStableName;
            }
            else if (type.IsArray)
            {
                Type itemType = type.GetElementType();
                ValidatePreviousCollectionTypes(type, itemType, previousCollectionTypes);
                stableName = GetCollectionStableName(type, itemType, previousCollectionTypes, out CollectionDataContractAttribute collectionContractAttribute);
            }
            return stableName != null;
        }

        internal static bool TryGetDCAttribute(Type type, out DataContractAttribute dataContractAttribute)
        {
            dataContractAttribute = null;

            object[] dataContractAttributes = type.GetCustomAttributes(Globals.TypeOfDataContractAttribute, false);
            if (dataContractAttributes != null && dataContractAttributes.Length > 0)
            {
#if DEBUG
                if (dataContractAttributes.Length > 1)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.TooManyDataContracts, DataContract.GetClrTypeFullName(type))));
                }
#endif
                dataContractAttribute = (DataContractAttribute)dataContractAttributes[0];
            }

            return dataContractAttribute != null;
        }

        internal static XmlQualifiedName GetCollectionStableName(Type type, Type itemType, out CollectionDataContractAttribute collectionContractAttribute)
        {
            return GetCollectionStableName(type, itemType, new Dictionary<Type, object>(), out collectionContractAttribute);
        }

        private static XmlQualifiedName GetCollectionStableName(Type type, Type itemType, Dictionary<Type, object> previousCollectionTypes, out CollectionDataContractAttribute collectionContractAttribute)
        {
            string name, ns;
            object[] collectionContractAttributes = type.GetCustomAttributes(Globals.TypeOfCollectionDataContractAttribute, false);
            if (collectionContractAttributes != null && collectionContractAttributes.Length > 0)
            {
#if DEBUG
                if (collectionContractAttributes.Length > 1)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.TooManyCollectionContracts, DataContract.GetClrTypeFullName(type))));
                }
#endif
                collectionContractAttribute = (CollectionDataContractAttribute)collectionContractAttributes[0];
                if (collectionContractAttribute.IsNameSetExplicitly)
                {
                    name = collectionContractAttribute.Name;
                    if (name == null || name.Length == 0)
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.InvalidCollectionContractName, DataContract.GetClrTypeFullName(type))));
                    }

                    if (type.IsGenericType && !type.IsGenericTypeDefinition)
                    {
                        name = ExpandGenericParameters(name, type);
                    }

                    name = DataContract.EncodeLocalName(name);
                }
                else
                {
                    name = GetDefaultStableLocalName(type);
                }

                if (collectionContractAttribute.IsNamespaceSetExplicitly)
                {
                    ns = collectionContractAttribute.Namespace;
                    if (ns == null)
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.InvalidCollectionContractNamespace, DataContract.GetClrTypeFullName(type))));
                    }

                    CheckExplicitDataContractNamespaceUri(ns, type);
                }
                else
                {
                    ns = GetDefaultDataContractNamespace(type);
                }
            }
            else
            {
                collectionContractAttribute = null;
                string arrayOfPrefix = Globals.ArrayPrefix + GetArrayPrefix(ref itemType);
                XmlQualifiedName elementStableName = GetStableName(itemType, previousCollectionTypes, out bool hasDataContract);
                name = arrayOfPrefix + elementStableName.Name;
                ns = GetCollectionNamespace(elementStableName.Namespace);
            }
            return CreateQualifiedName(name, ns);
        }

        private static string GetArrayPrefix(ref Type itemType)
        {
            string arrayOfPrefix = string.Empty;
            while (itemType.IsArray)
            {
                if (DataContract.GetBuiltInDataContract(itemType) != null)
                {
                    break;
                }

                arrayOfPrefix += Globals.ArrayPrefix;
                itemType = itemType.GetElementType();
            }
            return arrayOfPrefix;
        }

        internal XmlQualifiedName GetArrayTypeName(bool isNullable)
        {
            XmlQualifiedName itemName;
            if (IsValueType && isNullable)
            {
                GenericInfo genericInfo = new GenericInfo(DataContract.GetStableName(Globals.TypeOfNullable), Globals.TypeOfNullable.FullName);
                genericInfo.Add(new GenericInfo(StableName, null));
                genericInfo.AddToLevel(0, 1);
                itemName = genericInfo.GetExpandedStableName();
            }
            else
            {
                itemName = StableName;
            }

            string ns = GetCollectionNamespace(itemName.Namespace);
            string name = Globals.ArrayPrefix + itemName.Name;
            return new XmlQualifiedName(name, ns);
        }

        internal static string GetCollectionNamespace(string elementNs)
        {
            return IsBuiltInNamespace(elementNs) ? Globals.CollectionsNamespace : elementNs;
        }

        internal static XmlQualifiedName GetDefaultStableName(Type type)
        {
            return CreateQualifiedName(GetDefaultStableLocalName(type), GetDefaultStableNamespace(type));
        }

        private static string GetDefaultStableLocalName(Type type)
        {
            if (type.IsGenericParameter)
            {
                return "{" + type.GenericParameterPosition + "}";
            }

            string typeName;
            string arrayPrefix = null;
            if (type.IsArray)
            {
                arrayPrefix = GetArrayPrefix(ref type);
            }

            if (type.DeclaringType == null)
            {
                typeName = type.Name;
            }
            else
            {
                int nsLen = (type.Namespace == null) ? 0 : type.Namespace.Length;
                if (nsLen > 0)
                {
                    nsLen++; //include the . following namespace
                }

                typeName = DataContract.GetClrTypeFullName(type).Substring(nsLen).Replace('+', '.');
            }
            if (arrayPrefix != null)
            {
                typeName = arrayPrefix + typeName;
            }

            if (type.IsGenericType)
            {
                StringBuilder localName = new StringBuilder();
                StringBuilder namespaces = new StringBuilder();
                bool parametersFromBuiltInNamespaces = true;
                int iParam = typeName.IndexOf('[');
                if (iParam >= 0)
                {
                    typeName = typeName.Substring(0, iParam);
                }

                IList<int> nestedParamCounts = GetDataContractNameForGenericName(typeName, localName);
                bool isTypeOpenGeneric = type.IsGenericTypeDefinition;
                Type[] genParams = type.GetGenericArguments();
                for (int i = 0; i < genParams.Length; i++)
                {
                    Type genParam = genParams[i];
                    if (isTypeOpenGeneric)
                    {
                        localName.Append("{").Append(i).Append("}");
                    }
                    else
                    {
                        XmlQualifiedName qname = DataContract.GetStableName(genParam);
                        localName.Append(qname.Name);
                        namespaces.Append(" ").Append(qname.Namespace);
                        if (parametersFromBuiltInNamespaces)
                        {
                            parametersFromBuiltInNamespaces = IsBuiltInNamespace(qname.Namespace);
                        }
                    }
                }
                if (isTypeOpenGeneric)
                {
                    localName.Append("{#}");
                }
                else if (nestedParamCounts.Count > 1 || !parametersFromBuiltInNamespaces)
                {
                    foreach (int count in nestedParamCounts)
                    {
                        namespaces.Insert(0, count).Insert(0, " ");
                    }

                    localName.Append(GetNamespacesDigest(namespaces.ToString()));
                }
                typeName = localName.ToString();
            }
            return DataContract.EncodeLocalName(typeName);
        }

        private static string GetDefaultDataContractNamespace(Type type)
        {
            string clrNs = type.Namespace;
            if (clrNs == null)
            {
                clrNs = string.Empty;
            }

            string ns = GetGlobalDataContractNamespace(clrNs, type.Module);
            if (ns == null)
            {
                ns = GetGlobalDataContractNamespace(clrNs, type.Assembly);
            }

            if (ns == null)
            {
                ns = GetDefaultStableNamespace(type);
            }
            else
            {
                CheckExplicitDataContractNamespaceUri(ns, type);
            }

            return ns;
        }

        internal static IList<int> GetDataContractNameForGenericName(string typeName, StringBuilder localName)
        {
            List<int> nestedParamCounts = new List<int>();
            for (int startIndex = 0, endIndex; ;)
            {
                endIndex = typeName.IndexOf('`', startIndex);
                if (endIndex < 0)
                {
                    if (localName != null)
                    {
                        localName.Append(typeName.Substring(startIndex));
                    }

                    nestedParamCounts.Add(0);
                    break;
                }
                if (localName != null)
                {
                    localName.Append(typeName.Substring(startIndex, endIndex - startIndex));
                }

                while ((startIndex = typeName.IndexOf('.', startIndex + 1, endIndex - startIndex - 1)) >= 0)
                {
                    nestedParamCounts.Add(0);
                }

                startIndex = typeName.IndexOf('.', endIndex);
                if (startIndex < 0)
                {
                    nestedParamCounts.Add(int.Parse(typeName.Substring(endIndex + 1), CultureInfo.InvariantCulture));
                    break;
                }
                else
                {
                    nestedParamCounts.Add(int.Parse(typeName.Substring(endIndex + 1, startIndex - endIndex - 1), CultureInfo.InvariantCulture));
                }
            }
            if (localName != null)
            {
                localName.Append("Of");
            }

            return nestedParamCounts;
        }

        internal static bool IsBuiltInNamespace(string ns)
        {
            return (ns == Globals.SchemaNamespace || ns == Globals.SerializationNamespace);
        }

        internal static string GetDefaultStableNamespace(Type type)
        {
            if (type.IsGenericParameter)
            {
                return "{ns}";
            }

            return GetDefaultStableNamespace(type.Namespace);
        }

        internal static XmlQualifiedName CreateQualifiedName(string localName, string ns)
        {
            return new XmlQualifiedName(localName, GetNamespace(ns));
        }

        internal static string GetDefaultStableNamespace(string clrNs)
        {
            if (clrNs == null)
            {
                clrNs = string.Empty;
            }

            return new Uri(Globals.DataContractXsdBaseNamespaceUri, clrNs).AbsoluteUri;
        }

        private static void CheckExplicitDataContractNamespaceUri(string dataContractNs, Type type)
        {
            if (dataContractNs.Length > 0)
            {
                string trimmedNs = dataContractNs.Trim();
                // Code similar to XmlConvert.ToUri (string.Empty is a valid uri but not "   ")
                if (trimmedNs.Length == 0 || trimmedNs.IndexOf("##", StringComparison.Ordinal) != -1)
                {
                    ThrowInvalidDataContractException(SR.Format(SR.DataContractNamespaceIsNotValid, dataContractNs), type);
                }

                dataContractNs = trimmedNs;
            }
            if (Uri.TryCreate(dataContractNs, UriKind.RelativeOrAbsolute, out Uri uri))
            {
                if (uri.ToString() == Globals.SerializationNamespace)
                {
                    ThrowInvalidDataContractException(SR.Format(SR.DataContractNamespaceReserved, Globals.SerializationNamespace), type);
                }
            }
            else
            {
                ThrowInvalidDataContractException(SR.Format(SR.DataContractNamespaceIsNotValid, dataContractNs), type);
            }
        }

        internal static string GetClrTypeFullName(Type type)
        {
            return !type.IsGenericTypeDefinition && type.ContainsGenericParameters ? string.Format(CultureInfo.InvariantCulture, "{0}.{1}", type.Namespace, type.Name) : type.FullName;
        }

        private static readonly ConcurrentDictionary<
                Type,
                (bool HasTypeForwardedFrom, string AssemblyName)
            > GetClrAssemblyNameCache = new();
        
        public static Func<Type, string>? ClrAssemblyNameOverride = null; 
        
        internal static string GetClrAssemblyName(Type type, out bool hasTypeForwardedFrom)
        {
            var result = GetClrAssemblyNameCache.GetOrAdd(type,
                static type => {
                var result = ClrAssemblyNameOverride?.Invoke(type);
                if (result != null)
                    return (false, result);

                var typeAttribute
                    = type.GetCustomAttribute<TypeForwardedFromAttribute>(false);

                return typeAttribute != null
                    ? (true, typeAttribute.AssemblyFullName)
                    : (false, type.Assembly.FullName);
            });
            
            hasTypeForwardedFrom = result.HasTypeForwardedFrom;
            return result.AssemblyName;
        }

        internal static string GetClrTypeFullNameUsingTypeForwardedFromAttribute(Type type)
        {
            if (type.IsArray)
            {
                return GetClrTypeFullNameForArray(type);
            }
            else
            {
                return GetClrTypeFullNameForNonArrayTypes(type);
            }
        }

        private static string GetClrTypeFullNameForArray(Type type)
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}",
                GetClrTypeFullNameUsingTypeForwardedFromAttribute(type.GetElementType()), Globals.OpenBracket, Globals.CloseBracket);
        }

        private static string GetClrTypeFullNameForNonArrayTypes(Type type)
        {
            if (!type.IsGenericType)
            {
                return DataContract.GetClrTypeFullName(type);
            }

            Type[] genericArguments = type.GetGenericArguments();
            StringBuilder builder = new StringBuilder(type.GetGenericTypeDefinition().FullName).Append(Globals.OpenBracket);

            foreach (Type genericArgument in genericArguments)
            {
                builder.Append(Globals.OpenBracket).Append(GetClrTypeFullNameUsingTypeForwardedFromAttribute(genericArgument)).Append(Globals.Comma);
                builder.Append(Globals.Space).Append(GetClrAssemblyName(genericArgument, out bool hasTypeForwardedFrom));
                builder.Append(Globals.CloseBracket).Append(Globals.Comma);
            }

            //remove the last comma and close typename for generic with a close bracket
            return builder.Remove(builder.Length - 1, 1).Append(Globals.CloseBracket).ToString();
        }

        internal static void GetClrNameAndNamespace(string fullTypeName, out string localName, out string ns)
        {
            int nsEnd = fullTypeName.LastIndexOf('.');
            if (nsEnd < 0)
            {
                ns = string.Empty;
                localName = fullTypeName.Replace('+', '.');
            }
            else
            {
                ns = fullTypeName.Substring(0, nsEnd);
                localName = fullTypeName.Substring(nsEnd + 1).Replace('+', '.');
            }
            int iParam = localName.IndexOf('[');
            if (iParam >= 0)
            {
                localName = localName.Substring(0, iParam);
            }
        }

        internal static void GetDefaultStableName(string fullTypeName, out string localName, out string ns)
        {
            CodeTypeReference typeReference = new CodeTypeReference(fullTypeName);
            GetDefaultStableName(typeReference, out localName, out ns);
        }

        private static void GetDefaultStableName(CodeTypeReference typeReference, out string localName, out string ns)
        {
            string fullTypeName = typeReference.BaseType;
            DataContract dataContract = GetBuiltInDataContract(fullTypeName);
            if (dataContract != null)
            {
                localName = dataContract.StableName.Name;
                ns = dataContract.StableName.Namespace;
                return;
            }
            GetClrNameAndNamespace(fullTypeName, out localName, out ns);
            if (typeReference.TypeArguments.Count > 0)
            {
                StringBuilder localNameBuilder = new StringBuilder();
                StringBuilder argNamespacesBuilder = new StringBuilder();
                bool parametersFromBuiltInNamespaces = true;
                IList<int> nestedParamCounts = GetDataContractNameForGenericName(localName, localNameBuilder);
                foreach (CodeTypeReference typeArg in typeReference.TypeArguments)
                {
                    GetDefaultStableName(typeArg, out string typeArgName, out string typeArgNs);
                    localNameBuilder.Append(typeArgName);
                    argNamespacesBuilder.Append(" ").Append(typeArgNs);
                    if (parametersFromBuiltInNamespaces)
                    {
                        parametersFromBuiltInNamespaces = IsBuiltInNamespace(typeArgNs);
                    }
                }
                if (nestedParamCounts.Count > 1 || !parametersFromBuiltInNamespaces)
                {
                    foreach (int count in nestedParamCounts)
                    {
                        argNamespacesBuilder.Insert(0, count).Insert(0, " ");
                    }

                    localNameBuilder.Append(GetNamespacesDigest(argNamespacesBuilder.ToString()));
                }
                localName = localNameBuilder.ToString();
            }
            localName = DataContract.EncodeLocalName(localName);
            ns = GetDefaultStableNamespace(ns);
        }

        internal static string GetDataContractNamespaceFromUri(string uriString)
        {
            return uriString.StartsWith(Globals.DataContractXsdBaseNamespace, StringComparison.Ordinal) ? uriString.Substring(Globals.DataContractXsdBaseNamespace.Length) : uriString;
        }

        private static string GetGlobalDataContractNamespace(string clrNs, ICustomAttributeProvider customAttribuetProvider)
        {
            object[] nsAttributes = customAttribuetProvider.GetCustomAttributes(typeof(ContractNamespaceAttribute), false);
            string dataContractNs = null;
            for (int i = 0; i < nsAttributes.Length; i++)
            {
                ContractNamespaceAttribute nsAttribute = (ContractNamespaceAttribute)nsAttributes[i];
                string clrNsInAttribute = nsAttribute.ClrNamespace;
                if (clrNsInAttribute == null)
                {
                    clrNsInAttribute = string.Empty;
                }

                if (clrNsInAttribute == clrNs)
                {
                    if (nsAttribute.ContractNamespace == null)
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.InvalidGlobalDataContractNamespace, clrNs)));
                    }

                    if (dataContractNs != null)
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.DataContractNamespaceAlreadySet, dataContractNs, nsAttribute.ContractNamespace, clrNs)));
                    }

                    dataContractNs = nsAttribute.ContractNamespace;
                }
            }
            return dataContractNs;
        }

        private static string GetNamespacesDigest(string namespaces)
        {
            byte[] namespaceBytes = Encoding.UTF8.GetBytes(namespaces);
            byte[] digestBytes = HashHelper.ComputeHash(namespaceBytes);
            char[] digestChars = new char[24];
            const int digestLen = 6;
            int digestCharsLen = Convert.ToBase64CharArray(digestBytes, 0, digestLen, digestChars, 0);
            StringBuilder digest = new StringBuilder();
            for (int i = 0; i < digestCharsLen; i++)
            {
                char ch = digestChars[i];
                switch (ch)
                {
                    case '=':
                        break;
                    case '/':
                        digest.Append("_S");
                        break;
                    case '+':
                        digest.Append("_P");
                        break;
                    default:
                        digest.Append(ch);
                        break;
                }
            }
            return digest.ToString();
        }

        private static string ExpandGenericParameters(string format, Type type)
        {
            GenericNameProvider genericNameProviderForType = new GenericNameProvider(type);
            return ExpandGenericParameters(format, genericNameProviderForType);
        }

        internal static string ExpandGenericParameters(string format, IGenericNameProvider genericNameProvider)
        {
            string digest = null;
            StringBuilder typeName = new StringBuilder();
            IList<int> nestedParameterCounts = genericNameProvider.GetNestedParameterCounts();
            for (int i = 0; i < format.Length; i++)
            {
                char ch = format[i];
                if (ch == '{')
                {
                    i++;
                    int start = i;
                    for (; i < format.Length; i++)
                    {
                        if (format[i] == '}')
                        {
                            break;
                        }
                    }

                    if (i == format.Length)
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.GenericNameBraceMismatch, format, genericNameProvider.GetGenericTypeName())));
                    }

                    if (format[start] == '#' && i == (start + 1))
                    {
                        if (nestedParameterCounts.Count > 1 || !genericNameProvider.ParametersFromBuiltInNamespaces)
                        {
                            if (digest == null)
                            {
                                StringBuilder namespaces = new StringBuilder(genericNameProvider.GetNamespaces());
                                foreach (int count in nestedParameterCounts)
                                {
                                    namespaces.Insert(0, count).Insert(0, " ");
                                }

                                digest = GetNamespacesDigest(namespaces.ToString());
                            }
                            typeName.Append(digest);
                        }
                    }
                    else
                    {
                        if (!int.TryParse(format.Substring(start, i - start), out int paramIndex) || paramIndex < 0 || paramIndex >= genericNameProvider.GetParameterCount())
                        {
                            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.GenericParameterNotValid, format.Substring(start, i - start), genericNameProvider.GetGenericTypeName(), genericNameProvider.GetParameterCount() - 1)));
                        }

                        typeName.Append(genericNameProvider.GetParameterName(paramIndex));
                    }
                }
                else
                {
                    typeName.Append(ch);
                }
            }
            return typeName.ToString();
        }

        internal static bool IsTypeNullable(Type type)
        {
            return !type.IsValueType ||
                    (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == Globals.TypeOfNullable);
        }

        public static void ThrowTypeNotSerializable(Type type)
        {
            ThrowInvalidDataContractException(SR.Format(SR.TypeNotSerializable, type), type);
        }

        internal static DataContractDictionary ImportKnownTypeAttributes(Type type)
        {
            DataContractDictionary knownDataContracts = null;
            Dictionary<Type, Type> typesChecked = new Dictionary<Type, Type>();
            ImportKnownTypeAttributes(type, typesChecked, ref knownDataContracts);
            return knownDataContracts;
        }

        private static void ImportKnownTypeAttributes(Type type, Dictionary<Type, Type> typesChecked, ref DataContractDictionary knownDataContracts)
        {
            while (type != null && DataContract.IsTypeSerializable(type))
            {
                if (typesChecked.ContainsKey(type))
                {
                    return;
                }

                typesChecked.Add(type, type);
                object[] knownTypeAttributes = type.GetCustomAttributes(Globals.TypeOfKnownTypeAttribute, false);
                if (knownTypeAttributes != null)
                {
                    KnownTypeAttribute kt;
                    bool useMethod = false, useType = false;
                    for (int i = 0; i < knownTypeAttributes.Length; ++i)
                    {
                        kt = (KnownTypeAttribute)knownTypeAttributes[i];
                        if (kt.Type != null)
                        {
                            if (useMethod)
                            {
                                DataContract.ThrowInvalidDataContractException(SR.Format(SR.KnownTypeAttributeOneScheme, DataContract.GetClrTypeFullName(type)), type);
                            }

                            CheckAndAdd(kt.Type, typesChecked, ref knownDataContracts);
                            useType = true;
                        }
                        else
                        {
                            if (useMethod || useType)
                            {
                                DataContract.ThrowInvalidDataContractException(SR.Format(SR.KnownTypeAttributeOneScheme, DataContract.GetClrTypeFullName(type)), type);
                            }

                            string methodName = kt.MethodName;
                            if (methodName == null)
                            {
                                DataContract.ThrowInvalidDataContractException(SR.Format(SR.KnownTypeAttributeNoData, DataContract.GetClrTypeFullName(type)), type);
                            }

                            if (methodName.Length == 0)
                            {
                                DataContract.ThrowInvalidDataContractException(SR.Format(SR.KnownTypeAttributeEmptyString, DataContract.GetClrTypeFullName(type)), type);
                            }

                            MethodInfo method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public, null, Globals.EmptyTypeArray, null);
                            if (method == null)
                            {
                                DataContract.ThrowInvalidDataContractException(SR.Format(SR.KnownTypeAttributeUnknownMethod, methodName, DataContract.GetClrTypeFullName(type)), type);
                            }

                            if (!Globals.TypeOfTypeEnumerable.IsAssignableFrom(method.ReturnType))
                            {
                                DataContract.ThrowInvalidDataContractException(SR.Format(SR.KnownTypeAttributeReturnType, DataContract.GetClrTypeFullName(type), methodName), type);
                            }

                            object types = method.Invoke(null, Globals.EmptyObjectArray);
                            if (types == null)
                            {
                                DataContract.ThrowInvalidDataContractException(SR.Format(SR.KnownTypeAttributeMethodNull, DataContract.GetClrTypeFullName(type)), type);
                            }

                            foreach (Type ty in (IEnumerable<Type>)types)
                            {
                                if (ty == null)
                                {
                                    DataContract.ThrowInvalidDataContractException(SR.Format(SR.KnownTypeAttributeValidMethodTypes, DataContract.GetClrTypeFullName(type)), type);
                                }

                                CheckAndAdd(ty, typesChecked, ref knownDataContracts);
                            }

                            useMethod = true;
                        }
                    }
                }

                type = type.BaseType;
            }
        }


        private static void CheckRootTypeInConfigIsGeneric(Type type, ref Type rootType, ref Type[] genArgs)
        {
            if (rootType.IsGenericType)
            {
                if (!rootType.ContainsGenericParameters)
                {
                    genArgs = rootType.GetGenericArguments();
                    rootType = rootType.GetGenericTypeDefinition();
                }
                else
                {
                    DataContract.ThrowInvalidDataContractException(SR.Format(SR.TypeMustBeConcrete, type), type);
                }
            }
        }

        private static bool IsElemTypeNullOrNotEqualToRootType(string elemTypeName, Type rootType)
        {
            Type t = Type.GetType(elemTypeName, false);
            if (t == null || !rootType.Equals(t))
            {
                return true;
            }
            return false;
        }

        private static bool IsCollectionElementTypeEqualToRootType(string collectionElementTypeName, Type rootType)
        {
            if (collectionElementTypeName.StartsWith(DataContract.GetClrTypeFullName(rootType), StringComparison.Ordinal))
            {
                Type t = Type.GetType(collectionElementTypeName, false);
                if (t != null)
                {
                    if (t.IsGenericType && !IsOpenGenericType(t))
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.Format(SR.KnownTypeConfigClosedGenericDeclared, collectionElementTypeName)));
                    }
                    else if (rootType.Equals(t))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static void CheckAndAdd(Type type, Dictionary<Type, Type> typesChecked, ref DataContractDictionary nameToDataContractTable)
        {
            type = DataContract.UnwrapNullableType(type);
            DataContract dataContract = DataContract.GetDataContract(type);
            if (nameToDataContractTable == null)
            {
                nameToDataContractTable = new DataContractDictionary();
            }
            else if (nameToDataContractTable.TryGetValue(dataContract.StableName, out DataContract alreadyExistingContract))
            {
                if (alreadyExistingContract.UnderlyingType != DataContractCriticalHelper.GetDataContractAdapterType(type))
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.Format(SR.DupContractInKnownTypes, type, alreadyExistingContract.UnderlyingType, dataContract.StableName.Namespace, dataContract.StableName.Name)));
                }

                return;
            }
            nameToDataContractTable.Add(dataContract.StableName, dataContract);
            ImportKnownTypeAttributes(type, typesChecked, ref nameToDataContractTable);
        }

        private static bool IsOpenGenericType(Type t)
        {
            Type[] args = t.GetGenericArguments();
            for (int i = 0; i < args.Length; ++i)
            {
                if (!args[i].IsGenericParameter)
                {
                    return false;
                }
            }

            return true;
        }

        public sealed override bool Equals(object other)
        {
            if ((object)this == other)
            {
                return true;
            }

            return Equals(other, new Dictionary<DataContractPairKey, object>());
        }

        internal virtual bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
        {
            DataContract dataContract = other as DataContract;
            if (dataContract != null)
            {
                return (StableName.Name == dataContract.StableName.Name && StableName.Namespace == dataContract.StableName.Namespace && IsReference == dataContract.IsReference);
            }
            return false;
        }

        internal bool IsEqualOrChecked(object other, Dictionary<DataContractPairKey, object> checkedContracts)
        {
            if ((object)this == other)
            {
                return true;
            }

            if (checkedContracts != null)
            {
                DataContractPairKey contractPairKey = new DataContractPairKey(this, other);
                if (checkedContracts.ContainsKey(contractPairKey))
                {
                    return true;
                }

                checkedContracts.Add(contractPairKey, null);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        internal void ThrowInvalidDataContractException(string message)
        {
            ThrowInvalidDataContractException(message, UnderlyingType);
        }

        internal static bool IsTypeVisible(Type t)
        {
            // Generic parameters are always considered visible.
            if (t.IsGenericParameter)
            {
                return true;
            }

            // The normal Type.IsVisible check requires all nested types to be IsNestedPublic.
            // This does not comply with our convention where they can also have InternalsVisibleTo
            // with our assembly.   The following method performs a recursive walk back the declaring
            // type hierarchy to perform this enhanced IsVisible check.
            if (!IsTypeAndDeclaringTypeVisible(t))
            {
                return false;
            }

            // All generic argument types must also be visible.
            // Nested types perform this test recursively for all their declaring types.
            foreach (Type genericType in t.GetGenericArguments())
            {
                if (!IsTypeVisible(genericType))
                {
                    return false;
                }
            }

            return true;
        }

        internal static bool IsTypeAndDeclaringTypeVisible(Type t)
        {
            // Arrays, etc. must consider the underlying element type because the
            // non-element type does not reflect the same type nesting.  For example,
            // MyClass[] would not show as a nested type, even when MyClass is nested.
            if (t.HasElementType)
            {
                return IsTypeVisible(t.GetElementType());
            }

            // Nested types are not visible unless their declaring type is visible.
            // Additionally, they must be either IsNestedPublic or in an assembly with InternalsVisibleTo this current assembly.
            // Non-nested types must be public or have this same InternalsVisibleTo relation.
            return t.IsNested
                    ? (t.IsNestedPublic || IsTypeVisibleInSerializationModule(t)) && IsTypeVisible(t.DeclaringType)
                    : t.IsPublic || IsTypeVisibleInSerializationModule(t);
        }

        internal static bool ConstructorRequiresMemberAccess(ConstructorInfo ctor)
        {
            return ctor != null && !ctor.IsPublic && !IsMemberVisibleInSerializationModule(ctor);
        }

        internal static bool MethodRequiresMemberAccess(MethodInfo method)
        {
            return method != null && !method.IsPublic && !IsMemberVisibleInSerializationModule(method);
        }

        internal static bool FieldRequiresMemberAccess(FieldInfo field)
        {
            return field != null && !field.IsPublic && !IsMemberVisibleInSerializationModule(field);
        }

        private static bool IsTypeVisibleInSerializationModule(Type type)
        {
            return (type.Module.Equals(typeof(CodeGenerator).Module) || IsAssemblyFriendOfSerialization(type.Assembly)) && !type.IsNestedPrivate;
        }

        private static bool IsMemberVisibleInSerializationModule(MemberInfo member)
        {
            if (!IsTypeVisibleInSerializationModule(member.DeclaringType))
            {
                return false;
            }

            if (member is MethodInfo method)
            {
                return (method.IsAssembly || method.IsFamilyOrAssembly);
            }
            else if (member is FieldInfo field)
            {
                return (field.IsAssembly || field.IsFamilyOrAssembly) && IsTypeVisible(field.FieldType);
            }
            else if (member is ConstructorInfo constructor)
            {
                return (constructor.IsAssembly || constructor.IsFamilyOrAssembly);
            }

            return false;
        }

        internal static bool IsAssemblyFriendOfSerialization(Assembly assembly)
        {
            InternalsVisibleToAttribute[] internalsVisibleAttributes = (InternalsVisibleToAttribute[])assembly.GetCustomAttributes(typeof(InternalsVisibleToAttribute), false);
            foreach (InternalsVisibleToAttribute internalsVisibleAttribute in internalsVisibleAttributes)
            {
                string internalsVisibleAttributeAssemblyName = internalsVisibleAttribute.AssemblyName;

                if (Regex.IsMatch(internalsVisibleAttributeAssemblyName, Globals.SimpleSRSInternalsVisiblePattern) ||
                    Regex.IsMatch(internalsVisibleAttributeAssemblyName, Globals.FullSRSInternalsVisiblePattern))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
