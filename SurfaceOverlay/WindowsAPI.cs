using System;
using System.Runtime.InteropServices;

// Sources:
// https://stackoverflow.com/questions/12591896/disable-wpf-window-focus
// https://dzone.com/articles/sending-keys-other-apps
// https://stackoverflow.com/questions/38774139/show-touch-keyboard-tabtip-exe-in-windows-10-anniversary-edition
// https://github.com/1j01/node-ahk/blob/e58609f3386c248362593353f7d38e8e65fbb0fa/IronAHK/Rusty/Windows/Input.cs#L8

namespace SurfaceOverlay
{
    public static class WindowsAPI
    {
        public static void SetWindowNotFocusable(IntPtr hwnd)
        {
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_NOACTIVATE);
        }
        public static void SendCtrlV()
        {
            SendModifierPlusKey(LCONTROL, V);
        }

        public static void SendCtrlC()
        {
            SendModifierPlusKey(LCONTROL, C);
        }

        public static void SendCtrlF()
        {
            SendModifierPlusKey(LCONTROL, F);
        }

        public static void SendCtrlZ()
        {
            SendModifierPlusKey(LCONTROL, Z);
        }

        public static void SendWinJ()
        {
            SendModifierPlusKey(LWIN, J);
        }
        public static void SendWinSpace()
        {
            SendModifierPlusKey(LWIN, SPACE);
        }

        public static void ToggleVirtualKeyboard()
        {
            var uiHostNoLaunch = new UIHostNoLaunch();
            var tipInvocation = (ITipInvocation)uiHostNoLaunch;
            tipInvocation.Toggle(GetDesktopWindow());
            Marshal.ReleaseComObject(uiHostNoLaunch);
        }

        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [ComImport, Guid("4ce576fa-83dc-4F88-951c-9d0782b4e376")]
        private class UIHostNoLaunch
        {
        }

        [ComImport, Guid("37c994e7-432b-4834-a2f7-dce1f13b834b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface ITipInvocation
        {
            void Toggle(IntPtr hwnd);
        }

        [DllImport("user32.dll", SetLastError = false)]
        private static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct HARDWAREINPUT
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        private struct INPUT
        {
            public uint type;
            public INPUTDATA i;
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct INPUTDATA
        {
            [FieldOffset(0)]
            public MOUSEINPUT m;

            [FieldOffset(0)]
            public KEYBDINPUT k;

            [FieldOffset(0)]
            public HARDWAREINPUT h;
        }

        private const int INPUT_KEYBOARD = 1;

        private const int KEYEVENTF_EXTENDEDKEY = 1;
        private const int KEYEVENTF_KEYUP = 2;
        private const int KEYEVENTF_UNICODE = 4;
        private const int KEYEVENTF_KEYDOWN = 0;

        private const ushort LCONTROL = 0xA2;
        private const ushort LWIN = 0x5B;
        private const ushort C = 0x43;
        private const ushort F = 0x46;
        private const ushort J = 0x4A;
        private const ushort V = 0x56;
        private const ushort Z = 0x5A;
        private const ushort SPACE = 0x20;

        private static void SendModifierPlusKey(ushort modifier, ushort key)
        {
            INPUT[] data = new INPUT[] {
                new INPUT()
                {
                    type = INPUT_KEYBOARD,
                    i = new INPUTDATA
                    {
                        k = new KEYBDINPUT
                        {
                            wVk = modifier,
                            wScan = 0,
                            dwFlags = KEYEVENTF_KEYDOWN,
                            dwExtraInfo = GetMessageExtraInfo(),
                        }
                    }
                },
                new INPUT()
                {
                    type = INPUT_KEYBOARD,
                    i = new INPUTDATA
                    {
                        k = new KEYBDINPUT
                        {
                            wVk = key,
                            wScan = 0,
                            dwFlags = KEYEVENTF_KEYDOWN,
                            dwExtraInfo = GetMessageExtraInfo(),
                        }
                    }
                },
                new INPUT()
                {
                    type = INPUT_KEYBOARD,
                    i = new INPUTDATA
                    {
                        k = new KEYBDINPUT
                        {
                            wVk = key,
                            wScan = 0,
                            dwFlags = KEYEVENTF_KEYUP,
                            dwExtraInfo = GetMessageExtraInfo(),
                        }
                    }
                },
                new INPUT()
                {
                    type = INPUT_KEYBOARD,
                    i = new INPUTDATA
                    {
                        k = new KEYBDINPUT
                        {
                            wVk = modifier,
                            wScan = 0,
                            dwFlags = KEYEVENTF_KEYUP,
                            dwExtraInfo = GetMessageExtraInfo(),
                        }
                    }
                }
            };
            SendInput((uint)data.Length, data, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
