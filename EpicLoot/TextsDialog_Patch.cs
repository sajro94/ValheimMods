﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using EpicLoot.Adventure;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using EpicLoot.LegendarySystem;
using Object = UnityEngine.Object;

namespace EpicLoot
{
    [HarmonyPatch(typeof(TextsDialog), nameof(TextsDialog.UpdateTextsList))]
    public static class TextsDialog_UpdateTextsList_Patch
    {
        private class EffectInformation {
            public string Name { get; }
            public ItemRarity Rarity { get; }

            public EffectInformation(string name, ItemRarity rarity) {
                Name = name;
                Rarity = rarity;
            }
        }

        public static void Postfix(TextsDialog __instance)
        {
            var player = Player.m_localPlayer;
            if (player == null)
            {
                return;
            }

            AddMagicEffectsPage(__instance, player);
            AddMagicEffectsExplainPage(__instance);
            AddTreasureAndBountiesPage(__instance, player);
        }

        public static void AddMagicEffectsPage(TextsDialog textsDialog, Player player)
        {
            var magicEffects = new Dictionary<string, List<KeyValuePair<MagicItemEffect, EffectInformation>>>();

            var allEquipment = player.GetEquipment();

            foreach (var item in allEquipment)
            {
                if (item.IsMagic())
                {
                    foreach (var effect in item.GetMagicItem().Effects)
                    {
                        if (!magicEffects.TryGetValue(effect.EffectType, out var effectList))
                        {
                            effectList = new List<KeyValuePair<MagicItemEffect, EffectInformation>>();
                            magicEffects.Add(effect.EffectType, effectList);
                        }

                        var effectInformation = new EffectInformation(item.GetDecoratedName(),item.GetRarity());
                        effectList.Add(new KeyValuePair<MagicItemEffect, EffectInformation>(effect, effectInformation));
                    }
                }
            }

            var allSets = player.GetEquippedSets();
            foreach (var set in allSets) {
                var setPieces = player.GetEquippedSetPieces(set.ID);
                var setPieceCount = setPieces.Count;
                foreach(var bonus in set.SetBonuses) {
                    if(bonus.Count <= setPieceCount) {

                        if (!magicEffects.TryGetValue(bonus.Effect.Type, out var effectList)) {
                            effectList = new List<KeyValuePair<MagicItemEffect, EffectInformation>>();
                            magicEffects.Add(bonus.Effect.Type, effectList);
                        }

                        var effect = new MagicItemEffect(bonus.Effect.Type, bonus.Effect.Values?.MinValue ?? 0);
                        var effectInformation = new EffectInformation(set.Name, ItemRarity.Legendary);                     

                        effectList.Add(new KeyValuePair<MagicItemEffect, EffectInformation>(effect, effectInformation));
                    }
                }
            }

            var t = new StringBuilder();

            foreach (var entry in magicEffects)
            {
                var effectType = entry.Key;
                var effectDef = MagicItemEffectDefinitions.Get(effectType);
                var sum = entry.Value.Sum(x => x.Key.EffectValue);
                var totalEffectText = MagicItem.GetEffectText(effectDef, sum);
                var highestRarity = (ItemRarity) entry.Value.Max(x => (int) x.Value.Rarity);

                t.AppendLine($"<size=20><color={EpicLoot.GetRarityColor(highestRarity)}>{totalEffectText}</color></size>");
                foreach (var entry2 in entry.Value)
                {
                    var effect = entry2.Key;
                    var effectInfo = entry2.Value;
                    t.AppendLine($" <color=silver>- {MagicItem.GetEffectText(effect, effectInfo.Rarity, false)} ({effectInfo.Name})</color>");
                }

                t.AppendLine();
            }

            textsDialog.m_texts.Insert(EpicLoot.HasAuga ? 0 : 2, 
                new TextsDialog.TextInfo(
                    Localization.instance.Localize($"{EpicLoot.GetMagicEffectPip(false)} $mod_epicloot_active_magic_effects"), 
                    Localization.instance.Localize(t.ToString())));
        }
                
        public static void AddTreasureAndBountiesPage(TextsDialog textsDialog, Player player)
        {
            var t = new StringBuilder();

            var saveData = player.GetAdventureSaveData();

            t.AppendLine("<color=orange><size=30>$mod_epicloot_merchant_treasuremaps</size></color>");
            t.AppendLine();

            var sortedTreasureMaps = saveData.TreasureMaps.Where(x => x.State == TreasureMapState.Purchased).OrderBy(x => GetBiomeOrder(x.Biome));
            foreach (var treasureMap in sortedTreasureMaps)
            {
                t.AppendLine(Localization.instance.Localize($"$mod_epicloot_merchant_treasuremaps: <color={GetBiomeColor(treasureMap.Biome)}>$biome_{treasureMap.Biome.ToString().ToLower()} #{treasureMap.Interval + 1}</color>"));
            }

            t.AppendLine();
            t.AppendLine();
            t.AppendLine("<color=orange><size=30>$mod_epicloot_activebounties</size></color>");
            t.AppendLine();

            var sortedBounties = saveData.Bounties.OrderBy(x => x.State);
            foreach (var bounty in sortedBounties)
            {
                if (bounty.State == BountyState.Claimed)
                {
                    continue;
                }

                var targetName = AdventureDataManager.GetBountyName(bounty);
                t.AppendLine($"<size=24>{targetName}</size>");
                t.Append($"  <color=silver>$mod_epicloot_activebounties_classification: <color=#d66660>{AdventureDataManager.GetMonsterName(bounty.Target.MonsterID)}</color>, ");
                t.AppendLine($" $mod_epicloot_activebounties_biome: <color={GetBiomeColor(bounty.Biome)}>$biome_{bounty.Biome.ToString().ToLower()}</color></color>");

                var status = "";
                switch (bounty.State)
                {
                    case BountyState.InProgress:
                        status = ("<color=#00f0ff>$mod_epicloot_bounties_tooltip_inprogress</color>");
                        break;
                    case BountyState.Complete:
                        status = ("<color=#70f56c>$mod_epicloot_bounties_tooltip_vanquished</color>");
                        break;
                }

                t.Append($"  <color=silver>$mod_epicloot_bounties_tooltip_status {status}");

                var iron = bounty.RewardIron;
                var gold = bounty.RewardGold;
                t.AppendLine($", $mod_epicloot_bounties_tooltip_rewards {(iron > 0 ? $"<color=white>{MerchantPanel.GetIronBountyTokenName()} x{iron}</color>" : "")}{(iron > 0 && gold > 0 ? ", " : "")}{(gold > 0 ? $"<color=#f5da53>{MerchantPanel.GetGoldBountyTokenName()} x{gold}</color>" : "")}</color>");
                t.AppendLine();
            }

            textsDialog.m_texts.Insert(EpicLoot.HasAuga ? 2 : 4, 
                new TextsDialog.TextInfo(
                    Localization.instance.Localize($"{EpicLoot.GetMagicEffectPip(false)} $mod_epicloot_adventure_title"), 
                    Localization.instance.Localize(t.ToString())));
        }
        
        public static string GetBiomeColor(Heightmap.Biome biome)
        {
            var biomeColor = "white";
            switch (biome)
            {
                case Heightmap.Biome.Meadows: biomeColor = "#75d966"; break;
                case Heightmap.Biome.BlackForest: biomeColor = "#72a178"; break;
                case Heightmap.Biome.Swamp: biomeColor = "#a88a6f"; break;
                case Heightmap.Biome.Mountain: biomeColor = "#a3bcd6"; break;
                case Heightmap.Biome.Plains: biomeColor = "#d6cea3"; break;
            }

            return biomeColor;
        }
        
        public static float GetBiomeOrder(Heightmap.Biome biome)
        {
            if (biome == Heightmap.Biome.BlackForest)
            {
                return 1.5f;
            }

            return (float) biome;
        }

        public static void AddMagicEffectsExplainPage(TextsDialog textsDialog)
        {
            string prefix = null;//TODO
            var sortedMagicEffects = MagicItemEffectDefinitions.AllDefinitions
                .Where(x => !x.Value.Requirements.NoRoll)
                .Select(x => new KeyValuePair<string, string>(string.Format(Localization.instance.Localize(x.Value.DisplayText), "<b><color=yellow>X</color></b>", prefix), Localization.instance.Localize(x.Value.Description)))
                .OrderBy(x => x.Key);

            var t = new StringBuilder();
            foreach (var effectEntry in sortedMagicEffects)
            {
                t.AppendLine($"<size=24>{effectEntry.Key}</size>");
                t.AppendLine($"<color=silver>{effectEntry.Value}</color>");
                t.AppendLine();
            }

            textsDialog.m_texts.Insert(EpicLoot.HasAuga ? 1 : 3,
                new TextsDialog.TextInfo(
                    Localization.instance.Localize($"{EpicLoot.GetMagicEffectPip(false)} $mod_epicloot_me_explaintitle"),
                    Localization.instance.Localize(t.ToString())));
        }
    }

    [HarmonyPatch(typeof(TextsDialog), nameof(TextsDialog.ShowText), typeof(TextsDialog.TextInfo))]
    public static class TextsDialog_ShowText_Patch
    {
        public static Transform TextContainer;
        public static Text TitleTextPrefab;
        public static Text DescriptionTextPrefab;

        public static bool Prefix(TextsDialog __instance, TextsDialog.TextInfo text)
        {
            if (TitleTextPrefab == null)
            {
                TextContainer = __instance.m_textAreaTopic.transform.parent;
                var textContainerBackground = TextContainer.gameObject.AddComponent<Image>();
                textContainerBackground.color = new Color();
                textContainerBackground.raycastTarget = true;

                var verticalLayoutGroup = TextContainer.GetComponent<VerticalLayoutGroup>();
                verticalLayoutGroup.spacing = 0;

                TitleTextPrefab = Object.Instantiate(__instance.m_textAreaTopic, __instance.transform);
                TitleTextPrefab.gameObject.SetActive(false);
            }

            if (DescriptionTextPrefab == null)
            {
                DescriptionTextPrefab = Object.Instantiate(__instance.m_textArea, __instance.transform);
                DescriptionTextPrefab.gameObject.SetActive(false);
            }

            for (var i = 0; i < TextContainer.childCount; i++)
            {
                Object.Destroy(TextContainer.GetChild(i).gameObject);
            }

            var description = Object.Instantiate(TitleTextPrefab, TextContainer);
            description.gameObject.SetActive(true);
            description.text = text.m_topic;

            var parts = text.m_text.Split('\n');
            foreach (var part in parts)
            {
                var paragraphText = Object.Instantiate(DescriptionTextPrefab, TextContainer);
                paragraphText.gameObject.SetActive(true);
                paragraphText.text = part;
            }

            return false;
        }
    }
}
