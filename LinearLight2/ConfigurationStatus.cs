using System.Diagnostics.CodeAnalysis;

namespace LinearLight2
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum ConfigurationStatus
    {
        OK = 0,
        FAIL = 1,
        Saving = 0x5AFE,
        LoadingDefault = 0xDEFA,
    }
}