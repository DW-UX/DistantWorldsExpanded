using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Runtime.Serialization
{
    using DataContractDictionary = Dictionary<XmlQualifiedName, DataContract>;

    internal delegate IXmlSerializable CreateXmlSerializableDelegate();

    internal sealed class XmlDataContract : DataContract
    {
        private readonly XmlDataContractCriticalHelper _helper;

        internal XmlDataContract()
            : base(new XmlDataContractCriticalHelper())
        {
            _helper = base.Helper as XmlDataContractCriticalHelper;
        }

        internal XmlDataContract(Type type)
            : base(new XmlDataContractCriticalHelper(type))
        {
            _helper = base.Helper as XmlDataContractCriticalHelper;
        }

        internal override DataContractDictionary KnownDataContracts
        {
            get => _helper.KnownDataContracts;
            set => _helper.KnownDataContracts = value;
        }

        internal XmlSchemaType XsdType
        {
            get => _helper.XsdType;
            set => _helper.XsdType = value;
        }

        internal bool IsAnonymous => _helper.IsAnonymous;

        internal override bool HasRoot
        {
            get => _helper.HasRoot;
            set => _helper.HasRoot = value;
        }

        internal override XmlDictionaryString TopLevelElementName
        {
            get => _helper.TopLevelElementName;
            set => _helper.TopLevelElementName = value;
        }

        internal override XmlDictionaryString TopLevelElementNamespace
        {
            get => _helper.TopLevelElementNamespace;
            set => _helper.TopLevelElementNamespace = value;
        }

        internal bool IsTopLevelElementNullable
        {
            get => _helper.IsTopLevelElementNullable;
            set => _helper.IsTopLevelElementNullable = value;
        }

        internal bool IsTypeDefinedOnImport
        {
            get => _helper.IsTypeDefinedOnImport;
            set => _helper.IsTypeDefinedOnImport = value;
        }

        internal CreateXmlSerializableDelegate CreateXmlSerializableDelegate
        {
            get
            {
                if (_helper.CreateXmlSerializableDelegate == null)
                {
                    lock (this)
                    {
                        if (_helper.CreateXmlSerializableDelegate == null)
                        {
                            CreateXmlSerializableDelegate tempCreateXmlSerializable = GenerateCreateXmlSerializableDelegate();
                            Thread.MemoryBarrier();
                            _helper.CreateXmlSerializableDelegate = tempCreateXmlSerializable;
                        }
                    }
                }
                return _helper.CreateXmlSerializableDelegate;
            }
        }

        internal override bool CanContainReferences => false;

        internal override bool IsBuiltInDataContract => UnderlyingType == Globals.TypeOfXmlElement || UnderlyingType == Globals.TypeOfXmlNodeArray;

        private class XmlDataContractCriticalHelper : DataContract.DataContractCriticalHelper
        {
            private DataContractDictionary knownDataContracts;
            private bool isKnownTypeAttributeChecked;
            private XmlDictionaryString topLevelElementName;
            private XmlDictionaryString topLevelElementNamespace;
            private bool isTopLevelElementNullable;
            private bool isTypeDefinedOnImport;
            private XmlSchemaType xsdType;
            private bool hasRoot;
            private CreateXmlSerializableDelegate createXmlSerializable;

            internal XmlDataContractCriticalHelper()
            {
            }

            internal XmlDataContractCriticalHelper(Type type)
                : base(type)
            {
                if (type.IsDefined(Globals.TypeOfDataContractAttribute, false))
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new System.Runtime.Serialization.InvalidDataContractException(SR.Format(SR.IXmlSerializableCannotHaveDataContract, DataContract.GetClrTypeFullName(type))));
                }

                if (type.IsDefined(Globals.TypeOfCollectionDataContractAttribute, false))
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new System.Runtime.Serialization.InvalidDataContractException(SR.Format(SR.IXmlSerializableCannotHaveCollectionDataContract, DataContract.GetClrTypeFullName(type))));
                }

                SchemaExporter.GetXmlTypeInfo(type, out XmlQualifiedName stableName, out XmlSchemaType xsdType, out bool hasRoot);

                StableName = stableName;
                XsdType = xsdType;
                HasRoot = hasRoot;
                XmlDictionary dictionary = new XmlDictionary();
                Name = dictionary.Add(StableName.Name);
                Namespace = dictionary.Add(StableName.Namespace);
                object[] xmlRootAttributes = (UnderlyingType == null) ? null : UnderlyingType.GetCustomAttributes(Globals.TypeOfXmlRootAttribute, false);
                if (xmlRootAttributes == null || xmlRootAttributes.Length == 0)
                {
                    if (hasRoot)
                    {
                        topLevelElementName = Name;
                        topLevelElementNamespace = (StableName.Namespace == Globals.SchemaNamespace) ? DictionaryGlobals.EmptyString : Namespace;
                        isTopLevelElementNullable = true;
                    }
                }
                else
                {
                    if (hasRoot)
                    {
                        XmlRootAttribute xmlRootAttribute = (XmlRootAttribute)xmlRootAttributes[0];
                        isTopLevelElementNullable = xmlRootAttribute.IsNullable;
                        string elementName = xmlRootAttribute.ElementName;
                        topLevelElementName = (elementName == null || elementName.Length == 0) ? Name : dictionary.Add(DataContract.EncodeLocalName(elementName));
                        string elementNs = xmlRootAttribute.Namespace;
                        topLevelElementNamespace = (elementNs == null || elementNs.Length == 0) ? DictionaryGlobals.EmptyString : dictionary.Add(elementNs);
                    }
                    else
                    {
                        throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new System.Runtime.Serialization.InvalidDataContractException(SR.Format(SR.IsAnyCannotHaveXmlRoot, DataContract.GetClrTypeFullName(UnderlyingType))));
                    }
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

            internal XmlSchemaType XsdType
            {
                get => xsdType;
                set => xsdType = value;
            }

            internal bool IsAnonymous => xsdType != null;

            internal override bool HasRoot
            {
                get => hasRoot;
                set => hasRoot = value;
            }

            internal override XmlDictionaryString TopLevelElementName
            {
                get => topLevelElementName;
                set => topLevelElementName = value;
            }

            internal override XmlDictionaryString TopLevelElementNamespace
            {
                get => topLevelElementNamespace;
                set => topLevelElementNamespace = value;
            }

            internal bool IsTopLevelElementNullable
            {
                get => isTopLevelElementNullable;
                set => isTopLevelElementNullable = value;
            }

            internal bool IsTypeDefinedOnImport
            {
                get => isTypeDefinedOnImport;
                set => isTypeDefinedOnImport = value;
            }

            internal CreateXmlSerializableDelegate CreateXmlSerializableDelegate
            {
                get => createXmlSerializable;
                set => createXmlSerializable = value;
            }

        }

        private ConstructorInfo GetConstructor()
        {
            Type type = UnderlyingType;

            if (type.IsValueType)
            {
                return null;
            }

            ConstructorInfo ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, Globals.EmptyTypeArray, null);
            if (ctor == null)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new System.Runtime.Serialization.InvalidDataContractException(SR.Format(SR.IXmlSerializableMustHaveDefaultConstructor, DataContract.GetClrTypeFullName(type))));
            }

            return ctor;
        }

        internal void SetTopLevelElementName(XmlQualifiedName elementName)
        {
            if (elementName != null)
            {
                XmlDictionary dictionary = new XmlDictionary();
                TopLevelElementName = dictionary.Add(elementName.Name);
                TopLevelElementNamespace = dictionary.Add(elementName.Namespace);
            }
        }

        internal CreateXmlSerializableDelegate GenerateCreateXmlSerializableDelegate()
        {
            Type type = UnderlyingType;
            CodeGenerator ilg = new CodeGenerator();
            bool memberAccessFlag = RequiresMemberAccessForCreate(null);
            ilg.BeginMethod("Create" + DataContract.GetClrTypeFullName(type), typeof(CreateXmlSerializableDelegate), memberAccessFlag);
            if (type.IsValueType)
            {
                System.Reflection.Emit.LocalBuilder local = ilg.DeclareLocal(type, type.Name + "Value");
                ilg.Ldloca(local);
                ilg.InitObj(type);
                ilg.Ldloc(local);
            }
            else
            {
                ilg.New(GetConstructor());
            }
            ilg.ConvertValue(UnderlyingType, Globals.TypeOfIXmlSerializable);
            ilg.Ret();
            return (CreateXmlSerializableDelegate)ilg.EndMethod();
        }

        private bool RequiresMemberAccessForCreate(SecurityException securityException)
        {
            if (!IsTypeVisible(UnderlyingType))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(SR.PartialTrustIXmlSerializableTypeNotPublic, DataContract.GetClrTypeFullName(UnderlyingType)),
                        securityException));
                }
                return true;
            }

            if (ConstructorRequiresMemberAccess(GetConstructor()))
            {
                if (securityException != null)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(
                        new SecurityException(SR.Format(SR.PartialTrustIXmlSerialzableNoPublicConstructor, DataContract.GetClrTypeFullName(UnderlyingType)),
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

            XmlDataContract dataContract = other as XmlDataContract;
            if (dataContract != null)
            {
                if (HasRoot != dataContract.HasRoot)
                {
                    return false;
                }

                if (IsAnonymous)
                {
                    return dataContract.IsAnonymous;
                }
                else
                {
                    return (StableName.Name == dataContract.StableName.Name && StableName.Namespace == dataContract.StableName.Namespace);
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
            if (context == null)
            {
                XmlObjectSerializerWriteContext.WriteRootIXmlSerializable(xmlWriter, obj);
            }
            else
            {
                context.WriteIXmlSerializable(xmlWriter, obj);
            }
        }

        public override object ReadXmlValue(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context)
        {
            object o;
            if (context == null)
            {
                o = XmlObjectSerializerReadContext.ReadRootIXmlSerializable(xmlReader, this, true /*isMemberType*/);
            }
            else
            {
                o = context.ReadIXmlSerializable(xmlReader, this, true /*isMemberType*/);
                context.AddNewObject(o);
            }
            xmlReader.ReadEndElement();
            return o;
        }

    }
}
