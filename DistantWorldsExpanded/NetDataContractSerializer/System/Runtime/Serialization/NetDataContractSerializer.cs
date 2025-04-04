﻿using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Xml;
using IFormatter = System.Runtime.Serialization.IFormatter;
using ISerializationSurrogate = System.Runtime.Serialization.ISerializationSurrogate;
using ISurrogateSelector = System.Runtime.Serialization.ISurrogateSelector;
using SerializationBinder = System.Runtime.Serialization.SerializationBinder;
using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
using StreamingContext = System.Runtime.Serialization.StreamingContext;
using StreamingContextStates = System.Runtime.Serialization.StreamingContextStates;

namespace System.Runtime.Serialization
{
    public sealed class NetDataContractSerializer : XmlObjectSerializer2, IFormatter
    {
        private XmlDictionaryString rootName;
        private XmlDictionaryString rootNamespace;
        private StreamingContext context;
        private SerializationBinder binder;
        private ISurrogateSelector surrogateSelector;
        private int maxItemsInObjectGraph;
        private bool ignoreExtensionDataObject;
        private FormatterAssemblyStyle assemblyFormat;
        private DataContract cachedDataContract;
        private static readonly Hashtable typeNameCache = new Hashtable();

        public NetDataContractSerializer()
            : this(new StreamingContext(StreamingContextStates.All))
        {
        }

        public NetDataContractSerializer(StreamingContext context)
            : this(context, int.MaxValue, false, FormatterAssemblyStyle.Full, null)
        {
        }

        public NetDataContractSerializer(StreamingContext context,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            FormatterAssemblyStyle assemblyFormat,
            ISurrogateSelector surrogateSelector)
        {
            Initialize(context, maxItemsInObjectGraph, ignoreExtensionDataObject, assemblyFormat, surrogateSelector);
        }

        public NetDataContractSerializer(string rootName, string rootNamespace)
            : this(rootName, rootNamespace, new StreamingContext(StreamingContextStates.All), int.MaxValue, false, FormatterAssemblyStyle.Full, null)
        {
        }

        public NetDataContractSerializer(string rootName, string rootNamespace,
            StreamingContext context,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            FormatterAssemblyStyle assemblyFormat,
            ISurrogateSelector surrogateSelector)
        {
            XmlDictionary dictionary = new XmlDictionary(2);
            Initialize(dictionary.Add(rootName), dictionary.Add(DataContract.GetNamespace(rootNamespace)), context, maxItemsInObjectGraph, ignoreExtensionDataObject, assemblyFormat, surrogateSelector);
        }

        public NetDataContractSerializer(XmlDictionaryString rootName, XmlDictionaryString rootNamespace)
            : this(rootName, rootNamespace, new StreamingContext(StreamingContextStates.All), int.MaxValue, false, FormatterAssemblyStyle.Full, null)
        {
        }

        public NetDataContractSerializer(XmlDictionaryString rootName, XmlDictionaryString rootNamespace,
            StreamingContext context,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            FormatterAssemblyStyle assemblyFormat,
            ISurrogateSelector surrogateSelector)
        {
            Initialize(rootName, rootNamespace, context, maxItemsInObjectGraph, ignoreExtensionDataObject, assemblyFormat, surrogateSelector);
        }

        private void Initialize(StreamingContext context,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            FormatterAssemblyStyle assemblyFormat,
            ISurrogateSelector surrogateSelector)
        {
            this.context = context;
            if (maxItemsInObjectGraph < 0)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException(nameof(maxItemsInObjectGraph), SR.ValueMustBeNonNegative));
            }

            this.maxItemsInObjectGraph = maxItemsInObjectGraph;
            this.ignoreExtensionDataObject = ignoreExtensionDataObject;
            this.surrogateSelector = surrogateSelector;
            AssemblyFormat = assemblyFormat;
        }

        private void Initialize(XmlDictionaryString rootName, XmlDictionaryString rootNamespace,
            StreamingContext context,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            FormatterAssemblyStyle assemblyFormat,
            ISurrogateSelector surrogateSelector)
        {
            Initialize(context, maxItemsInObjectGraph, ignoreExtensionDataObject, assemblyFormat, surrogateSelector);
            this.rootName = rootName;
            this.rootNamespace = rootNamespace;
        }

        public StreamingContext Context
        {
            get => context;
            set => context = value;
        }

        public SerializationBinder Binder
        {
            get => binder;
            set => binder = value;
        }

        public ISurrogateSelector SurrogateSelector
        {
            get => surrogateSelector;
            set => surrogateSelector = value;
        }

        public FormatterAssemblyStyle AssemblyFormat
        {
            get => assemblyFormat;
            set
            {
                if (value != FormatterAssemblyStyle.Full && value != FormatterAssemblyStyle.Simple)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.Format(SR.InvalidAssemblyFormat, value)));
                }

                assemblyFormat = value;
            }
        }

        public int MaxItemsInObjectGraph => maxItemsInObjectGraph;

        public bool IgnoreExtensionDataObject => ignoreExtensionDataObject;

        public void Serialize(Stream stream, object graph)
        {
            base.WriteObject(stream, graph);
        }

        public object Deserialize(Stream stream)
        {
            return base.ReadObject(stream);
        }

        public object Deserialize(XmlReader reader)
        {
            return base.ReadObject(reader);
        }

        internal override void InternalWriteObject(XmlWriterDelegator writer, object graph)
        {
            Hashtable surrogateDataContracts = null;
            DataContract contract = GetDataContract(graph, ref surrogateDataContracts);

            InternalWriteStartObject(writer, graph, contract);
            InternalWriteObjectContent(writer, graph, contract, surrogateDataContracts);
            InternalWriteEndObject(writer);
        }

        public override void WriteObject(XmlWriter writer, object graph)
        {
            WriteObjectHandleExceptions(new XmlWriterDelegator(writer), graph);
        }

        public override void WriteStartObject(XmlWriter writer, object graph)
        {
            WriteStartObjectHandleExceptions(new XmlWriterDelegator(writer), graph);
        }

        public override void WriteObjectContent(XmlWriter writer, object graph)
        {
            WriteObjectContentHandleExceptions(new XmlWriterDelegator(writer), graph);
        }

        public override void WriteEndObject(XmlWriter writer)
        {
            WriteEndObjectHandleExceptions(new XmlWriterDelegator(writer));
        }

        public override void WriteStartObject(XmlDictionaryWriter writer, object graph)
        {
            WriteStartObjectHandleExceptions(new XmlWriterDelegator(writer), graph);
        }

        internal override void InternalWriteStartObject(XmlWriterDelegator writer, object graph)
        {
            Hashtable surrogateDataContracts = null;
            DataContract contract = GetDataContract(graph, ref surrogateDataContracts);
            InternalWriteStartObject(writer, graph, contract);
        }

        private void InternalWriteStartObject(XmlWriterDelegator writer, object graph, DataContract contract)
        {
            WriteRootElement(writer, contract, rootName, rootNamespace, CheckIfNeedsContractNsAtRoot(rootName, rootNamespace, contract));
        }

        public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
        {
            WriteObjectContentHandleExceptions(new XmlWriterDelegator(writer), graph);
        }

        internal override void InternalWriteObjectContent(XmlWriterDelegator writer, object graph)
        {
            Hashtable surrogateDataContracts = null;
            DataContract contract = GetDataContract(graph, ref surrogateDataContracts);
            InternalWriteObjectContent(writer, graph, contract, surrogateDataContracts);
        }

        private void InternalWriteObjectContent(XmlWriterDelegator writer, object graph, DataContract contract, Hashtable surrogateDataContracts)
        {
            if (MaxItemsInObjectGraph == 0)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.ExceededMaxItemsQuota, MaxItemsInObjectGraph)));
            }

            if (IsRootXmlAny(rootName, contract))
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.IsAnyNotSupportedByNetDataContractSerializer, contract.UnderlyingType)));
            }
            else if (graph == null)
            {
                WriteNull(writer);
            }
            else
            {
                Type graphType = graph.GetType();
                if (contract.UnderlyingType != graphType)
                {
                    contract = GetDataContract(graph, ref surrogateDataContracts);
                }

                XmlObjectSerializerWriteContext context = null;
                if (contract.CanContainReferences)
                {
                    context = XmlObjectSerializerWriteContext.CreateContext(this, surrogateDataContracts);
                    context.HandleGraphAtTopLevel(writer, graph, contract);
                }

                WriteClrTypeInfo(writer, contract, binder);
                contract.WriteXmlValue(writer, graph, context);
            }
        }

        // Update the overloads whenever you are changing this method
        internal static void WriteClrTypeInfo(XmlWriterDelegator writer, DataContract dataContract, SerializationBinder binder)
        {
            if (!dataContract.IsISerializable && !(dataContract is SurrogateDataContract))
            {
                TypeInformation typeInformation = null;
                Type clrType = dataContract.OriginalUnderlyingType;
                string clrTypeName = null;
                string clrAssemblyName = null;

                if (binder != null)
                {
                    binder.BindToName(clrType, out clrAssemblyName, out clrTypeName);
                }

                if (clrTypeName == null)
                {
                    typeInformation = NetDataContractSerializer.GetTypeInformation(clrType);
                    clrTypeName = typeInformation.FullTypeName;
                }

                if (clrAssemblyName == null)
                {
                    clrAssemblyName = (typeInformation == null) ?
                        NetDataContractSerializer.GetTypeInformation(clrType).AssemblyString :
                        typeInformation.AssemblyString;

                    // Throw in the [TypeForwardedFrom] case to prevent a partially trusted assembly from forwarding itself to an assembly with higher privileges
                    if (!UnsafeTypeForwardingEnabled && !clrType.Assembly.IsFullyTrusted && !IsAssemblyNameForwardingSafe(clrType.Assembly.FullName, clrAssemblyName))
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.TypeCannotBeForwardedFrom, DataContract.GetClrTypeFullName(clrType), clrType.Assembly.FullName, clrAssemblyName)));
                    }
                }

                WriteClrTypeInfo(writer, clrTypeName, clrAssemblyName);
            }
        }

        public static bool UnsafeTypeForwardingEnabled => false;

        // Update the overloads whenever you are changing this method
        internal static void WriteClrTypeInfo(XmlWriterDelegator writer, Type dataContractType, SerializationBinder binder, string defaultClrTypeName, string defaultClrAssemblyName)
        {
            string clrTypeName = null;
            string clrAssemblyName = null;

            if (binder != null)
            {
                binder.BindToName(dataContractType, out clrAssemblyName, out clrTypeName);
            }

            if (clrTypeName == null)
            {
                clrTypeName = defaultClrTypeName;
            }

            if (clrAssemblyName == null)
            {
                clrAssemblyName = defaultClrAssemblyName;
            }

            WriteClrTypeInfo(writer, clrTypeName, clrAssemblyName);
        }

        // Update the overloads whenever you are changing this method
        internal static void WriteClrTypeInfo(
            XmlWriterDelegator writer,
            Type dataContractType,
            SerializationBinder binder,
            SerializationInfo serInfo)
        {
            TypeInformation typeInformation = null;
            string clrTypeName = null;
            string clrAssemblyName = null;

            if (binder != null)
            {
                binder.BindToName(dataContractType, out clrAssemblyName, out clrTypeName);
            }

            if (clrTypeName == null)
            {
                if (serInfo.IsFullTypeNameSetExplicit)
                {
                    clrTypeName = serInfo.FullTypeName;
                }
                else
                {
                    typeInformation = NetDataContractSerializer.GetTypeInformation(serInfo.ObjectType);
                    clrTypeName = typeInformation.FullTypeName;
                }
            }

            if (clrAssemblyName == null)
            {
                if (serInfo.IsAssemblyNameSetExplicit)
                {
                    clrAssemblyName = serInfo.AssemblyName;
                }
                else
                {
                    clrAssemblyName = (typeInformation == null) ?
                    NetDataContractSerializer.GetTypeInformation(serInfo.ObjectType).AssemblyString :
                    typeInformation.AssemblyString;
                }
            }

            WriteClrTypeInfo(writer, clrTypeName, clrAssemblyName);
        }

        private static void WriteClrTypeInfo(XmlWriterDelegator writer, string clrTypeName, string clrAssemblyName)
        {
            if (clrTypeName != null)
            {
                writer.WriteAttributeString(Globals.SerPrefix, DictionaryGlobals.ClrTypeLocalName, DictionaryGlobals.SerializationNamespace, DataContract.GetClrTypeString(clrTypeName));
            }

            if (clrAssemblyName != null)
            {
                writer.WriteAttributeString(Globals.SerPrefix, DictionaryGlobals.ClrAssemblyLocalName, DictionaryGlobals.SerializationNamespace, DataContract.GetClrTypeString(clrAssemblyName));
            }
        }

        public override void WriteEndObject(XmlDictionaryWriter writer)
        {
            WriteEndObjectHandleExceptions(new XmlWriterDelegator(writer));
        }

        internal override void InternalWriteEndObject(XmlWriterDelegator writer)
        {
            writer.WriteEndElement();
        }

        public override object ReadObject(XmlReader reader)
        {
            return ReadObjectHandleExceptions(new XmlReaderDelegator(reader), true /*verifyObjectName*/);
        }

        public override object ReadObject(XmlReader reader, bool verifyObjectName)
        {
            return ReadObjectHandleExceptions(new XmlReaderDelegator(reader), verifyObjectName);
        }

        public override bool IsStartObject(XmlReader reader)
        {
            return IsStartObjectHandleExceptions(new XmlReaderDelegator(reader));
        }

        public override object ReadObject(XmlDictionaryReader reader, bool verifyObjectName)
        {
            return ReadObjectHandleExceptions(new XmlReaderDelegator(reader), verifyObjectName);
        }

        public override bool IsStartObject(XmlDictionaryReader reader)
        {
            return IsStartObjectHandleExceptions(new XmlReaderDelegator(reader));
        }

        internal override object InternalReadObject(XmlReaderDelegator xmlReader, bool verifyObjectName)
        {
            if (MaxItemsInObjectGraph == 0)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.ExceededMaxItemsQuota, MaxItemsInObjectGraph)));
            }

            // verifyObjectName has no effect in SharedType mode
            if (!IsStartElement(xmlReader))
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationExceptionWithReaderDetails(SR.Format(SR.ExpectingElementAtDeserialize, XmlNodeType.Element), xmlReader));
            }

            XmlObjectSerializerReadContext context = XmlObjectSerializerReadContext.CreateContext(this);
            return context.InternalDeserialize(xmlReader, null, null, null);
        }

        internal override bool InternalIsStartObject(XmlReaderDelegator reader)
        {
            return IsStartElement(reader);
        }

        internal DataContract GetDataContract(object obj, ref Hashtable surrogateDataContracts)
        {
            return GetDataContract(((obj == null) ? Globals.TypeOfObject : obj.GetType()), ref surrogateDataContracts);
        }

        internal DataContract GetDataContract(Type type, ref Hashtable surrogateDataContracts)
        {
            return GetDataContract(type.TypeHandle, type, ref surrogateDataContracts);
        }

        internal DataContract GetDataContract(RuntimeTypeHandle typeHandle, Type type, ref Hashtable surrogateDataContracts)
        {
            DataContract dataContract = GetDataContractFromSurrogateSelector(surrogateSelector, Context, typeHandle, type, ref surrogateDataContracts);
            if (dataContract != null)
            {
                return dataContract;
            }

            if (cachedDataContract == null)
            {
                dataContract = DataContract.GetDataContract(typeHandle, type, SerializationMode.SharedType);
                cachedDataContract = dataContract;
                return dataContract;
            }

            DataContract currentCachedDataContract = cachedDataContract;
            if (currentCachedDataContract.UnderlyingType.TypeHandle.Equals(typeHandle))
            {
                return currentCachedDataContract;
            }

            return DataContract.GetDataContract(typeHandle, type, SerializationMode.SharedType);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static ISerializationSurrogate GetSurrogate(
            Type type,
            ISurrogateSelector surrogateSelector,
            StreamingContext context)
        {
            return surrogateSelector.GetSurrogate(type, context, out ISurrogateSelector surrogateSelectorNotUsed);
        }

        internal static DataContract GetDataContractFromSurrogateSelector(
            ISurrogateSelector surrogateSelector,
            StreamingContext context,
            RuntimeTypeHandle typeHandle,
            Type type,
            ref Hashtable surrogateDataContracts)
        {
            if (surrogateSelector == null)
            {
                return null;
            }

            if (type == null)
            {
                type = Type.GetTypeFromHandle(typeHandle);
            }

            DataContract builtInDataContract = DataContract.GetBuiltInDataContract(type);
            if (builtInDataContract != null)
            {
                return builtInDataContract;
            }

            if (surrogateDataContracts != null)
            {
                DataContract cachedSurrogateContract = (DataContract)surrogateDataContracts[type];
                if (cachedSurrogateContract != null)
                {
                    return cachedSurrogateContract;
                }
            }
            DataContract surrogateContract = null;
            ISerializationSurrogate surrogate = GetSurrogate(type, surrogateSelector, context);
            if (surrogate != null)
            {
                surrogateContract = new SurrogateDataContract(type, surrogate);
            }
            else if (type.IsArray)
            {
                Type elementType = type.GetElementType();
                DataContract itemContract = GetDataContractFromSurrogateSelector(surrogateSelector, context, elementType.TypeHandle, elementType, ref surrogateDataContracts);
                if (itemContract == null)
                {
                    itemContract = DataContract.GetDataContract(elementType.TypeHandle, elementType, SerializationMode.SharedType);
                }

                surrogateContract = new CollectionDataContract(type, itemContract);
            }
            if (surrogateContract != null)
            {
                if (surrogateDataContracts == null)
                {
                    surrogateDataContracts = new Hashtable();
                }

                surrogateDataContracts.Add(type, surrogateContract);
                return surrogateContract;
            }
            return null;
        }


        internal static TypeInformation GetTypeInformation(Type type)
        {
            TypeInformation typeInformation = null;
            object typeInformationObject = typeNameCache[type];
            if (typeInformationObject == null)
            {
                string assemblyName = DataContract.GetClrAssemblyName(type, out bool hasTypeForwardedFrom);
                typeInformation = new TypeInformation(DataContract.GetClrTypeFullNameUsingTypeForwardedFromAttribute(type), assemblyName, hasTypeForwardedFrom);
                lock (typeNameCache)
                {
                    typeNameCache[type] = typeInformation;
                }
            }
            else
            {
                typeInformation = (TypeInformation)typeInformationObject;
            }
            return typeInformation;
        }

        private static bool IsAssemblyNameForwardingSafe(string originalAssemblyName, string newAssemblyName)
        {
            if (originalAssemblyName == newAssemblyName)
            {
                return true;
            }

            AssemblyName originalAssembly = new AssemblyName(originalAssemblyName);
            AssemblyName newAssembly = new AssemblyName(newAssemblyName);

            // mscorlib will get loaded by the runtime regardless of its string casing or its public key token,
            // so setting the assembly name to mscorlib is always unsafe
            if (string.Equals(newAssembly.Name, Globals.MscorlibAssemblySimpleName, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(newAssembly.Name, Globals.MscorlibFileName, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return IsPublicKeyTokenForwardingSafe(originalAssembly.GetPublicKeyToken(), newAssembly.GetPublicKeyToken());
        }

        private static bool IsPublicKeyTokenForwardingSafe(byte[] sourceToken, byte[] destinationToken)
        {
            if (sourceToken == null || destinationToken == null || sourceToken.Length == 0 || destinationToken.Length == 0 || sourceToken.Length != destinationToken.Length)
            {
                return false;
            }

            for (int i = 0; i < sourceToken.Length; i++)
            {
                if (sourceToken[i] != destinationToken[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
