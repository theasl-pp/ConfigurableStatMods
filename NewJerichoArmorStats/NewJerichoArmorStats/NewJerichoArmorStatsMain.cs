using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
using System;
using System.Linq;
using UnityEngine;

namespace NewJerichoArmorStats
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class NewJerichoArmorStatsMain : ModMain
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
            public int FumblePerc;

            public ArmorValues(float armor, float speed, float perception,
                float stealth, float accuracy, int weight, int fumblePerc)
            {
                Armor = armor;
                Speed = speed;
                Perception = perception;
                Stealth = stealth;
                Accuracy = accuracy;
                Weight = weight;
                FumblePerc = fumblePerc;
            }
        }

        /// Config is accessible at any time, if any is declared.
        public new NewJerichoArmorStatsConfig Config => (NewJerichoArmorStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private TacticalItemDef WardogHelmetItem, WardogBodyItem, WardogLegItem,
            Anvil2HelmetItem, Anvil2BodyItem, Anvil2LegItem,
            EidolonHelmetItem, EidolonBodyItem, EidolonLegItem,
            Techops7HelmetItem, Techops7BodyItem, Techops7LegItem;
        private TacticalItemDef WardogLeftArmItem, WardogRightArmItem,
            WardogLeftLegItem, WardogRightLegItem,
            Anvil2LeftArmItem, Anvil2RightArmItem,
            Anvil2LeftLegItem, Anvil2RightLegItem,
            EidolonLeftArmItem, EidolonRightArmItem,
            EidolonLeftLegItem, EidolonRightLegItem,
            Techops7LeftArmItem, Techops7RightArmItem,
            Techops7LeftLegItem, Techops7RightLegItem;
        private ArmorValues DefaultWardogHelmetValues, DefaultWardogBodyValues, DefaultWardogLegValues,
            DefaultAnvil2HelmetValues, DefaultAnvil2BodyValues, DefaultAnvil2LegValues,
            DefaultEidolonHelmetValues, DefaultEidolonBodyValues, DefaultEidolonLegValues,
            DefaultTechops7HelmetValues, DefaultTechops7BodyValues, DefaultTechops7LegValues;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            WardogHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault_Helmet_BodyPartDef"));
            WardogBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault_Torso_BodyPartDef"));
            WardogLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault_Legs_ItemDef"));
            Anvil2HelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy_Helmet_BodyPartDef"));
            Anvil2BodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy_Torso_BodyPartDef"));
            Anvil2LegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy_Legs_ItemDef"));
            EidolonHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper_Helmet_BodyPartDef"));
            EidolonBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper_Torso_BodyPartDef"));
            EidolonLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper_Legs_ItemDef"));
            Techops7HelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician_Helmet_BodyPartDef"));
            Techops7BodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician_Torso_BodyPartDef"));
            Techops7LegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician_Legs_ItemDef"));

            WardogLeftArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault_Torso_BodyPartDef"));
            WardogRightArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault_Torso_BodyPartDef"));
            WardogLeftLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault_Legs_BodyPartDef"));
            WardogRightLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Assault_Legs_BodyPartDef"));
            Anvil2LeftArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy_Torso_BodyPartDef"));
            Anvil2RightArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy_Torso_BodyPartDef"));
            Anvil2LeftLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy_Legs_BodyPartDef"));
            Anvil2RightLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Heavy_Legs_BodyPartDef"));
            EidolonLeftArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper_Torso_BodyPartDef"));
            EidolonRightArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper_Torso_BodyPartDef"));
            EidolonLeftLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper_Legs_BodyPartDef"));
            EidolonRightLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Sniper_Legs_BodyPartDef"));
            Techops7LeftArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician_Torso_BodyPartDef"));
            Techops7RightArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician_Torso_BodyPartDef"));
            Techops7LeftLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician_Legs_BodyPartDef"));
            Techops7RightLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician_Legs_BodyPartDef"));

            //printArmorDef(WardogHelmetItem);
            //printArmorDef(WardogBodyItem);
            //printArmorDef(WardogLegItem);
            //printArmorDef(Anvil2HelmetItem);
            //printArmorDef(Anvil2BodyItem);
            //printArmorDef(Anvil2LegItem);
            //printArmorDef(EidolonHelmetItem);
            //printArmorDef(EidolonBodyItem);
            //printArmorDef(EidolonLegItem);
            //printArmorDef(Techops7HelmetItem);
            //printArmorDef(Techops7BodyItem);
            //printArmorDef(Techops7LegItem);

            //printArmorDef(WardogLeftArmItem);
            //printArmorDef(WardogRightArmItem);
            //printArmorDef(WardogLeftLegItem);
            //printArmorDef(WardogRightLegItem);
            //printArmorDef(Anvil2LeftArmItem);
            //printArmorDef(Anvil2RightArmItem);
            //printArmorDef(Anvil2LeftLegItem);
            //printArmorDef(Anvil2RightLegItem);
            //printArmorDef(EidolonLeftArmItem);
            //printArmorDef(EidolonRightArmItem);
            //printArmorDef(EidolonLeftLegItem);
            //printArmorDef(EidolonRightLegItem);
            //printArmorDef(Techops7LeftArmItem);
            //printArmorDef(Techops7RightArmItem);
            //printArmorDef(Techops7LeftLegItem);
            //printArmorDef(Techops7RightLegItem);

            DefaultWardogHelmetValues = getArmorValuesFromArmorDef(WardogHelmetItem);
            DefaultWardogBodyValues = getArmorValuesFromArmorDef(WardogBodyItem);
            DefaultWardogLegValues = getArmorValuesFromArmorDef(WardogLegItem);
            DefaultAnvil2HelmetValues = getArmorValuesFromArmorDef(Anvil2HelmetItem);
            DefaultAnvil2BodyValues = getArmorValuesFromArmorDef(Anvil2BodyItem);
            DefaultAnvil2LegValues = getArmorValuesFromArmorDef(Anvil2LegItem);
            DefaultEidolonHelmetValues = getArmorValuesFromArmorDef(EidolonHelmetItem);
            DefaultEidolonBodyValues = getArmorValuesFromArmorDef(EidolonBodyItem);
            DefaultEidolonLegValues = getArmorValuesFromArmorDef(EidolonLegItem);
            DefaultTechops7HelmetValues = getArmorValuesFromArmorDef(Techops7HelmetItem);
            DefaultTechops7BodyValues = getArmorValuesFromArmorDef(Techops7BodyItem);
            DefaultTechops7LegValues = getArmorValuesFromArmorDef(Techops7LegItem);

            OnConfigChanged();
        }

        /// <summary>
        /// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
        /// Guaranteed to have OnModEnabled before.
        /// </summary>
        public override void OnModDisabled()
        {
            setDefsFromArmorValues(DefaultWardogHelmetValues, WardogHelmetItem, true);
            setDefsFromArmorValues(DefaultWardogBodyValues, WardogBodyItem, true);
            setDefsFromArmorValues(DefaultWardogLegValues, WardogLegItem, true);
            setDefsFromArmorValues(DefaultAnvil2HelmetValues, Anvil2HelmetItem, true);
            setDefsFromArmorValues(DefaultAnvil2BodyValues, Anvil2BodyItem, true);
            setDefsFromArmorValues(DefaultAnvil2LegValues, Anvil2LegItem, true);
            setDefsFromArmorValues(DefaultEidolonHelmetValues, EidolonHelmetItem, true);
            setDefsFromArmorValues(DefaultEidolonBodyValues, EidolonBodyItem, true);
            setDefsFromArmorValues(DefaultEidolonLegValues, EidolonLegItem, true);
            setDefsFromArmorValues(DefaultTechops7HelmetValues, Techops7HelmetItem, true);
            setDefsFromArmorValues(DefaultTechops7BodyValues, Techops7BodyItem, true);
            setDefsFromArmorValues(DefaultTechops7LegValues, Techops7LegItem, true);

            setDefsFromArmorValues(DefaultWardogBodyValues, WardogLeftArmItem, false);
            setDefsFromArmorValues(DefaultWardogBodyValues, WardogRightArmItem, false);
            setDefsFromArmorValues(DefaultWardogLegValues, WardogLeftLegItem, false);
            setDefsFromArmorValues(DefaultWardogLegValues, WardogRightLegItem, false);
            setDefsFromArmorValues(DefaultAnvil2BodyValues, Anvil2LeftArmItem, false);
            setDefsFromArmorValues(DefaultAnvil2BodyValues, Anvil2RightArmItem, false);
            setDefsFromArmorValues(DefaultAnvil2LegValues, Anvil2LeftLegItem, false);
            setDefsFromArmorValues(DefaultAnvil2LegValues, Anvil2RightLegItem, false);
            setDefsFromArmorValues(DefaultEidolonBodyValues, EidolonLeftArmItem, false);
            setDefsFromArmorValues(DefaultEidolonBodyValues, EidolonRightArmItem, false);
            setDefsFromArmorValues(DefaultEidolonLegValues, EidolonLeftLegItem, false);
            setDefsFromArmorValues(DefaultEidolonLegValues, EidolonRightLegItem, false);
            setDefsFromArmorValues(DefaultTechops7BodyValues, Techops7LeftArmItem, false);
            setDefsFromArmorValues(DefaultTechops7BodyValues, Techops7RightArmItem, false);
            setDefsFromArmorValues(DefaultTechops7LegValues, Techops7LeftLegItem, false);
            setDefsFromArmorValues(DefaultTechops7LegValues, Techops7RightLegItem, false);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            ArmorValues WardogHelmetValues = new ArmorValues(
                Config.WardogHelmetArmor,
                Config.WardogHelmetSpeed,
                Config.WardogHelmetPerception,
                Config.WardogHelmetStealth,
                Config.WardogHelmetAccuracy,
                Config.WardogHelmetWeight,
                0
            );
            ArmorValues WardogBodyValues = new ArmorValues(
                Config.WardogBodyArmor,
                Config.WardogBodySpeed,
                Config.WardogBodyPerception,
                Config.WardogBodyStealth,
                Config.WardogBodyAccuracy,
                Config.WardogBodyWeight,
                0
            );
            ArmorValues WardogLegValues = new ArmorValues(
                Config.WardogLegArmor,
                Config.WardogLegSpeed,
                Config.WardogLegPerception,
                Config.WardogLegStealth,
                Config.WardogLegAccuracy,
                Config.WardogLegWeight,
                0
            );
            ArmorValues Anvil2HelmetValues = new ArmorValues(
                Config.Anvil2HelmetArmor,
                Config.Anvil2HelmetSpeed,
                Config.Anvil2HelmetPerception,
                Config.Anvil2HelmetStealth,
                Config.Anvil2HelmetAccuracy,
                Config.Anvil2HelmetWeight,
                0
            );
            ArmorValues Anvil2BodyValues = new ArmorValues(
                Config.Anvil2BodyArmor,
                Config.Anvil2BodySpeed,
                Config.Anvil2BodyPerception,
                Config.Anvil2BodyStealth,
                Config.Anvil2BodyAccuracy,
                Config.Anvil2BodyWeight,
                Config.Anvil2BodyFumblePerc
            );
            ArmorValues Anvil2LegValues = new ArmorValues(
                Config.Anvil2LegArmor,
                Config.Anvil2LegSpeed,
                Config.Anvil2LegPerception,
                Config.Anvil2LegStealth,
                Config.Anvil2LegAccuracy,
                Config.Anvil2LegWeight,
                0
            );
            ArmorValues EidolonHelmetValues = new ArmorValues(
                Config.EidolonHelmetArmor,
                Config.EidolonHelmetSpeed,
                Config.EidolonHelmetPerception,
                Config.EidolonHelmetStealth,
                Config.EidolonHelmetAccuracy,
                Config.EidolonHelmetWeight,
                0
            );
            ArmorValues EidolonBodyValues = new ArmorValues(
                Config.EidolonBodyArmor,
                Config.EidolonBodySpeed,
                Config.EidolonBodyPerception,
                Config.EidolonBodyStealth,
                Config.EidolonBodyAccuracy,
                Config.EidolonBodyWeight,
                0
            );
            ArmorValues EidolonLegValues = new ArmorValues(
                Config.EidolonLegArmor,
                Config.EidolonLegSpeed,
                Config.EidolonLegPerception,
                Config.EidolonLegStealth,
                Config.EidolonLegAccuracy,
                Config.EidolonLegWeight,
                0
            );
            ArmorValues Techops7HelmetValues = new ArmorValues(
                Config.Techops7HelmetArmor,
                Config.Techops7HelmetSpeed,
                Config.Techops7HelmetPerception,
                Config.Techops7HelmetStealth,
                Config.Techops7HelmetAccuracy,
                Config.Techops7HelmetWeight,
                0
            );
            ArmorValues Techops7BodyValues = new ArmorValues(
                Config.Techops7BodyArmor,
                Config.Techops7BodySpeed,
                Config.Techops7BodyPerception,
                Config.Techops7BodyStealth,
                Config.Techops7BodyAccuracy,
                Config.Techops7BodyWeight,
                0
            );
            ArmorValues Techops7LegValues = new ArmorValues(
                Config.Techops7LegArmor,
                Config.Techops7LegSpeed,
                Config.Techops7LegPerception,
                Config.Techops7LegStealth,
                Config.Techops7LegAccuracy,
                Config.Techops7LegWeight,
                0
            );
            setDefsFromArmorValues(WardogHelmetValues, WardogHelmetItem, true);
            setDefsFromArmorValues(WardogBodyValues, WardogBodyItem, true);
            setDefsFromArmorValues(WardogLegValues, WardogLegItem, true);
            setDefsFromArmorValues(Anvil2HelmetValues, Anvil2HelmetItem, true);
            setDefsFromArmorValues(Anvil2BodyValues, Anvil2BodyItem, true);
            setDefsFromArmorValues(Anvil2LegValues, Anvil2LegItem, true);
            setDefsFromArmorValues(EidolonHelmetValues, EidolonHelmetItem, true);
            setDefsFromArmorValues(EidolonBodyValues, EidolonBodyItem, true);
            setDefsFromArmorValues(EidolonLegValues, EidolonLegItem, true);
            setDefsFromArmorValues(Techops7HelmetValues, Techops7HelmetItem, true);
            setDefsFromArmorValues(Techops7BodyValues, Techops7BodyItem, true);
            setDefsFromArmorValues(Techops7LegValues, Techops7LegItem, true);

            setDefsFromArmorValues(WardogBodyValues, WardogLeftArmItem, false);
            setDefsFromArmorValues(WardogBodyValues, WardogRightArmItem, false);
            setDefsFromArmorValues(WardogLegValues, WardogLeftLegItem, false);
            setDefsFromArmorValues(WardogLegValues, WardogRightLegItem, false);
            setDefsFromArmorValues(Anvil2BodyValues, Anvil2LeftArmItem, false);
            setDefsFromArmorValues(Anvil2BodyValues, Anvil2RightArmItem, false);
            setDefsFromArmorValues(Anvil2LegValues, Anvil2LeftLegItem, false);
            setDefsFromArmorValues(Anvil2LegValues, Anvil2RightLegItem, false);
            setDefsFromArmorValues(EidolonBodyValues, EidolonLeftArmItem, false);
            setDefsFromArmorValues(EidolonBodyValues, EidolonRightArmItem, false);
            setDefsFromArmorValues(EidolonLegValues, EidolonLeftLegItem, false);
            setDefsFromArmorValues(EidolonLegValues, EidolonRightLegItem, false);
            setDefsFromArmorValues(Techops7BodyValues, Techops7LeftArmItem, false);
            setDefsFromArmorValues(Techops7BodyValues, Techops7RightArmItem, false);
            setDefsFromArmorValues(Techops7LegValues, Techops7LeftLegItem, false);
            setDefsFromArmorValues(Techops7LegValues, Techops7RightLegItem, false);
        }

        /* WEAPON DATA FUNCTIONS */

        private ArmorValues getArmorValuesFromArmorDef(TacticalItemDef armorDef)
        {
            int FumblePerc = 0;
            for (int i = 0; i < armorDef.Abilities.Length; i++)
            {
                if (armorDef.Abilities[i].GetType() == typeof(JetJumpAbilityDef))
                {
                    FumblePerc = ((JetJumpAbilityDef)(armorDef.Abilities[i])).FumblePerc;
                    break;
                }
            }
            return new ArmorValues(
                armorDef.Armor,
                armorDef.BodyPartAspectDef.Speed,
                armorDef.BodyPartAspectDef.Perception,
                armorDef.BodyPartAspectDef.Stealth * 100f,
                armorDef.BodyPartAspectDef.Accuracy * 100f,
                armorDef.Weight,
                FumblePerc
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
                for (int i = 0; i < armorDef.Abilities.Length; i++)
                {
                    if (armorDef.Abilities[i].GetType() == typeof(JetJumpAbilityDef))
                    {
                        ((JetJumpAbilityDef)(armorDef.Abilities[i])).FumblePerc = armorValues.FumblePerc;
                        break;
                    }
                }
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
