using System.Collections.Generic;

namespace System.Runtime.Serialization
{
    public sealed class ExtensionDataObject
    {
        internal ExtensionDataObject()
        {
        }

        internal IList<ExtensionDataMember> Members { get; set; }
    }
}
