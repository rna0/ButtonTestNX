using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ControllerTestNX
{
    public partial class MainWindow
    {
        private const int TIMES_PRESS_TO_EXIT = 5;
        private const int MAX_COUNT = 40;
        private static string OnExitButtonKey = "";
        private static int OnExitButtonCount;

        public MainWindow()
        {
            InitializeComponent();
            AddTimesToExitComponentText();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var currentKey = e.Key.ToString();
            ExitCheckXTimes(currentKey);
            CreateKeyTextComponent(currentKey);
            RemoveKeyTextComponentLeftovers();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ExitApplication();
        }

        private void AddTimesToExitComponentText()
        {
            PressTimesToExitText.Text = $"Press any button {TIMES_PRESS_TO_EXIT} times to end the test.";
        }

        private void CreateKeyTextComponent(string currentKey)
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
                Text = currentKey,
                FontSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)
            };

            keyBorder.Child = singleButtonPress;
            ButtonsStack.Children.Insert(0, keyBorder);
        }

        private void RemoveKeyTextComponentLeftovers()
        {
            if (ButtonsStack.Children.Count == MAX_COUNT)
            {
                ButtonsStack.Children.RemoveAt(MAX_COUNT - 1);
            }
        }

        private static void ExitCheckXTimes(string currentKey)
        {
            if (currentKey != OnExitButtonKey)
            {
                OnExitButtonKey = currentKey;
                OnExitButtonCount = 1;
            }
            else if (++OnExitButtonCount == TIMES_PRESS_TO_EXIT)
            {
                ExitApplication();
            }
        }

        private static void ExitApplication()
        {
            Environment.Exit(0);
        }
    }
}