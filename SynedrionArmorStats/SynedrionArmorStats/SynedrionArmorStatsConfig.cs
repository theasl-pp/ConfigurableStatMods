using PhoenixPoint.Modding;

namespace SynedrionArmorStats
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class SynedrionArmorStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Phlegethon Helmet
        public float PhlegethonHelmetArmor = 18f;
        public float PhlegethonHelmetSpeed = 0f;
        public float PhlegethonHelmetPerception = 0f;
        [ConfigField(text: "Phlegethon Helmet Stealth (in %)")]
        public float PhlegethonHelmetStealth = 0f;
        [ConfigField(text: "Phlegethon Helmet Accuracy (in %)")]
        public float PhlegethonHelmetAccuracy = 6f;
        public int PhlegethonHelmetWeight = 1;

        /// Phlegethon Body
        public float PhlegethonBodyArmor = 20f;
        public float PhlegethonBodySpeed = 1f;
        public float PhlegethonBodyPerception = 0f;
        [ConfigField(text: "Phlegethon Body Stealth (in %)")]
        public float PhlegethonBodyStealth = 0f;
        [ConfigField(text: "Phlegethon Body Accuracy (in %)")]
        public float PhlegethonBodyAccuracy = 3f;
        public int PhlegethonBodyWeight = 2;

        /// Phlegethon Leg
        public float PhlegethonLegArmor = 18f;
        public float PhlegethonLegSpeed = 1f;
        public float PhlegethonLegPerception = 0f;
        [ConfigField(text: "Phlegethon Leg Stealth (in %)")]
        public float PhlegethonLegStealth = 0f;
        [ConfigField(text: "Phlegethon Leg Accuracy (in %)")]
        public float PhlegethonLegAccuracy = 2f;
        public int PhlegethonLegWeight = 2;

        /// Styx Helmet
        public float StyxHelmetArmor = 12f;
        public float StyxHelmetSpeed = 0f;
        public float StyxHelmetPerception = 10f;
        [ConfigField(text: "Styx Helmet Stealth (in %)")]
        public float StyxHelmetStealth = 10f;
        [ConfigField(text: "Styx Helmet Accuracy (in %)")]
        public float StyxHelmetAccuracy = 0f;
        public int StyxHelmetWeight = 0;

        /// Styx Body
        public float StyxBodyArmor = 16f;
        public float StyxBodySpeed = 0f;
        public float StyxBodyPerception = 0f;
        [ConfigField(text: "Styx Body Stealth (in %)")]
        public float StyxBodyStealth = 20f;
        [ConfigField(text: "Styx Body Accuracy (in %)")]
        public float StyxBodyAccuracy = 0f;
        public int StyxBodyWeight = 1;

        /// Styx Leg
        public float StyxLegArmor = 16f;
        public float StyxLegSpeed = 0f;
        public float StyxLegPerception = 0f;
        [ConfigField(text: "Styx Leg Stealth (in %)")]
        public float StyxLegStealth = 20f;
        [ConfigField(text: "Styx Leg Accuracy (in %)")]
        public float StyxLegAccuracy = 0f;
        public int StyxLegWeight = 1;

        /// Acheron Helmet
        public float AcheronHelmetArmor = 14f;
        public float AcheronHelmetSpeed = 0f;
        public float AcheronHelmetPerception = 7f;
        [ConfigField(text: "Acheron Helmet Stealth (in %)")]
        public float AcheronHelmetStealth = 5f;
        [ConfigField(text: "Acheron Helmet Accuracy (in %)")]
        public float AcheronHelmetAccuracy = 10f;
        public int AcheronHelmetWeight = 1;

        /// Acheron Body
        public float AcheronBodyArmor = 16f;
        public float AcheronBodySpeed = -1f;
        public float AcheronBodyPerception = 0f;
        [ConfigField(text: "Acheron Body Stealth (in %)")]
        public float AcheronBodyStealth = 10f;
        [ConfigField(text: "Acheron Body Accuracy (in %)")]
        public float AcheronBodyAccuracy = 5f;
        public int AcheronBodyWeight = 2;

        /// Acheron Leg
        public float AcheronLegArmor = 14f;
        public float AcheronLegSpeed = 0f;
        public float AcheronLegPerception = 0f;
        [ConfigField(text: "Acheron Leg Stealth (in %)")]
        public float AcheronLegStealth = 10f;
        [ConfigField(text: "Acheron Leg Accuracy (in %)")]
        public float AcheronLegAccuracy = 5f;
        public int AcheronLegWeight = 2;
    }
}
