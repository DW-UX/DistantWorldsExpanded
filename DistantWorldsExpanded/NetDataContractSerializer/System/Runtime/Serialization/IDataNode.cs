using System;

namespace System.Runtime.Serialization
{
    internal interface IDataNode
    {
        Type DataType { get; }
        object Value { get; set; }  // boxes for primitives
        string DataContractName { get; set; }
        string DataContractNamespace { get; set; }
        string ClrTypeName { get; set; }
        string ClrAssemblyName { get; set; }
        string Id { get; set; }
        bool PreservesReferences { get; }

        // NOTE: consider moving below APIs to DataNode<T> if IDataNode API is made public
        void GetData(ElementData element);
        bool IsFinalValue { get; set; }
        void Clear();
    }
}
