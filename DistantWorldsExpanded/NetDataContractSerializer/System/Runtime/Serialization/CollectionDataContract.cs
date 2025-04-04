﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Xml;
using CollectionDataContractAttribute = System.Runtime.Serialization.CollectionDataContractAttribute;
using DataContractAttribute = System.Runtime.Serialization.DataContractAttribute;
using DataMemberAttribute = System.Runtime.Serialization.DataMemberAttribute;
using InvalidDataContractException = System.Runtime.Serialization.InvalidDataContractException;

namespace System.Runtime.Serialization
{
    using DataContractDictionary = Dictionary<XmlQualifiedName, DataContract>;

    [DataContract(Namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays")]
    internal struct KeyValue<K, V>
    {
        private K _key;
        private V _value;

        internal KeyValue(K key, V value)
        {
            _key = key;
            _value = value;
        }

        [DataMember(IsRequired = true)]
        public K Key
        {
            get => _key;
            set => _key = value;
        }

        [DataMember(IsRequired = true)]
        public V Value
        {
            get => _value;
            set => _value = value;
        }
    }

    internal enum CollectionKind : byte
    {
        None,
        GenericDictionary,
        Dictionary,
        GenericList,
        GenericCollection,
        List,
        GenericEnumerable,
        Collection,
        Enumerable,
        Array,
    }

    internal sealed class CollectionDataContract : DataContract
    {
        private XmlDictionaryString collectionItemName;

        private XmlDictionaryString childElementNamespace;

        private DataContract itemContract;

        private CollectionDataContractCriticalHelper helper;

        internal CollectionDataContract(CollectionKind kind)
            : base(new CollectionDataContractCriticalHelper(kind))
        {
            InitCollectionDataContract(this);
        }

        internal CollectionDataContract(Type type)
            : base(new CollectionDataContractCriticalHelper(type))
        {
            InitCollectionDataContract(this);
        }

        internal CollectionDataContract(Type type, DataContract itemContract)
            : base(new CollectionDataContractCriticalHelper(type, itemContract))
        {
            InitCollectionDataContract(this);
        }


        private CollectionDataContract(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, string serializationExceptionMessage, string deserializationExceptionMessage)
            : base(new CollectionDataContractCriticalHelper(type, kind, itemType, getEnumeratorMethod, serializationExceptionMessage, deserializationExceptionMessage))
        {
            InitCollectionDataContract(GetSharedTypeContract(type));
        }

        private CollectionDataContract(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, MethodInfo addMethod, ConstructorInfo constructor)
            : base(new CollectionDataContractCriticalHelper(type, kind, itemType, getEnumeratorMethod, addMethod, constructor))
        {
            InitCollectionDataContract(GetSharedTypeContract(type));
        }

        private CollectionDataContract(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, MethodInfo addMethod, ConstructorInfo constructor, bool isConstructorCheckRequired)
            : base(new CollectionDataContractCriticalHelper(type, kind, itemType, getEnumeratorMethod, addMethod, constructor, isConstructorCheckRequired))
        {
            InitCollectionDataContract(GetSharedTypeContract(type));
        }

        private CollectionDataContract(Type type, string invalidCollectionInSharedContractMessage)
            : base(new CollectionDataContractCriticalHelper(type, invalidCollectionInSharedContractMessage))
        {
            InitCollectionDataContract(GetSharedTypeContract(type));
        }

        private void InitCollectionDataContract(DataContract sharedTypeContract)
        {
            helper = base.Helper as CollectionDataContractCriticalHelper;
            collectionItemName = helper.CollectionItemName;
            if (helper.Kind == CollectionKind.Dictionary || helper.Kind == CollectionKind.GenericDictionary)
            {
                itemContract = helper.ItemContract;
            }
            helper.SharedTypeContract = sharedTypeContract;
        }

        private void InitSharedTypeContract()
        {
        }

        private static Type[] KnownInterfaces => CollectionDataContractCriticalHelper.KnownInterfaces;

        internal CollectionKind Kind => helper.Kind;

        internal Type ItemType => helper.ItemType;

        public DataContract ItemContract
        {
            get => itemContract ?? helper.ItemContract;

            set
            {
                itemContract = value;
                helper.ItemContract = value;
            }
        }

        internal DataContract SharedTypeContract => helper.SharedTypeContract;

        internal string ItemName
        {
            get => helper.ItemName;
            set => helper.ItemName = value;
        }

        public XmlDictionaryString CollectionItemName => collectionItemName;

        internal string KeyName
        {
            get => helper.KeyName;

            set => helper.KeyName = value;
        }

        internal string ValueName
        {
            get => helper.ValueName;
            set => helper.ValueName = value;
        }

        internal bool IsDictionary => KeyName != null;

        public XmlDictionaryString ChildElementNamespace
        {
            get
            {
                if (childElementNamespace == null)
                {
                    lock (this)
                    {
                        if (childElementNamespace == null)
                        {
                            if (helper.ChildElementNamespace == null && !IsDictionary)
                            {
                                XmlDictionaryString tempChildElementNamespace = ClassDataContract.GetChildNamespaceToDeclare(this, ItemType, new XmlDictionary());
                                Thread.MemoryBarrier();
                                helper.ChildElementNamespace = tempChildElementNamespace;
                            }
                            childElementNamespace = helper.ChildElementNamespace;
                        }
                    }
                }
                return childElementNamespace;
            }
        }

        internal bool IsItemTypeNullable
        {
            get => helper.IsItemTypeNullable;
            set => helper.IsItemTypeNullable = value;
        }

        internal bool IsConstructorCheckRequired
        {
            get => helper.IsConstructorCheckRequired;
            set => helper.IsConstructorCheckRequired = value;
        }

        internal MethodInfo GetEnumeratorMethod => helper.GetEnumeratorMethod;

        internal MethodInfo AddMethod => helper.AddMethod;

        internal ConstructorInfo Constructor => helper.Constructor;

        internal override DataContractDictionary KnownDataContracts
        {
            get => helper.KnownDataContracts;
            set => helper.KnownDataContracts = value;
        }

        internal string InvalidCollectionInSharedContractMessage => helper.InvalidCollectionInSharedContractMessage;

        internal string SerializationExceptionMessage => helper.SerializationExceptionMessage;

        internal string DeserializationExceptionMessage => helper.DeserializationExceptionMessage;

        internal bool IsReadOnlyContract => DeserializationExceptionMessage != null;

        private bool ItemNameSetExplicit => helper.ItemNameSetExplicit;

        internal XmlFormatCollectionWriterDelegate XmlFormatWriterDelegate
        {
            get
            {
                if (helper.XmlFormatWriterDelegate == null)
                {
                    lock (this)
                    {
                        if (helper.XmlFormatWriterDelegate == null)
                        {
                            XmlFormatCollectionWriterDelegate tempDelegate = new XmlFormatWriterGenerator().GenerateCollectionWriter(this);
                            Thread.MemoryBarrier();
                            helper.XmlFormatWriterDelegate = tempDelegate;
                        }
                    }
                }
                return helper.XmlFormatWriterDelegate;
            }
        }

        internal XmlFormatCollectionReaderDelegate XmlFormatReaderDelegate
        {
            get
            {
                if (helper.XmlFormatReaderDelegate == null)
                {
                    lock (this)
                    {
                        if (helper.XmlFormatReaderDelegate == null)
                        {
                            if (IsReadOnlyContract)
                            {
                                ThrowInvalidDataContractException(helper.DeserializationExceptionMessage, null /*type*/);
                            }
                            XmlFormatCollectionReaderDelegate tempDelegate = new XmlFormatReaderGenerator().GenerateCollectionReader(this);
                            Thread.MemoryBarrier();
                            helper.XmlFormatReaderDelegate = tempDelegate;
                        }
                    }
                }
                return helper.XmlFormatReaderDelegate;
            }
        }

        internal XmlFormatGetOnlyCollectionReaderDelegate XmlFormatGetOnlyCollectionReaderDelegate
        {
            get
            {
                if (helper.XmlFormatGetOnlyCollectionReaderDelegate == null)
                {
                    lock (this)
                    {
                        if (helper.XmlFormatGetOnlyCollectionReaderDelegate == null)
                        {
                            if (UnderlyingType.IsInterface && (Kind == CollectionKind.Enumerable || Kind == CollectionKind.Collection || Kind == CollectionKind.GenericEnumerable))
                            {
                                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.GetOnlyCollectionMustHaveAddMethod, DataContract.GetClrTypeFullName(UnderlyingType))));
                            }
                            if (IsReadOnlyContract)
                            {
                                ThrowInvalidDataContractException(helper.DeserializationExceptionMessage, null /*type*/);
                            }
                            Fx.Assert(AddMethod != null || Kind == CollectionKind.Array, "Add method cannot be null if the collection is being used as a get-only property");
                            XmlFormatGetOnlyCollectionReaderDelegate tempDelegate = new XmlFormatReaderGenerator().GenerateGetOnlyCollectionReader(this);
                            Thread.MemoryBarrier();
                            helper.XmlFormatGetOnlyCollectionReaderDelegate = tempDelegate;
                        }
                    }
                }
                return helper.XmlFormatGetOnlyCollectionReaderDelegate;
            }
        }

        private class CollectionDataContractCriticalHelper : DataContract.DataContractCriticalHelper
        {
            private static Type[] _knownInterfaces;
            private Type itemType;
            private bool isItemTypeNullable;
            private CollectionKind kind;
            private readonly MethodInfo getEnumeratorMethod, addMethod;
            private readonly ConstructorInfo constructor;
            private readonly string serializationExceptionMessage, deserializationExceptionMessage;
            private DataContract itemContract;
            private DataContract sharedTypeContract;
            private DataContractDictionary knownDataContracts;
            private bool isKnownTypeAttributeChecked;
            private string itemName;
            private bool itemNameSetExplicit;
            private XmlDictionaryString collectionItemName;
            private string keyName;
            private string valueName;
            private XmlDictionaryString childElementNamespace;
            private readonly string invalidCollectionInSharedContractMessage;
            private XmlFormatCollectionReaderDelegate xmlFormatReaderDelegate;
            private XmlFormatGetOnlyCollectionReaderDelegate xmlFormatGetOnlyCollectionReaderDelegate;
            private XmlFormatCollectionWriterDelegate xmlFormatWriterDelegate;
            private bool isConstructorCheckRequired = false;

            internal static Type[] KnownInterfaces
            {
                get
                {
                    if (_knownInterfaces == null)
                    {
                        // Listed in priority order
                        _knownInterfaces = new Type[]
                    {
                        Globals.TypeOfIDictionaryGeneric,
                        Globals.TypeOfIDictionary,
                        Globals.TypeOfIListGeneric,
                        Globals.TypeOfICollectionGeneric,
                        Globals.TypeOfIList,
                        Globals.TypeOfIEnumerableGeneric,
                        Globals.TypeOfICollection,
                        Globals.TypeOfIEnumerable
                    };
                    }
                    return _knownInterfaces;
                }
            }

            private void Init(CollectionKind kind, Type itemType, CollectionDataContractAttribute collectionContractAttribute)
            {
                this.kind = kind;
                if (itemType != null)
                {
                    this.itemType = itemType;
                    isItemTypeNullable = DataContract.IsTypeNullable(itemType);

                    bool isDictionary = (kind == CollectionKind.Dictionary || kind == CollectionKind.GenericDictionary);
                    string itemName = null, keyName = null, valueName = null;
                    if (collectionContractAttribute != null)
                    {
                        if (collectionContractAttribute.IsItemNameSetExplicitly)
                        {
                            if (collectionContractAttribute.ItemName == null || collectionContractAttribute.ItemName.Length == 0)
                            {
                                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.InvalidCollectionContractItemName, DataContract.GetClrTypeFullName(UnderlyingType))));
                            }

                            itemName = DataContract.EncodeLocalName(collectionContractAttribute.ItemName);
                            itemNameSetExplicit = true;
                        }
                        if (collectionContractAttribute.IsKeyNameSetExplicitly)
                        {
                            if (collectionContractAttribute.KeyName == null || collectionContractAttribute.KeyName.Length == 0)
                            {
                                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.InvalidCollectionContractKeyName, DataContract.GetClrTypeFullName(UnderlyingType))));
                            }

                            if (!isDictionary)
                            {
                                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.InvalidCollectionContractKeyNoDictionary, DataContract.GetClrTypeFullName(UnderlyingType), collectionContractAttribute.KeyName)));
                            }

                            keyName = DataContract.EncodeLocalName(collectionContractAttribute.KeyName);
                        }
                        if (collectionContractAttribute.IsValueNameSetExplicitly)
                        {
                            if (collectionContractAttribute.ValueName == null || collectionContractAttribute.ValueName.Length == 0)
                            {
                                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.InvalidCollectionContractValueName, DataContract.GetClrTypeFullName(UnderlyingType))));
                            }

                            if (!isDictionary)
                            {
                                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.InvalidCollectionContractValueNoDictionary, DataContract.GetClrTypeFullName(UnderlyingType), collectionContractAttribute.ValueName)));
                            }

                            valueName = DataContract.EncodeLocalName(collectionContractAttribute.ValueName);
                        }
                    }

                    XmlDictionary dictionary = isDictionary ? new XmlDictionary(5) : new XmlDictionary(3);
                    Name = dictionary.Add(StableName.Name);
                    Namespace = dictionary.Add(StableName.Namespace);
                    this.itemName = itemName ?? DataContract.GetStableName(DataContract.UnwrapNullableType(itemType)).Name;
                    collectionItemName = dictionary.Add(this.itemName);
                    if (isDictionary)
                    {
                        this.keyName = keyName ?? Globals.KeyLocalName;
                        this.valueName = valueName ?? Globals.ValueLocalName;
                    }
                }
                if (collectionContractAttribute != null)
                {
                    IsReference = collectionContractAttribute.IsReference;
                }
            }

            internal CollectionDataContractCriticalHelper(CollectionKind kind)
                : base()
            {
                Init(kind, null, null);
            }

            // array
            internal CollectionDataContractCriticalHelper(Type type)
                : base(type)
            {
                if (type == Globals.TypeOfArray)
                {
                    type = Globals.TypeOfObjectArray;
                }

                if (type.GetArrayRank() > 1)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.SupportForMultidimensionalArraysNotPresent));
                }

                StableName = DataContract.GetStableName(type);
                Init(CollectionKind.Array, type.GetElementType(), null);
            }

            // array
            internal CollectionDataContractCriticalHelper(Type type, DataContract itemContract)
                : base(type)
            {
                if (type.GetArrayRank() > 1)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException(SR.SupportForMultidimensionalArraysNotPresent));
                }

                StableName = CreateQualifiedName(Globals.ArrayPrefix + itemContract.StableName.Name, itemContract.StableName.Namespace);
                this.itemContract = itemContract;
                Init(CollectionKind.Array, type.GetElementType(), null);
            }

            // read-only collection
            internal CollectionDataContractCriticalHelper(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, string serializationExceptionMessage, string deserializationExceptionMessage)
                : base(type)
            {
                if (getEnumeratorMethod == null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.CollectionMustHaveGetEnumeratorMethod, DataContract.GetClrTypeFullName(type))));
                }

                if (itemType == null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.CollectionMustHaveItemType, DataContract.GetClrTypeFullName(type))));
                }

                StableName = DataContract.GetCollectionStableName(type, itemType, out CollectionDataContractAttribute collectionContractAttribute);

                Init(kind, itemType, collectionContractAttribute);
                this.getEnumeratorMethod = getEnumeratorMethod;
                this.serializationExceptionMessage = serializationExceptionMessage;
                this.deserializationExceptionMessage = deserializationExceptionMessage;
            }

            // collection
            internal CollectionDataContractCriticalHelper(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, MethodInfo addMethod, ConstructorInfo constructor)
                : this(type, kind, itemType, getEnumeratorMethod, null, (string)null)
            {
                if (addMethod == null && !type.IsInterface)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.CollectionMustHaveAddMethod, DataContract.GetClrTypeFullName(type))));
                }

                this.addMethod = addMethod;
                this.constructor = constructor;
            }

            // collection
            internal CollectionDataContractCriticalHelper(Type type, CollectionKind kind, Type itemType, MethodInfo getEnumeratorMethod, MethodInfo addMethod, ConstructorInfo constructor, bool isConstructorCheckRequired)
                : this(type, kind, itemType, getEnumeratorMethod, addMethod, constructor)
            {
                this.isConstructorCheckRequired = isConstructorCheckRequired;
            }

            internal CollectionDataContractCriticalHelper(Type type, string invalidCollectionInSharedContractMessage)
                : base(type)
            {
                Init(CollectionKind.Collection, null /*itemType*/, null);
                this.invalidCollectionInSharedContractMessage = invalidCollectionInSharedContractMessage;
            }

            internal CollectionKind Kind => kind;

            internal Type ItemType => itemType;

            internal DataContract ItemContract
            {
                get
                {
                    if (itemContract == null && UnderlyingType != null)
                    {
                        if (IsDictionary)
                        {
                            if (string.CompareOrdinal(KeyName, ValueName) == 0)
                            {
                                DataContract.ThrowInvalidDataContractException(
                                    SR.Format(SR.DupKeyValueName, DataContract.GetClrTypeFullName(UnderlyingType), KeyName),
                                    UnderlyingType);
                            }
                            itemContract = ClassDataContract.CreateClassDataContractForKeyValue(ItemType, Namespace, new string[] { KeyName, ValueName });
                            // Ensure that DataContract gets added to the static DataContract cache for dictionary items
                            DataContract.GetDataContract(ItemType);
                        }
                        else
                        {
                            itemContract = DataContract.GetDataContract(ItemType);
                        }
                    }
                    return itemContract;
                }
                set => itemContract = value;
            }

            internal DataContract SharedTypeContract
            {
                get => sharedTypeContract;
                set => sharedTypeContract = value;
            }

            internal string ItemName
            {
                get => itemName;
                set => itemName = value;
            }

            internal bool IsConstructorCheckRequired
            {
                get => isConstructorCheckRequired;
                set => isConstructorCheckRequired = value;
            }

            public XmlDictionaryString CollectionItemName => collectionItemName;

            internal string KeyName
            {
                get => keyName;
                set => keyName = value;
            }

            internal string ValueName
            {
                get => valueName;
                set => valueName = value;
            }

            internal bool IsDictionary => KeyName != null;

            public string SerializationExceptionMessage => serializationExceptionMessage;

            public string DeserializationExceptionMessage => deserializationExceptionMessage;

            public XmlDictionaryString ChildElementNamespace
            {
                get => childElementNamespace;
                set => childElementNamespace = value;
            }

            internal bool IsItemTypeNullable
            {
                get => isItemTypeNullable;
                set => isItemTypeNullable = value;
            }

            internal MethodInfo GetEnumeratorMethod => getEnumeratorMethod;

            internal MethodInfo AddMethod => addMethod;

            internal ConstructorInfo Constructor => constructor;

            internal override DataContractDictionary KnownDataContracts
            {
                get
                {
                    if (!isKnownTypeAttributeChecked && UnderlyingType != null)
                    {
                        lock (this)
                        {
                            if (!isKnownTypeAttributeChecked)
                            {
                                knownDataContracts = DataContract.ImportKnownTypeAttributes(UnderlyingType);
                                Thread.MemoryBarrier();
                                isKnownTypeAttributeChecked = true;
                            }
                        }
                    }
                    return knownDataContracts;
                }
                set => knownDataContracts = value;
            }

            internal string InvalidCollectionInSharedContractMessage => invalidCollectionInSharedContractMessage;

            internal bool ItemNameSetExplicit => itemNameSetExplicit;

            internal XmlFormatCollectionWriterDelegate XmlFormatWriterDelegate
            {
                get => xmlFormatWriterDelegate;
                set => xmlFormatWriterDelegate = value;
            }

            internal XmlFormatCollectionReaderDelegate XmlFormatReaderDelegate
            {
                get => xmlFormatReaderDelegate;
                set => xmlFormatReaderDelegate = value;
            }

            internal XmlFormatGetOnlyCollectionReaderDelegate XmlFormatGetOnlyCollectionReaderDelegate
            {
                get => xmlFormatGetOnlyCollectionReaderDelegate;
                set => xmlFormatGetOnlyCollectionReaderDelegate = value;
            }
        }

        private DataContract GetSharedTypeContract(Type type)
        {
            if (type.IsDefined(Globals.TypeOfCollectionDataContractAttribute, false))
            {
                return this;
            }
            // ClassDataContract.IsNonAttributedTypeValidForSerialization does not need to be called here. It should
            // never pass because it returns false for types that implement any of CollectionDataContract.KnownInterfaces
            if (type.IsSerializable || type.IsDefined(Globals.TypeOfDataContractAttribute, false))
            {
                return new ClassDataContract(type);
            }
            return null;
        }

        internal static bool IsCollectionInterface(Type type)
        {
            if (type.IsGenericType)
            {
                type = type.GetGenericTypeDefinition();
            }

            return ((IList<Type>)KnownInterfaces).Contains(type);
        }

        internal static bool IsCollection(Type type)
        {
            return IsCollection(type, out Type itemType);
        }

        internal static bool IsCollection(Type type, out Type itemType)
        {
            return IsCollectionHelper(type, out itemType, true /*constructorRequired*/);
        }

        internal static bool IsCollection(Type type, bool constructorRequired, bool skipIfReadOnlyContract)
        {
            return IsCollectionHelper(type, out Type itemType, constructorRequired, skipIfReadOnlyContract);
        }

        private static bool IsCollectionHelper(Type type, out Type itemType, bool constructorRequired, bool skipIfReadOnlyContract = false)
        {
            if (type.IsArray && DataContract.GetBuiltInDataContract(type) == null)
            {
                itemType = type.GetElementType();
                return true;
            }
            return IsCollectionOrTryCreate(type, false /*tryCreate*/, out DataContract dataContract, out itemType, constructorRequired, skipIfReadOnlyContract);
        }

        internal static bool TryCreate(Type type, out DataContract dataContract)
        {
            return IsCollectionOrTryCreate(type, true /*tryCreate*/, out dataContract, out Type itemType, true /*constructorRequired*/);
        }

        internal static bool TryCreateGetOnlyCollectionDataContract(Type type, out DataContract dataContract)
        {
            if (type.IsArray)
            {
                dataContract = new CollectionDataContract(type);
                return true;
            }
            else
            {
                return IsCollectionOrTryCreate(type, true /*tryCreate*/, out dataContract, out Type itemType, false /*constructorRequired*/);
            }
        }

        internal static MethodInfo GetTargetMethodWithName(string name, Type type, Type interfaceType)
        {
            InterfaceMapping mapping = type.GetInterfaceMap(interfaceType);
            for (int i = 0; i < mapping.TargetMethods.Length; i++)
            {
                if (mapping.InterfaceMethods[i].Name == name)
                {
                    return mapping.InterfaceMethods[i];
                }
            }
            return null;
        }

        private static bool IsArraySegment(Type t)
        {
            return t.IsGenericType && (t.GetGenericTypeDefinition() == typeof(ArraySegment<>));
        }

        private static bool IsCollectionOrTryCreate(Type type, bool tryCreate, out DataContract dataContract, out Type itemType, bool constructorRequired, bool skipIfReadOnlyContract = false)
        {
            dataContract = null;
            itemType = Globals.TypeOfObject;

            if (DataContract.GetBuiltInDataContract(type) != null)
            {
                return HandleIfInvalidCollection(type, tryCreate, false/*hasCollectionDataContract*/, false/*isBaseTypeCollection*/,
                    SR.CollectionTypeCannotBeBuiltIn, null, ref dataContract);
            }
            MethodInfo addMethod, getEnumeratorMethod;
            bool hasCollectionDataContract = IsCollectionDataContract(type);
            bool isReadOnlyContract = false;
            string serializationExceptionMessage = null, deserializationExceptionMessage = null;
            Type baseType = type.BaseType;
            bool isBaseTypeCollection = (baseType != null && baseType != Globals.TypeOfObject
                && baseType != Globals.TypeOfValueType && baseType != Globals.TypeOfUri) ? IsCollection(baseType) : false;

            // Avoid creating an invalid collection contract for Serializable types since we can create a ClassDataContract instead
            bool createContractWithException = isBaseTypeCollection && !type.IsSerializable;

            if (type.IsDefined(Globals.TypeOfDataContractAttribute, false))
            {
                return HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, createContractWithException,
                    SR.CollectionTypeCannotHaveDataContract, null, ref dataContract);
            }

            if (Globals.TypeOfIXmlSerializable.IsAssignableFrom(type) || IsArraySegment(type))
            {
                return false;
            }

            if (!Globals.TypeOfIEnumerable.IsAssignableFrom(type))
            {
                return HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, createContractWithException,
                    SR.CollectionTypeIsNotIEnumerable, null, ref dataContract);
            }
            if (type.IsInterface)
            {
                Type interfaceTypeToCheck = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                Type[] knownInterfaces = KnownInterfaces;
                for (int i = 0; i < knownInterfaces.Length; i++)
                {
                    if (knownInterfaces[i] == interfaceTypeToCheck)
                    {
                        addMethod = null;
                        if (type.IsGenericType)
                        {
                            Type[] genericArgs = type.GetGenericArguments();
                            if (interfaceTypeToCheck == Globals.TypeOfIDictionaryGeneric)
                            {
                                itemType = Globals.TypeOfKeyValue.MakeGenericType(genericArgs);
                                addMethod = type.GetMethod(Globals.AddMethodName);
                                getEnumeratorMethod = Globals.TypeOfIEnumerableGeneric.MakeGenericType(Globals.TypeOfKeyValuePair.MakeGenericType(genericArgs)).GetMethod(Globals.GetEnumeratorMethodName);
                            }
                            else
                            {
                                itemType = genericArgs[0];
                                if (interfaceTypeToCheck == Globals.TypeOfICollectionGeneric || interfaceTypeToCheck == Globals.TypeOfIListGeneric)
                                {
                                    addMethod = Globals.TypeOfICollectionGeneric.MakeGenericType(itemType).GetMethod(Globals.AddMethodName);
                                }
                                getEnumeratorMethod = Globals.TypeOfIEnumerableGeneric.MakeGenericType(itemType).GetMethod(Globals.GetEnumeratorMethodName);
                            }
                        }
                        else
                        {
                            if (interfaceTypeToCheck == Globals.TypeOfIDictionary)
                            {
                                itemType = typeof(KeyValue<object, object>);
                                addMethod = type.GetMethod(Globals.AddMethodName);
                            }
                            else
                            {
                                itemType = Globals.TypeOfObject;
                                if (interfaceTypeToCheck == Globals.TypeOfIList)
                                {
                                    addMethod = Globals.TypeOfIList.GetMethod(Globals.AddMethodName);
                                }
                            }
                            getEnumeratorMethod = Globals.TypeOfIEnumerable.GetMethod(Globals.GetEnumeratorMethodName);
                        }
                        if (tryCreate)
                        {
                            dataContract = new CollectionDataContract(type, (CollectionKind)(i + 1), itemType, getEnumeratorMethod, addMethod, null/*defaultCtor*/);
                        }

                        return true;
                    }
                }
            }
            ConstructorInfo defaultCtor = null;
            if (!type.IsValueType)
            {
                defaultCtor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Globals.EmptyTypeArray, null);
                if (defaultCtor == null && constructorRequired)
                {
                    // All collection types could be considered read-only collections except collection types that are marked [Serializable]. 
                    // Collection types marked [Serializable] cannot be read-only collections for backward compatibility reasons.
                    // DataContract types and POCO types cannot be collection types, so they don't need to be factored in
                    if (type.IsSerializable)
                    {
                        return HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, createContractWithException,
                            SR.CollectionTypeDoesNotHaveDefaultCtor, null, ref dataContract);
                    }
                    else
                    {
                        isReadOnlyContract = true;
                        GetReadOnlyCollectionExceptionMessages(type, hasCollectionDataContract, SR.CollectionTypeDoesNotHaveDefaultCtor, null, out serializationExceptionMessage, out deserializationExceptionMessage);
                    }
                }
            }

            Type knownInterfaceType = null;
            CollectionKind kind = CollectionKind.None;
            bool multipleDefinitions = false;
            Type[] interfaceTypes = type.GetInterfaces();
            foreach (Type interfaceType in interfaceTypes)
            {
                Type interfaceTypeToCheck = interfaceType.IsGenericType ? interfaceType.GetGenericTypeDefinition() : interfaceType;
                Type[] knownInterfaces = KnownInterfaces;
                for (int i = 0; i < knownInterfaces.Length; i++)
                {
                    if (knownInterfaces[i] == interfaceTypeToCheck)
                    {
                        CollectionKind currentKind = (CollectionKind)(i + 1);
                        if (kind == CollectionKind.None || currentKind < kind)
                        {
                            kind = currentKind;
                            knownInterfaceType = interfaceType;
                            multipleDefinitions = false;
                        }
                        else if ((kind & currentKind) == currentKind)
                        {
                            multipleDefinitions = true;
                        }

                        break;
                    }
                }
            }

            if (kind == CollectionKind.None)
            {
                return HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, createContractWithException,
                    SR.CollectionTypeIsNotIEnumerable, null, ref dataContract);
            }

            if (kind == CollectionKind.Enumerable || kind == CollectionKind.Collection || kind == CollectionKind.GenericEnumerable)
            {
                if (multipleDefinitions)
                {
                    knownInterfaceType = Globals.TypeOfIEnumerable;
                }

                itemType = knownInterfaceType.IsGenericType ? knownInterfaceType.GetGenericArguments()[0] : Globals.TypeOfObject;
                GetCollectionMethods(type, knownInterfaceType, new Type[] { itemType },
                                     false /*addMethodOnInterface*/,
                                     out getEnumeratorMethod, out addMethod);
                if (addMethod == null)
                {
                    // All collection types could be considered read-only collections except collection types that are marked [Serializable]. 
                    // Collection types marked [Serializable] cannot be read-only collections for backward compatibility reasons.
                    // DataContract types and POCO types cannot be collection types, so they don't need to be factored in.
                    if (type.IsSerializable || skipIfReadOnlyContract)
                    {
                        return HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, createContractWithException && !skipIfReadOnlyContract,
                            SR.CollectionTypeDoesNotHaveAddMethod, DataContract.GetClrTypeFullName(itemType), ref dataContract);
                    }
                    else
                    {
                        isReadOnlyContract = true;
                        GetReadOnlyCollectionExceptionMessages(type, hasCollectionDataContract, SR.CollectionTypeDoesNotHaveAddMethod, DataContract.GetClrTypeFullName(itemType), out serializationExceptionMessage, out deserializationExceptionMessage);
                    }
                }

                if (tryCreate)
                {
                    dataContract = isReadOnlyContract ?
                        new CollectionDataContract(type, kind, itemType, getEnumeratorMethod, serializationExceptionMessage, deserializationExceptionMessage) :
                        new CollectionDataContract(type, kind, itemType, getEnumeratorMethod, addMethod, defaultCtor, !constructorRequired);
                }
            }
            else
            {
                if (multipleDefinitions)
                {
                    return HandleIfInvalidCollection(type, tryCreate, hasCollectionDataContract, createContractWithException,
                        SR.CollectionTypeHasMultipleDefinitionsOfInterface, KnownInterfaces[(int)kind - 1].Name, ref dataContract);
                }
                Type[] addMethodTypeArray = null;
                switch (kind)
                {
                    case CollectionKind.GenericDictionary:
                        addMethodTypeArray = knownInterfaceType.GetGenericArguments();
                        bool isOpenGeneric = knownInterfaceType.IsGenericTypeDefinition
                            || (addMethodTypeArray[0].IsGenericParameter && addMethodTypeArray[1].IsGenericParameter);
                        itemType = isOpenGeneric ? Globals.TypeOfKeyValue : Globals.TypeOfKeyValue.MakeGenericType(addMethodTypeArray);
                        break;
                    case CollectionKind.Dictionary:
                        addMethodTypeArray = new Type[] { Globals.TypeOfObject, Globals.TypeOfObject };
                        itemType = Globals.TypeOfKeyValue.MakeGenericType(addMethodTypeArray);
                        break;
                    case CollectionKind.GenericList:
                    case CollectionKind.GenericCollection:
                        addMethodTypeArray = knownInterfaceType.GetGenericArguments();
                        itemType = addMethodTypeArray[0];
                        break;
                    case CollectionKind.List:
                        itemType = Globals.TypeOfObject;
                        addMethodTypeArray = new Type[] { itemType };
                        break;
                }

                if (tryCreate)
                {
                    GetCollectionMethods(type, knownInterfaceType, addMethodTypeArray,
                                     true /*addMethodOnInterface*/,
                                     out getEnumeratorMethod, out addMethod);
                    dataContract = isReadOnlyContract ?
                        new CollectionDataContract(type, kind, itemType, getEnumeratorMethod, serializationExceptionMessage, deserializationExceptionMessage) :
                        new CollectionDataContract(type, kind, itemType, getEnumeratorMethod, addMethod, defaultCtor, !constructorRequired);
                }
            }

            return !(isReadOnlyContract && skipIfReadOnlyContract);
        }

        internal static bool IsCollectionDataContract(Type type)
        {
            return type.IsDefined(Globals.TypeOfCollectionDataContractAttribute, false);
        }

        private static bool HandleIfInvalidCollection(Type type, bool tryCreate, bool hasCollectionDataContract, bool createContractWithException, string message, string param, ref DataContract dataContract)
        {
            if (hasCollectionDataContract)
            {
                if (tryCreate)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(GetInvalidCollectionMessage(message, SR.Format(SR.InvalidCollectionDataContract, DataContract.GetClrTypeFullName(type)), param)));
                }

                return true;
            }

            if (createContractWithException)
            {
                if (tryCreate)
                {
                    dataContract = new CollectionDataContract(type, GetInvalidCollectionMessage(message, SR.Format(SR.InvalidCollectionType, DataContract.GetClrTypeFullName(type)), param));
                }

                return true;
            }

            return false;
        }

        private static void GetReadOnlyCollectionExceptionMessages(Type type, bool hasCollectionDataContract, string message, string param, out string serializationExceptionMessage, out string deserializationExceptionMessage)
        {
            serializationExceptionMessage = GetInvalidCollectionMessage(message, SR.Format(hasCollectionDataContract ? SR.InvalidCollectionDataContract : SR.InvalidCollectionType, DataContract.GetClrTypeFullName(type)), param);
            deserializationExceptionMessage = GetInvalidCollectionMessage(message, SR.Format(SR.ReadOnlyCollectionDeserialization, DataContract.GetClrTypeFullName(type)), param);
        }

        private static string GetInvalidCollectionMessage(string message, string nestedMessage, string param)
        {
            return (param == null) ? SR.Format(message, nestedMessage) : SR.Format(message, nestedMessage, param);
        }

        private static void FindCollectionMethodsOnInterface(Type type, Type interfaceType, ref MethodInfo addMethod, ref MethodInfo getEnumeratorMethod)
        {
            InterfaceMapping mapping = type.GetInterfaceMap(interfaceType);
            for (int i = 0; i < mapping.TargetMethods.Length; i++)
            {
                if (mapping.InterfaceMethods[i].Name == Globals.AddMethodName)
                {
                    addMethod = mapping.InterfaceMethods[i];
                }
                else if (mapping.InterfaceMethods[i].Name == Globals.GetEnumeratorMethodName)
                {
                    getEnumeratorMethod = mapping.InterfaceMethods[i];
                }
            }
        }

        private static void GetCollectionMethods(Type type, Type interfaceType, Type[] addMethodTypeArray, bool addMethodOnInterface, out MethodInfo getEnumeratorMethod, out MethodInfo addMethod)
        {
            addMethod = getEnumeratorMethod = null;

            if (addMethodOnInterface)
            {
                addMethod = type.GetMethod(Globals.AddMethodName, BindingFlags.Instance | BindingFlags.Public, null, addMethodTypeArray, null);
                if (addMethod == null || addMethod.GetParameters()[0].ParameterType != addMethodTypeArray[0])
                {
                    FindCollectionMethodsOnInterface(type, interfaceType, ref addMethod, ref getEnumeratorMethod);
                    if (addMethod == null)
                    {
                        Type[] parentInterfaceTypes = interfaceType.GetInterfaces();
                        foreach (Type parentInterfaceType in parentInterfaceTypes)
                        {
                            if (IsKnownInterface(parentInterfaceType))
                            {
                                FindCollectionMethodsOnInterface(type, parentInterfaceType, ref addMethod, ref getEnumeratorMethod);
                                if (addMethod == null)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // GetMethod returns Add() method with parameter closest matching T in assignability/inheritance chain
                addMethod = type.GetMethod(Globals.AddMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, addMethodTypeArray, null);
            }

            if (getEnumeratorMethod == null)
            {
                getEnumeratorMethod = type.GetMethod(Globals.GetEnumeratorMethodName, BindingFlags.Instance | BindingFlags.Public, null, Globals.EmptyTypeArray, null);
                if (getEnumeratorMethod == null || !Globals.TypeOfIEnumerator.IsAssignableFrom(getEnumeratorMethod.ReturnType))
                {
                    Type ienumerableInterface = interfaceType.GetInterface("System.Collections.Generic.IEnumerable*");
                    if (ienumerableInterface == null)
                    {
                        ienumerableInterface = Globals.TypeOfIEnumerable;
                    }

                    getEnumeratorMethod = GetTargetMethodWithName(Globals.GetEnumeratorMethodName, type, ienumerableInterface);
                }
            }
        }

        private static bool IsKnownInterface(Type type)
        {
            Type typeToCheck = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
            foreach (Type knownInterfaceType in KnownInterfaces)
            {
                if (typeToCheck == knownInterfaceType)
                {
                    return true;
                }
            }
            return false;
        }

        internal override DataContract BindGenericParameters(DataContract[] paramContracts, Dictionary<DataContract, DataContract> boundContracts)
        {
            if (boundContracts.TryGetValue(this, out DataContract boundContract))
            {
                return boundContract;
            }

            CollectionDataContract boundCollectionContract = new CollectionDataContract(Kind);
            boundContracts.Add(this, boundCollectionContract);
            boundCollectionContract.ItemContract = ItemContract.BindGenericParameters(paramContracts, boundContracts);
            boundCollectionContract.IsItemTypeNullable = !boundCollectionContract.ItemContract.IsValueType;
            boundCollectionContract.ItemName = ItemNameSetExplicit ? ItemName : boundCollectionContract.ItemContract.StableName.Name;
            boundCollectionContract.KeyName = KeyName;
            boundCollectionContract.ValueName = ValueName;
            boundCollectionContract.StableName = CreateQualifiedName(DataContract.ExpandGenericParameters(XmlConvert.DecodeName(StableName.Name), new GenericNameProvider(DataContract.GetClrTypeFullName(UnderlyingType), paramContracts)),
                IsCollectionDataContract(UnderlyingType) ? StableName.Namespace : DataContract.GetCollectionNamespace(boundCollectionContract.ItemContract.StableName.Namespace));
            return boundCollectionContract;
        }

        internal override DataContract GetValidContract(SerializationMode mode)
        {
            if (mode == SerializationMode.SharedType)
            {
                if (SharedTypeContract == null)
                {
                    DataContract.ThrowTypeNotSerializable(UnderlyingType);
                }

                return SharedTypeContract;
            }

            ThrowIfInvalid();
            return this;
        }

        private void ThrowIfInvalid()
        {
            if (InvalidCollectionInSharedContractMessage != null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(InvalidCollectionInSharedContractMessage));
            }
        }

        internal override DataContract GetValidContract()
        {
            if (IsConstructorCheckRequired)
            {
                CheckConstructor();
            }
            return this;
        }

        private void CheckConstructor()
        {
            if (Constructor == null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.CollectionTypeDoesNotHaveDefaultCtor, DataContract.GetClrTypeFullName(UnderlyingType))));
            }
            else
            {
                IsConstructorCheckRequired = false;
            }
        }

        internal override bool IsValidContract(SerializationMode mode)
        {
            if (mode == SerializationMode.SharedType)
            {
                return (SharedTypeContract != null);
            }

            return (InvalidCollectionInSharedContractMessage == null);
        }

        internal bool RequiresMemberAccessForRead(SecurityException securityException)
        {
            if (!IsTypeVisible(UnderlyingType))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustCollectionContractTypeNotPublic,
                                DataContract.GetClrTypeFullName(UnderlyingType)),
                            securityException));
                }
                return true;
            }
            if (ItemType != null && !IsTypeVisible(ItemType))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustCollectionContractTypeNotPublic,
                                DataContract.GetClrTypeFullName(ItemType)),
                            securityException));
                }
                return true;
            }
            if (ConstructorRequiresMemberAccess(Constructor))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustCollectionContractNoPublicConstructor,
                                DataContract.GetClrTypeFullName(UnderlyingType)),
                            securityException));
                }
                return true;
            }
            if (MethodRequiresMemberAccess(AddMethod))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                           new SecurityException(SR.Format(
                                   SR.PartialTrustCollectionContractAddMethodNotPublic,
                                   DataContract.GetClrTypeFullName(UnderlyingType),
                                   AddMethod.Name),
                               securityException));
                }
                return true;
            }

            return false;
        }

        internal bool RequiresMemberAccessForWrite(SecurityException securityException)
        {
            if (!IsTypeVisible(UnderlyingType))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustCollectionContractTypeNotPublic,
                                DataContract.GetClrTypeFullName(UnderlyingType)),
                            securityException));
                }
                return true;
            }
            if (ItemType != null && !IsTypeVisible(ItemType))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustCollectionContractTypeNotPublic,
                                DataContract.GetClrTypeFullName(ItemType)),
                            securityException));
                }
                return true;
            }

            return false;
        }

        internal override bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
        {
            if (IsEqualOrChecked(other, checkedContracts))
            {
                return true;
            }

            if (base.Equals(other, checkedContracts))
            {
                CollectionDataContract dataContract = other as CollectionDataContract;
                if (dataContract != null)
                {
                    bool thisItemTypeIsNullable = (ItemContract == null) ? false : !ItemContract.IsValueType;
                    bool otherItemTypeIsNullable = (dataContract.ItemContract == null) ? false : !dataContract.ItemContract.IsValueType;
                    return ItemName == dataContract.ItemName &&
                        (IsItemTypeNullable || thisItemTypeIsNullable) == (dataContract.IsItemTypeNullable || otherItemTypeIsNullable) &&
                        ItemContract.Equals(dataContract.ItemContract, checkedContracts);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void WriteXmlValue(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context)
        {
            // IsGetOnlyCollection value has already been used to create current collectiondatacontract, value can now be reset. 
            context.IsGetOnlyCollection = false;
            XmlFormatWriterDelegate(xmlWriter, obj, context, this);
        }

        public override object ReadXmlValue(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context)
        {
            xmlReader.Read();
            object o = null;
            if (context.IsGetOnlyCollection)
            {
                // IsGetOnlyCollection value has already been used to create current collectiondatacontract, value can now be reset. 
                context.IsGetOnlyCollection = false;
                XmlFormatGetOnlyCollectionReaderDelegate(xmlReader, context, CollectionItemName, Namespace, this);
            }
            else
            {
                o = XmlFormatReaderDelegate(xmlReader, context, CollectionItemName, Namespace, this);
            }
            xmlReader.ReadEndElement();
            return o;
        }

        public class DictionaryEnumerator : IEnumerator<KeyValue<object, object>>
        {
            private readonly IDictionaryEnumerator enumerator;

            public DictionaryEnumerator(IDictionaryEnumerator enumerator)
            {
                this.enumerator = enumerator;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public KeyValue<object, object> Current => new KeyValue<object, object>(enumerator.Key, enumerator.Value);

            object System.Collections.IEnumerator.Current => Current;

            public void Reset()
            {
                enumerator.Reset();
            }
        }

        public class GenericDictionaryEnumerator<K, V> : IEnumerator<KeyValue<K, V>>
        {
            private readonly IEnumerator<KeyValuePair<K, V>> enumerator;

            public GenericDictionaryEnumerator(IEnumerator<KeyValuePair<K, V>> enumerator)
            {
                this.enumerator = enumerator;
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            public KeyValue<K, V> Current
            {
                get
                {
                    KeyValuePair<K, V> current = enumerator.Current;
                    return new KeyValue<K, V>(current.Key, current.Value);
                }
            }

            object System.Collections.IEnumerator.Current => Current;

            public void Reset()
            {
                enumerator.Reset();
            }
        }

    }
}
