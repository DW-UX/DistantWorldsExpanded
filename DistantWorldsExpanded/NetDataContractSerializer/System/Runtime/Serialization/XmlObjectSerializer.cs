using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml;

namespace System.Runtime.Serialization
{
    using DataContractDictionary = Dictionary<XmlQualifiedName, DataContract>;

    public abstract class XmlObjectSerializer2 : XmlObjectSerializer
    {
        internal void WriteObjectHandleExceptions(XmlWriterDelegator writer, object graph)
        {
            WriteObjectHandleExceptions(writer, graph, null);
        }

        internal void WriteObjectHandleExceptions(XmlWriterDelegator writer, object graph, DataContractResolver dataContractResolver)
        {
            try
            {
                writer = writer ?? throw new ArgumentNullException(nameof(writer));
                InternalWriteObject(writer, graph, dataContractResolver);
            }
            catch (XmlException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorSerializing, GetSerializeType(graph), ex), ex));
            }
            catch (FormatException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorSerializing, GetSerializeType(graph), ex), ex));
            }
        }

        internal virtual DataContractDictionary KnownDataContracts => null;

        internal virtual void InternalWriteObject(XmlWriterDelegator writer, object graph)
        {
            WriteStartObject(writer.Writer, graph);
            WriteObjectContent(writer.Writer, graph);
            WriteEndObject(writer.Writer);
        }

        internal virtual void InternalWriteObject(XmlWriterDelegator writer, object graph, DataContractResolver dataContractResolver)
        {
            InternalWriteObject(writer, graph);
        }

        internal virtual void InternalWriteStartObject(XmlWriterDelegator writer, object graph)
        {
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
        }
        internal virtual void InternalWriteObjectContent(XmlWriterDelegator writer, object graph)
        {
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
        }
        internal virtual void InternalWriteEndObject(XmlWriterDelegator writer)
        {
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
        }

        internal void WriteStartObjectHandleExceptions(XmlWriterDelegator writer, object graph)
        {
            try
            {
                writer = writer ?? throw new ArgumentNullException(nameof(writer));
                InternalWriteStartObject(writer, graph);
            }
            catch (XmlException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorWriteStartObject, GetSerializeType(graph), ex), ex));
            }
            catch (FormatException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorWriteStartObject, GetSerializeType(graph), ex), ex));
            }
        }

        internal void WriteObjectContentHandleExceptions(XmlWriterDelegator writer, object graph)
        {
            try
            {
                writer = writer ?? throw new ArgumentNullException(nameof(writer));
                if (writer.WriteState != WriteState.Element)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(SR.Format(SR.XmlWriterMustBeInElement, writer.WriteState)));
                }
                InternalWriteObjectContent(writer, graph);
            }
            catch (XmlException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorSerializing, GetSerializeType(graph), ex), ex));
            }
            catch (FormatException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorSerializing, GetSerializeType(graph), ex), ex));
            }
        }

        internal void WriteEndObjectHandleExceptions(XmlWriterDelegator writer)
        {
            try
            {
                writer = writer ?? throw new ArgumentNullException(nameof(writer));
                InternalWriteEndObject(writer);
            }
            catch (XmlException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorWriteEndObject, null, ex), ex));
            }
            catch (FormatException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorWriteEndObject, null, ex), ex));
            }
        }

        internal void WriteRootElement(XmlWriterDelegator writer, DataContract contract, XmlDictionaryString name, XmlDictionaryString ns, bool needsContractNsAtRoot)
        {
            if (name == null) // root name not set explicitly
            {
                if (!contract.HasRoot)
                {
                    return;
                }

                contract.WriteRootElement(writer, contract.TopLevelElementName, contract.TopLevelElementNamespace);
            }
            else
            {
                contract.WriteRootElement(writer, name, ns);
                if (needsContractNsAtRoot)
                {
                    writer.WriteNamespaceDecl(contract.Namespace);
                }
            }
        }

        internal bool CheckIfNeedsContractNsAtRoot(XmlDictionaryString name, XmlDictionaryString ns, DataContract contract)
        {
            if (name == null)
            {
                return false;
            }

            if (contract.IsBuiltInDataContract || !contract.CanContainReferences || contract.IsISerializable)
            {
                return false;
            }

            string contractNs = contract.Namespace?.Value;
            if (string.IsNullOrEmpty(contractNs) || contractNs == ns?.Value)
            {
                return false;
            }

            return true;
        }

        internal static void WriteNull(XmlWriterDelegator writer)
        {
            writer.WriteAttributeBool(Globals.XsiPrefix, DictionaryGlobals.XsiNilLocalName, DictionaryGlobals.SchemaInstanceNamespace, true);
        }

        internal static bool IsContractDeclared(DataContract contract, DataContract declaredContract)
        {
            return (object.ReferenceEquals(contract.Name, declaredContract.Name) && object.ReferenceEquals(contract.Namespace, declaredContract.Namespace))
                || (contract.Name.Value == declaredContract.Name.Value && contract.Namespace.Value == declaredContract.Namespace.Value);
        }
        


        internal virtual object InternalReadObject(XmlReaderDelegator reader, bool verifyObjectName)
        {
            return ReadObject(reader.UnderlyingReader, verifyObjectName);
        }

        internal virtual object InternalReadObject(XmlReaderDelegator reader, bool verifyObjectName, DataContractResolver dataContractResolver)
        {
            return InternalReadObject(reader, verifyObjectName);
        }

        internal virtual bool InternalIsStartObject(XmlReaderDelegator reader)
        {
            Fx.Assert("XmlObjectSerializer.InternalIsStartObject should never get called");
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
        }

        internal object ReadObjectHandleExceptions(XmlReaderDelegator reader, bool verifyObjectName)
        {
            return ReadObjectHandleExceptions(reader, verifyObjectName, null);
        }

        internal object ReadObjectHandleExceptions(XmlReaderDelegator reader, bool verifyObjectName, DataContractResolver dataContractResolver)
        {
            try
            {
                reader = reader ?? throw new ArgumentNullException(nameof(reader));
                return InternalReadObject(reader, verifyObjectName, dataContractResolver);
            }
            catch (XmlException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorDeserializing, GetDeserializeType(), ex), ex));
            }
            catch (FormatException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorDeserializing, GetDeserializeType(), ex), ex));
            }
        }

        internal bool IsStartObjectHandleExceptions(XmlReaderDelegator reader)
        {
            try
            {
                reader = reader ?? throw new ArgumentNullException(nameof(reader));
                return InternalIsStartObject(reader);
            }
            catch (XmlException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorIsStartObject, GetDeserializeType(), ex), ex));
            }
            catch (FormatException ex)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(CreateSerializationException(GetTypeInfoError(SR.ErrorIsStartObject, GetDeserializeType(), ex), ex));
            }
        }

        internal bool IsRootXmlAny(XmlDictionaryString rootName, DataContract contract)
        {
            return (rootName == null) && !contract.HasRoot;
        }

        internal bool IsStartElement(XmlReaderDelegator reader)
        {
            return (reader.MoveToElement() || reader.IsStartElement());
        }

        internal bool IsRootElement(XmlReaderDelegator reader, DataContract contract, XmlDictionaryString name, XmlDictionaryString ns)
        {
            reader.MoveToElement();
            if (name != null) // root name set explicitly
            {
                return reader.IsStartElement(name, ns);
            }
            else
            {
                if (!contract.HasRoot)
                {
                    return reader.IsStartElement();
                }

                if (reader.IsStartElement(contract.TopLevelElementName, contract.TopLevelElementNamespace))
                {
                    return true;
                }

                ClassDataContract classContract = contract as ClassDataContract;
                if (classContract != null)
                {
                    classContract = classContract.BaseContract;
                }

                while (classContract != null)
                {
                    if (reader.IsStartElement(classContract.TopLevelElementName, classContract.TopLevelElementNamespace))
                    {
                        return true;
                    }

                    classContract = classContract.BaseContract;
                }
                if (classContract == null)
                {
                    DataContract objectContract = PrimitiveDataContract.GetPrimitiveDataContract(Globals.TypeOfObject);
                    if (reader.IsStartElement(objectContract.TopLevelElementName, objectContract.TopLevelElementNamespace))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        internal static string TryAddLineInfo(XmlReaderDelegator reader, string errorMessage)
        {
            if (reader.HasLineInfo())
            {
                return string.Format(CultureInfo.InvariantCulture, "{0} {1}", SR.Format(SR.ErrorInLine, reader.LineNumber, reader.LinePosition), errorMessage);
            }

            return errorMessage;
        }

        internal static Exception CreateSerializationExceptionWithReaderDetails(string errorMessage, XmlReaderDelegator reader)
        {
            return CreateSerializationException(TryAddLineInfo(reader, SR.Format(SR.EncounteredWithNameNamespace, errorMessage, reader.NodeType, reader.LocalName, reader.NamespaceURI)));
        }

        internal static SerializationException CreateSerializationException(string errorMessage)
        {
            return CreateSerializationException(errorMessage, null);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        internal static SerializationException CreateSerializationException(string errorMessage, Exception innerException)
        {
            return new SerializationException(errorMessage, innerException);
        }

        private static string GetTypeInfoError(string errorMessage, Type type, Exception innerException)
        {
            string typeInfo = (type == null) ? string.Empty : SR.Format(SR.ErrorTypeInfo, DataContract.GetClrTypeFullName(type));
            string innerExceptionMessage = innerException?.Message ?? string.Empty;
            return SR.Format(errorMessage, typeInfo, innerExceptionMessage);
        }

        internal virtual Type GetSerializeType(object graph)
        {
            return graph?.GetType();
        }

        internal virtual Type GetDeserializeType()
        {
            return null;
        }

        private static IFormatterConverter formatterConverter;
        internal static IFormatterConverter FormatterConverter
        {
            get
            {
                if (formatterConverter == null)
                {
                    formatterConverter = new FormatterConverter();
                }

                return formatterConverter;
            }
        }
    }
}
