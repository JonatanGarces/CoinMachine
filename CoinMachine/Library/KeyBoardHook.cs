﻿using Microsoft.Win32;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Library
{
    public class KeyBoardHook : IDisposable
    {
        private bool Global = false;

        public delegate void LocalKeyEventHandler(Keys key, bool Shift, bool Ctrl, bool Alt);

        public event LocalKeyEventHandler KeyDown;

        public event LocalKeyEventHandler KeyUp;

        public delegate int CallbackDelegate(int Code, int W, int L);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct KBDLLHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern int SetWindowsHookEx(HookType idHook, CallbackDelegate lpfn, int hInstance, int threadId);

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern bool UnhookWindowsHookEx(int idHook);

        [DllImport("user32", CallingConvention = CallingConvention.StdCall)]
        private static extern int CallNextHookEx(int idHook, int nCode, int wParam, int lParam);

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern int GetCurrentThreadId();

        public enum HookType : int
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        private int HookID = 0;
        private CallbackDelegate TheHookCB = null;

        //Start hook


            public void Start()
        {
            TheHookCB = new CallbackDelegate(KeybHookProc);
            if (Global)
            {
                HookID = SetWindowsHookEx(HookType.WH_KEYBOARD_LL, TheHookCB,
                    0, //0 for local hook. eller hwnd til user32 for global
                    0); //0 for global hook. eller thread for hooken
            }
            else
            {
                HookID = SetWindowsHookEx(HookType.WH_KEYBOARD, TheHookCB,
                    0, //0 for local hook. or hwnd to user32 for global
                    GetCurrentThreadId()); //0 for global hook. or thread for the hook
            }
        }
        public KeyBoardHook(bool Global)
        {
            this.Global = Global;
            
        }

        private bool IsFinalized = false;

        ~KeyBoardHook()
        {
            if (!IsFinalized)
            {
                UnhookWindowsHookEx(HookID);
                IsFinalized = true;
            }
        }

        public void Dispose()
        {
            if (!IsFinalized)
            {
                UnhookWindowsHookEx(HookID);
                IsFinalized = true;
            }
        }

        //The listener that will trigger events
        private int KeybHookProc(int Code, int W, int L)
        {
            KBDLLHookStruct LS = new KBDLLHookStruct();
            if (Code < 0)
            {
                if (CoinMachine.Global.Instance.KeyEnabled)
                {
                    return CallNextHookEx(HookID, Code, W, L);
                }
                else
                {
                    return 1;
                }
            }
            try
            {
                if (!Global)
                {
                    if (Code == 3)
                    {
                        IntPtr ptr = IntPtr.Zero;

                        int keydownup = L >> 30;
                        if (keydownup == 0)
                        {
                            if (KeyDown != null) KeyDown((Keys)W, GetShiftPressed(), GetCtrlPressed(), GetAltPressed());
                        }
                        if (keydownup == -1)
                        {
                            if (KeyUp != null) KeyUp((Keys)W, GetShiftPressed(), GetCtrlPressed(), GetAltPressed());
                        }
                        //System.Diagnostics.Debug.WriteLine("Down: " + (Keys)W);vb ttvb
                    }
                }
                else
                {
                    KeyEvents kEvent = (KeyEvents)W;

                    int vkCode = Marshal.ReadInt32((IntPtr)L); //Leser vkCode som er de første 32 bits hvor L peker.

                    if (kEvent != KeyEvents.KeyDown && kEvent != KeyEvents.KeyUp && kEvent != KeyEvents.SKeyDown && kEvent != KeyEvents.SKeyUp)
                    {
                    }
                    if (kEvent == KeyEvents.KeyDown || kEvent == KeyEvents.SKeyDown)
                    {
                        if (KeyDown != null) KeyDown((Keys)vkCode, GetShiftPressed(), GetCtrlPressed(), GetAltPressed());
                    }
                    if (kEvent == KeyEvents.KeyUp || kEvent == KeyEvents.SKeyUp)
                    {
                        if (KeyUp != null) KeyUp((Keys)vkCode, GetShiftPressed(), GetCtrlPressed(), GetAltPressed());
                    }
                }
            }
            catch (Exception)
            {
                //Ignore all errors...
            }

            if (CoinMachine.Global.Instance.KeyEnabled)
            {
                return CallNextHookEx(HookID, Code, W, L);
            }
            else
            {
                return 1;
            }
        }

        public enum KeyEvents
        {
            KeyDown = 0x0100,
            KeyUp = 0x0101,
            SKeyDown = 0x0104,
            SKeyUp = 0x0105
        }

        [DllImport("user32.dll")]
        static public extern short GetKeyState(Keys nVirtKey);

        public static bool GetCapslock()
        {
            return Convert.ToBoolean(GetKeyState(Keys.CapsLock)) & true;
        }

        public static bool GetNumlock()
        {
            return Convert.ToBoolean(GetKeyState(Keys.NumLock)) & true;
        }

        public static bool GetScrollLock()
        {
            return Convert.ToBoolean(GetKeyState(Keys.Scroll)) & true;
        }

        public static bool GetShiftPressed()
        {
            int state = GetKeyState(Keys.ShiftKey);
            if (state > 1 || state < -1) return true;
            return false;
        }

        public static bool GetCtrlPressed()
        {
            int state = GetKeyState(Keys.ControlKey);
            if (state > 1 || state < -1) return true;
            return false;
        }

        public static bool GetAltPressed()
        {
            int state = GetKeyState(Keys.Menu);
            if (state > 1 || state < -1) return true;
            return false;
        }

        private void DisableTaskManager()
        {
            RegistryKey regkey = default;
            string keyValueInt = "1";
            string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
            try
            {
                regkey = Registry.CurrentUser.CreateSubKey(subKey);
                regkey.SetValue("DisableTaskMgr", keyValueInt);
                regkey.Close();
            }
            catch (Exception ex)
            {
                // Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, "Registry Error!");
            }
        }

        public void EnableTaskManager()
        {
            RegistryKey regkey = default;
            string keyValueInt = "0";
            //0x00000000 (0)
            string subKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
            try
            {
                regkey = Registry.CurrentUser.CreateSubKey(subKey);
                regkey.SetValue("DisableTaskMgr", keyValueInt);
                regkey.Close();
            }
            catch (Exception ex)
            {
                //Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, "Registry Error!");
            }
        }
    }
}