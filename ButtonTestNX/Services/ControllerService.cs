using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static SDL2.SDL;

namespace ButtonTestNX.Services;

public class ControllerService
{
    private readonly Dictionary<int, Controller> _controllers = new();
    private int _nextPlayerIndex = 1;
    private CancellationTokenSource? _cts;

    public event Action<int, string>? ControllerInputReceived;

    public void Start()
    {
        _cts = new CancellationTokenSource();
        Task.Run(() => PollEvents(_cts.Token));
    }

    public void Stop()
    {
        _cts?.Cancel();
        _cts?.Dispose();
    }

    private void PollEvents(CancellationToken token)
    {
        if (SDL_Init(SDL_INIT_VIDEO | SDL_INIT_GAMECONTROLLER) < 0)
        {
            Console.WriteLine($"SDL could not initialize! SDL_Error: {SDL_GetError()}");
            return;
        }

        FindAndOpenInitialControllers();

        while (!token.IsCancellationRequested)
        {
            while (SDL_PollEvent(out SDL_Event e) != 0)
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
                        if (_controllers.TryGetValue(e.cbutton.which, out var btnCtrl))
                        {
                            btnCtrl.HandleButtonDown(e.cbutton);
                        }

                        break;

                    case SDL_EventType.SDL_CONTROLLERAXISMOTION:
                        if (_controllers.TryGetValue(e.caxis.which, out var axisCtrl))
                        {
                            axisCtrl.HandleAxisMotion(e.caxis);
                        }

                        break;
                }
            }

            SDL_Delay(16);
        }

        foreach (var controller in _controllers.Values)
        {
            Console.WriteLine($"[-] Controller {controller.PlayerIndex} disconnected on shutdown: '{controller.Name}'");
            controller.Close();
        }

        _controllers.Clear();
        SDL_Quit();
        Console.WriteLine("Controller service stopped and SDL shut down.");
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
        Controller? tempController = null;
        try
        {
            tempController = new Controller(deviceIndex, _nextPlayerIndex);

            if (_controllers.ContainsKey(tempController.JoystickInstanceId))
            {
                tempController.Close();
                return;
            }

            tempController.OnInput += OnControllerInput;
            _controllers.Add(tempController.JoystickInstanceId, tempController);

            Console.WriteLine(
                $"[+] Controller {_nextPlayerIndex} connected: '{tempController.Name}' (Instance ID: {tempController.JoystickInstanceId})");
            _nextPlayerIndex++;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to add controller with device index {deviceIndex}: {ex.Message}");
            tempController?.Close();
        }
    }

    private void RemoveController(int instanceId)
    {
        if (_controllers.TryGetValue(instanceId, out var controller))
        {
            Console.WriteLine($"[-] Controller {controller.PlayerIndex} disconnected: '{controller.Name}'");
            controller.OnInput -= OnControllerInput;
            controller.Close();
            _controllers.Remove(instanceId);
        }
    }

    private void OnControllerInput(Controller controller, string inputDescription)
    {
        ControllerInputReceived?.Invoke(controller.PlayerIndex, inputDescription);
    }
}