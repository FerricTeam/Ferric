# Ferric
Plugin framework for Facepunch's Rust.

# Disclaimer
### This is in no way finished, take the current version as a demo.

## Compiling from source
1. Build Ferric.Injection, Ferric.Patcher and Ferric
2. Put Ferric.Injection.dll, Ferric.Patcher.exe and the server assembly (Assembly-CSharp.dll) in a folder.
3. Replace (RustDir)\RustDedicated_Data\Managed\Assembly-CSharp.dll with the newly generated Assembly-CSharp-Ferric.dll
4. Create a new folder called (RustDir)\Ferric and drag Ferric.dll into it.
5. Create a new folder called (RustDir)\Ferric\Dependencies and drag Newtonsoft.json.dll found [here](https://github.com/JamesNK/Newtonsoft.Json/releases/download/13.0.1/Json130r1.zip) into it.
6. Ferric should now be installed when you run the server.

## Installing plugins:
- Drag the plugin dll into the (RustDir)\Ferric\Plugins folder.
- Restart/Start the server to generate configs.

## TODO:
- Documentation
- A shitton of events
- A shitton more wrapper classes
- APIs

## For developers:
- Plugins are class library targeting .net framework 4.7.2 (other version probably work aswell)
- Make a main plugin class that inherits from Ferric.API.Features.Plugin
- Override ID, Author, Name, Version, RequiredFerricVersion.
- To generate a config:
  - Create a new config class that inherits from Ferric.API.Features.Config.
  - Add any fields or properties you want (make sure to make them plugin)
  - Override the Config property in the Plugin class to be a instance of your class (override Config Config = new MyConfig)
- To use the config:
  - Cast the config to your custom config (config as MyConfig)
  - Recommended to make a method for that (in Plugin class: ```public MyConfig GetConfig => Config as MyConfig```)
- Subscribing to events:
 - Subscribe to the C# actions in the corresponding handler (server-related events in the ```ServerHandler``` class)
   - You will typically want to do this in ```OnEnabled``` and its considered good practice to unsubscribe from everything in ```OnDisabled``` (called on server shutdown - use it to save any data)

## For people looking to contribute:
Here is a quick rundown of how Ferric works:

- The Patcher takes the Injection class and puts it inside the server assembly.
- Then it will add a call in Bootstrap::StartServer to Injection:.Start
- ```Injection::Start``` checks if it can find the Ferric folder and Ferric.dll and calls ```Loader::LoadAll``` in Ferric.dll
- ```Loader::LoadAll``` does a few things:
  - It checks the dependency folder and load all dlls in it
    - If it cannot find all of the dlls specified in an array it will cancel the initialization (currently: Newtonsoft.json)
  - It checks the plugin folder for plugins and loads them (not enable)
  - Creates a harmony instance and calls ```HarmonyInstace::PatchAll```
  - Then it will call ConfigManager which does:
    - Check the config folder for files matching loaded plugins ID's
    - Deserialize the file into the plugins config type
    - Set the plugins config to the deserialized object
  - Lastly the Loader calls ```OnEnabled``` in all loaded plugins (if the config has ```Enabled = true``` and the ```RequiredFerricVersion``` matches)
- When an patched event method is called the corresponding handler method is called which invokes the event.

Check todo for what you can do.
Bug reports, suggestions and pull requests are welcome.
