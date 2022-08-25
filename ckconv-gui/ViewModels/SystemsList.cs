using ckconv_gui.Measurement.Enum;
using ImportedWPF.Collections;
using System;

namespace ckconv_gui.ViewModels
{
    internal class SystemsList
    {
        public ObservableImmutableList<EMeasurementSystem> Systems { get; } = new(Enum.GetValues<EMeasurementSystem>());
    }
}
