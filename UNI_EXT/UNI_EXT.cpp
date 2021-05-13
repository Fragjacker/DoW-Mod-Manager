// UNI_EXT.cpp : Defines the exported functions for the DLL application.
// Based on slashdiablo-maphack implementation
// https://github.com/planqi/slashdiablo-maphack

#define _DEFINE_PTRS
#include "stdafx.h"
#include "SoulstormPointers.h"
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

	// modify SWFwidget bind from 'ToggleArmy' back to 'ToggleArmy' ('deep_strike' will be next iteration)
	new Patch(Overwrite, SOULSTORM,{ 0x391C4E, -1 },{ 0x58 }, 1),

	// insert new FSCommand
	//new Patch(Call, SOULSTORM,{ 0x391C39, -1 }, (int)new_BindButtonClickedEntry_Function, 5),

	// create new strings in code
	//new Patch(Overwrite, SOULSTORM,{ 0x6D2447, -1 },{ 0x00, 't','h','r','o','w','n' }, 7),
	// modify runZoomAnimation? function to use new string
	//new Patch(Overwrite, SOULSTORM,{ 0x378108, -1 },{ 0x48, 0x24 }, 2),

	//new Patch(Overwrite, SOULSTORM,{ 0x39C590, -1 }, (int)Metamap_Action_Selector_Function, 2),
	//new Patch(Call, SOULSTORM,{ 0x378104, -1 }, (int)Animation_Selector_Function, 5),
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