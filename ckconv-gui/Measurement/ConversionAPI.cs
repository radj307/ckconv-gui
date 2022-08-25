using ckconv_gui.Measurement.Enum;
using ckconv_gui.Measurement.Systems;
using System;

namespace ckconv_gui.Measurement
{
    public static class ConversionAPI
    {
        public const decimal ONE_FOOT_IN_METERS = 0.3048m;
        public const decimal ONE_UNIT_IN_METERS = 0.0142875313m;
        public const decimal ONE_UNIT_IN_FEET = 0.046875m;

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

        public static decimal ConvertUnit(decimal in_unitcf, decimal v, decimal out_unitcf)
        {
            if (Equals(out_unitcf, 0.0))
                throw new ArgumentException("Cannot divide by 0!", nameof(out_unitcf));
            return v * in_unitcf / out_unitcf;
        }
        public static decimal ConvertSystem(EMeasurementSystem inSystem, decimal v_base, EMeasurementSystem outSystem)
        {
            if (inSystem.Equals(outSystem))
                return v_base;

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

        public static decimal Convert(Unit input, decimal value, Unit output)
        {
            if (input.UnitConversionFactor.Equals(0.0))
                throw new ArgumentException($"Illegal input conversion factor '{input.UnitConversionFactor}'!", nameof(input));
            else if (output.UnitConversionFactor.Equals(0.0))
                throw new ArgumentException($"Illegal input conversion factor '{input.UnitConversionFactor}'!", nameof(output));
            if (input.SystemID.Equals(output.SystemID))
                return ConvertUnit(input.UnitConversionFactor, value, output.UnitConversionFactor);
            return ConvertSystem(input.SystemID, input.ConvertToBase(value), output.SystemID) / output.UnitConversionFactor;
        }

        public static Unit? GetUnit(string s, Unit? defaultUnit = null)
        {
            if (ImperialSystem.Find(s) is Unit imperial) return imperial;
            if (MetricSystem.Find(ChangeMetreToMeter(s)) is Unit metric) return metric;
            if (CreationKitSystem.Find(s) is Unit creationkit) return creationkit;

            return defaultUnit;
        }
    }
}
