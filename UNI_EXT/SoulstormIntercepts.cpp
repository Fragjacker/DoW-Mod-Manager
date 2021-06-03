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
	int v4, v5, v11, v58;
	char v6;
	int(__thiscall *v7)(int);
	const wchar_t* v49;
	const char *v50;
	float v51;

	v4 = SOULSTORM_sub_96EAA0();
	v5 = ((int(__thiscall *)(int))SOULSTORM_sub_96F440)(v4);
	v6 = (*(int(__thiscall **)(int))(*(DWORD *)v5 + 636))(v5);
	v7 = *(int(__thiscall **)(int))(*(DWORD *)v5 + 640);
	
	v11 = *(DWORD *)(a1 + 376);

	v58 = SOULSTORM_sub_7761C0(v6, (char)v7);
	SOULSTORM_sub_66DE90(SOULSTORM_dieActionString_I, v58);
	SOULSTORM_runAnimationOnMetaMapMenuModel(*v49, *v50, v51);
	return 0;
	*/
	
	__asm
	{
		push    ebp
		/*
		push    0FFFFFFFFh
		push    offset FrameHandler3Jump ? _1
		mov     eax, large fs : 0
		push    eax
		mov     large fs : 0, esp
		sub     esp, 0x50
		
		push    ebx
		push    ebp
		push    esi
		push    edi
		*/
		mov     esi, ecx
		call    SOULSTORM_sub_96EAA0
		mov     ecx, eax
		call    SOULSTORM_sub_96F440
		mov     edi, eax
		mov     eax, [edi]
		mov     edx, [eax + 0x27C]
		mov     ecx, edi
		call    edx
		mov     bl, al
		mov     eax, [edi]
		mov     edx, [eax + 0x280]
		mov     ecx, edi
		mov     byte ptr[esp - 0x44], bl
		call    edx
		mov     byte ptr[esp - 0x40], al
		
		mov     eax, [esp - 0x40]
		mov     edx, [esp - 0x44]
        mov     ecx, [esi + 0x178]
	    push    eax
	    push    edx
		call    SOULSTORM_sub_7761C0
		
		//sub     esp, 0Ch
		
		mov     ecx, esp
		mov		[esp - 0x48], esp
		lea     edx, [esp - 0x48]
		push    edx
		push    SOULSTORM_dieActionString_I
		call    SOULSTORM_sub_66DE90
		mov     ecx, [esi + 0x178]
		call    SOULSTORM_runAnimationOnMetaMapMenuModel
		add     esp, 0xC

		pop    ebp
		//pop esi
		//retn 4
		/*
		pop     edi
		pop     esi
		pop     ebp
		//mov     large fs : 0, ecx
		pop     ebx
		//add     esp, 0x50
		//retn    4
		*/

	}
	/*
	// setOverlayEnabledFunction function
	__asm
	{
		mov     ecx, [esi + 0x178]
		call    SOULSTORM_sub_776140
		//mov     ecx, esi
		//call    SOULSTORM_setOverlayEnabledFunction
		retn 4
	}
	*/
	/*
	// definitionEnterMetamapScreenFunction
	__asm
	{
		push    ebx
		mov     ebx, [esp - 0x0C]
		push    esi
		push    ebx
		mov     esi, ecx
		call    SOULSTORM_sub_787340

		mov     eax, [esp + 0x24 - 0x18]
		push    eax
		mov     ecx, esi
		call    SOULSTORM_NewCampaignGameStart
		pop		esi
		pop		ebx
		retn	8
	}
	*/
	/*
	// runAnimationOnMetaMapMenuModel function
	__asm
	{
		sub     esp, 0x50
		push    esi
		mov     esi, ecx
		mov     ecx, esp
		//sub     esp, 0x0C
		mov		[esp + 0x7C], esp
		mov     edx, [esp + 0x7C]
		push    edx
		//lea     eax, [esi + 0x40]
		//push    eax
		//mov     eax, [esp + 0x7C]
		//push    eax
		push    SOULSTORM_dieActionString_I
        call    SOULSTORM_sub_66DE90
       // mov     ecx, [esi + 0x178]
        //call    SOULSTORM_runAnimationOnMetaMapMenuModel
		//fld     SOULSTORM_flt_C1F44C_I
		//sub     esp, 0x0C
		//mov     eax, esp
		//fstp    dword ptr[esp + 8]
		//mov[esp + 0x7C], esp
		//push    esi
		//mov     ecx, esi
		//call    SOULSTORM_sub_78EC00
		//add     esp, 0x4
		pop		esi
		retn	4
	}
	
	/*
	// bigger chunk of runAnimationOnMetaMapMenuModel function
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

// NOT WORKING
int __stdcall runActionOnModel(int)
{
	__asm
	{
		push    ebx
		push    esi
		push    edi
		
		mov     esi, ecx
		call    SOULSTORM_sub_96EAA0
		mov     ecx, eax
		call    SOULSTORM_sub_96F440
		mov     ecx, [esi + 0x178]
		test    ecx, ecx
		mov     edi, [esp + 0x10]
		mov     ebx, eax
		jz      skip1
		push    1
		push    edi
		call    SOULSTORM_runZoomAnimation
skip1:
			//add     esp, 0x4
		pop		edi
		pop		esi
		pop		ebx
		retn	4
	}
}

// PARTIALLY WORKING
// copy of function from definitionButtonInspectClicked button
int __stdcall definitionButtonInspectClicked(int)
{
	__asm
	{
		push    esi
		mov     esi, ecx
		push    SOULSTORM_String
		lea     eax, [esi + 0x50]
		push    SOULSTORM_a_root_sidebars
		push    eax
		call    USERINTERFACE_Invoke_SwfWidget_UI__QAAPBDPBD0ZZ
		add     esp, 0x10
		/*
		add     esp, 4
		mov     ecx, esp
		push    esi
		push    SOULSTORM_loc_78E070
		push    ecx
		call    SOULSTORM_setPointerToArgument2Function
		add     esp, 0x0C
		
		mov     ecx, esi
		call    SOULSTORM_sub_787320
		*/
		mov     edx, [esi + 0x38]
		push    edx
		mov     ecx, esi
		//call	runActionOnModel
		call    SOULSTORM_openAnotherGFXFile
		pop     esi
		retn    4
	}
}

// WORKING
// copy of function from ToggleArmy button
int __stdcall definitionButtonToggleArmyClicked(int)
{
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
		lea     ecx, [esp + 0x24 - 0x10]
		//lea     edx, [esp + 0x24 - 0x10]
		//push    definitionButtonInspectClicked
		//push	SOULSTORM_definitionEnterMetamapScreenFunction
		push    runAction
		//push    edx
		push	ecx
		call    SOULSTORM_sub_78F520
		//call    SOULSTORM_sub_78F4C0
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

