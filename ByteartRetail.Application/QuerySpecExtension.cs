using ByteartRetail.DataObjects;

namespace ByteartRetail.Application
{
    public static class QuerySpecExtension
    {
        public static bool IsVerbose(this QuerySpec spec)
        {
            return spec.Verbose ?? false;
        }
    }
}
