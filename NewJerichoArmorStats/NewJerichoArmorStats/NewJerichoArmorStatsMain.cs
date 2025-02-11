using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
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
            setDefsFromArmorValues(DefaultWardogHelmetValues, WardogHelmetItem);
            setDefsFromArmorValues(DefaultWardogBodyValues, WardogBodyItem);
            setDefsFromArmorValues(DefaultWardogLegValues, WardogLegItem);
            setDefsFromArmorValues(DefaultAnvil2HelmetValues, Anvil2HelmetItem);
            setDefsFromArmorValues(DefaultAnvil2BodyValues, Anvil2BodyItem);
            setDefsFromArmorValues(DefaultAnvil2LegValues, Anvil2LegItem);
            setDefsFromArmorValues(DefaultEidolonHelmetValues, EidolonHelmetItem);
            setDefsFromArmorValues(DefaultEidolonBodyValues, EidolonBodyItem);
            setDefsFromArmorValues(DefaultEidolonLegValues, EidolonLegItem);
            setDefsFromArmorValues(DefaultTechops7HelmetValues, Techops7HelmetItem);
            setDefsFromArmorValues(DefaultTechops7BodyValues, Techops7BodyItem);
            setDefsFromArmorValues(DefaultTechops7LegValues, Techops7LegItem);
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
            setDefsFromArmorValues(WardogHelmetValues, WardogHelmetItem);
            setDefsFromArmorValues(WardogBodyValues, WardogBodyItem);
            setDefsFromArmorValues(WardogLegValues, WardogLegItem);
            setDefsFromArmorValues(Anvil2HelmetValues, Anvil2HelmetItem);
            setDefsFromArmorValues(Anvil2BodyValues, Anvil2BodyItem);
            setDefsFromArmorValues(Anvil2LegValues, Anvil2LegItem);
            setDefsFromArmorValues(EidolonHelmetValues, EidolonHelmetItem);
            setDefsFromArmorValues(EidolonBodyValues, EidolonBodyItem);
            setDefsFromArmorValues(EidolonLegValues, EidolonLegItem);
            setDefsFromArmorValues(Techops7HelmetValues, Techops7HelmetItem);
            setDefsFromArmorValues(Techops7BodyValues, Techops7BodyItem);
            setDefsFromArmorValues(Techops7LegValues, Techops7LegItem);
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
        private void setDefsFromArmorValues(ArmorValues armorValues, TacticalItemDef armorDef)
        {
            armorDef.Armor = armorValues.Armor;
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
