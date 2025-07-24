using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Timers;

namespace LightGun
{
    static class Program
    {


        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private static LowLevelKeyboardProc _keyboardProc = KeyboardHookCallback;
        private static IntPtr _keyboardHookID = IntPtr.Zero;
        private static bool ctrlPressed = false;
        private static bool shiftPressed = false;
        static MainWindow mainWindow = new MainWindow();

        [STAThread]
        static void Main()
        {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            ApplicationConfiguration.Initialize();
            _keyboardHookID = SetKeyboardHook(_keyboardProc);
            mainWindow = new MainWindow();
            Application.Run(mainWindow);
            

            UnhookWindowsHookEx(_keyboardHookID);


        }
  

        private static IntPtr SetKeyboardHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_KEYUP))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;

                if (wParam == (IntPtr)WM_KEYDOWN)
                {

                    if (key == Keys.LControlKey)
                    {
                        ctrlPressed = true;

                    }
                    if (key == Keys.LShiftKey)
                    {

                        shiftPressed = true;

                    }

                    if (ctrlPressed && key == Keys.B)
                    {
                        mainWindow.OpenBorder();

                    }
                    if (shiftPressed && key == Keys.D1)
                    {

                        mainWindow.StartStopP1(null, null);
                    }
                    if (shiftPressed && key == Keys.D2)
                    {

                        mainWindow.StartStopP2(null,null);
                    }
                }
                else if (wParam == (IntPtr)WM_KEYUP)
                {
                    if (key == Keys.LControlKey)
                        ctrlPressed = false;
                    if (key == Keys.LShiftKey)
                    {
                        shiftPressed = false;

                    }
                }
            }
            return CallNextHookEx(_keyboardHookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);




    }
}