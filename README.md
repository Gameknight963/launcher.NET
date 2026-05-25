# launcher.NET

A lightweight game launcher capable of playing **any** game, through the power of plugins. It also has complete Thunderstore integration,
making mod management easy.

<img width="715" height="508" alt="launcherdotnet_QKwqSGktTu" src="https://github.com/user-attachments/assets/259e2a73-083d-451b-b6b8-cd949609bcbc" />
<br>
It still looks good even though its winforms

### launcher.net vs prism launcher

<img width="593" height="33" alt="image" src="https://github.com/user-attachments/assets/08510937-598f-4bed-85c8-0a0bfedc6ac6" />
<br>
<img width="600" height="35" alt="image" src="https://github.com/user-attachments/assets/afe0c9c1-35f6-4074-ada5-224599658116" />

Don't get me wrong I still love prism launcher, it's just here to give you a sense of scale. launcher.net will be 
incapable of launching Minecraft for the forseeable future, it is hypothetically possible though.

<img width="606" height="33" alt="image" src="https://github.com/user-attachments/assets/8242e95b-36e0-4605-a68a-f0b85759d775" />

optimization is a dead art to some holy. anyway moving on

## Features
 - Modular plugin system
 - Thunderstore API integration
 - Create multiple instances
 - Mod managment
 - Low RAM and CPU usage
 - Dependency management
 - Themable to light mode, dark mode, and other themes

## Planned features
 - Automatic Melonloader development enviornment setup
 - Removing restrictions on games outside launcher root
 - Saving space via mod profiles instead of duplicating game installations

### but how??

Instead of implementing installation a million times for a million games, launcher.NET uses 'plugins,' which are dll files that get loaded containing implementations for installing games. As of such, it doesn't currently come with much functionality. You'll need to either script plugins to install games, or find them online. However, this system makes it fully modular.

Preinstalled plugins are planned in the next few releases.

## Installation

1. Download the latest version from Github Releases.

2. Extract it whever you want, and run ``launcherdotnet.exe``.
 
That's literally it it's just a zip. An installer is not planned. Put it somewhere nice and add a shortcut to the start menu.

## Usage
### Adding an instance

Click "+ Add new Instance," select a game and version and click Install. You will be prompted to type a name.

### Installing mods

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

In the "plugins" tab of the settings menu, click "open plugins folder." Drag any plugins into here.

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
and black text gets rendered as invisible. So I just opted to set the text to white

<img width="578" height="488" alt="image" src="https://github.com/user-attachments/assets/b5696318-8a21-4185-80a4-09a9c929a080" />

### With DWMBlurGlassInstalled

#### Extended frame

<img width="725" height="488" alt="image" src="https://github.com/user-attachments/assets/f658e9b6-db08-455f-ac96-3b9a785e09b1" />

Color will vary based on your settings.

By the way, themes still apply to Messageboxes and Inputboxes:

<img width="469" height="167" alt="image" src="https://github.com/user-attachments/assets/6f845eb7-696f-4e8c-b177-73d465f09d72" />
<br>
<img width="304" height="148" alt="image" src="https://github.com/user-attachments/assets/11b41abb-ca0c-43e0-93e1-0983aab32baf" />

I reimplemented Messagebox and Inputbox in Winforms to acheive this

### Source of "Hello World" plugin
https://github.com/Gameknight963/launcher.NET-hello-world-plugin

(this plugin will probably be moved to the source soon)
