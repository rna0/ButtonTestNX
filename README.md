# ButtonTestNX

![psx](https://user-images.githubusercontent.com/47921907/133428480-cde6bd7e-519a-40c0-8669-433205552eda.png)

**ButtonTestNX is a C# Cross-platform Key testing software**

*This is a personal open-source project with the scope to learn about Xaml and cross platform controller mapping*

ButtonTestNX Currently only uses one external dependency and it is [Avalonia-UI](https://avaloniaui.net/).

At the moment the following is implemented:
- Full UI Features
- Avalonia for Cross-platform UI
- Full Keyboard detection (Except Tab)
- Button Press 5 times to exit

What is not implemented (but should be...):
- Button mapping for XInput controllers

> **Note:**  The Only cross platform XInput Button mapper Library in c# is [HIDDevices](https://github.com/DevDecoder/HIDDevices). this means Controller input will not be supported in Mac devices, see [this issue](https://github.com/DevDecoder/HIDDevices/issues/2) for more details.

## Compatibility

- Windows-x86
- Windows-x64
- Linux
- Mac

## Using the Program

1. Extract the file the latest [release](https://github.com/rna0/ButtonTestNX/releases) and open it.
2. press any key to see it on the screen.

## Screenshots
<p align="center" width="100%">
    <img width="45%" src="https://user-images.githubusercontent.com/47921907/133436220-982f9b24-93ec-4608-ae4a-ae5886545763.png"> 
    <img width="45%" src="https://user-images.githubusercontent.com/47921907/133436311-f9201ed4-d59f-4f26-a5f7-71e07e8ace8d.png"> 
</p>

## Quick Faq
- Why does it look like a the Button testing from the nintendo switch?

The Idea for this project came when I the one from the nintendo switch and wondered why there is no one for the PC, don't sue me nintendo...

- Why can't I see my controllers input?

This feature is not implemented yet
