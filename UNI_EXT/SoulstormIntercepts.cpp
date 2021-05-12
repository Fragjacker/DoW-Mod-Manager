#include <Windows.h>
#include "SoulstormPointers.h"

//DWORD patch_clip_region;
//DWORD* base_address;
DWORD return_address;

/*
int __declspec(naked) Metamap_Action_Selector_Function()
{
	__asm
	{
		push offset AD3A84;
		call sub_66DE90;
		call sub_777BD0;
//		pushad;
//		call GameLoop;
//		popad;

//		pop eax;
//		sub esp, 0x20;
//		mov[esp + 0xC], ecx;
//		push eax;
//		ret;
	}
}
*/

int __declspec(naked) New_FSCommand_Function()
{
	// save address to return in the future
	// save stack
	__asm
	{
		pop return_address
		//pushad
	}

	__asm
	{
		push    esi
		lea     edx, [esp + 0x24 - 0x10]
		push    offset SOULSTORM_buttonToggleArmyClicked_I
		push    edx
		call    SOULSTORM_sub_78F520
		add     esp, 0x0C
		push    eax
		push    offset SOULSTORM_aDeepStrike_I
		mov     ecx, edi
		call    ebx
	}
	
	//restore stack
	//restore overwritten code
	__asm
	{
		//popad
		push    esi
		lea     edx, [esp + 0x24 - 0x10]
		push return_address
		ret
	}
}
