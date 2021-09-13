// Based on slashdiablo-maphack implementation
// https://github.com/planqi/slashdiablo-maphack

#pragma once
#include "SoulstormVersion.h"
#include "Patch.h"

#ifdef _DEFINE_PTRS
#define FUNCPTR(dll, name, callingret, args, ...) \
	static Offsets f##dll##_##name##_offsets = { __VA_ARGS__ }; \
	__declspec(naked) callingret dll##_##name##args \
	{ \
		static DWORD f##dll##_##name = NULL; \
		if(f##dll##_##name == NULL) \
		{ \
		__asm { pushad } \
		f##dll##_##name = Patch::GetDllOffset(dll, *(&f##dll##_##name##_offsets._steam + SoulstormVersion::GetGameVersionID())); \
		__asm { popad } \
		} \
		__asm jmp [f##dll##_##name] \
	}

#define ASMPTR(dll, name, ...) \
	DWORD* Asm_##dll##_##name##(VOID) \
	{ \
		static DWORD f##Asm_##dll##_##name = NULL; \
		if(f##Asm_##dll##_##name## == NULL) \
		{ \
		static Offsets f##Asm_##_##name##_offsets = { __VA_ARGS__ }; \
		static int address = *(&f##Asm_##_##name##_offsets._steam + SoulstormVersion::GetGameVersionID()); \
		f##Asm_##dll##_##name## = Patch::GetDllOffset(dll, address); \
		} \
		return &##f##Asm_##dll##_##name; \
	} 

#define VARPTR(dll, name, type, ...) \
	type** Var_##dll##_##name##(VOID) \
	{ \
		static DWORD f##Var_##dll##_##name = NULL; \
		if(f##Var_##dll##_##name## == NULL) \
		{ \
		static Offsets f##Var_##_##name##_offsets = { __VA_ARGS__ }; \
		static int address = *(&f##Var_##_##name##_offsets._steam + SoulstormVersion::GetGameVersionID()); \
		f##Var_##dll##_##name## = Patch::GetDllOffset(dll, address); \
		} \
		return (type**)&##f##Var_##dll##_##name; \
	} 

#else
#define FUNCPTR(dll, name, callingret, args, ...) extern callingret dll##_##name##args;
#define ASMPTR(dll, name, ...) extern DWORD* Asm_##dll##_##name##(VOID); static DWORD dll##_##name = *Asm_##dll##_##name##();
#define VARPTR(dll, name, type, ...) extern type** Var_##dll##_##name##(VOID); static type* p##_##dll##_##name = (type*)*Var_##dll##_##name##();
#endif

//offsets are in format _steam, _cd

//for function new_BindButtonClickedEntry_Function
FUNCPTR(SOULSTORM, sub_78F520, int* __stdcall, (int, int, int), 0x38F520, -1)

ASMPTR(SOULSTORM, buttonToggleArmyClicked_I, 0x38DB80, -1)
ASMPTR(SOULSTORM, aToggleArmy_I, 0x6D4058, -1)
ASMPTR(SOULSTORM, aDeepStrike_I, 0x6D4020, -1)

//for function definitionButtonToggleArmyClicked
FUNCPTR(USERINTERFACE, Invoke_SwfWidget_UI__QAAPBDPBD0ZZ, char const* __cdecl, (char const*, char const*, ...), 0x4CEB0, -1)

ASMPTR(SOULSTORM, aD_I, 0x7455BC, -1)
ASMPTR(SOULSTORM, a_root_showst_0_I, 0x6D3AA4, -1)

//for function runAction
FUNCPTR(SOULSTORM, findMotionByGivenName, int __stdcall, (int, int), 0x26DE90, -1)
FUNCPTR(SOULSTORM, runMotionOnMetaMapMenuModel, int __stdcall, (int, int, int), 0x377BD0, -1)
FUNCPTR(SOULSTORM, sub_78F540, int __stdcall, (int, int, int), 0x38F540, -1)
FUNCPTR(SOULSTORM, sub_787020, int __stdcall, (int, int, int), 0x387020, -1)
FUNCPTR(SOULSTORM, sub_75EC70, int __stdcall, (int, int, int), 0x35EC70, -1)

ASMPTR(SOULSTORM, dieActionString_I, 0x6D3A84, -1)
ASMPTR(SOULSTORM, flt_C1F44C_I, 0x81F44C, -1)
ASMPTR(SOULSTORM, sub_78EC00, 0x38EC00, -1)
ASMPTR(SOULSTORM, off_C1F448_I, 0x81F448, -1)

// run standard metamap action 
FUNCPTR(SOULSTORM, sub_776140, int __stdcall, (void), 0x376140, -1)
FUNCPTR(SOULSTORM, setOverlayEnabledFunction, int __stdcall, (void), 0x38EC00, -1)

// definition for definitionEnterMetamapScreenFunction?
FUNCPTR(SOULSTORM, definitionEnterMetamapScreenFunction, int __stdcall, (int, int), 0x391990, -1)
FUNCPTR(SOULSTORM, sub_78F4C0, int __stdcall, (int, int, int), 0x38F4C0, -1)

// definition for definitionButtonEndTurnClicked function
FUNCPTR(SOULSTORM, setPointerToArgument2Function, int __cdecl, (int, int, int), 0x38F540, -1)
FUNCPTR(SOULSTORM, sub_787320, int __stdcall, (int, int), 0x387320, -1)
FUNCPTR(SOULSTORM, openAnotherGFXFile, int __stdcall, (int), 0x38EC70, -1)
FUNCPTR(SOULSTORM, runZoomAnimation, int __stdcall, (int, int), 0x378080, -1)
FUNCPTR(SOULSTORM, sub_96EAA0, int, (void), 0x56EAA0, -1)
FUNCPTR(SOULSTORM, sub_96F440, int, (void), 0x56F440, -1)
FUNCPTR(SOULSTORM, NewCampaignGameStart, int __stdcall, (int), 0x3912E0, -1)
FUNCPTR(SOULSTORM, sub_787340, int __stdcall, (int), 0x387340, -1)
FUNCPTR(SOULSTORM, toggleOverlayFunction, int __stdcall, (char, char), 0x3761C0, -1)

ASMPTR(SOULSTORM, String, 0x70DB8B, -1)
ASMPTR(SOULSTORM, a_root_sidebarh, 0x6D3174, -1)
ASMPTR(SOULSTORM, loc_78E070, 0x38E070, -1)
ASMPTR(SOULSTORM, FrameHandler3Jump, 0x668D1C, -1)
ASMPTR(SOULSTORM, flt_C1F44C, 0x81F44C, -1)

ASMPTR(SOULSTORM, a_root_sidebars, 0x6D3188, -1)

ASMPTR(SOULSTORM, loc_6CC271, 0x2CC271, -1)
ASMPTR(SOULSTORM, loc_6CB20B, 0x2CB20B, -1)

FUNCPTR(SOULSTORM, loc_78DBD0, int __stdcall, (int), 0x38DBD0, -1)

ASMPTR(SOULSTORM, aOnsidebarexite, 0x6D33B4, -1)

#undef FUNCPTR
#undef ASMPTR
#undef VARPTR
