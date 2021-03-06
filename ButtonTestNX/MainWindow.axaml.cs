using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace ButtonTestNX
{
    public partial class MainWindow : Window
    {
        private const int TimesPressToExit = 5;
        private const int MaxCount = 40;
        private static string OnExitButtonKey = "";
        private static int OnExitButtonCount;
        
        private static StackPanel ButtonsStack = null!;
        private static TextBlock PressTimesToExitText = null!;

        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            ButtonsStack = this.FindControl<StackPanel>("ButtonsStackPanel");
            PressTimesToExitText = this.FindControl<TextBlock>("PressTimesToExitTextBlock");
            this.FindControl<Window>("ButtonTestWindow");
            
            FontFamily = new FontFamily("avares://ButtonTestNX/Assets/Fonts#Nintendo Switch UI");
            PressTimesToExitText.Text = $"Press any button {TimesPressToExit} times to end the test.";
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
            if (ButtonsStack.Children.Count == MaxCount)
            {
                ButtonsStack.Children.RemoveAt(MaxCount - 1);
            }
        }

        private static void ExitCheckXTimes(string currentKey)
        {
            if (currentKey != OnExitButtonKey)
            {
                OnExitButtonKey = currentKey;
                OnExitButtonCount = 1;
            }
            else if (++OnExitButtonCount == TimesPressToExit)
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