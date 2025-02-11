using PhoenixPoint.Modding;

namespace BionicAugmentationStats
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class BionicAugmentationStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Clarity Head
        [ConfigField(text: "Clarity Head Armor")]
        public float JuggHeadArmor = 30f;
        [ConfigField(text: "Clarity Head Speed")]
        public float JuggHeadSpeed = 0f;
        [ConfigField(text: "Clarity Head Perception")]
        public float JuggHeadPerception = 0f;
        [ConfigField(text: "Clarity Head Stealth (in %)")]
        public float JuggHeadStealth = -10f;
        [ConfigField(text: "Clarity Head Accuracy (in %)")]
        public float JuggHeadAccuracy = 3f;
        [ConfigField(text: "Clarity Head Weight")]
        public int JuggHeadWeight = 2;
        [ConfigField(text: "Clarity Head Is Permanent Augment")]
        public bool JuggHeadIsPermanentAugment = true;

        /// Disruptor Head
        [ConfigField(text: "Disruptor Head Armor")]
        public float ExoHeadArmor = 16f;
        [ConfigField(text: "Disruptor Head Speed")]
        public float ExoHeadSpeed = 0f;
        [ConfigField(text: "Disruptor Head Perception")]
        public float ExoHeadPerception = 5f;
        [ConfigField(text: "Disruptor Head Stealth (in %)")]
        public float ExoHeadStealth = -5f;
        [ConfigField(text: "Disruptor Head Accuracy (in %)")]
        public float ExoHeadAccuracy = 12f;
        [ConfigField(text: "Disruptor Head Weight")]
        public int ExoHeadWeight = 1;
        [ConfigField(text: "Disruptor Head Is Permanent Augment")]
        public bool ExoHeadIsPermanentAugment = true;

        /// Echo Head
        [ConfigField(text: "Echo Head Armor")]
        public float ShinobiHeadArmor = 20f;
        [ConfigField(text: "Echo Head Speed")]
        public float ShinobiHeadSpeed = 0f;
        [ConfigField(text: "Echo Head Perception")]
        public float ShinobiHeadPerception = 0f;
        [ConfigField(text: "Echo Head Stealth (in %)")]
        public float ShinobiHeadStealth = 10f;
        [ConfigField(text: "Echo Head Accuracy (in %)")]
        public float ShinobiHeadAccuracy = 0f;
        [ConfigField(text: "Echo Head Weight")]
        public int ShinobiHeadWeight = 0;
        [ConfigField(text: "Echo Head Is Permanent Augment")]
        public bool ShinobiHeadIsPermanentAugment = true;

        /// Juggernaut Torso
        [ConfigField(text: "Juggernaut Torso Armor")]
        public float JuggTorsoArmor = 45f;
        [ConfigField(text: "Juggernaut Torso Speed")]
        public float JuggTorsoSpeed = -1f;
        [ConfigField(text: "Juggernaut Torso Perception")]
        public float JuggTorsoPerception = 0f;
        [ConfigField(text: "Juggernaut Torso Stealth (in %)")]
        public float JuggTorsoStealth = -20f;
        [ConfigField(text: "Juggernaut Torso Accuracy (in %)")]
        public float JuggTorsoAccuracy = 1f;
        [ConfigField(text: "Juggernaut Torso Weight")]
        public int JuggTorsoWeight = 5;
        [ConfigField(text: "Juggernaut Torso Is Permanent Augment")]
        public bool JuggTorsoIsPermanentAugment = true;

        /// Neural Torso
        [ConfigField(text: "Neural Torso Armor")]
        public float ExoTorsoArmor = 30f;
        [ConfigField(text: "Neural Torso Speed")]
        public float ExoTorsoSpeed = 0f;
        [ConfigField(text: "Neural Torso Perception")]
        public float ExoTorsoPerception = 0f;
        [ConfigField(text: "Neural Torso Stealth (in %)")]
        public float ExoTorsoStealth = -15f;
        [ConfigField(text: "Neural Torso Accuracy (in %)")]
        public float ExoTorsoAccuracy = 8f;
        [ConfigField(text: "Neural Torso Weight")]
        public int ExoTorsoWeight = 2;
        [ConfigField(text: "Neural Torso Is Permanent Augment")]
        public bool ExoTorsoIsPermanentAugment = true;

        /// Vengeance Torso
        [ConfigField(text: "Vengeance Torso Armor")]
        public float ShinobiTorsoArmor = 20f;
        [ConfigField(text: "Vengeance Torso Speed")]
        public float ShinobiTorsoSpeed = 0f;
        [ConfigField(text: "Vengeance Torso Perception")]
        public float ShinobiTorsoPerception = 0f;
        [ConfigField(text: "Vengeance Torso Stealth (in %)")]
        public float ShinobiTorsoStealth = 20f;
        [ConfigField(text: "Vengeance Torso Accuracy (in %)")]
        public float ShinobiTorsoAccuracy = -3f;
        [ConfigField(text: "Vengeance Torso Weight")]
        public int ShinobiTorsoWeight = 1;
        [ConfigField(text: "Vengeance Torso Is Permanent Augment")]
        public bool ShinobiTorsoIsPermanentAugment = true;

        /// Armadillo Legs
        [ConfigField(text: "Armadillo Legs Armor")]
        public float JuggLegsArmor = 40f;
        [ConfigField(text: "Armadillo Legs Speed")]
        public float JuggLegsSpeed = -1f;
        [ConfigField(text: "Armadillo Legs Perception")]
        public float JuggLegsPerception = 0f;
        [ConfigField(text: "Armadillo Legs Stealth (in %)")]
        public float JuggLegsStealth = -25f;
        [ConfigField(text: "Armadillo Legs Accuracy (in %)")]
        public float JuggLegsAccuracy = 3f;
        [ConfigField(text: "Armadillo Legs Weight")]
        public int JuggLegsWeight = 3;
        [ConfigField(text: "Armadillo Legs Is Permanent Augment")]
        public bool JuggLegsIsPermanentAugment = true;

        /// Propellor Legs
        [ConfigField(text: "Propellor Legs Armor")]
        public float ExoLegsArmor = 20f;
        [ConfigField(text: "Propellor Legs Speed")]
        public float ExoLegsSpeed = 3f;
        [ConfigField(text: "Propellor Legs Perception")]
        public float ExoLegsPerception = 0f;
        [ConfigField(text: "Propellor Legs Stealth (in %)")]
        public float ExoLegsStealth = -10f;
        [ConfigField(text: "Propellor Legs Accuracy (in %)")]
        public float ExoLegsAccuracy = 0f;
        [ConfigField(text: "Propellor Legs Weight")]
        public int ExoLegsWeight = 2;
        [ConfigField(text: "Propellor Legs Is Permanent Augment")]
        public bool ExoLegsIsPermanentAugment = true;

        /// Mirage Legs
        [ConfigField(text: "Mirage Legs Armor")]
        public float ShinobiLegsArmor = 20f;
        [ConfigField(text: "Mirage Legs Speed")]
        public float ShinobiLegsSpeed = 1f;
        [ConfigField(text: "Mirage Legs Perception")]
        public float ShinobiLegsPerception = 0f;
        [ConfigField(text: "Mirage Legs Stealth (in %)")]
        public float ShinobiLegsStealth = 20f;
        [ConfigField(text: "Mirage Legs Accuracy (in %)")]
        public float ShinobiLegsAccuracy = -3f;
        [ConfigField(text: "Mirage Legs Weight")]
        public int ShinobiLegsWeight = 1;
        [ConfigField(text: "Mirage Legs Is Permanent Augment")]
        public bool ShinobiLegsIsPermanentAugment = true;

    }
}
