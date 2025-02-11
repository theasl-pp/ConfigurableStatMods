using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using PhoenixPoint.Tactical.Entities.Weapons;
using System.Linq;
using UnityEngine;

namespace LivingWeaponStats
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class LivingWeaponStatsMain : ModMain
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
        public new LivingWeaponStatsConfig Config => (LivingWeaponStatsConfig)base.Config;

		/// This property indicates if mod can be Safely Disabled from the game.
		/// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
		/// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
		public override bool CanSafelyDisable => true;

        private WeaponDef AssaultRifle, MachineGun;
        private WeaponValues DefaultArValues, DefaultMgValues;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            AssaultRifle = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_AcidAssaultRifle_WeaponDef"));
            MachineGun = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_PoisonMachineGun_WeaponDef"));

            //printWeaponDef(AssaultRifle);
            //printWeaponDef(MachineGun);

            DefaultArValues = getWeaponValuesFromWeaponDef(AssaultRifle);
            DefaultMgValues = getWeaponValuesFromWeaponDef(MachineGun);

            OnConfigChanged();
        }

		/// <summary>
		/// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
		/// Guaranteed to have OnModEnabled before.
		/// </summary>
		public override void OnModDisabled()
        {
            setDefsFromWeaponValues(DefaultArValues, AssaultRifle, null);
            setDefsFromWeaponValues(DefaultMgValues, MachineGun, null);
        }

		/// <summary>
		/// Callback for when any property from mod's config is changed.
		/// </summary>
		public override void OnConfigChanged()
        {
            float[] ArDamage = { Config.DanchevArDamage, Config.DanchevArAcid };
            WeaponValues ArValues = new WeaponValues(
                ArDamage,
                0,
                Config.DanchevArBurst,
                Config.DanchevArProjectilesPerShot,
                Config.DanchevArEffectiveRange,
                Config.DanchevArApCost,
                Config.DanchevArHandsToUse,
                Config.DanchevArWeight,
                Config.DanchevArStopOnFirstHit
            );
            float[] MgDamage = { Config.DanchevMgDamage, Config.DanchevMgPoison };
            WeaponValues MgValues = new WeaponValues(
                MgDamage,
                0,
                Config.DanchevMgBurst,
                Config.DanchevMgProjectilesPerShot,
                Config.DanchevMgEffectiveRange,
                Config.DanchevMgApCost,
                Config.DanchevMgHandsToUse,
                Config.DanchevMgWeight,
                Config.DanchevMgStopOnFirstHit
            );
            setDefsFromWeaponValues(ArValues, AssaultRifle, null);
            setDefsFromWeaponValues(MgValues, MachineGun, null);
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
            weaponDef.Weight = weaponValues.Weight;
            if (itemDef != null) itemDef.ChargesMax = weaponValues.AmmoCapacity;
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
