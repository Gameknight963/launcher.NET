**Changes in v2.1.0 (previous version v2.0.1):**

- Deprecated builtin Thunderstore components, they now live in the Thunderstore plugin.
- Implement `IModSource` to add your own mod source to the game
- Use `PluginGameData<T>` (USE CRTP) to persist game-specific data between sessions

# launcher.net plugin development guide

This guide will walk you through the process of developing a launcher.net plugin to install any game you're capable of programming an installations script for.

## 1. Setting up

- Create a new Visual Studio Class Library project, target framework .NET 10. You can use any IDE you like or even .NET CLI, but this tutorial will cover Visual Studio.

- Set your target framework to `net10.0-windows` instead of `net10.0`. 

- Right click Dependencies > Add Project Reference. Open the folder where you have extracted launcher.net, and reference "launcherdotnet.dll"
  
  You've set up your project! Keep following along for it to actually do something.

### 2. Hello World

- Make your class inherit from (implement) ILauncherPlugin, after adding the necessary using directives:

```csharp
using launcherdotnet.PluginAPI;

namespace ClassLibrary1
{
    public class Class1 : ILauncherPlugin
    {

    }
}
```

IGameInstaller is an **interface.** An interface defines a **contract** that a class must follow, specifying methods, properties, etc.

- Implement the members of the interface. You can hover over 'ILauncherPlugin' to see what they are. 

```csharp
using launcherdotnet.PluginAPI;

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

To say Hello World just do ``PluginLogger.WriteLine("hello world!");``

```csharp
using launcherdotnet.PluginAPI;

namespace ClassLibrary1
{
    public class Class1 : ILauncherPlugin
    {
        public async Task Initialize()
        {
            PluginLogger.WriteLine("hello world!"); 
        }
    }
}
```

But wait! Even though this compiles, you haven't told launcher.net where to load your plugin from.

Add the LauncherPluginAttribute as shown:

```csharp
using launcherdotnet.PluginAPI;

[assembly: LauncherPlugin(typeof(ClassLibrary1.Class1),
    "agatrraaAAAAA",
    "Enter a detailed description bro bro bro bro bro bro bro bro bro",
    "2.1.0")]

namespace ClassLibrary1
{
    public class Class1 : ILauncherPlugin
    {
        public async Task Initialize()
        {
            PluginLogger.WriteLine("hello world!"); 
        }
    }
}
```

The LauncherPluginAttribute is constructed like this:
```csharp
public LauncherPluginAttribute(Type entryType, string name, string description, string targetApiVersion) { ... }
```
`entryType` is the type of the class that holds your plugin. Name should be self-explanatory. Description shows up in the left column of plugin settings when someone clicks on your plugin. For `targetApiVersion`: in order to get the API version of your install of launcher.net, open it, go to Settings > About > under launcher.net Plugin API. Make sure it's a valid Semantic version otherwise the loader will fail to parse it.

launcher.net compares the major versions of what API plugins target to the current API to determine whether they are compatible. This check can be disabled in Settings, but it will probably just throw ReflectionTypeLoadException on loading the plugin, mind you
 Now build your project, take the dll and put it in the Plugins folder.

 **IMPORTANT!** In order for this log to show up, you must have the **verbose logging** option enabled under Advanced. 

You should get something like this in your console:

```
[agatrraaAAAAA] hello world!
```

 If you want to force it to show regardless of verbose logging, pass the force parameter:

```csharp
PluginLogger.Msg("this message is forced to send", true);
```

You probably shouldn't use this unless something somewhat important has happened. Respect the user's preference about logging verbosity, but don't log too little, or nothing will appear in the console.

The 'verbose logging' section is basically intended to be used as a spam gate option. I mean don't like log on every byte you download but you get what I mean.

**_Important_**: As of launcher.net v2.0.0, `Initialize()` no longer runs on the main thread. Instead, it
now runs in parallel with all plugins' `Initialize()` methods.

If you need the main thread for non thread-safe operations, use `InitializeMainThread()` instead. 

### 3. Game installer

- The plugin you've just made isn't all that interesting. To make a game installer, you can implement the interface 
  `IGameInstaller`, but for this tutorial, we'll inherit from `GameInstallerBase` which is a base class implementing IGameInstaller,
  reducing boilerplate.
  If you can't use an abstract class since you need a base class yourself, then you should use IGameInstaller directly.
  
```csharp
using launcherdotnet.PluginAPI;

[assembly: LauncherPlugin(typeof(ClassLibrary1.Class1),
  "agatrraaAAAAA",
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
        // Return null to signal that your plugin shouldn't have a version selector. This is the default behavior if you don't override it.
        public override IEnumerable<string>? GetReleases() => new List<string> { "1.0.0" };
    }
}
```

Lets go over each member of GameInstallerBase to explain what they do:

```csharp
public virtual Task Initialize() => Task.CompletedTask;
```

This is when you should fetch any info (such as a version list) for your plugin. 
Called exactly once when the plugin is loaded, before any other plugin methods (besides InitializeMainThread) are called.

```csharp
public virtual IEnumerable<string>? GetReleases() => null;
```

The releases for your plugin. Returning null signifies it shouldn't have a version selector. 
This doesn't have to be just versions, it could also be a list of games for example, in the case of plugins such
as Steam Game Copier. I opted for a custom UI there though since I wanted to display some additional info

You should not fetch your releases over the Internet here. Fetch and cache them in Initialize. This is a synchronous method,
you will block the UI thread.

If you need to refresh your releases immediately when the user clicks for whatever reason, open an issue.

Instead of generating the version list as I do in my example, 
you'll probably want to fetch it from the API of the 
server where you're getting your game from. 
If you don't know what an API is, God help you.

```csharp
public virtual LabelQueryTime PromptForLabel => LabelQueryTime.BeforeInstall;
```

When the installer dialog should prompt for a label. Either before install, after install, or never. 
If never, **you need to provide one yourself** in PluginGameInfo. You should use 
`launcherdotnet.Launcher.LauncherDialogs.QueryLabel` to get a label for UI consistency.

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

PluginGameInfo is defined like this, excluding deprecated components:

```csharp

    public class PluginGameInfo
    {
        // The executable used to launch your game.
        public required string ExePath;

        // Whether the game should be run using a cmd command.
        public bool RunWithCmd = false;

        // The Id the GameInfo created from this PluginGameInfo will have.
        public readonly string Id = Guid.NewGuid().ToString();

        // Where PluginGameData can be stored for this game.
        public string DataDirectory => GameInfo.GetDataDirectory(Id);

        // The label this game will have. Override's the user's selection, so only specify if you're using see LabelQueryTime.Never
        public string? Label;

        // The id of the IModSource used to manage mods for this game.
        // Leave null to disable mod management.
        public string? ModManagerId;

        The name of this game (Lethal Company, Repo, etc). Not to be confused with the label of the instance.
        Leaving it blank will default to IGameInstaller.GameName.
        public string? GameName;
    }
```

To install your game, get it from wherever you want, however you want, and put it's game files inside installDir. As an example, 

here's an example that writes an empty exe called "thing.exe":

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

`InstanceTempDir` automatically creates a unique temporary folder and ensures it’s cleaned up when you’re done. 
This prevents clutter, avoids accidental overwrites, and handles errors safely. Due to this, you are strongly encouraged to use InstanceTempDir.

## I want synchronous methods!

No. Don't. It'll block the UI thread. The only place it's useful is Initialize, if you want to ignore the callback:

```csharp
Task Initialize()
{
    return Task.CompletedTask;
}
```

### Making a mod source

So you want to deal mods huh?

All you have to do is implement `IModSource`:

```csharp
using launcherdotnet.PluginAPI;

[assembly: LauncherPlugin(typeof(ClassLibrary1.Class1),
    "agatrraaAAAAA",
    "Enter a detailed description bro bro bro bro bro bro bro bro bro",
    "2.1.0")]

namespace ClassLibrary1
{
    public class Class1 : IModSource
    {
        // user-facing display name. collisions ok but may be confusing to read
        public string DisplayName => "🐠 Mod Manager";

        // Unique identifier for this mod source, used to match against GameInfo.
        // Follow the convention 'namespace.pluginname' or 'owner.pluginname'.
        public string Id => "gameknight963.🐠";

        // Return a collection of installed mods here,
        // for display in the package manager.
        public IEnumerable<InstalledMod> GetInstalledMods(GameInfo game)

        public Task Initialize()
        {
            return Task.CompletedTask;
        }

        // Open your mod browser and let the user install mods.
        public async Task OpenModBrowser(GameInfo game)

        // Uninstall all the mods in the `mods` list.
        // If you want to cancel the operation (such as the user cancelled), return `false`.
        // otherwise return true.
        public bool UninstallMods(GameInfo game, List<InstalledMod> mods)
    }
}
```

Then go into the game info editor and select your mod manager as the one for that game.

### Persisting game-specific information
(A way to persist general information is planned)

Use `PluginGameData<T>`. Since it's serialized with Newtonsoft.Json, you can
use `JsonPropertyAttribute` to change how it serializes.
```csharp
public class ThunderstoreConfig : PluginGameData<ThunderstoreConfig>
{
    [JsonProperty("installedMods")]
    public List<InstalledMod> InstalledMods { get; set; } = [];
}
```
Load it with: 
```csharp
ThunderstoreConfig.Load(GameInfo game, string SourceId)
```

SourceId is the file name (without the extension or path, just a name) of the file that gets saved.

Since this is heavily abstracted, all that matters is that if you save with a particular string, loading
with that same string gives you the file you saved.

### Note:
I made all of the code examples without an IDE, so they may be slightly wrong lol. Adjust accordingly.

### Other note:
You can have your plugins be any license, it doesn't have to necessarily be GPLv3 compliant. I don't own your plugins.
