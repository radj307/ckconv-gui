using ckconv_gui.Measurement.Enum;
using ImportedWPF.Collections;
using System.Collections.Generic;
using System.Linq;
using TypeExtensions;

namespace ckconv_gui.Measurement.Systems
{
    public class MeasurementSystem
    {
        public EMeasurementSystem ID { get; }
        private string? _name;
        public string Name => _name ??= System.Enum.GetName(ID) ?? string.Empty;

        public ObservableImmutableList<Unit> Units { get; }
        public static ObservableImmutableList<Unit> NoUnits { get; } = new();
        public static ObservableImmutableList<Unit> AllUnits { get; } = new();

        public Unit Base { get; }
        /// <summary>
        /// The string representation of the number base for this measurement system.
        /// </summary>
        public virtual string? BaseString => Base.ToString();

        public MeasurementSystem(EMeasurementSystem systemID, ObservableImmutableList<Unit> units, Unit @base)
        {
            AllUnits.AddRangeIfUnique(units);
            ID = systemID;
            Units = units;
            Base = @base;
        }
        public MeasurementSystem(EMeasurementSystem systemID, string name, ObservableImmutableList<Unit> units, Unit @base)
        {
            AllUnits.AddRangeIfUnique(units);
            _name = name;
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
                if ((u.Symbol.Length > 0 && s.Equals(u.Symbol, System.StringComparison.Ordinal))
                    || s.EqualsAny(System.StringComparison.OrdinalIgnoreCase, u.GetFullName(false), u.GetFullName(true))
                    || s.EqualsAny(System.StringComparison.OrdinalIgnoreCase, u.ExtraNames.AsEnumerable()))
                    return u;
            }
            return null;
        }
    }
}
