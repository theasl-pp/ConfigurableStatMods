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

namespace BionicAugmentationStats
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class BionicAugmentationStatsMain : ModMain
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
        public new BionicAugmentationStatsConfig Config => (BionicAugmentationStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private TacticalItemDef JuggHeadItem, JuggTorsoItem, JuggLegsItem,
            ExoHeadItem, ExoTorsoItem, ExoLegsItem,
            ShinobiHeadItem, ShinobiTorsoItem, ShinobiLegsItem;
        private TacticalItemDef JuggLeftArmItem, JuggRightArmItem,
            JuggLeftLegItem, JuggRightLegItem,
            ExoLeftArmItem, ExoRightArmItem,
            ExoLeftLegItem, ExoRightLegItem,
            ShinobiLeftArmItem, ShinobiRightArmItem,
            ShinobiLeftLegItem, ShinobiRightLegItem;
        private ArmorValues DefaultJuggHeadValues, DefaultJuggTorsoValues, DefaultJuggLegsValues,
            DefaultExoHeadValues, DefaultExoTorsoValues, DefaultExoLegsValues,
            DefaultShinobiHeadValues, DefaultShinobiTorsoValues, DefaultShinobiLegsValues;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            JuggHeadItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Jugg_BIO_Helmet_BodyPartDef"));
            JuggTorsoItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Jugg_BIO_Torso_BodyPartDef"));
            JuggLegsItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Jugg_BIO_Legs_ItemDef"));
            ExoHeadItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Exo_BIO_Helmet_BodyPartDef"));
            ExoTorsoItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Exo_BIO_Torso_BodyPartDef"));
            ExoLegsItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Exo_BIO_Legs_ItemDef"));
            ShinobiHeadItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Shinobi_BIO_Helmet_BodyPartDef"));
            ShinobiTorsoItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Shinobi_BIO_Torso_BodyPartDef"));
            ShinobiLegsItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Shinobi_BIO_Legs_ItemDef"));

            JuggLeftArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Jugg_BIO_LeftArm_BodyPartDef"));
            JuggRightArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Jugg_BIO_RightArm_BodyPartDef"));
            JuggLeftLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Jugg_BIO_LeftLeg_BodyPartDef"));
            JuggRightLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Jugg_BIO_RightLeg_BodyPartDef"));
            ExoLeftArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Exo_BIO_LeftArm_BodyPartDef"));
            ExoRightArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Exo_BIO_RightArm_BodyPartDef"));
            ExoLeftLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Exo_BIO_LeftLeg_BodyPartDef"));
            ExoRightLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Exo_BIO_RightLeg_BodyPartDef"));
            ShinobiLeftArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Shinobi_BIO_LeftArm_BodyPartDef"));
            ShinobiRightArmItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Shinobi_BIO_RightArm_BodyPartDef"));
            ShinobiLeftLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Shinobi_BIO_LeftLeg_BodyPartDef"));
            ShinobiRightLegItem = Repo.GetAllDefs<TacticalItemDef>().FirstOrDefault(a => a.name.Equals("SY_Shinobi_BIO_RightLeg_BodyPartDef"));

            //printArmorDef(JuggHeadItem);
            //printArmorDef(JuggTorsoItem);
            //printArmorDef(JuggLegsItem);
            //printArmorDef(ExoHeadItem);
            //printArmorDef(ExoTorsoItem);
            //printArmorDef(ExoLegsItem);
            //printArmorDef(ShinobiHeadItem);
            //printArmorDef(ShinobiTorsoItem);
            //printArmorDef(ShinobiLegsItem);

            //printArmorDef(JuggLeftArmItem);
            //printArmorDef(JuggRightArmItem);
            //printArmorDef(JuggLeftLegItem);
            //printArmorDef(JuggRightLegItem);
            //printArmorDef(ExoLeftArmItem);
            //printArmorDef(ExoRightArmItem);
            //printArmorDef(ExoLeftLegItem);
            //printArmorDef(ExoRightLegItem);
            //printArmorDef(ShinobiLeftArmItem);
            //printArmorDef(ShinobiRightArmItem);
            //printArmorDef(ShinobiLeftLegItem);
            //printArmorDef(ShinobiRightLegItem);

            DefaultJuggHeadValues = getArmorValuesFromArmorDef(JuggHeadItem);
            DefaultJuggTorsoValues = getArmorValuesFromArmorDef(JuggTorsoItem);
            DefaultJuggLegsValues = getArmorValuesFromArmorDef(JuggLegsItem);
            DefaultExoHeadValues = getArmorValuesFromArmorDef(ExoHeadItem);
            DefaultExoTorsoValues = getArmorValuesFromArmorDef(ExoTorsoItem);
            DefaultExoLegsValues = getArmorValuesFromArmorDef(ExoLegsItem);
            DefaultShinobiHeadValues = getArmorValuesFromArmorDef(ShinobiHeadItem);
            DefaultShinobiTorsoValues = getArmorValuesFromArmorDef(ShinobiTorsoItem);
            DefaultShinobiLegsValues = getArmorValuesFromArmorDef(ShinobiLegsItem);

            DefaultJuggTorsoValues = getArmorValuesFromArmorDef(JuggLeftArmItem);
            DefaultJuggTorsoValues = getArmorValuesFromArmorDef(JuggRightArmItem);
            DefaultJuggLegsValues = getArmorValuesFromArmorDef(JuggLeftLegItem);
            DefaultJuggLegsValues = getArmorValuesFromArmorDef(JuggRightLegItem);
            DefaultExoTorsoValues = getArmorValuesFromArmorDef(ExoLeftArmItem);
            DefaultExoTorsoValues = getArmorValuesFromArmorDef(ExoRightArmItem);
            DefaultExoLegsValues = getArmorValuesFromArmorDef(ExoLeftLegItem);
            DefaultExoLegsValues = getArmorValuesFromArmorDef(ExoRightLegItem);
            DefaultShinobiTorsoValues = getArmorValuesFromArmorDef(ShinobiLeftArmItem);
            DefaultShinobiTorsoValues = getArmorValuesFromArmorDef(ShinobiRightArmItem);
            DefaultShinobiLegsValues = getArmorValuesFromArmorDef(ShinobiLeftLegItem);
            DefaultShinobiLegsValues = getArmorValuesFromArmorDef(ShinobiRightLegItem);

            OnConfigChanged();
        }

        /// <summary>
        /// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
        /// Guaranteed to have OnModEnabled before.
        /// </summary>
        public override void OnModDisabled()
        {
            setDefsFromArmorValues(DefaultJuggHeadValues, JuggHeadItem, true);
            setDefsFromArmorValues(DefaultJuggTorsoValues, JuggTorsoItem, true);
            setDefsFromArmorValues(DefaultJuggLegsValues, JuggLegsItem, true);
            setDefsFromArmorValues(DefaultExoHeadValues, ExoHeadItem, true);
            setDefsFromArmorValues(DefaultExoTorsoValues, ExoTorsoItem, true);
            setDefsFromArmorValues(DefaultExoLegsValues, ExoLegsItem, true);
            setDefsFromArmorValues(DefaultShinobiHeadValues, ShinobiHeadItem, true);
            setDefsFromArmorValues(DefaultShinobiTorsoValues, ShinobiTorsoItem, true);
            setDefsFromArmorValues(DefaultShinobiLegsValues, ShinobiLegsItem, true);

            setDefsFromArmorValues(DefaultJuggTorsoValues, JuggLeftArmItem, false);
            setDefsFromArmorValues(DefaultJuggTorsoValues, JuggRightArmItem, false);
            setDefsFromArmorValues(DefaultJuggLegsValues, JuggLeftLegItem, false);
            setDefsFromArmorValues(DefaultJuggLegsValues, JuggRightLegItem, false);
            setDefsFromArmorValues(DefaultExoTorsoValues, ExoLeftArmItem, false);
            setDefsFromArmorValues(DefaultExoTorsoValues, ExoRightArmItem, false);
            setDefsFromArmorValues(DefaultExoLegsValues, ExoLeftLegItem, false);
            setDefsFromArmorValues(DefaultExoLegsValues, ExoRightLegItem, false);
            setDefsFromArmorValues(DefaultShinobiTorsoValues, ShinobiLeftArmItem, false);
            setDefsFromArmorValues(DefaultShinobiTorsoValues, ShinobiRightArmItem, false);
            setDefsFromArmorValues(DefaultShinobiLegsValues, ShinobiLeftLegItem, false);
            setDefsFromArmorValues(DefaultShinobiLegsValues, ShinobiRightLegItem, false);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            ArmorValues JuggHeadValues = new ArmorValues(
                Config.JuggHeadArmor,
                Config.JuggHeadSpeed,
                Config.JuggHeadPerception,
                Config.JuggHeadStealth,
                Config.JuggHeadAccuracy,
                Config.JuggHeadWeight,
                Config.JuggHeadIsPermanentAugment
            );
            ArmorValues JuggTorsoValues = new ArmorValues(
                Config.JuggTorsoArmor,
                Config.JuggTorsoSpeed,
                Config.JuggTorsoPerception,
                Config.JuggTorsoStealth,
                Config.JuggTorsoAccuracy,
                Config.JuggTorsoWeight,
                Config.JuggTorsoIsPermanentAugment
            );
            ArmorValues JuggLegsValues = new ArmorValues(
                Config.JuggLegsArmor,
                Config.JuggLegsSpeed,
                Config.JuggLegsPerception,
                Config.JuggLegsStealth,
                Config.JuggLegsAccuracy,
                Config.JuggLegsWeight,
                Config.JuggLegsIsPermanentAugment
            );
            ArmorValues ExoHeadValues = new ArmorValues(
                Config.ExoHeadArmor,
                Config.ExoHeadSpeed,
                Config.ExoHeadPerception,
                Config.ExoHeadStealth,
                Config.ExoHeadAccuracy,
                Config.ExoHeadWeight,
                Config.ExoHeadIsPermanentAugment
            );
            ArmorValues ExoTorsoValues = new ArmorValues(
                Config.ExoTorsoArmor,
                Config.ExoTorsoSpeed,
                Config.ExoTorsoPerception,
                Config.ExoTorsoStealth,
                Config.ExoTorsoAccuracy,
                Config.ExoTorsoWeight,
                Config.ExoTorsoIsPermanentAugment
            );
            ArmorValues ExoLegsValues = new ArmorValues(
                Config.ExoLegsArmor,
                Config.ExoLegsSpeed,
                Config.ExoLegsPerception,
                Config.ExoLegsStealth,
                Config.ExoLegsAccuracy,
                Config.ExoLegsWeight,
                Config.ExoLegsIsPermanentAugment
            );
            ArmorValues ShinobiHeadValues = new ArmorValues(
                Config.ShinobiHeadArmor,
                Config.ShinobiHeadSpeed,
                Config.ShinobiHeadPerception,
                Config.ShinobiHeadStealth,
                Config.ShinobiHeadAccuracy,
                Config.ShinobiHeadWeight,
                Config.ShinobiHeadIsPermanentAugment
            );
            ArmorValues ShinobiTorsoValues = new ArmorValues(
                Config.ShinobiTorsoArmor,
                Config.ShinobiTorsoSpeed,
                Config.ShinobiTorsoPerception,
                Config.ShinobiTorsoStealth,
                Config.ShinobiTorsoAccuracy,
                Config.ShinobiTorsoWeight,
                Config.ShinobiTorsoIsPermanentAugment
            );
            ArmorValues ShinobiLegsValues = new ArmorValues(
                Config.ShinobiLegsArmor,
                Config.ShinobiLegsSpeed,
                Config.ShinobiLegsPerception,
                Config.ShinobiLegsStealth,
                Config.ShinobiLegsAccuracy,
                Config.ShinobiLegsWeight,
                Config.ShinobiLegsIsPermanentAugment
            );
            setDefsFromArmorValues(JuggHeadValues, JuggHeadItem, true);
            setDefsFromArmorValues(JuggTorsoValues, JuggTorsoItem, true);
            setDefsFromArmorValues(JuggLegsValues, JuggLegsItem, true);
            setDefsFromArmorValues(ExoHeadValues, ExoHeadItem, true);
            setDefsFromArmorValues(ExoTorsoValues, ExoTorsoItem, true);
            setDefsFromArmorValues(ExoLegsValues, ExoLegsItem, true);
            setDefsFromArmorValues(ShinobiHeadValues, ShinobiHeadItem, true);
            setDefsFromArmorValues(ShinobiTorsoValues, ShinobiTorsoItem, true);
            setDefsFromArmorValues(ShinobiLegsValues, ShinobiLegsItem, true);

            setDefsFromArmorValues(JuggTorsoValues, JuggLeftArmItem, false);
            setDefsFromArmorValues(JuggTorsoValues, JuggRightArmItem, false);
            setDefsFromArmorValues(JuggLegsValues, JuggLeftLegItem, false);
            setDefsFromArmorValues(JuggLegsValues, JuggRightLegItem, false);
            setDefsFromArmorValues(ExoTorsoValues, ExoLeftArmItem, false);
            setDefsFromArmorValues(ExoTorsoValues, ExoRightArmItem, false);
            setDefsFromArmorValues(ExoLegsValues, ExoLeftLegItem, false);
            setDefsFromArmorValues(ExoLegsValues, ExoRightLegItem, false);
            setDefsFromArmorValues(ShinobiTorsoValues, ShinobiLeftArmItem, false);
            setDefsFromArmorValues(ShinobiTorsoValues, ShinobiRightArmItem, false);
            setDefsFromArmorValues(ShinobiLegsValues, ShinobiLeftLegItem, false);
            setDefsFromArmorValues(ShinobiLegsValues, ShinobiRightLegItem, false);
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
                armorDef.IsPermanentAugment = armorValues.IsPermanentAugment;
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
