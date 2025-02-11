using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using PhoenixPoint.Tactical.Entities.Weapons;
using System;
using System.Linq;
using UnityEngine;

namespace AnuWeaponStats
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class AnuWeaponStatsMain : ModMain
    {
        /// <summary>
        /// defines the modifiable values for any given weapon.
        /// </summary>
        private struct WeaponValues
        {
            public float[] Damage;
            public int AmmoCapacity;
            public int Burst;
            public int ProjectilesPerShot;
            public int EffectiveRange;
            public int ApCost;
            public int HandsToUse;
            public int Weight;
            public bool StopOnFirstHit;

            public WeaponValues(float[] damage, int ammoCapacity, int burst,
                int projectilesPerShot, int effectiveRange, int apCost,
                int handsToUse, int weight, bool stopOnFirstHit)
            {
                Damage = damage;
                AmmoCapacity = ammoCapacity;
                Burst = burst;
                ProjectilesPerShot = projectilesPerShot;
                EffectiveRange = effectiveRange;
                ApCost = apCost;
                HandsToUse = handsToUse;
                Weight = weight;
                StopOnFirstHit = stopOnFirstHit;
            }
        }
        
		/// Config is accessible at any time, if any is declared.
        public new AnuWeaponStatsConfig Config => (AnuWeaponStatsConfig)base.Config;

		/// This property indicates if mod can be Safely Disabled from the game.
		/// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
		/// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
		public override bool CanSafelyDisable => true;

        private WeaponDef Iconoclast, Harrower, NergalsWrath, Sanctifier, Redeemer, Subjugator, MarduksFist, DagonsTooth, ScionOfSharur;
        private ItemDef IconoclastClip, HarrowerClip, NergalsWrathClip, SanctifierClip, RedeemerClip, SubjugatorClip;
        private WeaponValues DefaultIconoclast, DefaultHarrower, DefaultNergalsWrath, DefaultSanctifier, DefaultRedeemer, DefaultSubjugator,
            DefaultMarduksFist, DefaultDagonsTooth, DefaultScionOfSharur;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            try
            {
                DefRepository Repo = GameUtl.GameComponent<DefRepository>();
                Iconoclast = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AN_Shotgun_WeaponDef"));
                Harrower = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AN_ShreddingShotgun_WeaponDef"));
                NergalsWrath = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AN_HandCannon_WeaponDef"));
                Sanctifier = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AN_AcidHandGun_WeaponDef"));
                Redeemer = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AN_Redemptor_WeaponDef"));
                Subjugator = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AN_Subjector_WeaponDef"));
                MarduksFist = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AN_Hammer_WeaponDef"));
                DagonsTooth = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AN_Blade_WeaponDef"));
                ScionOfSharur = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AN_Mace_WeaponDef"));
                IconoclastClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("AN_Shotgun_AmmoClip_ItemDef"));
                HarrowerClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("AN_ShreddingShotgun_AmmoClip_ItemDef"));
                NergalsWrathClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("AN_HandCannon_AmmoClip_ItemDef"));
                SanctifierClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("AN_AcidHandGun_AmmoClip_ItemDef"));
                RedeemerClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("AN_Redemptor_AmmoClip_ItemDef"));
                SubjugatorClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("AN_Subjector_AmmoClip_ItemDef"));

                //printWeaponDef(Iconoclast);
                //printWeaponDef(Harrower);
                //printWeaponDef(NergalsWrath);
                //printWeaponDef(Sanctifier);
                //printWeaponDef(Redeemer);
                //printWeaponDef(Subjugator);
                //printWeaponDef(MarduksFist);
                //printWeaponDef(DagonsTooth);
                //printWeaponDef(ScionOfSharur);

                DefaultIconoclast = getWeaponValuesFromWeaponDef(Iconoclast);
                DefaultHarrower = getWeaponValuesFromWeaponDef(Harrower);
                DefaultNergalsWrath = getWeaponValuesFromWeaponDef(NergalsWrath);
                DefaultSanctifier = getWeaponValuesFromWeaponDef(Sanctifier);
                DefaultRedeemer = getWeaponValuesFromWeaponDef(Redeemer);
                DefaultSubjugator = getWeaponValuesFromWeaponDef(Subjugator);
                DefaultMarduksFist = getWeaponValuesFromWeaponDef(MarduksFist);
                DefaultDagonsTooth = getWeaponValuesFromWeaponDef(DagonsTooth);
                DefaultScionOfSharur = getWeaponValuesFromWeaponDef(ScionOfSharur);

                OnConfigChanged();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message, ex);
                throw ex;
            }
        }

		/// <summary>
		/// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
		/// Guaranteed to have OnModEnabled before.
		/// </summary>
		public override void OnModDisabled()
        {
            setDefsFromWeaponValues(DefaultIconoclast, Iconoclast, IconoclastClip);
            setDefsFromWeaponValues(DefaultHarrower, Harrower, HarrowerClip);
            setDefsFromWeaponValues(DefaultNergalsWrath, NergalsWrath, NergalsWrathClip);
            setDefsFromWeaponValues(DefaultSanctifier, Sanctifier, SanctifierClip);
            setDefsFromWeaponValues(DefaultRedeemer, Redeemer, RedeemerClip);
            setDefsFromWeaponValues(DefaultSubjugator, Subjugator, SubjugatorClip);
            setDefsFromWeaponValues(DefaultMarduksFist, MarduksFist, null);
            setDefsFromWeaponValues(DefaultDagonsTooth, DagonsTooth, null);
            setDefsFromWeaponValues(DefaultScionOfSharur, ScionOfSharur, null);
        }

		/// <summary>
		/// Callback for when any property from mod's config is changed.
		/// </summary>
		public override void OnConfigChanged()
        {
            float[] IconoclastDamage = { Config.IconoclastDamage };
            WeaponValues IconoclastValues = new WeaponValues(
                IconoclastDamage,
                Config.IconoclastAmmoCapacity,
                Config.IconoclastBurst,
                Config.IconoclastProjectilesPerShot,
                Config.IconoclastEffectiveRange,
                Config.IconoclastApCost,
                Config.IconoclastHandsToUse,
                Config.IconoclastWeight,
                Config.IconoclastStopOnFirstHit
            );
            float[] HarrowerDamage = { Config.HarrowerDamage, Config.HarrowerShred };
            WeaponValues HarrowerValues = new WeaponValues(
                HarrowerDamage,
                Config.HarrowerAmmoCapacity,
                Config.HarrowerBurst,
                Config.HarrowerProjectilesPerShot,
                Config.HarrowerEffectiveRange,
                Config.HarrowerApCost,
                Config.HarrowerHandsToUse,
                Config.HarrowerWeight,
                Config.HarrowerStopOnFirstHit
            );
            float[] NergalsWrathDamage = { Config.NergalsWrathDamage, Config.NergalsWrathShred };
            WeaponValues NergalsWrathValues = new WeaponValues(
                NergalsWrathDamage,
                Config.NergalsWrathAmmoCapacity,
                Config.NergalsWrathBurst,
                Config.NergalsWrathProjectilesPerShot,
                Config.NergalsWrathEffectiveRange,
                Config.NergalsWrathApCost,
                Config.NergalsWrathHandsToUse,
                Config.NergalsWrathWeight,
                Config.NergalsWrathStopOnFirstHit
            );
            float[] SanctifierDamage = { Config.SanctifierDamage, Config.SanctifierAcid };
            WeaponValues SanctifierValues = new WeaponValues(
                SanctifierDamage,
                Config.SanctifierAmmoCapacity,
                Config.SanctifierBurst,
                Config.SanctifierProjectilesPerShot,
                Config.SanctifierEffectiveRange,
                Config.SanctifierApCost,
                Config.SanctifierHandsToUse,
                Config.SanctifierWeight,
                Config.SanctifierStopOnFirstHit
            );
            float[] RedeemerDamage = { Config.RedeemerDamage, Config.RedeemerPierce, Config.RedeemerVirus };
            WeaponValues RedeemerValues = new WeaponValues(
                RedeemerDamage,
                Config.RedeemerAmmoCapacity,
                Config.RedeemerBurst,
                Config.RedeemerProjectilesPerShot,
                Config.RedeemerEffectiveRange,
                Config.RedeemerApCost,
                Config.RedeemerHandsToUse,
                Config.RedeemerWeight,
                Config.RedeemerStopOnFirstHit
            );
            float[] SubjugatorDamage = { Config.SubjugatorDamage, Config.SubjugatorPierce , Config.SubjugatorVirus };
            WeaponValues SubjugatorValues = new WeaponValues(
                SubjugatorDamage,
                Config.SubjugatorAmmoCapacity,
                Config.SubjugatorBurst,
                Config.SubjugatorProjectilesPerShot,
                Config.SubjugatorEffectiveRange,
                Config.SubjugatorApCost,
                Config.SubjugatorHandsToUse,
                Config.SubjugatorWeight,
                Config.SubjugatorStopOnFirstHit
            );
            float[] MarduksFistDamage = { Config.MarduksFistDamage, Config.MarduksFistShock };
            WeaponValues MarduksFistValues = new WeaponValues(
                MarduksFistDamage,
                0,
                Config.MarduksFistBurst,
                1,
                1,
                Config.MarduksFistApCost,
                Config.MarduksFistHandsToUse,
                Config.MarduksFistWeight,
                false
            );
            float[] DagonsToothDamage = { Config.DagonsToothDamage, Config.DagonsToothBleed };
            WeaponValues DagonsToothValues = new WeaponValues(
                DagonsToothDamage,
                0,
                Config.DagonsToothBurst,
                1,
                1,
                Config.DagonsToothApCost,
                Config.DagonsToothHandsToUse,
                Config.DagonsToothWeight,
                false
            );
            float[] ScionOfSharurDamage = { Config.ScionOfSharurDamage, Config.ScionOfSharurPierce, Config.ScionOfSharurVirus };
            WeaponValues ScionOfSharurValues = new WeaponValues(
                ScionOfSharurDamage,
                0,
                Config.ScionOfSharurBurst,
                1,
                1,
                Config.ScionOfSharurApCost,
                Config.ScionOfSharurHandsToUse,
                Config.ScionOfSharurWeight,
                false
            );
            setDefsFromWeaponValues(IconoclastValues, Iconoclast, IconoclastClip);
            setDefsFromWeaponValues(HarrowerValues, Harrower, HarrowerClip);
            setDefsFromWeaponValues(NergalsWrathValues, NergalsWrath, NergalsWrathClip);
            setDefsFromWeaponValues(SanctifierValues, Sanctifier, SanctifierClip);
            setDefsFromWeaponValues(RedeemerValues, Redeemer, RedeemerClip);
            setDefsFromWeaponValues(SubjugatorValues, Subjugator, SubjugatorClip);
            setDefsFromWeaponValues(MarduksFistValues, MarduksFist, null);
            setDefsFromWeaponValues(DagonsToothValues, DagonsTooth, null);
            setDefsFromWeaponValues(ScionOfSharurValues, ScionOfSharur, null);
        }

        /* CALCULATION FUNCTIONS */

        private float CalculateSpreadDegrees(float EffectiveRange)
        {
            if (EffectiveRange == 999) return 0f;
            return 1f / (EffectiveRange / 41f);
        }

        private int CalculateAPToUsePerc(int ApCost)
        {
            return ApCost * 25;
        }
        private int CalculateApCost(int APToUsePerc)
        {
            return APToUsePerc / 25;
        }

        /* WEAPON DATA FUNCTIONS */

        private WeaponValues getWeaponValuesFromWeaponDef(WeaponDef weaponDef)
        {
            float[] Damage = new float[weaponDef.DamagePayload.DamageKeywords.Count];
            for (int i = 0; i < weaponDef.DamagePayload.DamageKeywords.Count; i++)
            {
                Damage[i] = weaponDef.DamagePayload.DamageKeywords[i].Value;
            }
            return new WeaponValues(
                Damage,
                weaponDef.ChargesMax,
                weaponDef.DamagePayload.AutoFireShotCount,
                weaponDef.DamagePayload.ProjectilesPerShot,
                weaponDef.EffectiveRange,
                CalculateApCost(weaponDef.APToUsePerc),
                weaponDef.HandsToUse,
                weaponDef.Weight,
                weaponDef.DamagePayload.StopOnFirstHit
            );
        }
        private void setDefsFromWeaponValues(WeaponValues weaponValues, WeaponDef weaponDef, ItemDef itemDef)
        {
            weaponDef.APToUsePerc = CalculateAPToUsePerc(weaponValues.ApCost);
            weaponDef.DamagePayload.AutoFireShotCount = weaponValues.Burst;
            for (int i = 0; i < weaponValues.Damage.Length; i++)
            {
                weaponDef.DamagePayload.DamageKeywords[i].Value = weaponValues.Damage[i];
            }
            weaponDef.DamagePayload.ProjectilesPerShot = weaponValues.ProjectilesPerShot;
            weaponDef.DamagePayload.StopOnFirstHit = weaponValues.StopOnFirstHit;
            weaponDef.SpreadDegrees = CalculateSpreadDegrees(weaponValues.EffectiveRange);
            weaponDef.HandsToUse = weaponValues.HandsToUse;
            weaponDef.ChargesMax = weaponValues.AmmoCapacity;
            if (itemDef != null) itemDef.ChargesMax = weaponValues.AmmoCapacity;
            weaponDef.Weight = weaponValues.Weight;
        }

        private void printWeaponDef(WeaponDef weaponDef)
        {
            Logger.LogInfo("=== " + weaponDef.name + " ===");
            Logger.LogInfo("Damage:");
            for (int i = 0; i < weaponDef.DamagePayload.DamageKeywords.Count; i++)
            {
                DamageKeywordPair damageKeywordPair = weaponDef.DamagePayload.DamageKeywords[i];
                Logger.LogInfo(" - " + i + " " + damageKeywordPair.ToString());
            }
            Logger.LogInfo("AmmoCapacity: " + weaponDef.ChargesMax);
            Logger.LogInfo("Burst: " + weaponDef.DamagePayload.AutoFireShotCount);
            Logger.LogInfo("ProjectilesPerShot: " + weaponDef.DamagePayload.ProjectilesPerShot);
            Logger.LogInfo("EffectiveRange: " + weaponDef.EffectiveRange);
            Logger.LogInfo("ApCost: " + CalculateApCost(weaponDef.APToUsePerc));
            Logger.LogInfo("HandsToUse: " + weaponDef.HandsToUse);
            Logger.LogInfo("Weight: " + weaponDef.Weight);
            Logger.LogInfo("StopOnFirstHit: " + weaponDef.DamagePayload.StopOnFirstHit);
        }
    }
}
