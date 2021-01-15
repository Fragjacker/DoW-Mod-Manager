// Decompiled with JetBrains decompiler
// Type: SSNoFog.FogRemover
// Assembly: SSNoFog, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: ABA6371C-50C2-412D-8152-53D605E42626
// Assembly location: D:\THQ\Dawn of War - Soulstorm\SSNoFog.exe
// This was decompiled from the "SSNoFog.exe" made by the russian DoW modding community.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace SSNoFog
{
    public static class FogRemover
    {
        private static readonly byte[] nope6 = new byte[6]
        {
            (byte) 144,
            (byte) 144,
            (byte) 144,
            (byte) 144,
            (byte) 144,
            (byte) 144
        };
        private static readonly byte[] float512 = new byte[4]
        {
            (byte) 0,
            (byte) 0,
            (byte) 0,
            (byte) 68
        };
        private static readonly byte[] codeF512 = new byte[4]
        {
            (byte) 0,
            (byte) 0,
            (byte) 192,
            (byte) 66
        };
        private static readonly byte[] jmpFog = new byte[6]
        {
            (byte) 217,
            (byte) 129,
            (byte) 96,
            (byte) 12,
            (byte) 0,
            (byte) 0
        };
        private static readonly byte[] jmpMapSkyDistance = new byte[6]
        {
            (byte) 217,
            (byte) 155,
            (byte) 112,
            (byte) 12,
            (byte) 0,
            (byte) 0
        };
        private const int PROCESS_WM_READ = 16;
        private const int PROCESS_WM_WRITE = 32;
        private const int PROCESS_VM_OPERATION = 8;
        private const int PAGE_EXECUTE_READWRITE = 64;
        private const int fogAddress12 = 7624048;
        private const int float512Address12 = 8796952;
        private const int mapSkyDistanceAddress12 = 7631050;
        private const int fogAddressSteam = 8553200;
        private const int float512AddressSteam = 11490504;
        private const int mapSkyDistanceAddressSteam = 8561466;

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(
          int dwDesiredAccess,
          bool bInheritHandle,
          int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(
          IntPtr hProcess,
          int lpBaseAddress,
          byte[] lpBuffer,
          int dwSize,
          out int lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool WriteProcessMemory(
          IntPtr hProcess,
          int lpBaseAddress,
          byte[] lpBuffer,
          int dwSize,
          out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll")]
        private static extern bool VirtualProtectEx(
          IntPtr hProcess,
          int lpAddress,
          int dwSize,
          int flNewProtect,
          out int lpflOldProtect);

        public static void DisableFog(Process process)
        {
            FogRemover.DisableFog(process, 7624048, 8796952, 7631050);
            FogRemover.DisableFog(process, 8553200, 11490504, 8561466);
        }

        private static void DisableFog(
            Process process,
            int fogAddress,
            int float512Address,
            int mapSkyDistanceAddress)
        {
            IntPtr num = FogRemover.OpenProcess(56, false, process.Id);
            try
            {
                FogRemover.CheckToggleMemory(fogAddress, FogRemover.jmpFog, FogRemover.nope6, num);
                FogRemover.CheckToggleMemory(float512Address, FogRemover.codeF512, FogRemover.float512, num);
                FogRemover.CheckToggleMemory(mapSkyDistanceAddress, FogRemover.jmpMapSkyDistance, FogRemover.nope6, num);
            }
            finally
            {
                FogRemover.CloseHandle(num);
            }
        }

        private static bool CheckToggleMemory(
          int addr,
          byte[] checkVal,
          byte[] setVal,
          IntPtr pHandle)
        {
            byte[] lpBuffer = new byte[checkVal.Length];
            int lpNumberOfBytesRead;
            if (!FogRemover.ReadProcessMemory(pHandle, addr, lpBuffer, lpBuffer.Length, out lpNumberOfBytesRead) || lpNumberOfBytesRead != lpBuffer.Length || !((IEnumerable<byte>)lpBuffer).SequenceEqual<byte>((IEnumerable<byte>)checkVal))
                return false;
            int lpflOldProtect;
            FogRemover.VirtualProtectEx(pHandle, addr, setVal.Length, 64, out lpflOldProtect);
            int num1;
            int num2 = FogRemover.WriteProcessMemory(pHandle, addr, setVal, setVal.Length, out num1) ? 1 : 0;
            FogRemover.VirtualProtectEx(pHandle, addr, setVal.Length, lpflOldProtect, out num1);
            return num2 != 0;
        }
    }
}
