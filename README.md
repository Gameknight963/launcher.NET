# launcher.NET

A lightweight game launcher capable of playing **any** game, through the power of plugins. 

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

Plugins have FULL ACCESS to your PC when running, as any other program would! Use plugins with caution.

## Developing plugins
see PLUGINS.MD

#### What is MLInstaller.SDK.dll?

It's a little open-source program I wrote to assist with fetching and downloading Melonloader versions. [View it's source code here](https://github.com/Gameknight963/MLInstaller.SDK)