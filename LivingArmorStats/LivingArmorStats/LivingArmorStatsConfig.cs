using PhoenixPoint.Modding;

namespace LivingArmorStats
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class LivingArmorStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Samnu Helmet
        public float SamnuHelmetArmor = 35f;
        public float SamnuHelmetSpeed = 0f;
        public float SamnuHelmetPerception = 0f;
        [ConfigField(text: "Samnu Helmet Stealth (in %)")]
        public float SamnuHelmetStealth = 5f;
        [ConfigField(text: "Samnu Helmet Accuracy (in %)")]
        public float SamnuHelmetAccuracy = 5f;
        public int SamnuHelmetWeight = 1;

        /// Samnu Body
        public float SamnuBodyArmor = 40f;
        public float SamnuBodySpeed = 0f;
        public float SamnuBodyPerception = 0f;
        [ConfigField(text: "Samnu Body Stealth (in %)")]
        public float SamnuBodyStealth = 10f;
        [ConfigField(text: "Samnu Body Accuracy (in %)")]
        public float SamnuBodyAccuracy = 5f;
        public int SamnuBodyWeight = 3;

        /// Samnu Leg
        public float SamnuLegArmor = 35f;
        public float SamnuLegSpeed = 0f;
        public float SamnuLegPerception = 0f;
        [ConfigField(text: "Samnu Leg Stealth (in %)")]
        public float SamnuLegStealth = 10f;
        [ConfigField(text: "Samnu Leg Accuracy (in %)")]
        public float SamnuLegAccuracy = 5f;
        public int SamnuLegWeight = 2;
    }
}
