#pragma warning disable SA1401 // Fields must be private

namespace Ferric
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Ferric.API.Attributes;
    using Ferric.API.CommandSystem;
    using Ferric.API.EventSystem;
    using Ferric.API.Features;
    using Ferric.API.Interfaces;
    using Ferric.API.Wrappers;
    using Ferric.Patches;
    using JetBrains.Annotations;
    using Console = Ferric.API.Wrappers.Console;

    /// <summary>
    /// Loads plugins.
    /// </summary>
    public static class Loader
    {
        /// <summary>
        /// The directory location of the ferric assembly.
        /// </summary>
        public static readonly string FerricDir = Directory.GetParent(Assembly.GetExecutingAssembly().Location)!.FullName;

        /// <summary>
        /// The ferric version of the assembly.
        /// </summary>
        public static readonly Version Version = Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// A list of all plugins.
        /// </summary>
        public static List<IPlugin> Plugins = new();

        /// <summary>
        /// A dictionary containing assemblies and their plugin instances.
        /// </summary>
        public static Dictionary<Assembly, IPlugin> PluginAssemblies = new();

        private const string SplashText = @"
            ______             _      
            |  ___|           (_)     
            | |_ ___ _ __ _ __ _  ___ 
            |  _/ _ \ '__| '__| |/ __|
            | ||  __/ |  | |  | | (__ 
            \_| \___|_|  |_|  |_|\___|
        ";

        /// <summary>
        /// An array of required dependencies.
        /// </summary>
        private static readonly string[] Dependencies =
        {
            "Newtonsoft.Json",
        };

        /// <summary>
        /// Initializes ferric.
        /// </summary>
        public static void LoadAll()
        {
            AssemblyName currentAssemblyName = Assembly.GetExecutingAssembly().GetName();

            Console.Warn($"{currentAssemblyName.Name} - v{currentAssemblyName.Version}");

            try
            {
                ConfigManager.LoadFerricConfig();
            }
            catch (Exception e)
            {
                Console.Error("Failed to load ferric config, aborting...");
                Console.Error(e.Message);
                return;
            }

            string dependenciesFolder = ConfigManager.FerricBootConfig.Instance.DependenciesFolder;
            string pluginFolder = ConfigManager.FerricBootConfig.Instance.PluginFolder;
            string configFolder = ConfigManager.FerricBootConfig.Instance.ConfigsFolder;

            if (!Directory.Exists(dependenciesFolder))
            {
                Console.Error($"Cannot find dependencies folder {dependenciesFolder}, aborting!");
                return;
            }

            List<string> dependenciesToLoad = Dependencies.ToList();
            foreach (string lib in Directory.GetFiles(dependenciesFolder, "*.dll"))
            {
                try
                {
                    Assembly assembly = Assembly.Load(File.ReadAllBytes(lib));
                    AssemblyName name = assembly.GetName();
                    dependenciesToLoad.Remove(name.Name);
                    Console.Info($"Loaded dependency {name.Name} - v{name.Version}");
                }
                catch (Exception e)
                {
                    Console.Error($"Could not load dependency {lib}: {e}");
                }
            }

            if (dependenciesToLoad.Count > 0)
            {
                Console.Error($"Missing required dependency: {string.Join(", ", dependenciesToLoad)}; aborting!");
                return;
            }

            Console.Info("Enabling harmony patches");
            Patcher.PatchAll($"ferric.{currentAssemblyName.Version}");

            Console.Warn("Loading plugins");
            Directory.CreateDirectory(pluginFolder);
            foreach (string lib in Directory.GetFiles(pluginFolder, "*.dll"))
            {
                try
                {
                    Assembly assembly = Assembly.Load(File.ReadAllBytes(lib));
                    IPlugin plugin = MakePlugin(assembly);
                    if (plugin is null)
                    {
                        Console.Error($"{assembly.GetName().Name} is not a valid plugin!");
                        continue;
                    }

                    if (plugin.ID is null)
                    {
                        Console.Error($"Plugin {lib} id is null!");
                        continue;
                    }

                    IPlugin duplicate;
                    if ((duplicate = GetPlugin(plugin.ID.ToLower())) is not null)
                    {
                        Console.Error($"Cannot load {plugin.Name} because another plugin is already registered with the same ID ({plugin.Name}-{plugin.ID.ToLower()};{duplicate.Name}-{duplicate.ID.ToLower()})!");
                        continue;
                    }

                    plugin.Assembly = assembly;
                    Plugins.Add(plugin);
                    PluginAssemblies.Add(assembly, plugin);

                    Console.Info($"Loaded plugin {plugin.Name} v{plugin.Version} by {plugin.Author}");
                }
                catch (Exception e)
                {
                    Console.Error($"Could not load plugin {lib}: {e}");
                }
            }

            if (!Directory.Exists(configFolder))
            {
                Console.Warn("Cannot find configs, creating defaults...");
                Directory.CreateDirectory(configFolder);
                ConfigManager.GenerateDefaultConfigs();
            }

            ConfigManager.LoadPluginConfigs();

            CommandSystem.Init();

            foreach (var plugin in Plugins)
            {
                try
                {
                    if (plugin.Config.Enabled)
                    {
                        plugin.OnEnabled();
                        if (plugin.IsModded && !Server.IsModded)
                            Server.IsModded = true;
                        RegisterEventAttributes(plugin);
                        Console.Info($"{plugin.Name} by {plugin.Author} - v{plugin.Version} has been enabled");
                        continue;
                    }

                    Console.Warn($"Not enabling {plugin.Name} by {plugin.Author} - v{plugin.Version} because it is disabled!");
                }
                catch (Exception e)
                {
                    Console.Error($"{plugin.Name} threw an error while enabling: {e}");
                }
            }

            Console.Info($"Welcome to {SplashText}");
        }

        /// <summary>
        /// Returns a plugin via its <see cref="IPlugin.ID"/>.
        /// </summary>
        /// <param name="id">The <see cref="IPlugin.ID"/> of the plugin.</param>
        /// <returns>The <see cref="IPlugin"/> instance matching the <see cref="IPlugin.ID"/>. Can be null.</returns>
        [CanBeNull]
        public static IPlugin GetPlugin(string id) => Plugins.FirstOrDefault(x => string.Equals(x.ID, id, StringComparison.CurrentCultureIgnoreCase));

        /// <summary>
        /// Registers all event attributes of a plugin.
        /// </summary>
        /// <param name="plugin">The plugin to register them for.</param>
        public static void RegisterEventAttributes(IPlugin plugin)
        {
            var types = new List<object>()
            {
                plugin,
            };

            foreach (var field in plugin.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
            {
                if (field.GetCustomAttributes(typeof(EventHandlerAttribute)).Count() != 0)
                {
                    types.Add(field.GetValue(plugin));
                }
            }

            foreach (var obj in types)
            {
                var type = obj.GetType();
                var publicMethods = type.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static);
                var privateMethods = ConfigManager.ConfigFerric.CheckPrivateMethods.Value
                    ? type.GetMethods(BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static)
                    : new MethodInfo[] { };
                for (int i = 0; i < publicMethods.Length + privateMethods.Length; i++)
                {
                    var method = i >= publicMethods.Length ? privateMethods[i - publicMethods.Length] : publicMethods[i];
                    var eventAttributes = method.GetCustomAttributes(typeof(EventAttribute)).ToArray();

                    if (eventAttributes.Length != 0)
                    {
                        foreach (var attribute in eventAttributes)
                        {
                            try
                            {
                                RegisterAttribute(attribute, method, obj);
                            }
                            catch (Exception e)
                            {
                                Console.Error($"Could not subscribe method {method.Name} to event {(attribute as EventAttribute)?.EventType.ToString() ?? "null"}");
                                Console.Error(e);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Disables all plugins.
        /// </summary>
        internal static void Shutdown()
        {
            foreach (var plugin in Plugins)
            {
                try
                {
                    plugin.OnDisabled();
                }
                catch (Exception e)
                {
                    Console.Error($"{plugin.Name} threw an error disabling: {e}");
                }
            }
        }

        private static void RegisterAttribute(Attribute attribute, MethodInfo method, object instance)
        {
            if (attribute is EventAttribute eventAttribute)
                AddToInvocationList(eventAttribute, method, instance);
            else
                throw new ArgumentException("Event attribute is not IEventAttribute, Ferric skill issue?", nameof(attribute));
        }

        private static void AddToInvocationList(EventAttribute attribute, MethodInfo methodInfo, object instance)
        {
            var mapping = EventTypeMappings.GetMapping(attribute.EventType) ??
                          throw new KeyNotFoundException(
                              $"Could not find mapping for event type {attribute.EventType}, is Ferric outdated?");
            var parameter = methodInfo.GetParameters().FirstOrDefault() ?? throw new ArgumentException("Argument count cannot be 0!");
            if (parameter.ParameterType != mapping.EventArgs)
            {
                throw new ArgumentException($"Argument type was {parameter.ParameterType.FullName}, expected: {mapping.EventArgs.FullName}");
            }

            if (!EventTypeMappings.ExtendedSubscribersMap.TryGetValue(attribute.EventType, out var methodList))
            {
                EventTypeMappings.ExtendedSubscribersMap.Add(attribute.EventType, new (object ClassInstance, MethodInfo[] MethodInfos)[] { (instance, new[] { methodInfo }) });
                return;
            }

            if (methodList.All(x => x.ClassInstance != instance))
            {
                EventTypeMappings.ExtendedSubscribersMap[attribute.EventType] = methodList.Append((instance, new[] { methodInfo })).ToArray();
                return;
            }

            int i = -1;
            foreach (var touple in EventTypeMappings.ExtendedSubscribersMap[attribute.EventType])
            {
                i++;
                if (touple.ClassInstance != instance)
                {
                    continue;
                }

                var valueTuple = touple;
                valueTuple.MethodInfos = touple.MethodInfos.Append(methodInfo).ToArray();
                EventTypeMappings.ExtendedSubscribersMap[attribute.EventType][i] = valueTuple;
                break;
            }
        }

        /// <summary>
        /// Creates and returns a new <see cref="IPlugin"/> instance using an plugin assembly.
        /// </summary>
        /// <param name="assembly">The plugin assembly.</param>
        /// <returns>The <see cref="IPlugin"/> instance.</returns>
        private static IPlugin MakePlugin(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;
                if (!(type.GetInterfaces().Contains(typeof(IPlugin)) || type.IsSubclassOf(typeof(Plugin))))
                    continue;

                IPlugin plugin = null;
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor is not null)
                {
                    plugin = constructor.Invoke(null) as IPlugin;
                }

                if (plugin is null)
                {
                    Console.Error($"Cannot instantiate {assembly.GetName().Name} because it does not have a accessible default constructor!");
                    continue;
                }

                if (plugin.RequiredFerricVersion is not null && !CheckFerricVersion(plugin))
                {
                    Console.Warn($"{plugin.Name} required ferric version does not match current version ({plugin.RequiredFerricVersion} vs {Version}), it wont be enabled!");
                    continue;
                }

                return plugin;
            }

            return null;
        }

        /// <summary>
        /// Checks if the plugin is compatible with the current ferric version.
        /// </summary>
        /// <param name="plugin">The plugin to check.</param>
        /// <returns>Whether or not the plugin is compatible with the current ferric version.</returns>
        private static bool CheckFerricVersion(IPlugin plugin) => plugin.RequiredFerricVersion.Major == Version.Major;
    }
}