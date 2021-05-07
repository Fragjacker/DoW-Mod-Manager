// Based on slashdiablo-maphack implementation
// https://github.com/planqi/slashdiablo-maphack

#pragma once
#include <vector>
#include <string>
#include <Windows.h>

class Patch;

enum Dll { SOULSTORM = 0 };
enum PatchType { Jump = 0xE9, Call = 0xE8, NOP = 0x90, Push = 0x6A };

struct Offsets {
	int _steam;
};

class Patch {
private:
	static std::vector<Patch*> Patches;
	Dll dll;
	PatchType type;
	Offsets offsets;
	int length, function;
	BYTE* oldCode;
	bool injected;
public:
	Patch(PatchType type, Dll dll, Offsets offsets, int function, int length);

	bool Install();
	bool Remove();

	bool IsInstalled() { return injected; };

	static int GetDllOffset(Dll dll, int offset);
	static bool WriteBytes(int address, int len, BYTE* bytes);
};