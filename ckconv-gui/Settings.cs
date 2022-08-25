using System.Reflection;

namespace ckconv_gui
{
    internal class Settings : AppConfig.ConfigurationFile
    {
        public Settings() : base($"{Assembly.GetExecutingAssembly().FullName}.json") { }

        public Log.Enum.EventType LogFilter { get; set; } = Log.Enum.EventType.ALL_EXCEPT_DEBUG;
    }
}
