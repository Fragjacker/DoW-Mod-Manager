// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"

DWORD WINAPI MainThread(LPVOID param)
{
	while (true)
	{
		if (GetAsyncKeyState(VK_F6) & 0x80000)
		{
			MessageBoxA(NULL, "F6 Pressed", "F6 Pressed", MB_OK);
		}
		Sleep(100);
	}
	return 0;
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
		MessageBoxA(NULL, "DLL Injected", "DLL Injected", MB_OK);
		CreateThread(0, 0, MainThread, hModule, 0, 0);
		break;

	case DLL_THREAD_ATTACH:
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		break;
	}
	return TRUE;
}

