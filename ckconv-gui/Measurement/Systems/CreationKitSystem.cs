using ckconv_gui.Measurement.Enum;
using ImportedWPF.Collections;
using System;
using System.Linq;
using TypeExtensions;

namespace ckconv_gui.Measurement.Systems
{
    public class CreationKitSystem : MeasurementSystem
    {
        static CreationKitSystem() => AllUnits.AddRangeIfUnique(StaticUnits);
        public static readonly ObservableImmutableList<Unit> StaticUnits = new()
        {
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.YOCTO), "yu", "Yoctounit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.ZEPTO), "zu", "Zeptounit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.ATTO), "au", "Attounit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.FEMTO), "fu", "Femtounit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.PICO), "pu", "Picounit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.NANO), "nu", "Nanounit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.MICRO), "uu", "Microunit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.MILLI), "mu", "Milliunit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.CENTI), "cu", "Centiunit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.DECI), "du", "Deciunit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.BASE), "u", "Unit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.DECA), "dau", "Decaunit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.HECTO), "hu", "Hectounit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.KILO), "ku", "Kilometer"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.MEGA), "Mu", "Megaunit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.GIGA), "Gu", "Gigaunit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.TERA), "Tu", "Teraunit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.PETA), "Pu", "Petaunit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.EXA), "Eu", "Exaunit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.ZETTA), "Zu", "Zettaunit"),
            new(EMeasurementSystem.CreationKit, (decimal)Math.Pow(10.0, (double)SIPrefix.YOTTA), "Yu", "Yottaunit"),
        };

        public CreationKitSystem() : base(EMeasurementSystem.CreationKit, StaticUnits, StaticUnits.First(u => u.UnitConversionFactor.Equals(1))) { }

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
