using CrimsonChatFilter.Utils;
using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using Unity.Collections;
using Unity.Entities;

namespace CrimsonChatFilter.Hooks;

[HarmonyPatch]
public static class ChatMessageSystem_Patch
{
    [HarmonyPatch(typeof(ChatMessageSystem), nameof(ChatMessageSystem.OnUpdate))]
    [HarmonyPrefix]
    public static bool OnUpdate(ChatMessageSystem __instance)
    {
        if (!Plugin.Settings.GetActiveOption(Structs.Settings.Options.Enable)) return true;
        if (Plugin.Settings.GetActiveOption(Structs.Settings.Options.FullRemove)) return true;

        if (__instance.__query_661171423_0 != null)
        {
            NativeArray<Entity> entities = __instance.__query_661171423_0.ToEntityArray(Allocator.Temp);
            foreach (var entity in entities)
            {
                var fromData = __instance.EntityManager.GetComponentData<FromCharacter>(entity);
                var userData = __instance.EntityManager.GetComponentData<User>(fromData.User);
                var chatEventData = __instance.EntityManager.GetComponentData<ChatMessageEvent>(entity);

                var messageText = chatEventData.MessageText.ToString();

                if (chatEventData.MessageType == ChatMessageType.System) continue;

                messageText = messageText.Filter();

                chatEventData.MessageText = messageText;
                __instance.EntityManager.SetComponentData(entity, chatEventData);
            }
        }

        return true;
    }
}
