// Based on slashdiablo-maphack implementation
// https://github.com/planqi/slashdiablo-maphack

#pragma once

#ifndef _SOULSTORMVERSION_H
#define _SOULSTORMVERSION_H

#include <string>

enum VersionID {
	INVALID = -1,
	VERSION_STEAM = 0
};

namespace SoulstormVersion {
	extern VersionID versionID;
	VersionID GetGameVersionID();
	void Init();
	std::string GetGameVersionString();
	std::string GetHumanReadableVersion();
};

#endif