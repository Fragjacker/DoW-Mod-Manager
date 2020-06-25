using System;
using System.Runtime.InteropServices;

namespace DoW_Mod_Manager
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct TimerCaps
    {
        public uint PeriodMin;
        public uint PeriodMax;
        public uint PeriodCurrent;
    };

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SHELLEXECUTEINFO
    {
        public int cbSize;
        public uint fMask;
        public IntPtr hwnd;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpVerb;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpFile;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpParameters;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpDirectory;
        public int nShow;
        public IntPtr hInstApp;
        public IntPtr lpIDList;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string lpClass;
        public IntPtr hkeyClass;
        public uint dwHotKey;
        public IntPtr hIcon;
        public IntPtr hProcess;
    }

    public static class WinApiCalls
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern NtStatus NtQueryTimerResolution(out uint MinimumResolution, out uint MaximumResolution, out uint ActualResolution);

        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern NtStatus NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);

        public static TimerCaps QueryTimerResolution()
        {
            var caps = new TimerCaps();
            var result = NtQueryTimerResolution(out caps.PeriodMin, out caps.PeriodMax, out caps.PeriodCurrent);
            return caps;
        }

        public static ulong SetTimerResolution(uint timerResolutionIn100nsUnits, bool doSet = true)
        {
            uint currentRes = 0;
            var result = NtSetTimerResolution(timerResolutionIn100nsUnits, doSet, ref currentRes);
            return currentRes;
        }

        [DllImport("winmm.dll", EntryPoint = "TimeBeginPeriod", SetLastError = true)]
        public static extern uint TimeBeginPeriod(uint uMilliseconds);

        [DllImport("winmm.dll", EntryPoint = "TimeEndPeriod", SetLastError = true)]
        public static extern uint TimeEndPeriod(uint uMilliseconds);

        [DllImport("powrprof.dll", EntryPoint = "PowerGetActiveScheme")]
        public static extern uint PowerGetActiveScheme(IntPtr UserRootPowerKey, ref IntPtr ActivePolicyGuid);

        [DllImport("powrprof.dll", EntryPoint = "PowerSetActiveScheme")]
        public static extern uint PowerSetActiveScheme(IntPtr UserRootPowerKey, ref Guid ActivePolicyGuid);

        [DllImport("powrprof.dll", EntryPoint = "PowerReadACValue")]
        public static extern uint PowerReadACValue(IntPtr RootPowerKey, ref Guid SchemeGuid, ref Guid SubGroupOfPowerSettingGuid, ref Guid PowerSettingGuid, ref int Type, ref int Buffer, ref uint BufferSize);

        [DllImport("powrprof.dll", EntryPoint = "PowerWriteACValueIndex")]
        public static extern uint PowerWriteACValueIndex(IntPtr RootPowerKey, [MarshalAs(UnmanagedType.LPStruct)] Guid SchemeGuid, [MarshalAs(UnmanagedType.LPStruct)] Guid SubGroupOfPowerSettingsGuid, [MarshalAs(UnmanagedType.LPStruct)] Guid PowerSettingGuid, int AcValueIndex);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        private const int SW_SHOW = 5;
        private const uint SEE_MASK_INVOKEIDLIST = 12;
        public static bool ShowFileProperties(string Filename)
        {
            SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
            info.cbSize = Marshal.SizeOf(info);
            info.lpVerb = "properties";
            info.lpFile = Filename;
            info.nShow = SW_SHOW;
            info.fMask = SEE_MASK_INVOKEIDLIST;
            return ShellExecuteEx(ref info);
        }
    }
}