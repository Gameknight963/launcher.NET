**Changes in v1.0.0 (previous version v0.8.1):**

- **IGameInstaller.cs:**
  - You no longer have to call a function to register your plugin. The loader will do it for you automatically.
  - You can now choose when the launcher will prompt for a label using `PromptForLabel`.
  - You must now implement `IEnumerable<string>? GetReleases()`. This replaces registering your plugin with it's version list.
- **PluginRegistry.cs (formerly GameInstallerRegistry.cs):**
  - `RegisterGameInstaller` is now internal `Register`, since it is only used by the loader
  - You can now some informations about which plugins are loaded with `GameInstallPlugins`, `LauncherPlugins`, `PluginDescriptors`, and `PluginsWithSettings`.
- **Other:** You now need to add the `LauncherPluginAttribute` to your assembly in order to write a plugin.

**Changes in v2.0.0 (previous version v1.0.0):**
 - `GameInstallerBase.PromptForLabel` now defaults to BeforeInstall rather than Never.

# launcher.net plugin development guide

This guide will walk you through the process of developing a launcher.net plugin to install any game you're capable of programming an installations script for.

## 1. Setting up

- Create a new Visual Studio Class Library project, target framework .NET 10. You can use any IDE you like or even .NET CLI, but this tutorial will cover Visual Studio.

- Set your target framework to `net10.0-windows` instead of `net10.0`. 

- Right click Dependencies > Add Project Reference. Open the folder where you have extracted launcher.net, and reference "launcherdotnet.dll"
  
  You've set up your project! Keep following along for it to actually do something.

### 2. Hello World

- Make your class inherit from ILauncherPlugin, after adding the necessary using directives:

```csharp
using launcherdotnet.PluginAPI;

namespace ClassLibrary1
{
    public class Class1 : ILauncherPlugin
    {

    }
}
```

IGameInstaller is an **interface.** An interface defines a **contract** that a class must follow, specifying methods, properties, or events without providing their implementation.

- Implement the members of the interface. You can hover over 'ILauncherPlugin' to see what they are. Also, add the LauncherPlugin attribute as shown.

```csharp
using launcherdotnet.PluginAPI;

[assembly: LauncherPlugin(typeof(ClassLibrary1.Class1),
    ""agatrraaAAAAA"",
    "bro bro bro bro bro bro bro bro bro",
    "1.0.0")]

namespace ClassLibrary1
{
    public class Class1 : ILauncherPlugin
    {
        public Task Initialize()
        {
            return Task.CompletedTask
        }
    }
```

The LauncherPluginAttribute is constructed like this:
```csharp
public LauncherPluginAttribute(Type entryType, string name, string description, string targetApiVersion) { ... }
```
`entryType` is the type of the class that holds your plugin. Name and description should be self-explanatory. For `targetApiVersion`: in order to get the API version of your install of launcher.net, open it, go to Settings > About > under launcher.net Plugin API. Make sure it's a valid Semantic version otherwise the loader will fail to parse it.

launcher.net compares the major versions of what API plugins target to the current API to determine whether they are compatible. This check can be disabled in Settings, but it will probably just throw ReflectionTypeLoadException on loading the plugin (which is caught by the loader, not unhandled, mind you)

To say Hello World just do ``PluginLogger.Msg("hello world!");``

```csharp
using launcherdotnet.PluginAPI;

[assembly: LauncherPlugin(typeof(ClassLibrary1.Class1),
    ""agatrraaAAAAA"",
    "bro bro bro bro bro bro bro bro bro",
    "1.0.0")]

namespace ClassLibrary1
{
    public class Class1 : ILauncherPlugin
    {
        public async Task Initialize()
        {
            PluginLogger.Msg("hello world!"); 
        }
    }
}
```

 Now build your project, take the dll and put it in the Plugins folder.

 **IMPORTANT!** In order for this log to show up, you must have the **verbose logging** option enabled under Advanced. If you want to force it to show, pass the force parameter:

```csharp
PluginLogger.Msg("this message is forced to send", true);
```

You probably shouldn't use this unless something somewhat important has happened. Respect the user's preference about logging verbosity, but don't log too little, or nothing will appear in the console.

The 'verbose logging' section is basically a spam gate option. I mean don't like log on every byte you download but you get what I mean.

### 3. Game installer

- The plugin you've just made isn't all that interesting. To make a game installer, you can implement the interface 
  `IGameInstaller`, but for this tutorial, we'll inherit from `GameInstallerBase` which is a subclass of IGameInstaller that
  reduces boilerplate.
  If you can't use an abstract class since you need a specific one, then you should use IGameInstaller directly.
  
```csharp
using launcherdotnet.PluginAPI;

[assembly: LauncherPlugin(typeof(ClassLibrary1.Class1),
  ""agatrraaAAAAA"",
  "bro bro bro bro bro bro bro bro bro",
  "1.0.0")]

namespace ClassLibrary1
{
    public class Class1 : GameInstallerBase
    {
        // GameInstallerBase has this as a virutal method instead
        public override async Task Initialize()
        {
            PluginLogger.Msg("hello world!");
        }

        // Required abstract method
        public override string GameName => "agatrraaAAAAA 3.0";

        // Another abstract method
        public override async Task<PluginGameInfo> Install(string installDir, IProgress<double> progress, IProgress<string> status)
        {
            // your install logic goes here
            // make sure to return the PluginGameInfo!
            // use progress and status to display information on the installer window.
            return new PluginGameInfo
            {
                ExePath = "some path"
            }
        }
        // This is virtual but I choose to override it here
        // Neturn null to signal that your plugin shouldn't have a version selector. This is the default behavior if you don't override it.
        public override IEnumerable<string>? GetReleases() => new List<string> { "1.0.0" };
    }
}
```

Lets go over each member of GameInstallerBase to explain what they do:

```csharp
public virtual Task Initialize() => Task.CompletedTask;
```

This is when you should fetch any info (such as a version list) for your plugin. 
Called exactly once when the plugin is loaded, before any other plugin methods are called.

```csharp
public virtual IEnumerable<string>? GetReleases() => null;
```

The releases for your plugin. Returning null signifies it shouldn't have a version selector. 
This doesn't have to be just versions, it could also be a list of games for example, in the case of plugins such
as Steam Game Copier. I opted for a custom UI there though since I wanted to display some additional info

You should not fetch your releases over the internet here. Fetch and cache them in Initialize. This is a synchronous method
to prevent you from doing that without freezing the thead annoyingly.

Instead of generating the version list as I do in my example, 
you'll probably want to fetch it from the API of the 
server where you're getting your game from. 
If you don't know what an API is, God help you.

```csharp
public virtual LabelQueryTime PromptForLabel => LabelQueryTime.BeforeInstall;
```

When the installer dialog should prompt for a label. Either before install, after install, or never. 
If never, **you need to provide one yourself** in PluginGameInfo. You should use 
`launcherdotnet.Launcher.LauncherDialogs.QueryLabel` to get a label for consistency.

```csharp
public abstract string GameName { get; }
```
This one's simple. It's the name of the game your plugin installs.

```csharp
public abstract Task<PluginGameInfo?> Install(
    string installDir,
    IProgress<double> progress,
    IProgress<string> status,
    string? version = null);
```
Called on game installation. Install your game to `installDir`, reporting progress and status with `progress` and `status` respectively.

`version` is the version the user selected to install. If you defined GetReleases to return null, `version` is null. Otherwise, it is never null.

PluginGameInfo is defined like this:

```csharp

    public class PluginGameInfo
    {
        // The executable used to launch your game.
        public required string ExePath;
        /// Whether the game should be run using a cmd command.
        public bool RunWithCmd = false;
        // The slug used by Thunderstore APIs to identify your game.
        public string? ThunderstoreCommunitySlug;
        // Whether the launcher.net's mod manager should be enabled for this game. Defaults to true.
        public bool ModManageable = true;
        // The label this game will have. Override's the user's selection, so only specify if you're using LabelQueryTime.Never
        public string? Label;
        // The name of this game (Lethal Company, Repo, etc). Leaving it blank will default to IGameInstaller.GameName
        public string? GameName;
    }
```

To install your game, get it from wherever you want, however you want, and put it's game files inside installDir. As an example, here's an empty exe called "thing.exe":

```csharp
public override async Task<PluginGameInfo> Install(string installDir, ReleaseInfo release, IProgress<double> progress, IProgress<string> status, string? version = null)
{
    string path = Path.Combine(installDir, "thing.exe");
    File.WriteAllBytes(path, Array.Empty<byte>());
    return new PluginGameInfo(path);
}
```

## Tools

The Plugin API has a few useful tools you could use:

- ``PluginTools.FindGameExe``: Finds the most likely game EXE in a folder
  
- ``PluginTools.ToThunderstoreSlug``: Guesses the Thunderstore slug of a game from the name you pass

- ``PluginTools.CopyDirectoryWithProgress``: Copies a directory with IProgress<int> for progress bar and IProgress<string> for status

- ``PluginTools.FormatSize``: Formats an amount of bytes as a readable string (eg. 5GB.) These are Explorer units meaing that
  despite being in GB, it actually means GiB. For an interesting read on the topic, check out this blog post:
  <br>
  https://devblogs.microsoft.com/oldnewthing/20090611-00/?p=17933

- ``LauncherApiInfo.ApiVersion``: The current API version, if you need it at runtime for whatever reason.

- ``launcherdotnet.Launcher.Settings.LauncherConstants``: Information about launcher.net, either `const` or `readonly`. Here is some useful information in here:
  
  - ``TempDir``: A temporary directory where you can put things like zip files to keep them separate from the main game's files. 
  
  - ``BaseDir``: Shorthand for AppDomain.CurrentDomain.BaseDirectory
  
  - ``CurrentVersion``: The current version of launcher.net.
 
  - ``AppIcon``: The Icon launcher.net uses for most of it's forms. Feel free to use it, or use your own.

### Using InstanceTempDir

The plugin API provides a class InstanceTempDir that implements `IDisposable`. When you create an InstanceTempDir, it gives you a fresh temp folder. When you’re done and dispose of it, it automatically deletes that folder.

To use it:

1. Create a new InstanceTempDir inside a using block. 

2. Use its Path property to read or write files in the temporary folder. 
   
   When the block ends, the folder and its contents are automatically deleted.

Here's an example:

```csharp
using (InstanceTempDir tempDir = new InstanceTempDir())
{
    string filePath = System.IO.Path.Combine(tempDir.Path, "myfile.txt");

    // write something
    System.IO.File.WriteAllText(filePath, "Hello world");

    // do whatever operations you need inside the temp folder
    Console.WriteLine($"Temp folder: {tempDir.Path}");
} // disposed automatically here
```
You can also use simple using statements which the compiler will tell you to use or just call Dispose manually when you're done with it.

**Why use `InstanceTempDir` instead of writing to a temp folder manually?**

`InstanceTempDir` automatically creates a unique temporary folder and ensures it’s cleaned up when you’re done. This prevents clutter, avoids accidental overwrites, and handles errors safely. Due to this, you are strongly encouraged to use InstanceTempDir.

## I want synchronous methods!

No. Don't. It'll block the UI thread. The only place it's useful is Initialize, if you want to ignore it:

```csharp
Task Initialize()
{
    return Task.CompletedTask;
}
```

### Note:
I made all of the code examples without an IDE, so they may be slightly wrong lol. Adjust accordingly.
