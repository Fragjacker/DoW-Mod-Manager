// dllmain.cpp : Defines the entry point for the DLL application.
#include "stdafx.h"
#include "UNI_EXT.h"

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
					 )
{
	switch (ul_reason_for_call)
	{
	case DLL_PROCESS_ATTACH:
	case DLL_THREAD_ATTACH:
		UNI_EXT::Startup(hModule);
		break;
	case DLL_THREAD_DETACH:
	case DLL_PROCESS_DETACH:
		UNI_EXT::Shutdown();
		break;
	}
	return TRUE;
}
