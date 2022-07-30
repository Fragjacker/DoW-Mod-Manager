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

//*** FINISHED PATCHES ***

	// increase maximum number of defeated races in campaign commander screen
	new Patch(Overwrite, SOULSTORM, { 0x81F350, 0x6BDD24 }, { 0x0A, 0x00, 0x00, 0x00 }, 4),

	// increase maximum number of honor guard units in campaign metamap screen
	new Patch(Overwrite, SOULSTORM,{ 0x81F454, 0x6BDE28 },{ 0x12, 0x00, 0x00, 0x00 }, 4),

	// increase maximum number of honor guard units in campaign commander screen
	new Patch(Overwrite, SOULSTORM,{ 0x81F348, 0x6BDD1C },{ 0x12, 0x00, 0x00, 0x00 }, 4),

	// fog remover coded in UNI_EXT.dll
	/*
	new Patch(Overwrite, SOULSTORM,{ 0x4282F0, -1 },{ 217, 238,  15,  31,  64,   0 }, 6),
	new Patch(Overwrite, SOULSTORM,{ 0x6F54C8, -1 },{ 0,   0,   0,  68 }, 4),
	new Patch(Overwrite, SOULSTORM,{ 0x42A33A, -1 },{ 221, 216,  15,  31,  64,   0 }, 6),
	*/
	//*** END OF FINISHED PATCHES

	// modify SWFwidget bind from 'ToggleArmy' to 'deep_strike'
	//new Patch(Overwrite, SOULSTORM,{ 0x391C4E, -1 },{ 0x20, 0x40, 0xAD, 0x00

	// insert new FSCommand
	// Runs "die" action on metamap_main_menu model
	//new Patch(Call, SOULSTORM,{ 0x391C79, -1 }, (int)new_BindButtonClickedEntry_Function, 29),
	//new Patch(Call, SOULSTORM,{ 0x391C8D, -1 }, (int)new_BindButtonClickedEntry_Function, 5),
	new Patch(Call, SOULSTORM,{ 0x391C39, -1 }, (int)new_BindButtonClickedEntry_Function, 5),
	//new Patch(Call, SOULSTORM,{ 0x391C96, -1 }, (int)new_BindButtonClickedEntry_Function, 5),

	// 
	//new Patch(Overwrite, WXPMOD,{ 0x241E18, -1 },{ 0xC6, 0x45, 0xFC, 0x25 }, 4),
	//new Patch(Overwrite, WXPMOD,{ 0x241C65, -1 },{ 0xC6, 0x45, 0xFC, 0x25 }, 4),
	//new Patch(Overwrite, WXPMOD,{ 0x24137C, -1 },{ 0xC1, 0xE8, 0x25 }, 3),

	// disable stuff in load game screen
	//new Patch(Call, SOULSTORM,{ 0x2CC067, -1 }, (int)test_function, 5),

	// disable stuff in critical location function
	//new Patch(Call, SOULSTORM,{ 0x2CB1D2, -1 }, (int)test_function2, 5),

	//new Patch(Overwrite, SOULSTORM,{ 0x241E18, -1 },{ 0x0F, 0x84, 0x74, 0x01, 0x00, 0x00 }, 6),
	// enable action 'die' in 'metamap.gfx' every time it's loaded
	//new Patch(Overwrite, SOULSTORM,{ 0x3913C6, -1 },{ 0x90, 0x90 }, 2),
	//new Patch(Overwrite, SOULSTORM,{ 0x3919A0, -1 },{ 0x90, 0x90 }, 2),

	// modify campaign model starting action from 'die' to 'idle'
	//new Patch(Overwrite, SOULSTORM,{ 0x3913D7, -1 },{ 0x74, 0xBD, 0xB3, 0x00 }, 4),

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