using PhoenixPoint.Modding;

namespace AncientWeaponStats
{
    /// <summary>
    /// ModConfig is mod settings that players can change from within the game.
    /// Config is only editable from players in main menu.
    /// Only one config can exist per mod assembly.
    /// Config is serialized on disk as json.
    /// </summary>
    public class AncientWeaponStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// ShardGun
        public float ShardGunDamage = 25f;
        public float ShardGunPierce = 15f;
        public int ShardGunBurst = 1;
        public int ShardGunProjectilesPerShot = 15;
        public int ShardGunEffectiveRange = 13;
        public int ShardGunApCost = 2;
        public int ShardGunHandsToUse = 2;
        public int ShardGunWeight = 4;
        public bool ShardGunStopOnFirstHit = true;

        /// Scorpion
        public float ScorpionDamage = 180f;
        public float ScorpionPierce = 80f;
        public int ScorpionBurst = 1;
        public int ScorpionProjectilesPerShot = 1;
        public int ScorpionEffectiveRange = 58;
        public int ScorpionApCost = 3;
        public int ScorpionHandsToUse = 2;
        public int ScorpionWeight = 4;
        public bool ScorpionStopOnFirstHit = true;

        /// Rebuke
        public float RebukeBlast = 90f;
        public float RebukeShred = 30f;
        public int RebukeBurst = 1;
        public int RebukeProjectilesPerShot = 1;
        public int RebukeEffectiveRange = 25;
        public float RebukeAoeRadius = 3.5f;
        public int RebukeApCost = 3;
        public int RebukeHandsToUse = 2;
        public int RebukeWeight = 5;
        public bool RebukeStopOnFirstHit = true;

        /// CrystalCrb
        public float CrystalCrbDamage = 60f;
        public float CrystalCrbPierce = 40f;
        public int CrystalCrbBurst = 1;
        public int CrystalCrbProjectilesPerShot = 1;
        public int CrystalCrbEffectiveRange = 41;
        public int CrystalCrbApCost = 1;
        public int CrystalCrbHandsToUse = 2;
        public int CrystalCrbWeight = 3;
        public bool CrystalCrbStopOnFirstHit = true;

        /// Scyther
        public float ScytherDamage = 300f;
        public float ScytherShred = 30f;
        public int ScytherBurst = 1;
        public int ScytherApCost = 2;
        public int ScytherHandsToUse = 2;
        public int ScytherWeight = 5;

        /// Mattock of the Ancients
        public float MattockDamage = 220f;
        public float MattockShock = 400f;
        public int MattockBurst = 1;
        public int MattockApCost = 2;
        public int MattockHandsToUse = 1;
        public int MattockWeight = 5;
    }
}
