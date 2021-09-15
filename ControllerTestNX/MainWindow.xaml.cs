using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ControllerTestNX
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Border keyBorder = new()
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(0, 0, 5, 0),
                Padding = new Thickness(2, 0, 2, 0)
            };
            TextBlock singleButtonPress = new()
            {
                Text = e.Key.ToString(),
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };
            keyBorder.Child = singleButtonPress;
            ButtonsStack.Children.Insert(0, keyBorder);
            const int maxCount = 40;
            if (ButtonsStack.Children.Count == maxCount)
            {
                ButtonsStack.Children.RemoveAt(maxCount - 1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}