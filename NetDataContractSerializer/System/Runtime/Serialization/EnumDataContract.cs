using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security;
using System.Threading;
using System.Xml;
using DataContractAttribute = System.Runtime.Serialization.DataContractAttribute;
using EnumMemberAttribute = System.Runtime.Serialization.EnumMemberAttribute;

namespace System.Runtime.Serialization
{
    internal sealed class EnumDataContract : DataContract
    {
        private readonly EnumDataContractCriticalHelper helper;

        internal EnumDataContract()
            : base(new EnumDataContractCriticalHelper())
        {
            helper = base.Helper as EnumDataContractCriticalHelper;
        }

        internal EnumDataContract(Type type)
            : base(new EnumDataContractCriticalHelper(type))
        {
            helper = base.Helper as EnumDataContractCriticalHelper;
        }

        internal static XmlQualifiedName GetBaseContractName(Type type)
        {
            return EnumDataContractCriticalHelper.GetBaseContractName(type);
        }

        internal static Type GetBaseType(XmlQualifiedName baseContractName)
        {
            return EnumDataContractCriticalHelper.GetBaseType(baseContractName);
        }

        internal XmlQualifiedName BaseContractName
        {
            get => helper.BaseContractName;
            set => helper.BaseContractName = value;
        }

        internal List<DataMember> Members
        {
            get => helper.Members;
            set => helper.Members = value;
        }

        internal List<long> Values
        {
            get => helper.Values;
            set => helper.Values = value;
        }

        internal bool IsFlags
        {
            get => helper.IsFlags;
            set => helper.IsFlags = value;
        }

        internal bool IsULong
        {
            get => helper.IsULong;
        }

        private XmlDictionaryString[] ChildElementNames
        {
            get => helper.ChildElementNames;
        }

        internal override bool CanContainReferences => false;

        private class EnumDataContractCriticalHelper : DataContract.DataContractCriticalHelper
        {
            private static readonly Dictionary<Type, XmlQualifiedName> typeToName;
            private static readonly Dictionary<XmlQualifiedName, Type> nameToType;
            private XmlQualifiedName baseContractName;
            private List<DataMember> members;
            private List<long> values;
            private bool isULong;
            private bool isFlags;
            private readonly bool hasDataContract;
            private readonly XmlDictionaryString[] childElementNames;

            static EnumDataContractCriticalHelper()
            {
                typeToName = new Dictionary<Type, XmlQualifiedName>();
                nameToType = new Dictionary<XmlQualifiedName, Type>();
                Add(typeof(sbyte), "byte");
                Add(typeof(byte), "unsignedByte");
                Add(typeof(short), "short");
                Add(typeof(ushort), "unsignedShort");
                Add(typeof(int), "int");
                Add(typeof(uint), "unsignedInt");
                Add(typeof(long), "long");
                Add(typeof(ulong), "unsignedLong");
            }

            internal static void Add(Type type, string localName)
            {
                XmlQualifiedName stableName = CreateQualifiedName(localName, Globals.SchemaNamespace);
                typeToName.Add(type, stableName);
                nameToType.Add(stableName, type);
            }

            internal static XmlQualifiedName GetBaseContractName(Type type)
            {
                typeToName.TryGetValue(type, out XmlQualifiedName retVal);
                return retVal;
            }

            internal static Type GetBaseType(XmlQualifiedName baseContractName)
            {
                nameToType.TryGetValue(baseContractName, out Type retVal);
                return retVal;
            }

            internal EnumDataContractCriticalHelper()
            {
                IsValueType = true;
            }

            internal EnumDataContractCriticalHelper(Type type)
                : base(type)
            {
                StableName = DataContract.GetStableName(type, out hasDataContract);
                Type baseType = Enum.GetUnderlyingType(type);
                baseContractName = GetBaseContractName(baseType);
                ImportBaseType(baseType);
                IsFlags = type.IsDefined(Globals.TypeOfFlagsAttribute, false);
                ImportDataMembers();

                XmlDictionary dictionary = new XmlDictionary(2 + Members.Count);
                Name = dictionary.Add(StableName.Name);
                Namespace = dictionary.Add(StableName.Namespace);
                childElementNames = new XmlDictionaryString[Members.Count];
                for (int i = 0; i < Members.Count; i++)
                {
                    childElementNames[i] = dictionary.Add(Members[i].Name);
                }

                if (TryGetDCAttribute(type, out DataContractAttribute dataContractAttribute))
                {
                    if (dataContractAttribute.IsReference)
                    {
                        DataContract.ThrowInvalidDataContractException(
                                SR.Format(SR.EnumTypeCannotHaveIsReference,
                                    DataContract.GetClrTypeFullName(type),
                                    dataContractAttribute.IsReference,
                                    false),
                                type);
                    }
                }
            }

            internal XmlQualifiedName BaseContractName
            {
                get => baseContractName;
                set
                {
                    baseContractName = value;
                    Type baseType = GetBaseType(baseContractName);
                    if (baseType == null)
                    {
                        ThrowInvalidDataContractException(SR.Format(SR.InvalidEnumBaseType, value.Name, value.Namespace, StableName.Name, StableName.Namespace));
                    }

                    ImportBaseType(baseType);
                }
            }

            internal List<DataMember> Members
            {
                get => members;
                set => members = value;
            }

            internal List<long> Values
            {
                get => values;
                set => values = value;
            }

            internal bool IsFlags
            {
                get => isFlags;
                set => isFlags = value;
            }

            internal bool IsULong => isULong;

            internal XmlDictionaryString[] ChildElementNames => childElementNames;

            private void ImportBaseType(Type baseType)
            {
                isULong = (baseType == Globals.TypeOfULong);
            }

            private void ImportDataMembers()
            {
                Type type = UnderlyingType;
                FieldInfo[] fields = type.GetFields(BindingFlags.Static | BindingFlags.Public);
                Dictionary<string, DataMember> memberValuesTable = new Dictionary<string, DataMember>();
                List<DataMember> tempMembers = new List<DataMember>(fields.Length);
                List<long> tempValues = new List<long>(fields.Length);

                for (int i = 0; i < fields.Length; i++)
                {
                    FieldInfo field = fields[i];
                    bool enumMemberValid = false;
                    if (hasDataContract)
                    {
                        object[] memberAttributes = field.GetCustomAttributes(Globals.TypeOfEnumMemberAttribute, false);
                        if (memberAttributes != null && memberAttributes.Length > 0)
                        {
                            if (memberAttributes.Length > 1)
                            {
                                ThrowInvalidDataContractException(SR.Format(SR.TooManyEnumMembers, DataContract.GetClrTypeFullName(field.DeclaringType), field.Name));
                            }

                            EnumMemberAttribute memberAttribute = (EnumMemberAttribute)memberAttributes[0];

                            DataMember memberContract = new DataMember(field);
                            if (memberAttribute.IsValueSetExplicitly)
                            {
                                if (memberAttribute.Value == null || memberAttribute.Value.Length == 0)
                                {
                                    ThrowInvalidDataContractException(SR.Format(SR.InvalidEnumMemberValue, field.Name, DataContract.GetClrTypeFullName(type)));
                                }

                                memberContract.Name = memberAttribute.Value;
                            }
                            else
                            {
                                memberContract.Name = field.Name;
                            }

                            ClassDataContract.CheckAndAddMember(tempMembers, memberContract, memberValuesTable);
                            enumMemberValid = true;
                        }

                        object[] dataMemberAttributes = field.GetCustomAttributes(Globals.TypeOfDataMemberAttribute, false);
                        if (dataMemberAttributes != null && dataMemberAttributes.Length > 0)
                        {
                            ThrowInvalidDataContractException(SR.Format(SR.DataMemberOnEnumField, DataContract.GetClrTypeFullName(field.DeclaringType), field.Name));
                        }
                    }
                    else
                    {
                        if (!field.IsNotSerialized)
                        {
                            DataMember memberContract = new DataMember(field)
                            {
                                Name = field.Name
                            };
                            ClassDataContract.CheckAndAddMember(tempMembers, memberContract, memberValuesTable);
                            enumMemberValid = true;
                        }
                    }

                    if (enumMemberValid)
                    {
                        object enumValue = field.GetValue(null);
                        if (isULong)
                        {
                            tempValues.Add((long)((IConvertible)enumValue).ToUInt64(null));
                        }
                        else
                        {
                            tempValues.Add(((IConvertible)enumValue).ToInt64(null));
                        }
                    }
                }

                Thread.MemoryBarrier();
                members = tempMembers;
                values = tempValues;
            }
        }

        internal void WriteEnumValue(XmlWriterDelegator writer, object value)
        {
            long longValue = IsULong ? (long)((IConvertible)value).ToUInt64(null) : ((IConvertible)value).ToInt64(null);
            for (int i = 0; i < Values.Count; i++)
            {
                if (longValue == Values[i])
                {
                    writer.WriteString(ChildElementNames[i].Value);
                    return;
                }
            }
            if (IsFlags)
            {
                int zeroIndex = -1;
                bool noneWritten = true;
                for (int i = 0; i < Values.Count; i++)
                {
                    long current = Values[i];
                    if (current == 0)
                    {
                        zeroIndex = i;
                        continue;
                    }
                    if (longValue == 0)
                    {
                        break;
                    }

                    if ((current & longValue) == current)
                    {
                        if (noneWritten)
                        {
                            noneWritten = false;
                        }
                        else
                        {
                            writer.WriteString(DictionaryGlobals.Space.Value);
                        }

                        writer.WriteString(ChildElementNames[i].Value);
                        longValue &= ~current;
                    }
                }
                // enforce that enum value was completely parsed
                if (longValue != 0)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.InvalidEnumValueOnWrite, value, DataContract.GetClrTypeFullName(UnderlyingType))));
                }

                if (noneWritten && zeroIndex >= 0)
                {
                    writer.WriteString(ChildElementNames[zeroIndex].Value);
                }
            }
            else
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.InvalidEnumValueOnWrite, value, DataContract.GetClrTypeFullName(UnderlyingType))));
            }
        }

        internal object ReadEnumValue(XmlReaderDelegator reader)
        {
            string stringValue = reader.ReadElementContentAsString();
            long longValue = 0;
            int i = 0;
            if (IsFlags)
            {
                // Skip initial spaces
                for (; i < stringValue.Length; i++)
                {
                    if (stringValue[i] != ' ')
                    {
                        break;
                    }
                }

                // Read space-delimited values
                int startIndex = i;
                int count = 0;
                for (; i < stringValue.Length; i++)
                {
                    if (stringValue[i] == ' ')
                    {
                        count = i - startIndex;
                        if (count > 0)
                        {
                            longValue |= ReadEnumValue(stringValue, startIndex, count);
                        }

                        for (++i; i < stringValue.Length; i++)
                        {
                            if (stringValue[i] != ' ')
                            {
                                break;
                            }
                        }

                        startIndex = i;
                        if (i == stringValue.Length)
                        {
                            break;
                        }
                    }
                }
                count = i - startIndex;
                if (count > 0)
                {
                    longValue |= ReadEnumValue(stringValue, startIndex, count);
                }
            }
            else
            {
                if (stringValue.Length == 0)
                {
                    throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.InvalidEnumValueOnRead, stringValue, DataContract.GetClrTypeFullName(UnderlyingType))));
                }

                longValue = ReadEnumValue(stringValue, 0, stringValue.Length);
            }

            if (IsULong)
            {
                return Enum.ToObject(UnderlyingType, (ulong)longValue);
            }

            return Enum.ToObject(UnderlyingType, longValue);
        }

        private long ReadEnumValue(string value, int index, int count)
        {
            for (int i = 0; i < Members.Count; i++)
            {
                string memberName = Members[i].Name;
                if (memberName.Length == count && string.CompareOrdinal(value, index, memberName, 0, count) == 0)
                {
                    return Values[i];
                }
            }
            throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlObjectSerializer2.CreateSerializationException(SR.Format(SR.InvalidEnumValueOnRead, value.Substring(index, count), DataContract.GetClrTypeFullName(UnderlyingType))));
        }

        internal string GetStringFromEnumValue(long value)
        {
            if (IsULong)
            {
                return XmlConvert.ToString((ulong)value);
            }
            else
            {
                return XmlConvert.ToString(value);
            }
        }

        internal long GetEnumValueFromString(string value)
        {
            if (IsULong)
            {
                return (long)XmlConverter.ToUInt64(value);
            }
            else
            {
                return XmlConverter.ToInt64(value);
            }
        }

        internal override bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
        {
            if (IsEqualOrChecked(other, checkedContracts))
            {
                return true;
            }

            if (base.Equals(other, null))
            {
                EnumDataContract dataContract = other as EnumDataContract;
                if (dataContract != null)
                {
                    if (Members.Count != dataContract.Members.Count || Values.Count != dataContract.Values.Count)
                    {
                        return false;
                    }

                    string[] memberNames1 = new string[Members.Count], memberNames2 = new string[Members.Count];
                    for (int i = 0; i < Members.Count; i++)
                    {
                        memberNames1[i] = Members[i].Name;
                        memberNames2[i] = dataContract.Members[i].Name;
                    }
                    Array.Sort(memberNames1);
                    Array.Sort(memberNames2);
                    for (int i = 0; i < Members.Count; i++)
                    {
                        if (memberNames1[i] != memberNames2[i])
                        {
                            return false;
                        }
                    }

                    return (IsFlags == dataContract.IsFlags);
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
            WriteEnumValue(xmlWriter, obj);
        }

        public override object ReadXmlValue(XmlReaderDelegator xmlReader, XmlObjectSerializerReadContext context)
        {
            object obj = ReadEnumValue(xmlReader);
            if (context != null)
            {
                context.AddNewObject(obj);
            }

            return obj;
        }

    }
}
