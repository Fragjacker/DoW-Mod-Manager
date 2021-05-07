// DLL injection function available https://github.com/ihack4falafel/DLL-Injection
// Made by ihack4falafel

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Text;
using DoW_Mod_Manager;

namespace SSUNIEXDLL
{
    public static class UNIEXDLLLoader
    {
        private static readonly byte[] checkVal = new byte[4] { 8, 0, 0, 0 };
        private static readonly byte[] setVal = new byte[4] { 10, 0, 0, 0 };

        private const int PAGE_EXECUTE_READWRITE = 0x40;

        private const int numberOfRacesAddressSteam = 0xC1F350;

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(
        ProcessAccessFlags processAccess,
        bool bInheritHandle,
        int processId);
        public static IntPtr OpenProcess(Process proc, ProcessAccessFlags flags)
        {
            return OpenProcess(flags, false, proc.Id);
        }

        // VirtualAllocEx signture https://www.pinvoke.net/default.aspx/kernel32.virtualallocex
        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        // VirtualFreeEx signture  https://www.pinvoke.net/default.aspx/kernel32.virtualfreeex
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress,
        int dwSize, AllocationType dwFreeType);

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            IntPtr dwSize,
            AllocationType flAllocationType,
            MemoryProtection flProtect);

        // WriteProcessMemory signture https://www.pinvoke.net/default.aspx/kernel32/WriteProcessMemory.html
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
        IntPtr hProcess,
        IntPtr lpBaseAddress,
        [MarshalAs(UnmanagedType.AsAny)] object lpBuffer,
        int dwSize,
        out IntPtr lpNumberOfBytesWritten);

        // GetProcAddress signture https://www.pinvoke.net/default.aspx/kernel32.getprocaddress
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        // GetModuleHandle signture http://pinvoke.net/default.aspx/kernel32.GetModuleHandle
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        // CreateRemoteThread signture https://www.pinvoke.net/default.aspx/kernel32.createremotethread
        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(
        IntPtr hProcess,
        IntPtr lpThreadAttributes,
        uint dwStackSize,
        IntPtr lpStartAddress,
        IntPtr lpParameter,
        uint dwCreationFlags,
        IntPtr lpThreadId);

        // CloseHandle signture https://www.pinvoke.net/default.aspx/kernel32.closehandle
        [DllImport("kernel32.dll", SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

        public static void UNIEXdllInjector(Process process, string DllPath)
        {
            IntPtr Size = (IntPtr)DllPath.Length;
            string message = "";

            // Open handle to the target process
            IntPtr ProcHandle = OpenProcess(
                ProcessAccessFlags.All,
                false,
                process.Id);
            if (ProcHandle == null)
            {
                ThemedMessageBox.Show("[!] Handle to target process could not be obtained!", "UNIEX.DLL messages");
                
                System.Environment.Exit(1);
            }
            else
            {
                message += "[+] Handle (0x" + ProcHandle + ") to target process has been obtained.\n";
            }

            // Allocate DLL space
            IntPtr DllSpace = VirtualAllocEx(
                ProcHandle,
                IntPtr.Zero,
                Size,
                AllocationType.Reserve | AllocationType.Commit,
                MemoryProtection.ExecuteReadWrite);

            if (DllSpace == null)
            {
                ThemedMessageBox.Show(message+"[!] DLL space allocation failed.", "UNIEX.DLL messages");
                System.Environment.Exit(1);
            }
            else
            {
                message += "[+] DLL space (0x" + DllSpace + ") allocation is successful.\n";
            }

            // Write DLL content to VAS of target process
            IntPtr bytesread;
            byte[] bytes = Encoding.ASCII.GetBytes(DllPath);
            bool DllWrite = WriteProcessMemory(
                ProcHandle,
                DllSpace,
                bytes,
                (int)bytes.Length,
                out bytesread
                );

            if (DllWrite == false)
            {
                ThemedMessageBox.Show(message + "[!] Writing DLL content to target process failed.", "UNIEX.DLL messages");
                System.Environment.Exit(1);
            }
            else
            {
                message += "[+] Writing DLL content to target process is successful.\n";
            }

            // Get handle to Kernel32.dll and get address for LoadLibraryA
            IntPtr Kernel32Handle = GetModuleHandle("Kernel32.dll");
            IntPtr LoadLibraryAAddress = GetProcAddress(Kernel32Handle, "LoadLibraryA");

            if (LoadLibraryAAddress == null)
            {
                ThemedMessageBox.Show(message + "[!] Obtaining an addess to LoadLibraryA function has failed.", "UNIEX.DLL messages");
                System.Environment.Exit(1);
            }
            else
            {
                message += "[+] LoadLibraryA function address (0x" + LoadLibraryAAddress + ") has been obtained.\n";
            }

            // Create remote thread in the target process
            IntPtr RemoteThreadHandle = CreateRemoteThread(
                ProcHandle,
                IntPtr.Zero,
                0,
                LoadLibraryAAddress,
                DllSpace,
                0,
                IntPtr.Zero
                );

            if (RemoteThreadHandle == null)
            {
                ThemedMessageBox.Show(message + "[!] Obtaining a handle to remote thread in target process failed.", "UNIEX.DLL messages");
                System.Environment.Exit(1);
            }
            else
            {
                //message += "[+] Obtaining a handle to remote thread (0x" + RemoteThreadHandle + ") in target process is successful.\n";
                ThemedMessageBox.Show(message + "[+] Obtaining a handle to remote thread (0x" + RemoteThreadHandle + ") in target process is successful.", "UNIEX.DLL messages");
            }

            // Deallocate memory assigned to DLL
            bool FreeDllSpace = VirtualFreeEx(
                ProcHandle,
                DllSpace,
                0,
                AllocationType.Release);
            if (FreeDllSpace == false)
            {
                ThemedMessageBox.Show(message + "[!] Failed to release DLL memory in target process.", "UNIEX.DLL messages");
                System.Environment.Exit(1);
            }
            else
            {
                message += "[+] Successfully released DLL memory in target process.\n";
                //ThemedMessageBox.Show(message + "[+] Successfully released DLL memory in target process.", "UNIEX.DLL messages");
            }

            // Close remote thread handle
            CloseHandle(RemoteThreadHandle);

            // Close target process handle
            CloseHandle(ProcHandle);
            
        }
    }
}