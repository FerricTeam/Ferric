namespace Ferric
{
    using System;
    using System.IO;
    using System.Linq;
    using Ferric.API.Features;
    using Newtonsoft.Json;
    using Console = Ferric.API.Wrappers.Console;

    /// <summary>
    /// Manages configs.
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>
        /// Loads all configs from the config path.
        /// </summary>
        public static void LoadConfigs()
        {
            var files = Directory.GetFiles(Loader.ConfigsFolder, "*.json").ToList().ConvertAll(Path.GetFileNameWithoutExtension);

            if (Loader.Plugins.Count == 0)
                return;

            foreach (var plugin in Loader.Plugins)
            {
                var file = files.FirstOrDefault(x => x == plugin.ID);

                if (file is not null)
                {
                    using TextReader tr = new StreamReader(Path.Combine(Loader.ConfigsFolder, file + ".json"));
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
                    using var tw = new StreamWriter(Path.Combine(Loader.ConfigsFolder, $"{plugin.ID}.json"));
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
                using var tw = new StreamWriter(Path.Combine(Loader.ConfigsFolder, $"{plugin.ID}.json"));
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