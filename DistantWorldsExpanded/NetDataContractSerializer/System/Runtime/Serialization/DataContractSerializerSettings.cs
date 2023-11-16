using System;
using System.Collections.Generic;
using System.Xml;
using DataContractResolver = System.Runtime.Serialization.DataContractResolver;

namespace System.Runtime.Serialization
{
    public class DataContractSerializerSettings
    {
        /// <summary>
        /// Gets or sets Dummy documentation
        /// </summary>
        public XmlDictionaryString RootName { get; set; }

        /// <summary>
        /// Gets or sets Dummy documentation
        /// </summary>
        public XmlDictionaryString RootNamespace { get; set; }

        /// <summary>
        /// Gets or sets Dummy documentation
        /// </summary>
        public IEnumerable<Type> KnownTypes { get; set; }

        /// <summary>
        /// Gets or sets Dummy documentation
        /// </summary>
        public int MaxItemsInObjectGraph { get; set; } = int.MaxValue;

        /// <summary>
        /// Gets or sets a value indicating whether Dummy documentation
        /// </summary>
        public bool IgnoreExtensionDataObject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Dummy documentation
        /// </summary>
        public bool PreserveObjectReferences { get; set; }

        /// <summary>
        /// Gets or sets Dummy documentation
        /// </summary>
        internal IDataContractSurrogate DataContractSurrogate { get; set; }

        /// <summary>
        /// Gets or sets Dummy documentation
        /// </summary>
        public DataContractResolver DataContractResolver { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Dummy documentation
        /// </summary>
        public bool SerializeReadOnlyTypes { get; set; }
    }
}
