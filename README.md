# ButtonTestNX

![psx](https://github-production-user-asset-6210df.s3.amazonaws.com/47921907/468623592-1f0d0990-2811-4fac-99c8-085ad7b11efa.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250721%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250721T125908Z&X-Amz-Expires=300&X-Amz-Signature=845f6b75e3cf0e06be677b583d2b854531b3259f2d9bafb39eaff1df4beaadf6&X-Amz-SignedHeaders=host)

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
- Linux
- Mac

## Using the Program

1. Extract the file the latest [release](https://github.com/rna0/ButtonTestNX/releases) and open it.
2. press any key to see it on the screen.

## Screenshots
<p align="center" width="100%">
    <img width="45%" src="https://github-production-user-asset-6210df.s3.amazonaws.com/47921907/468639212-f57c9c1f-21bc-4846-b5af-329f6d618b78.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250721%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250721T133438Z&X-Amz-Expires=300&X-Amz-Signature=e41ea583694cbd2897d01a0e414b20454cc8eb455fc4b1824c8d78256e34f596&X-Amz-SignedHeaders=host"> 
    <img width="45%" src="https://github-production-user-asset-6210df.s3.amazonaws.com/47921907/468640461-c8cec7ff-d74a-47fc-a6c1-fd329842adc4.png?X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Credential=AKIAVCODYLSA53PQK4ZA%2F20250721%2Fus-east-1%2Fs3%2Faws4_request&X-Amz-Date=20250721T133657Z&X-Amz-Expires=300&X-Amz-Signature=a9a966e7465844671044fee23bda85c1770b828369edcbc540cc5a545992d3fa&X-Amz-SignedHeaders=host"> 
</p>

## Quick Faq
- Where did you get the idea for the program?

  **Answer:** By observing the implementation of the Test Input Devices program for the Nintendo Switch

- What Platforms are supported?

  **Answer:** Windows, Linux and Mac, no other Platforms are planned currently
- About ARM support?

  **Answer:** The program may work for arm but without a Device to check I can't tell for sure
