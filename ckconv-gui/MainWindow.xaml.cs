using System;
using System.Windows;

namespace ckconv_gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void OnWindowStateChanged(object? sender, EventArgs e)
        {
            switch (WindowState)
            {
            case WindowState.Minimized:
                Hide();
                break;
            case WindowState.Normal:
                Show();
                break;
            default: break;
            }
        }
    }
}
