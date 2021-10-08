﻿using System.Text;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace EpicLoot.Crafting
{
    public class AugmentsAvailableDialog : MonoBehaviour
    {
        public Text NameText;
        public Text Description;
        public Image Icon;
        public Image MagicBG;

        [UsedImplicitly]
        public void Awake()
        {
        }

        [UsedImplicitly]
        public void Update()
        {
            //EpicLoot.Log("AugmentsAvailableDialog.Update");
            if (!EpicLoot.HasAuga)
            {
                if (ZInput.GetButtonDown("Inventory") || ZInput.GetButtonDown("JoyButtonB") || (ZInput.GetButtonDown("JoyButtonY") || Input.GetKeyDown(KeyCode.Escape)) || ZInput.GetButtonDown("Use"))
                {
                    OnClose();
                }
            }
        }

        public void Show(AugmentTabController.AugmentRecipe recipe)
        {
            gameObject.SetActive(true);

            var item = recipe.FromItem;
            var rarity = item.GetRarity();
            var magicItem = item.GetMagicItem();
            var rarityColor = item.GetRarityColor();

            if (MagicBG != null)
            {
                MagicBG.enabled = item.IsMagic();
                MagicBG.color = rarityColor;
            }

            if (NameText != null)
            {
                NameText.text = Localization.instance.Localize(item.GetDecoratedName());
            }

            if (Icon != null)
            {
                Icon.sprite = item.GetIcon();
            }

            if (Description != null)
            {
                var availableEffects = AugmentTabController.GetAvailableAugments(recipe, item, magicItem, rarity);
                var t = new StringBuilder();
                foreach (var effectDef in availableEffects)
                {
                    var values = effectDef.GetValuesForRarity(item.GetRarity(),item.GetMagicItem().LegendaryID);
                    var valueDisplay = values != null ? Mathf.Approximately(values.MinValue, values.MaxValue) ? $"{values.MinValue}" : $"({values.MinValue}-{values.MaxValue})" : "";
                    string prefix = null; //TODO
                    t.AppendLine($"‣ {string.Format(Localization.instance.Localize(effectDef.DisplayText), valueDisplay, prefix)}");
                }

                Description.color = rarityColor;
                Description.text = t.ToString();
            }
        }

        public void OnClose()
        {
            gameObject.SetActive(false);
        }
    }
}
