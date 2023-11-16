﻿using System;
using System.Globalization;
using System.Xml;
using DataContractAttribute = System.Runtime.Serialization.DataContractAttribute;
using DataMemberAttribute = System.Runtime.Serialization.DataMemberAttribute;

namespace System.Runtime.Serialization
{
    [DataContract(Name = "DateTimeOffset", Namespace = "http://schemas.datacontract.org/2004/07/System")]
    internal struct DateTimeOffsetAdapter
    {
        private DateTime _utcDateTime;
        private short _offsetMinutes;

        public DateTimeOffsetAdapter(DateTime dateTime, short offsetMinutes)
        {
            _utcDateTime = dateTime;
            _offsetMinutes = offsetMinutes;
        }

        [DataMember(Name = "DateTime", IsRequired = true)]
        public DateTime UtcDateTime
        {
            get => _utcDateTime;
            set => _utcDateTime = value;
        }

        [DataMember(Name = "OffsetMinutes", IsRequired = true)]
        public short OffsetMinutes
        {
            get => _offsetMinutes;
            set => _offsetMinutes = value;
        }

        public static DateTimeOffset GetDateTimeOffset(DateTimeOffsetAdapter value)
        {
            try
            {
                switch (value.UtcDateTime.Kind)
                {
                    case DateTimeKind.Unspecified:
                        return new DateTimeOffset(value.UtcDateTime, new TimeSpan(0, value.OffsetMinutes, 0));

                    //DateTimeKind.Utc and DateTimeKind.Local
                    //Read in deserialized DateTime portion of the DateTimeOffsetAdapter and convert DateTimeKind to Unspecified.
                    //Apply ofset information read from OffsetMinutes portion of the DateTimeOffsetAdapter.
                    //Return converted DateTimeoffset object.
                    default:
                        DateTimeOffset deserialized = new DateTimeOffset(value.UtcDateTime);
                        return deserialized.ToOffset(new TimeSpan(0, value.OffsetMinutes, 0));
                }
            }
            catch (ArgumentException exception)
            {
                throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(XmlExceptionHelper.CreateConversionException(value.ToString(CultureInfo.InvariantCulture), "DateTimeOffset", exception));
            }
        }

        public static DateTimeOffsetAdapter GetDateTimeOffsetAdapter(DateTimeOffset value)
        {
            return new DateTimeOffsetAdapter(value.UtcDateTime, (short)value.Offset.TotalMinutes);
        }

        public string ToString(IFormatProvider provider)
        {
            return "DateTime: " + UtcDateTime + ", Offset: " + OffsetMinutes;
        }

    }
}
