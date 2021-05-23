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
int __stdcall runAction(int)
{
	/*
	__asm
	{
		//push    1
		add     ecx, 0x50 //'P'
		push    ecx
		push    SOULSTORM_dieActionString_I
		call    SOULSTORM_sub_66DE90
		call    SOULSTORM_runAnimationOnMetaMapMenuModel
		//push    ecx
		//add     esp, 0x10
		retn    4
	}
		*/
	//	SOULSTORM_runAnimationOnMetaMapMenuModel(SOULSTORM_dieActionString_I, SOULSTORM_dieActionString_I, 0);
//	return 0;
	
	__asm
	{
		push	esi
		mov     ecx, esp
		//add     esp, 0x0C
		mov		[esi + 0x7C], esp
		lea     edx, [esi + 0x7C]
		push    edx
		push    SOULSTORM_dieActionString_I
        call    SOULSTORM_sub_66DE90
        mov     ecx, [esi + 0x178]
        call    SOULSTORM_runAnimationOnMetaMapMenuModel
		//fld     SOULSTORM_flt_C1F44C_I
		add     esp, 0x0C
		pop		esi
		ret		4
	}
	/*
	__asm
	{
		push    esi
		sub     esp, 0x0C
		mov     ecx, esp
		mov		[esp + 0x7C], esp
		lea     edx, [esp + 0x7C]
		push    edx
		push    SOULSTORM_dieActionString_I
		call    SOULSTORM_sub_66DE90
		mov     ecx, [esi + 0x178]
		call    SOULSTORM_runAnimationOnMetaMapMenuModel
		fld     SOULSTORM_flt_C1F44C_I
		sub     esp, 0x0C
		mov     eax, esp
		fstp    dword ptr[esp + 8]
		mov		[esp + 0x7C], esp
		push    esi
		push    SOULSTORM_sub_78EC00
		push    eax
		call    SOULSTORM_sub_78F540
		add     esp, 0x0C
		mov     ecx, esi
		call    SOULSTORM_sub_787020
		mov     [esi + 0x183], 1
		mov     ecx, SOULSTORM_off_C1F448_I
		push    0
		push    ecx
		call	SOULSTORM_sub_75EC70
		mov     [esi + 0x182], 1
		pop		esi
		ret		4
	}
	*/
}

// copy of function from ToggleArmy button
int __stdcall definitionButtonToggleArmyClicked(int)
{
	/*
	DWORD zmienna;
	__asm
	{
		add     ecx, 0x50 //'P'
		mov		zmienna, ecx
	}
	USERINTERFACE_Invoke_SwfWidget_UI__QAAPBDPBD0ZZ(reinterpret_cast<const char*>(zmienna), "_root.ShowStatsArmy", "%d", "1" );
	
	
	__asm
	{
		add     esp, 0x10
		retn    4
	}
	*/
	
	__asm
	{
		push    1
		push    SOULSTORM_aD_I //"%d"
		add     ecx, 0x50 //'P'
		push    SOULSTORM_a_root_showst_0_I //"_root.ShowStatsArmy"
		push    ecx
		call    USERINTERFACE_Invoke_SwfWidget_UI__QAAPBDPBD0ZZ
		add     esp, 0x10
		retn    4
	}
	
}

int __declspec(naked) new_BindButtonClickedEntry_Function()
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
		push    runAction
		push    edx
		call    SOULSTORM_sub_78F520
		add     esp, 0x0C
		push    eax
		push    SOULSTORM_aDeepStrike_I
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
		push	return_address
		ret
	}
}

