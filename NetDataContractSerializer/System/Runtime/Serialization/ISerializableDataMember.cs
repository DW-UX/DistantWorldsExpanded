namespace System.Runtime.Serialization
{
    internal class ISerializableDataMember
    {
        internal string Name { get; set; }

        internal IDataNode Value { get; set; }
    }
}
