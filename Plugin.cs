using BepInEx;
using BepInEx.Unity.IL2CPP;
using CrimsonChatFilter.Structs;
using HarmonyLib;
using CrimsonChatFilter.Utils;
using BepInEx.Logging;

namespace CrimsonChatFilter;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("gg.deca.Bloodstone")]
[Bloodstone.API.Reloadable]
public class Plugin : BasePlugin
{
    Harmony _harmony;
    public static Settings Settings { get; private set; }
    public static ManualLogSource Logger { get; private set; }

    public override void Load()
    {
        Logger = Log;
        Settings = new(Config);
        Settings.InitConfig();
        Database.InitFiltered("filtered_words");

        // Plugin startup logic
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} version {MyPluginInfo.PLUGIN_VERSION} is loaded!");

        // Harmony patching
        _harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        _harmony.PatchAll(System.Reflection.Assembly.GetExecutingAssembly());

        Bloodstone.Hooks.Chat.OnChatMessage += ((x) => 
        {
            if (x.Type == ProjectM.Network.ChatMessageType.System) return;
            if (!Settings.GetActiveOption(Settings.Options.Enable)) return;
            if (!Settings.GetActiveOption(Settings.Options.FullRemove)) return;
            if (x.Message.ContainsFiltered())
            {
                Logger.LogWarning($"FILTERED: {x.Message}");
                x.Cancel();
            }
        });
    }

    public override bool Unload()
    {
        _harmony?.UnpatchSelf();
        return true;
    }

}
