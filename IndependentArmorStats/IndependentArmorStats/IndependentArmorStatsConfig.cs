using PhoenixPoint.Modding;

namespace IndependentArmorStats
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class IndependentArmorStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Praetorian2 Helmet
        [ConfigField(text: "Praetorian 2 Helmet Armor")]
        public float Praetorian2HelmetArmor = 18f;
        [ConfigField(text: "Praetorian 2 Helmet Speed")]
        public float Praetorian2HelmetSpeed = 0f;
        [ConfigField(text: "Praetorian 2 Helmet Perception")]
        public float Praetorian2HelmetPerception = 0f;
        [ConfigField(text: "Praetorian 2 Helmet Stealth (in %)")]
        public float Praetorian2HelmetStealth = -5f;
        [ConfigField(text: "Praetorian 2 Helmet Accuracy (in %)")]
        public float Praetorian2HelmetAccuracy = 0f;
        [ConfigField(text: "Praetorian 2 Helmet Weight")]
        public int Praetorian2HelmetWeight = 1;

        /// Praetorian2 Body
        [ConfigField(text: "Praetorian 2 Body Armor")]
        public float Praetorian2BodyArmor = 18f;
        [ConfigField(text: "Praetorian 2 Body Speed")]
        public float Praetorian2BodySpeed = 0f;
        [ConfigField(text: "Praetorian 2 Body Perception")]
        public float Praetorian2BodyPerception = 0f;
        [ConfigField(text: "Praetorian 2 Body Stealth (in %)")]
        public float Praetorian2BodyStealth = -10f;
        [ConfigField(text: "Praetorian 2 Body Accuracy (in %)")]
        public float Praetorian2BodyAccuracy = 0f;
        [ConfigField(text: "Praetorian 2 Body Weight")]
        public int Praetorian2BodyWeight = 3;

        /// Praetorian2 Leg
        [ConfigField(text: "Praetorian 2 Leg Armor")]
        public float Praetorian2LegArmor = 18f;
        [ConfigField(text: "Praetorian 2 Leg Speed")]
        public float Praetorian2LegSpeed = 1f;
        [ConfigField(text: "Praetorian 2 Leg Perception")]
        public float Praetorian2LegPerception = 0f;
        [ConfigField(text: "Praetorian 2 Leg Stealth (in %)")]
        public float Praetorian2LegStealth = -10f;
        [ConfigField(text: "Praetorian 2 Leg Accuracy (in %)")]
        public float Praetorian2LegAccuracy = 0f;
        [ConfigField(text: "Praetorian 2 Leg Weight")]
        public int Praetorian2LegWeight = 2;

        /// GuardianXA Helmet
        [ConfigField(text: "Guardian XA Helmet Armor")]
        public float GuardianXAHelmetArmor = 26f;
        [ConfigField(text: "Guardian XA Helmet Speed")]
        public float GuardianXAHelmetSpeed = 0f;
        [ConfigField(text: "Guardian XA Helmet Perception")]
        public float GuardianXAHelmetPerception = -7f;
        [ConfigField(text: "Guardian XA Helmet Stealth (in %)")]
        public float GuardianXAHelmetStealth = -10f;
        [ConfigField(text: "Guardian XA Helmet Accuracy (in %)")]
        public float GuardianXAHelmetAccuracy = -8f;
        [ConfigField(text: "Guardian XA Helmet Weight")]
        public int GuardianXAHelmetWeight = 2;

        /// GuardianXA Body
        [ConfigField(text: "Guardian XA Body Armor")]
        public float GuardianXABodyArmor = 34f;
        [ConfigField(text: "Guardian XA Body Speed")]
        public float GuardianXABodySpeed = -1f;
        [ConfigField(text: "Guardian XA Body Perception")]
        public float GuardianXABodyPerception = 0f;
        [ConfigField(text: "Guardian XA Body Stealth (in %)")]
        public float GuardianXABodyStealth = -20f;
        [ConfigField(text: "Guardian XA Body Accuracy (in %)")]
        public float GuardianXABodyAccuracy = -7f;
        [ConfigField(text: "Guardian XA Body Weight")]
        public int GuardianXABodyWeight = 3;

        /// GuardianXA Leg
        [ConfigField(text: "Guardian XA Leg Armor")]
        public float GuardianXALegArmor = 29f;
        [ConfigField(text: "Guardian XA Leg Speed")]
        public float GuardianXALegSpeed = 0f;
        [ConfigField(text: "Guardian XA Leg Perception")]
        public float GuardianXALegPerception = 0f;
        [ConfigField(text: "Guardian XA Leg Stealth (in %)")]
        public float GuardianXALegStealth = -20f;
        [ConfigField(text: "Guardian XA Leg Accuracy (in %)")]
        public float GuardianXALegAccuracy = -6f;
        [ConfigField(text: "Guardian XA Leg Weight")]
        public int GuardianXALegWeight = 2;

        /// SwampCat Helmet
        public float SwampCatHelmetArmor = 14f;
        public float SwampCatHelmetSpeed = 0f;
        public float SwampCatHelmetPerception = 0f;
        [ConfigField(text: "Swamp Cat Helmet Stealth (in %)")]
        public float SwampCatHelmetStealth = 0f;
        [ConfigField(text: "Swamp Cat Helmet Accuracy (in %)")]
        public float SwampCatHelmetAccuracy = 4f;
        public int SwampCatHelmetWeight = 1;

        /// SwampCat Body
        public float SwampCatBodyArmor = 16f;
        public float SwampCatBodySpeed = -1f;
        public float SwampCatBodyPerception = 0f;
        [ConfigField(text: "Swamp Cat Body Stealth (in %)")]
        public float SwampCatBodyStealth = 0f;
        [ConfigField(text: "Swamp Cat Body Accuracy (in %)")]
        public float SwampCatBodyAccuracy = 3f;
        public int SwampCatBodyWeight = 2;

        /// SwampCat Leg
        public float SwampCatLegArmor = 14f;
        public float SwampCatLegSpeed = -1f;
        public float SwampCatLegPerception = 0f;
        [ConfigField(text: "Swamp Cat Leg Stealth (in %)")]
        public float SwampCatLegStealth = 0f;
        [ConfigField(text: "Swamp Cat Leg Accuracy (in %)")]
        public float SwampCatLegAccuracy = 2f;
        public int SwampCatLegWeight = 2;
    }
}
