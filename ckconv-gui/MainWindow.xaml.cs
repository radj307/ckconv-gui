using System;
using System.ComponentModel;
using System.Linq;
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

            ExpressionBuilder.PropertyChanged += HandleExpressionBuilderPropertyChanged;
        }

        private ExpressionBuilder ExpressionBuilder => (FindResource("exprBuilder") as ExpressionBuilder)!;
        private void HandleExpressionBuilderPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is ExpressionBuilder exprBuilder && (e.PropertyName?.Equals(nameof(ExpressionBuilder.Conversions)) ?? false))
            {
                if (exprBuilder.Conversions is not null)
                {
                    Conversions.AddRange(exprBuilder.Conversions.AsEnumerable());
                    exprBuilder.Reset();
                }
            }
        }

        private ConversionList Conversions => (this.FindResource("Conversions") as ConversionList)!;

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

        private void commandBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
            case System.Windows.Input.Key.Enter:
                window.Focus();
                break;
            default:break;
            }
        }
    }
}
