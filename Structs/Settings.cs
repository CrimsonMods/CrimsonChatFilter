using BepInEx.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CrimsonChatFilter.Structs;

public readonly struct Settings
{
    private readonly ConfigFile CONFIG;
    private readonly ConfigEntry<bool> ENABLE_MOD;
    private readonly ConfigEntry<bool> FULL_REMOVE;
    private readonly ConfigEntry<bool> FILTER_URLS;

    public static readonly string CONFIG_PATH = Path.Combine(BepInEx.Paths.ConfigPath, "CrimsonChatFilter");

    public Settings(ConfigFile config)
    {
        CONFIG = config;
        ENABLE_MOD = CONFIG.Bind("Config", "EnableMod", true, "Enable or disable chat filtering");
        FULL_REMOVE = CONFIG.Bind("Config", "FullRemove", false, "If enabled, others won't see the message, otherwise replaces filtered words with ****");
        FILTER_URLS = CONFIG.Bind("Config", "FilterURLs", true, "Includes .com and server addresses in filter list");
    }

    public readonly void InitConfig()
    {
        WriteConfig();
    }

    public readonly void WriteConfig()
    { 
        if(!Directory.Exists(CONFIG_PATH)) Directory.CreateDirectory(CONFIG_PATH);
        if (!File.Exists(Path.Combine(CONFIG_PATH, "filtered_words.json")))
        {
            List<string> _filteredConfig = GenerateTemplate();
            string _json = JsonSerializer.Serialize(_filteredConfig, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(Path.Combine(CONFIG_PATH, "filtered_words.json"), _json);
        }
    }

    private List<string> GenerateTemplate()
    { 
        List<string> _template = new List<string> 
        {
            "coon",
            "jim crow",
            "nigger",
            "gook",
            "chink",
            "towelhead",
            "honky",
            "beaner",
            "spic"
        };
        return _template;
    }

    public readonly bool GetActiveOption(Options option)
    {
        return option switch
        {
            Options.Enable => ENABLE_MOD.Value,
            Options.FullRemove => FULL_REMOVE.Value,
            Options.FilterUrl => FILTER_URLS.Value,
            _ => false
        };
    }

    public enum Options
    { 
        Enable,
        FullRemove,
        FilterUrl
    }
}
