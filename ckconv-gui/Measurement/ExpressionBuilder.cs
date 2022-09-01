using Log;
using PropertyChanged;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TypeExtensions;

namespace ckconv_gui.Measurement
{
    [DoNotNotify]
    public class ExpressionBuilder : INotifyPropertyChanged
    {
        private struct Expr
        {
            public Expr(string s, int idx)
            {
                String = s;
                Index = idx;
            }

            public readonly string String;
            public readonly int Index;

            public struct UnitString : IEnumerable<string>
            {
                public UnitString(string[] items)
                {
                    Items = items;
                }

                public readonly string[] Items;
                public string this[int idx] => Items[idx];

                public IEnumerator<string> GetEnumerator() => ((IEnumerable<string>)Items).GetEnumerator();
                IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();

                public UnitString FromString(string s) => s.Any(char.IsDigit) ? throw new ArgumentOutOfRangeException(nameof(s), s, $"Digits cannot be present in a unit specifier! (\"{s}\")") : new(s.Split(',').ToArray());
            }

            public static Expr[] FromExpressionStrings(IList<string> expressionStrings)
            {
                var l = new Expr[expressionStrings.Count];
                for (int i = 0, end = expressionStrings.Count; i < end; ++i)
                    l[i] = new Expr(expressionStrings[i], i);
                return l;
            }
        }
        internal static List<string> BridgeWords = new()
        {
            "to",
            ">"
        };

        private static TwoWayConversion[] ParseExpr(Expr expr, TimeSpan regexTimeout)
        {
            List<TwoWayConversion> l = new();
            //                                         v  kywd arr v            v   digit arr  v
            var matches = Regex.Matches(expr.String, "({[ ,a-zA-Z]+}|[,a-zA-Z]+|{[ ,\\-\\.\\d]+}|[,\\-\\.\\d]+)", RegexOptions.Compiled, regexTimeout);
            //                                                       ^^^^^^^^^^ kywd             ^^^^^^^^^^^^^ digit

            List<Unit>? inUnits = null, outUnits = null;
            List<double>? inValues = null;

            //Unit? inUnit = null, outUnit = null;
            //double inVal = double.NaN;
            bool foundBridge = false;

            foreach (Match m in matches)
            {
                bool hasArrayBegin = m.Value.Contains('{');
                bool hasArrayEnd = m.Value.Contains('}');

                if (hasArrayBegin && !hasArrayEnd) throw new FormatException($"Specifier \"{m.Value}\" contains an array begin character ('{{'), but does not have an array end character ('}}')!");
                else if (!hasArrayBegin && hasArrayEnd) throw new FormatException($"Specifier \"{m.Value}\" contains an array end character ('}}'), but does not have an array begin character ('{{')!");

                if (hasArrayBegin && hasArrayEnd)
                { // m is an array:
                    var arrayElements = m.Value.Trim('{', '}').Trim().Split(',');

                    bool hasDigit = false;
                    bool hasAlpha = false;

                    foreach (string str in arrayElements)
                    {
                        string s = str.Trim();
                        if ((hasAlpha && !s.All(char.IsLetter)) || (hasDigit && !s.All(c => char.IsDigit(c) || c.EqualsAny('-', '.'))))
                        { // Previous array elements indicate that this is a unit array but we encountered digit chars
                            throw new FormatException($"Invalid array specifier \"{m.Value}\"; arrays cannot contain both units and values! ({s})");
                        }
                        else
                        { // Figure out what type of array we're dealing with
                            foreach (char c in s)
                            {
                                if (char.IsDigit(c) || c.EqualsAny('.', '-'))
                                    hasDigit = true;
                                else if (char.IsLetter(c))
                                    hasAlpha = true;
                            }
                        }
                    }

                    if (hasDigit)
                    { // values:
                        if (inValues is not null) throw new ArgumentOutOfRangeException("Input Value", arrayElements, $"Expression \"{expr.String}\" already specified a value!");
                        inValues = new();
                        arrayElements.ForEach(s => inValues.Add(Convert.ToDouble(s)));
                    }
                    else if (hasAlpha)
                    { // units:
                        if (inUnits is null)
                        {
                            inUnits = new();
                            arrayElements.ForEach(s =>
                            {
                                if (Unit.FromString(s) is Unit u)
                                    inUnits.Add(u);
                                else throw new ArgumentOutOfRangeException("Input Unit", s, $"Invalid unit \"{s}\" in array specifier \"{m.Value}\"; no units with that name exist!");
                            });
                        }
                        else if (outUnits is null)
                        {
                            outUnits = new();
                            arrayElements.ForEach(s =>
                            {
                                if (Unit.FromString(s) is Unit u)
                                    outUnits.Add(u);
                                else throw new ArgumentOutOfRangeException("Output Unit", s, $"Invalid unit \"{s}\" in array specifier \"{m.Value}\"; no units with that name exist!");
                            });
                        }
                        else throw new FormatException($"Expression \"{expr.String}\" contains too many unit specifiers for the number of value specifiers! (Expected Ratio:  2:1)");
                    }
                    // empty:
                    else throw new FormatException($"Invalid array specifier \"{m.Value}\"; arrays cannot be empty!");
                }
                else
                { // m is NOT an array:
                    if (m.Value.All(c => char.IsDigit(c) || c.EqualsAny('-', '.')))
                    {
                        if (inValues is not null) throw new ArgumentOutOfRangeException("Input Value", m.Value, $"Expression \"{m.Value}\" already specified a value!");
                        inValues = new() { Convert.ToDouble(m.Value) };
                    }
                    else if (m.Value.EqualsAny(BridgeWords, StringComparison.OrdinalIgnoreCase))
                    {
                        if (foundBridge) throw new FormatException($"Duplicate control specifier \"{m.Value}\"!");
                        else if (inUnits is null) throw new FormatException($"Invalid control specifier \"{m.Value}\"; control specifiers cannot appear before the input unit(s)!");
                        else if (outUnits is not null) throw new FormatException($"Invalid control specifier \"{m.Value}\"; control specifiers cannot appear after the output unit(s)!");
                        foundBridge = true;
                    }
                    else if (inUnits is null)
                    {
                        if (Unit.FromString(m.Value) is Unit u)
                            inUnits = new() { u };
                        else throw new ArgumentOutOfRangeException("Input Unit", m.Value, $"Invalid unit specifier \"{m.Value}\"; no units with that name exist!");
                    }
                    else if (outUnits is null)
                    {
                        if (Unit.FromString(m.Value) is Unit u)
                            outUnits = new() { u };
                        else throw new ArgumentOutOfRangeException("Output Unit", m.Value, $"Invalid unit specifier \"{m.Value}\"; no units with that name exist!");
                    }
                    else throw new FormatException($"Unexpected unit specifier \"{m.Value}\"; expression already specified input & output unit(s)!");
                }

                if (inUnits is not null && outUnits is not null && inValues is not null)
                {
                    l.AddRange(TwoWayConversion.Expand(inUnits, outUnits, inValues));
                    inUnits = null;
                    outUnits = null;
                    inValues = null;
                }
            }

            return l.ToArray();
        }

        private static readonly object ParseExpressionListLock = new();

        /// <summary>
        /// Parses any number of expressions in <paramref name="text"/> using regular expressions.
        /// </summary>
        /// <param name="text">Any number of conversion expressions.<br/>Expressions are composed of 3 parts:
        /// <list type="bullet">
        /// <item><term><b>Input Unit</b></term><description> The unit to convert from, which is a contiguous string composed of any alphabetic characters. The input unit is always the <b>first</b> string of alphabetic characters; it can appear before or after the value.</description></item>
        /// <item><term><b>Value</b></term><description> The value to convert, which is a contiguous string composed of the following characters: "0123456789.-"</description></item>
        /// <item><term><b>Output Unit</b></term><description> The unit to convert to, which is detected identically to the input unit. The output unit is always the <b>second</b> string in the expression, and may appear before or after the value, but <b>always after the input unit</b>. Additionally, the output unit <b>must be seperated from the input unit</b>, either with digits (the value) or with any number of whitespace characters.</description></item>
        /// </list>Any number of different expressions may appear within the string.</param>
        /// <param name="regexTimeout">The maximum amount of time to wait for the regular expression engine to find a match before cancelling the search.</param>
        /// <returns>An array of all of the valid expressions parsed from within <paramref name="text"/>; if none were found, the array is empty.</returns>
        public static TwoWayConversion[] ParseExpression(string text, TimeSpan regexTimeout)
        {
            List<TwoWayConversion> l = new();

            var expressionList = Expr.FromExpressionStrings(text.Split(';'));

            expressionList.AsParallel().ForAll((expr) =>
            {
                var conversions = ParseExpr(expr, regexTimeout);

                if (conversions.Length > 0)
                {
                    lock (ParseExpressionListLock)
                    {
                        l.AddRange(conversions);
                    }
                }
            });

            return l.ToArray();
        }
        /// <inheritdoc cref="ParseExpression(string, TimeSpan)"/>
        /// <remarks>If the <paramref name="text"/> parameter is not provided, the value of the <see cref="Text"/> property is used instead.</remarks>
        private TwoWayConversion[] ParseExpressionText(string? text = null) => ParseExpression(text ?? Text ?? string.Empty, ParseExpressionTimeout);

        /// <summary>
        /// Commit the current <see cref="Text"/> to the parser, but do not reset it.
        /// </summary>
        /// <param name="notify"><see langword="true"/> notifies of property changes by triggering the <see cref="PropertyChanged"/> event; <see langword="false"/> does not. Note that WPF data bindings will not detect the changes when this is <see langword="false"/>.<br/>Note that the event is only triggered if <see cref="Conversions"/> was actually updated.</param>
        /// <returns>The number of expressions parsed from the current value of the <see cref="Text"/> property, or <see langword="null"/> if the <see cref="Text"/> property was null.</returns>
        public int? Commit(bool notify = true)
        {
            if (Text is null)
            {
                if (_conversions is null) return null; //< don't do anything
                _conversions = null;
            }
            else
            {
                _conversions = ParseExpressionText();
            }
            if (notify)
            {
                NotifyPropertyChanged(nameof(Conversions));
            }
            return _conversions?.Length;
        }

        /// <summary>
        /// Resets the expression builder instance by setting the <see cref="Text"/> &amp; <see cref="Conversions"/> properties to <see langword="null"/>.
        /// </summary>
        /// <param name="notify"><see langword="true"/> notifies of property changes by triggering the <see cref="PropertyChanged"/> event; <see langword="false"/> does not. Note that WPF data bindings will not detect the changes when this is <see langword="false"/>.</param>
        public void Reset(bool notify = true)
        {
            _conversions = null;
            _text = null;
            if (notify)
            {
                NotifyPropertyChanged(nameof(Text));
                NotifyPropertyChanged(nameof(Conversions));
            }
        }

        #region Properties
        private static LogWriter Log => FLog.Log;
        public TimeSpan ParseExpressionTimeout { get; set; } = TimeSpan.FromSeconds(5);
        private string? _text;
        public string? Text
        {
            get => _text;
            set
            {
                if (value == _text) return;

                _text = value;
                _conversions = null;

                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Conversions));
            }
        }
        private TwoWayConversion[]? _conversions = null;
        public TwoWayConversion[]? Conversions => Text is null ? null : (_conversions ??= ParseExpressionText());
        #endregion Properties

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new(propertyName));
        #endregion Events
    }
}
