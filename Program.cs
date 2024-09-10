using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Timers;

namespace LightGun
{
    static class Program
    {

        private static System.Timers.Timer _timer;
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private static LowLevelMouseProc _mouseProc = MouseHookCallback;
        private static LowLevelKeyboardProc _keyboardProc = KeyboardHookCallback;
        private static IntPtr _mouseHookID = IntPtr.Zero;
        private static IntPtr _keyboardHookID = IntPtr.Zero;


     
        

        private static bool ctrlPressed = false;
        private static bool shiftPressed = false;

        [STAThread]
        static void Main()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = false;
            _timer.Enabled = true;
            _timer.Stop();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            _mouseHookID = SetMouseHook(_mouseProc);
            _keyboardHookID = SetKeyboardHook(_keyboardProc);
            Application.Run(new Form1());
            UnhookWindowsHookEx(_mouseHookID);
            UnhookWindowsHookEx(_keyboardHookID);


        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            
            SendKeys.SendWait(Form1.holdOutside);
        }

        private static IntPtr SetMouseHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
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
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
           /* if (nCode >= 0)
            {
                if (wParam == (IntPtr)WM_LBUTTONDOWN)
                {

                    if (Form1.movable && Form1.outSideOfScreen)
                    {
                        SendKeys.Send(Form1.clickOutside);
                        _timer.Stop();
                        _timer.Start();
                    }
                }
                else if (wParam == (IntPtr)WM_LBUTTONUP)
                {
                    _timer.Stop();
                }
            }*/
            return CallNextHookEx(_mouseHookID, nCode, wParam, lParam);
        }
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
                        Form1.OpenBorder();
                     
                    }
                    if (shiftPressed && key == Keys.B)
                    {

                       Form1.StartStop();


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
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

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