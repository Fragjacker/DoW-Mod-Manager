// Decompiled with JetBrains decompiler
// Type: SSNoFog.FogRemover
// Assembly: SSNoFog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABA6371C-50C2-412D-8152-53D605E42626
// Assembly location: D:\THQ\Dawn of War - Soulstorm\SSNoFog.exe
// This was decompiled from the "SSNoFog.exe" made by the russian DoW modding community.
// And later this file was changed by IgorTheLight to be more human-friendly

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace SSUNIEXDLL
{
    public static class UNIEXDLLLoader
    {
        private static readonly byte[] checkVal          = new byte[4] {   8,   0,   0,   0 };
        private static readonly byte[] setVal            = new byte[4] {  10,   0,   0,   0 };

        private const int PAGE_EXECUTE_READWRITE = 0x40;

        private const int numberOfRacesAddressSteam = 0xC1F350;
 
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int dwSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtectEx(IntPtr hProcess, int lpAddress, int dwSize, int flNewProtect, out int lpflOldProtect);

        public static void IncreaseDefeatedRaceCount(Process process)
        {
            //ActuallyDisableFog(process, fogAddress12, float512Address12, mapSkyDistanceAddress12);
            ActuallyDisableFog(process, numberOfRacesAddressSteam);
        }

        private static void ActuallyDisableFog(Process process, int defeatedRacesAddress)
        {
            IntPtr pHandle = OpenProcess(56, false, process.Id);
            try
            {
                CheckToggleMemory(defeatedRacesAddress, pHandle);
                //CheckToggleMemory(float512Address, codeF512, float512, pHandle);
                //CheckToggleMemory(mapSkyDistanceAddress, jmpMapSkyDistance, nope6, pHandle);
            }
            finally
            {
                CloseHandle(pHandle);
            }
        }

        private static bool CheckToggleMemory(int addr, IntPtr pHandle)
        {
            byte[] lpBuffer = new byte[checkVal.Length];

            int lpNumberOfBytesRead, lpflOldProtect, _;

            if (!ReadProcessMemory(pHandle, addr, lpBuffer, lpBuffer.Length, out lpNumberOfBytesRead)
                || lpNumberOfBytesRead != lpBuffer.Length
                || !((IEnumerable<byte>)lpBuffer).SequenceEqual(checkVal))
                return false;

            VirtualProtectEx(pHandle, addr, setVal.Length, PAGE_EXECUTE_READWRITE, out lpflOldProtect);
            int returnCode = WriteProcessMemory(pHandle, addr, setVal, setVal.Length, out _) ? 1 : 0;
            
            VirtualProtectEx(pHandle, addr, setVal.Length, lpflOldProtect, out _);
            return returnCode != 0;
        }
    }
}
