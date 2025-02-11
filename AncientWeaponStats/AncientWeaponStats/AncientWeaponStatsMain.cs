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

namespace AncientWeaponStats
{
    /// <summary>
    /// This is the main mod class. Only one can exist per assembly.
    /// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
    /// </summary>
    public class AncientWeaponStatsMain : ModMain
    {
        /// <summary>
        /// defines the modifiable values for any given weapon.
        /// </summary>
        private struct WeaponValues
        {
            public float[] Damage;
            public int Burst;
            public int ProjectilesPerShot;
            public int EffectiveRange;
            public bool UseMaximumRange;
            public float AoeRadius;
            public int ApCost;
            public int HandsToUse;
            public int Weight;
            public bool StopOnFirstHit;

            public WeaponValues(float[] damage, int burst, int projectilesPerShot,
                int effectiveRange, bool useMaximumRange, float aoeRadius, int apCost,
                int handsToUse, int weight, bool stopOnFirstHit)
            {
                Damage = damage;
                Burst = burst;
                ProjectilesPerShot = projectilesPerShot;
                EffectiveRange = effectiveRange;
                UseMaximumRange = useMaximumRange;
                AoeRadius = aoeRadius;
                ApCost = apCost;
                HandsToUse = handsToUse;
                Weight = weight;
                StopOnFirstHit = stopOnFirstHit;
            }
        }

        /// Config is accessible at any time, if any is declared.
        public new AncientWeaponStatsConfig Config => (AncientWeaponStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private WeaponDef ShardGun, Scorpion, Rebuke, CrystalCrb, Scyther, Mattock;
        private WeaponValues DefaultShardGun, DefaultScorpion, DefaultRebuke, DefaultCrystalCrb, DefaultScyther, DefaultMattock;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            try
            {
                DefRepository Repo = GameUtl.GameComponent<DefRepository>();
                ShardGun = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AC_ShardGun_WeaponDef"));
                Scorpion = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AC_Scorpion_WeaponDef"));
                Rebuke = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AC_Rebuke_WeaponDef"));
                CrystalCrb = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AC_CrystalCrossbow_WeaponDef"));
                Scyther = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AC_Scyther_WeaponDef"));
                Mattock = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("AC_Mattock_WeaponDef"));

                // printWeaponDef(ShardGun);
                // printWeaponDef(Scorpion);
                // printWeaponDef(Rebuke);
                // printWeaponDef(CrystalCrb);
                // printWeaponDef(Scyther);
                // printWeaponDef(Mattock);

                DefaultShardGun = getWeaponValuesFromWeaponDef(ShardGun);
                DefaultScorpion = getWeaponValuesFromWeaponDef(Scorpion);
                DefaultRebuke = getWeaponValuesFromWeaponDef(Rebuke);
                DefaultCrystalCrb = getWeaponValuesFromWeaponDef(CrystalCrb);
                DefaultScyther = getWeaponValuesFromWeaponDef(Scyther);
                DefaultMattock = getWeaponValuesFromWeaponDef(Mattock);

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
            setDefsFromWeaponValues(DefaultShardGun, ShardGun);
            setDefsFromWeaponValues(DefaultScorpion, Scorpion);
            setDefsFromWeaponValues(DefaultRebuke, Rebuke);
            setDefsFromWeaponValues(DefaultCrystalCrb, CrystalCrb);
            setDefsFromWeaponValues(DefaultScyther, Scyther);
            setDefsFromWeaponValues(DefaultMattock, Mattock);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            float[] ShardGunDamage = { Config.ShardGunDamage, Config.ShardGunPierce };
            WeaponValues ShardGunValues = new WeaponValues(
                ShardGunDamage,
                Config.ShardGunBurst,
                Config.ShardGunProjectilesPerShot,
                Config.ShardGunEffectiveRange, false,
                ShardGun.DamagePayload.AoeRadius,
                Config.ShardGunApCost,
                Config.ShardGunHandsToUse,
                Config.ShardGunWeight,
                Config.ShardGunStopOnFirstHit
            );
            float[] ScorpionDamage = { Config.ScorpionDamage, Config.ScorpionPierce };
            WeaponValues ScorpionValues = new WeaponValues(
                ScorpionDamage,
                Config.ScorpionBurst,
                Config.ScorpionProjectilesPerShot,
                Config.ScorpionEffectiveRange, false,
                Scorpion.DamagePayload.AoeRadius,
                Config.ScorpionApCost,
                Config.ScorpionHandsToUse,
                Config.ScorpionWeight,
                Config.ScorpionStopOnFirstHit
            );
            float[] RebukeDamage = { Config.RebukeBlast, Config.RebukeShred };
            WeaponValues RebukeValues = new WeaponValues(
                RebukeDamage,
                Config.RebukeBurst,
                Config.RebukeProjectilesPerShot,
                Config.RebukeEffectiveRange, true,
                Config.RebukeAoeRadius,
                Config.RebukeApCost,
                Config.RebukeHandsToUse,
                Config.RebukeWeight,
                Config.RebukeStopOnFirstHit
            );
            float[] CrystalCrbDamage = { Config.CrystalCrbDamage, Config.CrystalCrbPierce };
            WeaponValues CrystalCrbValues = new WeaponValues(
                CrystalCrbDamage,
                Config.CrystalCrbBurst,
                Config.CrystalCrbProjectilesPerShot,
                Config.CrystalCrbEffectiveRange, false,
                CrystalCrb.DamagePayload.AoeRadius,
                Config.CrystalCrbApCost,
                Config.CrystalCrbHandsToUse,
                Config.CrystalCrbWeight,
                Config.CrystalCrbStopOnFirstHit
            );
            float[] ScytherDamage = { Config.ScytherDamage, Config.ScytherShred };
            WeaponValues ScytherValues = new WeaponValues(
                ScytherDamage,
                Config.ScytherBurst,
                Scyther.DamagePayload.ProjectilesPerShot,
                Scyther.EffectiveRange, false,
                Scyther.DamagePayload.AoeRadius,
                Config.ScytherApCost,
                Config.ScytherHandsToUse,
                Config.ScytherWeight,
                Scyther.DamagePayload.StopOnFirstHit
            );
            float[] MattockDamage = { Config.MattockDamage, Config.MattockShock };
            WeaponValues MattockValues = new WeaponValues(
                MattockDamage,
                Config.MattockBurst,
                Mattock.DamagePayload.ProjectilesPerShot,
                Mattock.EffectiveRange, false,
                Mattock.DamagePayload.AoeRadius,
                Config.MattockApCost,
                Config.MattockHandsToUse,
                Config.MattockWeight,
                Mattock.DamagePayload.StopOnFirstHit
            );
            setDefsFromWeaponValues(ShardGunValues, ShardGun);
            setDefsFromWeaponValues(ScorpionValues, Scorpion);
            setDefsFromWeaponValues(RebukeValues, Rebuke);
            setDefsFromWeaponValues(CrystalCrbValues, CrystalCrb);
            setDefsFromWeaponValues(ScytherValues, Scyther);
            setDefsFromWeaponValues(MattockValues, Mattock);
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
                weaponDef.DamagePayload.AutoFireShotCount,
                weaponDef.DamagePayload.ProjectilesPerShot,
                weaponDef.EffectiveRange, !float.IsInfinity(weaponDef.MaximumRange),
                weaponDef.DamagePayload.AoeRadius,
                CalculateApCost(weaponDef.APToUsePerc),
                weaponDef.HandsToUse,
                weaponDef.Weight,
                weaponDef.DamagePayload.StopOnFirstHit
            );
        }
        private void setDefsFromWeaponValues(WeaponValues weaponValues, WeaponDef weaponDef)
        {
            weaponDef.APToUsePerc = CalculateAPToUsePerc(weaponValues.ApCost);
            weaponDef.DamagePayload.AutoFireShotCount = weaponValues.Burst;
            for (int i = 0; i < weaponValues.Damage.Length; i++)
            {
                weaponDef.DamagePayload.DamageKeywords[i].Value = weaponValues.Damage[i];
            }
            weaponDef.DamagePayload.ProjectilesPerShot = weaponValues.ProjectilesPerShot;
            weaponDef.DamagePayload.StopOnFirstHit = weaponValues.StopOnFirstHit;
            if (weaponValues.UseMaximumRange) weaponDef.DamagePayload.Range = weaponValues.EffectiveRange;
            else weaponDef.SpreadDegrees = CalculateSpreadDegrees(weaponValues.EffectiveRange);
            weaponDef.HandsToUse = weaponValues.HandsToUse;
            weaponDef.Weight = weaponValues.Weight;
            weaponDef.DamagePayload.AoeRadius = weaponValues.AoeRadius;
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
            Logger.LogInfo("Burst: " + weaponDef.DamagePayload.AutoFireShotCount);
            Logger.LogInfo("ProjectilesPerShot: " + weaponDef.DamagePayload.ProjectilesPerShot);
            Logger.LogInfo("EffectiveRange: " + weaponDef.EffectiveRange);
            Logger.LogInfo("AoeRadius: " + weaponDef.DamagePayload.AoeRadius);
            Logger.LogInfo("ApCost: " + CalculateApCost(weaponDef.APToUsePerc));
            Logger.LogInfo("HandsToUse: " + weaponDef.HandsToUse);
            Logger.LogInfo("Weight: " + weaponDef.Weight);
            Logger.LogInfo("StopOnFirstHit: " + weaponDef.DamagePayload.StopOnFirstHit);
        }
    }
}
