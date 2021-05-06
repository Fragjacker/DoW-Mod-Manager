// stdafx.cpp : source file that includes just the standard includes
// UNIEX2.pch will be the pre-compiled header
// stdafx.obj will contain the pre-compiled type information

#include "stdafx.h"
#include <Windows.h>

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

BOOL WINAPI DllMain(
	HINSTANCE hinstDLL,  // handle to DLL module
	DWORD fdwReason,     // reason for calling function
	LPVOID lpReserved)  // reserved
{
	// Perform actions based on the reason for calling.
	switch (fdwReason)
	{
	case DLL_PROCESS_ATTACH:
		MessageBoxA(NULL, "DLL Injected", "DLL Injected", MB_OK);
		CreateThread(0,0,MainThread,hinstDLL,0,0);
		break;

	case DLL_THREAD_ATTACH:
		// Do thread-specific initialization.
		break;

	case DLL_THREAD_DETACH:
		// Do thread-specific cleanup.
		break;

	case DLL_PROCESS_DETACH:
		// Perform any necessary cleanup.
		break;
	}
	return TRUE;  // Successful DLL_PROCESS_ATTACH.
}