using Log;
using System;
using System.Windows;

namespace ckconv_gui
{
    public static class Program
    {
        private static readonly Settings _settings = new();
        private static LogWriter Log => FLog.Log;

        [STAThread]
        public static void Main()
        {
            _settings.Load();

            var app = new App();

            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex);
                MessageBox.Show($"The application crashed due to an unhandled exception!\n\n{ex.Message}", "Fatal Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }
    }
}
