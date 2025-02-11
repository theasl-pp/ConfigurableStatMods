using PhoenixPoint.Modding;
using System;

namespace IndependentWeaponStats
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class IndependentWeaponStatsConfig : ModConfig
	{
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

		/// Assault Rifle
        public float YatArDamage = 30f;
		public float YatArShred = 1f;
        public int YatArAmmoCapacity = 30;
        public int YatArBurst = 6;
		public int YatArProjectilesPerShot = 1;
		public int YatArEffectiveRange = 17;
        public int YatArApCost = 2;
        public int YatArHandsToUse = 2;
        public int YatArWeight = 3;
        public bool YatArStopOnFirstHit = false;

        /// Sniper Rifle
        public float VyaraSrDamage = 100f;
        public int VyaraSrAmmoCapacity = 6;
        public int VyaraSrBurst = 1;
        public int VyaraSrProjectilesPerShot = 1;
        public int VyaraSrEffectiveRange = 45;
        public int VyaraSrApCost = 3;
        public int VyaraSrHandsToUse = 2;
        public int VyaraSrWeight = 4;
        public bool VyaraSrStopOnFirstHit = false;

        /// Pistol
        public float UdarHgDamage = 50f;
        public int UdarHgAmmoCapacity = 8;
        public int UdarHgBurst = 1;
        public int UdarHgProjectilesPerShot = 1;
        public int UdarHgEffectiveRange = 17;
        public int UdarHgApCost = 1;
        public int UdarHgHandsToUse = 1;
        public int UdarHgWeight = 2;
        public bool UdarHgStopOnFirstHit = false;

        /// Machine Gun
        public float UraganMgDamage = 35f;
        public float UraganMgShred = 2f;
        public int UraganMgAmmoCapacity = 40;
        public int UraganMgBurst = 10;
        public int UraganMgProjectilesPerShot = 1;
        public int UraganMgEffectiveRange = 11;
        public int UraganMgApCost = 3;
        public int UraganMgHandsToUse = 2;
        public int UraganMgWeight = 5;
        public bool UraganMgStopOnFirstHit = false;
    }
}
