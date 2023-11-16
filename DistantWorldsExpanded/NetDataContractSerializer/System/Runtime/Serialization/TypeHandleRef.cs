using System;

namespace System.Runtime.Serialization
{
    internal class TypeHandleRef
    {
        private RuntimeTypeHandle _value;

        public TypeHandleRef()
        {
        }

        public TypeHandleRef(RuntimeTypeHandle value)
        {
            _value = value;
        }

        public RuntimeTypeHandle Value
        {
            get => _value;
            set => _value = value;
        }
    }
}
