﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Meccha
{
    public class KeyboardHook : IDisposable
    {
        private const int WH_KEYBOARD_LL = 13; // lowlevel keyboard hook
        private const int WM_KEYDOWN = 0x0100; // keydown event
        private const int WM_KEYUP = 0x0101; // keyup event

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int hCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr hookId; // the id of our hook
        private LowLevelKeyboardProc hookCb; // so it doesnt get garbage cleaned, we need to store

        public List<Keys> KeysDown = new List<Keys>(); // no repeats, but may also be useful to expose

        public void Hook()
        {
            hookCb = HookCallback;

            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                hookId = SetWindowsHookEx(WH_KEYBOARD_LL, hookCb, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public void Dispose()
        {
            UnhookWindowsHookEx(hookId);
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                int vk = Marshal.ReadInt32(lParam);
                Keys k = (Keys)vk;
                KeyInputEventArgs kiea = new KeyInputEventArgs(k);

                if (wParam == (IntPtr)WM_KEYUP)
                {
                    if (KeysDown.Contains(k))
                        KeysDown.Remove(k);
                    OnKeyUp(kiea);
                }
                else if (wParam == (IntPtr)WM_KEYDOWN && !KeysDown.Contains(k))
                {
                    KeysDown.Add(k);
                    OnKeyDown(kiea);
                }
            }

            return CallNextHookEx(hookId, nCode, wParam, lParam); // next hook in the chain, pass it on
        }

        public event KeyUpEventHandler KeyUp;
        public event KeyDownEventHandler KeyDown;

        public delegate void KeyUpEventHandler(object sender, KeyInputEventArgs e);
        public delegate void KeyDownEventHandler(object sender, KeyInputEventArgs e);

        protected virtual void OnKeyUp(KeyInputEventArgs e)
        {
            if (KeyUp != null)
                KeyUp(this, e);
        }
        protected virtual void OnKeyDown(KeyInputEventArgs e)
        {
            if (KeyDown != null)
                KeyDown(this, e);
        }

        public class KeyInputEventArgs : EventArgs
        {
            public Keys Key { get; private set; }

            public KeyInputEventArgs(Keys k)
            {
                Key = k;
            }
        }
    }
}