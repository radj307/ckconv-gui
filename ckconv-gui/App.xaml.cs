using ImportedWPF.Collections;
using ImportedWPF.Controls;
using Log;
using System;
using System.ComponentModel;
using System.Windows;

namespace ckconv_gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // setup the tray icon
            TrayIcon = new(() => this.MainWindow.Visibility == Visibility.Visible)
            {
                Tooltip = $"ckconv-gui"
            };
            TrayIcon.DoubleClick += this.HandleTrayIconClick;
            TrayIcon.ShowClicked += this.HandleTrayIconClick;
            TrayIcon.HideClicked += (s, e) => this.HideMainWindow();
            TrayIcon.BringToFrontClicked += (s, e) => this.ActivateMainWindow();
            TrayIcon.CloseClicked += (s, e) => this.Shutdown();
            TrayIcon.Visible = true;
        }

        #region Fields
        public readonly VolumeControlNotifyIcon TrayIcon;
        #endregion Fields

        #region Properties
        private static LogWriter Log => FLog.Log;
        private static Settings Settings => (Settings.Default as Settings)!;
        #endregion Properties


        private void LogEventTypeFilter_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName?.Equals("Value", StringComparison.Ordinal) ?? false)
            {
                Settings.LogFilter = (this.FindResource("EventTypeOptions") as BindableEventType)!.Value;
            }
        }
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            // delete the tray icon
            TrayIcon.Dispose();
        }
        private void HideMainWindow() => this.MainWindow.Hide();
        private void ShowMainWindow()
        {
            this.MainWindow.Show();
            this.MainWindow.WindowState = WindowState.Normal;
        }
        private void ActivateMainWindow()
        {
            this.MainWindow.Show();
            _ = this.MainWindow.Activate();
        }
        private void HandleTrayIconClick(object? sender, EventArgs e)
        {
            if (this.MainWindow.IsVisible)
                this.HideMainWindow();
            else
                this.ShowMainWindow();
        }
    }
}
