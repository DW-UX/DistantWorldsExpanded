//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------
using System;
using System.Collections.ObjectModel;

namespace System.Runtime.Serialization
{
    public class ExportOptions
    {
        private Collection<Type> knownTypes;
        private IDataContractSurrogate dataContractSurrogate;

        public IDataContractSurrogate DataContractSurrogate
        {
            get => dataContractSurrogate;
            set => dataContractSurrogate = value;
        }

        internal IDataContractSurrogate GetSurrogate()
        {
            return dataContractSurrogate;
        }

        public Collection<Type> KnownTypes
        {
            get
            {
                if (knownTypes == null)
                {
                    knownTypes = new Collection<Type>();
                }
                return knownTypes;
            }
        }
    }
}

