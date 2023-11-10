namespace System.Runtime.Serialization
{
    internal class IntRef
    {
        private readonly int _value;

        public IntRef(int value)
        {
            _value = value;
        }

        public int Value => _value;
    }

}
