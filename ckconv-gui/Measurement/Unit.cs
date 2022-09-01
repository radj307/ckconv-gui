using ckconv_gui.Interfaces;
using ckconv_gui.Measurement.Enum;
using System.Collections.Generic;
using System.Linq;

namespace ckconv_gui.Measurement
{
    /**
     * @struct	Unit
     * @brief	Represents a length measurement unit. *(Does not contain a value.)*
     */
    public class Unit : IUnit
    {
        private readonly string? _fullName;
        private readonly string _fullNamePluralExt;
        private readonly bool _pluralIsOverrideNotExt = false;

        public Unit(EMeasurementSystem system, double unitcf, string symbol, string? fullName, string fullNamePluralExt = "s")
        {
            SystemID = system;
            UnitConversionFactor = unitcf;
            Symbol = symbol;
            _fullName = fullName;
            _fullNamePluralExt = fullNamePluralExt;
            _pluralIsOverrideNotExt = false;
            ExtraNames = new();
        }

        public Unit(EMeasurementSystem system, double unitcf, string symbol, string? fullName, string fullNamePluralExt, bool pluralIsOverrideNotExt, params string[] extraNames)
        {
            SystemID = system;
            UnitConversionFactor = unitcf;
            Symbol = symbol;
            _fullName = fullName;
            _fullNamePluralExt = fullNamePluralExt;
            _pluralIsOverrideNotExt = pluralIsOverrideNotExt;
            ExtraNames = extraNames.ToList();
        }

        public static readonly Unit NullUnit = new(EMeasurementSystem.None, 1.0, "null", "null", string.Empty);
        public EMeasurementSystem SystemID { get; private set; }
        public double UnitConversionFactor { get; private set; }
        public string Symbol { get; private set; }
        public List<string> ExtraNames { get; }
        public string FullName
        {
            get => GetFullName(false);
        }

        public string GetFullName(bool plural = true) => (plural ? _pluralIsOverrideNotExt ? _fullNamePluralExt : $"{_fullName}{_fullNamePluralExt}" : _fullName) ?? Symbol;
        public double ConvertToBase(double value) => value * UnitConversionFactor;
        public bool HasUniquePlural() => _pluralIsOverrideNotExt;

        /// <inheritdoc cref="ConversionAPI.GetUnit(string, Unit?)"/>
        public static Unit? FromString(string s) => ConversionAPI.GetUnit(s);
    };
}
