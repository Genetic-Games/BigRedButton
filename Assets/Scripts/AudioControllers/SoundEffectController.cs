using System.Collections.Generic;
using UnityEngine;

namespace BigRedButton.AudioControllers
{
    public class SoundEffectController : MonoBehaviour
    {
        /// <summary>
        /// A list of sound effects to use at arbitrary score times
        /// </summary>
        public List<AudioClip> SoundEffects;

        /// <summary>
        /// A mapping of score numbers to audio clips that could be played at specific scores
        /// @TODO - Figure out why this won't show up in Unity Game Editor
        /// </summary>
        public Dictionary<ulong, AudioClip> TargetScoreSoundEffects;

        // Initialize the sound effects starting state
        void Start()
        {
            if (Debug.isDebugBuild)
            {
                Debug.Assert(SoundEffects != null && SoundEffects.Count > 0, "ERROR: No sound effects found, list empty or null");

                // @TODO - Uncomment me when ready after figuring this out
                // Debug.Assert(TargetScoreSoundEffects != null, "ERROR: Target score sound effects not found, dictionary null");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayRandomSound()
        {
            int sfxIndex = Random.Range(0, SoundEffects.Count - 1);
            // @TODO - Now that you have an index of a sound you want to play, figure out how to only play it if no other sounds are playing
        }
    }
}