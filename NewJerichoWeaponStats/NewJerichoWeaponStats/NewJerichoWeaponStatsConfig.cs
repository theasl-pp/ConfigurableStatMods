using PhoenixPoint.Modding;

namespace NewJerichoWeaponStats
{
    /// <summary>
    /// ModConfig is mod settings that players can change from within the game.
    /// Config is only editable from players in main menu.
    /// Only one config can exist per mod assembly.
    /// Config is serialized on disk as json.
    /// </summary>
    public class NewJerichoWeaponStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Bulldog AR-50
        public float BulldogArDamage = 40f;
        public float BulldogArShred = 2f;
        public int BulldogArAmmoCapacity = 32;
        public int BulldogArBurst = 4;
        public int BulldogArProjectilesPerShot = 1;
        public int BulldogArEffectiveRange = 22;
        public int BulldogArApCost = 2;
        public int BulldogArHandsToUse = 2;
        public int BulldogArWeight = 3;
        public bool BulldogArStopOnFirstHit = false;

        /// Piranha AR-51
        public float PiranhaArDamage = 40f;
        public float PiranhaArPierce = 15f;
        public int PiranhaArAmmoCapacity = 32;
        public int PiranhaArBurst = 4;
        public int PiranhaArProjectilesPerShot = 1;
        public int PiranhaArEffectiveRange = 22;
        public int PiranhaArApCost = 2;
        public int PiranhaArHandsToUse = 2;
        public int PiranhaArWeight = 3;
        public bool PiranhaArStopOnFirstHit = false;

        /// Cyclops SR7
        public float CyclopsSrDamage = 130f;
        public int CyclopsSrAmmoCapacity = 10;
        public int CyclopsSrBurst = 1;
        public int CyclopsSrProjectilesPerShot = 1;
        public int CyclopsSrEffectiveRange = 45;
        public int CyclopsSrApCost = 3;
        public int CyclopsSrHandsToUse = 2;
        public int CyclopsSrWeight = 4;
        public bool CyclopsSrStopOnFirstHit = false;

        /// Raven SR13
        public float RavenSrDamage = 130f;
        public float RavenSrPierce = 50f;
        public int RavenSrAmmoCapacity = 10;
        public int RavenSrBurst = 1;
        public int RavenSrProjectilesPerShot = 1;
        public int RavenSrEffectiveRange = 45;
        public int RavenSrApCost = 3;
        public int RavenSrHandsToUse = 2;
        public int RavenSrWeight = 4;
        public bool RavenSrStopOnFirstHit = false;

        /// Iron Fury HG
        public float IronFuryHgDamage = 60f;
        public int IronFuryHgAmmoCapacity = 12;
        public int IronFuryHgBurst = 1;
        public int IronFuryHgProjectilesPerShot = 1;
        public int IronFuryHgEffectiveRange = 14;
        public int IronFuryHgApCost = 1;
        public int IronFuryHgHandsToUse = 1;
        public int IronFuryHgWeight = 2;
        public bool IronFuryHgStopOnFirstHit = false;

        /// Deceptor MG
        public float DeceptorMgDamage = 35f;
        public float DeceptorMgShred = 2f;
        public int DeceptorMgAmmoCapacity = 60;
        public int DeceptorMgBurst = 12;
        public int DeceptorMgProjectilesPerShot = 1;
        public int DeceptorMgEffectiveRange = 14;
        public int DeceptorMgApCost = 3;
        public int DeceptorMgHandsToUse = 2;
        public int DeceptorMgWeight = 5;
        public bool DeceptorMgStopOnFirstHit = false;

        /// Archangel RL1
        public float ArchangelRlBlast = 100f;
        public float ArchangelRlShred = 20f;
        public int ArchangelRlAmmoCapacity = 4;
        public int ArchangelRlClipCharges = 4;
        public int ArchangelRlBurst = 1;
        public int ArchangelRlProjectilesPerShot = 1;
        public float ArchangelRlAoeRadius = 3.5f;
        public int ArchangelRlApCost = 3;
        public int ArchangelRlHandsToUse = 2;
        public int ArchangelRlWeight = 5;
        public bool ArchangelRlStopOnFirstHit = true;

        /// Fury-2
        public float FuryBlast = 70f;
        public float FuryShred = 10f;
        public int FuryAmmoCapacity = 2;
        public int FuryClipCharges = 1;
        public int FuryBurst = 1;
        public int FuryProjectilesPerShot = 1;
        public int FuryEffectiveRange = 25;
        public float FuryAoeRadius = 3.5f;
        public int FuryApCost = 1;
        public int FuryHandsToUse = 0;
        public int FuryWeight = 2;
        public bool FuryStopOnFirstHit = true;

        /// Thor AML
        public float ThorAmlBlast = 100f;
        public float ThorAmlShred = 20f;
        public int ThorAmlAmmoCapacity = 2;
        public int ThorAmlClipCharges = 1;
        public int ThorAmlBurst = 1;
        public int ThorAmlProjectilesPerShot = 1;
        public int ThorAmlEffectiveRange = 35;
        public float ThorAmlAoeRadius = 3.5f;
        public int ThorAmlApCost = 1;
        public int ThorAmlHandsToUse = 0;
        public int ThorAmlWeight = 2;
        public bool ThorAmlStopOnFirstHit = true;

        /// Dante FT
        public float DanteFtDamage = 80f;
        public float DanteFtFire = 40f;
        public int DanteFtAmmoCapacity = 4;
        public int DanteFtBurst = 1;
        public int DanteFtProjectilesPerShot = 1;
        public int DanteFtEffectiveRange = 12;
        public float DanteFtConeRadius = 1.5f;
        public int DanteFtApCost = 2;
        public int DanteFtHandsToUse = 2;
        public int DanteFtWeight = 5;
        public bool DanteFtStopOnFirstHit = true;

        /// VDM Defender
        public float VdmDefenderDamage = 30f;
        public float VdmDefenderShred = 1f;
        public int VdmDefenderAmmoCapacity = 40;
        public int VdmDefenderBurst = 4;
        public int VdmDefenderProjectilesPerShot = 1;
        public int VdmDefenderEffectiveRange = 16;
        public int VdmDefenderApCost = 1;
        public int VdmDefenderHandsToUse = 2;
        public int VdmDefenderWeight = 2;
        public bool VdmDefenderStopOnFirstHit = false;

        /// VDM Enforcer
        public float VdmEnforcerDamage = 20f;
        public float VdmEnforcerPierce = 20f;
        public int VdmEnforcerAmmoCapacity = 40;
        public int VdmEnforcerBurst = 4;
        public int VdmEnforcerProjectilesPerShot = 1;
        public int VdmEnforcerEffectiveRange = 19;
        public int VdmEnforcerApCost = 1;
        public int VdmEnforcerHandsToUse = 2;
        public int VdmEnforcerWeight = 2;
        public bool VdmEnforcerStopOnFirstHit = false;

        /// Watcher AT
        public float WatcherAtDamage = 40f;
        public float WatcherAtShred = 3f;
        public int WatcherAtAmmoCapacity = 80;
        public int WatcherAtBurst = 8;
        public int WatcherAtProjectilesPerShot = 1;
        public int WatcherAtEffectiveRange = 22;
        public int WatcherAtApCost = 4;
        public int WatcherAtWeight = 4;
        public bool WatcherAtStopOnFirstHit = false;

        /// Rattlesnake AT
        public float RattlesnakeAtDamage = 40f;
        public float RattlesnakeAtPierce = 30f;
        public int RattlesnakeAtAmmoCapacity = 96;
        public int RattlesnakeAtBurst = 8;
        public int RattlesnakeAtProjectilesPerShot = 1;
        public int RattlesnakeAtEffectiveRange = 25;
        public int RattlesnakeAtApCost = 4;
        public int RattlesnakeAtWeight = 4;
        public bool RattlesnakeAtStopOnFirstHit = false;

        /// VVA-2 Arms
        public float VvaArmsShock = 180f;
        public int VvaArmsAmmoCapacity = 5;
        public int VvaArmsBurst = 1;
        public int VvaArmsApCost = 1;
        public int VvaArmsHandsToUse = 0;
        public int VvaArmsWeight = 3;
    }
}
