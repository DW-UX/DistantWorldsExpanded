using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;

namespace System.Runtime.Serialization
{
    internal class XmlObjectSerializerReadContextComplex : XmlObjectSerializerReadContext
    {
        private static readonly Hashtable dataContractTypeCache = new Hashtable();
        private readonly bool preserveObjectReferences;
        protected IDataContractSurrogate dataContractSurrogate;
        private readonly SerializationMode mode;
        private readonly System.Runtime.Serialization.SerializationBinder binder;
        private readonly System.Runtime.Serialization.ISurrogateSelector surrogateSelector;
        private readonly FormatterAssemblyStyle assemblyFormat;
        private Hashtable surrogateDataContracts;

        internal XmlObjectSerializerReadContextComplex(
            DataContractSerializer serializer,
            DataContract rootTypeDataContract,
            System.Runtime.Serialization.DataContractResolver dataContractResolver)
            : base(serializer, rootTypeDataContract, dataContractResolver)
        {
            mode = SerializationMode.SharedContract;
            preserveObjectReferences = serializer.PreserveObjectReferences;
            dataContractSurrogate = serializer.DataContractSurrogate;
        }

        internal XmlObjectSerializerReadContextComplex(NetDataContractSerializer serializer)
            : base(serializer)
        {
            mode = SerializationMode.SharedType;
            preserveObjectReferences = true;
            binder = serializer.Binder;
            surrogateSelector = serializer.SurrogateSelector;
            assemblyFormat = serializer.AssemblyFormat;
        }

        internal XmlObjectSerializerReadContextComplex(XmlObjectSerializer2 serializer, int maxItemsInObjectGraph, System.Runtime.Serialization.StreamingContext streamingContext, bool ignoreExtensionDataObject)
            : base(serializer, maxItemsInObjectGraph, streamingContext, ignoreExtensionDataObject)
        {
        }

        internal override SerializationMode Mode => mode;

        internal override DataContract GetDataContract(int id, RuntimeTypeHandle typeHandle)
        {
            DataContract dataContract = null;
            if (mode == SerializationMode.SharedType && surrogateSelector != null)
            {
                dataContract = NetDataContractSerializer.GetDataContractFromSurrogateSelector(surrogateSelector, GetStreamingContext(), typeHandle, null /*type*/, ref surrogateDataContracts);
            }

            if (dataContract != null)
            {
                if (IsGetOnlyCollection && dataContract is SurrogateDataContract)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new System.Runtime.Serialization.InvalidDataContractException(SR.Format(SR.SurrogatesWithGetOnlyCollectionsNotSupportedSerDeser,
                        DataContract.GetClrTypeFullName(dataContract.UnderlyingType))));
                }
                return dataContract;
            }

            return base.GetDataContract(id, typeHandle);
        }

        internal override DataContract GetDataContract(RuntimeTypeHandle typeHandle, Type type)
        {
            DataContract dataContract = null;
            if (mode == SerializationMode.SharedType && surrogateSelector != null)
            {
                dataContract = NetDataContractSerializer.GetDataContractFromSurrogateSelector(surrogateSelector, GetStreamingContext(), typeHandle, type, ref surrogateDataContracts);
            }

            if (dataContract != null)
            {
                if (IsGetOnlyCollection && dataContract is SurrogateDataContract)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new System.Runtime.Serialization.InvalidDataContractException(SR.Format(SR.SurrogatesWithGetOnlyCollectionsNotSupportedSerDeser,
                        DataContract.GetClrTypeFullName(dataContract.UnderlyingType))));
                }
                return dataContract;
            }

            return base.GetDataContract(typeHandle, type);
        }

        public override object InternalDeserialize(XmlReaderDelegator xmlReader, int declaredTypeID, RuntimeTypeHandle declaredTypeHandle, string name, string ns)
        {
            if (mode == SerializationMode.SharedContract)
            {
                if (dataContractSurrogate == null)
                {
                    return base.InternalDeserialize(xmlReader, declaredTypeID, declaredTypeHandle, name, ns);
                }
                else
                {
                    return InternalDeserializeWithSurrogate(xmlReader, Type.GetTypeFromHandle(declaredTypeHandle), null /*surrogateDataContract*/, name, ns);
                }
            }
            else
            {
                return InternalDeserializeInSharedTypeMode(xmlReader, declaredTypeID, Type.GetTypeFromHandle(declaredTypeHandle), name, ns);
            }
        }

        internal override object InternalDeserialize(XmlReaderDelegator xmlReader, Type declaredType, string name, string ns)
        {
            if (mode == SerializationMode.SharedContract)
            {
                if (dataContractSurrogate == null)
                {
                    return base.InternalDeserialize(xmlReader, declaredType, name, ns);
                }
                else
                {
                    return InternalDeserializeWithSurrogate(xmlReader, declaredType, null /*surrogateDataContract*/, name, ns);
                }
            }
            else
            {
                return InternalDeserializeInSharedTypeMode(xmlReader, -1, declaredType, name, ns);
            }
        }

        internal override object InternalDeserialize(XmlReaderDelegator xmlReader, Type declaredType, DataContract dataContract, string name, string ns)
        {
            if (mode == SerializationMode.SharedContract)
            {
                if (dataContractSurrogate == null)
                {
                    return base.InternalDeserialize(xmlReader, declaredType, dataContract, name, ns);
                }
                else
                {
                    return InternalDeserializeWithSurrogate(xmlReader, declaredType, dataContract, name, ns);
                }
            }
            else
            {
                return InternalDeserializeInSharedTypeMode(xmlReader, -1, declaredType, name, ns);
            }
        }

        private object InternalDeserializeInSharedTypeMode(XmlReaderDelegator xmlReader, int declaredTypeID, Type declaredType, string name, string ns)
        {
            object retObj = null;
            if (TryHandleNullOrRef(xmlReader, declaredType, name, ns, ref retObj))
            {
                return retObj;
            }

            DataContract dataContract;
            string assemblyName = attributes.ClrAssembly;
            string typeName = attributes.ClrType;
            if (assemblyName != null && typeName != null)
            {
                dataContract = ResolveDataContractInSharedTypeMode(assemblyName, typeName, out Assembly assembly, out Type type);
                if (dataContract == null)
                {
                    if (assembly == null)
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.AssemblyNotFound, assemblyName)));
                    }

                    if (type == null)
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.ClrTypeNotFound, assembly.FullName, typeName)));
                    }
                }
                //Array covariance is not supported in XSD. If declared type is array, data is sent in format of base array
                if (declaredType != null && declaredType.IsArray)
                {
                    dataContract = (declaredTypeID < 0) ? GetDataContract(declaredType) : GetDataContract(declaredTypeID, declaredType.TypeHandle);
                }
            }
            else
            {
                if (assemblyName != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(XmlObjectSerializer2.TryAddLineInfo(xmlReader, SR.Format(SR.AttributeNotFound, Globals.SerializationNamespace, Globals.ClrTypeLocalName, xmlReader.NodeType, xmlReader.NamespaceURI, xmlReader.LocalName))));
                }
                else if (typeName != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(XmlObjectSerializer2.TryAddLineInfo(xmlReader, SR.Format(SR.AttributeNotFound, Globals.SerializationNamespace, Globals.ClrAssemblyLocalName, xmlReader.NodeType, xmlReader.NamespaceURI, xmlReader.LocalName))));
                }
                else if (declaredType == null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(XmlObjectSerializer2.TryAddLineInfo(xmlReader, SR.Format(SR.AttributeNotFound, Globals.SerializationNamespace, Globals.ClrTypeLocalName, xmlReader.NodeType, xmlReader.NamespaceURI, xmlReader.LocalName))));
                }

                dataContract = (declaredTypeID < 0) ? GetDataContract(declaredType) : GetDataContract(declaredTypeID, declaredType.TypeHandle);
            }
            return ReadDataContractValue(dataContract, xmlReader);
        }

        private object InternalDeserializeWithSurrogate(XmlReaderDelegator xmlReader, Type declaredType, DataContract surrogateDataContract, string name, string ns)
        {
            DataContract dataContract = surrogateDataContract ??
                GetDataContract(DataContractSurrogateCaller.GetDataContractType(dataContractSurrogate, declaredType));
            if (IsGetOnlyCollection && dataContract.UnderlyingType != declaredType)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new System.Runtime.Serialization.InvalidDataContractException(SR.Format(SR.SurrogatesWithGetOnlyCollectionsNotSupportedSerDeser,
                    DataContract.GetClrTypeFullName(declaredType))));
            }
            ReadAttributes(xmlReader);
            string objectId = GetObjectId();
            object oldObj = InternalDeserialize(xmlReader, name, ns, declaredType, ref dataContract);
            object obj = DataContractSurrogateCaller.GetDeserializedObject(dataContractSurrogate, oldObj, dataContract.UnderlyingType, declaredType);
            ReplaceDeserializedObject(objectId, oldObj, obj);

            return obj;
        }

        private Type ResolveDataContractTypeInSharedTypeMode(string assemblyName, string typeName, out Assembly assembly)
        {
            assembly = null;
            Type type = null;

            if (binder != null)
            {
                type = binder.BindToType(assemblyName, typeName);
            }

            if (type == null)
            {
                XmlObjectDataContractTypeKey key = new XmlObjectDataContractTypeKey(assemblyName, typeName);
                XmlObjectDataContractTypeInfo dataContractTypeInfo = (XmlObjectDataContractTypeInfo)dataContractTypeCache[key];
                if (dataContractTypeInfo == null)
                {
                    if (assemblyFormat == FormatterAssemblyStyle.Full)
                    {
                        if (assemblyName == Globals.MscorlibAssemblyName)
                        {
                            assembly = Globals.TypeOfInt.Assembly;
                        }
                        else
                        {
                            assembly = Assembly.Load(assemblyName);
                        }
                        if (assembly != null)
                        {
                            type = assembly.GetType(typeName);
                        }
                    }
                    else
                    {
                        assembly = XmlObjectSerializerReadContextComplex.ResolveSimpleAssemblyName(assemblyName);
                        if (assembly != null)
                        {
                            // Catching any exceptions that could be thrown from a failure on assembly load
                            // This is necessary, for example, if there are generic parameters that are qualified with a version of the assembly that predates the one available
                            try
                            {
                                type = assembly.GetType(typeName);
                            }
                            catch (TypeLoadException) { }
                            catch (FileNotFoundException) { }
                            catch (FileLoadException) { }
                            catch (BadImageFormatException) { }

                            if (type == null)
                            {
                                type = Type.GetType(typeName, XmlObjectSerializerReadContextComplex.ResolveSimpleAssemblyName, new TopLevelAssemblyTypeResolver(assembly).ResolveType, false /* throwOnError */);
                            }
                        }
                    }

                    if (type != null)
                    {
                        CheckTypeForwardedTo(assembly, type.Assembly, type);

                        dataContractTypeInfo = new XmlObjectDataContractTypeInfo(assembly, type);
                        lock (dataContractTypeCache)
                        {
                            if (!dataContractTypeCache.ContainsKey(key))
                            {
                                dataContractTypeCache[key] = dataContractTypeInfo;
                            }
                        }
                    }
                }
                else
                {
                    assembly = dataContractTypeInfo.Assembly;
                    type = dataContractTypeInfo.Type;
                }
            }

            return type;
        }

        private DataContract ResolveDataContractInSharedTypeMode(string assemblyName, string typeName, out Assembly assembly, out Type type)
        {
            type = ResolveDataContractTypeInSharedTypeMode(assemblyName, typeName, out assembly);
            if (type != null)
            {
                return GetDataContract(type);
            }

            return null;
        }

        protected override DataContract ResolveDataContractFromTypeName()
        {
            if (mode == SerializationMode.SharedContract)
            {
                return base.ResolveDataContractFromTypeName();
            }
            else
            {
                if (attributes.ClrAssembly != null && attributes.ClrType != null)
                {
                    return ResolveDataContractInSharedTypeMode(attributes.ClrAssembly, attributes.ClrType, out Assembly assembly, out Type type);
                }
            }
            return null;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool CheckIfTypeSerializableForSharedTypeMode(Type memberType)
        {
            Fx.Assert(surrogateSelector != null, "Method should not be called when surrogateSelector is null.");
            return (surrogateSelector.GetSurrogate(memberType, GetStreamingContext(), out System.Runtime.Serialization.ISurrogateSelector surrogateSelectorNotUsed) != null);
        }

        internal override void CheckIfTypeSerializable(Type memberType, bool isMemberTypeSerializable)
        {
            if (mode == SerializationMode.SharedType && surrogateSelector != null &&
                CheckIfTypeSerializableForSharedTypeMode(memberType))
            {
                return;
            }
            else
            {
                if (dataContractSurrogate != null)
                {
                    while (memberType.IsArray)
                    {
                        memberType = memberType.GetElementType();
                    }

                    memberType = DataContractSurrogateCaller.GetDataContractType(dataContractSurrogate, memberType);
                    if (!DataContract.IsTypeSerializable(memberType))
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new System.Runtime.Serialization.InvalidDataContractException(SR.Format(SR.TypeNotSerializable, memberType)));
                    }

                    return;
                }
            }

            base.CheckIfTypeSerializable(memberType, isMemberTypeSerializable);
        }

        internal override Type GetSurrogatedType(Type type)
        {
            if (dataContractSurrogate == null)
            {
                return base.GetSurrogatedType(type);
            }
            else
            {
                type = DataContract.UnwrapNullableType(type);
                Type surrogateType = DataContractSerializer.GetSurrogatedType(dataContractSurrogate, type);
                if (IsGetOnlyCollection && surrogateType != type)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new System.Runtime.Serialization.InvalidDataContractException(SR.Format(SR.SurrogatesWithGetOnlyCollectionsNotSupportedSerDeser,
                        DataContract.GetClrTypeFullName(type))));
                }
                else
                {
                    return surrogateType;
                }
            }
        }

#if USE_REFEMIT
        public override int GetArraySize()
#else
        internal override int GetArraySize()
#endif
        {
            return preserveObjectReferences ? attributes.ArraySZSize : -1;
        }

        private static Assembly ResolveSimpleAssemblyName(AssemblyName assemblyName)
        {
            return ResolveSimpleAssemblyName(assemblyName.FullName);
        }

        private static Assembly ResolveSimpleAssemblyName(string assemblyName)
        {
            Assembly assembly;
            if (assemblyName == Globals.MscorlibAssemblyName)
            {
                assembly = Globals.TypeOfInt.Assembly;
            }
            else
            {
                assembly = Assembly.LoadWithPartialName(assemblyName);
                if (assembly == null)
                {
                    AssemblyName an = new AssemblyName(assemblyName)
                    {
                        Version = null
                    };
                    assembly = Assembly.LoadWithPartialName(an.FullName);
                }
            }
            return assembly;
        }

        private static void CheckTypeForwardedTo(Assembly sourceAssembly, Assembly destinationAssembly, Type resolvedType)
        {
            if (sourceAssembly != destinationAssembly && !NetDataContractSerializer.UnsafeTypeForwardingEnabled && !sourceAssembly.IsFullyTrusted)
            {
                // We have a TypeForwardedTo attribute
                //if (!destinationAssembly.PermissionSet.IsSubsetOf(sourceAssembly.PermissionSet))
                //{
                //    // We look for a matching TypeForwardedFrom attribute
                //    TypeInformation typeInfo = NetDataContractSerializer.GetTypeInformation(resolvedType);
                //    if (typeInfo.HasTypeForwardedFrom)
                //    {
                //        Assembly typeForwardedFromAssembly = null;
                //        try
                //        {
                //            // if this Assembly.Load fails, we still want to throw security exception
                //            typeForwardedFromAssembly = Assembly.Load(typeInfo.AssemblyString);
                //        }
                //        catch { }

                //        if (typeForwardedFromAssembly == sourceAssembly)
                //        {
                //            return;
                //        }
                //    }
                //    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.CannotDeserializeForwardedType, DataContract.GetClrTypeFullName(resolvedType))));
                //}
            }
        }

        private sealed class TopLevelAssemblyTypeResolver
        {
            private readonly Assembly topLevelAssembly;

            public TopLevelAssemblyTypeResolver(Assembly topLevelAssembly)
            {
                this.topLevelAssembly = topLevelAssembly;
            }
            public Type ResolveType(Assembly assembly, string simpleTypeName, bool ignoreCase)
            {
                if (assembly == null)
                {
                    assembly = topLevelAssembly;
                }

                return assembly.GetType(simpleTypeName, false, ignoreCase);
            }
        }

        private class XmlObjectDataContractTypeInfo
        {
            private readonly Assembly assembly;
            private readonly Type type;
            public XmlObjectDataContractTypeInfo(Assembly assembly, Type type)
            {
                this.assembly = assembly;
                this.type = type;
            }

            public Assembly Assembly => assembly;

            public Type Type => type;
        }

        private class XmlObjectDataContractTypeKey
        {
            private readonly string assemblyName;
            private readonly string typeName;
            public XmlObjectDataContractTypeKey(string assemblyName, string typeName)
            {
                this.assemblyName = assemblyName;
                this.typeName = typeName;
            }

            public override bool Equals(object obj)
            {
                if (object.ReferenceEquals(this, obj))
                {
                    return true;
                }

                XmlObjectDataContractTypeKey other = obj as XmlObjectDataContractTypeKey;
                if (other == null)
                {
                    return false;
                }

                if (assemblyName != other.assemblyName)
                {
                    return false;
                }

                if (typeName != other.typeName)
                {
                    return false;
                }

                return true;
            }

            public override int GetHashCode()
            {
                int hashCode = 0;
                if (assemblyName != null)
                {
                    hashCode = assemblyName.GetHashCode();
                }

                if (typeName != null)
                {
                    hashCode ^= typeName.GetHashCode();
                }

                return hashCode;
            }
        }
    }
}
