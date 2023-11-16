using System;
using System.Reflection;
using System.Collections.ObjectModel;

namespace System.Runtime.Serialization
{
    public interface IDataContractSurrogate
    {
        Type GetDataContractType(Type type);
        object GetObjectToSerialize(object obj, Type targetType);
        object GetDeserializedObject(object obj, Type targetType);
        object GetCustomDataToExport(MemberInfo memberInfo, Type dataContractType);
        object GetCustomDataToExport(Type clrType, Type dataContractType);
        void GetKnownCustomDataTypes(Collection<Type> customDataTypes);
        Type GetReferencedTypeOnImport(string typeName, string typeNamespace, object customData);
    }
}
