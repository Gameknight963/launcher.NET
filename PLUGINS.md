WARNING!! This guide has not been updates for plugin API v0.6.0! Refer to the source code when there's a discrepancy. Here are some changes I know off the top of my head:

 - **IGameInstaller.cs:** It is now type ``Task<PluginGameInfo>``.  PluginGameInfo (inside PluginGameInfo.cs) is a class containing string ExePath and bool RunWithCmd.
 - **GameInstallerRegisty.cs (formerly PluginApi.cs):** Class PluginApi renamned to GameInstallerRegisty
 - **ILauncherPlugin.cs:** ILauncherPlugin.Initialize() is now of type ``Task``. For synchronous methods, return ``Task.CompletedTask`` at the end of Initialize(). For asynchronous methods, nothing needs to change, just make sure it has ``async`` modifier

# launcher.NET plugin development guide

This guide will walk you through the process of developing a launcher.NET plugin to install any game you're capable of programming an installations script for.

## 1. Setting up

- Create a new Visual Studio Class Library project, target framework .NET 10. You can use any IDE you like, but this tutorial will cover Visual Studio.

- Go to your csproj properties, search for "target OS", and set it to Windows. This will stop visual studio from complaining about "reachable on all platforms" bs.

- Right click Dependencies > Add Project Refrence. Open the folder where you have extracted launcher.NET, and refrence "launcherdotnet.dll" and "Semver.dll"
  
  You've set up your project! Keep following along for it to actually do something.

### 2. Hello World

- Make your class inherit from ILauncherPlugin, after adding the necessaryusing directives:
  
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

- Implement the members of the interface. You can over over 'ILauncherPlugin' to see what they are.

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

        public void Initialize()
        {

        }
    }
```

  I would hope that Name and Description are pretty self-explanatory. In order to get the API version of your install of launcher.NET, open it, go to Settings > About > under launcher.NET Plugin API.

  launcher.NET compares the major versions of plugins to the API to determine whether they are compatabile. However, on a major version of 0, breaking changes may happen with any update. Use at your own risk.

  To say Hello World just do ``PluginLogger.Log("hello world!");``

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

        public void Initialize()
        {
            PluginLogger.Log("hello world!"); 
        }
    }
}
```

  Now build your project, take the dll and put it in the Plugins folder.

  **IMPORTANT!** In order for this log to show up, you must have the **verbose logging** option enabled under Advanced. If you want to force it to show, pass the force parameter:

```csharp
PluginLogger.Log("this message is forced to send", true);
```

  You probably shouldn't use this unless something bad has happened.

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
  
          public void Initialize()
          {
              PluginLogger.Log("hello world!");
          }
  
          // --- new stuff ---
  
          public string GameName => "agatrraaAAAAA 2.0";
  
          public string Install(string installDir, IProgress<double> progress, IProgress<string> status)
          {
              // your install logic goes here
              // make sure to return the final path of the game exe!
              // use progress and status to display information on the installer window.
          }
      }
  }
  ```

Don't worry about implementing Install() just yet.

Inside Initialize(), you'll need to get your list of versions, and add your plugin to the game installer registry:

```csharp
    public void Initialize()
    {
        PluginLogger.Log("hello world!");
        List<SemVersion> versionList = new List<SemVersion> { new SemVersion(1, 0, 0) };
        PluginApi.RegisterGameInstallPlugin(this, versionList);
    }
```

  **Note:** There will probably be a dedicated Update() function for this soon. Doing it in Initialize() is limitied, as it requires launcher.NET to restart to recive game updates.

  Instead of generating the version list as I do, you'll probably want to fetch it from the API of the server where you're getting your game from. If you don't know what an API is, God help you.

  To install your game, get it from wherever you want, however you want, and put it's game files inside installDir. As an example:

```csharp
public string Install(string installDir, IProgress<double> progress, IProgress<string> status)
{
    string path = Path.Combine(installDir, "thing.exe");
    File.WriteAllBytes(path, Array.Empty<byte>());
    return path;
}
```

## Tools

The Plugin API has a few useful tools you could use:

- ``PluginTools.FindGameEXE``: Finds the most likely game EXE in a folder

- ``LauncherApiInfo.ApiVersion``: The current API version, if you need it for whatever reason.

- ``launcherdotnet.Config``: Information about launcher.NET. Here is some useful information in here:
  
  - ``TempDir``: A temporary directory where you can put things like zip files to keep them seperate from the main game's files. You are strongly encouraged to use this directory.
  
  - ``BaseDir``: Shorthand for AppDomain.CurrentDomain.BaseDirectory
  
  - ``CurrentVersion``: The current version of launcher.NET.

Everything else in launcher.NET is marked as internal, so you won't be able to access it (at least, not easily. Obviously I can't stop you from doing some pointer stuff)

### Practices:

- Do not include more than one plugin per assembly (i.e. do not inherit from either IGameInstaller or ILauncherPlugin more than once). IGameInstaller inherits from ILaunchePlugin, so you do not lose any features using it. Although it is technically possible to do it, it reduces user control over exactly which plugins they choose to use.

- Catch your exceptions. Not a huge issue since launcher.NET catches them for you but still good practice.

- Listen to the arguments that Install() passes. If you don't, something bad will probably happen. launcher.NET is still unstable.

## Notes

- If it wasn't made clear, this API is in an ALPHA state! Expect breaking changes anytime!

- Please don't use launcher.NET to pirate games
