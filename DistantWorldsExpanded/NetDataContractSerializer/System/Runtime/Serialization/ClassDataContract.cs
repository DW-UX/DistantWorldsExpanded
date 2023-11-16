using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Xml;
using DataContractAttribute = System.Runtime.Serialization.DataContractAttribute;
using DataMemberAttribute = System.Runtime.Serialization.DataMemberAttribute;
using IgnoreDataMemberAttribute = System.Runtime.Serialization.IgnoreDataMemberAttribute;
using InvalidDataContractException = System.Runtime.Serialization.InvalidDataContractException;
using SerializationInfo = System.Runtime.Serialization.SerializationInfo;
using StreamingContext = System.Runtime.Serialization.StreamingContext;

namespace System.Runtime.Serialization
{
    using DataContractDictionary = Dictionary<XmlQualifiedName, DataContract>;

    internal sealed class ClassDataContract : DataContract
    {
        public XmlDictionaryString[] ContractNamespaces;

        public XmlDictionaryString[] MemberNames;

        public XmlDictionaryString[] MemberNamespaces;

        private XmlDictionaryString[] childElementNamespaces;

        private ClassDataContractCriticalHelper helper;

        internal ClassDataContract()
            : base(new ClassDataContractCriticalHelper())
        {
            InitClassDataContract();
        }

        internal ClassDataContract(Type type)
            : base(new ClassDataContractCriticalHelper(type))
        {
            InitClassDataContract();
        }

        private ClassDataContract(Type type, XmlDictionaryString ns, string[] memberNames)
            : base(new ClassDataContractCriticalHelper(type, ns, memberNames))
        {
            InitClassDataContract();
        }

        private void InitClassDataContract()
        {
            helper = base.Helper as ClassDataContractCriticalHelper;
            ContractNamespaces = helper.ContractNamespaces;
            MemberNames = helper.MemberNames;
            MemberNamespaces = helper.MemberNamespaces;
        }

        internal ClassDataContract BaseContract
        {
            get => helper.BaseContract;
            set => helper.BaseContract = value;
        }

        internal List<DataMember> Members
        {
            get => helper.Members;
            set => helper.Members = value;
        }

        public XmlDictionaryString[] ChildElementNamespaces
        {
            get
            {
                if (childElementNamespaces == null)
                {
                    lock (this)
                    {
                        if (childElementNamespaces == null)
                        {
                            if (helper.ChildElementNamespaces == null)
                            {
                                XmlDictionaryString[] tempChildElementamespaces = CreateChildElementNamespaces();
                                Thread.MemoryBarrier();
                                helper.ChildElementNamespaces = tempChildElementamespaces;
                            }
                            childElementNamespaces = helper.ChildElementNamespaces;
                        }
                    }
                }
                return childElementNamespaces;
            }
        }

        internal MethodInfo OnSerializing => helper.OnSerializing;

        internal MethodInfo OnSerialized => helper.OnSerialized;

        internal MethodInfo OnDeserializing => helper.OnDeserializing;

        internal MethodInfo OnDeserialized => helper.OnDeserialized;

        internal MethodInfo ExtensionDataSetMethod => helper.ExtensionDataSetMethod;

        internal override DataContractDictionary KnownDataContracts
        {
            get => helper.KnownDataContracts;
            set => helper.KnownDataContracts = value;
        }

        internal override bool IsISerializable
        {
            get => helper.IsISerializable;
            set => helper.IsISerializable = value;
        }

        internal bool IsNonAttributedType => helper.IsNonAttributedType;

        internal bool HasDataContract => helper.HasDataContract;

        internal bool HasExtensionData => helper.HasExtensionData;

        internal string SerializationExceptionMessage => helper.SerializationExceptionMessage;

        internal string DeserializationExceptionMessage => helper.DeserializationExceptionMessage;

        internal bool IsReadOnlyContract => DeserializationExceptionMessage != null;

        internal ConstructorInfo GetISerializableConstructor()
        {
            return helper.GetISerializableConstructor();
        }

        internal ConstructorInfo GetNonAttributedTypeConstructor()
        {
            return helper.GetNonAttributedTypeConstructor();
        }

        internal XmlFormatClassWriterDelegate XmlFormatWriterDelegate
        {
            get
            {
                if (helper.XmlFormatWriterDelegate == null)
                {
                    lock (this)
                    {
                        if (helper.XmlFormatWriterDelegate == null)
                        {
                            XmlFormatClassWriterDelegate tempDelegate = new XmlFormatWriterGenerator().GenerateClassWriter(this);
                            Thread.MemoryBarrier();
                            helper.XmlFormatWriterDelegate = tempDelegate;
                        }
                    }
                }
                return helper.XmlFormatWriterDelegate;
            }
        }

        internal XmlFormatClassReaderDelegate XmlFormatReaderDelegate
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
                            XmlFormatClassReaderDelegate tempDelegate = new XmlFormatReaderGenerator().GenerateClassReader(this);
                            Thread.MemoryBarrier();
                            helper.XmlFormatReaderDelegate = tempDelegate;
                        }
                    }
                }
                return helper.XmlFormatReaderDelegate;
            }
        }

        internal static ClassDataContract CreateClassDataContractForKeyValue(Type type, XmlDictionaryString ns, string[] memberNames)
        {
            return new ClassDataContract(type, ns, memberNames);
        }

        internal static void CheckAndAddMember(List<DataMember> members, DataMember memberContract, Dictionary<string, DataMember> memberNamesTable)
        {
            if (memberNamesTable.TryGetValue(memberContract.Name, out DataMember existingMemberContract))
            {
                Type declaringType = memberContract.MemberInfo.DeclaringType;
                DataContract.ThrowInvalidDataContractException(
                    SR.Format((declaringType.IsEnum ? SR.DupEnumMemberValue : SR.DupMemberName),
                        existingMemberContract.MemberInfo.Name,
                        memberContract.MemberInfo.Name,
                        DataContract.GetClrTypeFullName(declaringType),
                        memberContract.Name),
                    declaringType);
            }
            memberNamesTable.Add(memberContract.Name, memberContract);
            members.Add(memberContract);
        }

        internal static XmlDictionaryString GetChildNamespaceToDeclare(DataContract dataContract, Type childType, XmlDictionary dictionary)
        {
            childType = DataContract.UnwrapNullableType(childType);
            if (!childType.IsEnum && !Globals.TypeOfIXmlSerializable.IsAssignableFrom(childType)
                && DataContract.GetBuiltInDataContract(childType) == null && childType != Globals.TypeOfDBNull)
            {
                string ns = DataContract.GetStableName(childType).Namespace;
                if (ns.Length > 0 && ns != dataContract.Namespace.Value)
                {
                    return dictionary.Add(ns);
                }
            }
            return null;
        }

        // check whether a corresponding update is required in DataContractCriticalHelper.CreateDataContract
        internal static bool IsNonAttributedTypeValidForSerialization(Type type)
        {
            if (type.IsArray)
            {
                return false;
            }

            if (type.IsEnum)
            {
                return false;
            }

            if (type.IsGenericParameter)
            {
                return false;
            }

            if (Globals.TypeOfIXmlSerializable.IsAssignableFrom(type))
            {
                return false;
            }

            if (type.IsPointer)
            {
                return false;
            }

            if (type.IsDefined(Globals.TypeOfCollectionDataContractAttribute, false))
            {
                return false;
            }

            Type[] interfaceTypes = type.GetInterfaces();
            foreach (Type interfaceType in interfaceTypes)
            {
                if (CollectionDataContract.IsCollectionInterface(interfaceType))
                {
                    return false;
                }
            }

            if (type.IsSerializable)
            {
                return false;
            }

            if (Globals.TypeOfISerializable.IsAssignableFrom(type))
            {
                return false;
            }

            if (type.IsDefined(Globals.TypeOfDataContractAttribute, false))
            {
                return false;
            }

            if (type == Globals.TypeOfExtensionDataObject)
            {
                return false;
            }

            if (type.IsValueType)
            {
                return type.IsVisible;
            }
            else
            {
                return (type.IsVisible &&
                    type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, Globals.EmptyTypeArray, null) != null);
            }
        }

        private XmlDictionaryString[] CreateChildElementNamespaces()
        {
            if (Members == null)
            {
                return null;
            }

            XmlDictionaryString[] baseChildElementNamespaces = null;
            if (BaseContract != null)
            {
                baseChildElementNamespaces = BaseContract.ChildElementNamespaces;
            }

            int baseChildElementNamespaceCount = (baseChildElementNamespaces != null) ? baseChildElementNamespaces.Length : 0;
            XmlDictionaryString[] childElementNamespaces = new XmlDictionaryString[Members.Count + baseChildElementNamespaceCount];
            if (baseChildElementNamespaceCount > 0)
            {
                Array.Copy(baseChildElementNamespaces, 0, childElementNamespaces, 0, baseChildElementNamespaces.Length);
            }

            XmlDictionary dictionary = new XmlDictionary();
            for (int i = 0; i < Members.Count; i++)
            {
                childElementNamespaces[i + baseChildElementNamespaceCount] = GetChildNamespaceToDeclare(this, Members[i].MemberType, dictionary);
            }

            return childElementNamespaces;
        }

        private void EnsureMethodsImported()
        {
            helper.EnsureMethodsImported();
        }

        public override void WriteXmlValue(XmlWriterDelegator xmlWriter, object obj, XmlObjectSerializerWriteContext context)
        {
            XmlFormatWriterDelegate(xmlWriter, obj, context, this);
        }

        public override object ReadXmlValue(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context)
        {
            xmlReader.Read();
            object o = XmlFormatReaderDelegate(xmlReader, context, MemberNames, MemberNamespaces);
            xmlReader.ReadEndElement();
            return o;
        }

        internal bool RequiresMemberAccessForRead(SecurityException securityException)
        {
            EnsureMethodsImported();

            if (!IsTypeVisible(UnderlyingType))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustDataContractTypeNotPublic,
                                DataContract.GetClrTypeFullName(UnderlyingType)),
                            securityException));
                }
                return true;
            }

            if (BaseContract != null && BaseContract.RequiresMemberAccessForRead(securityException))
            {
                return true;
            }

            if (ConstructorRequiresMemberAccess(GetISerializableConstructor()))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustISerializableNoPublicConstructor,
                                DataContract.GetClrTypeFullName(UnderlyingType)),
                            securityException));
                }
                return true;
            }


            if (ConstructorRequiresMemberAccess(GetNonAttributedTypeConstructor()))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustNonAttributedSerializableTypeNoPublicConstructor,
                                DataContract.GetClrTypeFullName(UnderlyingType)),
                            securityException));
                }
                return true;
            }


            if (MethodRequiresMemberAccess(OnDeserializing))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustDataContractOnDeserializingNotPublic,
                                DataContract.GetClrTypeFullName(UnderlyingType),
                                OnDeserializing.Name),
                            securityException));
                }
                return true;
            }

            if (MethodRequiresMemberAccess(OnDeserialized))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustDataContractOnDeserializedNotPublic,
                                DataContract.GetClrTypeFullName(UnderlyingType),
                                OnDeserialized.Name),
                            securityException));
                }
                return true;
            }

            if (Members != null)
            {
                for (int i = 0; i < Members.Count; i++)
                {
                    if (Members[i].RequiresMemberAccessForSet())
                    {
                        if (securityException != null)
                        {
                            if (Members[i].MemberInfo is FieldInfo)
                            {
                                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                                    new SecurityException(SR.Format(
                                            SR.PartialTrustDataContractFieldSetNotPublic,
                                            DataContract.GetClrTypeFullName(UnderlyingType),
                                            Members[i].MemberInfo.Name),
                                        securityException));
                            }
                            else
                            {
                                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                                    new SecurityException(SR.Format(
                                            SR.PartialTrustDataContractPropertySetNotPublic,
                                            DataContract.GetClrTypeFullName(UnderlyingType),
                                            Members[i].MemberInfo.Name),
                                        securityException));
                            }
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        internal bool RequiresMemberAccessForWrite(SecurityException securityException)
        {
            EnsureMethodsImported();

            if (!IsTypeVisible(UnderlyingType))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustDataContractTypeNotPublic,
                                DataContract.GetClrTypeFullName(UnderlyingType)),
                            securityException));
                }
                return true;
            }

            if (BaseContract != null && BaseContract.RequiresMemberAccessForWrite(securityException))
            {
                return true;
            }

            if (MethodRequiresMemberAccess(OnSerializing))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustDataContractOnSerializingNotPublic,
                                DataContract.GetClrTypeFullName(UnderlyingType),
                                OnSerializing.Name),
                            securityException));
                }
                return true;
            }

            if (MethodRequiresMemberAccess(OnSerialized))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(
                                SR.PartialTrustDataContractOnSerializedNotPublic,
                                DataContract.GetClrTypeFullName(UnderlyingType),
                                OnSerialized.Name),
                            securityException));
                }
                return true;
            }

            if (Members != null)
            {
                for (int i = 0; i < Members.Count; i++)
                {
                    if (Members[i].RequiresMemberAccessForGet())
                    {
                        if (securityException != null)
                        {
                            if (Members[i].MemberInfo is FieldInfo)
                            {
                                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                                    new SecurityException(SR.Format(
                                            SR.PartialTrustDataContractFieldGetNotPublic,
                                            DataContract.GetClrTypeFullName(UnderlyingType),
                                            Members[i].MemberInfo.Name),
                                        securityException));
                            }
                            else
                            {
                                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                                    new SecurityException(SR.Format(
                                            SR.PartialTrustDataContractPropertyGetNotPublic,
                                            DataContract.GetClrTypeFullName(UnderlyingType),
                                            Members[i].MemberInfo.Name),
                                        securityException));
                            }
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        private class ClassDataContractCriticalHelper : DataContract.DataContractCriticalHelper
        {
            private ClassDataContract baseContract;
            private List<DataMember> members;
            private MethodInfo onSerializing, onSerialized;
            private MethodInfo onDeserializing, onDeserialized;
            private MethodInfo extensionDataSetMethod;
            private DataContractDictionary knownDataContracts;
            private string serializationExceptionMessage;
            private bool isISerializable;
            private bool isKnownTypeAttributeChecked;
            private bool isMethodChecked;
            private readonly bool hasExtensionData;
            private bool isNonAttributedType;
            private bool hasDataContract;
            private XmlDictionaryString[] childElementNamespaces;
            private XmlFormatClassReaderDelegate xmlFormatReaderDelegate;
            private XmlFormatClassWriterDelegate xmlFormatWriterDelegate;

            public XmlDictionaryString[] ContractNamespaces;
            public XmlDictionaryString[] MemberNames;
            public XmlDictionaryString[] MemberNamespaces;

            internal ClassDataContractCriticalHelper()
                : base()
            {
            }

            internal ClassDataContractCriticalHelper(Type type)
                : base(type)
            {
                XmlQualifiedName stableName = GetStableNameAndSetHasDataContract(type);
                if (type == Globals.TypeOfDBNull)
                {
                    StableName = stableName;
                    members = new List<DataMember>();
                    XmlDictionary dictionary = new XmlDictionary(2);
                    Name = dictionary.Add(StableName.Name);
                    Namespace = dictionary.Add(StableName.Namespace);
                    ContractNamespaces = MemberNames = MemberNamespaces = new XmlDictionaryString[] { };
                    EnsureMethodsImported();
                    return;
                }
                Type baseType = type.BaseType;
                isISerializable = (Globals.TypeOfISerializable.IsAssignableFrom(type));
                SetIsNonAttributedType(type);
                if (isISerializable)
                {
                    if (HasDataContract)
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.ISerializableCannotHaveDataContract, DataContract.GetClrTypeFullName(type))));
                    }

                    if (baseType != null && !(baseType.IsSerializable && Globals.TypeOfISerializable.IsAssignableFrom(baseType)))
                    {
                        baseType = null;
                    }
                }
                IsValueType = type.IsValueType;
                if (baseType != null && baseType != Globals.TypeOfObject && baseType != Globals.TypeOfValueType && baseType != Globals.TypeOfUri)
                {
                    DataContract baseContract = DataContract.GetDataContract(baseType);
                    if (baseContract is CollectionDataContract)
                    {
                        BaseContract = ((CollectionDataContract)baseContract).SharedTypeContract as ClassDataContract;
                    }
                    else
                    {
                        BaseContract = baseContract as ClassDataContract;
                    }

                    if (BaseContract != null && BaseContract.IsNonAttributedType && !isNonAttributedType)
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError
                            (new InvalidDataContractException(SR.Format(SR.AttributedTypesCannotInheritFromNonAttributedSerializableTypes,
                            DataContract.GetClrTypeFullName(type), DataContract.GetClrTypeFullName(baseType))));
                    }
                }
                else
                {
                    BaseContract = null;
                }

                hasExtensionData = (Globals.TypeOfIExtensibleDataObject.IsAssignableFrom(type));
                if (hasExtensionData && !HasDataContract && !IsNonAttributedType)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.OnlyDataContractTypesCanHaveExtensionData, DataContract.GetClrTypeFullName(type))));
                }

                if (isISerializable)
                {
                    SetDataContractName(stableName);
                }
                else
                {
                    StableName = stableName;
                    ImportDataMembers();
                    XmlDictionary dictionary = new XmlDictionary(2 + Members.Count);
                    Name = dictionary.Add(StableName.Name);
                    Namespace = dictionary.Add(StableName.Namespace);

                    int baseMemberCount = 0;
                    int baseContractCount = 0;
                    if (BaseContract == null)
                    {
                        MemberNames = new XmlDictionaryString[Members.Count];
                        MemberNamespaces = new XmlDictionaryString[Members.Count];
                        ContractNamespaces = new XmlDictionaryString[1];
                    }
                    else
                    {
                        if (BaseContract.IsReadOnlyContract)
                        {
                            serializationExceptionMessage = BaseContract.SerializationExceptionMessage;
                        }
                        baseMemberCount = BaseContract.MemberNames.Length;
                        MemberNames = new XmlDictionaryString[Members.Count + baseMemberCount];
                        Array.Copy(BaseContract.MemberNames, MemberNames, baseMemberCount);
                        MemberNamespaces = new XmlDictionaryString[Members.Count + baseMemberCount];
                        Array.Copy(BaseContract.MemberNamespaces, MemberNamespaces, baseMemberCount);
                        baseContractCount = BaseContract.ContractNamespaces.Length;
                        ContractNamespaces = new XmlDictionaryString[1 + baseContractCount];
                        Array.Copy(BaseContract.ContractNamespaces, ContractNamespaces, baseContractCount);
                    }
                    ContractNamespaces[baseContractCount] = Namespace;
                    for (int i = 0; i < Members.Count; i++)
                    {
                        MemberNames[i + baseMemberCount] = dictionary.Add(Members[i].Name);
                        MemberNamespaces[i + baseMemberCount] = Namespace;
                    }
                }
                EnsureMethodsImported();
            }

            internal ClassDataContractCriticalHelper(Type type, XmlDictionaryString ns, string[] memberNames)
                : base(type)
            {
                StableName = new XmlQualifiedName(GetStableNameAndSetHasDataContract(type).Name, ns.Value);
                ImportDataMembers();
                XmlDictionary dictionary = new XmlDictionary(1 + Members.Count);
                Name = dictionary.Add(StableName.Name);
                Namespace = ns;
                ContractNamespaces = new XmlDictionaryString[] { Namespace };
                MemberNames = new XmlDictionaryString[Members.Count];
                MemberNamespaces = new XmlDictionaryString[Members.Count];
                for (int i = 0; i < Members.Count; i++)
                {
                    Members[i].Name = memberNames[i];
                    MemberNames[i] = dictionary.Add(Members[i].Name);
                    MemberNamespaces[i] = Namespace;
                }
                EnsureMethodsImported();
            }

            private void EnsureIsReferenceImported(Type type)
            {
                bool isReference = false;
                bool hasDataContractAttribute = TryGetDCAttribute(type, out DataContractAttribute dataContractAttribute);

                if (BaseContract != null)
                {
                    if (hasDataContractAttribute && dataContractAttribute.IsReferenceSetExplicitly)
                    {
                        bool baseIsReference = BaseContract.IsReference;
                        if ((baseIsReference && !dataContractAttribute.IsReference) ||
                            (!baseIsReference && dataContractAttribute.IsReference))
                        {
                            DataContract.ThrowInvalidDataContractException(
                                    SR.Format(SR.InconsistentIsReference,
                                        DataContract.GetClrTypeFullName(type),
                                        dataContractAttribute.IsReference,
                                        DataContract.GetClrTypeFullName(BaseContract.UnderlyingType),
                                        BaseContract.IsReference),
                                    type);
                        }
                        else
                        {
                            isReference = dataContractAttribute.IsReference;
                        }
                    }
                    else
                    {
                        isReference = BaseContract.IsReference;
                    }
                }
                else if (hasDataContractAttribute)
                {
                    if (dataContractAttribute.IsReference)
                    {
                        isReference = dataContractAttribute.IsReference;
                    }
                }

                if (isReference && type.IsValueType)
                {
                    DataContract.ThrowInvalidDataContractException(
                            SR.Format(SR.ValueTypeCannotHaveIsReference,
                                DataContract.GetClrTypeFullName(type),
                                true,
                                false),
                            type);
                    return;
                }

                IsReference = isReference;
            }

            private void ImportDataMembers()
            {
                Type type = UnderlyingType;
                EnsureIsReferenceImported(type);
                List<DataMember> tempMembers = new List<DataMember>();
                Dictionary<string, DataMember> memberNamesTable = new Dictionary<string, DataMember>();

                MemberInfo[] memberInfos;
                if (isNonAttributedType)
                {
                    memberInfos = type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
                }
                else
                {
                    memberInfos = type.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                }

                for (int i = 0; i < memberInfos.Length; i++)
                {
                    MemberInfo member = memberInfos[i];
                    if (HasDataContract)
                    {
                        object[] memberAttributes = member.GetCustomAttributes(typeof(DataMemberAttribute), false);
                        if (memberAttributes != null && memberAttributes.Length > 0)
                        {
                            if (memberAttributes.Length > 1)
                            {
                                ThrowInvalidDataContractException(SR.Format(SR.TooManyDataMembers, DataContract.GetClrTypeFullName(member.DeclaringType), member.Name));
                            }

                            DataMember memberContract = new DataMember(member);

                            if (member.MemberType == MemberTypes.Property)
                            {
                                PropertyInfo property = (PropertyInfo)member;

                                MethodInfo getMethod = property.GetGetMethod(true);
                                if (getMethod != null && IsMethodOverriding(getMethod))
                                {
                                    continue;
                                }

                                MethodInfo setMethod = property.GetSetMethod(true);
                                if (setMethod != null && IsMethodOverriding(setMethod))
                                {
                                    continue;
                                }

                                if (getMethod == null)
                                {
                                    ThrowInvalidDataContractException(SR.Format(SR.NoGetMethodForProperty, property.DeclaringType, property.Name));
                                }

                                if (setMethod == null)
                                {
                                    if (!SetIfGetOnlyCollection(memberContract, skipIfReadOnlyContract: false))
                                    {
                                        serializationExceptionMessage = SR.Format(SR.NoSetMethodForProperty, property.DeclaringType, property.Name);
                                    }
                                }
                                if (getMethod.GetParameters().Length > 0)
                                {
                                    ThrowInvalidDataContractException(SR.Format(SR.IndexedPropertyCannotBeSerialized, property.DeclaringType, property.Name));
                                }
                            }
                            else if (member.MemberType != MemberTypes.Field)
                            {
                                ThrowInvalidDataContractException(SR.Format(SR.InvalidMember, DataContract.GetClrTypeFullName(type), member.Name));
                            }

                            DataMemberAttribute memberAttribute = (DataMemberAttribute)memberAttributes[0];
                            if (memberAttribute.IsNameSetExplicitly)
                            {
                                if (memberAttribute.Name == null || memberAttribute.Name.Length == 0)
                                {
                                    ThrowInvalidDataContractException(SR.Format(SR.InvalidDataMemberName, member.Name, DataContract.GetClrTypeFullName(type)));
                                }

                                memberContract.Name = memberAttribute.Name;
                            }
                            else
                            {
                                memberContract.Name = member.Name;
                            }

                            memberContract.Name = DataContract.EncodeLocalName(memberContract.Name);
                            memberContract.IsNullable = DataContract.IsTypeNullable(memberContract.MemberType);
                            memberContract.IsRequired = memberAttribute.IsRequired;
                            if (memberAttribute.IsRequired && IsReference)
                            {
                                ThrowInvalidDataContractException(
                                    SR.Format(SR.IsRequiredDataMemberOnIsReferenceDataContractType,
                                    DataContract.GetClrTypeFullName(member.DeclaringType),
                                    member.Name, true), type);
                            }
                            memberContract.EmitDefaultValue = memberAttribute.EmitDefaultValue;
                            memberContract.Order = memberAttribute.Order;
                            CheckAndAddMember(tempMembers, memberContract, memberNamesTable);
                        }
                    }
                    else if (isNonAttributedType)
                    {
                        FieldInfo field = member as FieldInfo;
                        PropertyInfo property = member as PropertyInfo;
                        if ((field == null && property == null) || (field != null && field.IsInitOnly))
                        {
                            continue;
                        }

                        object[] memberAttributes = member.GetCustomAttributes(typeof(IgnoreDataMemberAttribute), false);
                        if (memberAttributes != null && memberAttributes.Length > 0)
                        {
                            if (memberAttributes.Length > 1)
                            {
                                ThrowInvalidDataContractException(SR.Format(SR.TooManyIgnoreDataMemberAttributes, DataContract.GetClrTypeFullName(member.DeclaringType), member.Name));
                            }
                            else
                            {
                                continue;
                            }
                        }
                        DataMember memberContract = new DataMember(member);
                        if (property != null)
                        {
                            MethodInfo getMethod = property.GetGetMethod();
                            if (getMethod == null || IsMethodOverriding(getMethod) || getMethod.GetParameters().Length > 0)
                            {
                                continue;
                            }

                            MethodInfo setMethod = property.GetSetMethod(true);
                            if (setMethod == null)
                            {
                                // if the collection doesn't have the 'Add' method, we will skip it, for compatibility with 4.0
                                if (!SetIfGetOnlyCollection(memberContract, skipIfReadOnlyContract: true))
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                if (!setMethod.IsPublic || IsMethodOverriding(setMethod))
                                {
                                    continue;
                                }
                            }

                            //skip ExtensionData member of type ExtensionDataObject if IExtensibleDataObject is implemented in non-attributed type
                            if (hasExtensionData && memberContract.MemberType == Globals.TypeOfExtensionDataObject
                                && member.Name == Globals.ExtensionDataObjectPropertyName)
                            {
                                continue;
                            }
                        }

                        memberContract.Name = DataContract.EncodeLocalName(member.Name);
                        memberContract.IsNullable = DataContract.IsTypeNullable(memberContract.MemberType);
                        CheckAndAddMember(tempMembers, memberContract, memberNamesTable);
                    }
                    else
                    {
                        FieldInfo field = member as FieldInfo;
                        if (field != null && !field.IsNotSerialized)
                        {
                            DataMember memberContract = new DataMember(member)
                            {
                                Name = DataContract.EncodeLocalName(member.Name)
                            };
                            object[] optionalFields = field.GetCustomAttributes(Globals.TypeOfOptionalFieldAttribute, false);
                            if (optionalFields == null || optionalFields.Length == 0)
                            {
                                if (IsReference)
                                {
                                    ThrowInvalidDataContractException(
                                        SR.Format(SR.NonOptionalFieldMemberOnIsReferenceSerializableType,
                                        DataContract.GetClrTypeFullName(member.DeclaringType),
                                        member.Name, true), type);
                                }
                                memberContract.IsRequired = true;
                            }
                            memberContract.IsNullable = DataContract.IsTypeNullable(memberContract.MemberType);
                            CheckAndAddMember(tempMembers, memberContract, memberNamesTable);
                        }
                    }
                }
                if (tempMembers.Count > 1)
                {
                    tempMembers.Sort(DataMemberComparer.Singleton);
                }

                SetIfMembersHaveConflict(tempMembers);

                Thread.MemoryBarrier();
                members = tempMembers;
            }

            private bool SetIfGetOnlyCollection(DataMember memberContract, bool skipIfReadOnlyContract)
            {
                //OK to call IsCollection here since the use of surrogated collection types is not supported in get-only scenarios
                if (CollectionDataContract.IsCollection(memberContract.MemberType, false /*isConstructorRequired*/, skipIfReadOnlyContract) && !memberContract.MemberType.IsValueType)
                {
                    memberContract.IsGetOnlyCollection = true;
                    return true;
                }
                return false;
            }

            private void SetIfMembersHaveConflict(List<DataMember> members)
            {
                if (BaseContract == null)
                {
                    return;
                }

                int baseTypeIndex = 0;
                List<Member> membersInHierarchy = new List<Member>();
                foreach (DataMember member in members)
                {
                    membersInHierarchy.Add(new Member(member, StableName.Namespace, baseTypeIndex));
                }
                ClassDataContract currContract = BaseContract;
                while (currContract != null)
                {
                    baseTypeIndex++;
                    foreach (DataMember member in currContract.Members)
                    {
                        membersInHierarchy.Add(new Member(member, currContract.StableName.Namespace, baseTypeIndex));
                    }
                    currContract = currContract.BaseContract;
                }

                IComparer<Member> comparer = DataMemberConflictComparer.Singleton;
                membersInHierarchy.Sort(comparer);

                for (int i = 0; i < membersInHierarchy.Count - 1; i++)
                {
                    int startIndex = i;
                    int endIndex = i;
                    bool hasConflictingType = false;
                    while (endIndex < membersInHierarchy.Count - 1
                        && string.CompareOrdinal(membersInHierarchy[endIndex].member.Name, membersInHierarchy[endIndex + 1].member.Name) == 0
                        && string.CompareOrdinal(membersInHierarchy[endIndex].ns, membersInHierarchy[endIndex + 1].ns) == 0)
                    {
                        membersInHierarchy[endIndex].member.ConflictingMember = membersInHierarchy[endIndex + 1].member;
                        if (!hasConflictingType)
                        {
                            if (membersInHierarchy[endIndex + 1].member.HasConflictingNameAndType)
                            {
                                hasConflictingType = true;
                            }
                            else
                            {
                                hasConflictingType = (membersInHierarchy[endIndex].member.MemberType != membersInHierarchy[endIndex + 1].member.MemberType);
                            }
                        }
                        endIndex++;
                    }

                    if (hasConflictingType)
                    {
                        for (int j = startIndex; j <= endIndex; j++)
                        {
                            membersInHierarchy[j].member.HasConflictingNameAndType = true;
                        }
                    }

                    i = endIndex + 1;
                }
            }

            private XmlQualifiedName GetStableNameAndSetHasDataContract(Type type)
            {
                return DataContract.GetStableName(type, out hasDataContract);
            }

            private void SetIsNonAttributedType(Type type)
            {
                isNonAttributedType = !type.IsSerializable && !hasDataContract && IsNonAttributedTypeValidForSerialization(type);
            }

            private static bool IsMethodOverriding(MethodInfo method)
            {
                return method.IsVirtual && ((method.Attributes & MethodAttributes.NewSlot) == 0);
            }

            internal void EnsureMethodsImported()
            {
                if (!isMethodChecked && UnderlyingType != null)
                {
                    lock (this)
                    {
                        if (!isMethodChecked)
                        {
                            Type type = UnderlyingType;
                            MethodInfo[] methods = type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                            for (int i = 0; i < methods.Length; i++)
                            {
                                MethodInfo method = methods[i];
                                Type prevAttributeType = null;
                                ParameterInfo[] parameters = method.GetParameters();
                                if (HasExtensionData && IsValidExtensionDataSetMethod(method, parameters))
                                {
                                    if (method.Name == Globals.ExtensionDataSetExplicitMethod || !method.IsPublic)
                                    {
                                        extensionDataSetMethod = XmlFormatGeneratorStatics.ExtensionDataSetExplicitMethodInfo;
                                    }
                                    else
                                    {
                                        extensionDataSetMethod = method;
                                    }
                                }
                                if (IsValidCallback(method, parameters, Globals.TypeOfOnSerializingAttribute, onSerializing, ref prevAttributeType))
                                {
                                    onSerializing = method;
                                }

                                if (IsValidCallback(method, parameters, Globals.TypeOfOnSerializedAttribute, onSerialized, ref prevAttributeType))
                                {
                                    onSerialized = method;
                                }

                                if (IsValidCallback(method, parameters, Globals.TypeOfOnDeserializingAttribute, onDeserializing, ref prevAttributeType))
                                {
                                    onDeserializing = method;
                                }

                                if (IsValidCallback(method, parameters, Globals.TypeOfOnDeserializedAttribute, onDeserialized, ref prevAttributeType))
                                {
                                    onDeserialized = method;
                                }
                            }
                            Thread.MemoryBarrier();
                            isMethodChecked = true;
                        }
                    }
                }
            }

            private bool IsValidExtensionDataSetMethod(MethodInfo method, ParameterInfo[] parameters)
            {
                if (method.Name == Globals.ExtensionDataSetExplicitMethod || method.Name == Globals.ExtensionDataSetMethod)
                {
                    if (extensionDataSetMethod != null)
                    {
                        ThrowInvalidDataContractException(SR.Format(SR.DuplicateExtensionDataSetMethod, method, extensionDataSetMethod, DataContract.GetClrTypeFullName(method.DeclaringType)));
                    }

                    if (method.ReturnType != Globals.TypeOfVoid)
                    {
                        DataContract.ThrowInvalidDataContractException(SR.Format(SR.ExtensionDataSetMustReturnVoid, DataContract.GetClrTypeFullName(method.DeclaringType), method), method.DeclaringType);
                    }

                    if (parameters == null || parameters.Length != 1 || parameters[0].ParameterType != Globals.TypeOfExtensionDataObject)
                    {
                        DataContract.ThrowInvalidDataContractException(SR.Format(SR.ExtensionDataSetParameterInvalid, DataContract.GetClrTypeFullName(method.DeclaringType), method, Globals.TypeOfExtensionDataObject), method.DeclaringType);
                    }

                    return true;
                }
                return false;
            }

            private static bool IsValidCallback(MethodInfo method, ParameterInfo[] parameters, Type attributeType, MethodInfo currentCallback, ref Type prevAttributeType)
            {
                if (method.IsDefined(attributeType, false))
                {
                    if (currentCallback != null)
                    {
                        DataContract.ThrowInvalidDataContractException(SR.Format(SR.DuplicateCallback, method, currentCallback, DataContract.GetClrTypeFullName(method.DeclaringType), attributeType), method.DeclaringType);
                    }
                    else if (prevAttributeType != null)
                    {
                        DataContract.ThrowInvalidDataContractException(SR.Format(SR.DuplicateAttribute, prevAttributeType, attributeType, DataContract.GetClrTypeFullName(method.DeclaringType), method), method.DeclaringType);
                    }
                    else if (method.IsVirtual)
                    {
                        DataContract.ThrowInvalidDataContractException(SR.Format(SR.CallbacksCannotBeVirtualMethods, method, DataContract.GetClrTypeFullName(method.DeclaringType), attributeType), method.DeclaringType);
                    }
                    else
                    {
                        if (method.ReturnType != Globals.TypeOfVoid)
                        {
                            DataContract.ThrowInvalidDataContractException(SR.Format(SR.CallbackMustReturnVoid, DataContract.GetClrTypeFullName(method.DeclaringType), method), method.DeclaringType);
                        }

                        if (parameters == null || parameters.Length != 1 || parameters[0].ParameterType != Globals.TypeOfStreamingContext)
                        {
                            DataContract.ThrowInvalidDataContractException(SR.Format(SR.CallbackParameterInvalid, DataContract.GetClrTypeFullName(method.DeclaringType), method, Globals.TypeOfStreamingContext), method.DeclaringType);
                        }

                        prevAttributeType = attributeType;
                    }
                    return true;
                }
                return false;
            }

            internal ClassDataContract BaseContract
            {
                get => baseContract;
                set
                {
                    baseContract = value;
                    if (baseContract != null && IsValueType)
                    {
                        ThrowInvalidDataContractException(SR.Format(SR.ValueTypeCannotHaveBaseType, StableName.Name, StableName.Namespace, baseContract.StableName.Name, baseContract.StableName.Namespace));
                    }
                }
            }

            internal List<DataMember> Members
            {
                get => members;
                set => members = value;
            }

            internal MethodInfo OnSerializing
            {
                get
                {
                    EnsureMethodsImported();
                    return onSerializing;
                }
            }

            internal MethodInfo OnSerialized
            {
                get
                {
                    EnsureMethodsImported();
                    return onSerialized;
                }
            }

            internal MethodInfo OnDeserializing
            {
                get
                {
                    EnsureMethodsImported();
                    return onDeserializing;
                }
            }

            internal MethodInfo OnDeserialized
            {
                get
                {
                    EnsureMethodsImported();
                    return onDeserialized;
                }
            }

            internal MethodInfo ExtensionDataSetMethod
            {
                get
                {
                    EnsureMethodsImported();
                    return extensionDataSetMethod;
                }
            }

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

            internal string SerializationExceptionMessage => serializationExceptionMessage;

            internal string DeserializationExceptionMessage
            {
                get
                {
                    if (serializationExceptionMessage == null)
                    {
                        return null;
                    }
                    else
                    {
                        return SR.Format(SR.ReadOnlyClassDeserialization, serializationExceptionMessage);
                    }
                }
            }

            internal override bool IsISerializable
            {
                get => isISerializable;
                set => isISerializable = value;
            }

            internal bool HasDataContract => hasDataContract;

            internal bool HasExtensionData => hasExtensionData;

            internal bool IsNonAttributedType => isNonAttributedType;

            internal ConstructorInfo GetISerializableConstructor()
            {
                if (!IsISerializable)
                {
                    return null;
                }

                ConstructorInfo ctor = UnderlyingType.GetConstructor(Globals.ScanAllMembers, null, SerInfoCtorArgs, null);
                if (ctor == null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.SerializationInfo_ConstructorNotFound, DataContract.GetClrTypeFullName(UnderlyingType))));
                }

                return ctor;
            }

            internal ConstructorInfo GetNonAttributedTypeConstructor()
            {
                if (!IsNonAttributedType)
                {
                    return null;
                }

                Type type = UnderlyingType;

                if (type.IsValueType)
                {
                    return null;
                }

                ConstructorInfo ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, Globals.EmptyTypeArray, null);
                if (ctor == null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidDataContractException(SR.Format(SR.NonAttributedSerializableTypesMustHaveDefaultConstructor, DataContract.GetClrTypeFullName(type))));
                }

                return ctor;
            }

            internal XmlFormatClassWriterDelegate XmlFormatWriterDelegate
            {
                get => xmlFormatWriterDelegate;
                set => xmlFormatWriterDelegate = value;
            }

            internal XmlFormatClassReaderDelegate XmlFormatReaderDelegate
            {
                get => xmlFormatReaderDelegate;
                set => xmlFormatReaderDelegate = value;
            }

            public XmlDictionaryString[] ChildElementNamespaces
            {
                get => childElementNamespaces;
                set => childElementNamespaces = value;
            }

            private static Type[] serInfoCtorArgs;

            private static Type[] SerInfoCtorArgs
            {
                get
                {
                    if (serInfoCtorArgs == null)
                    {
                        serInfoCtorArgs = new Type[] { typeof(SerializationInfo), typeof(StreamingContext) };
                    }

                    return serInfoCtorArgs;
                }
            }

            internal struct Member
            {
                internal Member(DataMember member, string ns, int baseTypeIndex)
                {
                    this.member = member;
                    this.ns = ns;
                    this.baseTypeIndex = baseTypeIndex;
                }
                internal DataMember member;
                internal string ns;
                internal int baseTypeIndex;
            }

            internal class DataMemberConflictComparer : IComparer<Member>
            {
                public int Compare(Member x, Member y)
                {
                    int nsCompare = string.CompareOrdinal(x.ns, y.ns);
                    if (nsCompare != 0)
                    {
                        return nsCompare;
                    }

                    int nameCompare = string.CompareOrdinal(x.member.Name, y.member.Name);
                    if (nameCompare != 0)
                    {
                        return nameCompare;
                    }

                    return x.baseTypeIndex - y.baseTypeIndex;
                }

                internal static DataMemberConflictComparer Singleton = new DataMemberConflictComparer();
            }

        }

        internal override DataContract BindGenericParameters(DataContract[] paramContracts, Dictionary<DataContract, DataContract> boundContracts)
        {
            Type type = UnderlyingType;
            if (!type.IsGenericType || !type.ContainsGenericParameters)
            {
                return this;
            }

            lock (this)
            {
                if (boundContracts.TryGetValue(this, out DataContract boundContract))
                {
                    return boundContract;
                }

                ClassDataContract boundClassContract = new ClassDataContract();
                boundContracts.Add(this, boundClassContract);
                XmlQualifiedName stableName;
                object[] genericParams;
                if (type.IsGenericTypeDefinition)
                {
                    stableName = StableName;
                    genericParams = paramContracts;
                }
                else
                {
                    //partial Generic: Construct stable name from its open generic type definition
                    stableName = DataContract.GetStableName(type.GetGenericTypeDefinition());
                    Type[] paramTypes = type.GetGenericArguments();
                    genericParams = new object[paramTypes.Length];
                    for (int i = 0; i < paramTypes.Length; i++)
                    {
                        Type paramType = paramTypes[i];
                        if (paramType.IsGenericParameter)
                        {
                            genericParams[i] = paramContracts[paramType.GenericParameterPosition];
                        }
                        else
                        {
                            genericParams[i] = paramType;
                        }
                    }
                }
                boundClassContract.StableName = CreateQualifiedName(
                    DataContract.ExpandGenericParameters(XmlConvert.DecodeName(stableName.Name),
                    new GenericNameProvider(DataContract.GetClrTypeFullName(UnderlyingType), genericParams)),
                    stableName.Namespace);
                if (BaseContract != null)
                {
                    boundClassContract.BaseContract = (ClassDataContract)BaseContract.BindGenericParameters(paramContracts, boundContracts);
                }

                boundClassContract.IsISerializable = IsISerializable;
                boundClassContract.IsValueType = IsValueType;
                boundClassContract.IsReference = IsReference;
                if (Members != null)
                {
                    boundClassContract.Members = new List<DataMember>(Members.Count);
                    foreach (DataMember member in Members)
                    {
                        boundClassContract.Members.Add(member.BindGenericParameters(paramContracts, boundContracts));
                    }
                }
                return boundClassContract;
            }
        }

        internal override bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
        {
            if (IsEqualOrChecked(other, checkedContracts))
            {
                return true;
            }

            if (base.Equals(other, checkedContracts))
            {
                ClassDataContract dataContract = other as ClassDataContract;
                if (dataContract != null)
                {
                    if (IsISerializable)
                    {
                        if (!dataContract.IsISerializable)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        if (dataContract.IsISerializable)
                        {
                            return false;
                        }

                        if (Members == null)
                        {
                            if (dataContract.Members != null)
                            {
                                // check that all the datamembers in dataContract.Members are optional
                                if (!IsEveryDataMemberOptional(dataContract.Members))
                                {
                                    return false;
                                }
                            }
                        }
                        else if (dataContract.Members == null)
                        {
                            // check that all the datamembers in Members are optional
                            if (!IsEveryDataMemberOptional(Members))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            Dictionary<string, DataMember> membersDictionary = new Dictionary<string, DataMember>(Members.Count);
                            List<DataMember> dataContractMembersList = new List<DataMember>();
                            for (int i = 0; i < Members.Count; i++)
                            {
                                membersDictionary.Add(Members[i].Name, Members[i]);
                            }

                            for (int i = 0; i < dataContract.Members.Count; i++)
                            {
                                // check that all datamembers common to both datacontracts match
                                if (membersDictionary.TryGetValue(dataContract.Members[i].Name, out DataMember dataMember))
                                {
                                    if (dataMember.Equals(dataContract.Members[i], checkedContracts))
                                    {
                                        membersDictionary.Remove(dataMember.Name);
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                // otherwise save the non-matching datamembers for later verification 
                                else
                                {
                                    dataContractMembersList.Add(dataContract.Members[i]);
                                }
                            }

                            // check that datamembers left over from either datacontract are optional
                            if (!IsEveryDataMemberOptional(membersDictionary.Values))
                            {
                                return false;
                            }

                            if (!IsEveryDataMemberOptional(dataContractMembersList))
                            {
                                return false;
                            }
                        }
                    }

                    if (BaseContract == null)
                    {
                        return (dataContract.BaseContract == null);
                    }
                    else if (dataContract.BaseContract == null)
                    {
                        return false;
                    }
                    else
                    {
                        return BaseContract.Equals(dataContract.BaseContract, checkedContracts);
                    }
                }
            }
            return false;
        }

        private bool IsEveryDataMemberOptional(IEnumerable<DataMember> dataMembers)
        {
            foreach (DataMember dataMember in dataMembers)
            {
                if (dataMember.IsRequired)
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        internal class DataMemberComparer : IComparer<DataMember>
        {
            public int Compare(DataMember x, DataMember y)
            {
                int orderCompare = x.Order - y.Order;
                if (orderCompare != 0)
                {
                    return orderCompare;
                }

                return string.CompareOrdinal(x.Name, y.Name);
            }

            internal static DataMemberComparer Singleton = new DataMemberComparer();
        }
    }
}
