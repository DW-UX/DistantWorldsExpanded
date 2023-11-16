//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;
using InvalidDataContractException = System.Runtime.Serialization.InvalidDataContractException;

namespace System.Runtime.Serialization
{

    public class XsdDataContractExporter
    {
        private ExportOptions options;
        private XmlSchemaSet schemas;
        private DataContractSet dataContractSet;

        public XsdDataContractExporter()
        {
        }

        public XsdDataContractExporter(XmlSchemaSet schemas)
        {
            this.schemas = schemas;
        }

        public ExportOptions Options
        {
            get => options;
            set => options = value;
        }

        public XmlSchemaSet Schemas
        {
            get
            {
                XmlSchemaSet schemaSet = GetSchemaSet();
                SchemaImporter.CompileSchemaSet(schemaSet);
                return schemaSet;
            }
        }

        private XmlSchemaSet GetSchemaSet()
        {
            if (schemas == null)
            {
                schemas = new XmlSchemaSet
                {
                    XmlResolver = null
                };
            }
            return schemas;
        }

        private DataContractSet DataContractSet
        {
            get
            {
                if (dataContractSet == null)
                {
                    dataContractSet = new DataContractSet((Options == null) ? null : Options.GetSurrogate());
                }
                return dataContractSet;
            }
        }

        public void Export(ICollection<Assembly> assemblies)
        {
            if (assemblies == null)
            {
                throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("assemblies"));
            }

            DataContractSet oldValue = (dataContractSet == null) ? null : new DataContractSet(dataContractSet);
            try
            {
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly == null)
                    {
                        throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.Format(SR.CannotExportNullAssembly, "assemblies")));
                    }

                    Type[] types = assembly.GetTypes();
                    for (int j = 0; j < types.Length; j++)
                    {
                        CheckAndAddType(types[j]);
                    }
                }

                Export();
            }
            catch (Exception ex)
            {
                if (Fx.IsFatal(ex))
                {
                    throw;
                }
                dataContractSet = oldValue;
                throw;
            }
        }

        public void Export(ICollection<Type> types)
        {
            if (types == null)
            {
                throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException(nameof(types)));
            }

            DataContractSet oldValue = (dataContractSet == null) ? null : new DataContractSet(dataContractSet);
            try
            {
                foreach (Type type in types)
                {
                    if (type == null)
                    {
                        throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.Format(SR.CannotExportNullType, nameof(types))));
                    }

                    AddType(type);
                }

                Export();
            }
            catch (Exception ex)
            {
                if (Fx.IsFatal(ex))
                {
                    throw;
                }
                dataContractSet = oldValue;
                throw;
            }
        }

        public void Export(Type type)
        {
            if (type == null)
            {
                throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException(nameof(type)));
            }

            DataContractSet oldValue = (dataContractSet == null) ? null : new DataContractSet(dataContractSet);
            try
            {
                AddType(type);
                Export();
            }
            catch (Exception ex)
            {
                if (Fx.IsFatal(ex))
                {
                    throw;
                }
                dataContractSet = oldValue;
                throw;
            }
        }

        public XmlQualifiedName GetSchemaTypeName(Type type)
        {
            if (type == null)
            {
                throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException(nameof(type)));
            }

            type = GetSurrogatedType(type);
            DataContract dataContract = DataContract.GetDataContract(type);
            DataContractSet.EnsureTypeNotGeneric(dataContract.UnderlyingType);
            XmlDataContract xmlDataContract = dataContract as XmlDataContract;
            if (xmlDataContract != null && xmlDataContract.IsAnonymous)
            {
                return XmlQualifiedName.Empty;
            }

            return dataContract.StableName;
        }

        public XmlSchemaType GetSchemaType(Type type)
        {
            if (type == null)
            {
                throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("type"));
            }

            type = GetSurrogatedType(type);
            DataContract dataContract = DataContract.GetDataContract(type);
            DataContractSet.EnsureTypeNotGeneric(dataContract.UnderlyingType);
            XmlDataContract xmlDataContract = dataContract as XmlDataContract;
            if (xmlDataContract != null && xmlDataContract.IsAnonymous)
            {
                return xmlDataContract.XsdType;
            }

            return null;
        }

        public XmlQualifiedName GetRootElementName(Type type)
        {
            if (type == null)
            {
                throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("type"));
            }

            type = GetSurrogatedType(type);
            DataContract dataContract = DataContract.GetDataContract(type);
            DataContractSet.EnsureTypeNotGeneric(dataContract.UnderlyingType);
            if (dataContract.HasRoot)
            {
                return new XmlQualifiedName(dataContract.TopLevelElementName.Value, dataContract.TopLevelElementNamespace.Value);
            }
            else
            {
                return null;
            }
        }

        private Type GetSurrogatedType(Type type)
        {
            IDataContractSurrogate dataContractSurrogate;
            if (options != null && (dataContractSurrogate = Options.GetSurrogate()) != null)
            {
                type = DataContractSurrogateCaller.GetDataContractType(dataContractSurrogate, type);
            }

            return type;
        }

        private void CheckAndAddType(Type type)
        {
            type = GetSurrogatedType(type);
            if (!type.ContainsGenericParameters && DataContract.IsTypeSerializable(type))
            {
                AddType(type);
            }
        }

        private void AddType(Type type)
        {
            DataContractSet.Add(type);
        }

        private void Export()
        {
            AddKnownTypes();
            SchemaExporter schemaExporter = new SchemaExporter(GetSchemaSet(), DataContractSet);
            schemaExporter.Export();
        }

        private void AddKnownTypes()
        {
            if (Options != null)
            {
                Collection<Type> knownTypes = Options.KnownTypes;

                if (knownTypes != null)
                {
                    for (int i = 0; i < knownTypes.Count; i++)
                    {
                        Type type = knownTypes[i];
                        if (type == null)
                        {
                            throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.CannotExportNullKnownType));
                        }

                        AddType(type);
                    }
                }
            }
        }

        public bool CanExport(ICollection<Assembly> assemblies)
        {
            if (assemblies == null)
            {
                throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("assemblies"));
            }

            DataContractSet oldValue = (dataContractSet == null) ? null : new DataContractSet(dataContractSet);
            try
            {
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly == null)
                    {
                        throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.Format(SR.CannotExportNullAssembly, nameof(assemblies))));
                    }

                    Type[] types = assembly.GetTypes();
                    for (int j = 0; j < types.Length; j++)
                    {
                        CheckAndAddType(types[j]);
                    }
                }
                AddKnownTypes();
                return true;
            }
            catch (InvalidDataContractException)
            {
                dataContractSet = oldValue;
                return false;
            }
            catch (Exception ex)
            {
                if (Fx.IsFatal(ex))
                {
                    throw;
                }
                dataContractSet = oldValue;
                throw;
            }
        }

        public bool CanExport(ICollection<Type> types)
        {
            if (types == null)
            {
                throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException(nameof(types)));
            }

            DataContractSet oldValue = (dataContractSet == null) ? null : new DataContractSet(dataContractSet);
            try
            {
                foreach (Type type in types)
                {
                    if (type == null)
                    {
                        throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.Format(SR.CannotExportNullType, nameof(types))));
                    }

                    AddType(type);
                }
                AddKnownTypes();
                return true;
            }
            catch (InvalidDataContractException)
            {
                dataContractSet = oldValue;
                return false;
            }
            catch (Exception ex)
            {
                if (Fx.IsFatal(ex))
                {
                    throw;
                }
                dataContractSet = oldValue;
                throw;
            }
        }

        public bool CanExport(Type type)
        {
            if (type == null)
            {
                throw System.Runtime.Serialization.DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException(nameof(type)));
            }

            DataContractSet oldValue = (dataContractSet == null) ? null : new DataContractSet(dataContractSet);
            try
            {
                AddType(type);
                AddKnownTypes();
                return true;
            }
            catch (InvalidDataContractException)
            {
                dataContractSet = oldValue;
                return false;
            }
            catch (Exception ex)
            {
                if (Fx.IsFatal(ex))
                {
                    throw;
                }
                dataContractSet = oldValue;
                throw;
            }
        }
    }
}

