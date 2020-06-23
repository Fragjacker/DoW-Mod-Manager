using System;
using System.Runtime.InteropServices;
using System.Security;

namespace DoW_Mod_Manager
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TimerCaps
    {
        public uint PeriodMin;
        public uint PeriodMax;
        public uint PeriodCurrent;
    };

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

        /// <summary>TimeBeginPeriod(). See the Windows API documentation for details.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage"), SuppressUnmanagedCodeSecurity]
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
        public static extern uint TimeBeginPeriod(uint uMilliseconds);

        /// <summary>TimeEndPeriod(). See the Windows API documentation for details.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage"), SuppressUnmanagedCodeSecurity]
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod", SetLastError = true)]
        public static extern uint TimeEndPeriod(uint uMilliseconds);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage"), SuppressUnmanagedCodeSecurity]
        [DllImport("powrprof.dll")]
        public static extern uint PowerGetActiveScheme(IntPtr UserRootPowerKey, ref IntPtr ActivePolicyGuid);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage"), SuppressUnmanagedCodeSecurity]
        [DllImport("powrprof.dll")]
        public static extern uint PowerSetActiveScheme(IntPtr UserRootPowerKey, [MarshalAs(UnmanagedType.LPStruct)] Guid SchemeGuid);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage"), SuppressUnmanagedCodeSecurity]
        [DllImport("powrprof.dll")]
        public static extern uint PowerReadACValue(IntPtr RootPowerKey, ref Guid SchemeGuid, ref Guid SubGroupOfPowerSettingGuid, ref Guid PowerSettingGuid, ref int Type, ref int Buffer, ref uint BufferSize);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage"), SuppressUnmanagedCodeSecurity]
        [DllImport("powrprof.dll")]
        public static extern uint PowerWriteACValueIndex(IntPtr RootPowerKey, [MarshalAs(UnmanagedType.LPStruct)] Guid SchemeGuid, [MarshalAs(UnmanagedType.LPStruct)] Guid SubGroupOfPowerSettingsGuid, [MarshalAs(UnmanagedType.LPStruct)] Guid PowerSettingGuid, int AcValueIndex);
    }
}