using Base;
using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MutationStats
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class MutationStatsMain : ModMain
    {
        /// <summary>
        /// defines the modifiable values for any given armor.
        /// </summary>
        private struct ArmorValues
        {
            public float Armor;
            public float Speed;
            public float Perception;
            public float Stealth;
            public float Accuracy;
            public int Weight;
            public bool IsPermanentAugment;

            public ArmorValues(float armor, float speed, float perception,
                float stealth, float accuracy, int weight, bool isPermanentAugment)
            {
                Armor = armor;
                Speed = speed;
                Perception = perception;
                Stealth = stealth;
                Accuracy = accuracy;
                Weight = weight;
                IsPermanentAugment = isPermanentAugment;
            }
        }

        /// Config is accessible at any time, if any is declared.
        public new MutationStatsConfig Config => (MutationStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private TacticalItemDef HeavyHeadItem, HeavyTorsoItem, HeavyLegsItem,
            WatcherHeadItem, WatcherTorsoItem, WatcherLegsItem,
            ShooterHeadItem, ShooterTorsoItem, ShooterLegsItem,
            PriestHead01Item, PriestHead02Item, PriestHead03Item;
        private ArmorValues DefaultHeavyHeadValues, DefaultHeavyTorsoValues, DefaultHeavyLegsValues,
            DefaultWatcherHeadValues, DefaultWatcherTorsoValues, DefaultWatcherLegsValues,
            DefaultShooterHeadValues, DefaultShooterTorsoValues, DefaultShooterLegsValues,
            DefaultPriestHead01Values, DefaultPriestHead02Values, DefaultPriestHead03Values;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            HeavyHeadItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Heavy_Helmet_BodyPartDef"));
            HeavyTorsoItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Heavy_Torso_BodyPartDef"));
            HeavyLegsItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Heavy_Legs_ItemDef"));
            WatcherHeadItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Watcher_Helmet_BodyPartDef"));
            WatcherTorsoItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Watcher_Torso_BodyPartDef"));
            WatcherLegsItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Watcher_Legs_ItemDef"));
            ShooterHeadItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Shooter_Helmet_BodyPartDef"));
            ShooterTorsoItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Shooter_Torso_BodyPartDef"));
            ShooterLegsItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Shooter_Legs_ItemDef"));
            PriestHead01Item = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Priest_Head01_BodyPartDef"));
            PriestHead02Item = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Priest_Head02_BodyPartDef"));
            PriestHead03Item = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Priest_Head03_BodyPartDef"));

            //printArmorDef(HeavyHeadItem);
            //printArmorDef(HeavyTorsoItem);
            //printArmorDef(HeavyLegsItem);
            //printArmorDef(WatcherHeadItem);
            //printArmorDef(WatcherTorsoItem);
            //printArmorDef(WatcherLegsItem);
            //printArmorDef(ShooterHeadItem);
            //printArmorDef(ShooterTorsoItem);
            //printArmorDef(ShooterLegsItem);
            //printArmorDef(PriestHead01Item);
            //printArmorDef(PriestHead02Item);
            //printArmorDef(PriestHead03Item);

            DefaultHeavyHeadValues = getArmorValuesFromArmorDef(HeavyHeadItem);
            DefaultHeavyTorsoValues = getArmorValuesFromArmorDef(HeavyTorsoItem);
            DefaultHeavyLegsValues = getArmorValuesFromArmorDef(HeavyLegsItem);
            DefaultWatcherHeadValues = getArmorValuesFromArmorDef(WatcherHeadItem);
            DefaultWatcherTorsoValues = getArmorValuesFromArmorDef(WatcherTorsoItem);
            DefaultWatcherLegsValues = getArmorValuesFromArmorDef(WatcherLegsItem);
            DefaultShooterHeadValues = getArmorValuesFromArmorDef(ShooterHeadItem);
            DefaultShooterTorsoValues = getArmorValuesFromArmorDef(ShooterTorsoItem);
            DefaultShooterLegsValues = getArmorValuesFromArmorDef(ShooterLegsItem);
            DefaultPriestHead01Values = getArmorValuesFromArmorDef(PriestHead01Item);
            DefaultPriestHead02Values = getArmorValuesFromArmorDef(PriestHead02Item);
            DefaultPriestHead03Values = getArmorValuesFromArmorDef(PriestHead03Item);

            OnConfigChanged();
        }

        /// <summary>
        /// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
        /// Guaranteed to have OnModEnabled before.
        /// </summary>
        public override void OnModDisabled()
        {
            setDefsFromArmorValues(DefaultHeavyHeadValues, HeavyHeadItem);
            setDefsFromArmorValues(DefaultHeavyTorsoValues, HeavyTorsoItem);
            setDefsFromArmorValues(DefaultHeavyLegsValues, HeavyLegsItem);
            setDefsFromArmorValues(DefaultWatcherHeadValues, WatcherHeadItem);
            setDefsFromArmorValues(DefaultWatcherTorsoValues, WatcherTorsoItem);
            setDefsFromArmorValues(DefaultWatcherLegsValues, WatcherLegsItem);
            setDefsFromArmorValues(DefaultShooterHeadValues, ShooterHeadItem);
            setDefsFromArmorValues(DefaultShooterTorsoValues, ShooterTorsoItem);
            setDefsFromArmorValues(DefaultShooterLegsValues, ShooterLegsItem);
            setDefsFromArmorValues(DefaultPriestHead01Values, PriestHead01Item);
            setDefsFromArmorValues(DefaultPriestHead02Values, PriestHead02Item);
            setDefsFromArmorValues(DefaultPriestHead03Values, PriestHead03Item);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            ArmorValues HeavyHeadValues = new ArmorValues(
                Config.HeavyHeadArmor,
                Config.HeavyHeadSpeed,
                Config.HeavyHeadPerception,
                Config.HeavyHeadStealth,
                Config.HeavyHeadAccuracy,
                Config.HeavyHeadWeight,
                Config.HeavyHeadIsPermanentAugment
            );
            ArmorValues HeavyTorsoValues = new ArmorValues(
                Config.HeavyTorsoArmor,
                Config.HeavyTorsoSpeed,
                Config.HeavyTorsoPerception,
                Config.HeavyTorsoStealth,
                Config.HeavyTorsoAccuracy,
                Config.HeavyTorsoWeight,
                Config.HeavyTorsoIsPermanentAugment
            );
            ArmorValues HeavyLegsValues = new ArmorValues(
                Config.HeavyLegsArmor,
                Config.HeavyLegsSpeed,
                Config.HeavyLegsPerception,
                Config.HeavyLegsStealth,
                Config.HeavyLegsAccuracy,
                Config.HeavyLegsWeight,
                Config.HeavyLegsIsPermanentAugment
            );
            ArmorValues WatcherHeadValues = new ArmorValues(
                Config.WatcherHeadArmor,
                Config.WatcherHeadSpeed,
                Config.WatcherHeadPerception,
                Config.WatcherHeadStealth,
                Config.WatcherHeadAccuracy,
                Config.WatcherHeadWeight,
                Config.WatcherHeadIsPermanentAugment
            );
            ArmorValues WatcherTorsoValues = new ArmorValues(
                Config.WatcherTorsoArmor,
                Config.WatcherTorsoSpeed,
                Config.WatcherTorsoPerception,
                Config.WatcherTorsoStealth,
                Config.WatcherTorsoAccuracy,
                Config.WatcherTorsoWeight,
                Config.WatcherTorsoIsPermanentAugment
            );
            ArmorValues WatcherLegsValues = new ArmorValues(
                Config.WatcherLegsArmor,
                Config.WatcherLegsSpeed,
                Config.WatcherLegsPerception,
                Config.WatcherLegsStealth,
                Config.WatcherLegsAccuracy,
                Config.WatcherLegsWeight,
                Config.WatcherLegsIsPermanentAugment
            );
            ArmorValues ShooterHeadValues = new ArmorValues(
                Config.ShooterHeadArmor,
                Config.ShooterHeadSpeed,
                Config.ShooterHeadPerception,
                Config.ShooterHeadStealth,
                Config.ShooterHeadAccuracy,
                Config.ShooterHeadWeight,
                Config.ShooterHeadIsPermanentAugment
            );
            ArmorValues ShooterTorsoValues = new ArmorValues(
                Config.ShooterTorsoArmor,
                Config.ShooterTorsoSpeed,
                Config.ShooterTorsoPerception,
                Config.ShooterTorsoStealth,
                Config.ShooterTorsoAccuracy,
                Config.ShooterTorsoWeight,
                Config.ShooterTorsoIsPermanentAugment
            );
            ArmorValues ShooterLegsValues = new ArmorValues(
                Config.ShooterLegsArmor,
                Config.ShooterLegsSpeed,
                Config.ShooterLegsPerception,
                Config.ShooterLegsStealth,
                Config.ShooterLegsAccuracy,
                Config.ShooterLegsWeight,
                Config.ShooterLegsIsPermanentAugment
            );
            ArmorValues PriestHead01Values = new ArmorValues(
                Config.PriestHead01Armor,
                Config.PriestHead01Speed,
                Config.PriestHead01Perception,
                Config.PriestHead01Stealth,
                Config.PriestHead01Accuracy,
                Config.PriestHead01Weight,
                Config.PriestHead01IsPermanentAugment
            );
            ArmorValues PriestHead02Values = new ArmorValues(
                Config.PriestHead02Armor,
                Config.PriestHead02Speed,
                Config.PriestHead02Perception,
                Config.PriestHead02Stealth,
                Config.PriestHead02Accuracy,
                Config.PriestHead02Weight,
                Config.PriestHead02IsPermanentAugment
            );
            ArmorValues PriestHead03Values = new ArmorValues(
                Config.PriestHead03Armor,
                Config.PriestHead03Speed,
                Config.PriestHead03Perception,
                Config.PriestHead03Stealth,
                Config.PriestHead03Accuracy,
                Config.PriestHead03Weight,
                Config.PriestHead03IsPermanentAugment
            );
            setDefsFromArmorValues(HeavyHeadValues, HeavyHeadItem);
            setDefsFromArmorValues(HeavyTorsoValues, HeavyTorsoItem);
            setDefsFromArmorValues(HeavyLegsValues, HeavyLegsItem);
            setDefsFromArmorValues(WatcherHeadValues, WatcherHeadItem);
            setDefsFromArmorValues(WatcherTorsoValues, WatcherTorsoItem);
            setDefsFromArmorValues(WatcherLegsValues, WatcherLegsItem);
            setDefsFromArmorValues(ShooterHeadValues, ShooterHeadItem);
            setDefsFromArmorValues(ShooterTorsoValues, ShooterTorsoItem);
            setDefsFromArmorValues(ShooterLegsValues, ShooterLegsItem);
            setDefsFromArmorValues(PriestHead01Values, PriestHead01Item);
            setDefsFromArmorValues(PriestHead02Values, PriestHead02Item);
            setDefsFromArmorValues(PriestHead03Values, PriestHead03Item);
        }

        /* WEAPON DATA FUNCTIONS */

        private ArmorValues getArmorValuesFromArmorDef(TacticalItemDef armorDef)
        {
            return new ArmorValues(
                armorDef.Armor,
                armorDef.BodyPartAspectDef.Speed,
                armorDef.BodyPartAspectDef.Perception,
                armorDef.BodyPartAspectDef.Stealth * 100f,
                armorDef.BodyPartAspectDef.Accuracy * 100f,
                armorDef.Weight,
                armorDef.IsPermanentAugment
            );
        }
        private void setDefsFromArmorValues(ArmorValues armorValues, TacticalItemDef armorDef)
        {
            armorDef.Armor = armorValues.Armor;
            armorDef.BodyPartAspectDef.Speed = armorValues.Speed;
            armorDef.BodyPartAspectDef.Perception = armorValues.Perception;
            armorDef.BodyPartAspectDef.Stealth = armorValues.Stealth / 100f;
            armorDef.BodyPartAspectDef.Accuracy = armorValues.Accuracy / 100f;
            armorDef.Weight = armorValues.Weight;
            armorDef.IsPermanentAugment = armorValues.IsPermanentAugment;
        }

        private void printArmorDef(TacticalItemDef armorDef)
        {
            Logger.LogInfo("=== " + armorDef.name + " ===");
            Logger.LogInfo("Armor: " + armorDef.Armor);
            Logger.LogInfo("Speed: " + armorDef.BodyPartAspectDef.Speed);
            Logger.LogInfo("Perception: " + armorDef.BodyPartAspectDef.Perception);
            Logger.LogInfo("Stealth%: " + armorDef.BodyPartAspectDef.Stealth * 100f);
            Logger.LogInfo("Accuracy%: " + armorDef.BodyPartAspectDef.Accuracy * 100f);
            Logger.LogInfo("Weight: " + armorDef.Weight);
            Logger.LogInfo("IsPermanentAugment: " + armorDef.IsPermanentAugment);
            Logger.LogInfo("Abilities:");
            for (int i = 0; i < armorDef.Abilities.Length; i++)
            {
                Logger.LogInfo(" - " + i + " " + armorDef.Abilities[i]);
                if (armorDef.Abilities[i].GetType() == typeof(JetJumpAbilityDef))
                {
                    Logger.LogInfo("   - FumblePerc: " + ((JetJumpAbilityDef)(armorDef.Abilities[i])).FumblePerc);
                }
            }
        }
    }
}
