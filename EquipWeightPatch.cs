using HarmonyLib;
using ItemStatsSystem;

namespace NoEquipWeight
{
    [HarmonyPatch(typeof(Item), "get_TotalWeight")]
    public static class NoEquipWeightPatch
    {
        [HarmonyPostfix]
        static void Postfix(Item __instance, ref float __result)
        {
            if (__instance == null) return;
            
            if (__instance.IsCharacter)
            {
                float equipmentTotalWeight = 0f;
                if (__instance.Slots != null)
                {
                    foreach (var slot in __instance.Slots)
                    {
                        if (slot != null && slot.Content != null)
                        {
                            equipmentTotalWeight += slot.Content.TotalWeight;
                        }
                    }
                }
                __result -= equipmentTotalWeight;
            }
        }
    }
}