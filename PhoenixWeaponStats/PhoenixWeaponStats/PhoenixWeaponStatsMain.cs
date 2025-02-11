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

namespace PhoenixWeaponStats
{
    /// <summary>
    /// This is the main mod class. Only one can exist per assembly.
    /// If no ModMain is detected in assembly, then no other classes/callbacks will be called.
    /// </summary>
    public class PhoenixWeaponStatsMain : ModMain
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
            public int ApCost;
            public int HandsToUse;
            public int Weight;
            public bool StopOnFirstHit;

            public WeaponValues(float[] damage, int ammoCapacity, int clipCharges,
                int burst, int projectilesPerShot, int effectiveRange, bool useMaximumRange,
                float aoeRadius, int apCost, int handsToUse, int weight, bool stopOnFirstHit)
            {
                Damage = damage;
                AmmoCapacity = ammoCapacity;
                ClipCharges = clipCharges;
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
        public new PhoenixWeaponStatsConfig Config => (PhoenixWeaponStatsConfig)base.Config;

        /// This property indicates if mod can be Safely Disabled from the game.
        /// Safely sisabled mods can be reenabled again. Unsafely disabled mods will need game restart ot take effect.
        /// Unsafely disabled mods usually cannot revert thier changes in OnModDisabled
        public override bool CanSafelyDisable => true;

        private WeaponDef Ares, Mercy, Slamstrike, Vidar, Firebird, Hawk, Gungnir, Cypher,
            Tyr, Hel, Jormungandr, Goliath, Destiny, Ragnarok, Gorgon, Scorcher, ShockLance, Neurazer;
        private ItemDef AresClip, MercyClip, SlamstrikeClip, VidarClip, FirebirdClip, HawkClip, GungnirClip, CypherClip,
            TyrClip, HelClip, JormungandrClip, GoliathClip, DestinyClip, RagnarokClip, GorgonClip, ScorcherClip;
        private WeaponValues DefaultAres, DefaultMercy, DefaultSlamstrike, DefaultVidar,
            DefaultFirebird, DefaultHawk, DefaultGungnir, DefaultCypher,
            DefaultTyr, DefaultHel, DefaultJormungandr, DefaultGoliath, DefaultDestiny, DefaultRagnarok,
            DefaultGorgon, DefaultScorcher, DefaultShockLance, DefaultNeurazer;

        /// <summary>
        /// Callback for when mod is enabled. Called even on game starup.
        /// </summary>
        public override void OnModEnabled()
        {
            try
            {
                DefRepository Repo = GameUtl.GameComponent<DefRepository>();

                Ares = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_AssaultRifle_WeaponDef"));
                Mercy = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_ShotgunRifle_WeaponDef"));
                Slamstrike = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("FS_SlamstrikeShotgun_WeaponDef"));
                Vidar = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("FS_AssaultGrenadeLauncher_WeaponDef"));
                Firebird = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_SniperRifle_WeaponDef"));
                Hawk = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("FS_LightSniperRifle_WeaponDef"));
                Gungnir = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_VirophageSniperRifle_WeaponDef"));
                Cypher = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_Pistol_WeaponDef"));
                Tyr = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("FS_Autocannon_WeaponDef"));
                Hel = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_HeavyCannon_WeaponDef"));
                Jormungandr = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_AcidCannon_WeaponDef"));
                Goliath = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_GrenadeLauncher_WeaponDef"));
                Destiny = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_LaserArrayPack_WeaponDef"));
                Ragnarok = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_ShredingMissileLauncherPack_WeaponDef"));
                Gorgon = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_LaserPDW_WeaponDef"));
                Scorcher = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_LaserTechTurretGun_WeaponDef"));
                ShockLance = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_StunRod_WeaponDef"));
                Neurazer = Repo.GetAllDefs<WeaponDef>().FirstOrDefault(a => a.name.Equals("PX_Neurazer_WeaponDef"));

                AresClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_AssaultRifle_AmmoClip_ItemDef"));
                MercyClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_ShotgunRifle_AmmoClip_ItemDef"));
                SlamstrikeClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("FS_SlamstrikeShotgun_AmmoClip_ItemDef"));
                VidarClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("FS_AssaultGrenadeLauncher_AmmoClip_ItemDef"));
                FirebirdClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_SniperRifle_AmmoClip_ItemDef"));
                HawkClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("FS_LightSniperRifle_AmmoClip_ItemDef"));
                GungnirClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_VirophageSniperRifle_AmmoClip_ItemDef"));
                CypherClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_Pistol_AmmoClip_ItemDef"));
                TyrClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("FS_Autocannon_AmmoClip_ItemDef"));
                HelClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_HeavyCannon_AmmoClip_ItemDef"));
                JormungandrClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_AcidCannon_AmmoClip_ItemDef"));
                GoliathClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_GrenadeLauncher_AmmoClip_ItemDef"));
                DestinyClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_LaserArray_AmmoClip_ItemDef"));
                RagnarokClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_ShredingMissileLauncher_AmmoClip_ItemDef"));
                GorgonClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_LaserPDW_AmmoClip_ItemDef"));
                ScorcherClip = Repo.GetAllDefs<ItemDef>().FirstOrDefault(a => a.name.Equals("PX_LaserTechTurretGun_AmmoClip_ItemDef"));

                //printDefs(Ares, AresClip);
                //printDefs(Mercy, MercyClip);
                //printDefs(Slamstrike, SlamstrikeClip);
                //printDefs(Vidar, VidarClip);
                //printDefs(Firebird, FirebirdClip);
                //printDefs(Hawk, HawkClip);
                //printDefs(Gungnir, GungnirClip);
                //printDefs(Cypher, CypherClip);
                //printDefs(Tyr, TyrClip);
                //printDefs(Hel, HelClip);
                //printDefs(Jormungandr, JormungandrClip);
                //printDefs(Goliath, GoliathClip);
                //printDefs(Destiny, DestinyClip);
                //printDefs(Ragnarok, RagnarokClip);
                //printDefs(Gorgon, GorgonClip);
                //printDefs(Scorcher, ScorcherClip);
                //printDefs(ShockLance, null);
                //printDefs(Neurazer, null);

                DefaultAres = getWeaponValuesFromDefs(Ares, AresClip);
                DefaultMercy = getWeaponValuesFromDefs(Mercy, MercyClip);
                DefaultSlamstrike = getWeaponValuesFromDefs(Slamstrike, SlamstrikeClip);
                DefaultVidar = getWeaponValuesFromDefs(Vidar, VidarClip);
                DefaultFirebird = getWeaponValuesFromDefs(Firebird, FirebirdClip);
                DefaultHawk = getWeaponValuesFromDefs(Hawk, HawkClip);
                DefaultGungnir = getWeaponValuesFromDefs(Gungnir, GungnirClip);
                DefaultCypher = getWeaponValuesFromDefs(Cypher, CypherClip);
                DefaultTyr = getWeaponValuesFromDefs(Tyr, TyrClip);
                DefaultHel = getWeaponValuesFromDefs(Hel, HelClip);
                DefaultJormungandr = getWeaponValuesFromDefs(Jormungandr, JormungandrClip);
                DefaultGoliath = getWeaponValuesFromDefs(Goliath, GoliathClip);
                DefaultDestiny = getWeaponValuesFromDefs(Destiny, DestinyClip);
                DefaultRagnarok = getWeaponValuesFromDefs(Ragnarok, RagnarokClip);
                DefaultGorgon = getWeaponValuesFromDefs(Gorgon, GorgonClip);
                DefaultScorcher = getWeaponValuesFromDefs(Scorcher, ScorcherClip);
                DefaultShockLance = getWeaponValuesFromDefs(ShockLance, null);
                DefaultNeurazer = getWeaponValuesFromDefs(Neurazer, null);

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
            setDefsFromWeaponValues(DefaultAres, Ares, AresClip);
            setDefsFromWeaponValues(DefaultMercy, Mercy, MercyClip);
            setDefsFromWeaponValues(DefaultSlamstrike, Slamstrike, SlamstrikeClip);
            setDefsFromWeaponValues(DefaultVidar, Vidar, VidarClip);
            setDefsFromWeaponValues(DefaultFirebird, Firebird, FirebirdClip);
            setDefsFromWeaponValues(DefaultHawk, Hawk, HawkClip);
            setDefsFromWeaponValues(DefaultGungnir, Gungnir, GungnirClip);
            setDefsFromWeaponValues(DefaultCypher, Cypher, CypherClip);
            setDefsFromWeaponValues(DefaultTyr, Tyr, TyrClip);
            setDefsFromWeaponValues(DefaultHel, Hel, HelClip);
            setDefsFromWeaponValues(DefaultJormungandr, Jormungandr, JormungandrClip);
            setDefsFromWeaponValues(DefaultGoliath, Goliath, GoliathClip);
            setDefsFromWeaponValues(DefaultDestiny, Destiny, DestinyClip);
            setDefsFromWeaponValues(DefaultRagnarok, Ragnarok, RagnarokClip);
            setDefsFromWeaponValues(DefaultGorgon, Gorgon, GorgonClip);
            setDefsFromWeaponValues(DefaultScorcher, Scorcher, ScorcherClip);
            setDefsFromWeaponValues(DefaultShockLance, ShockLance, null);
            setDefsFromWeaponValues(DefaultNeurazer, Neurazer, null);
        }

        /// <summary>
        /// Callback for when any property from mod's config is changed.
        /// </summary>
        public override void OnConfigChanged()
        {
            float[] AresDamage = { Config.AresArDamage, Config.AresArShred };
            WeaponValues AresValues = new WeaponValues(
                AresDamage,
                Config.AresArAmmoCapacity,
                Config.AresArAmmoCapacity,
                Config.AresArBurst,
                Config.AresArProjectilesPerShot,
                Config.AresArEffectiveRange, false,
                Ares.DamagePayload.AoeRadius,
                Config.AresArApCost,
                Config.AresArHandsToUse,
                Config.AresArWeight,
                Config.AresArStopOnFirstHit
            );
            float[] MercyDamage = { Config.MercySgDamage };
            WeaponValues MercyValues = new WeaponValues(
                MercyDamage,
                Config.MercySgAmmoCapacity,
                Config.MercySgAmmoCapacity,
                Config.MercySgBurst,
                Config.MercySgProjectilesPerShot,
                Config.MercySgEffectiveRange, false,
                Mercy.DamagePayload.AoeRadius,
                Config.MercySgApCost,
                Config.MercySgHandsToUse,
                Config.MercySgWeight,
                Config.MercySgStopOnFirstHit
            );
            float[] SlamstrikeDamage = { Config.SlamstrikeDamage, Config.SlamstrikeShred, Config.SlamstrikeShock };
            WeaponValues SlamstrikeValues = new WeaponValues(
                SlamstrikeDamage,
                Config.SlamstrikeAmmoCapacity,
                Config.SlamstrikeAmmoCapacity,
                Config.SlamstrikeBurst,
                Config.SlamstrikeProjectilesPerShot,
                Config.SlamstrikeEffectiveRange, false,
                Slamstrike.DamagePayload.AoeRadius,
                Config.SlamstrikeApCost,
                Config.SlamstrikeHandsToUse,
                Config.SlamstrikeWeight,
                Config.SlamstrikeStopOnFirstHit
            );
            float[] VidarDamage = { Config.VidarGlBlast, Config.VidarGlShred };
            WeaponValues VidarValues = new WeaponValues(
                VidarDamage,
                Config.VidarGlAmmoCapacity,
                Config.VidarGlAmmoCapacity,
                Config.VidarGlBurst,
                Config.VidarGlProjectilesPerShot,
                Config.VidarGlEffectiveRange, true,
                Config.VidarGlAoeRadius,
                Config.VidarGlApCost,
                Config.VidarGlHandsToUse,
                Config.VidarGlWeight,
                Config.VidarGlStopOnFirstHit
            );
            float[] FirebirdDamage = { Config.FirebirdSrDamage };
            WeaponValues FirebirdValues = new WeaponValues(
                FirebirdDamage,
                Config.FirebirdSrAmmoCapacity,
                Config.FirebirdSrAmmoCapacity,
                Config.FirebirdSrBurst,
                Config.FirebirdSrProjectilesPerShot,
                Config.FirebirdSrEffectiveRange, false,
                Firebird.DamagePayload.AoeRadius,
                Config.FirebirdSrApCost,
                Config.FirebirdSrHandsToUse,
                Config.FirebirdSrWeight,
                Config.FirebirdSrStopOnFirstHit
            );
            float[] HawkDamage = { Config.HawkLsrDamage };
            WeaponValues HawkValues = new WeaponValues(
                HawkDamage,
                Config.HawkLsrAmmoCapacity,
                Config.HawkLsrAmmoCapacity,
                Config.HawkLsrBurst,
                Config.HawkLsrProjectilesPerShot,
                Config.HawkLsrEffectiveRange, false,
                Hawk.DamagePayload.AoeRadius,
                Config.HawkLsrApCost,
                Config.HawkLsrHandsToUse,
                Config.HawkLsrWeight,
                Config.HawkLsrStopOnFirstHit
            );
            float[] GungnirDamage = { Config.GungnirSrDamage, Config.GungnirSrVirophage };
            WeaponValues GungnirValues = new WeaponValues(
                GungnirDamage,
                Config.GungnirSrAmmoCapacity,
                Config.GungnirSrAmmoCapacity,
                Config.GungnirSrBurst,
                Config.GungnirSrProjectilesPerShot,
                Config.GungnirSrEffectiveRange, false,
                Gungnir.DamagePayload.AoeRadius,
                Config.GungnirSrApCost,
                Config.GungnirSrHandsToUse,
                Config.GungnirSrWeight,
                Config.GungnirSrStopOnFirstHit
            );
            float[] CypherDamage = { Config.CypherHgDamage };
            WeaponValues CypherValues = new WeaponValues(
                CypherDamage,
                Config.CypherHgAmmoCapacity,
                Config.CypherHgAmmoCapacity,
                Config.CypherHgBurst,
                Config.CypherHgProjectilesPerShot,
                Config.CypherHgEffectiveRange, false,
                Cypher.DamagePayload.AoeRadius,
                Config.CypherHgApCost,
                Config.CypherHgHandsToUse,
                Config.CypherHgWeight,
                Config.CypherHgStopOnFirstHit
            );
            float[] TyrDamage = { Config.TyrAcDamage, Config.TyrAcShred, Config.TyrAcShock };
            WeaponValues TyrValues = new WeaponValues(
                TyrDamage,
                Config.TyrAcAmmoCapacity,
                Config.TyrAcAmmoCapacity,
                Config.TyrAcBurst,
                Config.TyrAcProjectilesPerShot,
                Config.TyrAcEffectiveRange, false,
                Tyr.DamagePayload.AoeRadius,
                Config.TyrAcApCost,
                Config.TyrAcHandsToUse,
                Config.TyrAcWeight,
                Config.TyrAcStopOnFirstHit
            );
            float[] HelDamage = { Config.HelIiDamage, Config.HelIiShred, Config.HelIiShock };
            WeaponValues HelValues = new WeaponValues(
                HelDamage,
                Config.HelIiAmmoCapacity,
                Config.HelIiAmmoCapacity,
                Config.HelIiBurst,
                Config.HelIiProjectilesPerShot,
                Config.HelIiEffectiveRange, false,
                Hel.DamagePayload.AoeRadius,
                Config.HelIiApCost,
                Config.HelIiHandsToUse,
                Config.HelIiWeight,
                Config.HelIiStopOnFirstHit
            );
            float[] JormungandrDamage = { Config.JormungandrDamage, Config.JormungandrAcid };
            WeaponValues JormungandrValues = new WeaponValues(
                JormungandrDamage,
                Config.JormungandrAmmoCapacity,
                Config.JormungandrAmmoCapacity,
                Config.JormungandrBurst,
                Config.JormungandrProjectilesPerShot,
                Config.JormungandrEffectiveRange, false,
                Config.JormungandrAoeRadius,
                Config.JormungandrApCost,
                Config.JormungandrHandsToUse,
                Config.JormungandrWeight,
                Config.JormungandrStopOnFirstHit
            );
            float[] GoliathDamage = { Config.GoliathGlBlast, Config.GoliathGlShred };
            WeaponValues GoliathValues = new WeaponValues(
                GoliathDamage,
                Config.GoliathGlAmmoCapacity,
                Config.GoliathGlAmmoCapacity,
                Config.GoliathGlBurst,
                Config.GoliathGlProjectilesPerShot,
                Config.GoliathGlEffectiveRange, true,
                Config.GoliathGlAoeRadius,
                Config.GoliathGlApCost,
                Config.GoliathGlHandsToUse,
                Config.GoliathGlWeight,
                Config.GoliathGlStopOnFirstHit
            );
            float[] DestinyDamage = { Config.DestinyIiiDamage };
            WeaponValues DestinyValues = new WeaponValues(
                DestinyDamage,
                Config.DestinyIiiAmmoCapacity,
                Config.DestinyIiiAmmoCapacity,
                Config.DestinyIiiBurst,
                Config.DestinyIiiProjectilesPerShot,
                Destiny.EffectiveRange, false,
                Destiny.DamagePayload.AoeRadius,
                Config.DestinyIiiApCost,
                Config.DestinyIiiHandsToUse,
                Config.DestinyIiiWeight,
                Config.DestinyIiiStopOnFirstHit
            );
            float[] RagnarokDamage = { Config.RagnarokDamage, Config.RagnarokShred };
            WeaponValues RagnarokValues = new WeaponValues(
                RagnarokDamage,
                Config.RagnarokAmmoCapacity,
                Config.RagnarokClipCharges,
                Config.RagnarokBurst,
                Config.RagnarokProjectilesPerShot,
                Config.RagnarokEffectiveRange, true,
                Config.RagnarokAoeRadius,
                Config.RagnarokApCost,
                Config.RagnarokHandsToUse,
                Config.RagnarokWeight,
                Config.RagnarokStopOnFirstHit
            );
            float[] GorgonDamage = { Config.GorgonPdwDamage };
            WeaponValues GorgonValues = new WeaponValues(
                GorgonDamage,
                Config.GorgonPdwAmmoCapacity,
                Config.GorgonPdwAmmoCapacity,
                Config.GorgonPdwBurst,
                Config.GorgonPdwProjectilesPerShot,
                Config.GorgonPdwEffectiveRange, false,
                Gorgon.DamagePayload.AoeRadius,
                Config.GorgonPdwApCost,
                Config.GorgonPdwHandsToUse,
                Config.GorgonPdwWeight,
                Config.GorgonPdwStopOnFirstHit
            );
            float[] ScorcherDamage = { Config.ScorcherAtDamage };
            WeaponValues ScorcherValues = new WeaponValues(
                ScorcherDamage,
                Config.ScorcherAtAmmoCapacity,
                Config.ScorcherAtAmmoCapacity,
                Config.ScorcherAtBurst,
                Config.ScorcherAtProjectilesPerShot,
                Config.ScorcherAtEffectiveRange, false,
                Scorcher.DamagePayload.AoeRadius,
                Config.ScorcherAtApCost,
                Scorcher.HandsToUse,
                Config.ScorcherAtWeight,
                Config.ScorcherAtStopOnFirstHit
            );
            float[] ShockLanceDamage = { Config.ShockLanceDamage, Config.ShockLanceShock };
            WeaponValues ShockLanceValues = new WeaponValues(
                ShockLanceDamage,
                ShockLance.ChargesMax,
                ShockLance.ChargesMax,
                Config.ShockLanceBurst,
                ShockLance.DamagePayload.ProjectilesPerShot,
                ShockLance.EffectiveRange, true,
                ShockLance.DamagePayload.AoeRadius,
                Config.ShockLanceApCost,
                Config.ShockLanceHandsToUse,
                Config.ShockLanceWeight,
                ShockLance.DamagePayload.StopOnFirstHit
            );
            float[] NeurazerDamage = { Config.NeurazerDamage, Config.NeurazerPierce, Config.NeurazerParalysis };
            WeaponValues NeurazerValues = new WeaponValues(
                NeurazerDamage,
                Neurazer.ChargesMax,
                Neurazer.ChargesMax,
                Config.NeurazerBurst,
                Neurazer.DamagePayload.ProjectilesPerShot,
                Neurazer.EffectiveRange, true,
                Neurazer.DamagePayload.AoeRadius,
                Config.NeurazerApCost,
                Config.NeurazerHandsToUse,
                Config.NeurazerWeight,
                Neurazer.DamagePayload.StopOnFirstHit
            );
            setDefsFromWeaponValues(AresValues, Ares, AresClip);
            setDefsFromWeaponValues(MercyValues, Mercy, MercyClip);
            setDefsFromWeaponValues(SlamstrikeValues, Slamstrike, SlamstrikeClip);
            setDefsFromWeaponValues(VidarValues, Vidar, VidarClip);
            setDefsFromWeaponValues(FirebirdValues, Firebird, FirebirdClip);
            setDefsFromWeaponValues(HawkValues, Hawk, HawkClip);
            setDefsFromWeaponValues(GungnirValues, Gungnir, GungnirClip);
            setDefsFromWeaponValues(CypherValues, Cypher, CypherClip);
            setDefsFromWeaponValues(TyrValues, Tyr, TyrClip);
            setDefsFromWeaponValues(HelValues, Hel, HelClip);
            setDefsFromWeaponValues(JormungandrValues, Jormungandr, JormungandrClip);
            setDefsFromWeaponValues(GoliathValues, Goliath, GoliathClip);
            setDefsFromWeaponValues(DestinyValues, Destiny, DestinyClip);
            setDefsFromWeaponValues(RagnarokValues, Ragnarok, RagnarokClip);
            setDefsFromWeaponValues(GorgonValues, Gorgon, GorgonClip);
            setDefsFromWeaponValues(ScorcherValues, Scorcher, ScorcherClip);
            setDefsFromWeaponValues(ShockLanceValues, ShockLance, null);
            setDefsFromWeaponValues(NeurazerValues, Neurazer, null);
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
                itemDef == null ? weaponDef.ChargesMax : itemDef.ChargesMax,
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
            Logger.LogInfo("ClipCharges: " + (itemDef == null ? weaponDef.ChargesMax : itemDef.ChargesMax));
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
