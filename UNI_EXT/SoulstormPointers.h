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
FUNCPTR(SOULSTORM, sub_78F520, int* __stdcall, (int, int, int), 0x38F520, -1)

ASMPTR(SOULSTORM, buttonToggleArmyClicked_I, 0x38DB80, -1)
ASMPTR(SOULSTORM, aToggleArmy_I, 0x6D4058, -1)
ASMPTR(SOULSTORM, aDeepStrike_I, 0x6D4020, -1)
//VARPTR(SOULSTORM, buttonToggleArmyClicked, DWORD, 0x78DB80, -1)

#undef FUNCPTR
#undef ASMPTR
#undef VARPTR
