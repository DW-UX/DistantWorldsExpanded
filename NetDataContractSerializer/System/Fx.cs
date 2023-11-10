using System;
using System.Diagnostics;

namespace System
{
    internal class Fx
    {
        public static void Assert(string message)
        {
            Debug.Assert(false, message);
        }

        public static void Assert(bool condition, string message)
        {
            Debug.Assert(condition, message);
        }

        public static bool IsFatal(Exception ex)
        {
            ex = Unwrap(ex);

            return ex is NullReferenceException ||
                   ex is StackOverflowException ||
                   ex is OutOfMemoryException ||
                   ex is System.Threading.ThreadAbortException ||
                   ex is System.Runtime.InteropServices.SEHException ||
                   ex is System.Security.SecurityException;
        }

        internal static Exception Unwrap(Exception ex)
        {
            // for certain types of exceptions, we care more about the inner
            // exception
            while (ex.InnerException != null &&
                    (ex is System.Reflection.TargetInvocationException))
            {
                ex = ex.InnerException;
            }

            return ex;
        }
    }
}
