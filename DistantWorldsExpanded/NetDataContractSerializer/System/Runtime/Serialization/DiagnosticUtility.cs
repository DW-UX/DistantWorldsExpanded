using System;
using System.Diagnostics;

namespace System.Runtime.Serialization
{
    internal class DiagnosticUtility
    {
        public class ExceptionUtility
        {
            internal static Exception ThrowHelperFatal(string message, Exception innerException)
            {
                return ThrowHelperError(new FatalException(message, innerException));
            }

            internal static Exception ThrowHelperError(Exception exception)
            {
                return ThrowHelper(exception, TraceEventType.Error);
            }

            internal static Exception ThrowHelper(Exception exception, TraceEventType eventType)
            {
                return exception;
            }

        }
    }
}
