using ckconv_gui.Measurement.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ckconv_gui
{
    static class Utils
    {
        /// @brief	Changes the *first occurrence* of the word 'metre' in the given string to 'meter'. This function ignores case.
        public static string ChangeMetreToMeter(string s)
        {
            return s.Replace("metre", "meter");
        }
        /// @brief	Changes the *first occurrence* of the word 'meter' in the given string to 'metre'. This function ignores case.
        public static string ChangeMeterToMetre(string s)
        {
            return s.Replace("meter", "metre");
        }
    }

    /**
     * @struct	Unit
     * @brief	Represents a length measurement unit. *(Does not contain a value.)*
     */
    public class Unit
    {
        private readonly string? _fullName;
        private readonly string _fullNamePluralExt;
        private readonly bool _pluralIsOverrideNotExt = false;

        public Unit(EMeasurementSystem system, double unitcf, string symbol, string? fullName, string fullNamePluralExt = "s")
        {
            EMeasurementSystem = system;
            UnitConversionFactor = unitcf;
            Symbol = symbol;
            _fullName = fullName;
            _fullNamePluralExt = fullNamePluralExt;
            _pluralIsOverrideNotExt = false;
            ExtraNames = new();
        }

        public Unit(EMeasurementSystem system, double unitcf, string symbol, string? fullName, string fullNamePluralExt, bool pluralIsOverrideNotExt, params string[] extraNames)
        {
            EMeasurementSystem = system;
            UnitConversionFactor = unitcf;
            Symbol = symbol;
            _fullName = fullName;
            _fullNamePluralExt = fullNamePluralExt;
            _pluralIsOverrideNotExt = pluralIsOverrideNotExt;
            ExtraNames = extraNames.ToList();
        }

        public EMeasurementSystem EMeasurementSystem { get; private set; }
        public double UnitConversionFactor { get; private set; }
        public string Symbol { get; private set; }
        public List<string> ExtraNames { get; }
        public string FullName
        {
            get => GetFullName();
        }

        public string GetFullName(bool plural = true) => (plural ? (_pluralIsOverrideNotExt ? _fullNamePluralExt : $"{_fullName}{_fullNamePluralExt}") : _fullName) ?? Symbol;
        public double ConvertToBase(double value) => value * UnitConversionFactor;
        public bool HasUniquePlural() => _pluralIsOverrideNotExt;
    };

    /**
     * @enum	Powers
     * @brief	Defines all SI prefixes and their base-10 exponent.
     *\n		See https://en.wikipedia.org/wiki/Metric_prefix#List_of_SI_prefixes
     */
    public enum SIPrefix : int
    {
        YOCTO = -24,
        ZEPTO = -21,
        ATTO = -18,
        FEMTO = -15,
        PICO = -12,
        NANO = -9,
        MICRO = -6,
        MILLI = -3,
        CENTI = -2,
        DECI = -1,
        BASE = 0,
        DECA = 1,
        HECTO = 2,
        KILO = 3,
        MEGA = 6,
        GIGA = 9,
        TERA = 12,
        PETA = 15,
        EXA = 18,
        ZETTA = 21,
        YOTTA = 24,
    };

    public class MeasurementSystem
    {
        public EMeasurementSystem ID { get; }
        private string? _name;
        public string Name => _name ??= Enum.GetName(ID) ?? string.Empty;

        public List<Unit> Units { get; }

        public Unit Base { get; }

        public MeasurementSystem(EMeasurementSystem systemID, List<Unit> units, Unit @base)
        {
            ID = systemID;
            Units = units;
            Base = @base;
        }

        public static bool CompareUnitSymbol(string s, string symbol) => s == symbol;
        public static bool CompareUnitName(string s, string name)
        {
            string sLower = s.ToLower();
            string nameLower = name.ToLower();

            if (sLower.Equals(nameLower)) return true;

            if (sLower.EndsWith('s'))
                sLower = sLower.Remove(sLower.Length - 1);

            return sLower.Equals(nameLower);
        }
        public static bool CompareUnitExtraNames(string s, List<string> extraNames) => extraNames.Any(n => CompareUnitName(s, n));

        public Unit? Find(string s)
        {
            foreach (Unit u in Units)
            {
                if (u.HasUniquePlural() && (CompareUnitName(s, u.GetFullName(false)) || CompareUnitName(s, u.GetFullName(true))))
                    return u;
                if (CompareUnitSymbol(s, u.Symbol) || CompareUnitName(s, u.GetFullName()) || CompareUnitExtraNames(s, u.ExtraNames))
                    return u;
            }
            return null;
        }
    }

    public class MetricSystem : MeasurementSystem
    {
        public static readonly List<Unit> StaticUnits = new()
        {
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.YOCTO), "ym", "Yoctometer"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.ZEPTO), "zm", "Zeptometer"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.ATTO), "am", "Attometer"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.FEMTO), "fm", "Femtometer"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.PICO), "pm", "Picometer"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.NANO), "nm", "Nanometer"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.MICRO), "um", "Micrometer"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.MILLI), "mm", "Millimeter"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.CENTI), "cm", "Centimeter"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.DECI), "dm", "Decimeter"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.BASE), "m", "Meter"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.DECA), "dam", "Decameter"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.HECTO), "hm", "Hectometer"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.KILO), "km", "Kilometer"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.MEGA), "Mm", "Megameter"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.GIGA), "Gm", "Gigameter"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.TERA), "Tm", "Terameter"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.PETA), "Pm", "Petameter"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.EXA), "Em", "Exameter"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.ZETTA), "Zm", "Zettameter"),
            new(EMeasurementSystem.Metric, Math.Pow(10.0, (double)SIPrefix.YOTTA), "Ym", "Yottameter"),
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

    public class CreationKitSystem : MeasurementSystem
    {
        public static readonly List<Unit> StaticUnits = new()
        {
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.YOCTO), "yu", "Yoctounit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.ZEPTO), "zu", "Zeptounit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.ATTO), "au", "Attounit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.FEMTO), "fu", "Femtounit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.PICO), "pu", "Picounit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.NANO), "nu", "Nanounit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.MICRO), "uu", "Microunit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.MILLI), "mu", "Milliunit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.CENTI), "cu", "Centiunit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.DECI), "du", "Deciunit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.BASE), "u", "Unit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.DECA), "dau", "Decaunit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.HECTO), "hu", "Hectounit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.KILO), "ku", "Kilometer"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.MEGA), "Mu", "Megaunit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.GIGA), "Gu", "Gigaunit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.TERA), "Tu", "Teraunit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.PETA), "Pu", "Petaunit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.EXA), "Eu", "Exaunit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.ZETTA), "Zu", "Zettaunit"),
            new(EMeasurementSystem.CreationKit, Math.Pow(10.0, (double)SIPrefix.YOTTA), "Yu", "Yottaunit"),
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

    public class ImperialSystem : MeasurementSystem
    {
        public static readonly List<Unit> StaticUnits = new()
        {
            new(EMeasurementSystem.Imperial, (1.0 / 17280.0), "", "Twip"),
            new(EMeasurementSystem.Imperial, (1.0 / 12000.0), "th", "Thou"),
            new(EMeasurementSystem.Imperial, (1.0 / 36.0), "Bc", "Barleycorn"),
            new(EMeasurementSystem.Imperial, (1.0 / 12.0), "\"", "Inch", "es", false, "in"),
            new(EMeasurementSystem.Imperial, (1.0 / 3.0), "h", "Hand"),
            new(EMeasurementSystem.Imperial, (1.0), "'", "Foot", "Feet", true, "ft"),
            new(EMeasurementSystem.Imperial, (3.0), "yd", "Yard"),
            new(EMeasurementSystem.Imperial, (66.0), "ch", "Chain"),
            new(EMeasurementSystem.Imperial, (660.0), "fur", "Furlong"),
            new(EMeasurementSystem.Imperial, (5280.0), "mi", "Mile"),
            new(EMeasurementSystem.Imperial, (15840.0), "lea", "League"),
            new(EMeasurementSystem.Imperial, (6.0761), "ftm", "Fathom"),
            new(EMeasurementSystem.Imperial, (607.61), "", "Cable"),
            new(EMeasurementSystem.Imperial, (6076.1), "nmi", "NauticalMile", "s", false, "Nautical Mile", "nmile"),
            new(EMeasurementSystem.Imperial, (66.0 / 100.0), "", "Link"),
            new(EMeasurementSystem.Imperial, (66.0 / 4.0), "rd", "Rod"),
        };

        public ImperialSystem() : base(EMeasurementSystem.Imperial, StaticUnits, StaticUnits.First(u => u.UnitConversionFactor.Equals(1))) { }

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

    public static class ConversionAPI
    {
        public const double ONE_FOOT_IN_METERS = 0.3048;
        public const double ONE_UNIT_IN_METERS = 0.0142875313;
        public const double ONE_UNIT_IN_FEET = 0.046875;

        public static double ConvertUnit(double in_unitcf, double v, double out_unitcf)
        {
            if (Math.Equals(out_unitcf, 0.0))
            {
                throw new ArgumentException("Cannot divide by 0!", nameof(out_unitcf));
            }
            return ((v * in_unitcf) / out_unitcf);
        }
        public static double ConvertSystem(EMeasurementSystem inSystem, double v_base, EMeasurementSystem outSystem)
        {
            if (inSystem.Equals(outSystem))
            {
                return v_base;
            }

            switch (inSystem)
            {
            case EMeasurementSystem.Metric:
                switch (outSystem)
                { // Metric ->
                case EMeasurementSystem.Imperial:
                    return v_base / ONE_FOOT_IN_METERS;
                case EMeasurementSystem.CreationKit:
                    return v_base / ONE_UNIT_IN_METERS;
                }
                break;
            case EMeasurementSystem.Imperial:
                switch (outSystem)
                { // Imperial ->
                case EMeasurementSystem.Metric:
                    return v_base * ONE_FOOT_IN_METERS;
                case EMeasurementSystem.CreationKit:
                    return v_base / ONE_UNIT_IN_FEET;
                }
                break;
            case EMeasurementSystem.CreationKit:
                switch (outSystem)
                { // CreationKit ->
                case EMeasurementSystem.Metric:
                    return v_base * ONE_UNIT_IN_METERS;
                case EMeasurementSystem.Imperial:
                    return v_base * ONE_UNIT_IN_FEET;
                }
                break;
            default: break;
            }
            throw new ArgumentException($"Invalid System Conversion: {inSystem:G} => {outSystem:G}");
        }

        public static double Convert(Unit input, double value, Unit output)
        {
            if (input.UnitConversionFactor.Equals(0.0))
                throw new ArgumentException($"Illegal input conversion factor '{input.UnitConversionFactor}'!", nameof(input));
            else if (output.UnitConversionFactor.Equals(0.0))
                throw new ArgumentException($"Illegal input conversion factor '{input.UnitConversionFactor}'!", nameof(output));
            if (input.EMeasurementSystem.Equals(output.EMeasurementSystem))
                return ConvertUnit(input.UnitConversionFactor, value, output.UnitConversionFactor);
            return ConvertSystem(input.EMeasurementSystem, input.ConvertToBase(value), output.EMeasurementSystem);
        }

        public static Unit? GetUnit(string s, Unit? defaultUnit = null)
        {
            if (ImperialSystem.Find(s) is Unit imperial) return imperial;
            if (MetricSystem.Find(Utils.ChangeMetreToMeter(s)) is Unit metric) return metric;
            if (CreationKitSystem.Find(s) is Unit creationkit) return creationkit;

            return defaultUnit;
        }
    }

    public class Conversion : INotifyPropertyChanged
    {
        public Unit? _inUnit;
        public Unit? InUnit
        {
            get => _inUnit;
            set
            {
                _inUnit = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(OutValue));
            }
        }
        public double _inValue;
        public double InValue
        {
            get => _inValue;
            set
            {
                _inValue = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(OutValue));
            }
        }
        public Unit? _outUnit;
        public Unit? OutUnit
        {
            get => _outUnit;
            set
            {
                _outUnit = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(OutValue));
            }
        }
        public double? OutValue
        {
            get
            {
                if (InUnit == null || OutUnit == null) return null;
                return ConversionAPI.Convert(InUnit, InValue, OutUnit);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new(propertyName));
    }
}
