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

namespace NewJerichoWeaponStats
{
    /// <summary>
    /// This is the main mod class. Only one can exist per assembly.
    /// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
    /// </summary>
    public class NewJerichoWeaponStatsMain : ModMain
    {
        /// <summary>
        /// defines the modifiable values for any given weapon.
        /// </summary>
        private struct WeaponValues
        {
            public float[] Damage;
            public int AmmoCapacity;
            public int ClipCharges;
            public int Burst;
            public int ProjectilesPerShot;
            public int EffectiveRange;
            public bool UseMaximumRange;
            public float AoeRadius;
            public float ConeRadius;
            public int ApCost;
            public int HandsToUse;
            public int Weight;
            public bool StopOnFirstHit;

            public WeaponValues(float[] damage, int ammoCapacity, int clipCharges,
                int burst, int projectilesPerShot, int effectiveRange, bool useMaximumRange,
                float aoeRadius, float coneRadius, int apCost, int handsToUse,
                int weight, bool stopOnFirstHit)
            {
                Damage = damage;
                AmmoCapacity = ammoCapacity;
                ClipCharges = clipCharges;
                Burst = burst;
                ProjectilesPerShot = projectilesPerShot;
                EffectiveRange = effectiveRange;
                UseMaximumRange = useMaximumRange;
                AoeRadius = aoeRadius;
                ConeRadius = coneRadius;
                ApCost = apCost;
                HandsToUse = handsToUse;
                Weight = weight;
                StopOnFirstHit = stopOnFirstHit;
            }
        }

        /// Config is accessible at any time, if any is declared.
        public new NewJerichoWeaponStatsConfig Config => (NewJerichoWeaponStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private WeaponDef Bulldog, Piranha, Cyclops, Raven, IronFury, Deceptor, Archangel, Fury, Thor, Dante, Defender, Enforcer, Watcher, Rattlesnake, Arms;
        private ItemDef BulldogClip, PiranhaClip, CyclopsClip, RavenClip, IronFuryClip, DeceptorClip, ArchangelClip, FuryClip, ThorClip, DanteClip,
            DefenderClip, EnforcerClip, WatcherClip, RattlesnakeClip, ArmsClip;
        private WeaponValues DefaultBulldog, DefaultPiranha, DefaultCyclops, DefaultRaven, DefaultIronFury, DefaultDeceptor, DefaultArchangel,
            DefaultFury, DefaultThor, DefaultDante, DefaultDefender, DefaultEnforcer, DefaultWatcher, DefaultRattlesnake, DefaultArms;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            try
            {
                DefRepository Repo = GameUtl.GameComponent<DefRepository>();
                Bulldog = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_Gauss_AssaultRifle_WeaponDef"));
                Piranha = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_PRCR_AssaultRifle_WeaponDef"));
                Cyclops = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_Gauss_SniperRifle_WeaponDef"));
                Raven = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_PRCR_SniperRifle_WeaponDef"));
                IronFury = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_Gauss_HandGun_WeaponDef"));
                Deceptor = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_Gauss_MachineGun_WeaponDef"));
                Archangel = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_HeavyRocketLauncher_WeaponDef"));
                Fury = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_RocketLauncherPack_WeaponDef"));
                Thor = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_GuidedMissileLauncherPack_WeaponDef"));
                Dante = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_FlameThrower_WeaponDef"));
                Defender = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_Gauss_PDW_WeaponDef"));
                Enforcer = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_PRCR_PDW_WeaponDef"));
                Watcher = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_TechTurretGun_WeaponDef"));
                Rattlesnake = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_PRCRTechTurretGun_WeaponDef"));
                Arms = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("NJ_Technician_MechArms_WeaponDef"));
                BulldogClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Gauss_AssaultRifle_AmmoClip_ItemDef"));
                PiranhaClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_PRCR_AssaultRifle_AmmoClip_ItemDef"));
                CyclopsClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Gauss_SniperRifle_AmmoClip_ItemDef"));
                RavenClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_PRCR_SniperRifle_AmmoClip_ItemDef"));
                IronFuryClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Gauss_HandGun_AmmoClip_ItemDef"));
                DeceptorClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Gauss_MachineGun_AmmoClip_ItemDef"));
                ArchangelClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_HeavyRocketLauncher_AmmoClip_ItemDef"));
                FuryClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_RocketLauncher_AmmoClip_ItemDef"));
                ThorClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_GuidedMissileLauncher_AmmoClip_ItemDef"));
                DanteClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Flamethrower_AmmoClip_ItemDef"));
                DefenderClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_Gauss_PDW_WeaponDef"));
                EnforcerClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_PRCR_PDW_AmmoClip_ItemDef"));
                WatcherClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_TechTurretGun_AmmoClip_ItemDef"));
                RattlesnakeClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("NJ_PRCRTechTurretGun_AmmoClip_ItemDef"));
                ArmsClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("MechArms_AmmoClip_ItemDef"));

                //printDefs(Bulldog, BulldogClip);
                //printDefs(Piranha, PiranhaClip);
                //printDefs(Cyclops, CyclopsClip);
                //printDefs(Raven, RavenClip);
                //printDefs(IronFury, IronFuryClip);
                //printDefs(Deceptor, DeceptorClip);
                //printDefs(Archangel, ArchangelClip);
                //printDefs(Fury, FuryClip);
                //printDefs(Thor, ThorClip);
                //printDefs(Dante, DanteClip);
                //printDefs(Defender, DefenderClip);
                //printDefs(Enforcer, EnforcerClip);
                //printDefs(Watcher, WatcherClip);
                //printDefs(Rattlesnake, RattlesnakeClip);
                //printDefs(Arms, ArmsClip);

                DefaultBulldog = getWeaponValuesFromDefs(Bulldog, BulldogClip);
                DefaultPiranha = getWeaponValuesFromDefs(Piranha, PiranhaClip);
                DefaultCyclops = getWeaponValuesFromDefs(Cyclops, CyclopsClip);
                DefaultRaven = getWeaponValuesFromDefs(Raven, RavenClip);
                DefaultIronFury = getWeaponValuesFromDefs(IronFury, IronFuryClip);
                DefaultDeceptor = getWeaponValuesFromDefs(Deceptor, DeceptorClip);
                DefaultArchangel = getWeaponValuesFromDefs(Archangel, ArchangelClip);
                DefaultFury = getWeaponValuesFromDefs(Fury, FuryClip);
                DefaultThor = getWeaponValuesFromDefs(Thor, ThorClip);
                DefaultDante = getWeaponValuesFromDefs(Dante, DanteClip);
                DefaultDefender = getWeaponValuesFromDefs(Defender, DefenderClip);
                DefaultEnforcer = getWeaponValuesFromDefs(Enforcer, EnforcerClip);
                DefaultWatcher = getWeaponValuesFromDefs(Watcher, WatcherClip);
                DefaultRattlesnake = getWeaponValuesFromDefs(Rattlesnake, RattlesnakeClip);
                DefaultArms = getWeaponValuesFromDefs(Arms, ArmsClip);

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
            setDefsFromWeaponValues(DefaultBulldog, Bulldog, BulldogClip);
            setDefsFromWeaponValues(DefaultPiranha, Piranha, PiranhaClip);
            setDefsFromWeaponValues(DefaultCyclops, Cyclops, CyclopsClip);
            setDefsFromWeaponValues(DefaultRaven, Raven, RavenClip);
            setDefsFromWeaponValues(DefaultIronFury, IronFury, IronFuryClip);
            setDefsFromWeaponValues(DefaultDeceptor, Deceptor, DeceptorClip);
            setDefsFromWeaponValues(DefaultArchangel, Archangel, ArchangelClip);
            setDefsFromWeaponValues(DefaultFury, Fury, FuryClip);
            setDefsFromWeaponValues(DefaultThor, Thor, ThorClip);
            setDefsFromWeaponValues(DefaultDante, Dante, DanteClip);
            setDefsFromWeaponValues(DefaultDefender, Defender, DefenderClip);
            setDefsFromWeaponValues(DefaultEnforcer, Enforcer, EnforcerClip);
            setDefsFromWeaponValues(DefaultWatcher, Watcher, WatcherClip);
            setDefsFromWeaponValues(DefaultRattlesnake, Rattlesnake, RattlesnakeClip);
            setDefsFromWeaponValues(DefaultArms, Arms, ArmsClip);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            float[] BulldogDamage = { Config.BulldogArDamage, Config.BulldogArShred };
            WeaponValues BulldogValues = new WeaponValues(
                BulldogDamage,
                Config.BulldogArAmmoCapacity,
                Config.BulldogArAmmoCapacity,
                Config.BulldogArBurst,
                Config.BulldogArProjectilesPerShot,
                Config.BulldogArEffectiveRange, false,
                Bulldog.DamagePayload.AoeRadius,
                Bulldog.DamagePayload.ConeRadius,
                Config.BulldogArApCost,
                Config.BulldogArHandsToUse,
                Config.BulldogArWeight,
                Config.BulldogArStopOnFirstHit
            );
            float[] PiranhaDamage = { Config.PiranhaArDamage, Config.PiranhaArPierce };
            WeaponValues PiranhaValues = new WeaponValues(
                PiranhaDamage,
                Config.PiranhaArAmmoCapacity,
                Config.PiranhaArAmmoCapacity,
                Config.PiranhaArBurst,
                Config.PiranhaArProjectilesPerShot,
                Config.PiranhaArEffectiveRange, false,
                Piranha.DamagePayload.AoeRadius,
                Piranha.DamagePayload.ConeRadius,
                Config.PiranhaArApCost,
                Config.PiranhaArHandsToUse,
                Config.PiranhaArWeight,
                Config.PiranhaArStopOnFirstHit
            );
            float[] CyclopsDamage = { Config.CyclopsSrDamage };
            WeaponValues CyclopsValues = new WeaponValues(
                CyclopsDamage,
                Config.CyclopsSrAmmoCapacity,
                Config.CyclopsSrAmmoCapacity,
                Config.CyclopsSrBurst,
                Config.CyclopsSrProjectilesPerShot,
                Config.CyclopsSrEffectiveRange, false,
                Cyclops.DamagePayload.AoeRadius,
                Cyclops.DamagePayload.ConeRadius,
                Config.CyclopsSrApCost,
                Config.CyclopsSrHandsToUse,
                Config.CyclopsSrWeight,
                Config.CyclopsSrStopOnFirstHit
            );
            float[] RavenDamage = { Config.RavenSrDamage, Config.RavenSrPierce };
            WeaponValues RavenValues = new WeaponValues(
                RavenDamage,
                Config.RavenSrAmmoCapacity,
                Config.RavenSrAmmoCapacity,
                Config.RavenSrBurst,
                Config.RavenSrProjectilesPerShot,
                Config.RavenSrEffectiveRange, false,
                Raven.DamagePayload.AoeRadius,
                Raven.DamagePayload.ConeRadius,
                Config.RavenSrApCost,
                Config.RavenSrHandsToUse,
                Config.RavenSrWeight,
                Config.RavenSrStopOnFirstHit
            );
            float[] IronFuryDamage = { Config.IronFuryHgDamage };
            WeaponValues IronFuryValues = new WeaponValues(
                IronFuryDamage,
                Config.IronFuryHgAmmoCapacity,
                Config.IronFuryHgAmmoCapacity,
                Config.IronFuryHgBurst,
                Config.IronFuryHgProjectilesPerShot,
                Config.IronFuryHgEffectiveRange, false,
                IronFury.DamagePayload.AoeRadius,
                IronFury.DamagePayload.ConeRadius,
                Config.IronFuryHgApCost,
                Config.IronFuryHgHandsToUse,
                Config.IronFuryHgWeight,
                Config.IronFuryHgStopOnFirstHit
            );
            float[] DeceptorDamage = { Config.DeceptorMgDamage, Config.DeceptorMgShred };
            WeaponValues DeceptorValues = new WeaponValues(
                DeceptorDamage,
                Config.DeceptorMgAmmoCapacity,
                Config.DeceptorMgAmmoCapacity,
                Config.DeceptorMgBurst,
                Config.DeceptorMgProjectilesPerShot,
                Config.DeceptorMgEffectiveRange, false,
                Deceptor.DamagePayload.AoeRadius,
                Deceptor.DamagePayload.ConeRadius,
                Config.DeceptorMgApCost,
                Config.DeceptorMgHandsToUse,
                Config.DeceptorMgWeight,
                Config.DeceptorMgStopOnFirstHit
            );
            float[] ArchangelDamage = { Config.ArchangelRlBlast, Config.ArchangelRlShred };
            WeaponValues ArchangelValues = new WeaponValues(
                ArchangelDamage,
                Config.ArchangelRlAmmoCapacity,
                Config.ArchangelRlAmmoCapacity,
                Config.ArchangelRlBurst,
                Config.ArchangelRlProjectilesPerShot,
                Archangel.EffectiveRange, true,
                Config.ArchangelRlAoeRadius,
                Archangel.DamagePayload.ConeRadius,
                Config.ArchangelRlApCost,
                Config.ArchangelRlHandsToUse,
                Config.ArchangelRlWeight,
                Archangel.DamagePayload.StopOnFirstHit
            );
            float[] FuryDamage = { Config.FuryBlast, Config.FuryShred };
            WeaponValues FuryValues = new WeaponValues(
                FuryDamage,
                Config.FuryAmmoCapacity,
                Config.FuryClipCharges,
                Config.FuryBurst,
                Config.FuryProjectilesPerShot,
                Config.FuryEffectiveRange, true,
                Config.FuryAoeRadius,
                Fury.DamagePayload.ConeRadius,
                Config.FuryApCost,
                Config.FuryHandsToUse,
                Config.FuryWeight,
                Fury.DamagePayload.StopOnFirstHit
            );
            float[] ThorDamage = { Config.ThorAmlBlast, Config.ThorAmlShred };
            WeaponValues ThorValues = new WeaponValues(
                ThorDamage,
                Config.ThorAmlAmmoCapacity,
                Config.ThorAmlClipCharges,
                Config.ThorAmlBurst,
                Config.ThorAmlProjectilesPerShot,
                Config.ThorAmlEffectiveRange, true,
                Config.ThorAmlAoeRadius,
                Thor.DamagePayload.ConeRadius,
                Config.ThorAmlApCost,
                Config.ThorAmlHandsToUse,
                Config.ThorAmlWeight,
                Thor.DamagePayload.StopOnFirstHit
            );
            float[] DanteDamage = { Config.DanteFtDamage, Config.DanteFtFire };
            WeaponValues DanteValues = new WeaponValues(
                DanteDamage,
                Config.DanteFtAmmoCapacity,
                Config.DanteFtAmmoCapacity,
                Config.DanteFtBurst,
                Config.DanteFtProjectilesPerShot,
                Config.DanteFtEffectiveRange, true,
                Dante.DamagePayload.AoeRadius,
                Config.DanteFtConeRadius,
                Config.DanteFtApCost,
                Config.DanteFtHandsToUse,
                Config.DanteFtWeight,
                Dante.DamagePayload.StopOnFirstHit
            );
            float[] DefenderDamage = { Config.VdmDefenderDamage, Config.VdmDefenderShred };
            WeaponValues DefenderValues = new WeaponValues(
                DefenderDamage,
                Config.VdmDefenderAmmoCapacity,
                Config.VdmDefenderAmmoCapacity,
                Config.VdmDefenderBurst,
                Config.VdmDefenderProjectilesPerShot,
                Config.VdmDefenderEffectiveRange, false,
                Defender.DamagePayload.AoeRadius,
                Defender.DamagePayload.ConeRadius,
                Config.VdmDefenderApCost,
                Config.VdmDefenderHandsToUse,
                Config.VdmDefenderWeight,
                Config.VdmDefenderStopOnFirstHit
            );
            float[] EnforcerDamage = { Config.VdmEnforcerDamage, Config.VdmEnforcerPierce };
            WeaponValues EnforcerValues = new WeaponValues(
                EnforcerDamage,
                Config.VdmEnforcerAmmoCapacity,
                Config.VdmEnforcerAmmoCapacity,
                Config.VdmEnforcerBurst,
                Config.VdmEnforcerProjectilesPerShot,
                Config.VdmEnforcerEffectiveRange, false,
                Enforcer.DamagePayload.AoeRadius,
                Enforcer.DamagePayload.ConeRadius,
                Config.VdmEnforcerApCost,
                Config.VdmEnforcerHandsToUse,
                Config.VdmEnforcerWeight,
                Config.VdmEnforcerStopOnFirstHit
            );
            float[] WatcherDamage = { Config.WatcherAtDamage, Config.WatcherAtShred };
            WeaponValues WatcherValues = new WeaponValues(
                WatcherDamage,
                Config.WatcherAtAmmoCapacity,
                Config.WatcherAtAmmoCapacity,
                Config.WatcherAtBurst,
                Config.WatcherAtProjectilesPerShot,
                Config.WatcherAtEffectiveRange, false,
                Watcher.DamagePayload.AoeRadius,
                Watcher.DamagePayload.ConeRadius,
                Config.WatcherAtApCost,
                Watcher.HandsToUse,
                Config.WatcherAtWeight,
                Config.WatcherAtStopOnFirstHit
            );
            float[] RattlesnakeDamage = { Config.RattlesnakeAtDamage, Config.RattlesnakeAtPierce };
            WeaponValues RattlesnakeValues = new WeaponValues(
                RattlesnakeDamage,
                Config.RattlesnakeAtAmmoCapacity,
                Config.RattlesnakeAtAmmoCapacity,
                Config.RattlesnakeAtBurst,
                Config.RattlesnakeAtProjectilesPerShot,
                Config.RattlesnakeAtEffectiveRange, false,
                Rattlesnake.DamagePayload.AoeRadius,
                Rattlesnake.DamagePayload.ConeRadius,
                Config.RattlesnakeAtApCost,
                Rattlesnake.HandsToUse,
                Config.RattlesnakeAtWeight,
                Config.RattlesnakeAtStopOnFirstHit
            );
            float[] ArmsDamage = { Config.VvaArmsShock };
            WeaponValues ArmsValues = new WeaponValues(
                ArmsDamage,
                Config.VvaArmsAmmoCapacity,
                Config.VvaArmsAmmoCapacity,
                Config.VvaArmsBurst,
                Arms.DamagePayload.ProjectilesPerShot,
                Arms.EffectiveRange, true,
                Arms.DamagePayload.AoeRadius,
                Arms.DamagePayload.ConeRadius,
                Config.VvaArmsApCost,
                Config.VvaArmsHandsToUse,
                Config.VvaArmsWeight,
                Arms.DamagePayload.StopOnFirstHit
            );
            setDefsFromWeaponValues(BulldogValues, Bulldog, BulldogClip);
            setDefsFromWeaponValues(PiranhaValues, Piranha, PiranhaClip);
            setDefsFromWeaponValues(CyclopsValues, Cyclops, CyclopsClip);
            setDefsFromWeaponValues(RavenValues, Raven, RavenClip);
            setDefsFromWeaponValues(IronFuryValues, IronFury, IronFuryClip);
            setDefsFromWeaponValues(DeceptorValues, Deceptor, DeceptorClip);
            setDefsFromWeaponValues(ArchangelValues, Archangel, ArchangelClip);
            setDefsFromWeaponValues(FuryValues, Fury, FuryClip);
            setDefsFromWeaponValues(ThorValues, Thor, ThorClip);
            setDefsFromWeaponValues(DanteValues, Dante, DanteClip);
            setDefsFromWeaponValues(DefenderValues, Defender, DefenderClip);
            setDefsFromWeaponValues(EnforcerValues, Enforcer, EnforcerClip);
            setDefsFromWeaponValues(WatcherValues, Watcher, WatcherClip);
            setDefsFromWeaponValues(RattlesnakeValues, Rattlesnake, RattlesnakeClip);
            setDefsFromWeaponValues(ArmsValues, Arms, ArmsClip);
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

        private WeaponValues getWeaponValuesFromDefs(WeaponDef weaponDef, ItemDef itemDef)
        {
            float[] Damage = new float[weaponDef.DamagePayload.DamageKeywords.Count];
            for (int i = 0; i < weaponDef.DamagePayload.DamageKeywords.Count; i++)
            {
                Damage[i] = weaponDef.DamagePayload.DamageKeywords[i].Value;
            }
            return new WeaponValues(
                Damage,
                weaponDef.ChargesMax,
                itemDef.ChargesMax,
                weaponDef.DamagePayload.AutoFireShotCount,
                weaponDef.DamagePayload.ProjectilesPerShot,
                weaponDef.EffectiveRange, !float.IsInfinity(weaponDef.MaximumRange),
                weaponDef.DamagePayload.AoeRadius,
                weaponDef.DamagePayload.ConeRadius,
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
            if (weaponValues.UseMaximumRange) weaponDef.DamagePayload.Range = weaponValues.EffectiveRange;
            else weaponDef.SpreadDegrees = CalculateSpreadDegrees(weaponValues.EffectiveRange);
            weaponDef.HandsToUse = weaponValues.HandsToUse;
            weaponDef.DamagePayload.AoeRadius = weaponValues.AoeRadius;
            weaponDef.DamagePayload.ConeRadius = weaponValues.ConeRadius;
            weaponDef.ChargesMax = weaponValues.AmmoCapacity;
            if (itemDef != null) itemDef.ChargesMax = weaponValues.ClipCharges;
            weaponDef.Weight = weaponValues.Weight;
        }

        private void printDefs(WeaponDef weaponDef, ItemDef itemDef)
        {
            Logger.LogInfo("=== " + weaponDef.name + " ===");
            Logger.LogInfo("Damage:");
            for (int i = 0; i < weaponDef.DamagePayload.DamageKeywords.Count; i++)
            {
                DamageKeywordPair damageKeywordPair = weaponDef.DamagePayload.DamageKeywords[i];
                Logger.LogInfo(" - " + i + " " + damageKeywordPair.ToString());
            }
            Logger.LogInfo("AmmoCapacity: " + weaponDef.ChargesMax);
            Logger.LogInfo("ClipCharges: " + itemDef.ChargesMax);
            Logger.LogInfo("Burst: " + weaponDef.DamagePayload.AutoFireShotCount);
            Logger.LogInfo("ProjectilesPerShot: " + weaponDef.DamagePayload.ProjectilesPerShot);
            Logger.LogInfo("EffectiveRange: " + weaponDef.EffectiveRange);
            Logger.LogInfo("AoeRadius: " + weaponDef.DamagePayload.AoeRadius);
            Logger.LogInfo("ConeRadius: " + weaponDef.DamagePayload.ConeRadius);
            Logger.LogInfo("ApCost: " + CalculateApCost(weaponDef.APToUsePerc));
            Logger.LogInfo("HandsToUse: " + weaponDef.HandsToUse);
            Logger.LogInfo("Weight: " + weaponDef.Weight);
            Logger.LogInfo("StopOnFirstHit: " + weaponDef.DamagePayload.StopOnFirstHit);
        }
    }
}
