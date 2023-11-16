using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Runtime.Serialization
{
    internal class CollectionDataNode : DataNode<Array>
    {
        private IList<IDataNode> items;
        private string itemName;
        private string itemNamespace;
        private int size = -1;

        internal CollectionDataNode()
        {
            dataType = Globals.TypeOfCollectionDataNode;
        }

        internal IList<IDataNode> Items
        {
            get => items;
            set => items = value;
        }

        internal string ItemName
        {
            get => itemName;
            set => itemName = value;
        }

        internal string ItemNamespace
        {
            get => itemNamespace;
            set => itemNamespace = value;
        }

        internal int Size
        {
            get => size;
            set => size = value;
        }

        public override void GetData(ElementData element)
        {
            base.GetData(element);

            element.AddAttribute(Globals.SerPrefix, Globals.SerializationNamespace, Globals.ArraySizeLocalName, Size.ToString(NumberFormatInfo.InvariantInfo));
        }

        public override void Clear()
        {
            base.Clear();
            items = null;
            size = -1;
        }
    }
}
