using PhoenixPoint.Modding;

namespace LivingWeaponStats
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class LivingWeaponStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Assault Rifle
        public float DanchevArDamage = 30f;
        public float DanchevArAcid = 5f;
        public int DanchevArBurst = 6;
        public int DanchevArProjectilesPerShot = 1;
        public int DanchevArEffectiveRange = 27;
        public int DanchevArApCost = 2;
        public int DanchevArHandsToUse = 2;
        public int DanchevArWeight = 3;
        public bool DanchevArStopOnFirstHit = true;

        /// Machine Gun
        public float DanchevMgDamage = 35f;
        public float DanchevMgShred = 3f;
        public float DanchevMgPoison = 5f;
        public int DanchevMgBurst = 10;
        public int DanchevMgProjectilesPerShot = 1;
        public int DanchevMgEffectiveRange = 17;
        public int DanchevMgApCost = 3;
        public int DanchevMgHandsToUse = 2;
        public int DanchevMgWeight = 5;
        public bool DanchevMgStopOnFirstHit = true;
    }
}
