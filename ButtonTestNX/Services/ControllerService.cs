using System;
using System.Collections.Generic;
using System.Linq;
using static SDL2.SDL;

namespace ButtonTestNX.Services;

public class ControllerService
{
    private readonly List<Controller?> _controllers = new();

    public event Action<int, string>? ControllerInputReceived;

    public void Initialize()
    {
        if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_GAMECONTROLLER) < 0)
        {
            Console.WriteLine($"SDL could not initialize! SDL_Error: {SDL_GetError()}");
            return;
        }
        FindAndOpenInitialControllers();
    }

    public void Shutdown()
    {
        foreach (var controller in _controllers)
        {
            controller?.Close();
        }
        _controllers.Clear();
        SDL_Quit();
        Console.WriteLine("Controller service stopped and SDL shut down.");
    }

    public void PollEvents()
    {
        while (SDL_PollEvent(out var e) != 0)
        {
            switch (e.type)
            {
                case SDL_EventType.SDL_CONTROLLERDEVICEADDED:
                    AddController(e.cdevice.which);
                    break;

                case SDL_EventType.SDL_CONTROLLERDEVICEREMOVED:
                    RemoveController(e.cdevice.which);
                    break;

                case SDL_EventType.SDL_CONTROLLERBUTTONDOWN:
                    FindControllerByInstanceId(e.cbutton.which)?.HandleButtonDown(e.cbutton);
                    break;

                case SDL_EventType.SDL_CONTROLLERAXISMOTION:
                    FindControllerByInstanceId(e.caxis.which)?.HandleAxisMotion(e.caxis);
                    break;
            }
        }
    }

    private void FindAndOpenInitialControllers()
    {
        for (var i = 0; i < SDL_NumJoysticks(); i++)
        {
            if (SDL_IsGameController(i) == SDL_bool.SDL_TRUE)
            {
                AddController(i);
            }
        }
    }

    private void AddController(int deviceIndex)
    {
        var playerSlotIndex = _controllers.FindIndex(c => c == null);

        if (playerSlotIndex == -1)
        {
            playerSlotIndex = _controllers.Count;
            _controllers.Add(null);
        }
        
        var playerIndexForUi = playerSlotIndex + 1;

        Controller? newController = null;
        try
        {
            newController = new Controller(deviceIndex, playerIndexForUi);
            
            if (FindControllerByInstanceId(newController.JoystickInstanceId) != null)
            {
                newController.Close();
                return;
            }

            newController.OnInput += OnControllerInput;
            _controllers[playerSlotIndex] = newController;
            Console.WriteLine($"[+] Controller {playerIndexForUi} connected: '{newController.Name}' (Instance ID: {newController.JoystickInstanceId})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add controller with device index {deviceIndex}: {ex.Message}");
            newController?.Close();
            if (playerSlotIndex < _controllers.Count)
            {
                _controllers[playerSlotIndex] = null;
            }
        }
    }

    private void RemoveController(int instanceId)
    {
        var playerSlotIndex = _controllers.FindIndex(c => c?.JoystickInstanceId == instanceId);

        if (playerSlotIndex != -1)
        {
            var controller = _controllers[playerSlotIndex];
            if (controller != null)
            {
                Console.WriteLine($"[-] Controller {controller.PlayerIndex} disconnected: '{controller.Name}'");
                controller.OnInput -= OnControllerInput;
                controller.Close();
            }
            
            _controllers[playerSlotIndex] = null;
        }
    }

    private void OnControllerInput(Controller controller, string inputDescription)
    {
        ControllerInputReceived?.Invoke(controller.PlayerIndex, inputDescription);
    }

    private Controller? FindControllerByInstanceId(int joystickInstanceId)
    {
        return _controllers.FirstOrDefault(c => c?.JoystickInstanceId == joystickInstanceId);
    }
}