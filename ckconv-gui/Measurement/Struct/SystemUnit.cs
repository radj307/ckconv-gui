using ckconv_gui.Measurement.Enum;

namespace ckconv_gui.Measurement.Struct
{
    public struct SystemUnit
    {
        public EMeasurementSystem System { get; set; }
        public double UnitCF { get; set; }
        public string Name { get; set; }
        public string? Symbol { get; set; }
    }
}
