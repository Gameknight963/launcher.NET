# launcher.net

A lightweight game launcher capable of playing **any** game, through the power of plugins. It also has complete Thunderstore integration,
making mod management easy.

<img width="715" height="508" alt="launcherdotnet_QKwqSGktTu" src="https://github.com/user-attachments/assets/259e2a73-083d-451b-b6b8-cd949609bcbc" />
<br>
It still looks good even though its winforms

### launcher.net vs prism launcher

<img width="593" height="33" alt="image" src="https://github.com/user-attachments/assets/08510937-598f-4bed-85c8-0a0bfedc6ac6" />
<br>
<img width="600" height="35" alt="image" src="https://github.com/user-attachments/assets/afe0c9c1-35f6-4074-ada5-224599658116" />

Don't get me wrong I still love prism launcher, it's just here to give you a sense of scale. 

A minecraft plugin is possible to make btw

## Features
 - **Modular plugin system.** Program support for anything you want (mod managers, games, features, whatever.)
 - **Builtin Thunderstore API integration plugin**. Install mods with a few clicks. 
 - Create multiple instances. Useful for modders like myself
 - Low RAM and CPU usage. Built with Winforms, not some Electron BS.
 - Themable to light mode, dark mode, and cool transparency themes

## Planned features
 - One-click automatic Melonloader development environment setup via a plugin
 - Better DPI scaling. The layout can look a little weird on high dpi.
 - Removing restrictions on games outside launcher root
 - Editable theme json files
 
## Possible in the future
 - Saving space via symblinks
 - Hooking uxtheme draw calls to make certain native controls look better

If there's a feature you'd like to see, open an issue!

### but how??

Instead of implementing installation a million times for a million games, subsequently bloating the application, launcher.net uses 'plugins,' which are (.NET) dlls containing additional functionality. It currently comes with five plugins:
 - Hello World: Generates an EXE that prints Hello World to the console.
 - Game from Url: Downloads a game from a ZIP download url and installs it
 - Copy Steam Game: Copies any steam game you have installed. It should work for games without DRM.
 - Thunderstore Mod Manager: Download, install, and remove mods from Thunderstore with the press of a button (or two, i forgot)
 - Miside Zero Installer: Downloads msz from [my mirror](https://github.com/Gameknight963/MSZVersionArchive). Useful for me cause I mod this game

## Installation

1. Download the latest version from Github Releases.

2. Extract it whever you want, and run ``launcherdotnet.exe``.
 
An installer is not planned. Put it somewhere nice and add a shortcut to the start menu.

## Usage
### Adding an instance

Click "+ Add new Instance," select a game and version and click Install. You will be prompted to type a name. (Or not, if the plugin is poorly scripted)

### I have a weird error

Please [open an issue](https://github.com/Gameknight963/launcher.NET/issues/new)!

## Installing plugins

In the "plugins" tab of the settings menu, click "open plugins folder." Put any plugins into here.

> [!WARNING]
> Plugins have FULL ACCESS to your PC when running, as any other program would! Use plugins with caution.
> I was planning to use CasCore for security, but it really doesn't make sense to try to stop a program with Internet access from doing bad things. Same as modloaders, in general they don't have any security on the mods they load.

## Developing plugins
see [PLUGINS.MD](https://github.com/Gameknight963/launcher.NET/blob/main/PLUGINS.md)

## Screenshots

#### Light

<img width="711" height="481" alt="image" src="https://github.com/user-attachments/assets/dc5ebd17-e4a0-4ef3-a169-ed355149983d" />

#### Dark

<img width="711" height="481" alt="image" src="https://github.com/user-attachments/assets/fbc81937-3ed7-4def-bca2-9ff4eece81aa" />

#### Blurred Background

<img width="711" height="481" alt="image" src="https://github.com/user-attachments/assets/e0c21a65-7ee1-4849-a3a4-2fe93591ae7f" />

#### Acrylic

<img width="711" height="481" alt="image" src="https://github.com/user-attachments/assets/7aa56f23-f191-4e78-89ea-8ad24f4b1d3e" />

#### Extended frame (dark)

<img width="725" height="488" alt="image" src="https://github.com/user-attachments/assets/bbe09315-bc76-44e6-9ade-3925c05871fe" />

#### Issues with extended frame (light)

Using this theme causes the text to become invisisble. GDI operations eat the alpha channel, and this is difficult to fix properly. Currently I just opted to set the text to white so it would work with titlebar blurring software.

It is possible to hook GDI methods using a hooking library and patch the alpha channel. Here is a project that does just that:

https://windhawk.net/mods/translucent-windows

#### Possible to blur the titlebar as well?
**No.** The titlebar is part of the **non-client area**, which basically means application's don't have any control over how it looks. Apps targeting modern frameworks such as WinUI typically use a custom titlebar to get around this, but I don't want to do that. Perhaps it will be an optional feature in the future.

### With DWMBlurGlassInstalled

#### Extended frame

<img width="725" height="488" alt="image" src="https://github.com/user-attachments/assets/f658e9b6-db08-455f-ac96-3b9a785e09b1" />

Color will vary based on your DWMBlurGlass settings. It also works with other titlebar blurring software such as OpenGlass.

By the way, themes still apply to Messageboxes and Inputboxes:

<img width="469" height="167" alt="image" src="https://github.com/user-attachments/assets/6f845eb7-696f-4e8c-b177-73d465f09d72" />
<br>
<img width="304" height="148" alt="image" src="https://github.com/user-attachments/assets/11b41abb-ca0c-43e0-93e1-0983aab32baf" />

I reimplemented Messagebox and Inputbox in Winforms to acheive this

Also, you can set the gradient color of transparent themes to whatever you want:

<img width="711" height="481" alt="image" src="https://github.com/user-attachments/assets/48652da6-2803-4ac6-96d6-04ea02edbef7" />

<img width="564" height="488" alt="image" src="https://github.com/user-attachments/assets/e333ebfb-327b-41a1-87cb-e6087446da96" />
