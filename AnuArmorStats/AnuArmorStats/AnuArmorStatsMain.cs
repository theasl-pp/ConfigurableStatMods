using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
using System.Linq;
using UnityEngine;

namespace AnuArmorStats
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class AnuArmorStatsMain : ModMain
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
        public new AnuArmorStatsConfig Config => (AnuArmorStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private TacticalItemDef AcolyteHelmetItem, AcolyteBodyItem, AcolyteLegItem,
            AksuHelmetItem, AksuBodyItem, AksuLegItem,
            AmphionBodyItem, AmphionLegItem;
        private ArmorValues DefaultAcolyteHelmetValues, DefaultAcolyteBodyValues, DefaultAcolyteLegValues,
            DefaultAksuHelmetValues, DefaultAksuBodyValues, DefaultAksuLegValues,
            DefaultAmphionBodyValues, DefaultAmphionLegValues;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            AcolyteHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Assault_Helmet_BodyPartDef"));
            AcolyteBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Assault_Torso_BodyPartDef"));
            AcolyteLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Assault_Legs_ItemDef"));
            AksuHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Helmet_BodyPartDef"));
            AksuBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Torso_BodyPartDef"));
            AksuLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Berserker_Legs_ItemDef"));
            AmphionBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Priest_Torso_BodyPartDef"));
            AmphionLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("AN_Priest_Legs_ItemDef"));

            //printArmorDef(AcolyteHelmetItem);
            //printArmorDef(AcolyteBodyItem);
            //printArmorDef(AcolyteLegItem);
            //printArmorDef(AksuHelmetItem);
            //printArmorDef(AksuBodyItem);
            //printArmorDef(AksuLegItem);
            //printArmorDef(AmphionBodyItem);
            //printArmorDef(AmphionLegItem);

            DefaultAcolyteHelmetValues = getArmorValuesFromArmorDef(AcolyteHelmetItem);
            DefaultAcolyteBodyValues = getArmorValuesFromArmorDef(AcolyteBodyItem);
            DefaultAcolyteLegValues = getArmorValuesFromArmorDef(AcolyteLegItem);
            DefaultAksuHelmetValues = getArmorValuesFromArmorDef(AksuHelmetItem);
            DefaultAksuBodyValues = getArmorValuesFromArmorDef(AksuBodyItem);
            DefaultAksuLegValues = getArmorValuesFromArmorDef(AksuLegItem);
            DefaultAmphionBodyValues = getArmorValuesFromArmorDef(AmphionBodyItem);
            DefaultAmphionLegValues = getArmorValuesFromArmorDef(AmphionLegItem);

            OnConfigChanged();
        }

        /// <summary>
        /// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
        /// Guaranteed to have OnModEnabled before.
        /// </summary>
        public override void OnModDisabled()
        {
            setDefsFromArmorValues(DefaultAcolyteHelmetValues, AcolyteHelmetItem);
            setDefsFromArmorValues(DefaultAcolyteBodyValues, AcolyteBodyItem);
            setDefsFromArmorValues(DefaultAcolyteLegValues, AcolyteLegItem);
            setDefsFromArmorValues(DefaultAksuHelmetValues, AksuHelmetItem);
            setDefsFromArmorValues(DefaultAksuBodyValues, AksuBodyItem);
            setDefsFromArmorValues(DefaultAksuLegValues, AksuLegItem);
            setDefsFromArmorValues(DefaultAmphionBodyValues, AmphionBodyItem);
            setDefsFromArmorValues(DefaultAmphionLegValues, AmphionLegItem);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            ArmorValues AcolyteHelmetValues = new ArmorValues(
                Config.AcolyteHelmetArmor,
                Config.AcolyteHelmetSpeed,
                Config.AcolyteHelmetPerception,
                Config.AcolyteHelmetStealth,
                Config.AcolyteHelmetAccuracy,
                Config.AcolyteHelmetWeight
            );
            ArmorValues AcolyteBodyValues = new ArmorValues(
                Config.AcolyteBodyArmor,
                Config.AcolyteBodySpeed,
                Config.AcolyteBodyPerception,
                Config.AcolyteBodyStealth,
                Config.AcolyteBodyAccuracy,
                Config.AcolyteBodyWeight
            );
            ArmorValues AcolyteLegValues = new ArmorValues(
                Config.AcolyteLegArmor,
                Config.AcolyteLegSpeed,
                Config.AcolyteLegPerception,
                Config.AcolyteLegStealth,
                Config.AcolyteLegAccuracy,
                Config.AcolyteLegWeight
            );
            ArmorValues AksuHelmetValues = new ArmorValues(
                Config.AksuHelmetArmor,
                Config.AksuHelmetSpeed,
                Config.AksuHelmetPerception,
                Config.AksuHelmetStealth,
                Config.AksuHelmetAccuracy,
                Config.AksuHelmetWeight
            );
            ArmorValues AksuBodyValues = new ArmorValues(
                Config.AksuBodyArmor,
                Config.AksuBodySpeed,
                Config.AksuBodyPerception,
                Config.AksuBodyStealth,
                Config.AksuBodyAccuracy,
                Config.AksuBodyWeight
            );
            ArmorValues AksuLegValues = new ArmorValues(
                Config.AksuLegArmor,
                Config.AksuLegSpeed,
                Config.AksuLegPerception,
                Config.AksuLegStealth,
                Config.AksuLegAccuracy,
                Config.AksuLegWeight
            );
            ArmorValues AmphionBodyValues = new ArmorValues(
                Config.AmphionBodyArmor,
                Config.AmphionBodySpeed,
                Config.AmphionBodyPerception,
                Config.AmphionBodyStealth,
                Config.AmphionBodyAccuracy,
                Config.AmphionBodyWeight
            );
            ArmorValues AmphionLegValues = new ArmorValues(
                Config.AmphionLegArmor,
                Config.AmphionLegSpeed,
                Config.AmphionLegPerception,
                Config.AmphionLegStealth,
                Config.AmphionLegAccuracy,
                Config.AmphionLegWeight
            );
            setDefsFromArmorValues(AcolyteHelmetValues, AcolyteHelmetItem);
            setDefsFromArmorValues(AcolyteBodyValues, AcolyteBodyItem);
            setDefsFromArmorValues(AcolyteLegValues, AcolyteLegItem);
            setDefsFromArmorValues(AksuHelmetValues, AksuHelmetItem);
            setDefsFromArmorValues(AksuBodyValues, AksuBodyItem);
            setDefsFromArmorValues(AksuLegValues, AksuLegItem);
            setDefsFromArmorValues(AmphionBodyValues, AmphionBodyItem);
            setDefsFromArmorValues(AmphionLegValues, AmphionLegItem);
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
