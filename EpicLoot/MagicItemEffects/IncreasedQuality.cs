using HarmonyLib;
using JetBrains.Annotations;

namespace EpicLoot.MagicItemEffects
{
    [HarmonyPatch(typeof(MagicItem), nameof(MagicItem.AddEffect))]
    class IncreasedQuality_AddEffect_Patch {

        [UsedImplicitly]
        public static void Postfix(MagicItem __instance, MagicItemEffect miEffect) {

            if (miEffect.EffectType == MagicEffectType.IncreasedQuality) {
                IncreasedMaxQuality_Helper.IncreaseMaxQuality(miEffect, __instance);
            }
        }
    }

    [HarmonyPatch(typeof(MagicItem), nameof(MagicItem.ReplaceEffect))]
    class IncreasedQuality_ReplaceEffect_Patch {

        [UsedImplicitly]
        public static void Prefix(MagicItem __instance, int index) {
            if(__instance.Effects[index].EffectType == MagicEffectType.IncreasedQuality) {
                IncreasedMaxQuality_Helper.ResetMaxQuality(__instance);
            }
        }

        [UsedImplicitly]
        public static void Postfix(MagicItem __instance, MagicItemEffect newEffect) {

            if (newEffect.EffectType == MagicEffectType.IncreasedQuality) {
                IncreasedMaxQuality_Helper.IncreaseMaxQuality(newEffect, __instance);
            }
        }

    }



    static class IncreasedMaxQuality_Helper {
        public static void ResetMaxQuality(MagicItem mi) {
            mi.ParentComponent.ItemData.m_shared.m_maxQuality = mi.MundaneMaxQuality;
            if (mi.ParentComponent.ItemData.m_quality > mi.MundaneMaxQuality) {
                mi.ParentComponent.ItemData.m_quality = mi.MundaneMaxQuality;
            }
        }

        public static void IncreaseMaxQuality(MagicItemEffect miEffect, MagicItem mi) {
            if (mi.MundaneMaxQuality == -1) {
                mi.MundaneMaxQuality = mi.ParentComponent.ItemData.m_shared.m_maxQuality;
            }
            mi.ParentComponent.ItemData.m_shared.m_maxQuality = mi.MundaneMaxQuality + (int)miEffect.EffectValue;
        }
    }
}

