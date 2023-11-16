using System;
using System.Collections;
using System.Reflection;
using System.Xml;
using IDeserializationCallback = System.Runtime.Serialization.IDeserializationCallback;
using IExtensibleDataObject = System.Runtime.Serialization.IExtensibleDataObject;
using SerializationException = System.Runtime.Serialization.SerializationException;

namespace System.Runtime.Serialization
{
    internal static class XmlFormatGeneratorStatics
    {
        private static MethodInfo writeStartElementMethod2;
        internal static MethodInfo WriteStartElementMethod2
        {
            get
            {
                if (writeStartElementMethod2 == null)
                {
                    writeStartElementMethod2 = typeof(XmlWriterDelegator).GetMethod(
                        nameof(XmlWriterDelegator.WriteStartElement),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(XmlDictionaryString), typeof(XmlDictionaryString) },
                        null);
                }

                return writeStartElementMethod2;
            }
        }

        private static MethodInfo writeStartElementMethod3;
        internal static MethodInfo WriteStartElementMethod3
        {
            get
            {
                if (writeStartElementMethod3 == null)
                {
                    writeStartElementMethod3 = typeof(XmlWriterDelegator).GetMethod(
                        nameof(XmlWriterDelegator.WriteStartElement),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(string), typeof(XmlDictionaryString), typeof(XmlDictionaryString) },
                        null);
                }

                return writeStartElementMethod3;
            }
        }

        private static MethodInfo writeEndElementMethod;
        internal static MethodInfo WriteEndElementMethod
        {
            get
            {
                if (writeEndElementMethod == null)
                {
                    writeEndElementMethod = typeof(XmlWriterDelegator).GetMethod(
                        nameof(XmlWriterDelegator.WriteEndElement),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { },
                        null);
                }

                return writeEndElementMethod;
            }
        }

        private static MethodInfo writeNamespaceDeclMethod;
        internal static MethodInfo WriteNamespaceDeclMethod
        {
            get
            {
                if (writeNamespaceDeclMethod == null)
                {
                    writeNamespaceDeclMethod = typeof(XmlWriterDelegator).GetMethod(
                        nameof(XmlWriterDelegator.WriteNamespaceDecl),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(XmlDictionaryString) },
                        null);
                }

                return writeNamespaceDeclMethod;
            }
        }

        private static PropertyInfo extensionDataProperty;
        internal static PropertyInfo ExtensionDataProperty
        {
            get
            {
                if (extensionDataProperty == null)
                {
                    extensionDataProperty = typeof(IExtensibleDataObject).GetProperty(nameof(IExtensibleDataObject.ExtensionData));
                }

                return extensionDataProperty;
            }
        }

        private static MethodInfo boxPointer;
        internal static MethodInfo BoxPointer
        {
            get
            {
                if (boxPointer == null)
                {
                    boxPointer = typeof(Pointer).GetMethod(nameof(Pointer.Box));
                }

                return boxPointer;
            }
        }

        private static ConstructorInfo dictionaryEnumeratorCtor;
        internal static ConstructorInfo DictionaryEnumeratorCtor
        {
            get
            {
                if (dictionaryEnumeratorCtor == null)
                {
                    dictionaryEnumeratorCtor = Globals.TypeOfDictionaryEnumerator.GetConstructor(Globals.ScanAllMembers, null, new Type[] { Globals.TypeOfIDictionaryEnumerator }, null);
                }

                return dictionaryEnumeratorCtor;
            }
        }

        private static MethodInfo ienumeratorMoveNextMethod;
        internal static MethodInfo MoveNextMethod
        {
            get
            {
                if (ienumeratorMoveNextMethod == null)
                {
                    ienumeratorMoveNextMethod = typeof(IEnumerator).GetMethod(nameof(IEnumerator.MoveNext));
                }

                return ienumeratorMoveNextMethod;
            }
        }

        private static MethodInfo ienumeratorGetCurrentMethod;
        internal static MethodInfo GetCurrentMethod
        {
            get
            {
                if (ienumeratorGetCurrentMethod == null)
                {
                    ienumeratorGetCurrentMethod = typeof(IEnumerator).GetProperty(nameof(IEnumerator.Current)).GetGetMethod();
                }

                return ienumeratorGetCurrentMethod;
            }
        }

        private static MethodInfo getItemContractMethod;
        internal static MethodInfo GetItemContractMethod
        {
            get
            {
                if (getItemContractMethod == null)
                {
                    getItemContractMethod = typeof(CollectionDataContract).GetProperty(
                        nameof(CollectionDataContract.ItemContract),
                        Globals.ScanAllMembers).GetGetMethod(true/*nonPublic*/);
                }

                return getItemContractMethod;
            }
        }

        private static MethodInfo isStartElementMethod2;
        internal static MethodInfo IsStartElementMethod2
        {
            get
            {
                if (isStartElementMethod2 == null)
                {
                    isStartElementMethod2 = typeof(XmlReaderDelegator).GetMethod(
                        nameof(XmlReaderDelegator.IsStartElement),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(XmlDictionaryString), typeof(XmlDictionaryString) },
                        null);
                }

                return isStartElementMethod2;
            }
        }


        private static MethodInfo isStartElementMethod0;
        internal static MethodInfo IsStartElementMethod0
        {
            get
            {
                if (isStartElementMethod0 == null)
                {
                    isStartElementMethod0 = typeof(XmlReaderDelegator).GetMethod(
                        nameof(XmlReaderDelegator.IsStartElement),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { },
                        null);
                }

                return isStartElementMethod0;
            }
        }


        private static MethodInfo getUninitializedObjectMethod;
        internal static MethodInfo GetUninitializedObjectMethod
        {
            get
            {
                if (getUninitializedObjectMethod == null)
                {
                    getUninitializedObjectMethod = typeof(XmlFormatReaderGenerator).GetMethod(
                        nameof(XmlFormatReaderGenerator.UnsafeGetUninitializedObject),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(int) },
                        null);
                }

                return getUninitializedObjectMethod;
            }
        }


        private static MethodInfo onDeserializationMethod;
        internal static MethodInfo OnDeserializationMethod
        {
            get
            {
                if (onDeserializationMethod == null)
                {
                    onDeserializationMethod = typeof(IDeserializationCallback).GetMethod(
                        nameof(IDeserializationCallback.OnDeserialization));
                }

                return onDeserializationMethod;
            }
        }


        private static MethodInfo unboxPointer;
        internal static MethodInfo UnboxPointer
        {
            get
            {
                if (unboxPointer == null)
                {
                    unboxPointer = typeof(Pointer).GetMethod(nameof(Pointer.Unbox));
                }

                return unboxPointer;
            }
        }


        private static PropertyInfo nodeTypeProperty;
        internal static PropertyInfo NodeTypeProperty
        {
            get
            {
                if (nodeTypeProperty == null)
                {
                    nodeTypeProperty = typeof(XmlReaderDelegator).GetProperty(
                        nameof(XmlReaderDelegator.NodeType),
                        Globals.ScanAllMembers);
                }

                return nodeTypeProperty;
            }
        }


        private static ConstructorInfo serializationExceptionCtor;
        internal static ConstructorInfo SerializationExceptionCtor
        {

            get
            {
                if (serializationExceptionCtor == null)
                {
                    serializationExceptionCtor = typeof(SerializationException).GetConstructor(new Type[] { typeof(string) });
                }

                return serializationExceptionCtor;
            }
        }


        private static ConstructorInfo extensionDataObjectCtor;
        internal static ConstructorInfo ExtensionDataObjectCtor
        {

            get
            {
                if (extensionDataObjectCtor == null)
                {
                    extensionDataObjectCtor = typeof(ExtensionDataObject).GetConstructor(Globals.ScanAllMembers, null, new Type[] { }, null);
                }

                return extensionDataObjectCtor;
            }
        }


        private static ConstructorInfo hashtableCtor;
        internal static ConstructorInfo HashtableCtor
        {

            get
            {
                if (hashtableCtor == null)
                {
                    hashtableCtor = Globals.TypeOfHashtable.GetConstructor(Globals.ScanAllMembers, null, Globals.EmptyTypeArray, null);
                }

                return hashtableCtor;
            }
        }


        private static MethodInfo getStreamingContextMethod;
        internal static MethodInfo GetStreamingContextMethod
        {

            get
            {
                if (getStreamingContextMethod == null)
                {
                    getStreamingContextMethod = typeof(XmlObjectSerializerContext).GetMethod(
                        nameof(XmlObjectSerializerContext.GetStreamingContext),
                        Globals.ScanAllMembers);
                }

                return getStreamingContextMethod;
            }
        }


        private static MethodInfo getCollectionMemberMethod;
        internal static MethodInfo GetCollectionMemberMethod
        {

            get
            {
                if (getCollectionMemberMethod == null)
                {
                    getCollectionMemberMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.GetCollectionMember),
                        Globals.ScanAllMembers);
                }

                return getCollectionMemberMethod;
            }
        }


        private static MethodInfo storeCollectionMemberInfoMethod;
        internal static MethodInfo StoreCollectionMemberInfoMethod
        {

            get
            {
                if (storeCollectionMemberInfoMethod == null)
                {
                    storeCollectionMemberInfoMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.StoreCollectionMemberInfo),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(object) },
                        null);
                }

                return storeCollectionMemberInfoMethod;
            }
        }


        private static MethodInfo storeIsGetOnlyCollectionMethod;
        internal static MethodInfo StoreIsGetOnlyCollectionMethod
        {

            get
            {
                if (storeIsGetOnlyCollectionMethod == null)
                {
                    storeIsGetOnlyCollectionMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.StoreIsGetOnlyCollection),
                        Globals.ScanAllMembers);
                }

                return storeIsGetOnlyCollectionMethod;
            }
        }


        private static MethodInfo throwNullValueReturnedForGetOnlyCollectionExceptionMethod;
        internal static MethodInfo ThrowNullValueReturnedForGetOnlyCollectionExceptionMethod
        {

            get
            {
                if (throwNullValueReturnedForGetOnlyCollectionExceptionMethod == null)
                {
                    throwNullValueReturnedForGetOnlyCollectionExceptionMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.ThrowNullValueReturnedForGetOnlyCollectionException),
                        Globals.ScanAllMembers);
                }

                return throwNullValueReturnedForGetOnlyCollectionExceptionMethod;
            }
        }

        private static MethodInfo throwArrayExceededSizeExceptionMethod;
        internal static MethodInfo ThrowArrayExceededSizeExceptionMethod
        {

            get
            {
                if (throwArrayExceededSizeExceptionMethod == null)
                {
                    throwArrayExceededSizeExceptionMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.ThrowArrayExceededSizeException),
                        Globals.ScanAllMembers);
                }

                return throwArrayExceededSizeExceptionMethod;
            }
        }


        private static MethodInfo incrementItemCountMethod;
        internal static MethodInfo IncrementItemCountMethod
        {

            get
            {
                if (incrementItemCountMethod == null)
                {
                    incrementItemCountMethod = typeof(XmlObjectSerializerContext).GetMethod(
                        nameof(XmlObjectSerializerContext.IncrementItemCount),
                        Globals.ScanAllMembers);
                }

                return incrementItemCountMethod;
            }
        }

        private static MethodInfo internalDeserializeMethod;
        internal static MethodInfo InternalDeserializeMethod
        {

            get
            {
                if (internalDeserializeMethod == null)
                {
                    internalDeserializeMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.InternalDeserialize),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(XmlReaderDelegator), typeof(int), typeof(RuntimeTypeHandle), typeof(string), typeof(string) },
                        null);
                }

                return internalDeserializeMethod;
            }
        }


        private static MethodInfo moveToNextElementMethod;
        internal static MethodInfo MoveToNextElementMethod
        {

            get
            {
                if (moveToNextElementMethod == null)
                {
                    moveToNextElementMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.MoveToNextElement),
                        Globals.ScanAllMembers);
                }

                return moveToNextElementMethod;
            }
        }


        private static MethodInfo getMemberIndexMethod;
        internal static MethodInfo GetMemberIndexMethod
        {

            get
            {
                if (getMemberIndexMethod == null)
                {
                    getMemberIndexMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.GetMemberIndex),
                        Globals.ScanAllMembers);
                }

                return getMemberIndexMethod;
            }
        }


        private static MethodInfo getMemberIndexWithRequiredMembersMethod;
        internal static MethodInfo GetMemberIndexWithRequiredMembersMethod
        {

            get
            {
                if (getMemberIndexWithRequiredMembersMethod == null)
                {
                    getMemberIndexWithRequiredMembersMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.GetMemberIndexWithRequiredMembers),
                        Globals.ScanAllMembers);
                }

                return getMemberIndexWithRequiredMembersMethod;
            }
        }


        private static MethodInfo throwRequiredMemberMissingExceptionMethod;
        internal static MethodInfo ThrowRequiredMemberMissingExceptionMethod
        {

            get
            {
                if (throwRequiredMemberMissingExceptionMethod == null)
                {
                    throwRequiredMemberMissingExceptionMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.ThrowRequiredMemberMissingException),
                        Globals.ScanAllMembers);
                }

                return throwRequiredMemberMissingExceptionMethod;
            }
        }


        private static MethodInfo skipUnknownElementMethod;
        internal static MethodInfo SkipUnknownElementMethod
        {

            get
            {
                if (skipUnknownElementMethod == null)
                {
                    skipUnknownElementMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.SkipUnknownElement),
                        Globals.ScanAllMembers);
                }

                return skipUnknownElementMethod;
            }
        }


        private static MethodInfo readIfNullOrRefMethod;
        internal static MethodInfo ReadIfNullOrRefMethod
        {

            get
            {
                if (readIfNullOrRefMethod == null)
                {
                    readIfNullOrRefMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.ReadIfNullOrRef),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(XmlReaderDelegator), typeof(Type), typeof(bool) },
                        null);
                }

                return readIfNullOrRefMethod;
            }
        }


        private static MethodInfo readAttributesMethod;
        internal static MethodInfo ReadAttributesMethod
        {

            get
            {
                if (readAttributesMethod == null)
                {
                    readAttributesMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.ReadAttributes),
                        Globals.ScanAllMembers);
                }

                return readAttributesMethod;
            }
        }


        private static MethodInfo resetAttributesMethod;
        internal static MethodInfo ResetAttributesMethod
        {

            get
            {
                if (resetAttributesMethod == null)
                {
                    resetAttributesMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.ResetAttributes),
                        Globals.ScanAllMembers);
                }

                return resetAttributesMethod;
            }
        }


        private static MethodInfo getObjectIdMethod;
        internal static MethodInfo GetObjectIdMethod
        {

            get
            {
                if (getObjectIdMethod == null)
                {
                    getObjectIdMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.GetObjectId),
                        Globals.ScanAllMembers);
                }

                return getObjectIdMethod;
            }
        }


        private static MethodInfo getArraySizeMethod;
        internal static MethodInfo GetArraySizeMethod
        {

            get
            {
                if (getArraySizeMethod == null)
                {
                    getArraySizeMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.GetArraySize),
                        Globals.ScanAllMembers);
                }

                return getArraySizeMethod;
            }
        }


        private static MethodInfo addNewObjectMethod;
        internal static MethodInfo AddNewObjectMethod
        {

            get
            {
                if (addNewObjectMethod == null)
                {
                    addNewObjectMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.AddNewObject),
                        Globals.ScanAllMembers);
                }

                return addNewObjectMethod;
            }
        }


        private static MethodInfo addNewObjectWithIdMethod;
        internal static MethodInfo AddNewObjectWithIdMethod
        {

            get
            {
                if (addNewObjectWithIdMethod == null)
                {
                    addNewObjectWithIdMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.AddNewObjectWithId),
                        Globals.ScanAllMembers);
                }

                return addNewObjectWithIdMethod;
            }
        }


        private static MethodInfo replaceDeserializedObjectMethod;
        internal static MethodInfo ReplaceDeserializedObjectMethod
        {

            get
            {
                if (replaceDeserializedObjectMethod == null)
                {
                    replaceDeserializedObjectMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.ReplaceDeserializedObject),
                        Globals.ScanAllMembers);
                }

                return replaceDeserializedObjectMethod;
            }
        }


        private static MethodInfo getExistingObjectMethod;
        internal static MethodInfo GetExistingObjectMethod
        {

            get
            {
                if (getExistingObjectMethod == null)
                {
                    getExistingObjectMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.GetExistingObject),
                        Globals.ScanAllMembers);
                }

                return getExistingObjectMethod;
            }
        }


        private static MethodInfo getRealObjectMethod;
        internal static MethodInfo GetRealObjectMethod
        {

            get
            {
                if (getRealObjectMethod == null)
                {
                    getRealObjectMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.GetRealObject),
                        Globals.ScanAllMembers);
                }

                return getRealObjectMethod;
            }
        }


        private static MethodInfo readMethod;
        internal static MethodInfo ReadMethod
        {

            get
            {
                if (readMethod == null)
                {
                    readMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.Read),
                        Globals.ScanAllMembers);
                }

                return readMethod;
            }
        }


        private static MethodInfo ensureArraySizeMethod;
        internal static MethodInfo EnsureArraySizeMethod
        {

            get
            {
                if (ensureArraySizeMethod == null)
                {
                    ensureArraySizeMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.EnsureArraySize),
                        Globals.ScanAllMembers);
                }

                return ensureArraySizeMethod;
            }
        }


        private static MethodInfo trimArraySizeMethod;
        internal static MethodInfo TrimArraySizeMethod
        {

            get
            {
                if (trimArraySizeMethod == null)
                {
                    trimArraySizeMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.TrimArraySize),
                        Globals.ScanAllMembers);
                }

                return trimArraySizeMethod;
            }
        }


        private static MethodInfo checkEndOfArrayMethod;
        internal static MethodInfo CheckEndOfArrayMethod
        {

            get
            {
                if (checkEndOfArrayMethod == null)
                {
                    checkEndOfArrayMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.CheckEndOfArray),
                        Globals.ScanAllMembers);
                }

                return checkEndOfArrayMethod;
            }
        }


        private static MethodInfo getArrayLengthMethod;
        internal static MethodInfo GetArrayLengthMethod
        {

            get
            {
                if (getArrayLengthMethod == null)
                {
                    getArrayLengthMethod = Globals.TypeOfArray.GetProperty("Length").GetGetMethod();
                }

                return getArrayLengthMethod;
            }
        }


        private static MethodInfo readSerializationInfoMethod;
        internal static MethodInfo ReadSerializationInfoMethod
        {

            get
            {
                if (readSerializationInfoMethod == null)
                {
                    readSerializationInfoMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.ReadSerializationInfo),
                        Globals.ScanAllMembers);
                }

                return readSerializationInfoMethod;
            }
        }


        private static MethodInfo createUnexpectedStateExceptionMethod;
        internal static MethodInfo CreateUnexpectedStateExceptionMethod
        {

            get
            {
                if (createUnexpectedStateExceptionMethod == null)
                {
                    createUnexpectedStateExceptionMethod = typeof(XmlObjectSerializerReadContext).GetMethod(
                        nameof(XmlObjectSerializerReadContext.CreateUnexpectedStateException),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(XmlNodeType), typeof(XmlReaderDelegator) },
                        null);
                }

                return createUnexpectedStateExceptionMethod;
            }
        }


        private static MethodInfo internalSerializeReferenceMethod;
        internal static MethodInfo InternalSerializeReferenceMethod
        {

            get
            {
                if (internalSerializeReferenceMethod == null)
                {
                    internalSerializeReferenceMethod = typeof(XmlObjectSerializerWriteContext).GetMethod("InternalSerializeReference", Globals.ScanAllMembers);
                }

                return internalSerializeReferenceMethod;
            }
        }


        private static MethodInfo internalSerializeMethod;
        internal static MethodInfo InternalSerializeMethod
        {

            get
            {
                if (internalSerializeMethod == null)
                {
                    internalSerializeMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.InternalSerialize),
                        Globals.ScanAllMembers);
                }

                return internalSerializeMethod;
            }
        }


        private static MethodInfo writeNullMethod;
        internal static MethodInfo WriteNullMethod
        {

            get
            {
                if (writeNullMethod == null)
                {
                    writeNullMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.WriteNull),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(XmlWriterDelegator), typeof(Type), typeof(bool) },
                        null);
                }

                return writeNullMethod;
            }
        }


        private static MethodInfo incrementArrayCountMethod;
        internal static MethodInfo IncrementArrayCountMethod
        {

            get
            {
                if (incrementArrayCountMethod == null)
                {
                    incrementArrayCountMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.IncrementArrayCount),
                        Globals.ScanAllMembers);
                }

                return incrementArrayCountMethod;
            }
        }


        private static MethodInfo incrementCollectionCountMethod;
        internal static MethodInfo IncrementCollectionCountMethod
        {

            get
            {
                if (incrementCollectionCountMethod == null)
                {
                    incrementCollectionCountMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.IncrementCollectionCount),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(XmlWriterDelegator), typeof(ICollection) },
                        null);
                }

                return incrementCollectionCountMethod;
            }
        }


        private static MethodInfo incrementCollectionCountGenericMethod;
        internal static MethodInfo IncrementCollectionCountGenericMethod
        {

            get
            {
                if (incrementCollectionCountGenericMethod == null)
                {
                    incrementCollectionCountGenericMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.IncrementCollectionCountGeneric),
                        Globals.ScanAllMembers);
                }

                return incrementCollectionCountGenericMethod;
            }
        }


        private static MethodInfo getDefaultValueMethod;
        internal static MethodInfo GetDefaultValueMethod
        {

            get
            {
                if (getDefaultValueMethod == null)
                {
                    getDefaultValueMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.GetDefaultValue),
                        Globals.ScanAllMembers);
                }

                return getDefaultValueMethod;
            }
        }


        private static MethodInfo getNullableValueMethod;
        internal static MethodInfo GetNullableValueMethod
        {

            get
            {
                if (getNullableValueMethod == null)
                {
                    getNullableValueMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.GetNullableValue),
                        Globals.ScanAllMembers);
                }

                return getNullableValueMethod;
            }
        }


        private static MethodInfo throwRequiredMemberMustBeEmittedMethod;
        internal static MethodInfo ThrowRequiredMemberMustBeEmittedMethod
        {

            get
            {
                if (throwRequiredMemberMustBeEmittedMethod == null)
                {
                    throwRequiredMemberMustBeEmittedMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.ThrowRequiredMemberMustBeEmitted),
                        Globals.ScanAllMembers);
                }

                return throwRequiredMemberMustBeEmittedMethod;
            }
        }


        private static MethodInfo getHasValueMethod;
        internal static MethodInfo GetHasValueMethod
        {

            get
            {
                if (getHasValueMethod == null)
                {
                    getHasValueMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.GetHasValue),
                        Globals.ScanAllMembers);
                }

                return getHasValueMethod;
            }
        }


        private static MethodInfo writeISerializableMethod;
        internal static MethodInfo WriteISerializableMethod
        {

            get
            {
                if (writeISerializableMethod == null)
                {
                    writeISerializableMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.WriteISerializable),
                        Globals.ScanAllMembers);
                }

                return writeISerializableMethod;
            }
        }


        private static MethodInfo writeExtensionDataMethod;
        internal static MethodInfo WriteExtensionDataMethod
        {

            get
            {
                if (writeExtensionDataMethod == null)
                {
                    writeExtensionDataMethod = typeof(XmlObjectSerializerWriteContext).GetMethod(
                        nameof(XmlObjectSerializerWriteContext.WriteExtensionData),
                        Globals.ScanAllMembers);
                }

                return writeExtensionDataMethod;
            }
        }


        private static MethodInfo writeXmlValueMethod;
        internal static MethodInfo WriteXmlValueMethod
        {

            get
            {
                if (writeXmlValueMethod == null)
                {
                    writeXmlValueMethod = typeof(DataContract).GetMethod(
                        nameof(DataContract.WriteXmlValue),
                        Globals.ScanAllMembers);
                }

                return writeXmlValueMethod;
            }
        }


        private static MethodInfo readXmlValueMethod;
        internal static MethodInfo ReadXmlValueMethod
        {

            get
            {
                if (readXmlValueMethod == null)
                {
                    readXmlValueMethod = typeof(DataContract).GetMethod(
                        nameof(DataContract.ReadXmlValue),
                        Globals.ScanAllMembers);
                }

                return readXmlValueMethod;
            }
        }


        private static MethodInfo throwTypeNotSerializableMethod;
        internal static MethodInfo ThrowTypeNotSerializableMethod
        {

            get
            {
                if (throwTypeNotSerializableMethod == null)
                {
                    throwTypeNotSerializableMethod = typeof(DataContract).GetMethod(
                        nameof(DataContract.ThrowTypeNotSerializable),
                        Globals.ScanAllMembers);
                }

                return throwTypeNotSerializableMethod;
            }
        }


        private static PropertyInfo namespaceProperty;
        internal static PropertyInfo NamespaceProperty
        {

            get
            {
                if (namespaceProperty == null)
                {
                    namespaceProperty = typeof(DataContract).GetProperty(
                        nameof(DataContract.Namespace),
                        Globals.ScanAllMembers);
                }

                return namespaceProperty;
            }
        }


        private static FieldInfo contractNamespacesField;
        internal static FieldInfo ContractNamespacesField
        {

            get
            {
                if (contractNamespacesField == null)
                {
                    contractNamespacesField = typeof(ClassDataContract).GetField(
                        nameof(ClassDataContract.ContractNamespaces),
                        Globals.ScanAllMembers);
                }

                return contractNamespacesField;
            }
        }


        private static FieldInfo memberNamesField;
        internal static FieldInfo MemberNamesField
        {

            get
            {
                if (memberNamesField == null)
                {
                    memberNamesField = typeof(ClassDataContract).GetField(
                        nameof(ClassDataContract.MemberNames),
                        Globals.ScanAllMembers);
                }

                return memberNamesField;
            }
        }


        private static MethodInfo extensionDataSetExplicitMethodInfo;
        internal static MethodInfo ExtensionDataSetExplicitMethodInfo
        {

            get
            {
                if (extensionDataSetExplicitMethodInfo == null)
                {
                    extensionDataSetExplicitMethodInfo = typeof(System.Runtime.Serialization.IExtensibleDataObject).GetMethod(Globals.ExtensionDataSetMethod);
                }

                return extensionDataSetExplicitMethodInfo;
            }
        }


        private static PropertyInfo childElementNamespacesProperty;
        internal static PropertyInfo ChildElementNamespacesProperty
        {

            get
            {
                if (childElementNamespacesProperty == null)
                {
                    childElementNamespacesProperty = typeof(ClassDataContract).GetProperty(
                        nameof(ClassDataContract.ChildElementNamespaces),
                        Globals.ScanAllMembers);
                }

                return childElementNamespacesProperty;
            }
        }


        private static PropertyInfo collectionItemNameProperty;
        internal static PropertyInfo CollectionItemNameProperty
        {

            get
            {
                if (collectionItemNameProperty == null)
                {
                    collectionItemNameProperty = typeof(CollectionDataContract).GetProperty(
                        nameof(CollectionDataContract.CollectionItemName),
                        Globals.ScanAllMembers);
                }

                return collectionItemNameProperty;
            }
        }


        private static PropertyInfo childElementNamespaceProperty;
        internal static PropertyInfo ChildElementNamespaceProperty
        {

            get
            {
                if (childElementNamespaceProperty == null)
                {
                    childElementNamespaceProperty = typeof(CollectionDataContract).GetProperty(
                        nameof(CollectionDataContract.ChildElementNamespace),
                        Globals.ScanAllMembers);
                }

                return childElementNamespaceProperty;
            }
        }


        private static MethodInfo getDateTimeOffsetMethod;
        internal static MethodInfo GetDateTimeOffsetMethod
        {

            get
            {
                if (getDateTimeOffsetMethod == null)
                {
                    getDateTimeOffsetMethod = typeof(DateTimeOffsetAdapter).GetMethod(
                        nameof(DateTimeOffsetAdapter.GetDateTimeOffset),
                        Globals.ScanAllMembers);
                }

                return getDateTimeOffsetMethod;
            }
        }


        private static MethodInfo getDateTimeOffsetAdapterMethod;
        internal static MethodInfo GetDateTimeOffsetAdapterMethod
        {

            get
            {
                if (getDateTimeOffsetAdapterMethod == null)
                {
                    getDateTimeOffsetAdapterMethod = typeof(DateTimeOffsetAdapter).GetMethod(
                        nameof(DateTimeOffsetAdapter.GetDateTimeOffsetAdapter),
                        Globals.ScanAllMembers);
                }

                return getDateTimeOffsetAdapterMethod;
            }
        }


        private static MethodInfo traceInstructionMethod;
        internal static MethodInfo TraceInstructionMethod
        {

            get
            {
                if (traceInstructionMethod == null)
                {
                    traceInstructionMethod = typeof(SerializationTrace).GetMethod(
                        nameof(SerializationTrace.TraceInstruction),
                        Globals.ScanAllMembers);
                }

                return traceInstructionMethod;
            }
        }


        private static MethodInfo throwInvalidDataContractExceptionMethod;
        internal static MethodInfo ThrowInvalidDataContractExceptionMethod
        {

            get
            {
                if (throwInvalidDataContractExceptionMethod == null)
                {
                    throwInvalidDataContractExceptionMethod = typeof(DataContract).GetMethod(
                        nameof(DataContract.ThrowInvalidDataContractException),
                        Globals.ScanAllMembers,
                        null,
                        new Type[] { typeof(string), typeof(Type) },
                        null);
                }

                return throwInvalidDataContractExceptionMethod;
            }
        }


        private static PropertyInfo serializeReadOnlyTypesProperty;
        internal static PropertyInfo SerializeReadOnlyTypesProperty
        {

            get
            {
                if (serializeReadOnlyTypesProperty == null)
                {
                    serializeReadOnlyTypesProperty = typeof(XmlObjectSerializerWriteContext).GetProperty(
                        nameof(XmlObjectSerializerWriteContext.SerializeReadOnlyTypes),
                        Globals.ScanAllMembers);
                }

                return serializeReadOnlyTypesProperty;
            }
        }


        private static PropertyInfo classSerializationExceptionMessageProperty;
        internal static PropertyInfo ClassSerializationExceptionMessageProperty
        {

            get
            {
                if (classSerializationExceptionMessageProperty == null)
                {
                    classSerializationExceptionMessageProperty = typeof(ClassDataContract).GetProperty(
                        nameof(ClassDataContract.SerializationExceptionMessage),
                        Globals.ScanAllMembers);
                }

                return classSerializationExceptionMessageProperty;
            }
        }


        private static PropertyInfo collectionSerializationExceptionMessageProperty;
        internal static PropertyInfo CollectionSerializationExceptionMessageProperty
        {

            get
            {
                if (collectionSerializationExceptionMessageProperty == null)
                {
                    collectionSerializationExceptionMessageProperty = typeof(CollectionDataContract).GetProperty(
                        nameof(CollectionDataContract.SerializationExceptionMessage),
                        Globals.ScanAllMembers);
                }

                return collectionSerializationExceptionMessageProperty;
            }
        }
    }
}
