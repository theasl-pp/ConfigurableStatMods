using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
using System.Linq;
using UnityEngine;

namespace IndependentArmorStats
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class IndependentArmorStatsMain : ModMain
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
        public new IndependentArmorStatsConfig Config => (IndependentArmorStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private TacticalItemDef Praetorian2HelmetItem, Praetorian2BodyItem, Praetorian2LegItem,
            GuardianXAHelmetItem, GuardianXABodyItem, GuardianXALegItem,
            SwampCatHelmetItem, SwampCatBodyItem, SwampCatLegItem;
        private ArmorValues DefaultPraetorian2HelmetValues, DefaultPraetorian2BodyValues, DefaultPraetorian2LegValues,
            DefaultGuardianXAHelmetValues, DefaultGuardianXABodyValues, DefaultGuardianXALegValues,
            DefaultSwampCatHelmetValues, DefaultSwampCatBodyValues, DefaultSwampCatLegValues;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            Praetorian2HelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("IN_Assault_Helmet_BodyPartDef"));
            Praetorian2BodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("IN_Assault_Torso_BodyPartDef"));
            Praetorian2LegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("IN_Assault_Legs_ItemDef"));
            GuardianXAHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("IN_Heavy_Helmet_BodyPartDef"));
            GuardianXABodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("IN_Heavy_Torso_BodyPartDef"));
            GuardianXALegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("IN_Heavy_Legs_ItemDef"));
            SwampCatHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("IN_Sniper_Helmet_BodyPartDef"));
            SwampCatBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("IN_Sniper_Torso_BodyPartDef"));
            SwampCatLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("IN_Sniper_Legs_ItemDef"));

            //printArmorDef(Praetorian2HelmetItem);
            //printArmorDef(Praetorian2BodyItem);
            //printArmorDef(Praetorian2LegItem);
            //printArmorDef(GuardianXAHelmetItem);
            //printArmorDef(GuardianXABodyItem);
            //printArmorDef(GuardianXALegItem);
            //printArmorDef(SwampCatHelmetItem);
            //printArmorDef(SwampCatBodyItem);
            //printArmorDef(SwampCatLegItem);

            DefaultPraetorian2HelmetValues = getArmorValuesFromArmorDef(Praetorian2HelmetItem);
            DefaultPraetorian2BodyValues = getArmorValuesFromArmorDef(Praetorian2BodyItem);
            DefaultPraetorian2LegValues = getArmorValuesFromArmorDef(Praetorian2LegItem);
            DefaultGuardianXAHelmetValues = getArmorValuesFromArmorDef(GuardianXAHelmetItem);
            DefaultGuardianXABodyValues = getArmorValuesFromArmorDef(GuardianXABodyItem);
            DefaultGuardianXALegValues = getArmorValuesFromArmorDef(GuardianXALegItem);
            DefaultSwampCatHelmetValues = getArmorValuesFromArmorDef(SwampCatHelmetItem);
            DefaultSwampCatBodyValues = getArmorValuesFromArmorDef(SwampCatBodyItem);
            DefaultSwampCatLegValues = getArmorValuesFromArmorDef(SwampCatLegItem);

            OnConfigChanged();
        }

        /// <summary>
        /// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
        /// Guaranteed to have OnModEnabled before.
        /// </summary>
        public override void OnModDisabled()
        {
            setDefsFromArmorValues(DefaultPraetorian2HelmetValues, Praetorian2HelmetItem);
            setDefsFromArmorValues(DefaultPraetorian2BodyValues, Praetorian2BodyItem);
            setDefsFromArmorValues(DefaultPraetorian2LegValues, Praetorian2LegItem);
            setDefsFromArmorValues(DefaultGuardianXAHelmetValues, GuardianXAHelmetItem);
            setDefsFromArmorValues(DefaultGuardianXABodyValues, GuardianXABodyItem);
            setDefsFromArmorValues(DefaultGuardianXALegValues, GuardianXALegItem);
            setDefsFromArmorValues(DefaultSwampCatHelmetValues, SwampCatHelmetItem);
            setDefsFromArmorValues(DefaultSwampCatBodyValues, SwampCatBodyItem);
            setDefsFromArmorValues(DefaultSwampCatLegValues, SwampCatLegItem);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            ArmorValues Praetorian2HelmetValues = new ArmorValues(
                Config.Praetorian2HelmetArmor,
                Config.Praetorian2HelmetSpeed,
                Config.Praetorian2HelmetPerception,
                Config.Praetorian2HelmetStealth,
                Config.Praetorian2HelmetAccuracy,
                Config.Praetorian2HelmetWeight
            );
            ArmorValues Praetorian2BodyValues = new ArmorValues(
                Config.Praetorian2BodyArmor,
                Config.Praetorian2BodySpeed,
                Config.Praetorian2BodyPerception,
                Config.Praetorian2BodyStealth,
                Config.Praetorian2BodyAccuracy,
                Config.Praetorian2BodyWeight
            );
            ArmorValues Praetorian2LegValues = new ArmorValues(
                Config.Praetorian2LegArmor,
                Config.Praetorian2LegSpeed,
                Config.Praetorian2LegPerception,
                Config.Praetorian2LegStealth,
                Config.Praetorian2LegAccuracy,
                Config.Praetorian2LegWeight
            );
            ArmorValues GuardianXAHelmetValues = new ArmorValues(
                Config.GuardianXAHelmetArmor,
                Config.GuardianXAHelmetSpeed,
                Config.GuardianXAHelmetPerception,
                Config.GuardianXAHelmetStealth,
                Config.GuardianXAHelmetAccuracy,
                Config.GuardianXAHelmetWeight
            );
            ArmorValues GuardianXABodyValues = new ArmorValues(
                Config.GuardianXABodyArmor,
                Config.GuardianXABodySpeed,
                Config.GuardianXABodyPerception,
                Config.GuardianXABodyStealth,
                Config.GuardianXABodyAccuracy,
                Config.GuardianXABodyWeight
            );
            ArmorValues GuardianXALegValues = new ArmorValues(
                Config.GuardianXALegArmor,
                Config.GuardianXALegSpeed,
                Config.GuardianXALegPerception,
                Config.GuardianXALegStealth,
                Config.GuardianXALegAccuracy,
                Config.GuardianXALegWeight
            );
            ArmorValues SwampCatHelmetValues = new ArmorValues(
                Config.SwampCatHelmetArmor,
                Config.SwampCatHelmetSpeed,
                Config.SwampCatHelmetPerception,
                Config.SwampCatHelmetStealth,
                Config.SwampCatHelmetAccuracy,
                Config.SwampCatHelmetWeight
            );
            ArmorValues SwampCatBodyValues = new ArmorValues(
                Config.SwampCatBodyArmor,
                Config.SwampCatBodySpeed,
                Config.SwampCatBodyPerception,
                Config.SwampCatBodyStealth,
                Config.SwampCatBodyAccuracy,
                Config.SwampCatBodyWeight
            );
            ArmorValues SwampCatLegValues = new ArmorValues(
                Config.SwampCatLegArmor,
                Config.SwampCatLegSpeed,
                Config.SwampCatLegPerception,
                Config.SwampCatLegStealth,
                Config.SwampCatLegAccuracy,
                Config.SwampCatLegWeight
            );
            setDefsFromArmorValues(Praetorian2HelmetValues, Praetorian2HelmetItem);
            setDefsFromArmorValues(Praetorian2BodyValues, Praetorian2BodyItem);
            setDefsFromArmorValues(Praetorian2LegValues, Praetorian2LegItem);
            setDefsFromArmorValues(GuardianXAHelmetValues, GuardianXAHelmetItem);
            setDefsFromArmorValues(GuardianXABodyValues, GuardianXABodyItem);
            setDefsFromArmorValues(GuardianXALegValues, GuardianXALegItem);
            setDefsFromArmorValues(SwampCatHelmetValues, SwampCatHelmetItem);
            setDefsFromArmorValues(SwampCatBodyValues, SwampCatBodyItem);
            setDefsFromArmorValues(SwampCatLegValues, SwampCatLegItem);
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
