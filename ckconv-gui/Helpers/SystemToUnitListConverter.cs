using ckconv_gui.Measurement.Enum;
using ImportedWPF.Collections;
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace ckconv_gui.Helpers
{
    [ValueConversion(typeof(EMeasurementSystem), typeof(ObservableImmutableList<Unit>))]
    internal class SystemToUnitListConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EMeasurementSystem sys)
            {
                return sys switch
                {
                    EMeasurementSystem.Metric => MetricSystem.StaticUnits,
                    EMeasurementSystem.Imperial => ImperialSystem.StaticUnits,
                    EMeasurementSystem.CreationKit => CreationKitSystem.StaticUnits,
                    _ => null,
                };
            }
            return null;
        }
        public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableImmutableList<Unit> list)
                return list.FirstOrDefault()?.EMeasurementSystem;
            else return null;
        }
    }
}
