using ckconv_gui.Measurement.Enum;
using ImportedWPF.Collections;

namespace ckconv_gui.Measurement.Interface
{
    public interface IMutableSystem : ISystem
    {
        new EMeasurementSystem System { get; set; }
        new ObservableImmutableList<Unit>? Units { get; set; }
    }
}
