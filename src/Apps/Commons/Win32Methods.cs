﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Avv.Apps.Commons
{
    [ComVisible(false)]
    public static class Win32Methods
    {
        [DllImport("user32.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern uint SendMessage(IntPtr hWnd, uint wMsg, uint wParam, uint lParam);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        [ComVisible(false)]
        public static extern int FlashWindowEx(ref FLASHINFO f);

        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [ComVisible(false)]
        public static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [ComVisible(false)]
        public static extern int GetSysColor(int nIndex);

        [DllImport("user32", SetLastError = true)]
        [ComVisible(false)]
        public static extern int ShowScrollBar(IntPtr handle, int wBar, int bShow);

        [DllImport("user32.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern IntPtr SetActiveWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern int PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("wininet.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern bool InternetGetCookie(string lpszUrl, string lpszCookieName, StringBuilder lpCookieData, ref long lpdwSize);

        [DllImport("wininet.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern bool InternetSetCookie(string lpszUrl, string lpszCookieName, string lpCookieData);

        [DllImport("user32.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern bool SetSysColors(int cElements, int[] lpaElements, int[] lpaRgbValues);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        [ComVisible(false)]
        public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        [DllImport("user32.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern int RegisterHotKey(IntPtr HWnd, int ID, int MOD_KEY, int KEY);

        [DllImport("user32.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern int UnregisterHotKey(IntPtr HWnd, int ID);

        [DllImport("user32.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll", SetLastError = true)]
        [ComVisible(false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint flags);

        [DllImport("user32.dll", SetLastError = true)]
        [ComVisible(false)]
        public static extern bool GetKeyboardState(byte[] lpKeyState);
    }
}
