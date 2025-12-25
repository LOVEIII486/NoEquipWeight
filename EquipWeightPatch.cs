using HarmonyLib;
using ItemStatsSystem;

namespace NoEquipWeight
{
    [HarmonyPatch(typeof(Item), "get_TotalWeight")]
    public static class NoEquipWeightPatch
    {
        [HarmonyPrefix]
        static bool Prefix(Item __instance, ref float __result)
        {
            var mainPlayer = CharacterMainControl.Main;
            if (mainPlayer == null || __instance != mainPlayer.CharacterItem) return true;

            float totalContentsWeight = 0f;

            // 口袋物资重量
            if (__instance.Inventory != null)
            {
                totalContentsWeight += __instance.Inventory.CachedWeight;
            }

            // 身上所有装备里塞的物资重量
            // if (__instance.Slots != null)
            // {
            //     foreach (var slot in __instance.Slots)
            //     {
            //         if (slot.Content != null && slot.Content.Inventory != null)
            //         {
            //             totalContentsWeight += slot.Content.Inventory.CachedWeight;
            //         }
            //     }
            // }

            __result = totalContentsWeight;
            return false; 
        }
    }
}