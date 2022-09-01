using ckconv_gui.Interfaces;
using ckconv_gui.Measurement.Enum;
using ckconv_gui.Measurement.Interfaces;
using Log;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ckconv_gui.Measurement
{
    [DoNotNotify] //< we have custom notification logic
    public class TwoWayConversion : IConversion, INotifyPropertyChanged
    {
        #region Constructors
        public TwoWayConversion() { }
        public TwoWayConversion(double lv, Unit lu, Unit ru)
        {
            LeftValue = lv;
            LeftUnit = lu;

            RightUnit = ru;

            ConversionAPI.TryConvert(lu, lv, ru, out double? rv, out Exception? ex);
            if (ex is not null) Log.Error($"{lv} {lu.FullName} => {ru.FullName} conversion failed!", ex);
            RightValue = rv;
        }
        public TwoWayConversion(Unit lu, double rv, Unit ru)
        {
            LeftUnit = lu;

            RightValue = rv;
            RightUnit = ru;

            ConversionAPI.TryConvert(ru, rv, lu, out double? lv, out Exception? ex);
            if (ex is not null) Log.Error($"{rv} {ru.FullName} => {lu.FullName} conversion failed!", ex);
            LeftValue = lv;
        }
        #endregion Constructors

        #region Fields
        private const string NaN_string = "NaN";
        #endregion Fields

        #region Properties
        private static LogWriter Log => FLog.Log;
        public ENotation Notation { get; set; } = ENotation.Standard;

        #region LeftSide
        private double? _leftValue;
        /// <summary>
        /// Left-side value of type <see langword="double"/>.
        /// </summary>
        public double? LeftValue
        {
            get => _leftValue ??= (RightIsValid && LeftUnit is not null ? ConversionAPI.Convert(RightUnit!, RightValue!.Value, LeftUnit) : null);
            set
            {
                _leftValue = value;
                _rightValue = null;

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(LeftString));
                NotifyPropertyChanged(nameof(RightValue));
                NotifyPropertyChanged(nameof(RightString));
            }
        }
        private Unit? _leftUnit;
        /// <summary>
        /// Left-side <see cref="Unit"/>.
        /// </summary>
        public Unit? LeftUnit
        {
            get => _leftUnit;
            set
            {
                _leftUnit = value;
                _leftValue = null;

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(LeftValue));
                NotifyPropertyChanged(nameof(LeftString));
            }
        }
        public bool LeftIsValid => _leftUnit is not null && _leftValue.HasValue;
        public string? LeftString
        {
            get => LeftValue?.ToString();
            set
            {
                try
                {
                    LeftValue = Convert.ToDouble(value);
                }
                catch (FormatException ex)
                {
                    Log.Error($"Cannot convert value '{value}' of type {typeof(string).Name} to {typeof(double).Name} because it is not in a valid format! (See Exception)", ex);
                }
                catch (OverflowException)
                {
                    try
                    {
                        LeftValue = Convert.ToDouble(value);
                    }
                    catch (OverflowException ex)
                    {
                        Log.Error($"Couldn't convert value '{value}' to a numeric type because it exceeds the minimum/maximum boundaries of all supported types! ({double.MinValue} < {typeof(double).Name} < {double.MaxValue}) ({double.MinValue} < {typeof(double).Name} < {double.MaxValue})", ex);
                    }
                }
            }
        }
        //public string LeftString => LeftValue.HasValue ? (NumericFormat.FormatNumber(LeftValue.Value, Notation) ?? NaN_string) : NaN_string;
        #endregion LeftSide

        #region RightSide
        private double? _rightValue;
        /// <summary>
        /// Right-side value of type <see langword="double"/>.
        /// </summary>
        public double? RightValue
        {
            get => _rightValue ??= (LeftIsValid && RightUnit is not null ? ConversionAPI.Convert(LeftUnit!, LeftValue!.Value, RightUnit) : null);
            set
            {
                _rightValue = value;
                _leftValue = null;

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RightString));
                NotifyPropertyChanged(nameof(LeftValue));
                NotifyPropertyChanged(nameof(LeftString));
            }
        }
        private Unit? _rightUnit;
        /// <summary>
        /// Right-side <see cref="Unit"/>.
        /// </summary>
        public Unit? RightUnit
        {
            get => _rightUnit;
            set
            {
                _rightUnit = value;
                _rightValue = null;

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(RightValue));
                NotifyPropertyChanged(nameof(RightString));
            }
        }
        public bool RightIsValid => _rightUnit is not null && _rightValue.HasValue;
        public string? RightString
        {
            get => RightValue?.ToString();
            set
            {
                try
                {
                    RightValue = Convert.ToDouble(value);
                }
                catch (FormatException ex)
                {
                    Log.Error($"Cannot convert value '{value}' of type {typeof(string).Name} to {typeof(double).Name} because it is not in a valid format! (See Exception)", ex);
                }
                catch (OverflowException)
                {
                    try
                    {
                        RightValue = Convert.ToDouble(value);
                    }
                    catch (OverflowException ex)
                    {
                        Log.Error($"Couldn't convert value '{value}' to a numeric type because it exceeds the minimum/maximum boundaries of all supported types! ({double.MinValue} < {typeof(double).Name} < {double.MaxValue}) ({double.MinValue} < {typeof(double).Name} < {double.MaxValue})", ex);
                    }
                }
            }
        }// => RightValue.HasValue ? NumericFormat.FormatNumber(RightValue.Value, Notation) ?? NaN_string : NaN_string;
        #endregion RightSide
        #endregion Properties

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new(propertyName));
        #endregion Events

        #region Expand
        /// <summary>
        /// Expands the given conversion array expression into individual conversions.<br/>
        /// If any of the <paramref name="inUnits"/>, <paramref name="outUnits"/>, or <paramref name="inValues"/> arrays are empty, no conversions are returned.
        /// </summary>
        /// <param name="inUnits">Any number of input units.</param>
        /// <param name="outUnits">Any number of output units.</param>
        /// <param name="inValues">Any number of input values.</param>
        /// <returns>An array of <see cref="TwoWayConversion"/> instances that represent all conversions specified by the given parameters.</returns>
        public static TwoWayConversion[] Expand(IEnumerable<Unit> inUnits, IEnumerable<Unit> outUnits, IEnumerable<double> inValues)
        {
            List<TwoWayConversion> l = new();

            foreach (double val in inValues)
            {
                foreach (Unit inUnit in inUnits)
                {
                    foreach (Unit outUnit in outUnits)
                    {
                        l.Add(new(val, inUnit, outUnit));
                    }
                }
            }

            return l.ToArray();
        }
        /// <inheritdoc cref="Expand(IEnumerable{Unit}, IEnumerable{Unit}, IEnumerable{double})"/>
        public static TwoWayConversion[] Expand(Unit inUnit, IEnumerable<Unit> outUnits, IEnumerable<double> inValues) => Expand(new Unit[] { inUnit }, outUnits, inValues);
        /// <inheritdoc cref="Expand(IEnumerable{Unit}, IEnumerable{Unit}, IEnumerable{double})"/>
        public static TwoWayConversion[] Expand(IEnumerable<Unit> inUnits, Unit outUnit, IEnumerable<double> inValues) => Expand(inUnits, new Unit[] { outUnit }, inValues);
        /// <inheritdoc cref="Expand(IEnumerable{Unit}, IEnumerable{Unit}, IEnumerable{double})"/>
        public static TwoWayConversion[] Expand(IEnumerable<Unit> inUnits, IEnumerable<Unit> outUnits, double inValue) => Expand(inUnits, outUnits, new double[] { inValue });
        /// <inheritdoc cref="Expand(IEnumerable{Unit}, IEnumerable{Unit}, IEnumerable{double})"/>
        public static TwoWayConversion[] Expand(IEnumerable<Unit> inUnits, Unit outUnit, double inValue) => Expand(inUnits, new Unit[] { outUnit }, new double[] { inValue });
        /// <inheritdoc cref="Expand(IEnumerable{Unit}, IEnumerable{Unit}, IEnumerable{double})"/>
        public static TwoWayConversion[] Expand(Unit inUnit, IEnumerable<Unit> outUnits, double inValue) => Expand(new Unit[] { inUnit }, outUnits, new double[] { inValue });
        /// <inheritdoc cref="Expand(IEnumerable{Unit}, IEnumerable{Unit}, IEnumerable{double})"/>
        public static TwoWayConversion[] Expand(Unit inUnit, Unit outUnit, IEnumerable<double> inValues) => Expand(new Unit[] { inUnit }, new Unit[] { outUnit }, inValues);
        /// <remarks>
        /// This overload acts as a pass-through for single conversions, and always returns an array with 1 element.
        /// </remarks>
        /// <inheritdoc cref="Expand(IEnumerable{Unit}, IEnumerable{Unit}, IEnumerable{double})"/>
        public static TwoWayConversion[] Expand(Unit inUnit, Unit outUnit, double inValue) => Expand(new Unit[] { inUnit }, new Unit[] { outUnit }, new double[] { inValue });
        #endregion Expand
    }
}
