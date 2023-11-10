using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Runtime.Serialization
{
    internal class DataMember
    {
        private readonly CriticalHelper helper;

        internal DataMember()
        {
            helper = new CriticalHelper();
        }

        internal DataMember(MemberInfo memberInfo)
        {
            helper = new CriticalHelper(memberInfo);
        }

        internal DataMember(string name)
        {
            helper = new CriticalHelper(name);
        }

        internal DataMember(DataContract memberTypeContract, string name, bool isNullable, bool isRequired, bool emitDefaultValue, int order)
        {
            helper = new CriticalHelper(memberTypeContract, name, isNullable, isRequired, emitDefaultValue, order);
        }

        internal MemberInfo MemberInfo => helper.MemberInfo;

        internal string Name
        {
            get => helper.Name;
            set => helper.Name = value;
        }

        internal int Order
        {
            get => helper.Order;
            set => helper.Order = value;
        }

        internal bool IsRequired
        {
            get => helper.IsRequired;
            set => helper.IsRequired = value;
        }

        internal bool EmitDefaultValue
        {
            get => helper.EmitDefaultValue;
            set => helper.EmitDefaultValue = value;
        }

        internal bool IsNullable
        {
            get => helper.IsNullable;
            set => helper.IsNullable = value;
        }

        internal bool IsGetOnlyCollection
        {
            get => helper.IsGetOnlyCollection;
            set => helper.IsGetOnlyCollection = value;
        }

        internal Type MemberType => helper.MemberType;

        internal DataContract MemberTypeContract
        {
            get => helper.MemberTypeContract;
            set => helper.MemberTypeContract = value;
        }

        internal bool HasConflictingNameAndType
        {
            get => helper.HasConflictingNameAndType;
            set => helper.HasConflictingNameAndType = value;
        }

        internal DataMember ConflictingMember
        {
            get => helper.ConflictingMember;
            set => helper.ConflictingMember = value;
        }

        private class CriticalHelper
        {
            private DataContract memberTypeContract;
            private string name;
            private int order;
            private bool isRequired;
            private bool emitDefaultValue;
            private bool isNullable;
            private bool isGetOnlyCollection = false;
            private readonly MemberInfo memberInfo;
            private bool hasConflictingNameAndType;
            private DataMember conflictingMember;

            internal CriticalHelper()
            {
                emitDefaultValue = Globals.DefaultEmitDefaultValue;
            }

            internal CriticalHelper(MemberInfo memberInfo)
            {
                emitDefaultValue = Globals.DefaultEmitDefaultValue;
                this.memberInfo = memberInfo;
            }

            internal CriticalHelper(string name)
            {
                Name = name;
            }

            internal CriticalHelper(DataContract memberTypeContract, string name, bool isNullable, bool isRequired, bool emitDefaultValue, int order)
            {
                MemberTypeContract = memberTypeContract;
                Name = name;
                IsNullable = isNullable;
                IsRequired = isRequired;
                EmitDefaultValue = emitDefaultValue;
                Order = order;
            }

            internal MemberInfo MemberInfo => memberInfo;

            internal string Name
            {
                get => name;
                set => name = value;
            }

            internal int Order
            {
                get => order;
                set => order = value;
            }

            internal bool IsRequired
            {
                get => isRequired;
                set => isRequired = value;
            }

            internal bool EmitDefaultValue
            {
                get => emitDefaultValue;
                set => emitDefaultValue = value;
            }

            internal bool IsNullable
            {
                get => isNullable;
                set => isNullable = value;
            }

            internal bool IsGetOnlyCollection
            {
                get => isGetOnlyCollection;
                set => isGetOnlyCollection = value;
            }

            internal Type MemberType
            {
                get
                {
                    FieldInfo field = MemberInfo as FieldInfo;
                    if (field != null)
                    {
                        return field.FieldType;
                    }

                    return ((PropertyInfo)MemberInfo).PropertyType;
                }
            }

            internal DataContract MemberTypeContract
            {
                get
                {
                    if (memberTypeContract == null)
                    {
                        if (MemberInfo != null)
                        {
                            if (IsGetOnlyCollection)
                            {
                                memberTypeContract = DataContract.GetGetOnlyCollectionDataContract(DataContract.GetId(MemberType.TypeHandle), MemberType.TypeHandle, MemberType, SerializationMode.SharedContract);
                            }
                            else
                            {
                                memberTypeContract = DataContract.GetDataContract(MemberType);
                            }
                        }
                    }
                    return memberTypeContract;
                }
                set => memberTypeContract = value;
            }

            internal bool HasConflictingNameAndType
            {
                get => hasConflictingNameAndType;
                set => hasConflictingNameAndType = value;
            }

            internal DataMember ConflictingMember
            {
                get => conflictingMember;
                set => conflictingMember = value;
            }
        }

        internal bool RequiresMemberAccessForGet()
        {
            MemberInfo memberInfo = MemberInfo;
            FieldInfo field = memberInfo as FieldInfo;
            if (field != null)
            {
                return DataContract.FieldRequiresMemberAccess(field);
            }
            else
            {
                PropertyInfo property = (PropertyInfo)memberInfo;
                MethodInfo getMethod = property.GetGetMethod(true /*nonPublic*/);
                if (getMethod != null)
                {
                    return DataContract.MethodRequiresMemberAccess(getMethod) || !DataContract.IsTypeVisible(property.PropertyType);
                }
            }
            return false;
        }

        internal bool RequiresMemberAccessForSet()
        {
            MemberInfo memberInfo = MemberInfo;
            FieldInfo field = memberInfo as FieldInfo;
            if (field != null)
            {
                return DataContract.FieldRequiresMemberAccess(field);
            }
            else
            {
                PropertyInfo property = (PropertyInfo)memberInfo;
                MethodInfo setMethod = property.GetSetMethod(true /*nonPublic*/);
                if (setMethod != null)
                {
                    return DataContract.MethodRequiresMemberAccess(setMethod) || !DataContract.IsTypeVisible(property.PropertyType);
                }
            }
            return false;
        }

        internal DataMember BindGenericParameters(DataContract[] paramContracts, Dictionary<DataContract, DataContract> boundContracts)
        {
            DataContract memberTypeContract = MemberTypeContract.BindGenericParameters(paramContracts, boundContracts);
            DataMember boundDataMember = new DataMember(memberTypeContract,
                Name,
                !memberTypeContract.IsValueType,
                IsRequired,
                EmitDefaultValue,
                Order);
            return boundDataMember;
        }

        internal bool Equals(object other, Dictionary<DataContractPairKey, object> checkedContracts)
        {
            if ((object)this == other)
            {
                return true;
            }

            DataMember dataMember = other as DataMember;
            if (dataMember != null)
            {
                // Note: comparison does not use Order hint since it influences element order but does not specify exact order
                bool thisIsNullable = (MemberTypeContract == null) ? false : !MemberTypeContract.IsValueType;
                bool dataMemberIsNullable = (dataMember.MemberTypeContract == null) ? false : !dataMember.MemberTypeContract.IsValueType;
                return (Name == dataMember.Name
                        && (IsNullable || thisIsNullable) == (dataMember.IsNullable || dataMemberIsNullable)
                        && IsRequired == dataMember.IsRequired
                        && EmitDefaultValue == dataMember.EmitDefaultValue
                        && MemberTypeContract.Equals(dataMember.MemberTypeContract, checkedContracts));
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
