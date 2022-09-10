namespace Ferric
{
    using System;
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
        public class FerricConfig
        {
            /// <summary>
            /// Gets the directory where Ferric is located.
            /// </summary>
            public static string FerricFolder => Directory.GetParent(Assembly.GetExecutingAssembly().Location)!.FullName;

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
            /// Gets the Singleton.
            /// </summary>
            /// <returns>The Instance.</returns>
            public static FerricConfig Instance => instance ??= new FerricConfig();

            private static FerricConfig instance;
        }

        /// <summary>
        /// Loads Ferric configuration.
        /// </summary>
        public static void LoadFerricConfig()
        {
            var configFile = Path.Combine(FerricConfig.FerricFolder, "FConfig.txt");

            if (!File.Exists(configFile))
            {
                File.Create(configFile).Close();
            }

            using (StreamReader sr = new StreamReader(configFile))
            {
                var content = sr.ReadToEnd().Split('\n');
                if (content.Length != 3)
                {
                    sr.Dispose();
                    using (var sw = new StreamWriter(configFile))
                    {
                        sw.WriteLine(FerricConfig.Instance.DependenciesFolder);
                        sw.WriteLine(FerricConfig.Instance.PluginFolder);
                        sw.WriteLine(FerricConfig.Instance.ConfigsFolder);
                    }
                }
                else
                {
                    FerricConfig.Instance.DependenciesFolder = content[0];
                    FerricConfig.Instance.PluginFolder = content[1];
                    FerricConfig.Instance.ConfigsFolder = content[2];
                }
            }
        }

        /// <summary>
        /// Loads all plugin configs from the config path.
        /// </summary>
        public static void LoadPluginConfigs()
        {
            var files = Directory.GetFiles(FerricConfig.Instance.ConfigsFolder, "*.json").ToList().ConvertAll(Path.GetFileNameWithoutExtension);

            if (Loader.Plugins.Count == 0)
                return;

            foreach (var plugin in Loader.Plugins)
            {
                var file = files.FirstOrDefault(x => x == plugin.ID);

                if (file is not null)
                {
                    using TextReader tr = new StreamReader(Path.Combine(FerricConfig.Instance.ConfigsFolder, file + ".json"));
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

                    Config newConfig = deserialized as Config;

                    if (newConfig is null)
                    {
                        Console.Error($"File is not a valid config: {file}");
                        continue;
                    }

                    plugin.Config = newConfig;
                }
                else
                {
                    Console.Warn($"Cannot find config for plugin: {plugin.Name}, generating...");
                    using var tw = new StreamWriter(Path.Combine(FerricConfig.Instance.ConfigsFolder, $"{plugin.ID}.json"));
                    WriteConfig(tw, plugin.Config);
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
                using var tw = new StreamWriter(Path.Combine(FerricConfig.Instance.ConfigsFolder, $"{plugin.ID}.json"));
                WriteConfig(tw, plugin.Config);
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