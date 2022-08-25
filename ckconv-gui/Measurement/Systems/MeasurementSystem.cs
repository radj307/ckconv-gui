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

        public MeasurementSystem(EMeasurementSystem systemID, ObservableImmutableList<Unit> units, Unit @base)
        {
            AllUnits.AddRangeIfUnique(units);
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
}
