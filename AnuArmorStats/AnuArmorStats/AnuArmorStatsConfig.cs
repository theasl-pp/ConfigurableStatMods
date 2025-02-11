using PhoenixPoint.Modding;

namespace AnuArmorStats
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class AnuArmorStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Acolyte Helmet
        public float AcolyteHelmetArmor = 18f;
        public float AcolyteHelmetSpeed = 0f;
        public float AcolyteHelmetPerception = 4f;
        [ConfigField(text: "Acolyte Helmet Stealth (in %)")]
        public float AcolyteHelmetStealth = 0f;
        [ConfigField(text: "Acolyte Helmet Accuracy (in %)")]
        public float AcolyteHelmetAccuracy = 0f;
        public int AcolyteHelmetWeight = 1;

        /// Acolyte Body
        public float AcolyteBodyArmor = 20f;
        public float AcolyteBodySpeed = 1f;
        public float AcolyteBodyPerception = 0f;
        [ConfigField(text: "Acolyte Body Stealth (in %)")]
        public float AcolyteBodyStealth = 0f;
        [ConfigField(text: "Acolyte Body Accuracy (in %)")]
        public float AcolyteBodyAccuracy = 0f;
        public int AcolyteBodyWeight = 2;

        /// Acolyte Leg
        public float AcolyteLegArmor = 18f;
        public float AcolyteLegSpeed = 2f;
        public float AcolyteLegPerception = 0f;
        [ConfigField(text: "Acolyte Leg Stealth (in %)")]
        public float AcolyteLegStealth = 0f;
        [ConfigField(text: "Acolyte Leg Accuracy (in %)")]
        public float AcolyteLegAccuracy = 0f;
        public int AcolyteLegWeight = 2;

        /// Aksu Helmet
        public float AksuHelmetArmor = 10f;
        public float AksuHelmetSpeed = 0f;
        public float AksuHelmetPerception = 10f;
        [ConfigField(text: "Aksu Helmet Stealth (in %)")]
        public float AksuHelmetStealth = 0f;
        [ConfigField(text: "Aksu Helmet Accuracy (in %)")]
        public float AksuHelmetAccuracy = 0f;
        public int AksuHelmetWeight = 0;

        /// Aksu Body
        public float AksuBodyArmor = 14f;
        public float AksuBodySpeed = 2f;
        public float AksuBodyPerception = 0f;
        [ConfigField(text: "Aksu Body Stealth (in %)")]
        public float AksuBodyStealth = 0f;
        [ConfigField(text: "Aksu Body Accuracy (in %)")]
        public float AksuBodyAccuracy = -5f;
        public int AksuBodyWeight = 1;

        /// Aksu Leg
        public float AksuLegArmor = 10f;
        public float AksuLegSpeed = 2f;
        public float AksuLegPerception = 0f;
        [ConfigField(text: "Aksu Leg Stealth (in %)")]
        public float AksuLegStealth = 0f;
        [ConfigField(text: "Aksu Leg Accuracy (in %)")]
        public float AksuLegAccuracy = -5f;
        public int AksuLegWeight = 1;

        /// Amphion Body
        public float AmphionBodyArmor = 16f;
        public float AmphionBodySpeed = 0f;
        public float AmphionBodyPerception = 7f;
        [ConfigField(text: "Amphion Body Stealth (in %)")]
        public float AmphionBodyStealth = 0f;
        [ConfigField(text: "Amphion Body Accuracy (in %)")]
        public float AmphionBodyAccuracy = 5f;
        public int AmphionBodyWeight = 2;

        /// Amphion Leg
        public float AmphionLegArmor = 14f;
        public float AmphionLegSpeed = 0f;
        public float AmphionLegPerception = 4f;
        [ConfigField(text: "Amphion Leg Stealth (in %)")]
        public float AmphionLegStealth = 0f;
        [ConfigField(text: "Amphion Leg Accuracy (in %)")]
        public float AmphionLegAccuracy = 5f;
        public int AmphionLegWeight = 2;
    }
}
