using System;
using System.Globalization;
using System.Windows.Data;
using TypeExtensions;

namespace ckconv_gui.Helpers
{
    [ValueConversion(typeof(string), typeof(decimal))]
    public class StringToDecimalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is string s ? System.Convert.ToDecimal(s.RemoveIf(char.IsAscii)) : decimal.Zero;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => ((decimal)value).ToString();
    }
}
