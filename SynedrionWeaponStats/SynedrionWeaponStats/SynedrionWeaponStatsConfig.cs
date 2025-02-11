using PhoenixPoint.Modding;

namespace SynedrionWeaponStats
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class SynedrionWeaponStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.
        
        /// Deimos AR
        public float DeimosArDamage = 30f;
        public float DeimosArShred = 1f;
        public int DeimosArAmmoCapacity = 60;
        public int DeimosArBurst = 6;
        public int DeimosArProjectilesPerShot = 1;
        public int DeimosArEffectiveRange = 32;
        public int DeimosArApCost = 2;
        public int DeimosArHandsToUse = 2;
        public int DeimosArWeight = 3;
        public bool DeimosArStopOnFirstHit = false;

        /// Pythagoras SR
        public float PythagorasSrDamage = 120f;
        public int PythagorasSrAmmoCapacity = 15;
        public int PythagorasSrBurst = 1;
        public int PythagorasSrProjectilesPerShot = 1;
        public int PythagorasSrEffectiveRange = 63;
        public int PythagorasSrApCost = 3;
        public int PythagorasSrHandsToUse = 2;
        public int PythagorasSrWeight = 4;
        public bool PythagorasSrStopOnFirstHit = false;

        /// Hephaestus HG
        public float HephaestusHgDamage = 50f;
        public int HephaestusHgAmmoCapacity = 22;
        public int HephaestusHgBurst = 1;
        public int HephaestusHgProjectilesPerShot = 1;
        public int HephaestusHgEffectiveRange = 27;
        public int HephaestusHgApCost = 1;
        public int HephaestusHgHandsToUse = 1;
        public int HephaestusHgWeight = 2;
        public bool HephaestusHgStopOnFirstHit = false;

        /// Athena NS
        public float AthenaNsDamage = 10f;
        public float AthenaNsPierce = 60f;
        public float AthenaNsParalysis = 16f;
        public int AthenaNsAmmoCapacity = 12;
        public int AthenaNsBurst = 1;
        public int AthenaNsProjectilesPerShot = 1;
        public int AthenaNsEffectiveRange = 45;
        public int AthenaNsApCost = 3;
        public int AthenaNsHandsToUse = 2;
        public int AthenaNsWeight = 4;
        public bool AthenaNsStopOnFirstHit = false;

        /// Hera NP
        public float HeraNpDamage = 10f;
        public float HeraNpPierce = 30f;
        public float HeraNpParalysis = 7f;
        public int HeraNpAmmoCapacity = 8;
        public int HeraNpBurst = 1;
        public int HeraNpProjectilesPerShot = 1;
        public int HeraNpEffectiveRange = 17;
        public int HeraNpApCost = 1;
        public int HeraNpHandsToUse = 1;
        public int HeraNpWeight = 2;
        public bool HeraNpStopOnFirstHit = false;

        /// Eros CRB
        public float ErosCrbDamage = 60f;
        public int ErosCrbAmmoCapacity = 3;
        public int ErosCrbBurst = 1;
        public int ErosCrbProjectilesPerShot = 1;
        public int ErosCrbEffectiveRange = 34;
        public int ErosCrbApCost = 1;
        public int ErosCrbHandsToUse = 2;
        public int ErosCrbWeight = 3;
        public bool ErosCrbStopOnFirstHit = false;

        /// Psyche CRB
        public float PsycheCrbDamage = 50f;
        public float PsycheCrbPoison = 40f;
        public int PsycheCrbAmmoCapacity = 3;
        public int PsycheCrbBurst = 1;
        public int PsycheCrbProjectilesPerShot = 1;
        public int PsycheCrbEffectiveRange = 34;
        public int PsycheCrbApCost = 1;
        public int PsycheCrbHandsToUse = 2;
        public int PsycheCrbWeight = 3;
        public bool PsycheCrbStopOnFirstHit = false;

        /// Arachni SP
        public float ArachniSpDamage = 5f;
        public int ArachniSpAmmoCapacity = 3;
        public int ArachniSpApCost = 2;
        public int ArachniSpHandsToUse = 1;
        public int ArachniSpWeight = 2;
    }
}
