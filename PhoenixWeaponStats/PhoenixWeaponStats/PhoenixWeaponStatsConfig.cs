using PhoenixPoint.Modding;

namespace PhoenixWeaponStats
{
    /// <summary>
    /// ModConfig is mod settings that players can change from within the game.
    /// Config is only editable from players in main menu.
    /// Only one config can exist per mod assembly.
    /// Config is serialized on disk as json.
    /// </summary>
    public class PhoenixWeaponStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Ares AR-1
        public float AresArDamage = 30f;
        public float AresArShred = 1f;
        public int AresArAmmoCapacity = 36;
        public int AresArBurst = 6;
        public int AresArProjectilesPerShot = 1;
        public int AresArEffectiveRange = 25;
        public int AresArApCost = 2;
        public int AresArHandsToUse = 2;
        public int AresArWeight = 3;
        public bool AresArStopOnFirstHit = false;

        /// Mercy SG3
        public float MercySgDamage = 35f;
        public int MercySgAmmoCapacity = 48;
        public int MercySgBurst = 1;
        public int MercySgProjectilesPerShot = 8;
        public int MercySgEffectiveRange = 10;
        public int MercySgApCost = 2;
        public int MercySgHandsToUse = 2;
        public int MercySgWeight = 3;
        public bool MercySgStopOnFirstHit = false;

        /// Slamstrike
        public float SlamstrikeDamage = 120f;
        public float SlamstrikeShred = 5f;
        public float SlamstrikeShock = 140f;
        public int SlamstrikeAmmoCapacity = 4;
        public int SlamstrikeBurst = 1;
        public int SlamstrikeProjectilesPerShot = 1;
        public int SlamstrikeEffectiveRange = 19;
        public int SlamstrikeApCost = 2;
        public int SlamstrikeHandsToUse = 2;
        public int SlamstrikeWeight = 4;
        public bool SlamstrikeStopOnFirstHit = true;

        /// Vidar GL
        public float VidarGlBlast = 50f;
        public float VidarGlShred = 10f;
        public int VidarGlAmmoCapacity = 1;
        public int VidarGlBurst = 1;
        public int VidarGlProjectilesPerShot = 1;
        public int VidarGlEffectiveRange = 20;
        public float VidarGlAoeRadius = 2.5f;
        public int VidarGlApCost = 1;
        public int VidarGlHandsToUse = 2;
        public int VidarGlWeight = 3;
        public bool VidarGlStopOnFirstHit = false;

        /// Firebird SR
        public float FirebirdSrDamage = 110f;
        public int FirebirdSrAmmoCapacity = 8;
        public int FirebirdSrBurst = 1;
        public int FirebirdSrProjectilesPerShot = 1;
        public int FirebirdSrEffectiveRange = 51;
        public int FirebirdSrApCost = 3;
        public int FirebirdSrHandsToUse = 2;
        public int FirebirdSrWeight = 4;
        public bool FirebirdSrStopOnFirstHit = false;

        /// Hawk-41K
        public float HawkLsrDamage = 90f;
        public int HawkLsrAmmoCapacity = 7;
        public int HawkLsrBurst = 1;
        public int HawkLsrProjectilesPerShot = 1;
        public int HawkLsrEffectiveRange = 51;
        public int HawkLsrApCost = 2;
        public int HawkLsrHandsToUse = 2;
        public int HawkLsrWeight = 4;
        public bool HawkLsrStopOnFirstHit = false;

        /// Gungnir SR-2
        public float GungnirSrDamage = 110f;
        public float GungnirSrVirophage = 80f;
        public int GungnirSrAmmoCapacity = 10;
        public int GungnirSrBurst = 1;
        public int GungnirSrProjectilesPerShot = 1;
        public int GungnirSrEffectiveRange = 45;
        public int GungnirSrApCost = 3;
        public int GungnirSrHandsToUse = 2;
        public int GungnirSrWeight = 4;
        public bool GungnirSrStopOnFirstHit = false;

        /// Cypher Hg
        public float CypherHgDamage = 50f;
        public int CypherHgAmmoCapacity = 8;
        public int CypherHgBurst = 1;
        public int CypherHgProjectilesPerShot = 1;
        public int CypherHgEffectiveRange = 18;
        public int CypherHgApCost = 1;
        public int CypherHgHandsToUse = 1;
        public int CypherHgWeight = 2;
        public bool CypherHgStopOnFirstHit = false;

        /// Tyr-1 Autocannon
        public float TyrAcDamage = 60f;
        public float TyrAcShred = 5f;
        public float TyrAcShock = 100f;
        public int TyrAcAmmoCapacity = 12;
        public int TyrAcBurst = 3;
        public int TyrAcProjectilesPerShot = 1;
        public int TyrAcEffectiveRange = 20;
        public int TyrAcApCost = 3;
        public int TyrAcHandsToUse = 2;
        public int TyrAcWeight = 4;
        public bool TyrAcStopOnFirstHit = false;

        /// Hel II Cannon
        public float HelIiDamage = 200f;
        public float HelIiShred = 20f;
        public float HelIiShock = 200f;
        public int HelIiAmmoCapacity = 6;
        public int HelIiBurst = 1;
        public int HelIiProjectilesPerShot = 1;
        public int HelIiEffectiveRange = 17;
        public int HelIiApCost = 3;
        public int HelIiHandsToUse = 2;
        public int HelIiWeight = 5;
        public bool HelIiStopOnFirstHit = false;

        /// Jormungandr Cannon
        public float JormungandrDamage = 10f;
        public float JormungandrAcid = 40f;
        public int JormungandrAmmoCapacity = 50;
        public int JormungandrBurst = 1;
        public int JormungandrProjectilesPerShot = 10;
        public int JormungandrEffectiveRange = 17;
        public float JormungandrAoeRadius = 3.5f;
        public int JormungandrApCost = 3;
        public int JormungandrHandsToUse = 2;
        public int JormungandrWeight = 5;
        public bool JormungandrStopOnFirstHit = true;

        /// Goliath GL-2
        public float GoliathGlBlast = 60f;
        public float GoliathGlShred = 10f;
        public int GoliathGlAmmoCapacity = 8;
        public int GoliathGlBurst = 1;
        public int GoliathGlProjectilesPerShot = 1;
        public int GoliathGlEffectiveRange = 25;
        public float GoliathGlAoeRadius = 3.5f;
        public int GoliathGlApCost = 3;
        public int GoliathGlHandsToUse = 2;
        public int GoliathGlWeight = 5;
        public bool GoliathGlStopOnFirstHit = true;

        /// Destiny III
        public float DestinyIiiDamage = 70f;
        public int DestinyIiiAmmoCapacity = 12;
        public int DestinyIiiBurst = 3;
        public int DestinyIiiProjectilesPerShot = 1;
        public int DestinyIiiApCost = 1;
        public int DestinyIiiHandsToUse = 0;
        public int DestinyIiiWeight = 2;
        public bool DestinyIiiStopOnFirstHit = false;

        /// Ragnarok
        public float RagnarokDamage = 80f;
        public float RagnarokShred = 30f;
        public int RagnarokAmmoCapacity = 2;
        public int RagnarokClipCharges = 1;
        public int RagnarokBurst = 1;
        public int RagnarokProjectilesPerShot = 1;
        public int RagnarokEffectiveRange = 25;
        public float RagnarokAoeRadius = 3.5f;
        public int RagnarokApCost = 1;
        public int RagnarokHandsToUse = 0;
        public int RagnarokWeight = 2;
        public bool RagnarokStopOnFirstHit = true;

        /// Gorgon Eye-A
        public float GorgonPdwDamage = 40f;
        public int GorgonPdwAmmoCapacity = 48;
        public int GorgonPdwBurst = 4;
        public int GorgonPdwProjectilesPerShot = 1;
        public int GorgonPdwEffectiveRange = 20;
        public int GorgonPdwApCost = 1;
        public int GorgonPdwHandsToUse = 2;
        public int GorgonPdwWeight = 2;
        public bool GorgonPdwStopOnFirstHit = false;

        /// Scorcher AT
        public float ScorcherAtDamage = 60f;
        public int ScorcherAtAmmoCapacity = 160;
        public int ScorcherAtBurst = 8;
        public int ScorcherAtProjectilesPerShot = 1;
        public int ScorcherAtEffectiveRange = 29;
        public int ScorcherAtApCost = 4;
        public int ScorcherAtWeight = 4;
        public bool ScorcherAtStopOnFirstHit = false;

        /// Shock Lance
        public float ShockLanceDamage = 160f;
        public float ShockLanceShock = 600f;
        public int ShockLanceBurst = 1;
        public int ShockLanceApCost = 2;
        public int ShockLanceHandsToUse = 1;
        public int ShockLanceWeight = 2;

        /// Neurazer
        public float NeurazerDamage = 10f;
        public float NeurazerPierce = 40f;
        public float NeurazerParalysis = 8f;
        public int NeurazerBurst = 1;
        public int NeurazerApCost = 1;
        public int NeurazerHandsToUse = 1;
        public int NeurazerWeight = 2;
    }
}
