using ckconv_gui.Measurement.Enum;

namespace ckconv_gui.Measurement
{
    public static class NumericFormat
    {
        public static string? FormatNumber(decimal n, ENotation notation) => notation switch
        {
            // NUMBER FORMAT SPECIFIER
            ENotation.Default => $"{n:N}",
            // EXPONENTIAL FORMAT SPECIFIER
            ENotation.Scientific => $"{n:E}",
            // FIXED-POINT FORMAT SPECIFIER
            ENotation.Standard => $"{n:F}",
            // GENERAL FORMAT SPECIFIER
            ENotation.Compact => $"{n:G}",
            _ => null,
        };
    }
}
