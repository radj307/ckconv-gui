using ckconv_gui.Measurement.Enum;
using ckconv_gui.Measurement.Systems;
using System;
using System.Diagnostics.CodeAnalysis;
using TypeExtensions;

namespace ckconv_gui.Measurement
{
    public static class ConversionAPI
    {
        const double ONE_FOOT_IN_METERS = 0.3048;
        const double ONE_UNIT_IN_METERS = 0.0142875313;
        const double ONE_UNIT_IN_FEET = 0.046875;

        internal static string ChangeMetreToMeter(string s) => s.Replace("metre", "meter");
        internal static string ChangeMeterToMetre(string s) => s.Replace("meter", "metre");

        private static double ConvertUnit(double in_unitcf, double v, double out_unitcf)
        {
            if (out_unitcf.Equals(0.0))
                throw new ArgumentException("Cannot divide by 0!", nameof(out_unitcf));
            return v * in_unitcf / out_unitcf;
        }
        private static double ConvertSystem(EMeasurementSystem inSystem, double v_base, EMeasurementSystem outSystem)
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

        public static double Convert(Unit input, double value, Unit output)
        {
            if (input.UnitConversionFactor.Equals(0.0m))
                throw new ArgumentException($"Illegal input conversion factor '{input.UnitConversionFactor}'!", nameof(input));
            else if (output.UnitConversionFactor.Equals(0.0m))
                throw new ArgumentException($"Illegal input conversion factor '{input.UnitConversionFactor}'!", nameof(output));
            if (input.SystemID.Equals(output.SystemID))
                return ConvertUnit(input.UnitConversionFactor, value, output.UnitConversionFactor);
            return ConvertSystem(input.SystemID, input.ConvertToBase(value), output.SystemID) / output.UnitConversionFactor;
        }
        public static bool TryConvert(Unit input, double inValue, Unit output, [MaybeNullWhen(false)] out double? outValue, [MaybeNullWhen(true)] out Exception? exception)
        {
            try
            {
                outValue = Convert(input, inValue, output);
                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                outValue = null;
                exception = ex;
                return false;
            }
        }
        public static bool TryConvert(Unit input, double inValue, Unit output, [MaybeNullWhen(false)] out double? outValue) => TryConvert(input, inValue, output, out outValue, out Exception? _);

        public static Unit? GetUnit(string s, Unit? defaultUnit = null)
        {
            if (ImperialSystem.Find(s) is Unit imperial) return imperial;
            if (MetricSystem.Find(ChangeMetreToMeter(s)) is Unit metric) return metric;
            if (CreationKitSystem.Find(s) is Unit creationkit) return creationkit;

            return defaultUnit;
        }
    }
}
