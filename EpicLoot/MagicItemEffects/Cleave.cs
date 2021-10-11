using HarmonyLib;
using JetBrains.Annotations;
using UnityEngine;

namespace EpicLoot.MagicItemEffects
{
    [HarmonyPatch(typeof(Attack), nameof(Attack.DoMeleeAttack))]
    public class Cleave
    {
        [UsedImplicitly]
        private static bool Prefix(Attack __instance)
        {
            if (__instance.GetWeapon().IsMagic() && __instance.GetWeapon().HasMagicEffect(MagicEffectType.Cleave))
            {
                __instance.m_lowerDamagePerHit = false;
            }
            return true;
        }
    }
}