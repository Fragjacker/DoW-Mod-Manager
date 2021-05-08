// Based on slashdiablo-maphack implementation
// https://github.com/planqi/slashdiablo-maphack

#include "SoulstormVersion.h"
#include <Windows.h>

#pragma comment(lib,"Version.lib")

VersionID SoulstormVersion::versionID = INVALID;

void SoulstormVersion::Init() {
	std::string version = GetGameVersionString();

	if (version == "1.3.310.7442") {
		versionID = VERSION_STEAM;
	}
//	else if (version == "1.0.13.64") {
//		versionID = VERSION_113d;
//	}
	else {
		versionID = INVALID;
		MessageBox(NULL, L"Game version not detected or is unsupported!", L"Failed to Detect Game Version", MB_OK);
		exit(0);
	}
}

VersionID SoulstormVersion::GetGameVersionID() {
	if (versionID == INVALID) {
		Init();
	}
	return versionID;
}

// Taken from StackOverflow user crashmstr
std::string SoulstormVersion::GetGameVersionString() {
	LPCWSTR szVersionFile = L"Soulstorm.exe";
	DWORD  verHandle = 0;
	UINT   size = 0;
	LPBYTE lpBuffer = NULL;
	DWORD  verSize = GetFileVersionInfoSize(szVersionFile, &verHandle);
	std::string message = verSize + "\n";

	std::string returnValue;

	if (verSize != NULL)
	{
		LPSTR verData = new char[verSize];

		if (GetFileVersionInfo(szVersionFile, verHandle, verSize, verData))
		{
			if (VerQueryValue(verData, L"\\", (VOID FAR* FAR*)&lpBuffer, &size))
			{
				if (size)
				{
					VS_FIXEDFILEINFO *verInfo = (VS_FIXEDFILEINFO *)lpBuffer;
					if (verInfo->dwSignature == 0xfeef04bd)
					{
						char szBuffer[MAX_PATH];
						// Doesn't matter if you are on 32 bit or 64 bit,
						// DWORD is always 32 bits, so first two revision numbers
						// come from dwFileVersionMS, last two come from dwFileVersionLS
						sprintf_s(szBuffer, "%d.%d.%d.%d",
							(verInfo->dwFileVersionMS >> 16) & 0xffff,
							(verInfo->dwFileVersionMS >> 0) & 0xffff,
							(verInfo->dwFileVersionLS >> 16) & 0xffff,
							(verInfo->dwFileVersionLS >> 0) & 0xffff
							);

						returnValue = std::string(szBuffer);
						message += returnValue + "\n";
						MessageBoxA(NULL, message.c_str(), "DLL Injected", MB_OK);
					}
				}
			}
		}
		delete[] verData;
	}
	return returnValue;
}

std::string SoulstormVersion::GetHumanReadableVersion() {
	std::string returnValue;

	switch (GetGameVersionID()) {
	case VERSION_STEAM:
		returnValue = "steam";
		break;
//	case VERSION_113d:
//		returnValue = "1.13d";
//		break;
	default:
		returnValue = "unknown";
	}

	return returnValue;
}