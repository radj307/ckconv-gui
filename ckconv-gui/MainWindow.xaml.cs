using ckconv_gui.Measurement;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            default: break;
            }
        }
        private void commandBoxCommitButton_Click(object sender, RoutedEventArgs e)
        {
            string text = commandBox.Text;
            if (text.Length == 0 || text.All(char.IsWhiteSpace))
                return;
            else if (commandBox.GetBindingExpression(TextBox.TextProperty) is BindingExpression bExpr)
            {
                try
                {
                    bExpr.UpdateSource();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Couldn't parse expression '{text}' due to an exception!\n\n{ex.Message}", "Invalid Expression", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button b)
            {
                var item = b.CommandParameter;
                Conversions.Remove(item);
            }
        }
    }
}
