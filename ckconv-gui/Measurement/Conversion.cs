using ckconv_gui.Measurement.Enum;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ckconv_gui.Measurement
{
    public class Conversion : INotifyPropertyChanged
    {
        public Unit? _inUnit = Unit.NullUnit;
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
        public decimal _inValue;
        public decimal InValue
        {
            get => _inValue;
            set
            {
                _inValue = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(OutValue));
            }
        }
        public Unit? _outUnit = Unit.NullUnit;
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
        public decimal? OutValue
        {
            get
            {
                if (InUnit == null || InUnit.SystemID.Equals(EMeasurementSystem.None) || OutUnit == null || OutUnit.SystemID.Equals(EMeasurementSystem.None)) return null;
                return ConversionAPI.Convert(InUnit, InValue, OutUnit);
            }
        }
        public string OutValueString
        {
            get
            {
                if (OutValue is decimal v)
                    v.ToString();
                return string.Empty;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new(propertyName));
    }
}
