using PhoenixPoint.Modding;

namespace AnuWeaponStats
{
    /// <summary>
    /// ModConfig is mod settings that players can change from within the game.
    /// Config is only editable from players in main menu.
    /// Only one config can exist per mod assembly.
    /// Config is serialized on disk as json.
    /// </summary>
    public class AnuWeaponStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.
        
        /// Iconoclast
        public float IconoclastDamage = 35f;
        public int IconoclastAmmoCapacity = 50;
        public int IconoclastBurst = 1;
        public int IconoclastProjectilesPerShot = 10;
        public int IconoclastEffectiveRange = 8;
        public int IconoclastApCost = 2;
        public int IconoclastHandsToUse = 2;
        public int IconoclastWeight = 3;
        public bool IconoclastStopOnFirstHit = false;

        /// Harrower
        public float HarrowerDamage = 35f;
        public float HarrowerShred = 8f;
        public int HarrowerAmmoCapacity = 40;
        public int HarrowerBurst = 1;
        public int HarrowerProjectilesPerShot = 8;
        public int HarrowerEffectiveRange = 10;
        public int HarrowerApCost = 2;
        public int HarrowerHandsToUse = 2;
        public int HarrowerWeight = 4;
        public bool HarrowerStopOnFirstHit = false;

        /// Nergal's Wrath
        public float NergalsWrathDamage = 100f;
        public float NergalsWrathShred = 10f;
        public int NergalsWrathAmmoCapacity = 10;
        public int NergalsWrathBurst = 1;
        public int NergalsWrathProjectilesPerShot = 1;
        public int NergalsWrathEffectiveRange = 14;
        public int NergalsWrathApCost = 2;
        public int NergalsWrathHandsToUse = 1;
        public int NergalsWrathWeight = 2;
        public bool NergalsWrathStopOnFirstHit = false;

        /// Sanctifier
        public float SanctifierDamage = 10f;
        public float SanctifierAcid = 10f;
        public int SanctifierAmmoCapacity = 30;
        public int SanctifierBurst = 1;
        public int SanctifierProjectilesPerShot = 5;
        public int SanctifierEffectiveRange = 17;
        public int SanctifierApCost = 1;
        public int SanctifierHandsToUse = 1;
        public int SanctifierWeight = 2;
        public bool SanctifierStopOnFirstHit = false;

        /// Redeemer
        public float RedeemerDamage = 20f;
        public float RedeemerPierce = 10f;
        public float RedeemerVirus = 5f;
        public int RedeemerAmmoCapacity = 18;
        public int RedeemerBurst = 3;
        public int RedeemerProjectilesPerShot = 1;
        public int RedeemerEffectiveRange = 17;
        public int RedeemerApCost = 2;
        public int RedeemerHandsToUse = 2;
        public int RedeemerWeight = 2;
        public bool RedeemerStopOnFirstHit = false;

        /// Subjugator
        public float SubjugatorDamage = 50f;
        public float SubjugatorPierce = 20f;
        public float SubjugatorVirus = 10f;
        public int SubjugatorAmmoCapacity = 10;
        public int SubjugatorBurst = 1;
        public int SubjugatorProjectilesPerShot = 1;
        public int SubjugatorEffectiveRange = 51;
        public int SubjugatorApCost = 3;
        public int SubjugatorHandsToUse = 2;
        public int SubjugatorWeight = 4;
        public bool SubjugatorStopOnFirstHit = false;

        /// Marduk's Fist
        public float MarduksFistDamage = 160f;
        public float MarduksFistShock = 200f;
        public int MarduksFistBurst = 1;
        public int MarduksFistApCost = 2;
        public int MarduksFistHandsToUse = 1;
        public int MarduksFistWeight = 2;

        /// Dagon's Tooth
        public float DagonsToothDamage = 140f;
        public float DagonsToothBleed = 50f;
        public int DagonsToothBurst = 1;
        public int DagonsToothApCost = 2;
        public int DagonsToothHandsToUse = 1;
        public int DagonsToothWeight = 2;

        /// Scion of Sharur
        public float ScionOfSharurDamage = 100f;
        public float ScionOfSharurPierce = 50f;
        public float ScionOfSharurVirus = 15f;
        public int ScionOfSharurBurst = 1;
        public int ScionOfSharurApCost = 2;
        public int ScionOfSharurHandsToUse = 1;
        public int ScionOfSharurWeight = 2;
    }
}
