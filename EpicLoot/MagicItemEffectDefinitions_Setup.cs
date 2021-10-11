﻿using EpicLoot.Abilities;
using EpicLoot.MagicItemEffects;

namespace EpicLoot
{
    public static partial class MagicEffectType
    {
        public static string DvergerCirclet = nameof(DvergerCirclet);
        public static string Megingjord = nameof(Megingjord);
        public static string Wishbone = nameof(Wishbone);
        public static string Andvaranaut = nameof(Andvaranaut);
        public static string ModifyDamage = nameof(ModifyDamage);
        public static string ModifyPhysicalDamage = nameof(ModifyPhysicalDamage);
        public static string ModifyElementalDamage = nameof(ModifyElementalDamage);
        public static string ModifyDurability = nameof(ModifyDurability);
        public static string ReduceWeight = nameof(ReduceWeight);
        public static string RemoveSpeedPenalty = nameof(RemoveSpeedPenalty);
        public static string ModifyBlockPower = nameof(ModifyBlockPower);
        public static string ModifyParry = nameof(ModifyParry);
        public static string ModifyArmor = nameof(ModifyArmor);
        public static string ModifyBackstab = nameof(ModifyBackstab);
        public static string IncreaseHealth = nameof(IncreaseHealth);
        public static string IncreaseStamina = nameof(IncreaseStamina);
        public static string ModifyHealthRegen = nameof(ModifyHealthRegen);
        public static string AddHealthRegen = nameof(AddHealthRegen);
        public static string ModifyStaminaRegen = nameof(ModifyStaminaRegen);
        public static string AddBluntDamage = nameof(AddBluntDamage);
        public static string AddSlashingDamage = nameof(AddSlashingDamage);
        public static string AddPiercingDamage = nameof(AddPiercingDamage);
        public static string AddFireDamage = nameof(AddFireDamage);
        public static string AddFrostDamage = nameof(AddFrostDamage);
        public static string AddLightningDamage = nameof(AddLightningDamage);
        public static string AddPoisonDamage = nameof(AddPoisonDamage);
        public static string AddSpiritDamage = nameof(AddSpiritDamage);
        public static string AddFireResistance = nameof(AddFireResistance);
        public static string AddFireResistancePercentage = nameof(AddFireResistancePercentage);
        public static string AddFrostResistance = nameof(AddFrostResistance);
        public static string AddFrostResistancePercentage = nameof(AddFrostResistancePercentage);
        public static string AddLightningResistance = nameof(AddLightningResistance);
        public static string AddLightningResistancePercentage = nameof(AddLightningResistancePercentage);
        public static string AddPoisonResistance = nameof(AddPoisonResistance);
        public static string AddPoisonResistancePercentage = nameof(AddPoisonResistancePercentage);
        public static string AddSpiritResistance = nameof(AddSpiritResistance);
        public static string AddSpiritResistancePercentage = nameof(AddSpiritResistancePercentage);
        public static string AddElementalResistancePercentage = nameof(AddElementalResistancePercentage);
        public static string AddBluntResistancePercentage = nameof(AddBluntResistancePercentage);
        public static string AddSlashingResistancePercentage = nameof(AddSlashingResistancePercentage);
        public static string AddPiercingResistancePercentage = nameof(AddPiercingResistancePercentage);
        public static string AddChoppingResistancePercentage = nameof(AddChoppingResistancePercentage);
        public static string AddPhysicalResistancePercentage = nameof(AddPhysicalResistancePercentage);
        public static string ModifyMovementSpeed = nameof(ModifyMovementSpeed);
        public static string ModifySprintStaminaUse = nameof(ModifySprintStaminaUse);
        public static string ModifyJumpStaminaUse = nameof(ModifyJumpStaminaUse);
        public static string ModifyAttackStaminaUse = nameof(ModifyAttackStaminaUse);
        public static string ModifyBlockStaminaUse = nameof(ModifyBlockStaminaUse);
        public static string Indestructible = nameof(Indestructible);
        public static string Weightless = nameof(Weightless);
        public static string LifeSteal = nameof(LifeSteal);
        public static string AddCarryWeight = nameof(AddCarryWeight);
        public static string ModifyAttackSpeed = nameof(ModifyAttackSpeed);
        public static string Throwable = nameof(Throwable);
        public static string Waterproof = nameof(Waterproof);
        public static string Paralyze = nameof(Paralyze);
        public static string DoubleJump = nameof(DoubleJump);
        public static string WaterWalking = nameof(WaterWalking);
        public static string ExplosiveArrows = nameof(ExplosiveArrows);
        public static string QuickDraw = nameof(QuickDraw);
        public static string AddSwordsSkill = nameof(AddSwordsSkill);
        public static string AddKnivesSkill = nameof(AddKnivesSkill);
        public static string AddClubsSkill = nameof(AddClubsSkill);
        public static string AddPolearmsSkill = nameof(AddPolearmsSkill);
        public static string AddSpearsSkill = nameof(AddSpearsSkill);
        public static string AddBlockingSkill = nameof(AddBlockingSkill);
        public static string AddAxesSkill = nameof(AddAxesSkill);
        public static string AddBowsSkill = nameof(AddBowsSkill);
        public static string AddUnarmedSkill = nameof(AddUnarmedSkill);
        public static string AddPickaxesSkill = nameof(AddPickaxesSkill);
        public static string AddMovementSkills = nameof(AddMovementSkills);
        public static string ModifyStaggerDuration = nameof(ModifyStaggerDuration);
        public static string QuickLearner = nameof(QuickLearner);
        public static string RecallWeapon = nameof(RecallWeapon);
        public static string ReflectDamage = nameof(ReflectDamage);
        public static string AvoidDamageTaken = nameof(AvoidDamageTaken);
        public static string StaggerOnDamageTaken = nameof(StaggerOnDamageTaken);
        public static string FeatherFall = nameof(FeatherFall);
        public static string ModifyDiscoveryRadius = nameof(ModifyDiscoveryRadius);
        public static string FreeBuild = nameof(FreeBuild);
        public static string Comfortable = nameof(Comfortable);
        public static string ModifyMovementSpeedLowHealth = nameof(ModifyMovementSpeedLowHealth);
        public static string ModifyHealthRegenLowHealth = nameof(ModifyHealthRegenLowHealth);
        public static string ModifyStaminaRegenLowHealth = nameof(ModifyStaminaRegenLowHealth);
        public static string ModifyArmorLowHealth = nameof(ModifyArmorLowHealth);
        public static string ModifyDamageLowHealth = nameof(ModifyDamageLowHealth);
        public static string ModifyBlockPowerLowHealth = nameof(ModifyBlockPowerLowHealth);
        public static string ModifyParryLowHealth = nameof(ModifyParryLowHealth);
        public static string ModifyAttackSpeedLowHealth = nameof(ModifyAttackSpeedLowHealth);
        public static string AvoidDamageTakenLowHealth = nameof(AvoidDamageTakenLowHealth);
        public static string LifeStealLowHealth = nameof(LifeStealLowHealth);
        public static string Glowing = nameof(Glowing);
        public static string Executioner = nameof(Executioner);
        public static string Riches = nameof(Riches);
        public static string Opportunist = nameof(Opportunist);
        public static string Duelist = nameof(Duelist);
        public static string Immovable = nameof(Immovable);
        public static string ModifyStaggerDamage = nameof(ModifyStaggerDamage);
        public static string Luck = nameof(Luck);
        public static string ModifyParryWindow = nameof(ModifyParryWindow);
        public static string Slow = nameof(Slow);
        public static string FrostDamageAOE = nameof(FrostDamageAOE);
        public static string Cleave = nameof(Cleave);

        public static string Bulwark = nameof(Bulwark);
        public static string Undying = nameof(Undying);

        public static void Initialize()
        {
            AbilityFactory.Register("Undying", typeof(UndyingAbility));
        }
    }
}
