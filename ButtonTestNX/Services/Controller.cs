using System;
using static SDL2.SDL;

namespace ButtonTestNX.Services;

/// <summary>
/// Represents a single game controller, handling its own input and state.
/// </summary>
public class Controller
{
    private const int JoystickDeadZone = 8000;

    private class AxisState
    {
        public bool LeftStickUp, LeftStickDown, LeftStickLeft, LeftStickRight;
        public bool RightStickUp, RightStickDown, RightStickLeft, RightStickRight;
        public bool LeftTrigger, RightTrigger;
    }

    private readonly IntPtr _controllerHandle;
    private readonly AxisState _axisState = new();

    public int PlayerIndex { get; }
    public int JoystickInstanceId { get; }
    public string Name => SDL_GameControllerName(_controllerHandle);

    public event Action<Controller, string>? OnInput;

    public Controller(int deviceIndex, int playerIndex)
    {
        _controllerHandle = SDL_GameControllerOpen(deviceIndex);
        if (_controllerHandle == IntPtr.Zero)
        {
            throw new InvalidOperationException($"Could not open game controller {deviceIndex}: {SDL_GetError()}");
        }

        IntPtr joystick = SDL_GameControllerGetJoystick(_controllerHandle);
        JoystickInstanceId = SDL_JoystickInstanceID(joystick);
        PlayerIndex = playerIndex;
    }

    public void Close()
    {
        if (_controllerHandle != IntPtr.Zero)
        {
            SDL_GameControllerClose(_controllerHandle);
        }
    }

    public void HandleButtonDown(SDL_ControllerButtonEvent buttonEvent)
    {
        var button = (SDL_GameControllerButton)buttonEvent.button;
        OnInput?.Invoke(this, GetButtonName(button));
    }

    public void HandleAxisMotion(SDL_ControllerAxisEvent axisEvent)
    {
        var axis = (SDL_GameControllerAxis)axisEvent.axis;
        var value = axisEvent.axisValue;

        switch (axis)
        {
            case SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX:
                UpdateStickState(value, ref _axisState.LeftStickLeft, ref _axisState.LeftStickRight, "L-Stick ←", "L-Stick →");
                break;
            case SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTY:
                UpdateStickState(value, ref _axisState.LeftStickUp, ref _axisState.LeftStickDown, "L-Stick ↑", "L-Stick ↓");
                break;
            case SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTX:
                UpdateStickState(value, ref _axisState.RightStickLeft, ref _axisState.RightStickRight, "R-Stick ←", "R-Stick →");
                break;
            case SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTY:
                UpdateStickState(value, ref _axisState.RightStickUp, ref _axisState.RightStickDown, "R-Stick ↑", "R-Stick ↓");
                break;
            case SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERLEFT:
                UpdateTriggerState(value, ref _axisState.LeftTrigger, "ZL");
                break;
            case SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERRIGHT:
                UpdateTriggerState(value, ref _axisState.RightTrigger, "ZR");
                break;
        }
    }

    private void UpdateStickState(short value, ref bool negativeState, ref bool positiveState, string negativeMessage,
        string positiveMessage)
    {
        if (value < -JoystickDeadZone)
        {
            if (!negativeState)
            {
                OnInput?.Invoke(this, negativeMessage);
                negativeState = true;
            }
        }
        else
        {
            negativeState = false;
        }

        if (value > JoystickDeadZone)
        {
            if (!positiveState)
            {
                OnInput?.Invoke(this, positiveMessage);
                positiveState = true;
            }
        }
        else
        {
            positiveState = false;
        }
    }

    private void UpdateTriggerState(short value, ref bool state, string message)
    {
        if (value > JoystickDeadZone)
        {
            if (!state)
            {
                OnInput?.Invoke(this, message);
                state = true;
            }
        }
        else
        {
            state = false;
        }
    }

    private static string GetButtonName(SDL_GameControllerButton button) => button switch
    {
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A => "A",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B => "B",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X => "X",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y => "Y",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_BACK => "-",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_START => "+",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSTICK => "L-Stick",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSTICK => "R-Stick",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSHOULDER => "L",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSHOULDER => "R",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP => "D-Pad ↑",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN => "D-Pad ↓",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT => "D-Pad ←",
        SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT => "D-Pad →",
        _ => button.ToString()
    };
}