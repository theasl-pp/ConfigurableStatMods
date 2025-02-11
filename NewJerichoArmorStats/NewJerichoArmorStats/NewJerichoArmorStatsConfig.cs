using PhoenixPoint.Modding;

namespace NewJerichoArmorStats
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class NewJerichoArmorStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Wardog Helmet
        public float WardogHelmetArmor = 23f;
        public float WardogHelmetSpeed = 0f;
        public float WardogHelmetPerception = 0f;
        [ConfigField(text: "Wardog Helmet Stealth (in %)")]
        public float WardogHelmetStealth = -5f;
        [ConfigField(text: "Wardog Helmet Accuracy (in %)")]
        public float WardogHelmetAccuracy = 0f;
        public int WardogHelmetWeight = 1;

        /// Wardog Body
        public float WardogBodyArmor = 24f;
        public float WardogBodySpeed = 0f;
        public float WardogBodyPerception = 0f;
        [ConfigField(text: "Wardog Body Stealth (in %)")]
        public float WardogBodyStealth = -10f;
        [ConfigField(text: "Wardog Body Accuracy (in %)")]
        public float WardogBodyAccuracy = 0f;
        public int WardogBodyWeight = 3;

        /// Wardog Leg
        public float WardogLegArmor = 22f;
        public float WardogLegSpeed = 1f;
        public float WardogLegPerception = 0f;
        [ConfigField(text: "Wardog Leg Stealth (in %)")]
        public float WardogLegStealth = -10f;
        [ConfigField(text: "Wardog Leg Accuracy (in %)")]
        public float WardogLegAccuracy = 0f;
        public int WardogLegWeight = 2;

        /// Anvil-2 Helmet
        [ConfigField(text: "Anvil-2 Helmet Armor")]
        public float Anvil2HelmetArmor = 33f;
        [ConfigField(text: "Anvil-2 Helmet Speed")]
        public float Anvil2HelmetSpeed = 0f;
        [ConfigField(text: "Anvil-2 Helmet Perception")]
        public float Anvil2HelmetPerception = -7f;
        [ConfigField(text: "Anvil-2 Helmet Stealth (in %)")]
        public float Anvil2HelmetStealth = -10f;
        [ConfigField(text: "Anvil-2 Helmet Accuracy (in %)")]
        public float Anvil2HelmetAccuracy = -4f;
        [ConfigField(text: "Anvil-2 Helmet Weight")]
        public int Anvil2HelmetWeight = 2;

        /// Anvil-2 Body
        [ConfigField(text: "Anvil-2 Body Armor")]
        public float Anvil2BodyArmor = 40f;
        [ConfigField(text: "Anvil-2 Body Speed")]
        public float Anvil2BodySpeed = -1f;
        [ConfigField(text: "Anvil-2 Body Perception")]
        public float Anvil2BodyPerception = 0f;
        [ConfigField(text: "Anvil-2 Body Stealth (in %)")]
        public float Anvil2BodyStealth = -20f;
        [ConfigField(text: "Anvil-2 Body Accuracy (in %)")]
        public float Anvil2BodyAccuracy = -8f;
        [ConfigField(text: "Anvil-2 Body Weight")]
        public int Anvil2BodyWeight = 4;
        [ConfigField(text: "Anvil-2 Body Jet Jump Fumble Perc (in %)")]
        public int Anvil2BodyFumblePerc = 50;

        /// Anvil-2 Leg
        [ConfigField(text: "Anvil-2 Leg Armor")]
        public float Anvil2LegArmor = 35f;
        [ConfigField(text: "Anvil-2 Leg Speed")]
        public float Anvil2LegSpeed = -1f;
        [ConfigField(text: "Anvil-2 Leg Perception")]
        public float Anvil2LegPerception = 0f;
        [ConfigField(text: "Anvil-2 Leg Stealth (in %)")]
        public float Anvil2LegStealth = -20f;
        [ConfigField(text: "Anvil-2 Leg Accuracy (in %)")]
        public float Anvil2LegAccuracy = -4f;
        [ConfigField(text: "Anvil-2 Leg Weight")]
        public int Anvil2LegWeight = 3;

        /// Eidolon Helmet
        public float EidolonHelmetArmor = 18f;
        public float EidolonHelmetSpeed = 0f;
        public float EidolonHelmetPerception = 7f;
        [ConfigField(text: "Eidolon Helmet Stealth (in %)")]
        public float EidolonHelmetStealth = 0f;
        [ConfigField(text: "Eidolon Helmet Accuracy (in %)")]
        public float EidolonHelmetAccuracy = 9f;
        public int EidolonHelmetWeight = 1;

        /// Eidolon Body
        public float EidolonBodyArmor = 20f;
        public float EidolonBodySpeed = -1f;
        public float EidolonBodyPerception = 0f;
        [ConfigField(text: "Eidolon Body Stealth (in %)")]
        public float EidolonBodyStealth = 0f;
        [ConfigField(text: "Eidolon Body Accuracy (in %)")]
        public float EidolonBodyAccuracy = 5f;
        public int EidolonBodyWeight = 2;

        /// Eidolon Leg
        public float EidolonLegArmor = 18f;
        public float EidolonLegSpeed = -1f;
        public float EidolonLegPerception = 0f;
        [ConfigField(text: "Eidolon Leg Stealth (in %)")]
        public float EidolonLegStealth = 0f;
        [ConfigField(text: "Eidolon Leg Accuracy (in %)")]
        public float EidolonLegAccuracy = 5f;
        public int EidolonLegWeight = 2;

        /// TechOps-7 Helmet
        [ConfigField(text: "TechOps-7 Helmet Armor")]
        public float Techops7HelmetArmor = 25f;
        [ConfigField(text: "TechOps-7 Helmet Speed")]
        public float Techops7HelmetSpeed = 0f;
        [ConfigField(text: "TechOps-7 Helmet Perception")]
        public float Techops7HelmetPerception = 5f;
        [ConfigField(text: "TechOps-7 Helmet Stealth (in %)")]
        public float Techops7HelmetStealth = -5f;
        [ConfigField(text: "TechOps-7 Helmet Accuracy (in %)")]
        public float Techops7HelmetAccuracy = 6f;
        [ConfigField(text: "TechOps-7 Helmet Weight")]
        public int Techops7HelmetWeight = 1;

        /// TechOps-7 Body
        [ConfigField(text: "TechOps-7 Body Armor")]
        public float Techops7BodyArmor = 25f;
        [ConfigField(text: "TechOps-7 Body Speed")]
        public float Techops7BodySpeed = -1f;
        [ConfigField(text: "TechOps-7 Body Perception")]
        public float Techops7BodyPerception = 0f;
        [ConfigField(text: "TechOps-7 Body Stealth (in %)")]
        public float Techops7BodyStealth = -15f;
        [ConfigField(text: "TechOps-7 Body Accuracy (in %)")]
        public float Techops7BodyAccuracy = 3f;
        [ConfigField(text: "TechOps-7 Body Weight")]
        public int Techops7BodyWeight = 3;

        /// TechOps-7 Leg
        [ConfigField(text: "TechOps-7 Leg Armor")]
        public float Techops7LegArmor = 25f;
        [ConfigField(text: "TechOps-7 Leg Speed")]
        public float Techops7LegSpeed = -1f;
        [ConfigField(text: "TechOps-7 Leg Perception")]
        public float Techops7LegPerception = 0f;
        [ConfigField(text: "TechOps-7 Leg Stealth (in %)")]
        public float Techops7LegStealth = -10f;
        [ConfigField(text: "TechOps-7 Leg Accuracy (in %)")]
        public float Techops7LegAccuracy = 4f;
        [ConfigField(text: "TechOps-7 Leg Weight")]
        public int Techops7LegWeight = 2;
    }
}
