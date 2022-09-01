using ckconv_gui.Measurement;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;

namespace ckconv_gui
{
    [JsonObject]
    internal class Settings : AppConfig.ConfigurationFile
    {
        #region Constructor
        public Settings() : base($"{Assembly.GetExecutingAssembly().FullName}.json") => PropertyChanged += HandlePropertyChanged;
        #endregion Constructor

        #region Properties
        public TwoWayConversionList ConversionList { get; set; } = new();
        #region Log
        /// <summary>
        /// Gets or sets whether the log is enabled or not.<br/>
        /// See <see cref="Log.SettingsInterface.EnableLogging"/>
        /// </summary>
        /// <remarks><b>Default: <see langword="true"/></b></remarks>
        public bool EnableLogging { get; set; } = true;
        /// <summary>
        /// Gets or sets the location of the log file.<br/>
        /// See <see cref="Log.SettingsInterface.LogPath"/>
        /// </summary>
        /// <remarks><b>Default: "VolumeControl.log"</b></remarks>
        public string LogPath { get; set; } = "VolumeControl.log";
        /// <summary>
        /// Gets or sets the <see cref="Log.Enum.EventType"/> filter used for messages.<br/>
        /// See <see cref="Log.SettingsInterface.LogFilter"/>
        /// </summary>
        /// <remarks><b>Default: <see cref="Log.Enum.EventType.ALL_EXCEPT_DEBUG"/></b></remarks>
        public Log.Enum.EventType LogFilter { get; set; } = Log.Enum.EventType.ALL_EXCEPT_DEBUG;
        /// <summary>
        /// Gets or sets whether the log is cleared when the program starts.<br/>
        /// See <see cref="Log.SettingsInterface.LogClearOnInitialize"/>
        /// </summary>
        /// <remarks><b>Default: <see langword="true"/></b></remarks>
        public bool LogClearOnInitialize { get; set; } = true;
        /// <summary>
        /// Gets or sets the format string used for timestamps in the log.<br/>
        /// See <see cref="Log.SettingsInterface.LogTimestampFormat"/>
        /// </summary>
        /// <remarks><b>Default: "HH:mm:ss:fff"</b></remarks>
        public string LogTimestampFormat { get; set; } = "HH:mm:ss:fff";
        /// <summary>
        /// 
        /// See <see cref="Log.SettingsInterface.LogEnableStackTrace"/>
        /// </summary>
        /// <remarks><b>Default: <see langword="true"/></b></remarks>
        public bool LogEnableStackTrace { get; set; } = true;
        /// <summary>
        /// 
        /// See <see cref="Log.SettingsInterface.LogEnableStackTraceLineCount"/>
        /// </summary>
        /// <remarks><b>Default: <see langword="true"/></b></remarks>
        public bool LogEnableStackTraceLineCount { get; set; } = true;
        #endregion Log
        #endregion Properties

        #region EventHandlers
        private void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e) => this.Save();
        #endregion EventHandlers
    }
}
