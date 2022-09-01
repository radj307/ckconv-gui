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

        public static readonly Unit Yoctometer = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.YOCTO), "ym", "Yoctometer");
        public static readonly Unit Zeptometer = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.ZEPTO), "zm", "Zeptometer");
        public static readonly Unit Attometer = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.ATTO), "am", "Attometer");
        public static readonly Unit Femtometer = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.FEMTO), "fm", "Femtometer");
        public static readonly Unit Picometer = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.PICO), "pm", "Picometer");
        public static readonly Unit Nanometer = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.NANO), "nm", "Nanometer");
        public static readonly Unit Micrometer = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.MICRO), "um", "Micrometer");
        public static readonly Unit Millimeter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.MILLI), "mm", "Millimeter");
        public static readonly Unit Centimeter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.CENTI), "cm", "Centimeter");
        public static readonly Unit Decimeter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.DECI), "dm", "Decimeter");
        public static readonly Unit Meter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.BASE), "m", "Meter");
        public static readonly Unit Decameter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.DECA), "dam", "Decameter");
        public static readonly Unit Hectometer = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.HECTO), "hm", "Hectometer");
        public static readonly Unit Kilometer = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.KILO), "km", "Kilometer");
        public static readonly Unit Megameter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.MEGA), "Mm", "Megameter");
        public static readonly Unit Gigameter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.GIGA), "Gm", "Gigameter");
        public static readonly Unit Terameter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.TERA), "Tm", "Terameter");
        public static readonly Unit Petameter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.PETA), "Pm", "Petameter");
        public static readonly Unit Exameter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.EXA), "Em", "Exameter");
        public static readonly Unit Zettameter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.ZETTA), "Zm", "Zettameter");
        public static readonly Unit Yottameter = new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.YOTTA), "Ym", "Yottameter");

        public static readonly ObservableImmutableList<Unit> StaticUnits = new()
        {
            Yoctometer,
            Zeptometer,
            Attometer,
            Femtometer,
            Picometer,
            Nanometer,
            Micrometer,
            Millimeter,
            Centimeter,
            Decimeter,
            Meter,
            Decameter,
            Hectometer,
            Kilometer,
            Megameter,
            Gigameter,
            Terameter,
            Petameter,
            Exameter,
            Zettameter,
            Yottameter,
        };

        public MetricSystem() : base(EMeasurementSystem.Metric, StaticUnits, StaticUnits.First(u => u.UnitConversionFactor.Equals(1))) { }

        public new static Unit? Find(string s)
        {
            foreach (Unit u in StaticUnits)
            {
                if ((u.Symbol.Length > 0 && s.Equals(u.Symbol, System.StringComparison.Ordinal))
                    || s.EqualsAny(System.StringComparison.OrdinalIgnoreCase, u.GetFullName(false), u.GetFullName(true))
                    || s.EqualsAny(System.StringComparison.OrdinalIgnoreCase, u.ExtraNames.AsEnumerable()))
                    return u;
            }
            return null;
        }
    }
}
