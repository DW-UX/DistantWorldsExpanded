using System;
using System.Collections.Generic;

namespace System.Runtime.Serialization
{
    internal sealed class GenericParameterDataContract : DataContract
    {
        private readonly GenericParameterDataContractCriticalHelper _helper;

        internal GenericParameterDataContract(Type type)
            : base(new GenericParameterDataContractCriticalHelper(type))
        {
            _helper = base.Helper as GenericParameterDataContractCriticalHelper;
        }

        internal int ParameterPosition => _helper.ParameterPosition;

        internal override bool IsBuiltInDataContract => true;

        private class GenericParameterDataContractCriticalHelper : DataContract.DataContractCriticalHelper
        {
            private readonly int parameterPosition;

            internal GenericParameterDataContractCriticalHelper(Type type)
                : base(type)
            {
                SetDataContractName(DataContract.GetStableName(type));
                parameterPosition = type.GenericParameterPosition;
            }

            internal int ParameterPosition => parameterPosition;
        }

        internal override DataContract BindGenericParameters(DataContract[] paramContracts, Dictionary<DataContract, DataContract> boundContracts)
        {
            return paramContracts[ParameterPosition];
        }
    }
}
