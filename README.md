# launcher.NET

A lightweight game launcher capable of playing **any** game, through the power of plugins. 

<img width="711" height="476" alt="image" src="https://github.com/user-attachments/assets/452da1c3-e411-42ac-86ba-8c095e1122ee" />
<br>
It still looks good even though its winforms

### but how??

Instead of implementing installation a million times for a million games, launcher.NET uses 'plugins,' which are dll files that get loaded containing implementations for installing games. As of such, it doesn't currently come with much functionality. You'll need to either script plugins to install games, or find them online. However, this system makes it fully modular.

## Installation

1. Download the latest version from Github Releases.

2. Extract it whever you want, and run ``launcherdotnet.exe``.
 
3. You're done! Add shortcuts to your Start menu as you like.

## Usage
### Adding an instance

Click "+ Add new Instance," select a game and version and click Install. You will be prompted to type a name.

### Installing mods

Melonloader, a Unity modloader, is the only modloader currently integrated into launcher.NET. To install it, click 'Modify..." on the left panel with an instance selected, select a Melonloader version and click install.
> Note that the installer does not create the Mods folder for you. Create the Mods folder, and put any mods in there.

### Configuring it to your liking
Click "settings." Descriptions of what each setting does are included on the right panel of the settings menu.

## Installing plugins

In the "plugins" tab of the settings menu, click "open plugins folder." Drag any plugins into here.

**WARNING**

Plugins have FULL ACCESS to your PC when running, as any other program would! Use plugins with caution. I was planning to use CasCore for security, but it really doesn't make sense to try to stop a program with Internet access from doing bad things. Even without internet if it uses Http to get access to Docker its over.

## Developing plugins
see [PLUGINS.MD](https://github.com/Gameknight963/launcher.NET/blob/main/PLUGINS.md)

## Screenshots

**Light:**

<img width="711" height="481" alt="launcherdotnet_VNUgVsfIAu" src="https://github.com/user-attachments/assets/689b0d08-f9f4-4ce4-b7e9-8d28e22226b1" />

**Dark:**

<img width="711" height="481" alt="launcherdotnet_fXFJpTfPN3" src="https://github.com/user-attachments/assets/9fa9ddab-912e-432c-a063-0b7fc9de26e7" />

**Blurred background:**

<img width="711" height="481" alt="launcherdotnet_QHOQR7jgXj" src="https://github.com/user-attachments/assets/83317718-c57f-4125-8f9a-0d002ffa61b1" />

**Acrylic:**

<img width="725" height="488" alt="explorer_bx8k88Rmd7" src="https://github.com/user-attachments/assets/4334934c-8eb2-438f-a69a-3613790af3d2" />

**Extend frame (dark):** (oops the mouse got in the screenshot and i dont want to take it again)

<img width="711" height="481" alt="launcherdotnet_n2zwcfoXwV" src="https://github.com/user-attachments/assets/04330f9a-417d-4902-9a94-ca15b70c4f5f" />

Extend frame (light mode) does not currently work properly without external software (DWMBlurGlass).

### With DWMBlurGlass installed

**Extend frame:**

<img width="711" height="477" alt="launcherdotnet_W0lpRX6wEs" src="https://github.com/user-attachments/assets/3fa827c6-5c15-45d0-b3c8-4814f0af590a" />

**Extend frame (dark):**

<img width="711" height="476" alt="launcherdotnet_yQkKNYxFww" src="https://github.com/user-attachments/assets/bd16804d-8ff7-4b9e-8a3f-7de8fa1f85f4" />

_My settings: blur radius 4, light mode titlebar ``#6478B9FF``, dark mode titlebar ``#98010015``_

_DWMBlurGlass is awesome_

By the way, themes still apply to Messageboxes and Inputboxes:

<img width="469" height="167" alt="image" src="https://github.com/user-attachments/assets/6f845eb7-696f-4e8c-b177-73d465f09d72" />
<br>
<img width="304" height="148" alt="image" src="https://github.com/user-attachments/assets/11b41abb-ca0c-43e0-93e1-0983aab32baf" />

I reimplemented Messagebox and Inputbox in Winforms to acheive this

### Source of "Hello World" plugin
https://github.com/Gameknight963/launcher.NET-hello-world-plugin

#### What is MLInstaller.SDK.dll?

It's a little open-source program I wrote to assist with fetching and downloading Melonloader versions. [View it's source code here](https://github.com/Gameknight963/MLInstaller.SDK)
