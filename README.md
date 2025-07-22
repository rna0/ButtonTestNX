# ButtonTestNX

![psx](https://github.com/user-attachments/assets/50c1ee74-d93d-4444-8805-f86dfdf6d3c8)

**ButtonTestNX is a C# Cross-platform Key testing software**

*This is a personal open-source project with the scope to learn about Xaml and cross platform controller mapping*

ButtonTestNX Currently only uses one external dependency and it is [Avalonia-UI](https://avaloniaui.net/).

At the moment the following is implemented:
- Full UI Features
- Avalonia for Cross-platform UI
- Full Keyboard detection (Except Tab)
- Full Gamepad detection (XInput)
- Button Press 5 times to exit

What is not implemented (but should be...):
- Button mapping for XInput controllers

> **Note:**  The Only cross platform XInput Button mapper Library in c# is [HIDDevices](https://github.com/DevDecoder/HIDDevices). this means Controller input will not be supported in Mac devices, see [this issue](https://github.com/DevDecoder/HIDDevices/issues/2) for more details.

## Compatibility

- Windows-x86
- Windows-x64
- Linux-x64
- Mac-x64
- Mac-arm64

## Using the Program

1. Extract the file the latest [release](https://github.com/rna0/ButtonTestNX/releases) and open it.
2. press any key to see it on the screen.

## Screenshots
<p align="center" width="100%">
    <img width="45%" src="https://github.com/user-attachments/assets/2ed3bbd7-6a6e-437e-a426-692d4a48134a"> 
    <img width="45%" src="https://github.com/user-attachments/assets/13f50208-2d51-4b47-ac51-647c562fe3b6"> 
</p>

## Quick Faq
- Where did you get the idea for the program?

  **Answer:** By observing the implementation of the Test Input Devices program for the Nintendo Switch

- What Platforms are supported?

  **Answer:** Windows, Linux and Mac, no other Platforms are planned currently
