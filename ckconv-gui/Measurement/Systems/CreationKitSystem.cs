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

        public static readonly Unit Yoctounit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.YOCTO), "yu", "Yoctounit");
        public static readonly Unit Zeptounit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.ZEPTO), "zu", "Zeptounit");
        public static readonly Unit Attounit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.ATTO), "au", "Attounit");
        public static readonly Unit Femtounit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.FEMTO), "fu", "Femtounit");
        public static readonly Unit Picounit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.PICO), "pu", "Picounit");
        public static readonly Unit Nanounit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.NANO), "nu", "Nanounit");
        public static readonly Unit Microunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.MICRO), "uu", "Microunit");
        public static readonly Unit Milliunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.MILLI), "mu", "Milliunit");
        public static readonly Unit Centiunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.CENTI), "cu", "Centiunit");
        public static readonly Unit Deciunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.DECI), "du", "Deciunit");
        public static readonly Unit Unit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.BASE), "u", "Unit");
        public static readonly Unit Decaunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.DECA), "dau", "Decaunit");
        public static readonly Unit Hectounit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.HECTO), "hu", "Hectounit");
        public static readonly Unit Kilometer = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.KILO), "ku", "Kilometer");
        public static readonly Unit Megaunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.MEGA), "Mu", "Megaunit");
        public static readonly Unit Gigaunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.GIGA), "Gu", "Gigaunit");
        public static readonly Unit Teraunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.TERA), "Tu", "Teraunit");
        public static readonly Unit Petaunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.PETA), "Pu", "Petaunit");
        public static readonly Unit Exaunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.EXA), "Eu", "Exaunit");
        public static readonly Unit Zettaunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.ZETTA), "Zu", "Zettaunit");
        public static readonly Unit Yottaunit = new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.YOTTA), "Yu", "Yottaunit");

        public static readonly ObservableImmutableList<Unit> StaticUnits = new()
        {
            Yoctounit,
            Zeptounit,
            Attounit,
            Femtounit,
            Picounit,
            Nanounit,
            Microunit,
            Milliunit,
            Centiunit,
            Deciunit,
            Unit,
            Decaunit,
            Hectounit,
            Kilometer,
            Megaunit,
            Gigaunit,
            Teraunit,
            Petaunit,
            Exaunit,
            Zettaunit,
            Yottaunit,
        };

        public CreationKitSystem() : base(EMeasurementSystem.CreationKit, "Creation Kit", StaticUnits, StaticUnits.First(u => u.UnitConversionFactor.Equals(1))) { }

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
