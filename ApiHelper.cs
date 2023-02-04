using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Anti.LockScreen
{
    public class ApiHelper
    {
        #region Win32 API keybd_event & GetLastInputInfo
        [DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(byte bVk, //虚拟键值
            byte bScan, // 一般为0
            int dwFlags, //这里是整数类型  0 为按下，2为释放
            int dwExtraInfo //这里是整数类型 一般情况下设成为 0
        );
        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public uint dwTime;
        }
        [DllImport("user32.dll")]
        public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        public static uint GetLastInputTick()
        {
            LASTINPUTINFO vLastInputInfo = new LASTINPUTINFO();
            vLastInputInfo.cbSize = Marshal.SizeOf(vLastInputInfo);
            if (!GetLastInputInfo(ref vLastInputInfo))
                return 0;
            return vLastInputInfo.dwTime;
        }
        #endregion Win32 API keybd_event & GetLastInputInfo
        #region WTSRegisterSessionNotification
        [DllImport("WtsApi32.dll")]
        public static extern bool WTSRegisterSessionNotification(IntPtr hWnd, [MarshalAs(UnmanagedType.U4)]int dwFlags);
        [DllImport("WtsApi32.dll")]
        public static extern bool WTSUnRegisterSessionNotification(IntPtr hWnd);
        public const int NOTIFY_FOR_THIS_SESSION = 0;
        public const int WM_WTSSESSION_CHANGE = 0x2b1;
        public const int WTS_SESSION_LOCK = 0x7;
        public const int WTS_SESSION_UNLOCK = 0x8;
        public static bool IsSessionLocked = false;
        public static void OnWtsSessionChange(ref Message m)
        {
            if (m.Msg == WM_WTSSESSION_CHANGE) {
                var v = m.WParam.ToInt32();
                if (v == WTS_SESSION_LOCK) {
                    Log("WTS_SESSION_LOCK");
                    IsSessionLocked = true;
                } else if (v == WTS_SESSION_UNLOCK) {
                    IsSessionLocked = false;
                    Log("WTS_SESSION_UNLOCK");
                }
            }
        }
        #endregion WTSRegisterSessionNotification
        #region Win32 Registry Operation
        public const string AppRegKey = "SOFTWARE\\Anti.LockScreen";
        public const string KeyName_Setting = "";
        public const string KeyName_Handle = "H";
        public const string KeyName_Minimize = "M";
        public static string RegReadKey(string KeyName, string DefVal)
        {
            var Key = Registry.CurrentUser.CreateSubKey(AppRegKey);
            return Key.GetValue(KeyName, DefVal) as string;
        }
        public static void RegWriteKey(string KeyName, string Val)
        {
            var Key = Registry.CurrentUser.CreateSubKey(AppRegKey);
            Key.SetValue(KeyName, Val);
        }
        #endregion Win32 Registry Operation
        #region TryActivePreInstance
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        public static void TryActivePreInstance()
        {
            var strH = RegReadKey(KeyName_Handle, "");
            if (!string.IsNullOrEmpty(strH)) {
                var H = new IntPtr(long.Parse(strH));
                Log("Active Pre-Instance");
                ShowWindow(H, 5);
                SetForegroundWindow(H);
            }
        }
        #endregion TryActivePreInstance
        #region AppInit & Exit
        public static string OnAppInit(IntPtr Handle)
        {
            RegWriteKey(KeyName_Handle, Handle.ToInt64().ToString());
            WTSRegisterSessionNotification(Handle, NOTIFY_FOR_THIS_SESSION);
            return RegReadKey(KeyName_Setting, "4,11");
        }
        public static void OnAppExit(IntPtr Handle, string SettingStr)
        {
            RegWriteKey(KeyName_Setting, SettingStr);
            RegWriteKey(KeyName_Handle, "");
            WTSUnRegisterSessionNotification(Handle);
        }
        #endregion AppInit & Exit
        public static void Log(string Msg)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("{0} {1}", DateTime.Now.ToLongTimeString(), Msg));
        }

        public static bool StartupMinimize
        {
            get {
                var v = RegReadKey(KeyName_Minimize, "0");
                return v == "1";
            }
            set {
                RegWriteKey(KeyName_Minimize, value ? "1" : "0");
            }
        }
    }
}
