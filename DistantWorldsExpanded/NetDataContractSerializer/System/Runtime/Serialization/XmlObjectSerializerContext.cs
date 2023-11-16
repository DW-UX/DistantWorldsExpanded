using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Xml;

namespace System.Runtime.Serialization
{
    using DataContractDictionary = Dictionary<XmlQualifiedName, DataContract>;

    internal class XmlObjectSerializerContext
    {

        protected XmlObjectSerializer2 serializer;
        protected DataContract rootTypeDataContract;
        internal ScopedKnownTypes scopedKnownTypes = new ScopedKnownTypes();
        protected DataContractDictionary serializerKnownDataContracts;
        private bool isSerializerKnownDataContractsSetExplicit;
        protected IList<Type> serializerKnownTypeList;

        private int itemCount;
        private readonly int maxItemsInObjectGraph;
        private System.Runtime.Serialization.StreamingContext streamingContext;
        private readonly bool ignoreExtensionDataObject;
        private readonly System.Runtime.Serialization.DataContractResolver dataContractResolver;
        private KnownTypeDataContractResolver knownTypeResolver;

        internal XmlObjectSerializerContext(
            XmlObjectSerializer2 serializer, 
            int maxItemsInObjectGraph, 
            System.Runtime.Serialization.StreamingContext streamingContext, 
            bool ignoreExtensionDataObject, 
            System.Runtime.Serialization.DataContractResolver dataContractResolver)
        {
            this.serializer = serializer;
            itemCount = 1;
            this.maxItemsInObjectGraph = maxItemsInObjectGraph;
            this.streamingContext = streamingContext;
            this.ignoreExtensionDataObject = ignoreExtensionDataObject;
            this.dataContractResolver = dataContractResolver;
        }

        internal XmlObjectSerializerContext(XmlObjectSerializer2 serializer,
            int maxItemsInObjectGraph,
            System.Runtime.Serialization.StreamingContext streamingContext,
            bool ignoreExtensionDataObject)
            : this(serializer, maxItemsInObjectGraph, streamingContext, ignoreExtensionDataObject, null)
        {
        }

        internal XmlObjectSerializerContext(
            DataContractSerializer serializer,
            DataContract rootTypeDataContract,
            System.Runtime.Serialization.DataContractResolver dataContractResolver)
            : this(serializer,
            serializer.MaxItemsInObjectGraph,
            new System.Runtime.Serialization.StreamingContext(System.Runtime.Serialization.StreamingContextStates.All),
            serializer.IgnoreExtensionDataObject,
            dataContractResolver)
        {
            this.rootTypeDataContract = rootTypeDataContract;
            serializerKnownTypeList = serializer.knownTypeList;
        }

        internal XmlObjectSerializerContext(NetDataContractSerializer serializer)
            : this(serializer,
            serializer.MaxItemsInObjectGraph,
            serializer.Context,
            serializer.IgnoreExtensionDataObject)
        {
        }

        internal virtual SerializationMode Mode => SerializationMode.SharedContract;

        internal virtual bool IsGetOnlyCollection
        {
            get => false;
            set { }
        }

        public System.Runtime.Serialization.StreamingContext GetStreamingContext()
        {
            return streamingContext;
        }

        private static MethodInfo incrementItemCountMethod;
        internal static MethodInfo IncrementItemCountMethod
        {
            get
            {
                if (incrementItemCountMethod == null)
                {
                    incrementItemCountMethod = typeof(XmlObjectSerializerContext).GetMethod("IncrementItemCount", Globals.ScanAllMembers);
                }

                return incrementItemCountMethod;
            }
        }
        public void IncrementItemCount(int count)
        {
            if (count > maxItemsInObjectGraph - itemCount)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.ExceededMaxItemsQuota, maxItemsInObjectGraph)));
            }

            itemCount += count;
        }

        internal int RemainingItemCount => maxItemsInObjectGraph - itemCount;

        internal bool IgnoreExtensionDataObject => ignoreExtensionDataObject;

        protected System.Runtime.Serialization.DataContractResolver DataContractResolver => dataContractResolver;

        protected KnownTypeDataContractResolver KnownTypeResolver
        {
            get
            {
                if (knownTypeResolver == null)
                {
                    knownTypeResolver = new KnownTypeDataContractResolver(this);
                }
                return knownTypeResolver;
            }
        }

        internal DataContract GetDataContract(Type type)
        {
            return GetDataContract(type.TypeHandle, type);
        }

        internal virtual DataContract GetDataContract(RuntimeTypeHandle typeHandle, Type type)
        {
            if (IsGetOnlyCollection)
            {
                return DataContract.GetGetOnlyCollectionDataContract(DataContract.GetId(typeHandle), typeHandle, type, Mode);
            }
            else
            {
                return DataContract.GetDataContract(typeHandle, type, Mode);
            }
        }

        internal virtual DataContract GetDataContractSkipValidation(int typeId, RuntimeTypeHandle typeHandle, Type type)
        {
            if (IsGetOnlyCollection)
            {
                return DataContract.GetGetOnlyCollectionDataContractSkipValidation(typeId, typeHandle, type);
            }
            else
            {
                return DataContract.GetDataContractSkipValidation(typeId, typeHandle, type);
            }
        }


        internal virtual DataContract GetDataContract(int id, RuntimeTypeHandle typeHandle)
        {
            if (IsGetOnlyCollection)
            {
                return DataContract.GetGetOnlyCollectionDataContract(id, typeHandle, null /*type*/, Mode);
            }
            else
            {
                return DataContract.GetDataContract(id, typeHandle, Mode);
            }
        }

        internal virtual void CheckIfTypeSerializable(Type memberType, bool isMemberTypeSerializable)
        {
            if (!isMemberTypeSerializable)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new System.Runtime.Serialization.InvalidDataContractException(SR.Format(SR.TypeNotSerializable, memberType)));
            }
        }

        internal virtual Type GetSurrogatedType(Type type)
        {
            return type;
        }

        private DataContractDictionary SerializerKnownDataContracts
        {
            get
            {
                // This field must be initialized during construction by serializers using data contracts.
                if (!isSerializerKnownDataContractsSetExplicit)
                {
                    serializerKnownDataContracts = serializer.KnownDataContracts;
                    isSerializerKnownDataContractsSetExplicit = true;
                }
                return serializerKnownDataContracts;
            }
        }

        private DataContract GetDataContractFromSerializerKnownTypes(XmlQualifiedName qname)
        {
            DataContractDictionary serializerKnownDataContracts = SerializerKnownDataContracts;
            if (serializerKnownDataContracts == null)
            {
                return null;
            }

            return serializerKnownDataContracts.TryGetValue(qname, out DataContract outDataContract) ? outDataContract : null;
        }

        internal static DataContractDictionary GetDataContractsForKnownTypes(IList<Type> knownTypeList)
        {
            if (knownTypeList == null)
            {
                return null;
            }

            DataContractDictionary dataContracts = new DataContractDictionary();
            Dictionary<Type, Type> typesChecked = new Dictionary<Type, Type>();
            for (int i = 0; i < knownTypeList.Count; i++)
            {
                Type knownType = knownTypeList[i];
                if (knownType == null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.Format(SR.NullKnownType, "knownTypes")));
                }

                DataContract.CheckAndAdd(knownType, typesChecked, ref dataContracts);
            }
            return dataContracts;
        }

        internal bool IsKnownType(DataContract dataContract, DataContractDictionary knownDataContracts, Type declaredType)
        {
            bool knownTypesAddedInCurrentScope = false;
            if (knownDataContracts != null)
            {
                scopedKnownTypes.Push(knownDataContracts);
                knownTypesAddedInCurrentScope = true;
            }

            bool isKnownType = IsKnownType(dataContract, declaredType);

            if (knownTypesAddedInCurrentScope)
            {
                scopedKnownTypes.Pop();
            }
            return isKnownType;
        }

        internal bool IsKnownType(DataContract dataContract, Type declaredType)
        {
            DataContract knownContract = ResolveDataContractFromKnownTypes(dataContract.StableName.Name, dataContract.StableName.Namespace, null /*memberTypeContract*/, declaredType);
            return knownContract != null && knownContract.UnderlyingType == dataContract.UnderlyingType;
        }

        private DataContract ResolveDataContractFromKnownTypes(XmlQualifiedName typeName)
        {
            DataContract dataContract = PrimitiveDataContract.GetPrimitiveDataContract(typeName.Name, typeName.Namespace);
            if (dataContract == null)
            {
                dataContract = scopedKnownTypes.GetDataContract(typeName);
                if (dataContract == null)
                {
                    dataContract = GetDataContractFromSerializerKnownTypes(typeName);
                }
            }
            return dataContract;
        }

        private DataContract ResolveDataContractFromDataContractResolver(XmlQualifiedName typeName, Type declaredType)
        {
            Type dataContractType = DataContractResolver.ResolveName(typeName.Name, typeName.Namespace, declaredType, KnownTypeResolver);
            if (dataContractType == null)
            {
                return null;
            }
            else
            {
                return GetDataContract(dataContractType);
            }
        }

        internal Type ResolveNameFromKnownTypes(XmlQualifiedName typeName)
        {
            DataContract dataContract = ResolveDataContractFromKnownTypes(typeName);
            if (dataContract == null)
            {
                return null;
            }
            else
            {
                return dataContract.OriginalUnderlyingType;
            }
        }

        protected DataContract ResolveDataContractFromKnownTypes(string typeName, string typeNs, DataContract memberTypeContract, Type declaredType)
        {
            XmlQualifiedName qname = new XmlQualifiedName(typeName, typeNs);
            DataContract dataContract;
            if (DataContractResolver == null)
            {
                dataContract = ResolveDataContractFromKnownTypes(qname);
            }
            else
            {
                dataContract = ResolveDataContractFromDataContractResolver(qname, declaredType);
            }
            if (dataContract == null)
            {
                if (memberTypeContract != null
                    && !memberTypeContract.UnderlyingType.IsInterface
                    && memberTypeContract.StableName == qname)
                {
                    dataContract = memberTypeContract;
                }
                if (dataContract == null && rootTypeDataContract != null)
                {
                    dataContract = ResolveDataContractFromRootDataContract(qname);
                }
            }
            return dataContract;
        }

        protected virtual DataContract ResolveDataContractFromRootDataContract(XmlQualifiedName typeQName)
        {
            if (rootTypeDataContract.StableName == typeQName)
            {
                return rootTypeDataContract;
            }

            CollectionDataContract collectionContract = rootTypeDataContract as CollectionDataContract;
            while (collectionContract != null)
            {
                DataContract itemContract = GetDataContract(GetSurrogatedType(collectionContract.ItemType));
                if (itemContract.StableName == typeQName)
                {
                    return itemContract;
                }
                collectionContract = itemContract as CollectionDataContract;
            }
            return null;
        }

    }
}
