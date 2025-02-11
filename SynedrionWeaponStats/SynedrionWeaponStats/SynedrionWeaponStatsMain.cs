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

namespace SynedrionWeaponStats
{
	/// <summary>
	/// This is the main mod class. Only one can exist per assembly.
	/// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
	/// </summary>
	public class SynedrionWeaponStatsMain : ModMain
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
        public new SynedrionWeaponStatsConfig Config => (SynedrionWeaponStatsConfig)base.Config;

		/// This property indicates if mod can be Safely Disabled from the game.
		/// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
		/// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
		public override bool CanSafelyDisable => true;

        private WeaponDef Deimos, Pythagoras, Hephaestus, Athena, Hera, Eros, Psyche, Arachni;
        private ItemDef DeimosClip, PythagorasClip, HephaestusClip, AthenaClip, HeraClip, ErosClip, PsycheClip, ArachniClip;
        private WeaponValues DefaultDeimos, DefaultPythagoras, DefaultHephaestus, DefaultAthena, DefaultHera, DefaultEros, DefaultPsyche, DefaultArachni;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            try
            {
                DefRepository Repo = GameUtl.GameComponent<DefRepository>();
                Deimos = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("SY_LaserAssaultRifle_WeaponDef"));
                Pythagoras = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("SY_LaserSniperRifle_WeaponDef"));
                Hephaestus = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("SY_LaserPistol_WeaponDef"));
                Athena = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("SY_NeuralSniperRifle_WeaponDef"));
                Hera = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("SY_NeuralPistol_WeaponDef"));
                Eros = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("SY_Crossbow_WeaponDef"));
                Psyche = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("SY_Venombolt_WeaponDef"));
                Arachni = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("SY_SpiderDroneLauncher_WeaponDef"));
                DeimosClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("SY_LaserAssaultRifle_AmmoClip_ItemDef"));
                PythagorasClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("SY_LaserSniperRifle_AmmoClip_ItemDef"));
                HephaestusClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("SY_LaserPistol_AmmoClip_ItemDef"));
                AthenaClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("SY_NeuralSniperRifle_AmmoClip_ItemDef"));
                HeraClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("SY_NeuralPistol_AmmoClip_ItemDef"));
                ErosClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("SY_Crossbow_AmmoClip_ItemDef"));
                PsycheClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("SY_Venombolt_AmmoClip_ItemDef"));
                ArachniClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("SY_SpiderDroneLauncher_AmmoClip_ItemDef"));

                //printWeaponDef(Deimos);
                //printWeaponDef(Pythagoras);
                //printWeaponDef(Hephaestus);
                //printWeaponDef(Athena);
                //printWeaponDef(Hera);
                //printWeaponDef(Eros);
                //printWeaponDef(Psyche);
                //printWeaponDef(Arachni);

                DefaultDeimos = getWeaponValuesFromWeaponDef(Deimos);
                DefaultPythagoras = getWeaponValuesFromWeaponDef(Pythagoras);
                DefaultHephaestus = getWeaponValuesFromWeaponDef(Hephaestus);
                DefaultAthena = getWeaponValuesFromWeaponDef(Athena);
                DefaultHera = getWeaponValuesFromWeaponDef(Hera);
                DefaultEros = getWeaponValuesFromWeaponDef(Eros);
                DefaultPsyche = getWeaponValuesFromWeaponDef(Psyche);
                DefaultArachni = getWeaponValuesFromWeaponDef(Arachni);

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
            setDefsFromWeaponValues(DefaultDeimos, Deimos, DeimosClip);
            setDefsFromWeaponValues(DefaultPythagoras, Pythagoras, PythagorasClip);
            setDefsFromWeaponValues(DefaultHephaestus, Hephaestus, HephaestusClip);
            setDefsFromWeaponValues(DefaultAthena, Athena, AthenaClip);
            setDefsFromWeaponValues(DefaultHera, Hera, HeraClip);
            setDefsFromWeaponValues(DefaultEros, Eros, ErosClip);
            setDefsFromWeaponValues(DefaultPsyche, Psyche, PsycheClip);
            setDefsFromWeaponValues(DefaultArachni, Arachni, ArachniClip);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            float[] DeimosDamage = { Config.DeimosArDamage, Config.DeimosArShred };
            WeaponValues DeimosValues = new WeaponValues(
                DeimosDamage,
                Config.DeimosArAmmoCapacity,
                Config.DeimosArBurst,
                Config.DeimosArProjectilesPerShot,
                Config.DeimosArEffectiveRange,
                Config.DeimosArApCost,
                Config.DeimosArHandsToUse,
                Config.DeimosArWeight,
                Config.DeimosArStopOnFirstHit
            );
            float[] PythagorasDamage = { Config.PythagorasSrDamage };
            WeaponValues PythagorasValues = new WeaponValues(
                PythagorasDamage,
                Config.PythagorasSrAmmoCapacity,
                Config.PythagorasSrBurst,
                Config.PythagorasSrProjectilesPerShot,
                Config.PythagorasSrEffectiveRange,
                Config.PythagorasSrApCost,
                Config.PythagorasSrHandsToUse,
                Config.PythagorasSrWeight,
                Config.PythagorasSrStopOnFirstHit
            );
            float[] HephaestusDamage = { Config.HephaestusHgDamage };
            WeaponValues HephaestusValues = new WeaponValues(
                HephaestusDamage,
                Config.HephaestusHgAmmoCapacity,
                Config.HephaestusHgBurst,
                Config.HephaestusHgProjectilesPerShot,
                Config.HephaestusHgEffectiveRange,
                Config.HephaestusHgApCost,
                Config.HephaestusHgHandsToUse,
                Config.HephaestusHgWeight,
                Config.HephaestusHgStopOnFirstHit
            );
            float[] AthenaDamage = { Config.AthenaNsDamage, Config.AthenaNsPierce, Config.AthenaNsParalysis };
            WeaponValues AthenaValues = new WeaponValues(
                AthenaDamage,
                Config.AthenaNsAmmoCapacity,
                Config.AthenaNsBurst,
                Config.AthenaNsProjectilesPerShot,
                Config.AthenaNsEffectiveRange,
                Config.AthenaNsApCost,
                Config.AthenaNsHandsToUse,
                Config.AthenaNsWeight,
                Config.AthenaNsStopOnFirstHit
            );
            float[] HeraDamage = { Config.HeraNpDamage, Config.HeraNpPierce, Config.HeraNpParalysis };
            WeaponValues HeraValues = new WeaponValues(
                HeraDamage,
                Config.HeraNpAmmoCapacity,
                Config.HeraNpBurst,
                Config.HeraNpProjectilesPerShot,
                Config.HeraNpEffectiveRange,
                Config.HeraNpApCost,
                Config.HeraNpHandsToUse,
                Config.HeraNpWeight,
                Config.HeraNpStopOnFirstHit
            );
            float[] ErosDamage = { Config.ErosCrbDamage };
            WeaponValues ErosValues = new WeaponValues(
                ErosDamage,
                Config.ErosCrbAmmoCapacity,
                Config.ErosCrbBurst,
                Config.ErosCrbProjectilesPerShot,
                Config.ErosCrbEffectiveRange,
                Config.ErosCrbApCost,
                Config.ErosCrbHandsToUse,
                Config.ErosCrbWeight,
                Config.ErosCrbStopOnFirstHit
            );
            float[] PsycheDamage = { Config.PsycheCrbDamage, Config.PsycheCrbPoison };
            WeaponValues PsycheValues = new WeaponValues(
                PsycheDamage,
                Config.PsycheCrbAmmoCapacity,
                Config.PsycheCrbBurst,
                Config.PsycheCrbProjectilesPerShot,
                Config.PsycheCrbEffectiveRange,
                Config.PsycheCrbApCost,
                Config.PsycheCrbHandsToUse,
                Config.PsycheCrbWeight,
                Config.PsycheCrbStopOnFirstHit
            );
            float[] ArachniDamage = { Config.ArachniSpDamage };
            WeaponValues ArachniValues = new WeaponValues(
                ArachniDamage,
                Config.ArachniSpAmmoCapacity,
                1,
                1,
                15,
                Config.ArachniSpApCost,
                Config.ArachniSpHandsToUse,
                Config.ArachniSpWeight,
                true
            );
            setDefsFromWeaponValues(DeimosValues, Deimos, DeimosClip);
            setDefsFromWeaponValues(PythagorasValues, Pythagoras, PythagorasClip);
            setDefsFromWeaponValues(HephaestusValues, Hephaestus, HephaestusClip);
            setDefsFromWeaponValues(AthenaValues, Athena, AthenaClip);
            setDefsFromWeaponValues(HeraValues, Hera, HeraClip);
            setDefsFromWeaponValues(ErosValues, Eros, ErosClip);
            setDefsFromWeaponValues(PsycheValues, Psyche, PsycheClip);
            setDefsFromWeaponValues(ArachniValues, Arachni, ArachniClip);
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
