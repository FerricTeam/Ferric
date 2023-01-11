namespace Ferric
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Ferric.API.Features;
    using Newtonsoft.Json;
    using Console = Ferric.API.Wrappers.Console;

    /// <summary>
    /// Manages configs.
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>
        /// Ferric configuration.
        /// </summary>
        public class FerricBootConfig
        {
            /// <summary>
            /// Gets the directory where Ferric is located.
            /// </summary>
            public static string FerricFolder => Loader.FerricDir;

            /// <summary>
            /// The directory location of the dependencies folder.
            /// </summary>
            public string DependenciesFolder = Path.Combine(FerricFolder, "Dependencies");

            /// <summary>
            /// The directory location of the plugin folder.
            /// </summary>
            public string PluginFolder = Path.Combine(FerricFolder, "Plugins");

            /// <summary>
            /// The directory location of the plugin configs.
            /// </summary>
            public string ConfigsFolder = Path.Combine(FerricFolder, "Configs");

            /// <summary>
            /// The location of the configuration file for ferric.
            /// </summary>
            public string FerricConfig = Path.Combine(FerricFolder, "ferric.json");

            /// <summary>
            /// Gets the Singleton.
            /// </summary>
            /// <returns>The Instance.</returns>
            public static FerricBootConfig Instance => instance ??= new FerricBootConfig();

            private static FerricBootConfig instance;
        }

        /// <summary>
        /// Gets the main Ferric config.
        /// </summary>
        public static ConfigFerric ConfigFerric { get; private set; }

        /// <summary>
        /// Loads Ferric configuration.
        /// </summary>
        public static void LoadFerricConfig()
        {
            string configFile = Path.Combine(FerricBootConfig.FerricFolder, "FerricBoot.txt");

            if (!File.Exists(configFile))
                File.Create(configFile).Close();

            using (StreamReader streamReader = new StreamReader(configFile))
            {
                string[] content = streamReader.ReadToEnd().Split('\n');
                if (content.Length != 4)
                {
                    streamReader.Dispose();
                    using (StreamWriter streamWriter = new StreamWriter(configFile))
                    {
                        streamWriter.WriteLine(FerricBootConfig.Instance.DependenciesFolder);
                        streamWriter.WriteLine(FerricBootConfig.Instance.PluginFolder);
                        streamWriter.WriteLine(FerricBootConfig.Instance.ConfigsFolder);
                        streamWriter.WriteLine(FerricBootConfig.Instance.FerricConfig);
                    }
                }
                else
                {
                    FerricBootConfig.Instance.DependenciesFolder = content[0];
                    FerricBootConfig.Instance.PluginFolder = content[1];
                    FerricBootConfig.Instance.ConfigsFolder = content[2];
                    FerricBootConfig.Instance.FerricConfig = content[3];
                }
            }

            if (!File.Exists(FerricBootConfig.Instance.FerricConfig))
                File.WriteAllText(FerricBootConfig.Instance.FerricConfig, JsonConvert.SerializeObject(new ConfigFerric()));
            using (var sr = new StreamReader(FerricBootConfig.Instance.FerricConfig))
            {
                ConfigFerric = JsonConvert.DeserializeObject<ConfigFerric>(sr.ReadToEnd());
            }
        }

        /// <summary>
        /// Loads all plugin configs from the config path.
        /// </summary>
        public static void LoadPluginConfigs()
        {
            List<string> files = Directory.GetFiles(FerricBootConfig.Instance.ConfigsFolder, "*.json").ToList().ConvertAll(Path.GetFileNameWithoutExtension);

            if (Loader.Plugins.Count == 0)
                return;

            foreach (var plugin in Loader.Plugins)
            {
                var file = files.FirstOrDefault(x => x == plugin.ID);

                if (file is not null)
                {
                    using TextReader tr = new StreamReader(Path.Combine(FerricBootConfig.Instance.ConfigsFolder, file + ".json"));
                    object deserialized = null;
                    try
                    {
                        deserialized =
                            JsonConvert.DeserializeObject(tr.ReadToEnd(), plugin.Config.GetType());
                    }
                    catch (JsonReaderException e)
                    {
                        Console.Error($"Could not parse config {file}, {e}");
                        continue;
                    }
                    catch (Exception e)
                    {
                        Console.Error($"An error happened reading config: {e}");
                        continue;
                    }

                    if (deserialized is null)
                    {
                        Console.Error($"Cannot read config: {file}");
                        continue;
                    }

                    if (deserialized is not Config newConfig)
                    {
                        Console.Error($"File is not a valid config: {file}");
                        continue;
                    }

                    plugin.Config = newConfig;
                }
                else
                {
                    Console.Warn($"Cannot find config for plugin: {plugin.Name}, generating...");
                    using var streamWriter = new StreamWriter(Path.Combine(FerricBootConfig.Instance.ConfigsFolder, $"{plugin.ID}.json"));
                    WriteConfig(streamWriter, plugin.Config);
                }
            }
        }

        /// <summary>
        /// Creates a config file for every plugin with the default values.
        /// </summary>
        public static void GenerateDefaultConfigs()
        {
            foreach (var plugin in Loader.Plugins)
            {
                using var streamWriter = new StreamWriter(Path.Combine(FerricBootConfig.Instance.ConfigsFolder, $"{plugin.ID}.json"));
                WriteConfig(streamWriter, plugin.Config);
            }
        }

        /// <summary>
        /// Writes an config into an <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="tw">The <see cref="TextWriter"/> to write into.</param>
        /// <param name="cfg">The <see cref="Config"/> to write.</param>
        private static void WriteConfig(TextWriter tw, Config cfg)
        {
            tw.Write(JsonConvert.SerializeObject(cfg, Formatting.Indented));
        }
    }
}