// UNI_EXT.cpp : Defines the exported functions for the DLL application.
// Based on slashdiablo-maphack implementation
// https://github.com/planqi/slashdiablo-maphack

#include "stdafx.h"
#include "UNI_EXT.h"
#include "SoulstormHandlers.h"
#include "Patch.h"
#include "SoulstormIntercepts.h"

string UNI_EXT::path;
HINSTANCE UNI_EXT::instance;

Patch* patches[] = {
	// increase maximum number of defeated races in campaign commander screen
	new Patch(Overwrite, SOULSTORM, { 0x81F350, 0x6BDD24 }, { 0x0A, 0x00, 0x00, 0x00 }, 4),

	// increase maximum number of honor guard units in campaign metamap screen
	new Patch(Overwrite, SOULSTORM,{ 0x81F454, 0x6BDE28 },{ 0x12, 0x00, 0x00, 0x00 }, 4),

	// increase maximum number of honor guard units in campaign commander screen
	new Patch(Overwrite, SOULSTORM,{ 0x81F348, 0x6BDD1C },{ 0x12, 0x00, 0x00, 0x00 }, 4),
};


void UNI_EXT::Startup(HINSTANCE instance)
{
	// code that rewrites variable in program memory 
	// increased defeated races number

	for (int n = 0; n < (sizeof(patches) / sizeof(Patch*)); n++) {
		patches[n]->Install();
	}

//	MessageBoxA(NULL, "DLL Injected", "DLL Injected", MB_OK);
//	CreateThread(0, 0, MainThread, instance, 0, 0);

}

bool UNI_EXT::Shutdown()
{
	return true;
}