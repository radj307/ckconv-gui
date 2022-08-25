using ckconv_gui.Measurement.Enum;
using ImportedWPF.Collections;
using System;
using System.Linq;
using TypeExtensions;

namespace ckconv_gui.Measurement.Systems
{
    public class MetricSystem : MeasurementSystem
    {
        static MetricSystem() => AllUnits.AddRangeIfUnique(StaticUnits);
        public static readonly ObservableImmutableList<Unit> StaticUnits = new()
        {
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.YOCTO), "ym", "Yoctometer"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.ZEPTO), "zm", "Zeptometer"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.ATTO), "am", "Attometer"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.FEMTO), "fm", "Femtometer"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.PICO), "pm", "Picometer"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.NANO), "nm", "Nanometer"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.MICRO), "um", "Micrometer"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.MILLI), "mm", "Millimeter"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.CENTI), "cm", "Centimeter"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.DECI), "dm", "Decimeter"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.BASE), "m", "Meter"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.DECA), "dam", "Decameter"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.HECTO), "hm", "Hectometer"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.KILO), "km", "Kilometer"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.MEGA), "Mm", "Megameter"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.GIGA), "Gm", "Gigameter"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.TERA), "Tm", "Terameter"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.PETA), "Pm", "Petameter"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.EXA), "Em", "Exameter"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.ZETTA), "Zm", "Zettameter"),
            new(EMeasurementSystem.Metric, (decimal)Math.Pow(10.0, (double)SIPrefix.YOTTA), "Ym", "Yottameter"),
        };

        public MetricSystem() : base(EMeasurementSystem.Metric, StaticUnits, StaticUnits.First(u => u.UnitConversionFactor.Equals(1))) { }

        public new static Unit? Find(string s)
        {
            foreach (Unit u in StaticUnits)
            {
                if (u.HasUniquePlural() && (CompareUnitName(s, u.GetFullName(false)) || CompareUnitName(s, u.GetFullName(true))))
                    return u;
                if (CompareUnitSymbol(s, u.Symbol) || CompareUnitName(s, u.GetFullName()) || CompareUnitExtraNames(s, u.ExtraNames))
                    return u;
            }
            return null;
        }
    }
}
