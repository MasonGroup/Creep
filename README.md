# Creep Virus 🕷️💻

## Description

Creep is a custom virus that showcases visual glitches on the screen using GDI (Graphics Device Interface) 🎨 and plays annoying sounds 🔊 in an infinite loop. The virus manipulates the system's Master Boot Record (MBR) 🛠️ and eventually forces a Blue Screen of Death (BSOD) 💀. It also adds chaos by randomly moving the mouse cursor 🖱️, simulating a hacked environment with the message "YOU HAVE BEEN HACKED" displayed in red text on the screen.

The virus was developed for educational purposes by two individuals:
- **Mattia** from 🇮🇹 Italy
- **Abolhb** from 🇸🇦 Al Kharj

Both developers are part of the same group known as **FREEMASONRY** 🔑. This project showcases their collaboration, combining knowledge from different fields to create a unique and disruptive program.

## GDI Glitch Preview 📺🌀

![GDI Glitch Preview](https://i.ibb.co/jkv2XQ7/image.png)

## How It Works ⚙️

1. **Visual Glitches**: The virus continuously uses `BitBlt` to distort the screen by offsetting portions of the display, creating a glitch-like effect. 🌀
   
2. **Cursor Manipulation**: The virus prevents user input by blocking the input and randomly moving the mouse cursor in small, jittery movements. 🖱️

3. **MBR Manipulation**: The virus writes random data to the system's Master Boot Record (MBR), corrupting it and rendering the system unbootable. ⚠️

4. **Audio Disturbance**: The virus generates annoying sounds using bytebeat audio formulas and plays them in a loop. These formulas synthesize harsh, chaotic sounds, contributing to the unsettling effect. 🔊

5. **BSOD Trigger**: After two minutes of running, the virus targets system processes, specifically `svchost`, and terminates them, which can lead to a Blue Screen of Death (BSOD). 💀

### Code Breakdown 📝

- **`BitBlt` Screen Glitches**: The GDI function `BitBlt` is used to apply glitches by copying screen sections and distorting their positions.
- **`BlockInput` and `SetCursorPos`**: These functions control the mouse, preventing user interaction and causing the cursor to jitter around. 
- **`MasonMBR()`**: This function corrupts the Master Boot Record (MBR) by writing random bytes to it, making the system unbootable upon restart.
- **`MasonBSOD()`**: This function attempts to kill all instances of `svchost`, which is a critical process, forcing a system crash (BSOD).
- **Bytebeat Audio**: The class `Mattia` generates continuous audio loops using bytebeat formulas that produce chaotic and unpleasant sounds.

## Important Disclaimer ⚠️

This code is **for educational purposes only**. The creators, **Mattia** and **Abolhb**, do not condone the use of this virus for any malicious purposes. 🚫 Misuse of this code could cause serious damage to systems, including permanent data loss. 💾 Use it responsibly and **at your own risk**.

Both developers are members of **FREEMASONRY** 🔑, a group that emphasizes knowledge and collaboration. However, they are not responsible for any damages or legal issues arising from the misuse of this code. Always obtain permission before running code on someone else's system. 🛑
