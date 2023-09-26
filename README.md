# NoTankControls

A [ResoniteModLoader](https://github.com/resonite-modding-group/ResoniteModLoader) and
[NeosModLoader](https://github.com/zkxs/NeosModLoader) mod for Resonite and [Neos VR](https://neos.com/)
that allows you to move with your joysticks while holding any tool.

Neos/Resonite disables the joystick if you have a tooltip equipped which makes use of the "Secondary" action
(pressing down on that same joystick.)
This mod removes that restriction, allowing tools to be equipped while keeping full movement intact.

## Relavent Issue
Neos-Metaverse/NeosPublic#1969 seems related.

## Installation
1. Install the appropriate modloader.
1. Place [NoTankControls.dll](https://github.com/furrz/NoTankControls/releases/latest/download/NoTankControls.dll) into your `nml_mods` or `rml_mods` folder. Make sure to use an older release if on neos, the latest DLL download is for Resonite! You can create it if it's missing, or if you launch the game once with NeosModLoader installed it will create the folder for you.
1. Start the game. If you want to verify that the mod is working you can check your game logs, or try moving around with the DevToolTip equipped!
