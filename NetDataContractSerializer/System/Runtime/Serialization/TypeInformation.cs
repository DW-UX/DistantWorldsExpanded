namespace System.Runtime.Serialization
{
    internal sealed class TypeInformation
    {
        private readonly string _fullTypeName;
        private readonly string _assemblyString;
        private readonly bool _hasTypeForwardedFrom;

        internal TypeInformation(string fullTypeName, string assemblyString, bool hasTypeForwardedFrom)
        {
            _fullTypeName = fullTypeName;
            _assemblyString = assemblyString;
            _hasTypeForwardedFrom = hasTypeForwardedFrom;
        }

        internal string FullTypeName => _fullTypeName;

        internal string AssemblyString => _assemblyString;

        internal bool HasTypeForwardedFrom => _hasTypeForwardedFrom;
    }
}
