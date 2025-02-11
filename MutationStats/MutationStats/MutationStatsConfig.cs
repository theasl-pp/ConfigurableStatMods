using PhoenixPoint.Modding;

namespace MutationStats
{
	/// <summary>
	/// ModConfig is mod settings that players can change from within the game.
	/// Config is only editable from players in main menu.
	/// Only one config can exist per mod assembly.
	/// Config is serialized on disk as json.
	/// </summary>
	public class MutationStatsConfig : ModConfig
    {
        /// these are the default game values as far as I can tell.
        /// we need to set them here so that they show up when enabling
        /// the mod, but not when starting the game with the mod already
        /// enabled.

        /// Armored Head
        [ConfigField(text: "Armored Head Armor")]
        public float HeavyHeadArmor = 30f;
        [ConfigField(text: "Armored Head Speed")]
        public float HeavyHeadSpeed = 0f;
        [ConfigField(text: "Armored Head Perception")]
        public float HeavyHeadPerception = -4f;
        [ConfigField(text: "Armored Head Stealth (in %)")]
        public float HeavyHeadStealth = -5f;
        [ConfigField(text: "Armored Head Accuracy (in %)")]
        public float HeavyHeadAccuracy = -6f;
        [ConfigField(text: "Armored Head Weight")]
        public int HeavyHeadWeight = 1;
        [ConfigField(text: "Armored Head Is Permanent Augment")]
        public bool HeavyHeadIsPermanentAugment = true;

        /// Perceptor Head
        [ConfigField(text: "Perceptor Head Armor")]
        public float WatcherHeadArmor = 15f;
        [ConfigField(text: "Perceptor Head Speed")]
        public float WatcherHeadSpeed = 0f;
        [ConfigField(text: "Perceptor Head Perception")]
        public float WatcherHeadPerception = 14f;
        [ConfigField(text: "Perceptor Head Stealth (in %)")]
        public float WatcherHeadStealth = 5f;
        [ConfigField(text: "Perceptor Head Accuracy (in %)")]
        public float WatcherHeadAccuracy = 10f;
        [ConfigField(text: "Perceptor Head Weight")]
        public int WatcherHeadWeight = 1;
        [ConfigField(text: "Perceptor Head Is Permanent Augment")]
        public bool WatcherHeadIsPermanentAugment = true;

        /// Resistor Head
        [ConfigField(text: "Resistor Head Armor")]
        public float ShooterHeadArmor = 20f;
        [ConfigField(text: "Resistor Head Speed")]
        public float ShooterHeadSpeed = 0f;
        [ConfigField(text: "Resistor Head Perception")]
        public float ShooterHeadPerception = 0f;
        [ConfigField(text: "Resistor Head Stealth (in %)")]
        public float ShooterHeadStealth = 0f;
        [ConfigField(text: "Resistor Head Accuracy (in %)")]
        public float ShooterHeadAccuracy = 0f;
        [ConfigField(text: "Resistor Head Weight")]
        public int ShooterHeadWeight = 1;
        [ConfigField(text: "Resistor Head Is Permanent Augment")]
        public bool ShooterHeadIsPermanentAugment = true;

        /// Synod Head
        [ConfigField(text: "Synod Head Armor")]
        public float PriestHead01Armor = 12f;
        [ConfigField(text: "Synod Head Speed")]
        public float PriestHead01Speed = 0f;
        [ConfigField(text: "Synod Head Perception")]
        public float PriestHead01Perception = 0f;
        [ConfigField(text: "Synod Head Stealth (in %)")]
        public float PriestHead01Stealth = -10f;
        [ConfigField(text: "Synod Head Accuracy (in %)")]
        public float PriestHead01Accuracy = 0f;
        [ConfigField(text: "Synod Head Weight")]
        public int PriestHead01Weight = 1;
        [ConfigField(text: "Synod Head Is Permanent Augment")]
        public bool PriestHead01IsPermanentAugment = true;

        /// Judgment Head
        [ConfigField(text: "Judgment Head Armor")]
        public float PriestHead02Armor = 25f;
        [ConfigField(text: "Judgment Head Speed")]
        public float PriestHead02Speed = 0f;
        [ConfigField(text: "Judgment Head Perception")]
        public float PriestHead02Perception = 0f;
        [ConfigField(text: "Judgment Head Stealth (in %)")]
        public float PriestHead02Stealth = -10f;
        [ConfigField(text: "Judgment Head Accuracy (in %)")]
        public float PriestHead02Accuracy = 7f;
        [ConfigField(text: "Judgment Head Weight")]
        public int PriestHead02Weight = 1;
        [ConfigField(text: "Judgment Head Is Permanent Augment")]
        public bool PriestHead02IsPermanentAugment = true;

        /// Screaming Head
        [ConfigField(text: "Screaming Head Armor")]
        public float PriestHead03Armor = 12f;
        [ConfigField(text: "Screaming Head Speed")]
        public float PriestHead03Speed = 0f;
        [ConfigField(text: "Screaming Head Perception")]
        public float PriestHead03Perception = 0f;
        [ConfigField(text: "Screaming Head Stealth (in %)")]
        public float PriestHead03Stealth = -10f;
        [ConfigField(text: "Screaming Head Accuracy (in %)")]
        public float PriestHead03Accuracy = 0f;
        [ConfigField(text: "Screaming Head Weight")]
        public int PriestHead03Weight = 1;
        [ConfigField(text: "Screaming Head Is Permanent Augment")]
        public bool PriestHead03IsPermanentAugment = true;

        /// Regeneration Torso
        [ConfigField(text: "Regeneration Torso Armor")]
        public float HeavyTorsoArmor = 34f;
        [ConfigField(text: "Regeneration Torso Speed")]
        public float HeavyTorsoSpeed = 0f;
        [ConfigField(text: "Regeneration Torso Perception")]
        public float HeavyTorsoPerception = 0f;
        [ConfigField(text: "Regeneration Torso Stealth (in %)")]
        public float HeavyTorsoStealth = -10f;
        [ConfigField(text: "Regeneration Torso Accuracy (in %)")]
        public float HeavyTorsoAccuracy = -8f;
        [ConfigField(text: "Regeneration Torso Weight")]
        public int HeavyTorsoWeight = 3;
        [ConfigField(text: "Regeneration Torso Is Permanent Augment")]
        public bool HeavyTorsoIsPermanentAugment = true;

        /// Tentacle Torso
        [ConfigField(text: "Tentacle Torso Armor")]
        public float WatcherTorsoArmor = 16f;
        [ConfigField(text: "Tentacle Torso Speed")]
        public float WatcherTorsoSpeed = 1f;
        [ConfigField(text: "Tentacle Torso Perception")]
        public float WatcherTorsoPerception = 6f;
        [ConfigField(text: "Tentacle Torso Stealth (in %)")]
        public float WatcherTorsoStealth = 0f;
        [ConfigField(text: "Tentacle Torso Accuracy (in %)")]
        public float WatcherTorsoAccuracy = 0f;
        [ConfigField(text: "Tentacle Torso Weight")]
        public int WatcherTorsoWeight = 3;
        [ConfigField(text: "Tentacle Torso Is Permanent Augment")]
        public bool WatcherTorsoIsPermanentAugment = true;

        /// Venom Torso
        [ConfigField(text: "Venom Torso Armor")]
        public float ShooterTorsoArmor = 20f;
        [ConfigField(text: "Venom Torso Speed")]
        public float ShooterTorsoSpeed = 2f;
        [ConfigField(text: "Venom Torso Perception")]
        public float ShooterTorsoPerception = 0f;
        [ConfigField(text: "Venom Torso Stealth (in %)")]
        public float ShooterTorsoStealth = 10f;
        [ConfigField(text: "Venom Torso Accuracy (in %)")]
        public float ShooterTorsoAccuracy = 20f;
        [ConfigField(text: "Venom Torso Weight")]
        public int ShooterTorsoWeight = 3;
        [ConfigField(text: "Venom Torso Is Permanent Augment")]
        public bool ShooterTorsoIsPermanentAugment = true;

        /// Stomper Legs
        [ConfigField(text: "Stomper Legs Armor")]
        public float HeavyLegsArmor = 30f;
        [ConfigField(text: "Stomper Legs Speed")]
        public float HeavyLegsSpeed = -1f;
        [ConfigField(text: "Stomper Legs Perception")]
        public float HeavyLegsPerception = 0f;
        [ConfigField(text: "Stomper Legs Stealth (in %)")]
        public float HeavyLegsStealth = -10f;
        [ConfigField(text: "Stomper Legs Accuracy (in %)")]
        public float HeavyLegsAccuracy = 12f;
        [ConfigField(text: "Stomper Legs Weight")]
        public int HeavyLegsWeight = 2;
        [ConfigField(text: "Stomper Legs Is Permanent Augment")]
        public bool HeavyLegsIsPermanentAugment = true;

        /// Shadow Legs
        [ConfigField(text: "Shadow Legs Armor")]
        public float WatcherLegsArmor = 12f;
        [ConfigField(text: "Shadow Legs Speed")]
        public float WatcherLegsSpeed = 0f;
        [ConfigField(text: "Shadow Legs Perception")]
        public float WatcherLegsPerception = 0f;
        [ConfigField(text: "Shadow Legs Stealth (in %)")]
        public float WatcherLegsStealth = 30f;
        [ConfigField(text: "Shadow Legs Accuracy (in %)")]
        public float WatcherLegsAccuracy = 0f;
        [ConfigField(text: "Shadow Legs Weight")]
        public int WatcherLegsWeight = 2;
        [ConfigField(text: "Shadow Legs Is Permanent Augment")]
        public bool WatcherLegsIsPermanentAugment = true;

        /// Agile Legs
        [ConfigField(text: "Agile Legs Armor")]
        public float ShooterLegsArmor = 10f;
        [ConfigField(text: "Agile Legs Speed")]
        public float ShooterLegsSpeed = 4f;
        [ConfigField(text: "Agile Legs Perception")]
        public float ShooterLegsPerception = 0f;
        [ConfigField(text: "Agile Legs Stealth (in %)")]
        public float ShooterLegsStealth = 0f;
        [ConfigField(text: "Agile Legs Accuracy (in %)")]
        public float ShooterLegsAccuracy = 0f;
        [ConfigField(text: "Agile Legs Weight")]
        public int ShooterLegsWeight = 2;
        [ConfigField(text: "Agile Legs Is Permanent Augment")]
        public bool ShooterLegsIsPermanentAugment = true;

    }
}
