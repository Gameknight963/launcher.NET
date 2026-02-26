Changes since the last API version (v0.5.0):

- **IGameInstaller.cs:** IGameInstaller.Install is now type ``Task<PluginGameInfo>``.  PluginGameInfo (inside PluginGameInfo.cs) is a class containing string ExePath and bool RunWithCmd, whose functionality should be self-explanatory.
- **GameInstallerRegistry.cs (formerly PluginApi.cs):** Class PluginApi renamed to GameInstallerRegistry. This was just renamed.
- **ILauncherPlugin.cs:** ILauncherPlugin.Initialize() is now of type ``Task``. For synchronous methods, return ``Task.CompletedTask`` at the end of Initialize(). For asynchronous methods, nothing needs to change, just make sure it has ``async`` modifier and is of type ``Task``

# launcher.NET plugin development guide

This guide will walk you through the process of developing a launcher.NET plugin to install any game you're capable of programming an installations script for.

## 1. Setting up

- Create a new Visual Studio Class Library project, target framework .NET 10. You can use any IDE you like, but this tutorial will cover Visual Studio.

- Go to your csproj properties, search for "target OS", and set it to Windows. This will stop visual studio from complaining about "reachable on all platforms" bs.

- Right click Dependencies > Add Project Reference. Open the folder where you have extracted launcher.NET, and reference "launcherdotnet.dll" and "Semver.dll"
  
  You've set up your project! Keep following along for it to actually do something.

### 2. Hello World

- Make your class inherit from ILauncherPlugin, after adding the necessary using directives:

```csharp
using launcherdotnet.PluginAPI;
using Semver;

namespace ClassLibrary1
{
    public class Class1 : ILauncherPlugin
    {

    }
}
```

IGameInstaller is an **interface.** An interface defines a **contract** that a class must follow, specifying methods, properties, or events without providing their implementation.

- Implement the members of the interface. You can hover over 'ILauncherPlugin' to see what they are.

```csharp
using launcherdotnet.PluginAPI;
using Semver;

namespace ClassLibrary1
{
    public class Class1 : ILauncherPlugin
    {
        public string Name => "agatrraaAAAAA";
        public string Description => "bro bro bro bro bro bro bro bro bro";
        public global::Semver.SemVersion TargetApiVersion => new SemVersion(0, 5, 0); // v0.5.0

        public Task Initialize()
        {

        }
    }
```

I would hope that Name and Description are pretty self-explanatory. In order to get the API version of your install of launcher.NET, open it, go to Settings > About > under launcher.NET Plugin API.

launcher.NET compares the major versions of plugins to the API to determine whether they are compatible. However, on a major version of 0, breaking changes may happen with any update. Use at your own risk.

  To say Hello World just do ``PluginLogger.Msg("hello world!");``

```csharp
using launcherdotnet.PluginAPI;
using Semver;

namespace ClassLibrary1
{
    public class Class1 : ILauncherPlugin
    {
        public string Name => "agatrraaAAAAA";
        public string Description => "bro bro bro bro bro bro bro bro bro";
        public global::Semver.SemVersion TargetApiVersion => new SemVersion(0, 5, 0); // v0.5.0

        // I will get synchronous functions later
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

 You probably shouldn't use this unless something bad or otherwise really important has happened. Respect the user's preference about logging verbosity.

### 3. Game installer

- The plugin you've just made isn't all that interesting. To make a game installer, implement the interface IGameInstaller:
  
  ```csharp
  using launcherdotnet.PluginAPI;
  using Semver;
  
  namespace ClassLibrary1
  {
      public class Class1 : IGameInstaller
      {
          public string Name => "agatrraaAAAAA installer";
          public string Description => "bro bro bro bro bro bro bro bro bro (slowed and reverb)";
          public global::Semver.SemVersion TargetApiVersion => new SemVersion(0, 5, 0);
  
          public async Task Initialize()
          {
              PluginLogger.Msg("hello world!");
          }
  
          // --- new stuff ---
  
          public string GameName => "agatrraaAAAAA 3.0";
  
          public async Task<PluginGameInfo> Install(string installDir, IProgress<double> progress, IProgress<string> status)
          {
              // your install logic goes here
              // make sure to return the PluginGameInfo!
              // use progress and status to display information on the installer window.
              return new PluginGameInfo("my path");
          }
      }
  }
  ```

PluginGameInfo's constructor is defined as:

```csharp
    public PluginGameInfo(string exePath, bool runWithCmd = false)
    {
        ExePath = exePath;
        RunWithCmd = runWithCmd;
    }
```

So, when creating your new PluginGameInfo:

```csharp
new PluginGameInfo(/* Path */ "my path", /* RunWithCmd */ false);
```

Note that the second parameter ``RunWithCmd`` is optional and defaults to ``false``.

Don't worry about implementing Install() just yet.

Inside Initialize(), you'll need to get your list of versions, and add your plugin to the game installer registry. Here is an example with one version, 1.0.0:

```csharp
public async Task Initialize()
{
    PluginLogger.Msg("hello world!");
    ReleaseInfo version = new ReleaseInfo { Version = new SemVersion(1, 0, 0) };
    List<ReleaseInfo> versionList = new List<ReleaseInfo> { version };
    GameInstallerRegistry.RegisterGameInstallPlugin(this, versionList);
}
```

**Note:** There will probably be a dedicated Update() function for this soon. Doing it in Initialize() is limited, as it requires launcher.NET to restart to receive game updates.

Instead of generating the version list as I do, you'll probably want to fetch it from the API of the server where you're getting your game from. If you don't know what an API is, God help you.

To install your game, get it from wherever you want, however you want, and put it's game files inside installDir. As an example, here's an empty exe called "thing.exe":

```csharp
public async Task<PluginGameInfo> Install(string installDir, ReleaseInfo release, IProgress<double> progress, IProgress<string> status)
{
    string path = Path.Combine(installDir, "thing.exe");
    File.WriteAllBytes(path, Array.Empty<byte>());
    return new PluginGameInfo(path);
}
```

## Tools

The Plugin API has a few useful tools you could use:

- ``PluginTools.FindGameEXE``: Finds the most likely game EXE in a folder

- ``LauncherApiInfo.ApiVersion``: The current API version, if you need it at runtime for whatever reason.

- ``PluginConfig``: Information about launcher.NET. Here is some useful information in here:
  
  - ``TempDir``: A temporary directory where you can put things like zip files to keep them separate from the main game's files. 
  
  - ``BaseDir``: Shorthand for AppDomain.CurrentDomain.BaseDirectory
  
  - ``CurrentVersion``: The current version of launcher.NET.

Pretty much everything in the launcherdotnet namespace is marked as internal, so you won't be able to access it (at least, not easily. Obviously I can't stop you from doing some low level pointer stuff)

### Using InstanceTempDir

The plugin API provides a class InstanceTempDir. When you create an InstanceTempDir, it gives you a fresh temp folder. When you’re done and dispose of it, it automatically tries to delete that folder.

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

**Why use `InstanceTempDir` instead of writing to a temp folder manually?**

`InstanceTempDir` automatically creates a unique temporary folder and ensures it’s cleaned up when you’re done. This prevents clutter, avoids accidental overwrites, and handles errors safely. Due to this, you are strongly encouraged to use InstanceTempDir.

## I want synchronous methods!

No. Don't. It'll block the UI thread.

#### I still want synchronous methods!

Fine. 

For Tasks with return types:

```csharp
public Task<PluginGameInfo> Install(string installDir, ReleaseInfo release, IProgress<double> progress, IProgress<string> status)
{
    string resultPath = "/path/idk/manwth";
    return Task.FromResult(new PluginGameInfo(resultPath));
}
```

For normal Tasks:

```csharp
Task ILauncherPlugin.Initialize()
{
    ReleaseInfo version = new ReleaseInfo { Version = new SemVersion(1, 0, 0) };
    List<ReleaseInfo> versionList = new List<ReleaseInfo> { version };
    GameInstallerRegistry.RegisterGameInstallPlugin(this, versionList);
    return Task.CompletedTask;
}
```

### Practices:

- Do not include more than one plugin per assembly (i.e. do not inherit from either IGameInstaller or ILauncherPlugin more than once). IGameInstaller inherits from ILauncherPlugin, so you do not lose any features using it. Although it is technically possible to do it, it reduces user control over exactly which plugins they choose to use.

- Catch your exceptions, and throw your own, more user-friendly ones (or, better yet, use a MessageBox.) launcher.NET catches exceptions for you and displays a MessageBox with the exception message.

- Listen to the arguments that Install() passes. If you don't, something bad will probably happen. launcher.NET is still unstable.

## Notes

- If it wasn't made clear, this API is in an ALPHA state! Expect breaking changes anytime!
