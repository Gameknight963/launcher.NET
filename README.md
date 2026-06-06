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

Don't get me wrong I still love prism launcher, it's just here to give you a sense of scale. Eventually I might make a Minecraft plugin depending on how complicated the microsoft account stuff is

<img width="606" height="33" alt="image" src="https://github.com/user-attachments/assets/8242e95b-36e0-4605-a68a-f0b85759d775" />

optimization is a dead art to some holy. anyway moving on

## Features
 - Modular plugin system
 - Thunderstore API integration
 - Create multiple instances
 - Mod managment
 - Low RAM and CPU usage
 - Dependency management
 - Themable to light mode, dark mode, and a lot of other cool themes

## Planned features
 - Automatic Melonloader development enviornment setup
 - Removing restrictions on games outside launcher root
 - Saving space via symblinks
 - Plugin-based mod managers. This will allow the decoupling of launcher.net from Thunderstore since it's too tightly integrated

### but how??

Instead of implementing installation a million times for a million games, launcher.net uses 'plugins,' which are dll files that get loaded containing implementations for installing games. It currently comes with three plugins:
 - Hello World: Generates an EXE that prints Hello World to the console.
 - Game from Url: Downloads a game from a ZIP download url and installs it
 - Copy Steam Game: Copies any steam game you have installed. It should work for games without DRM.

## Installation

1. Download the latest version from Github Releases.

2. Extract it whever you want, and run ``launcherdotnet.exe``.
 
That's literally it it's just a zip. An installer is not planned. Put it somewhere nice and add a shortcut to the start menu.

## Usage
### Adding an instance

Click "+ Add new Instance," select a game and version and click Install. You will be prompted to type a name. (Or not, if the plugin is poorly scripted)

### Installing mods

*The screenshots in this section are slightly out of date, but it is more or less the same*

Click "Manage Mods" in order to see your modlist.

<img width="448" height="481" alt="image" src="https://github.com/user-attachments/assets/b951ab00-201f-4b63-90d0-81c86e98c444" />

From here you can add or delete mods. launcher.net will manage dependencies for you.

To get more mods from Thunderstore, click "Get more mods" (who would've thought) to bring up the Thunderstore Mod Browser.

<img width="817" height="515" alt="image" src="https://github.com/user-attachments/assets/ddfbe056-f386-40a0-b85a-01382dc806ac" />

From here you can select which mods you would like to install. Dependencies will be automatically installed. Once you press "Review and Confirm," 
you will be able to see all the mods you have selected:

<img width="359" height="475" alt="image" src="https://github.com/user-attachments/assets/9821a2cd-85f8-47bd-b4ce-25417f10299c" />

If your game does not have a Thunderstore slug, you can set one in the Game Info Editor:

<img width="371" height="361" alt="image" src="https://github.com/user-attachments/assets/433e03e2-a1dc-462a-ad16-c453b21a6bbc" />

To find the slug, take the last part from the community link:

https://thunderstore.io/c/{slug}/

For example, for Repo's community link:

https://thunderstore.io/c/repo/

it would be "repo".

(launcher.net is not affiliated with any of these links)

### Removing untracked files

Sometimes there are files that are created at runtime, that weren't installed with the game or any mods.

You can click "Clean untracked files" to get rid of some of them:

<img width="508" height="630" alt="image" src="https://github.com/user-attachments/assets/3b505819-d42d-4959-bb67-6c68ad625d61" />

This menu will also appear once you've uninstalled every mod.

As you can see it currently includes a lot of runtime generated Melonloader files. This will be fixed in the future.

### Configuring it to your liking
Click "settings." Descriptions of what each setting does are included on the right panel of the settings menu.

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

Using this theme causes the text to become invisisble. It's because I don't fully understand the dwmextendframeintoclientarea method
and black text gets rendered as invisible. So I just opted to set the text to white so it would work with titlebar blurring software

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


### Source of "Hello World" plugin in v1.1.1 and before
https://github.com/Gameknight963/launcher.NET-hello-world-plugin

The Hello World Plugin has been moved to this repo (under Plugins/) in any version past v1.1.1.
