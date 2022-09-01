namespace ckconv_gui.Measurement.Interfaces
{
    public interface IConversion
    {
        double? LeftValue { get; set; }
        Unit? LeftUnit { get; set; }
        bool LeftIsValid { get; }
        string? LeftString { get; set; }

        double? RightValue { get; set; }
        Unit? RightUnit { get; set; }
        bool RightIsValid { get; }
        string? RightString { get; set; }
    }
}
