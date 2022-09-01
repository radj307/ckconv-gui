using ckconv_gui.Measurement.Systems;
using ImportedWPF.Collections;

namespace ckconv_gui.Helpers
{
    public class MeasurementSystemSelectorViewModel
    {
        public ObservableImmutableList<MeasurementSystem> Systems { get; } = new()
        {
            new MetricSystem(),
            new CreationKitSystem(),
            new ImperialSystem(),
        };
    }
}
