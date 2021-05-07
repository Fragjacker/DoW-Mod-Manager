// Based on slashdiablo-maphack implementation
// https://github.com/planqi/slashdiablo-maphack

#pragma once

#ifdef _DEFINE_PTRS
#define FUNCPTR(dll, name, callingret, args, ...) \
	static Offsets f##dll##_##name##_offsets = { __VA_ARGS__ }; \
	__declspec(naked) callingret dll##_##name##args \
	{ \
		static DWORD f##dll##_##name = NULL; \
		if(f##dll##_##name == NULL) \
		{ \
		__asm { pushad } \
		f##dll##_##name = Patch::GetDllOffset(dll, *(&f##dll##_##name##_offsets._113c + D2Version::GetGameVersionID())); \
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
		static int address = *(&f##Asm_##_##name##_offsets._113c + D2Version::GetGameVersionID()); \
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
		static int address = *(&f##Var_##_##name##_offsets._113c + D2Version::GetGameVersionID()); \
		f##Var_##dll##_##name## = Patch::GetDllOffset(dll, address); \
		} \
		return (type**)&##f##Var_##dll##_##name; \
	} 

#else
#define FUNCPTR(dll, name, callingret, args, ...) extern callingret dll##_##name##args;
#define ASMPTR(dll, name, ...) extern DWORD* Asm_##dll##_##name##(VOID); static DWORD dll##_##name = *Asm_##dll##_##name##();
#define VARPTR(dll, name, type, ...) extern type** Var_##dll##_##name##(VOID); static type* p##_##dll##_##name = (type*)*Var_##dll##_##name##();
#endif
