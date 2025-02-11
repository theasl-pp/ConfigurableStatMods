using PhoenixPoint.Modding;

namespace PhoenixArmorStats
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class PhoenixArmorStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Odin Helmet
        public float OdinHelmetArmor = 20f;
        public float OdinHelmetSpeed = 0f;
        public float OdinHelmetPerception = 0f;
        [ConfigField(text: "Odin Helmet Stealth (in %)")]
        public float OdinHelmetStealth = 0f;
        [ConfigField(text: "Odin Helmet Accuracy (in %)")]
        public float OdinHelmetAccuracy = 0f;
        public int OdinHelmetWeight = 1;

        /// Odin Body
        public float OdinBodyArmor = 20f;
        public float OdinBodySpeed = 0f;
        public float OdinBodyPerception = 0f;
        [ConfigField(text: "Odin Body Stealth (in %)")]
        public float OdinBodyStealth = 0f;
        [ConfigField(text: "Odin Body Accuracy (in %)")]
        public float OdinBodyAccuracy = 0f;
        public int OdinBodyWeight = 3;

        /// Odin Leg
        public float OdinLegArmor = 20f;
        public float OdinLegSpeed = 1f;
        public float OdinLegPerception = 0f;
        [ConfigField(text: "Odin Leg Stealth (in %)")]
        public float OdinLegStealth = 0f;
        [ConfigField(text: "Odin Leg Accuracy (in %)")]
        public float OdinLegAccuracy = 0f;
        public int OdinLegWeight = 2;

        /// Golem Helmet
        public float GolemHelmetArmor = 27f;
        public float GolemHelmetSpeed = 0f;
        public float GolemHelmetPerception = -5f;
        [ConfigField(text: "Golem Helmet Stealth (in %)")]
        public float GolemHelmetStealth = -10f;
        [ConfigField(text: "Golem Helmet Accuracy (in %)")]
        public float GolemHelmetAccuracy = -4f;
        public int GolemHelmetWeight = 2;

        /// Golem Body
        public float GolemBodyArmor = 34f;
        public float GolemBodySpeed = -1f;
        public float GolemBodyPerception = 0f;
        [ConfigField(text: "Golem Body Stealth (in %)")]
        public float GolemBodyStealth = -20f;
        [ConfigField(text: "Golem Body Accuracy (in %)")]
        public float GolemBodyAccuracy = -7f;
        public int GolemBodyWeight = 3;
        [ConfigField(text: "Golem Body Jet Jump Fumble Perc (in %)")]
        public int GolemBodyFumblePerc = 50;

        /// Golem Leg
        public float GolemLegArmor = 30f;
        public float GolemLegSpeed = -1f;
        public float GolemLegPerception = 0f;
        [ConfigField(text: "Golem Leg Stealth (in %)")]
        public float GolemLegStealth = -10f;
        [ConfigField(text: "Golem Leg Accuracy (in %)")]
        public float GolemLegAccuracy = -4f;
        public int GolemLegWeight = 3;

        /// Banshee Helmet
        public float BansheeHelmetArmor = 14f;
        public float BansheeHelmetSpeed = 0f;
        public float BansheeHelmetPerception = 5f;
        [ConfigField(text: "Banshee Helmet Stealth (in %)")]
        public float BansheeHelmetStealth = 3f;
        [ConfigField(text: "Banshee Helmet Accuracy (in %)")]
        public float BansheeHelmetAccuracy = 8f;
        public int BansheeHelmetWeight = 1;

        /// Banshee Body
        public float BansheeBodyArmor = 18f;
        public float BansheeBodySpeed = -1f;
        public float BansheeBodyPerception = 0f;
        [ConfigField(text: "Banshee Body Stealth (in %)")]
        public float BansheeBodyStealth = 5f;
        [ConfigField(text: "Banshee Body Accuracy (in %)")]
        public float BansheeBodyAccuracy = 4f;
        public int BansheeBodyWeight = 2;

        /// Banshee Leg
        public float BansheeLegArmor = 16f;
        public float BansheeLegSpeed = 0f;
        public float BansheeLegPerception = 0f;
        [ConfigField(text: "Banshee Leg Stealth (in %)")]
        public float BansheeLegStealth = 6f;
        [ConfigField(text: "Banshee Leg Accuracy (in %)")]
        public float BansheeLegAccuracy = 4f;
        public int BansheeLegWeight = 2;
    }
}
