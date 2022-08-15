// Based on slashdiablo-maphack implementation
// https://github.com/planqi/slashdiablo-maphack

#pragma once
#include "stdafx.h"
#include <string>
using namespace std;

namespace UNI_EXT {
	extern string path;
	extern HINSTANCE instance;

	extern void Startup(HINSTANCE instance);

	extern bool Shutdown();
}
