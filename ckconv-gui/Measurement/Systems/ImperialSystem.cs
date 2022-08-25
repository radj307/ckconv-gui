using ckconv_gui.Measurement.Enum;
using ImportedWPF.Collections;
using System.Linq;
using TypeExtensions;

namespace ckconv_gui.Measurement.Systems
{
    public class ImperialSystem : MeasurementSystem
    {
        static ImperialSystem() => AllUnits.AddRangeIfUnique(StaticUnits);
        public static readonly ObservableImmutableList<Unit> StaticUnits = new()
        {
            new(EMeasurementSystem.Imperial, 1.0m / 17280.0m, "", "Twip"),
            new(EMeasurementSystem.Imperial, 1.0m / 12000.0m, "th", "Thou"),
            new(EMeasurementSystem.Imperial, 1.0m / 36.0m, "Bc", "Barleycorn"),
            new(EMeasurementSystem.Imperial, 1.0m / 12.0m, "\"", "Inch", "es", false, "in"),
            new(EMeasurementSystem.Imperial, 1.0m / 3.0m, "h", "Hand"),
            new(EMeasurementSystem.Imperial, 1.0m, "'", "Foot", "Feet", true, "ft"),
            new(EMeasurementSystem.Imperial, 3.0m, "yd", "Yard"),
            new(EMeasurementSystem.Imperial, 66.0m, "ch", "Chain"),
            new(EMeasurementSystem.Imperial, 660.0m, "fur", "Furlong"),
            new(EMeasurementSystem.Imperial, 5280.0m, "mi", "Mile"),
            new(EMeasurementSystem.Imperial, 15840.0m, "lea", "League"),
            new(EMeasurementSystem.Imperial, 6.0761m, "ftm", "Fathom"),
            new(EMeasurementSystem.Imperial, 607.61m, "", "Cable"),
            new(EMeasurementSystem.Imperial, 6076.1m, "nmi", "NauticalMile", "s", false, "Nautical Mile", "nmile"),
            new(EMeasurementSystem.Imperial, 66.0m / 100.0m, "", "Link"),
            new(EMeasurementSystem.Imperial, 66.0m / 4.0m, "rd", "Rod"),
        };

        public ImperialSystem() : base(EMeasurementSystem.Imperial, StaticUnits, StaticUnits.First(u => u.UnitConversionFactor.Equals(1m))) { }

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
