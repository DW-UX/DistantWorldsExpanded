namespace System.Runtime.Serialization
{
    internal class ExtensionDataMember
    {
        public string Name { get; set; }

        public string Namespace { get; set; }

        public IDataNode Value { get; set; }

        public int MemberIndex { get; set; }
    }
}
