using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ckconv_gui.Measurement
{
    [DoNotNotify]
    public class ExpressionBuilder : INotifyPropertyChanged
    {
        public Conversion[] ParseExpression(string text)
        {
            const string rgx = "([\\.\\-0-9]+|[a-z\'\"]+)\\s*([\\.\\-0-9]+|[a-z\'\"]+)\\s+([\\.\\-0-9]+|[a-z\'\"]+){0,1}";
            var matches = Regex.Matches(text, rgx, RegexOptions.Compiled | RegexOptions.IgnoreCase, ParseExpressionTimeout);

            List<Conversion> l = new();

            foreach (Match match in matches)
            {
                if (!match.Success) continue;

                GroupCollection groups = match.Groups;

                Unit? inU = null, outU = null;
                decimal? val = null;

                bool foundInU = false, foundOutU = false, foundVal = false;

                for (int i = 1; i < groups.Count; ++i)
                {
                    Group group = groups[i];

                    if (group.Value.Length <= 0) continue;

                    if (group.Value.All(char.IsDigit))
                    {
                        if (foundVal)
                            throw new Exception($"An expression cannot have multiple input values! (\"{val}\" & \"{group.Value}\")");
                        val = Convert.ToDecimal(group.Value);
                        foundVal = true;
                    }
                    else if (!foundInU)
                    {
                        inU = ConversionAPI.GetUnit(group.Value);
                        foundInU = true;
                    }
                    else if (!foundOutU)
                    {
                        outU = ConversionAPI.GetUnit(group.Value);
                        foundOutU = true;
                    }
                    else throw new Exception($"Expression specifies too many units! (\"{inU?.FullName}\", \"{outU?.FullName}\", \"{group.Value}\")");
                }

                if (inU is null) throw new Exception("No input unit specified!");
                else if (val is null) throw new Exception("No input value specified!");
                else if (outU is null) throw new Exception("No output unit specified!");
                else l.Add(new Conversion()
                {
                    InUnit = inU,
                    InValue = val.Value,
                    OutUnit = outU,
                });
            }

            return l.ToArray();
        }

        public void Reset()
        {
            _conversions = null;
            _text = null;
            NotifyPropertyChanged(nameof(Text));
            NotifyPropertyChanged(nameof(Conversions));
        }

        #region Properties
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
        private Conversion[]? _conversions = null;
        public Conversion[]? Conversions => Text is null ? null : (_conversions ??= ParseExpression(Text));
        #endregion Properties

        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new(propertyName));
        #endregion Events
    }
}
