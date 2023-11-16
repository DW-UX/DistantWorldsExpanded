using System.Diagnostics;

namespace System.Runtime.Serialization
{
    internal static class SerializationTrace
    {
        private static TraceSource _codeGen;

        internal static SourceSwitch CodeGenerationSwitch => CodeGenerationTraceSource.Switch;

        internal static void WriteInstruction(int lineNumber, string instruction)
        {
            CodeGenerationTraceSource.TraceInformation("{0:00000}: {1}", lineNumber, instruction);
        }

        internal static void TraceInstruction(string instruction)
        {
            CodeGenerationTraceSource.TraceEvent(TraceEventType.Verbose, 0, instruction);
        }

        private static TraceSource CodeGenerationTraceSource
        {
            get
            {
                if (_codeGen == null)
                {
                    _codeGen = new TraceSource("E5.Runtime.Serialization.CodeGeneration");
                }
                return _codeGen;
            }
        }
    }
}
