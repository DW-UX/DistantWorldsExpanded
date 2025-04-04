﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;
using DataContractResolver = System.Runtime.Serialization.DataContractResolver;

namespace System.Runtime.Serialization
{
    using DataContractDictionary = Dictionary<XmlQualifiedName, DataContract>;

    internal sealed class DataContractSerializer : XmlObjectSerializer2
    {
        private Type _rootType;
        private DataContract rootContract; // post-surrogate
        private bool needsContractNsAtRoot;
        private XmlDictionaryString rootName;
        private XmlDictionaryString rootNamespace;
        private int maxItemsInObjectGraph;
        private bool ignoreExtensionDataObject;
        private bool preserveObjectReferences;
        private IDataContractSurrogate dataContractSurrogate;
        private ReadOnlyCollection<Type> knownTypeCollection;
        internal IList<Type> knownTypeList;
        internal DataContractDictionary knownDataContracts;
        private DataContractResolver dataContractResolver;
        private bool serializeReadOnlyTypes;

        public DataContractSerializer(Type type)
            : this(type, (IEnumerable<Type>)null)
        {
        }

        public DataContractSerializer(Type type, IEnumerable<Type> knownTypes)
            : this(type, knownTypes, int.MaxValue, false, false, null)
        {
        }

        public DataContractSerializer(Type type,
            IEnumerable<Type> knownTypes,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            bool preserveObjectReferences,
            IDataContractSurrogate dataContractSurrogate)
            : this(type, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, null)
        {
        }

        public DataContractSerializer(Type type,
            IEnumerable<Type> knownTypes,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            bool preserveObjectReferences,
            IDataContractSurrogate dataContractSurrogate,
            DataContractResolver dataContractResolver)
        {
            Initialize(type, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, dataContractResolver, false);
        }

        public DataContractSerializer(Type type, string rootName, string rootNamespace)
            : this(type, rootName, rootNamespace, null)
        {
        }

        public DataContractSerializer(Type type, string rootName, string rootNamespace, IEnumerable<Type> knownTypes)
            : this(type, rootName, rootNamespace, knownTypes, int.MaxValue, false, false, null)
        {
        }

        public DataContractSerializer(Type type, string rootName, string rootNamespace,
            IEnumerable<Type> knownTypes,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            bool preserveObjectReferences,
            IDataContractSurrogate dataContractSurrogate)
            : this(type, rootName, rootNamespace, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, null)
        {
        }

        public DataContractSerializer(Type type, string rootName, string rootNamespace,
            IEnumerable<Type> knownTypes,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            bool preserveObjectReferences,
            IDataContractSurrogate dataContractSurrogate,
            DataContractResolver dataContractResolver)
        {
            XmlDictionary dictionary = new XmlDictionary(2);
            Initialize(type, dictionary.Add(rootName), dictionary.Add(DataContract.GetNamespace(rootNamespace)), knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, dataContractResolver, false);
        }

        public DataContractSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace)
            : this(type, rootName, rootNamespace, null)
        {
        }

        public DataContractSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace, IEnumerable<Type> knownTypes)
            : this(type, rootName, rootNamespace, knownTypes, int.MaxValue, false, false, null, null)
        {
        }

        public DataContractSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace,
            IEnumerable<Type> knownTypes,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            bool preserveObjectReferences,
            IDataContractSurrogate dataContractSurrogate)
            : this(type, rootName, rootNamespace, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, null)
        {
        }

        public DataContractSerializer(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace,
            IEnumerable<Type> knownTypes,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            bool preserveObjectReferences,
            IDataContractSurrogate dataContractSurrogate,
            DataContractResolver dataContractResolver)
        {
            Initialize(type, rootName, rootNamespace, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, dataContractResolver, false);
        }

        public DataContractSerializer(Type type, DataContractSerializerSettings settings)
        {
            if (settings == null)
            {
                settings = new DataContractSerializerSettings();
            }
            Initialize(type, settings.RootName, settings.RootNamespace, settings.KnownTypes, settings.MaxItemsInObjectGraph, settings.IgnoreExtensionDataObject,
                settings.PreserveObjectReferences, settings.DataContractSurrogate, settings.DataContractResolver, settings.SerializeReadOnlyTypes);
        }

        private void Initialize(
            Type type,
            IEnumerable<Type> knownTypes,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            bool preserveObjectReferences,
            IDataContractSurrogate dataContractSurrogate,
            DataContractResolver dataContractResolver,
            bool serializeReadOnlyTypes)
        {
            _rootType = type ?? throw new ArgumentNullException(nameof(type));

            if (knownTypes != null)
            {
                knownTypeList = new List<Type>();
                foreach (Type knownType in knownTypes)
                {
                    knownTypeList.Add(knownType);
                }
            }

            if (maxItemsInObjectGraph < 0)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException(nameof(maxItemsInObjectGraph), SR.ValueMustBeNonNegative));
            }

            this.maxItemsInObjectGraph = maxItemsInObjectGraph;

            this.ignoreExtensionDataObject = ignoreExtensionDataObject;
            this.preserveObjectReferences = preserveObjectReferences;
            this.dataContractSurrogate = dataContractSurrogate;
            this.dataContractResolver = dataContractResolver;
            this.serializeReadOnlyTypes = serializeReadOnlyTypes;
        }

        private void Initialize(Type type, XmlDictionaryString rootName, XmlDictionaryString rootNamespace,
            IEnumerable<Type> knownTypes,
            int maxItemsInObjectGraph,
            bool ignoreExtensionDataObject,
            bool preserveObjectReferences,
            IDataContractSurrogate dataContractSurrogate,
            DataContractResolver dataContractResolver,
            bool serializeReadOnlyTypes)
        {
            Initialize(type, knownTypes, maxItemsInObjectGraph, ignoreExtensionDataObject, preserveObjectReferences, dataContractSurrogate, dataContractResolver, serializeReadOnlyTypes);
            // validate root name and namespace are both non-null
            this.rootName = rootName;
            this.rootNamespace = rootNamespace;
        }

        public ReadOnlyCollection<Type> KnownTypes
        {
            get
            {
                if (knownTypeCollection == null)
                {
                    if (knownTypeList != null)
                    {
                        knownTypeCollection = new ReadOnlyCollection<Type>(knownTypeList);
                    }
                    else
                    {
                        knownTypeCollection = new ReadOnlyCollection<Type>(Globals.EmptyTypeArray);
                    }
                }
                return knownTypeCollection;
            }
        }

        internal override DataContractDictionary KnownDataContracts
        {
            get
            {
                if (knownDataContracts == null && knownTypeList != null)
                {
                    // This assignment may be performed concurrently and thus is a race condition.
                    // It's safe, however, because at worse a new (and identical) dictionary of 
                    // data contracts will be created and re-assigned to this field.  Introduction 
                    // of a lock here could lead to deadlocks.
                    knownDataContracts = XmlObjectSerializerContext.GetDataContractsForKnownTypes(knownTypeList);
                }
                return knownDataContracts;
            }
        }

        public int MaxItemsInObjectGraph => maxItemsInObjectGraph;

        public IDataContractSurrogate DataContractSurrogate => dataContractSurrogate;

        public bool PreserveObjectReferences => preserveObjectReferences;

        public bool IgnoreExtensionDataObject => ignoreExtensionDataObject;

        public DataContractResolver DataContractResolver => dataContractResolver;

        public bool SerializeReadOnlyTypes => serializeReadOnlyTypes;

        private DataContract RootContract
        {
            get
            {
                if (rootContract == null)
                {
                    rootContract = DataContract.GetDataContract(((dataContractSurrogate == null) ? _rootType : GetSurrogatedType(dataContractSurrogate, _rootType)));
                    needsContractNsAtRoot = CheckIfNeedsContractNsAtRoot(rootName, rootNamespace, rootContract);
                }
                return rootContract;
            }
        }

        internal override void InternalWriteObject(XmlWriterDelegator writer, object graph)
        {
            InternalWriteObject(writer, graph, null);
        }

        internal override void InternalWriteObject(XmlWriterDelegator writer, object graph, DataContractResolver dataContractResolver)
        {
            InternalWriteStartObject(writer, graph);
            InternalWriteObjectContent(writer, graph, dataContractResolver);
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

        public override void WriteObjectContent(XmlDictionaryWriter writer, object graph)
        {
            WriteObjectContentHandleExceptions(new XmlWriterDelegator(writer), graph);
        }

        public override void WriteEndObject(XmlDictionaryWriter writer)
        {
            WriteEndObjectHandleExceptions(new XmlWriterDelegator(writer));
        }

        public void WriteObject(XmlDictionaryWriter writer, object graph, DataContractResolver dataContractResolver)
        {
            WriteObjectHandleExceptions(new XmlWriterDelegator(writer), graph, dataContractResolver);
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

        public object ReadObject(XmlDictionaryReader reader, bool verifyObjectName, DataContractResolver dataContractResolver)
        {
            return ReadObjectHandleExceptions(new XmlReaderDelegator(reader), verifyObjectName, dataContractResolver);
        }

        internal override void InternalWriteStartObject(XmlWriterDelegator writer, object graph)
        {
            WriteRootElement(writer, RootContract, rootName, rootNamespace, needsContractNsAtRoot);
        }

        internal override void InternalWriteObjectContent(XmlWriterDelegator writer, object graph)
        {
            InternalWriteObjectContent(writer, graph, null);
        }

        internal void InternalWriteObjectContent(XmlWriterDelegator writer, object graph, DataContractResolver dataContractResolver)
        {
            if (MaxItemsInObjectGraph == 0)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.ExceededMaxItemsQuota, MaxItemsInObjectGraph)));
            }

            DataContract contract = RootContract;
            Type declaredType = contract.UnderlyingType;
            Type graphType = (graph == null) ? declaredType : graph.GetType();

            if (dataContractSurrogate != null)
            {
                graph = SurrogateToDataContractType(dataContractSurrogate, graph, declaredType, ref graphType);
            }

            if (dataContractResolver == null)
            {
                dataContractResolver = DataContractResolver;
            }

            if (graph == null)
            {
                if (IsRootXmlAny(rootName, contract))
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.IsAnyCannotBeNull, declaredType)));
                }

                WriteNull(writer);
            }
            else
            {
                if (declaredType == graphType)
                {
                    if (contract.CanContainReferences)
                    {
                        XmlObjectSerializerWriteContext context = XmlObjectSerializerWriteContext.CreateContext(this, contract, dataContractResolver);
                        context.HandleGraphAtTopLevel(writer, graph, contract);
                        context.SerializeWithoutXsiType(contract, writer, graph, declaredType.TypeHandle);
                    }
                    else
                    {
                        contract.WriteXmlValue(writer, graph, null);
                    }
                }
                else
                {
                    XmlObjectSerializerWriteContext context = null;
                    if (IsRootXmlAny(rootName, contract))
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.IsAnyCannotBeSerializedAsDerivedType, graphType, contract.UnderlyingType)));
                    }

                    contract = GetDataContract(contract, declaredType, graphType);
                    context = XmlObjectSerializerWriteContext.CreateContext(this, RootContract, dataContractResolver);
                    if (contract.CanContainReferences)
                    {
                        context.HandleGraphAtTopLevel(writer, graph, contract);
                    }
                    context.OnHandleIsReference(writer, contract, graph);
                    context.SerializeWithXsiTypeAtTopLevel(contract, writer, graph, declaredType.TypeHandle, graphType);
                }
            }
        }

        internal static DataContract GetDataContract(DataContract declaredTypeContract, Type declaredType, Type objectType)
        {
            if (declaredType.IsInterface && CollectionDataContract.IsCollectionInterface(declaredType))
            {
                return declaredTypeContract;
            }
            else if (declaredType.IsArray)//Array covariance is not supported in XSD
            {
                return declaredTypeContract;
            }
            else
            {
                return DataContract.GetDataContract(objectType.TypeHandle, objectType, SerializationMode.SharedContract);
            }
        }

        internal void SetDataContractSurrogate(IDataContractSurrogate adapter)
        {
            dataContractSurrogate = adapter;
        }

        internal override void InternalWriteEndObject(XmlWriterDelegator writer)
        {
            if (!IsRootXmlAny(rootName, RootContract))
            {
                writer.WriteEndElement();
            }
        }

        internal override object InternalReadObject(XmlReaderDelegator xmlReader, bool verifyObjectName)
        {
            return InternalReadObject(xmlReader, verifyObjectName, null);
        }

        internal override object InternalReadObject(XmlReaderDelegator xmlReader, bool verifyObjectName, DataContractResolver dataContractResolver)
        {
            if (MaxItemsInObjectGraph == 0)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.ExceededMaxItemsQuota, MaxItemsInObjectGraph)));
            }

            if (dataContractResolver == null)
            {
                dataContractResolver = DataContractResolver;
            }

            if (verifyObjectName)
            {
                if (!InternalIsStartObject(xmlReader))
                {
                    XmlDictionaryString expectedName;
                    XmlDictionaryString expectedNs;
                    if (rootName == null)
                    {
                        expectedName = RootContract.TopLevelElementName;
                        expectedNs = RootContract.TopLevelElementNamespace;
                    }
                    else
                    {
                        expectedName = rootName;
                        expectedNs = rootNamespace;
                    }
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationExceptionWithReaderDetails(SR.Format(SR.ExpectingElement, expectedNs, expectedName), xmlReader));
                }
            }
            else if (!IsStartElement(xmlReader))
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationExceptionWithReaderDetails(SR.Format(SR.ExpectingElementAtDeserialize, XmlNodeType.Element), xmlReader));
            }

            DataContract contract = RootContract;
            if (contract.IsPrimitive && object.ReferenceEquals(contract.UnderlyingType, _rootType) /*handle Nullable<T> differently*/)
            {
                return contract.ReadXmlValue(xmlReader, null);
            }

            if (IsRootXmlAny(rootName, contract))
            {
                return XmlObjectSerializerReadContext.ReadRootIXmlSerializable(xmlReader, contract as XmlDataContract, false /*isMemberType*/);
            }

            XmlObjectSerializerReadContext context = XmlObjectSerializerReadContext.CreateContext(this, contract, dataContractResolver);
            return context.InternalDeserialize(xmlReader, _rootType, contract, null, null);
        }

        internal override bool InternalIsStartObject(XmlReaderDelegator reader)
        {
            return IsRootElement(reader, RootContract, rootName, rootNamespace);
        }

        internal override Type GetSerializeType(object graph)
        {
            return (graph == null) ? _rootType : graph.GetType();
        }

        internal override Type GetDeserializeType()
        {
            return _rootType;
        }

        internal static object SurrogateToDataContractType(IDataContractSurrogate dataContractSurrogate, object oldObj, Type surrogatedDeclaredType, ref Type objType)
        {
            object obj = DataContractSurrogateCaller.GetObjectToSerialize(dataContractSurrogate, oldObj, objType, surrogatedDeclaredType);
            if (obj != oldObj)
            {
                if (obj == null)
                {
                    objType = Globals.TypeOfObject;
                }
                else
                {
                    objType = obj.GetType();
                }
            }
            return obj;
        }

        internal static Type GetSurrogatedType(IDataContractSurrogate dataContractSurrogate, Type type)
        {
            return DataContractSurrogateCaller.GetDataContractType(dataContractSurrogate, DataContract.UnwrapNullableType(type));
        }

    }
}
