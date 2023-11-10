namespace System.Runtime.Serialization
{
    internal class DataContractPairKey
    {
        private readonly object _object1;
        private readonly object _object2;

        public DataContractPairKey(object object1, object object2)
        {
            _object1 = object1;
            _object2 = object2;
        }

        public override bool Equals(object other)
        {
            DataContractPairKey otherKey = other as DataContractPairKey;
            if (otherKey == null)
            {
                return false;
            }

            return ((otherKey._object1 == _object1 && otherKey._object2 == _object2) || (otherKey._object1 == _object2 && otherKey._object2 == _object1));
        }

        public override int GetHashCode()
        {
            return _object1.GetHashCode() ^ _object2.GetHashCode();
        }
    }
}
