using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
using System.Linq;
using UnityEngine;

namespace SynedrionArmorStats
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class SynedrionArmorStatsMain : ModMain
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
        public new SynedrionArmorStatsConfig Config => (SynedrionArmorStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private TacticalItemDef PhlegethonHelmetItem, PhlegethonBodyItem, PhlegethonLegItem,
            StyxHelmetItem, StyxBodyItem, StyxLegItem,
            AcheronHelmetItem, AcheronBodyItem, AcheronLegItem;
        private ArmorValues DefaultPhlegethonHelmetValues, DefaultPhlegethonBodyValues, DefaultPhlegethonLegValues,
            DefaultStyxHelmetValues, DefaultStyxBodyValues, DefaultStyxLegValues,
            DefaultAcheronHelmetValues, DefaultAcheronBodyValues, DefaultAcheronLegValues;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            PhlegethonHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Assault_Helmet_BodyPartDef"));
            PhlegethonBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Assault_Torso_BodyPartDef"));
            PhlegethonLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Assault_Legs_ItemDef"));
            StyxHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Infiltrator_Helmet_BodyPartDef"));
            StyxBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Infiltrator_Torso_BodyPartDef"));
            StyxLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Infiltrator_Legs_ItemDef"));
            AcheronHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Sniper_Helmet_BodyPartDef"));
            AcheronBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Sniper_Torso_BodyPartDef"));
            AcheronLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Sniper_Legs_ItemDef"));

            //printArmorDef(PhlegethonHelmetItem);
            //printArmorDef(PhlegethonBodyItem);
            //printArmorDef(PhlegethonLegItem);
            //printArmorDef(StyxHelmetItem);
            //printArmorDef(StyxBodyItem);
            //printArmorDef(StyxLegItem);
            //printArmorDef(AcheronHelmetItem);
            //printArmorDef(AcheronBodyItem);
            //printArmorDef(AcheronLegItem);

            DefaultPhlegethonHelmetValues = getArmorValuesFromArmorDef(PhlegethonHelmetItem);
            DefaultPhlegethonBodyValues = getArmorValuesFromArmorDef(PhlegethonBodyItem);
            DefaultPhlegethonLegValues = getArmorValuesFromArmorDef(PhlegethonLegItem);
            DefaultStyxHelmetValues = getArmorValuesFromArmorDef(StyxHelmetItem);
            DefaultStyxBodyValues = getArmorValuesFromArmorDef(StyxBodyItem);
            DefaultStyxLegValues = getArmorValuesFromArmorDef(StyxLegItem);
            DefaultAcheronHelmetValues = getArmorValuesFromArmorDef(AcheronHelmetItem);
            DefaultAcheronBodyValues = getArmorValuesFromArmorDef(AcheronBodyItem);
            DefaultAcheronLegValues = getArmorValuesFromArmorDef(AcheronLegItem);

            OnConfigChanged();
        }

        /// <summary>
        /// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
        /// Guaranteed to have OnModEnabled before.
        /// </summary>
        public override void OnModDisabled()
        {
            setDefsFromArmorValues(DefaultPhlegethonHelmetValues, PhlegethonHelmetItem);
            setDefsFromArmorValues(DefaultPhlegethonBodyValues, PhlegethonBodyItem);
            setDefsFromArmorValues(DefaultPhlegethonLegValues, PhlegethonLegItem);
            setDefsFromArmorValues(DefaultStyxHelmetValues, StyxHelmetItem);
            setDefsFromArmorValues(DefaultStyxBodyValues, StyxBodyItem);
            setDefsFromArmorValues(DefaultStyxLegValues, StyxLegItem);
            setDefsFromArmorValues(DefaultAcheronHelmetValues, AcheronHelmetItem);
            setDefsFromArmorValues(DefaultAcheronBodyValues, AcheronBodyItem);
            setDefsFromArmorValues(DefaultAcheronLegValues, AcheronLegItem);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            ArmorValues PhlegethonHelmetValues = new ArmorValues(
                Config.PhlegethonHelmetArmor,
                Config.PhlegethonHelmetSpeed,
                Config.PhlegethonHelmetPerception,
                Config.PhlegethonHelmetStealth,
                Config.PhlegethonHelmetAccuracy,
                Config.PhlegethonHelmetWeight
            );
            ArmorValues PhlegethonBodyValues = new ArmorValues(
                Config.PhlegethonBodyArmor,
                Config.PhlegethonBodySpeed,
                Config.PhlegethonBodyPerception,
                Config.PhlegethonBodyStealth,
                Config.PhlegethonBodyAccuracy,
                Config.PhlegethonBodyWeight
            );
            ArmorValues PhlegethonLegValues = new ArmorValues(
                Config.PhlegethonLegArmor,
                Config.PhlegethonLegSpeed,
                Config.PhlegethonLegPerception,
                Config.PhlegethonLegStealth,
                Config.PhlegethonLegAccuracy,
                Config.PhlegethonLegWeight
            );
            ArmorValues StyxHelmetValues = new ArmorValues(
                Config.StyxHelmetArmor,
                Config.StyxHelmetSpeed,
                Config.StyxHelmetPerception,
                Config.StyxHelmetStealth,
                Config.StyxHelmetAccuracy,
                Config.StyxHelmetWeight
            );
            ArmorValues StyxBodyValues = new ArmorValues(
                Config.StyxBodyArmor,
                Config.StyxBodySpeed,
                Config.StyxBodyPerception,
                Config.StyxBodyStealth,
                Config.StyxBodyAccuracy,
                Config.StyxBodyWeight
            );
            ArmorValues StyxLegValues = new ArmorValues(
                Config.StyxLegArmor,
                Config.StyxLegSpeed,
                Config.StyxLegPerception,
                Config.StyxLegStealth,
                Config.StyxLegAccuracy,
                Config.StyxLegWeight
            );
            ArmorValues AcheronHelmetValues = new ArmorValues(
                Config.AcheronHelmetArmor,
                Config.AcheronHelmetSpeed,
                Config.AcheronHelmetPerception,
                Config.AcheronHelmetStealth,
                Config.AcheronHelmetAccuracy,
                Config.AcheronHelmetWeight
            );
            ArmorValues AcheronBodyValues = new ArmorValues(
                Config.AcheronBodyArmor,
                Config.AcheronBodySpeed,
                Config.AcheronBodyPerception,
                Config.AcheronBodyStealth,
                Config.AcheronBodyAccuracy,
                Config.AcheronBodyWeight
            );
            ArmorValues AcheronLegValues = new ArmorValues(
                Config.AcheronLegArmor,
                Config.AcheronLegSpeed,
                Config.AcheronLegPerception,
                Config.AcheronLegStealth,
                Config.AcheronLegAccuracy,
                Config.AcheronLegWeight
            );
            setDefsFromArmorValues(PhlegethonHelmetValues, PhlegethonHelmetItem);
            setDefsFromArmorValues(PhlegethonBodyValues, PhlegethonBodyItem);
            setDefsFromArmorValues(PhlegethonLegValues, PhlegethonLegItem);
            setDefsFromArmorValues(StyxHelmetValues, StyxHelmetItem);
            setDefsFromArmorValues(StyxBodyValues, StyxBodyItem);
            setDefsFromArmorValues(StyxLegValues, StyxLegItem);
            setDefsFromArmorValues(AcheronHelmetValues, AcheronHelmetItem);
            setDefsFromArmorValues(AcheronBodyValues, AcheronBodyItem);
            setDefsFromArmorValues(AcheronLegValues, AcheronLegItem);
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
        private void setDefsFromArmorValues(ArmorValues armorValues, TacticalItemDef armorDef)
        {
            armorDef.Armor = armorValues.Armor;
            armorDef.BodyPartAspectDef.Speed = armorValues.Speed;
            armorDef.BodyPartAspectDef.Perception = armorValues.Perception;
            armorDef.BodyPartAspectDef.Stealth = armorValues.Stealth / 100f;
            armorDef.BodyPartAspectDef.Accuracy = armorValues.Accuracy / 100f;
            armorDef.Weight = armorValues.Weight;
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
