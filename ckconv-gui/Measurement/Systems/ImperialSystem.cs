using ckconv_gui.Measurement.Enum;
using ImportedWPF.Collections;
using System.Linq;
using TypeExtensions;

namespace ckconv_gui.Measurement.Systems
{
    public class ImperialSystem : MeasurementSystem
    {
        static ImperialSystem() => AllUnits.AddRangeIfUnique(StaticUnits);

        public static readonly Unit Twip = new(EMeasurementSystem.Imperial, 1.0 / 17280.0, "", "Twip");
        public static readonly Unit Thou = new(EMeasurementSystem.Imperial, 1.0 / 12000.0, "th", "Thou");
        public static readonly Unit Barleycorn = new(EMeasurementSystem.Imperial, 1.0 / 36.0, "Bc", "Barleycorn");
        public static readonly Unit Inch = new(EMeasurementSystem.Imperial, 1.0 / 12.0, "\"", "Inch", "es", false, "in");
        public static readonly Unit Hand = new(EMeasurementSystem.Imperial, 1.0 / 3.0, "h", "Hand");
        public static readonly Unit Feet = new(EMeasurementSystem.Imperial, 1.0, "'", "Foot", "Feet", true, "ft");
        public static readonly Unit Yard = new(EMeasurementSystem.Imperial, 3.0, "yd", "Yard");
        public static readonly Unit Chain = new(EMeasurementSystem.Imperial, 66.0, "ch", "Chain");
        public static readonly Unit Furlong = new(EMeasurementSystem.Imperial, 660.0, "fur", "Furlong");
        public static readonly Unit Mile = new(EMeasurementSystem.Imperial, 5280.0, "mi", "Mile");
        public static readonly Unit League = new(EMeasurementSystem.Imperial, 15840.0, "lea", "League");
        public static readonly Unit Fathom = new(EMeasurementSystem.Imperial, 6.0761, "ftm", "Fathom");
        public static readonly Unit Cable = new(EMeasurementSystem.Imperial, 607.61, "", "Cable");
        public static readonly Unit NauticalMile = new(EMeasurementSystem.Imperial, 6076.1, "nmi", "NauticalMile", "s", false, "Nautical Mile", "nmile");
        public static readonly Unit Link = new(EMeasurementSystem.Imperial, 66.0 / 100.0, "", "Link");
        public static readonly Unit Rod = new(EMeasurementSystem.Imperial, 66.0 / 4.0, "rd", "Rod");

        public static readonly ObservableImmutableList<Unit> StaticUnits = new()
        {
            Twip,
            Thou,
            Barleycorn,
            Inch,
            Hand,
            Feet,
            Yard,
            Chain,
            Furlong,
            Mile,
            League,
            Fathom,
            Cable,
            NauticalMile,
            Link,
            Rod,
        };

        public ImperialSystem() : base(EMeasurementSystem.Imperial, StaticUnits, StaticUnits.First(u => u.UnitConversionFactor.Equals(1))) { }

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
