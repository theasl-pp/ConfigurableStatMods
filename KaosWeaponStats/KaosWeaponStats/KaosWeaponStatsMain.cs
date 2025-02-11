using Base.Core;
using Base.Defs;
using Base.Levels;
using PhoenixPoint.Common.Entities.Items;
using PhoenixPoint.Common.Game;
using PhoenixPoint.Modding;
using PhoenixPoint.Tactical.Entities.Weapons;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using System;
using PhoenixPoint.Tactical.Entities;
using PhoenixPoint.Tactical.Entities.DamageKeywords;
using System.Collections.Generic;

namespace KaosWeaponStats
{
    /// <summary>
    /// This is the main mod class. Only one can exist per assembly.
    /// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
    /// </summary>
    public class KaosWeaponStatsMain : ModMain
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
            public int ApCost;
            public int HandsToUse;
            public int Weight;
            public bool StopOnFirstHit;
            public int MalfunctionIncome;
            public int MalfunctionMax;

            public WeaponValues(float[] damage, int burst, int projectilesPerShot,
                int effectiveRange, int apCost, int handsToUse, int weight,
                bool stopOnFirstHit, int malfunctionIncome, int malfunctionMax)
            {
                Damage = damage;
                Burst = burst;
                ProjectilesPerShot = projectilesPerShot;
                EffectiveRange = effectiveRange;
                ApCost = apCost;
                HandsToUse = handsToUse;
                Weight = weight;
                StopOnFirstHit = stopOnFirstHit;
                MalfunctionIncome = malfunctionIncome;
                MalfunctionMax = malfunctionMax;
            }
        }

        /// Config is accessible at any time, if any is declared.
        public new KaosWeaponStatsConfig Config => (KaosWeaponStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private WeaponDef AssaultRifle, Shotgun, SniperRifle, Pistol, MachineGun;
        private WeaponValues DefaultArValues, DefaultSgValues, DefaultSrValues, DefaultHgValues, DefaultMgValues;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            DefRepository Repo = GameUtl.GameComponent<DefRepository>();
            AssaultRifle = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Obliterator_WeaponDef"));
            Shotgun = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Redemptor_WeaponDef"));
            SniperRifle = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Subjector_WeaponDef"));
            Pistol = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Tormentor_WeaponDef"));
            MachineGun = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("KS_Devastator_WeaponDef"));

            // printWeaponDef(AssaultRifle);
            // printWeaponDef(Shotgun);
            // printWeaponDef(SniperRifle);
            // printWeaponDef(Pistol);
            // printWeaponDef(MachineGun);

            DefaultArValues = getWeaponValuesFromWeaponDef(AssaultRifle);
            DefaultSgValues = getWeaponValuesFromWeaponDef(Shotgun);
            DefaultSrValues = getWeaponValuesFromWeaponDef(SniperRifle);
            DefaultHgValues = getWeaponValuesFromWeaponDef(Pistol);
            DefaultMgValues = getWeaponValuesFromWeaponDef(MachineGun);

            OnConfigChanged();
        }

        /// <summary>
        /// Callback for when mod is disabled. This will be called even if mod cannot be safely disabled.
        /// Guaranteed to have OnModEnabled before.
        /// </summary>
        public override void OnModDisabled()
        {
            setDefsFromWeaponValues(DefaultArValues, AssaultRifle);
            setDefsFromWeaponValues(DefaultSgValues, Shotgun);
            setDefsFromWeaponValues(DefaultSrValues, SniperRifle);
            setDefsFromWeaponValues(DefaultHgValues, Pistol);
            setDefsFromWeaponValues(DefaultMgValues, MachineGun);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            float[] ArDamage = { Config.ObliteratorDamage, Config.ObliteratorShred };
            WeaponValues ArValues = new WeaponValues(
                ArDamage,
                Config.ObliteratorBurst,
                Config.ObliteratorProjectilesPerShot,
                Config.ObliteratorEffectiveRange,
                Config.ObliteratorApCost,
                Config.ObliteratorHandsToUse,
                Config.ObliteratorWeight,
                Config.ObliteratorStopOnFirstHit,
                Config.ObliteratorMalfunctionIncrease,
                Config.ObliteratorMalfunctionMaximum
            );
            float[] SgDamage = { Config.RedemptorDamage, Config.RedemptorAcid };
            WeaponValues SgValues = new WeaponValues(
                SgDamage,
                Config.RedemptorBurst,
                Config.RedemptorProjectilesPerShot,
                Config.RedemptorEffectiveRange,
                Config.RedemptorApCost,
                Config.RedemptorHandsToUse,
                Config.RedemptorWeight,
                Config.RedemptorStopOnFirstHit,
                Config.RedemptorMalfunctionIncrease,
                Config.RedemptorMalfunctionMaximum
            );
            float[] SrDamage = { Config.SubjectorDamage, Config.SubjectorPoison };
            WeaponValues SrValues = new WeaponValues(
                SrDamage,
                Config.SubjectorBurst,
                Config.SubjectorProjectilesPerShot,
                Config.SubjectorEffectiveRange,
                Config.SubjectorApCost,
                Config.SubjectorHandsToUse,
                Config.SubjectorWeight,
                Config.SubjectorStopOnFirstHit,
                Config.SubjectorMalfunctionIncrease,
                Config.SubjectorMalfunctionMaximum
            );
            float[] HgDamage = { Config.TormentorDamage, Config.TormentorPierce };
            WeaponValues HgValues = new WeaponValues(
                HgDamage,
                Config.TormentorBurst,
                Config.TormentorProjectilesPerShot,
                Config.TormentorEffectiveRange,
                Config.TormentorApCost,
                Config.TormentorHandsToUse,
                Config.TormentorWeight,
                Config.TormentorStopOnFirstHit,
                Config.TormentorMalfunctionIncrease,
                Config.TormentorMalfunctionMaximum
            );
            float[] MgDamage = { Config.DevastatorDamage, Config.DevastatorShock };
            WeaponValues MgValues = new WeaponValues(
                MgDamage,
                Config.DevastatorBurst,
                Config.DevastatorProjectilesPerShot,
                Config.DevastatorEffectiveRange,
                Config.DevastatorApCost,
                Config.DevastatorHandsToUse,
                Config.DevasatorWeight,
                Config.DevastatorStopOnFirstHit,
                Config.DevastatorMalfunctionIncrease,
                Config.DevastatorMalfunctionMaximum
            );
            setDefsFromWeaponValues(ArValues, AssaultRifle);
            setDefsFromWeaponValues(SgValues, Shotgun);
            setDefsFromWeaponValues(SrValues, SniperRifle);
            setDefsFromWeaponValues(HgValues, Pistol);
            setDefsFromWeaponValues(MgValues, MachineGun);
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
                weaponDef.EffectiveRange,
                CalculateApCost(weaponDef.APToUsePerc),
                weaponDef.HandsToUse,
                weaponDef.Weight,
                weaponDef.DamagePayload.StopOnFirstHit,
                weaponDef.WeaponMalfunction.MalfunctionPercentIncome,
                weaponDef.WeaponMalfunction.MalfunctionPercentMax
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
            weaponDef.SpreadDegrees = CalculateSpreadDegrees(weaponValues.EffectiveRange);
            weaponDef.HandsToUse = weaponValues.HandsToUse;
            weaponDef.Weight = weaponValues.Weight;
            weaponDef.WeaponMalfunction.MalfunctionPercentIncome = weaponValues.MalfunctionIncome;
            weaponDef.WeaponMalfunction.MalfunctionPercentMax = weaponValues.MalfunctionMax;
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
            Logger.LogInfo("ApCost: " + CalculateApCost(weaponDef.APToUsePerc));
            Logger.LogInfo("HandsToUse: " + weaponDef.HandsToUse);
            Logger.LogInfo("Weight: " + weaponDef.Weight);
            Logger.LogInfo("StopOnFirstHit: " + weaponDef.DamagePayload.StopOnFirstHit);
            Logger.LogInfo("MalfunctionIncome: " + weaponDef.WeaponMalfunction.MalfunctionPercentIncome);
            Logger.LogInfo("MalfunctionMax: " + weaponDef.WeaponMalfunction.MalfunctionPercentMax);
        }
    }
}
