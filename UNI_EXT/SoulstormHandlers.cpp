//#include "SoulstormPointers.h"
#include "stdafx.h"

DWORD WINAPI MainThread(LPVOID param)
{
	bool inGame = false;
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

