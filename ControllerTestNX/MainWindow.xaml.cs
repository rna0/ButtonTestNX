using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ControllerTestNX
{
    public partial class MainWindow : Window
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
                Padding = new Thickness(5)
            };
            TextBlock singleButtonPress = new()
            {
                Text = e.Key.ToString(),
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };
            keyBorder.Child = singleButtonPress;
            ButtonsStack.Children.Add(keyBorder);
        }
    }
}