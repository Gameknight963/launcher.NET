# launcher.NET

A lightweight game launcher capable of playing **any** game, through the power of plugins. 

<img width="725" height="489" alt="launcherdotnet_Jjlpgl8Mfb" src="https://github.com/user-attachments/assets/b4e48baf-08ac-4050-8139-d0184529c5ef" />

Shown: Windows 10 with [Aero10](https://www.deviantart.com/vaporvance/art/Aero10-Vista-Seven-909711949) theme, _not_ windows 7. 

### but how??

Instead of implementing installation a million times for a million games, launcher.NET uses 'plugins,' which are dll files that get loaded containing implementations for installing games. As of such, it doesn't currently come with much functionality. You'll need to either script plugins to install games, or find them online. However, this system makes it fully modular.

## Installation

1. Download the latest version from Github Releases.

2. Extract it whever you want, and run ``launcherdotnet.exe``.
 
3. You're done! Add shortcuts to your Start menu as you like.

## Usage
### Adding an instance

Click "+ Add new Instance" and follow the instructions.

### Important
All instances are currently run through cmd.exe. This will be configurable in the next update.

### Installing mods

Melonloader, a Unity modloader, is the only modloader currently integrated into launcher.NET. To install it, click 'Install..." on the left panel with an instance selected, and follow the instructions.

Next, select your instance and click "open game folder." Note that the installer does not create the Mods folder for you. Create the Mods folder, and put any mods in there.

### Configuring it to your liking
Click "settings." Descriptions of what each setting does are included on the right panel of the settings menu.

## Installing plugins

In the "plugins" tab of the settings menu, click "open plugins folder." Drag any plugins into here.

**WARNING**

Plugins have FULL ACCESS to your PC when running, as any other program would! Use plugins with caution. There is some security planned. You may have noticed CasCore.dll (and its dependencies) in the latest release. While they are not currently in use, I plan to use it to make excecuting plugins much more secure. See https://douglasdwyer.github.io/CasCore/ for more details.

## Developing plugins
see [PLUGINS.MD](https://github.com/Gameknight963/launcher.NET/blob/main/PLUGINS.md)

## Screenshots

<img width="526" height="489" alt="launcherdotnet_iBvbmKe2Cb" src="https://github.com/user-attachments/assets/cfc0b90c-51a3-4979-912d-f820d69ade5a" />

<img width="725" height="489" alt="launcherdotnet_IgWOlE3189" src="https://github.com/user-attachments/assets/a8aa6b16-25ca-4a80-9604-cf6cf6afe951" />

#### What is MLInstaller.SDK.dll?

It's a little open-source program I wrote to assist with fetching and downloading Melonloader versions. [View it's source code here](https://github.com/Gameknight963/MLInstaller.SDK)
