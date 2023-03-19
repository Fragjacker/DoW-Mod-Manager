![image](https://user-images.githubusercontent.com/19975052/147577379-4ab095f3-e91a-42f2-8cae-fbbb50658130.png)

# DoW Mod Manager v2.3.2.2

This application allows for an easy launch of mods and management of large collections of mods for Warhammer 40K Dawn of War series.

## INSTALLATION:

- In order to install the Mod Manager drop the "DoW Mod Manager v2.3.2.2.exe" into your primary game directory which is either:

  - "..\Dawn of War - Soulstorm\"
  or
  - "..\Dawn of War - Dark Crusade\"
  or
  - "..\Dawn of War - Winter Assault\"
  or even
  - "..\Dawn of War - Gold\"

  DO NOT put executable file into a subfolder or it won't work!

## MOD MANAGER USAGE:

1. Once everything is in place launch the "DoW Mod Manager v2.3.2.2.exe" by double-clicking on it (You may want to create a shortcut of the Mod Manager on your Desktop for convenience and faster access)

2. Select a Mod from the left listing and it will show if all necessary dependency Mods are installed and if that is given, lets you directly launch the desired Mod

3. You have the option to directly launch the un-modded vanilla Game by clicking the "START BASE GAME" button.

## MOD MANAGER FEATURE EXPLANATION:

- **Install location**: Shows the current location of the active Mod Manager executable. You can have multiple Soulstorm installations with their own Mod Manager
(Simply repeat the installation process inside the new Soulstorm install location)

- **START BASE GAME**: Does what the name implies, starts a new Session of the unmodded Soulstorm base game.

- **START MOD**: Starts the Game with the selected Advanced Start Options and the selected Mod if all dependency mods are installed.

- **SETTINGS**: You could change game settings without launching the game. It also let's you change some HIDDEN game settings! From there you could also start System Performance Manager

- **TOGGLE LAA**: This button allows for convenient activation and deactivation of the LAA flag for the Soulstorm.exe and GraphicsConfig.exe

- **LAA Labels** : These two labels will show if both the Soulstorm.exe and GraphicsConfig.exe are LAA patched. Since the UA Mod and many other mods push the game engine really hard towards it's limits
it's recommended to have the LAA(Large Address Awareness AKA. 4GB Patch) activated on both executables to reduce the chance for the game crashing when playing mods. Since most people use the LAA Patch in Online matches anyways you'd do yourself a favor applying the LAA patch on said Executables. The LAA patch can ba applied and removed at any time by pressing the TOGGLE LAA button.

- **Download Mod...**: This button will open the new Download Manager window where you could download one (or how much you like) of popular mods. If mod has a patch - it will be downloaded too! Wait 5 seconds and mod will start downloading.

- **Check for errors**: This button will start a search for any critical errors in warnings.log. All errors would be presented to user

- **Fix MISSING**: This button will start a Mod Downloader and send it a missing module name so user could download it just in one click!

- **About and Updates**: This will open "About" window so user could read some useful information (such as authors names, license and changelog). Also there is a "Update" button which makes updating DoW Mod Manager very easy!

- **Advanced Start Options:**

  - **-dev** : Starts the game in developers mode that provides helpful debug tools and enables execution of scripts (such as the autoexec.lua) in skirmish games, which are not available in the base game.
	(WARNING: Don't use this in multiplayer as it will not work.)

  - **-nomovies** : Skips all movies and directly launches the game with the loading screen for a faster startup (Is checked by default for convenience)

  - **-forcehighpoly** : This option forces the game to display the higher resolution LOD of a Model at any given distance. (Hint : This may have a negative impact on your performance, use with caution.)

  - **/high /affinity 6** : This option sets your Daw of War game executable priority to "High" and set it's affinity (CPU thread usage) to utilize only threads 2 and 3. In theory it could help to improve performance. It WILL NOT work if you have less than 3 CPU cores!

  - **Disable Fog** : This option removes the brownish ambient fog from any map. This fog is particularly visible when zooming out really far or move the camera in a horizontal position, obstructing long distance view.

## MOD MERGER: (WARNING: This is an experimental feature that requires some user responsibility - use with caution.)

1. Pressing this button will open you another window where you have the opportunity to easily edit the Module file of any playable mod.

2. To use it you have to select the Mod first that you want to edit from the drop-down list.

3. Doing so will fill the upper Listbox with all the other Mods that your currently selected Mod will load upon launch.
The bottom list displays the Mods that are not currently loaded by your selected mod but are installed inside your Soulstorm directory.

4. You can select Mods from the bottom list and add them using the "+" Button. The selected Mod will be added to the top list and be set as "Active".

5. Using the Green/Red arrows allows you to change the load order of the selected Mod by moving it Up/Down in the list.

6. The Red "X" Button allows you to set a Mod to "Inactive" which means the the Mod will not be loaded into the Mod anymore but remain inside the Module file for later Reactivation if desired.

7. With the "-" you can remove an "Active/Inactive" Mod from the list of loaded mods and move it back to the bottom list of available but not yet added mods.

**HINT**: None of the operations so far remove or edit anything of the selected Mod. Reselecting the currently loaded Mod or any other from the top drop-down list will discard your changes.

If you want to save your changes click the "SAVE" button. This will create a copy of your loaded Mod with your new layout and generates a valid and launch-able file.

With the Mod Merger you can easily add, remove or change load orders of ANY desired Mod you want. You can quickly create large combiner Mods with many race mods merged into another Mod.

**For Example**:

Selecting *Unification_New_Races* from the drop-down list then adding any race Mod that you desire by selecting that race mod and hit "+" and then press "SAVE MERGE". This will create a new module file, with the exact same name as the original mod but with the postfix "MERGED" added to it. It will load your desired race into the Mod the next time you launch *Unification_New_Races_MERGED* from the Mod Manager now.

To restore the previous layout (If you want to rollback) you simply press the "DELETE MERGE" button. No original files will be harmed in this process.

## DISCLAIMER:

This application comes without guarantee! Developers tested it and could say that it's working as intended but it still may contain some... unexpected behaviors. So use it on your own risk!
