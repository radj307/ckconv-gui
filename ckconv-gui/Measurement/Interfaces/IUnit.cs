using ckconv_gui.Measurement.Enum;
using System.Collections.Generic;

namespace ckconv_gui.Interfaces
{
    public interface IUnit
    {
        EMeasurementSystem SystemID { get; }
        decimal UnitConversionFactor { get; }
        string FullName { get; }
        string Symbol { get; }
        List<string> ExtraNames { get; }
    }
}
