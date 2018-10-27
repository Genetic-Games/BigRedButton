using System.Collections.Generic;
using UnityEngine;

namespace BigRedButton.AudioControllers
{
    public class SoundEffectController : MonoBehaviour
    {
        /// <summary>
        /// The source of where the audio will play
        /// </summary>
        public AudioSource soundEffectSource;

        /// <summary>
        /// Default probability to play a random sound
        /// </summary>
        public float chanceToPlaySoundEffect = 0.05f;

        /// <summary>
        /// A list of sound effects to use at arbitrary score times
        /// </summary>
        public List<AudioClip> soundEffects;

        /// <summary>
        /// A mapping of score numbers to audio clips that could be played at specific scores
        /// @TODO - Figure out why this won't show up in Unity Game Editor
        /// </summary>
        public Dictionary<ulong, AudioClip> targetScoreSoundEffects;

        // Initialize the sound effects starting state
        void Start()
        {
            if (Debug.isDebugBuild)
            {
                Debug.Assert(soundEffectSource != null, "ERROR: No audio source found, object null or not initialized");
                Debug.Assert(soundEffects != null && soundEffects.Count > 0, "ERROR: No sound effects found, list empty or null");
                Debug.Assert(chanceToPlaySoundEffect >= 0.0f && chanceToPlaySoundEffect <= 1.0f, 
                    "ERROR: Random sound effect probability not within allowable limits [0.0f, 1.0f]");

                // @TODO - Uncomment me when ready after figuring this out
                // Debug.Assert(TargetScoreSoundEffects != null, "ERROR: Target score sound effects not found, dictionary null");
            }
        }

        /// <summary>
        /// Helper method to tell if a sound is current being played
        /// </summary>
        /// <returns>True if a sound is playing out of the audio source, false otherwise</returns>
        public bool IsSoundPlaying()
        {
            return soundEffectSource.isPlaying;
        }

        /// <summary>
        /// Helper method to determine if a sound should play based on probability
        /// </summary>
        /// <returns>True if a sound should play, false otherwise</returns>
        public bool ShouldRandomSoundPlay()
        {
            return Random.Range(0.0f, 1.0f) <= chanceToPlaySoundEffect;
        }

        /// <summary>
        /// Play a random sound from the list of sounds
        /// </summary>
        public void PlayRandomSound()
        {
            int sfxIndex = Random.Range(0, soundEffects.Count);
            soundEffectSource.clip = soundEffects[sfxIndex];
            soundEffectSource.Play();
        }
    }
}