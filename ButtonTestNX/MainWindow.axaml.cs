using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;
using ButtonTestNX.Services;

namespace ButtonTestNX;

public partial class MainWindow : Window
{
    private const int TimesPressToExit = 5;
    private const int MaxCount = 40;

    // --- Services and State (now instance-based) ---
    private readonly ControllerService _controllerService;
    private readonly Random _random = new();
    private readonly IReadOnlyList<Color> _playerColors;

    private string _onExitButtonKey = "";
    private int _onExitButtonCount;

    public MainWindow()
    {
        InitializeComponent();
        
        _playerColors = new List<Color>
        {
            Color.Parse("#2E2E2E"),
            Colors.DodgerBlue,
            Colors.Crimson,
            Colors.LimeGreen,
            Colors.Gold,
            Colors.DarkOrchid,
            Colors.Orange,
            Colors.Turquoise,
            Colors.HotPink,
            Colors.SlateGray,
            Colors.Brown
        };

        _controllerService = new ControllerService();
        _controllerService.ControllerInputReceived += OnControllerInput;
        _controllerService.Start();

        // --- Set Initial UI State ---
        ProcessInput(0, "Keyboard");
        PressTimesToExitTextBlock.Text = $"Press any button {TimesPressToExit} times to end the test.";
        ButtonsStackPanel.Children.Clear();
    }

    // Centralized input processing for both Keyboard and Controllers
    private void ProcessInput(int playerIndex, string inputKey)
    {
        // 1. Determine the color for the current input source
        var color = (playerIndex < _playerColors.Count)
            ? _playerColors[playerIndex]
            : Color.FromRgb((byte)_random.Next(256), (byte)_random.Next(256), (byte)_random.Next(256));

        // 2. Check for exit condition
        if (inputKey == "Keyboard") return;
        ExitCheckXTimes(inputKey);
        CreateKeyTextComponent(inputKey, color);
        RemoveKeyTextComponentLeftovers();
    }

    private void OnControllerInput(int playerIndex, string inputDescription)
    {
        Dispatcher.UIThread.Post(() => ProcessInput(playerIndex, inputDescription));
    }

    private void Window_KeyDown(object? sender, KeyEventArgs e)
    {
        ProcessInput(0, e.Key.ToString());
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        ExitApplication();
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        _controllerService.Stop();
        base.OnClosing(e);
    }

    private void CreateKeyTextComponent(string currentKey, Color keyColor)
    {
        var keyBorder = new Border
        {
            Background = new SolidColorBrush(keyColor),
            CornerRadius = new CornerRadius(100),
            Margin = new Thickness(0, 0, 5, 0),
            Padding = new Thickness(8, 2, 8, 12)
        };

        var singleButtonPress = new TextBlock
        {
            Text = currentKey,
            Foreground = Brushes.White,
            FontSize = 20,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        keyBorder.Child = singleButtonPress;
        ButtonsStackPanel.Children.Insert(0, keyBorder);
    }

    private void RemoveKeyTextComponentLeftovers()
    {
        if (ButtonsStackPanel.Children.Count > MaxCount)
        {
            ButtonsStackPanel.Children.RemoveAt(MaxCount);
        }
    }

    private void ExitCheckXTimes(string currentKey)
    {
        if (currentKey != _onExitButtonKey)
        {
            _onExitButtonKey = currentKey;
            _onExitButtonCount = 1;
        }
        else if (++_onExitButtonCount >= TimesPressToExit)
        {
            ExitApplication();
        }
    }

    private static void ExitApplication()
    {
        Environment.Exit(0);
    }
}