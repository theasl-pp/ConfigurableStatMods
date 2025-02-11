using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Geoscape.Entities;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.Abilities;
using PhoenixPoint.Tactical.Entities.Equipments;
using System.Linq;
using UnityEngine;

namespace PhoenixArmorStats
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class PhoenixArmorStatsMain : ModMain
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
        public new PhoenixArmorStatsConfig Config => (PhoenixArmorStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private TacticalItemDef OdinHelmetItem, OdinBodyItem, OdinLegItem,
            GolemHelmetItem, GolemBodyItem, GolemLegItem,
            BansheeHelmetItem, BansheeBodyItem, BansheeLegItem;
        private ArmorValues DefaultOdinHelmetValues, DefaultOdinBodyValues, DefaultOdinLegValues,
            DefaultGolemHelmetValues, DefaultGolemBodyValues, DefaultGolemLegValues,
            DefaultBansheeHelmetValues, DefaultBansheeBodyValues, DefaultBansheeLegValues;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            OdinHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Assault_Helmet_BodyPartDef"));
            OdinBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Assault_Torso_BodyPartDef"));
            OdinLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Assault_Legs_ItemDef"));
            GolemHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Heavy_Helmet_BodyPartDef"));
            GolemBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Heavy_Torso_BodyPartDef"));
            GolemLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Heavy_Legs_ItemDef"));
            BansheeHelmetItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Helmet_BodyPartDef"));
            BansheeBodyItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Torso_BodyPartDef"));
            BansheeLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("PX_Sniper_Legs_ItemDef"));

            //printArmorDef(OdinHelmetItem);
            //printArmorDef(OdinBodyItem);
            //printArmorDef(OdinLegItem);
            //printArmorDef(GolemHelmetItem);
            //printArmorDef(GolemBodyItem);
            //printArmorDef(GolemLegItem);
            //printArmorDef(BansheeHelmetItem);
            //printArmorDef(BansheeBodyItem);
            //printArmorDef(BansheeLegItem);

            DefaultOdinHelmetValues = getArmorValuesFromArmorDef(OdinHelmetItem);
            DefaultOdinBodyValues = getArmorValuesFromArmorDef(OdinBodyItem);
            DefaultOdinLegValues = getArmorValuesFromArmorDef(OdinLegItem);
            DefaultGolemHelmetValues = getArmorValuesFromArmorDef(GolemHelmetItem);
            DefaultGolemBodyValues = getArmorValuesFromArmorDef(GolemBodyItem);
            DefaultGolemLegValues = getArmorValuesFromArmorDef(GolemLegItem);
            DefaultBansheeHelmetValues = getArmorValuesFromArmorDef(BansheeHelmetItem);
            DefaultBansheeBodyValues = getArmorValuesFromArmorDef(BansheeBodyItem);
            DefaultBansheeLegValues = getArmorValuesFromArmorDef(BansheeLegItem);

            OnConfigChanged();
        }

        /// <summary>
        /// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
        /// Guaranteed to have OnModEnabled before.
        /// </summary>
        public override void OnModDisabled()
        {
            setDefsFromArmorValues(DefaultOdinHelmetValues, OdinHelmetItem);
            setDefsFromArmorValues(DefaultOdinBodyValues, OdinBodyItem);
            setDefsFromArmorValues(DefaultOdinLegValues, OdinLegItem);
            setDefsFromArmorValues(DefaultGolemHelmetValues, GolemHelmetItem);
            setDefsFromArmorValues(DefaultGolemBodyValues, GolemBodyItem);
            setDefsFromArmorValues(DefaultGolemLegValues, GolemLegItem);
            setDefsFromArmorValues(DefaultBansheeHelmetValues, BansheeHelmetItem);
            setDefsFromArmorValues(DefaultBansheeBodyValues, BansheeBodyItem);
            setDefsFromArmorValues(DefaultBansheeLegValues, BansheeLegItem);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            ArmorValues OdinHelmetValues = new ArmorValues(
                Config.OdinHelmetArmor,
                Config.OdinHelmetSpeed,
                Config.OdinHelmetPerception,
                Config.OdinHelmetStealth,
                Config.OdinHelmetAccuracy,
                Config.OdinHelmetWeight,
                0
            );
            ArmorValues OdinBodyValues = new ArmorValues(
                Config.OdinBodyArmor,
                Config.OdinBodySpeed,
                Config.OdinBodyPerception,
                Config.OdinBodyStealth,
                Config.OdinBodyAccuracy,
                Config.OdinBodyWeight,
                0
            );
            ArmorValues OdinLegValues = new ArmorValues(
                Config.OdinLegArmor,
                Config.OdinLegSpeed,
                Config.OdinLegPerception,
                Config.OdinLegStealth,
                Config.OdinLegAccuracy,
                Config.OdinLegWeight,
                0
            );
            ArmorValues GolemHelmetValues = new ArmorValues(
                Config.GolemHelmetArmor,
                Config.GolemHelmetSpeed,
                Config.GolemHelmetPerception,
                Config.GolemHelmetStealth,
                Config.GolemHelmetAccuracy,
                Config.GolemHelmetWeight,
                0
            );
            ArmorValues GolemBodyValues = new ArmorValues(
                Config.GolemBodyArmor,
                Config.GolemBodySpeed,
                Config.GolemBodyPerception,
                Config.GolemBodyStealth,
                Config.GolemBodyAccuracy,
                Config.GolemBodyWeight,
                Config.GolemBodyFumblePerc
            );
            ArmorValues GolemLegValues = new ArmorValues(
                Config.GolemLegArmor,
                Config.GolemLegSpeed,
                Config.GolemLegPerception,
                Config.GolemLegStealth,
                Config.GolemLegAccuracy,
                Config.GolemLegWeight,
                0
            );
            ArmorValues BansheeHelmetValues = new ArmorValues(
                Config.BansheeHelmetArmor,
                Config.BansheeHelmetSpeed,
                Config.BansheeHelmetPerception,
                Config.BansheeHelmetStealth,
                Config.BansheeHelmetAccuracy,
                Config.BansheeHelmetWeight,
                0
            );
            ArmorValues BansheeBodyValues = new ArmorValues(
                Config.BansheeBodyArmor,
                Config.BansheeBodySpeed,
                Config.BansheeBodyPerception,
                Config.BansheeBodyStealth,
                Config.BansheeBodyAccuracy,
                Config.BansheeBodyWeight,
                0
            );
            ArmorValues BansheeLegValues = new ArmorValues(
                Config.BansheeLegArmor,
                Config.BansheeLegSpeed,
                Config.BansheeLegPerception,
                Config.BansheeLegStealth,
                Config.BansheeLegAccuracy,
                Config.BansheeLegWeight,
                0
            );
            setDefsFromArmorValues(OdinHelmetValues, OdinHelmetItem);
            setDefsFromArmorValues(OdinBodyValues, OdinBodyItem);
            setDefsFromArmorValues(OdinLegValues, OdinLegItem);
            setDefsFromArmorValues(GolemHelmetValues, GolemHelmetItem);
            setDefsFromArmorValues(GolemBodyValues, GolemBodyItem);
            setDefsFromArmorValues(GolemLegValues, GolemLegItem);
            setDefsFromArmorValues(BansheeHelmetValues, BansheeHelmetItem);
            setDefsFromArmorValues(BansheeBodyValues, BansheeBodyItem);
            setDefsFromArmorValues(BansheeLegValues, BansheeLegItem);
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
