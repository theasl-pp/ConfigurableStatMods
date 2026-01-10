using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using PhoenixPoint.Tactical.Entities.Equipments;
using PhoenixPoint.Tactical.Entities.Statuses;
using PhoenixPoint.Tactical.Entities.Weapons;
using System;
using System.Linq;
using UnityEngine;

namespace LivingArmorStats
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class LivingArmorStatsMain : ModMain
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

            public ArmorValues(float armor, float speed, float perception,
                float stealth, float accuracy, int weight)
            {
                Armor = armor;
                Speed = speed;
                Perception = perception;
                Stealth = stealth;
                Accuracy = accuracy;
                Weight = weight;
            }
        }

        /// Config is accessible at any time, if any is declared.
        public new LivingArmorStatsConfig Config => (LivingArmorStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private TacticalItemDef HelmetItem, BodyItem, LegItem;
        private TacticalItemDef LeftArmItem, RightArmItem, LeftLegItem, RightLegItem;
        private ArmorValues DefaultHelmetValues, DefaultBodyValues, DefaultLegValues;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            HelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Chitin_Helmet_ItemDef"));
            BodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Chitin_Torso_ItemDef"));
            LegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Chitin_Legs_ItemDef"));

            LeftArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Chitin_LeftArm_ItemDef"));
            RightArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Chitin_RightArm_ItemDef"));
            LeftLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Chitin_LeftLeg_ItemDef"));
            RightLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Chitin_RightLeg_ItemDef"));

            // printArmorDef(HelmetItem);
            // printArmorDef(BodyItem);
            // printArmorDef(LegItem);

            // printArmorDef(LeftArmItem);
            // printArmorDef(RightArmItem);
            // printArmorDef(LeftLegItem);
            // printArmorDef(RightLegItem);

            DefaultHelmetValues = getArmorValuesFromArmorDef(HelmetItem);
            DefaultBodyValues = getArmorValuesFromArmorDef(BodyItem);
            DefaultLegValues = getArmorValuesFromArmorDef(LegItem);

            OnConfigChanged();
        }

        /// <summary>
        /// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
        /// Guaranteed to have OnModEnabled before.
        /// </summary>
        public override void OnModDisabled()
        {
            setDefsFromArmorValues(DefaultHelmetValues, HelmetItem, true);
            setDefsFromArmorValues(DefaultBodyValues, BodyItem, true);
            setDefsFromArmorValues(DefaultLegValues, LegItem, true);

            setDefsFromArmorValues(DefaultBodyValues, LeftArmItem, false);
            setDefsFromArmorValues(DefaultBodyValues, RightArmItem, false);
            setDefsFromArmorValues(DefaultLegValues, LeftLegItem, false);
            setDefsFromArmorValues(DefaultLegValues, RightLegItem, false);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            ArmorValues HelmetValues = new ArmorValues(
                Config.SamnuHelmetArmor,
                Config.SamnuHelmetSpeed,
                Config.SamnuHelmetPerception,
                Config.SamnuHelmetStealth,
                Config.SamnuHelmetAccuracy,
                Config.SamnuHelmetWeight
            );
            ArmorValues BodyValues = new ArmorValues(
                Config.SamnuBodyArmor,
                Config.SamnuBodySpeed,
                Config.SamnuBodyPerception,
                Config.SamnuBodyStealth,
                Config.SamnuBodyAccuracy,
                Config.SamnuBodyWeight
            );
            ArmorValues LegValues = new ArmorValues(
                Config.SamnuLegArmor,
                Config.SamnuLegSpeed,
                Config.SamnuLegPerception,
                Config.SamnuLegStealth,
                Config.SamnuLegAccuracy,
                Config.SamnuLegWeight
            );
            setDefsFromArmorValues(HelmetValues, HelmetItem, true);
            setDefsFromArmorValues(BodyValues, BodyItem, true);
            setDefsFromArmorValues(LegValues, LegItem, true);

            setDefsFromArmorValues(BodyValues, LeftArmItem, false);
            setDefsFromArmorValues(BodyValues, RightArmItem, false);
            setDefsFromArmorValues(LegValues, LeftLegItem, false);
            setDefsFromArmorValues(LegValues, RightLegItem, false);
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
                armorDef.Weight
            );
        }
        private void setDefsFromArmorValues(ArmorValues armorValues, TacticalItemDef armorDef, Boolean setAll)
        {
            armorDef.Armor = armorValues.Armor;
            if (setAll)
            {
                armorDef.BodyPartAspectDef.Speed = armorValues.Speed;
                armorDef.BodyPartAspectDef.Perception = armorValues.Perception;
                armorDef.BodyPartAspectDef.Stealth = armorValues.Stealth / 100f;
                armorDef.BodyPartAspectDef.Accuracy = armorValues.Accuracy / 100f;
                armorDef.Weight = armorValues.Weight;
            }
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
