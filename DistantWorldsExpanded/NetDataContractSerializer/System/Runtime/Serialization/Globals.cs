using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
    internal static class Globals
    {
        internal const BindingFlags ScanAllMembers = BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        private static XmlQualifiedName idQualifiedName;
        internal static XmlQualifiedName IdQualifiedName
        {

            get
            {
                if (idQualifiedName == null)
                {
                    idQualifiedName = new XmlQualifiedName(Globals.IdLocalName, Globals.SerializationNamespace);
                }

                return idQualifiedName;
            }
        }

        private static XmlQualifiedName refQualifiedName;
        internal static XmlQualifiedName RefQualifiedName
        {
            get
            {
                if (refQualifiedName == null)
                {
                    refQualifiedName = new XmlQualifiedName(Globals.RefLocalName, Globals.SerializationNamespace);
                }

                return refQualifiedName;
            }
        }

        private static Type typeOfObject;
        internal static Type TypeOfObject
        {
            get
            {
                if (typeOfObject == null)
                {
                    typeOfObject = typeof(object);
                }

                return typeOfObject;
            }
        }

        private static Type typeOfValueType;
        internal static Type TypeOfValueType
        {
            get
            {
                if (typeOfValueType == null)
                {
                    typeOfValueType = typeof(ValueType);
                }

                return typeOfValueType;
            }
        }

        private static Type typeOfArray;
        internal static Type TypeOfArray
        {
            get
            {
                if (typeOfArray == null)
                {
                    typeOfArray = typeof(Array);
                }

                return typeOfArray;
            }
        }

        private static Type typeOfString;
        internal static Type TypeOfString
        {
            get
            {
                if (typeOfString == null)
                {
                    typeOfString = typeof(string);
                }

                return typeOfString;
            }
        }

        private static Type typeOfInt;
        internal static Type TypeOfInt
        {
            get
            {
                if (typeOfInt == null)
                {
                    typeOfInt = typeof(int);
                }

                return typeOfInt;
            }
        }

        private static Type typeOfULong;
        internal static Type TypeOfULong
        {
            get
            {
                if (typeOfULong == null)
                {
                    typeOfULong = typeof(ulong);
                }

                return typeOfULong;
            }
        }

        private static Type typeOfVoid;
        internal static Type TypeOfVoid
        {
            get
            {
                if (typeOfVoid == null)
                {
                    typeOfVoid = typeof(void);
                }

                return typeOfVoid;
            }
        }

        private static Type typeOfByteArray;
        internal static Type TypeOfByteArray
        {
            get
            {
                if (typeOfByteArray == null)
                {
                    typeOfByteArray = typeof(byte[]);
                }

                return typeOfByteArray;
            }
        }

        private static Type typeOfTimeSpan;
        internal static Type TypeOfTimeSpan
        {
            get
            {
                if (typeOfTimeSpan == null)
                {
                    typeOfTimeSpan = typeof(TimeSpan);
                }

                return typeOfTimeSpan;
            }
        }

        private static Type typeOfGuid;
        internal static Type TypeOfGuid
        {
            get
            {
                if (typeOfGuid == null)
                {
                    typeOfGuid = typeof(Guid);
                }

                return typeOfGuid;
            }
        }

        private static Type typeOfDateTimeOffset;
        internal static Type TypeOfDateTimeOffset
        {
            get
            {
                if (typeOfDateTimeOffset == null)
                {
                    typeOfDateTimeOffset = typeof(DateTimeOffset);
                }

                return typeOfDateTimeOffset;
            }
        }

        private static Type typeOfDateTimeOffsetAdapter;
        internal static Type TypeOfDateTimeOffsetAdapter
        {
            get
            {
                if (typeOfDateTimeOffsetAdapter == null)
                {
                    typeOfDateTimeOffsetAdapter = typeof(DateTimeOffsetAdapter);
                }

                return typeOfDateTimeOffsetAdapter;
            }
        }

        private static Type typeOfUri;
        internal static Type TypeOfUri
        {
            get
            {
                if (typeOfUri == null)
                {
                    typeOfUri = typeof(Uri);
                }

                return typeOfUri;
            }
        }

        private static Type typeOfTypeEnumerable;
        internal static Type TypeOfTypeEnumerable
        {
            get
            {
                if (typeOfTypeEnumerable == null)
                {
                    typeOfTypeEnumerable = typeof(IEnumerable<Type>);
                }

                return typeOfTypeEnumerable;
            }
        }

        private static Type typeOfStreamingContext;
        internal static Type TypeOfStreamingContext
        {
            get
            {
                if (typeOfStreamingContext == null)
                {
                    typeOfStreamingContext = typeof(System.Runtime.Serialization.StreamingContext);
                }

                return typeOfStreamingContext;
            }
        }

        private static Type typeOfISerializable;
        internal static Type TypeOfISerializable
        {
            get
            {
                if (typeOfISerializable == null)
                {
                    typeOfISerializable = typeof(System.Runtime.Serialization.ISerializable);
                }

                return typeOfISerializable;
            }
        }

        private static Type typeOfIDeserializationCallback;
        internal static Type TypeOfIDeserializationCallback
        {
            get
            {
                if (typeOfIDeserializationCallback == null)
                {
                    typeOfIDeserializationCallback = typeof(System.Runtime.Serialization.IDeserializationCallback);
                }

                return typeOfIDeserializationCallback;
            }
        }

        private static Type typeOfIObjectReference;
        internal static Type TypeOfIObjectReference
        {
            get
            {
                if (typeOfIObjectReference == null)
                {
                    typeOfIObjectReference = typeof(System.Runtime.Serialization.IObjectReference);
                }

                return typeOfIObjectReference;
            }
        }

        private static Type typeOfXmlFormatClassWriterDelegate;
        internal static Type TypeOfXmlFormatClassWriterDelegate
        {
            get
            {
                if (typeOfXmlFormatClassWriterDelegate == null)
                {
                    typeOfXmlFormatClassWriterDelegate = typeof(XmlFormatClassWriterDelegate);
                }

                return typeOfXmlFormatClassWriterDelegate;
            }
        }

        private static Type typeOfXmlFormatCollectionWriterDelegate;
        internal static Type TypeOfXmlFormatCollectionWriterDelegate
        {
            get
            {
                if (typeOfXmlFormatCollectionWriterDelegate == null)
                {
                    typeOfXmlFormatCollectionWriterDelegate = typeof(XmlFormatCollectionWriterDelegate);
                }

                return typeOfXmlFormatCollectionWriterDelegate;
            }
        }

        private static Type typeOfXmlFormatClassReaderDelegate;
        internal static Type TypeOfXmlFormatClassReaderDelegate
        {
            get
            {
                if (typeOfXmlFormatClassReaderDelegate == null)
                {
                    typeOfXmlFormatClassReaderDelegate = typeof(XmlFormatClassReaderDelegate);
                }

                return typeOfXmlFormatClassReaderDelegate;
            }
        }


        private static Type typeOfXmlFormatCollectionReaderDelegate;
        internal static Type TypeOfXmlFormatCollectionReaderDelegate
        {
            get
            {
                if (typeOfXmlFormatCollectionReaderDelegate == null)
                {
                    typeOfXmlFormatCollectionReaderDelegate = typeof(XmlFormatCollectionReaderDelegate);
                }

                return typeOfXmlFormatCollectionReaderDelegate;
            }
        }


        private static Type typeOfXmlFormatGetOnlyCollectionReaderDelegate;
        internal static Type TypeOfXmlFormatGetOnlyCollectionReaderDelegate
        {

            get
            {
                if (typeOfXmlFormatGetOnlyCollectionReaderDelegate == null)
                {
                    typeOfXmlFormatGetOnlyCollectionReaderDelegate = typeof(XmlFormatGetOnlyCollectionReaderDelegate);
                }

                return typeOfXmlFormatGetOnlyCollectionReaderDelegate;
            }
        }


        private static Type typeOfKnownTypeAttribute;
        internal static Type TypeOfKnownTypeAttribute
        {

            get
            {
                if (typeOfKnownTypeAttribute == null)
                {
                    typeOfKnownTypeAttribute = typeof(System.Runtime.Serialization.KnownTypeAttribute);
                }

                return typeOfKnownTypeAttribute;
            }
        }


        private static Type typeOfDataContractAttribute;
        internal static Type TypeOfDataContractAttribute
        {

            get
            {
                if (typeOfDataContractAttribute == null)
                {
                    typeOfDataContractAttribute = typeof(System.Runtime.Serialization.DataContractAttribute);
                }

                return typeOfDataContractAttribute;
            }
        }


        private static Type typeOfContractNamespaceAttribute;
        internal static Type TypeOfContractNamespaceAttribute
        {

            get
            {
                if (typeOfContractNamespaceAttribute == null)
                {
                    typeOfContractNamespaceAttribute = typeof(System.Runtime.Serialization.ContractNamespaceAttribute);
                }

                return typeOfContractNamespaceAttribute;
            }
        }


        private static Type typeOfDataMemberAttribute;
        internal static Type TypeOfDataMemberAttribute
        {

            get
            {
                if (typeOfDataMemberAttribute == null)
                {
                    typeOfDataMemberAttribute = typeof(System.Runtime.Serialization.DataMemberAttribute);
                }

                return typeOfDataMemberAttribute;
            }
        }


        private static Type typeOfEnumMemberAttribute;
        internal static Type TypeOfEnumMemberAttribute
        {

            get
            {
                if (typeOfEnumMemberAttribute == null)
                {
                    typeOfEnumMemberAttribute = typeof(System.Runtime.Serialization.EnumMemberAttribute);
                }

                return typeOfEnumMemberAttribute;
            }
        }


        private static Type typeOfCollectionDataContractAttribute;
        internal static Type TypeOfCollectionDataContractAttribute
        {

            get
            {
                if (typeOfCollectionDataContractAttribute == null)
                {
                    typeOfCollectionDataContractAttribute = typeof(System.Runtime.Serialization.CollectionDataContractAttribute);
                }

                return typeOfCollectionDataContractAttribute;
            }
        }


        private static Type typeOfOptionalFieldAttribute;
        internal static Type TypeOfOptionalFieldAttribute
        {

            get
            {
                if (typeOfOptionalFieldAttribute == null)
                {
                    typeOfOptionalFieldAttribute = typeof(System.Runtime.Serialization.OptionalFieldAttribute);
                }

                return typeOfOptionalFieldAttribute;
            }
        }


        private static Type typeOfObjectArray;
        internal static Type TypeOfObjectArray
        {

            get
            {
                if (typeOfObjectArray == null)
                {
                    typeOfObjectArray = typeof(object[]);
                }

                return typeOfObjectArray;
            }
        }


        private static Type typeOfOnSerializingAttribute;
        internal static Type TypeOfOnSerializingAttribute
        {

            get
            {
                if (typeOfOnSerializingAttribute == null)
                {
                    typeOfOnSerializingAttribute = typeof(System.Runtime.Serialization.OnSerializingAttribute);
                }

                return typeOfOnSerializingAttribute;
            }
        }


        private static Type typeOfOnSerializedAttribute;
        internal static Type TypeOfOnSerializedAttribute
        {

            get
            {
                if (typeOfOnSerializedAttribute == null)
                {
                    typeOfOnSerializedAttribute = typeof(System.Runtime.Serialization.OnSerializedAttribute);
                }

                return typeOfOnSerializedAttribute;
            }
        }


        private static Type typeOfOnDeserializingAttribute;
        internal static Type TypeOfOnDeserializingAttribute
        {

            get
            {
                if (typeOfOnDeserializingAttribute == null)
                {
                    typeOfOnDeserializingAttribute = typeof(System.Runtime.Serialization.OnDeserializingAttribute);
                }

                return typeOfOnDeserializingAttribute;
            }
        }


        private static Type typeOfOnDeserializedAttribute;
        internal static Type TypeOfOnDeserializedAttribute
        {

            get
            {
                if (typeOfOnDeserializedAttribute == null)
                {
                    typeOfOnDeserializedAttribute = typeof(System.Runtime.Serialization.OnDeserializedAttribute);
                }

                return typeOfOnDeserializedAttribute;
            }
        }


        private static Type typeOfFlagsAttribute;
        internal static Type TypeOfFlagsAttribute
        {

            get
            {
                if (typeOfFlagsAttribute == null)
                {
                    typeOfFlagsAttribute = typeof(FlagsAttribute);
                }

                return typeOfFlagsAttribute;
            }
        }


        private static Type typeOfSerializableAttribute;
        internal static Type TypeOfSerializableAttribute
        {

            get
            {
                if (typeOfSerializableAttribute == null)
                {
                    typeOfSerializableAttribute = typeof(SerializableAttribute);
                }

                return typeOfSerializableAttribute;
            }
        }


        private static Type typeOfNonSerializedAttribute;
        internal static Type TypeOfNonSerializedAttribute
        {

            get
            {
                if (typeOfNonSerializedAttribute == null)
                {
                    typeOfNonSerializedAttribute = typeof(NonSerializedAttribute);
                }

                return typeOfNonSerializedAttribute;
            }
        }


        private static Type typeOfSerializationInfo;
        internal static Type TypeOfSerializationInfo
        {

            get
            {
                if (typeOfSerializationInfo == null)
                {
                    typeOfSerializationInfo = typeof(System.Runtime.Serialization.SerializationInfo);
                }

                return typeOfSerializationInfo;
            }
        }


        private static Type typeOfSerializationInfoEnumerator;
        internal static Type TypeOfSerializationInfoEnumerator
        {

            get
            {
                if (typeOfSerializationInfoEnumerator == null)
                {
                    typeOfSerializationInfoEnumerator = typeof(System.Runtime.Serialization.SerializationInfoEnumerator);
                }

                return typeOfSerializationInfoEnumerator;
            }
        }


        private static Type typeOfSerializationEntry;
        internal static Type TypeOfSerializationEntry
        {

            get
            {
                if (typeOfSerializationEntry == null)
                {
                    typeOfSerializationEntry = typeof(System.Runtime.Serialization.SerializationEntry);
                }

                return typeOfSerializationEntry;
            }
        }


        private static Type typeOfIXmlSerializable;
        internal static Type TypeOfIXmlSerializable
        {

            get
            {
                if (typeOfIXmlSerializable == null)
                {
                    typeOfIXmlSerializable = typeof(IXmlSerializable);
                }

                return typeOfIXmlSerializable;
            }
        }


        private static Type typeOfXmlSchemaProviderAttribute;
        internal static Type TypeOfXmlSchemaProviderAttribute
        {

            get
            {
                if (typeOfXmlSchemaProviderAttribute == null)
                {
                    typeOfXmlSchemaProviderAttribute = typeof(XmlSchemaProviderAttribute);
                }

                return typeOfXmlSchemaProviderAttribute;
            }
        }


        private static Type typeOfXmlRootAttribute;
        internal static Type TypeOfXmlRootAttribute
        {

            get
            {
                if (typeOfXmlRootAttribute == null)
                {
                    typeOfXmlRootAttribute = typeof(XmlRootAttribute);
                }

                return typeOfXmlRootAttribute;
            }
        }


        private static Type typeOfXmlQualifiedName;
        internal static Type TypeOfXmlQualifiedName
        {

            get
            {
                if (typeOfXmlQualifiedName == null)
                {
                    typeOfXmlQualifiedName = typeof(XmlQualifiedName);
                }

                return typeOfXmlQualifiedName;
            }
        }


        private static Type typeOfXmlSchemaType;
        internal static Type TypeOfXmlSchemaType
        {

            get
            {
                if (typeOfXmlSchemaType == null)
                {
                    typeOfXmlSchemaType = typeof(XmlSchemaType);
                }

                return typeOfXmlSchemaType;
            }
        }


        private static Type typeOfXmlSerializableServices;
        internal static Type TypeOfXmlSerializableServices
        {

            get
            {
                if (typeOfXmlSerializableServices == null)
                {
                    typeOfXmlSerializableServices = typeof(System.Runtime.Serialization.XmlSerializableServices);
                }

                return typeOfXmlSerializableServices;
            }
        }


        private static Type typeOfXmlNodeArray;
        internal static Type TypeOfXmlNodeArray
        {

            get
            {
                if (typeOfXmlNodeArray == null)
                {
                    typeOfXmlNodeArray = typeof(XmlNode[]);
                }

                return typeOfXmlNodeArray;
            }
        }


        private static Type typeOfXmlSchemaSet;
        internal static Type TypeOfXmlSchemaSet
        {

            get
            {
                if (typeOfXmlSchemaSet == null)
                {
                    typeOfXmlSchemaSet = typeof(XmlSchemaSet);
                }

                return typeOfXmlSchemaSet;
            }
        }


        private static object[] emptyObjectArray;
        internal static object[] EmptyObjectArray
        {

            get
            {
                if (emptyObjectArray == null)
                {
                    emptyObjectArray = new object[0];
                }

                return emptyObjectArray;
            }
        }


        private static Type[] emptyTypeArray;
        internal static Type[] EmptyTypeArray
        {

            get
            {
                if (emptyTypeArray == null)
                {
                    emptyTypeArray = new Type[0];
                }

                return emptyTypeArray;
            }
        }


        private static Type typeOfIPropertyChange;
        internal static Type TypeOfIPropertyChange
        {

            get
            {
                if (typeOfIPropertyChange == null)
                {
                    typeOfIPropertyChange = typeof(INotifyPropertyChanged);
                }

                return typeOfIPropertyChange;
            }
        }


        private static Type typeOfIExtensibleDataObject;
        internal static Type TypeOfIExtensibleDataObject
        {

            get
            {
                if (typeOfIExtensibleDataObject == null)
                {
                    typeOfIExtensibleDataObject = typeof(System.Runtime.Serialization.IExtensibleDataObject);
                }

                return typeOfIExtensibleDataObject;
            }
        }


        private static Type typeOfExtensionDataObject;
        internal static Type TypeOfExtensionDataObject
        {

            get
            {
                if (typeOfExtensionDataObject == null)
                {
                    typeOfExtensionDataObject = typeof(ExtensionDataObject);
                }

                return typeOfExtensionDataObject;
            }
        }


        private static Type typeOfISerializableDataNode;
        internal static Type TypeOfISerializableDataNode
        {

            get
            {
                if (typeOfISerializableDataNode == null)
                {
                    typeOfISerializableDataNode = typeof(ISerializableDataNode);
                }

                return typeOfISerializableDataNode;
            }
        }


        private static Type typeOfClassDataNode;
        internal static Type TypeOfClassDataNode
        {

            get
            {
                if (typeOfClassDataNode == null)
                {
                    typeOfClassDataNode = typeof(ClassDataNode);
                }

                return typeOfClassDataNode;
            }
        }


        private static Type typeOfCollectionDataNode;
        internal static Type TypeOfCollectionDataNode
        {

            get
            {
                if (typeOfCollectionDataNode == null)
                {
                    typeOfCollectionDataNode = typeof(CollectionDataNode);
                }

                return typeOfCollectionDataNode;
            }
        }


        private static Type typeOfXmlDataNode;
        internal static Type TypeOfXmlDataNode
        {

            get
            {
                if (typeOfXmlDataNode == null)
                {
                    typeOfXmlDataNode = typeof(XmlDataNode);
                }

                return typeOfXmlDataNode;
            }
        }


        private static Type typeOfNullable;
        internal static Type TypeOfNullable
        {

            get
            {
                if (typeOfNullable == null)
                {
                    typeOfNullable = typeof(Nullable<>);
                }

                return typeOfNullable;
            }
        }


        private static Type typeOfReflectionPointer;
        internal static Type TypeOfReflectionPointer
        {

            get
            {
                if (typeOfReflectionPointer == null)
                {
                    typeOfReflectionPointer = typeof(System.Reflection.Pointer);
                }

                return typeOfReflectionPointer;
            }
        }


        private static Type typeOfIDictionaryGeneric;
        internal static Type TypeOfIDictionaryGeneric
        {

            get
            {
                if (typeOfIDictionaryGeneric == null)
                {
                    typeOfIDictionaryGeneric = typeof(IDictionary<,>);
                }

                return typeOfIDictionaryGeneric;
            }
        }


        private static Type typeOfIDictionary;
        internal static Type TypeOfIDictionary
        {

            get
            {
                if (typeOfIDictionary == null)
                {
                    typeOfIDictionary = typeof(IDictionary);
                }

                return typeOfIDictionary;
            }
        }


        private static Type typeOfIListGeneric;
        internal static Type TypeOfIListGeneric
        {

            get
            {
                if (typeOfIListGeneric == null)
                {
                    typeOfIListGeneric = typeof(IList<>);
                }

                return typeOfIListGeneric;
            }
        }


        private static Type typeOfIList;
        internal static Type TypeOfIList
        {

            get
            {
                if (typeOfIList == null)
                {
                    typeOfIList = typeof(IList);
                }

                return typeOfIList;
            }
        }


        private static Type typeOfICollectionGeneric;
        internal static Type TypeOfICollectionGeneric
        {

            get
            {
                if (typeOfICollectionGeneric == null)
                {
                    typeOfICollectionGeneric = typeof(ICollection<>);
                }

                return typeOfICollectionGeneric;
            }
        }


        private static Type typeOfICollection;
        internal static Type TypeOfICollection
        {

            get
            {
                if (typeOfICollection == null)
                {
                    typeOfICollection = typeof(ICollection);
                }

                return typeOfICollection;
            }
        }


        private static Type typeOfIEnumerableGeneric;
        internal static Type TypeOfIEnumerableGeneric
        {

            get
            {
                if (typeOfIEnumerableGeneric == null)
                {
                    typeOfIEnumerableGeneric = typeof(IEnumerable<>);
                }

                return typeOfIEnumerableGeneric;
            }
        }


        private static Type typeOfIEnumerable;
        internal static Type TypeOfIEnumerable
        {

            get
            {
                if (typeOfIEnumerable == null)
                {
                    typeOfIEnumerable = typeof(IEnumerable);
                }

                return typeOfIEnumerable;
            }
        }


        private static Type typeOfIEnumeratorGeneric;
        internal static Type TypeOfIEnumeratorGeneric
        {

            get
            {
                if (typeOfIEnumeratorGeneric == null)
                {
                    typeOfIEnumeratorGeneric = typeof(IEnumerator<>);
                }

                return typeOfIEnumeratorGeneric;
            }
        }


        private static Type typeOfIEnumerator;
        internal static Type TypeOfIEnumerator
        {

            get
            {
                if (typeOfIEnumerator == null)
                {
                    typeOfIEnumerator = typeof(IEnumerator);
                }

                return typeOfIEnumerator;
            }
        }


        private static Type typeOfKeyValuePair;
        internal static Type TypeOfKeyValuePair
        {

            get
            {
                if (typeOfKeyValuePair == null)
                {
                    typeOfKeyValuePair = typeof(KeyValuePair<,>);
                }

                return typeOfKeyValuePair;
            }
        }


        private static Type typeOfKeyValue;
        internal static Type TypeOfKeyValue
        {

            get
            {
                if (typeOfKeyValue == null)
                {
                    typeOfKeyValue = typeof(KeyValue<,>);
                }

                return typeOfKeyValue;
            }
        }


        private static Type typeOfIDictionaryEnumerator;
        internal static Type TypeOfIDictionaryEnumerator
        {

            get
            {
                if (typeOfIDictionaryEnumerator == null)
                {
                    typeOfIDictionaryEnumerator = typeof(IDictionaryEnumerator);
                }

                return typeOfIDictionaryEnumerator;
            }
        }


        private static Type typeOfDictionaryEnumerator;
        internal static Type TypeOfDictionaryEnumerator
        {

            get
            {
                if (typeOfDictionaryEnumerator == null)
                {
                    typeOfDictionaryEnumerator = typeof(CollectionDataContract.DictionaryEnumerator);
                }

                return typeOfDictionaryEnumerator;
            }
        }


        private static Type typeOfGenericDictionaryEnumerator;
        internal static Type TypeOfGenericDictionaryEnumerator
        {

            get
            {
                if (typeOfGenericDictionaryEnumerator == null)
                {
                    typeOfGenericDictionaryEnumerator = typeof(CollectionDataContract.GenericDictionaryEnumerator<,>);
                }

                return typeOfGenericDictionaryEnumerator;
            }
        }


        private static Type typeOfDictionaryGeneric;
        internal static Type TypeOfDictionaryGeneric
        {

            get
            {
                if (typeOfDictionaryGeneric == null)
                {
                    typeOfDictionaryGeneric = typeof(Dictionary<,>);
                }

                return typeOfDictionaryGeneric;
            }
        }


        private static Type typeOfHashtable;
        internal static Type TypeOfHashtable
        {

            get
            {
                if (typeOfHashtable == null)
                {
                    typeOfHashtable = typeof(Hashtable);
                }

                return typeOfHashtable;
            }
        }


        private static Type typeOfListGeneric;
        internal static Type TypeOfListGeneric
        {

            get
            {
                if (typeOfListGeneric == null)
                {
                    typeOfListGeneric = typeof(List<>);
                }

                return typeOfListGeneric;
            }
        }


        private static Type typeOfXmlElement;
        internal static Type TypeOfXmlElement
        {

            get
            {
                if (typeOfXmlElement == null)
                {
                    typeOfXmlElement = typeof(XmlElement);
                }

                return typeOfXmlElement;
            }
        }


        private static Type typeOfDBNull;
        internal static Type TypeOfDBNull
        {

            get
            {
                if (typeOfDBNull == null)
                {
                    typeOfDBNull = typeof(DBNull);
                }

                return typeOfDBNull;
            }
        }


        private static Uri dataContractXsdBaseNamespaceUri;
        internal static Uri DataContractXsdBaseNamespaceUri
        {

            get
            {
                if (dataContractXsdBaseNamespaceUri == null)
                {
                    dataContractXsdBaseNamespaceUri = new Uri(DataContractXsdBaseNamespace);
                }

                return dataContractXsdBaseNamespaceUri;
            }
        }


        public const bool DefaultIsRequired = false;
        public const bool DefaultEmitDefaultValue = true;
        public const int DefaultOrder = 0;
        public const bool DefaultIsReference = false;
        // The value string.Empty aids comparisons (can do simple length checks
        //     instead of string comparison method calls in IL.)
        public static readonly string NewObjectId = string.Empty;
        public const string SimpleSRSInternalsVisiblePattern = @"^[\s]*System\.Runtime\.Serialization[\s]*$";
        public const string FullSRSInternalsVisiblePattern = @"^[\s]*System\.Runtime\.Serialization[\s]*,[\s]*PublicKey[\s]*=[\s]*(?i:00000000000000000400000000000000)[\s]*$";
        public const string NullObjectId = null;
        public const string Space = " ";
        public const string OpenBracket = "[";
        public const string CloseBracket = "]";
        public const string Comma = ",";
        public const string XsiPrefix = "i";
        public const string XsdPrefix = "x";
        public const string SerPrefix = "z";
        public const string SerPrefixForSchema = "ser";
        public const string ElementPrefix = "q";
        public const string DataContractXsdBaseNamespace = "http://schemas.datacontract.org/2004/07/";
        public const string DataContractXmlNamespace = DataContractXsdBaseNamespace + "System.Xml";
        public const string SchemaInstanceNamespace = XmlSchema.InstanceNamespace;
        public const string SchemaNamespace = XmlSchema.Namespace;
        public const string XsiNilLocalName = "nil";
        public const string XsiTypeLocalName = "type";
        public const string TnsPrefix = "tns";
        public const string OccursUnbounded = "unbounded";
        public const string AnyTypeLocalName = "anyType";
        public const string StringLocalName = "string";
        public const string IntLocalName = "int";
        public const string True = "true";
        public const string False = "false";
        public const string ArrayPrefix = "ArrayOf";
        public const string XmlnsNamespace = "http://www.w3.org/2000/xmlns/";
        public const string XmlnsPrefix = "xmlns";
        public const string SchemaLocalName = "schema";
        public const string CollectionsNamespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays";
        public const string DefaultClrNamespace = "GeneratedNamespace";
        public const string DefaultTypeName = "GeneratedType";
        public const string DefaultGeneratedMember = "GeneratedMember";
        public const string DefaultFieldSuffix = "Field";
        public const string DefaultPropertySuffix = "Property";
        public const string DefaultMemberSuffix = "Member";
        public const string NameProperty = "Name";
        public const string NamespaceProperty = "Namespace";
        public const string OrderProperty = "Order";
        public const string IsReferenceProperty = "IsReference";
        public const string IsRequiredProperty = "IsRequired";
        public const string EmitDefaultValueProperty = "EmitDefaultValue";
        public const string ClrNamespaceProperty = "ClrNamespace";
        public const string ItemNameProperty = "ItemName";
        public const string KeyNameProperty = "KeyName";
        public const string ValueNameProperty = "ValueName";
        public const string SerializationInfoPropertyName = "SerializationInfo";
        public const string SerializationInfoFieldName = "info";
        public const string NodeArrayPropertyName = "Nodes";
        public const string NodeArrayFieldName = "nodesField";
        public const string ExportSchemaMethod = "ExportSchema";
        public const string IsAnyProperty = "IsAny";
        public const string ContextFieldName = "context";
        public const string GetObjectDataMethodName = "GetObjectData";
        public const string GetEnumeratorMethodName = "GetEnumerator";
        public const string MoveNextMethodName = "MoveNext";
        public const string AddValueMethodName = "AddValue";
        public const string CurrentPropertyName = "Current";
        public const string ValueProperty = "Value";
        public const string EnumeratorFieldName = "enumerator";
        public const string SerializationEntryFieldName = "entry";
        public const string ExtensionDataSetMethod = "set_ExtensionData";
        public const string ExtensionDataSetExplicitMethod = "System.Runtime.Serialization.IExtensibleDataObject.set_ExtensionData";
        public const string ExtensionDataObjectPropertyName = "ExtensionData";
        public const string ExtensionDataObjectFieldName = "extensionDataField";
        public const string AddMethodName = "Add";
        public const string ParseMethodName = "Parse";
        public const string GetCurrentMethodName = "get_Current";
        // NOTE: These values are used in schema below. If you modify any value, please make the same change in the schema.
        public const string SerializationNamespace = "http://schemas.microsoft.com/2003/10/Serialization/";
        public const string ClrTypeLocalName = "Type";
        public const string ClrAssemblyLocalName = "Assembly";
        public const string IsValueTypeLocalName = "IsValueType";
        public const string EnumerationValueLocalName = "EnumerationValue";
        public const string SurrogateDataLocalName = "Surrogate";
        public const string GenericTypeLocalName = "GenericType";
        public const string GenericParameterLocalName = "GenericParameter";
        public const string GenericNameAttribute = "Name";
        public const string GenericNamespaceAttribute = "Namespace";
        public const string GenericParameterNestedLevelAttribute = "NestedLevel";
        public const string IsDictionaryLocalName = "IsDictionary";
        public const string ActualTypeLocalName = "ActualType";
        public const string ActualTypeNameAttribute = "Name";
        public const string ActualTypeNamespaceAttribute = "Namespace";
        public const string DefaultValueLocalName = "DefaultValue";
        public const string EmitDefaultValueAttribute = "EmitDefaultValue";
        public const string ISerializableFactoryTypeLocalName = "FactoryType";
        public const string IdLocalName = "Id";
        public const string RefLocalName = "Ref";
        public const string ArraySizeLocalName = "Size";
        public const string KeyLocalName = "Key";
        public const string ValueLocalName = "Value";
        public const string MscorlibAssemblyName = "0";
        public const string MscorlibAssemblySimpleName = "mscorlib";
        public const string MscorlibFileName = MscorlibAssemblySimpleName + ".dll";
        public const string SerializationSchema = @"<?xml version='1.0' encoding='utf-8'?>
<xs:schema elementFormDefault='qualified' attributeFormDefault='qualified' xmlns:tns='http://schemas.microsoft.com/2003/10/Serialization/' targetNamespace='http://schemas.microsoft.com/2003/10/Serialization/' xmlns:xs='http://www.w3.org/2001/XMLSchema'>
  <xs:element name='anyType' nillable='true' type='xs:anyType' />
  <xs:element name='anyURI' nillable='true' type='xs:anyURI' />
  <xs:element name='base64Binary' nillable='true' type='xs:base64Binary' />
  <xs:element name='boolean' nillable='true' type='xs:boolean' />
  <xs:element name='byte' nillable='true' type='xs:byte' />
  <xs:element name='dateTime' nillable='true' type='xs:dateTime' />
  <xs:element name='decimal' nillable='true' type='xs:decimal' />
  <xs:element name='double' nillable='true' type='xs:double' />
  <xs:element name='float' nillable='true' type='xs:float' />
  <xs:element name='int' nillable='true' type='xs:int' />
  <xs:element name='long' nillable='true' type='xs:long' />
  <xs:element name='QName' nillable='true' type='xs:QName' />
  <xs:element name='short' nillable='true' type='xs:short' />
  <xs:element name='string' nillable='true' type='xs:string' />
  <xs:element name='unsignedByte' nillable='true' type='xs:unsignedByte' />
  <xs:element name='unsignedInt' nillable='true' type='xs:unsignedInt' />
  <xs:element name='unsignedLong' nillable='true' type='xs:unsignedLong' />
  <xs:element name='unsignedShort' nillable='true' type='xs:unsignedShort' />
  <xs:element name='char' nillable='true' type='tns:char' />
  <xs:simpleType name='char'>
    <xs:restriction base='xs:int'/>
  </xs:simpleType>  
  <xs:element name='duration' nillable='true' type='tns:duration' />
  <xs:simpleType name='duration'>
    <xs:restriction base='xs:duration'>
      <xs:pattern value='\-?P(\d*D)?(T(\d*H)?(\d*M)?(\d*(\.\d*)?S)?)?' />
      <xs:minInclusive value='-P10675199DT2H48M5.4775808S' />
      <xs:maxInclusive value='P10675199DT2H48M5.4775807S' />
    </xs:restriction>
  </xs:simpleType>
  <xs:element name='guid' nillable='true' type='tns:guid' />
  <xs:simpleType name='guid'>
    <xs:restriction base='xs:string'>
      <xs:pattern value='[\da-fA-F]{8}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{4}-[\da-fA-F]{12}' />
    </xs:restriction>
  </xs:simpleType>
  <xs:attribute name='FactoryType' type='xs:QName' />
  <xs:attribute name='Id' type='xs:ID' />
  <xs:attribute name='Ref' type='xs:IDREF' />
</xs:schema>
";
    }
}
