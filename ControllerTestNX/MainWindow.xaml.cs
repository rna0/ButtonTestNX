using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ControllerTestNX
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

		private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
            Border keyBborder = new()
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(0, 0, 5, 0),
                Padding = new Thickness(5)
            };
            TextBlock SingleButtonPress = new()
			{
				Text = e.Key.ToString(),
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };
            keyBborder.Child = SingleButtonPress;
            ButtonsStack.Children.Add(keyBborder);
		}
	}
}