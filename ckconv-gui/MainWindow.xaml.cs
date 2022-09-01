using ckconv_gui.Measurement;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

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
            ExpressionBuilder.PropertyChanged -= HandleExpressionBuilderPropertyChanged;
            if (sender is ExpressionBuilder exprBuilder && (e.PropertyName?.Equals(nameof(ExpressionBuilder.Conversions)) ?? false))
            {
                if (exprBuilder.Conversions is not null && exprBuilder.Conversions.Length > 0)
                {
                    Conversions.AddRange(exprBuilder.Conversions.AsEnumerable());
                    exprBuilder.Reset();
                }
            }
            ExpressionBuilder.PropertyChanged += HandleExpressionBuilderPropertyChanged;
        }

        private static Settings Settings => (Settings.Default as Settings)!;
        private TwoWayConversionList Conversions => (FindResource("Conversions") as TwoWayConversionList)!;

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
            if (ExpressionBuilder.Text is null) return;
            string text = commandBox.Text;

            try
            {
                if (commandBox.GetBindingExpression(TextBox.TextProperty) is BindingExpression bExpr)
                {
                    bExpr.UpdateSource();
                }
                if (ExpressionBuilder.Commit() is int parsedCount && parsedCount > 0)
                {
                    ExpressionBuilder.Reset();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Couldn't parse expression '{text}' due to an exception!\n\n{ex.Message}", "Invalid Expression", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
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

        private void TreeView_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Key.Equals(Key.Enter)) return;

            if (sender is TreeView tv)
            {
                if (tv.SelectedItem is Unit selectedUnit)
                {
                    if (commandBox.Text.Length != 0) commandBox.Text += ' ';
                    commandBox.Text += (selectedUnit.Symbol.Length > 0 ? selectedUnit.Symbol : selectedUnit.GetFullName(false));
                }
            }
        }

        private void TreeView_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!e.ChangedButton.Equals(MouseButton.Left)) return;

            if (sender is TreeView tv)
            {
                if (tv.SelectedItem is Unit selectedUnit)
                {
                    if (commandBox.Text.Length != 0) commandBox.Text += ' ';
                    commandBox.Text += (selectedUnit.Symbol.Length > 0 ? selectedUnit.Symbol : selectedUnit.GetFullName(false));
                }
            }
        }
    }
}
