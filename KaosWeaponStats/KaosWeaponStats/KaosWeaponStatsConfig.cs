using PhoenixPoint.Modding;
using System;

namespace KaosWeaponStats
{
    /// <summary>
    /// ModConfig is mod settings that players can change from within the game.
    /// Config is only editable from players in main menu.
    /// Only one config can exist per mod assembly.
    /// Config is serialized on disk as json.
    /// </summary>
    public class KaosWeaponStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Obliterator
        public float ObliteratorDamage = 40f;
        public float ObliteratorShred = 1f;
        public int ObliteratorBurst = 8;
        public int ObliteratorProjectilesPerShot = 1;
        public int ObliteratorEffectiveRange = 25;
        public int ObliteratorApCost = 2;
        public int ObliteratorHandsToUse = 2;
        public int ObliteratorWeight = 3;
        public bool ObliteratorStopOnFirstHit = false;
        public int ObliteratorMalfunctionIncrease = 2;
        public int ObliteratorMalfunctionMaximum = 40;

        /// Redemptor
        public float RedemptorDamage = 50f;
        public float RedemptorAcid = 5f;
        public int RedemptorBurst = 1;
        public int RedemptorProjectilesPerShot = 8;
        public int RedemptorEffectiveRange = 19;
        public int RedemptorApCost = 2;
        public int RedemptorHandsToUse = 2;
        public int RedemptorWeight = 4;
        public bool RedemptorStopOnFirstHit = false;
        public int RedemptorMalfunctionIncrease = 2;
        public int RedemptorMalfunctionMaximum = 40;

        /// Subjector
        public float SubjectorDamage = 130f;
        public float SubjectorPoison = 30f;
        public int SubjectorBurst = 1;
        public int SubjectorProjectilesPerShot = 1;
        public int SubjectorEffectiveRange = 51;
        public int SubjectorApCost = 3;
        public int SubjectorHandsToUse = 2;
        public int SubjectorWeight = 4;
        public bool SubjectorStopOnFirstHit = false;
        public int SubjectorMalfunctionIncrease = 2;
        public int SubjectorMalfunctionMaximum = 40;

        /// Tormentor
        public float TormentorDamage = 50f;
        public float TormentorPierce = 10f;
        public int TormentorBurst = 2;
        public int TormentorProjectilesPerShot = 1;
        public int TormentorEffectiveRange = 22;
        public int TormentorApCost = 1;
        public int TormentorHandsToUse = 1;
        public int TormentorWeight = 2;
        public bool TormentorStopOnFirstHit = false;
        public int TormentorMalfunctionIncrease = 2;
        public int TormentorMalfunctionMaximum = 40;

        /// Devastator
        public float DevastatorDamage = 150f;
        public float DevastatorShock = 150f;
        public int DevastatorBurst = 2;
        public int DevastatorProjectilesPerShot = 1;
        public int DevastatorEffectiveRange = 17;
        public int DevastatorApCost = 3;
        public int DevastatorHandsToUse = 2;
        public int DevasatorWeight = 5;
        public bool DevastatorStopOnFirstHit = false;
        public int DevastatorMalfunctionIncrease = 2;
        public int DevastatorMalfunctionMaximum = 40;
    }
}
