using ckconv_gui.Measurement.Enum;
using ImportedWPF.Collections;

namespace ckconv_gui.Measurement.Interface
{
    public interface ISystem
    {
        EMeasurementSystem System { get; }
        ObservableImmutableList<Unit> Units { get; }
    }
}
