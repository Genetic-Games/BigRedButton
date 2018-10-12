using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BigRedButton.SceneControllers
{
    /// <summary>
    /// Controller for the Splash screen in charge of fading in and fading out the splash image and loading the next scene
    /// </summary>
    public class SplashController : MonoBehaviour
    {
        // Requires an input image (no default)
        public Image splashImage;
        public float fadeDuration = 4.0f;
        public float waitBetweenStepsDuration = 2.0f;

        private Color _invisible;
        private Color _visible;
        private bool _hasStartedFadeIn;
        private bool _hasCompletedFadeIn;
        private bool _hasCompletedFadeOut;
        private bool _isReadyForNextScene;
        private float _startTime;
        private float _fadeInTime;

        /// <summary>
        /// Initialize the controller on the first frame
        /// </summary>
        void Start()
        {
            // Validate that the splash image exists
            if (Debug.isDebugBuild)
            {
                Debug.Assert(splashImage != null, "Error: Splash Image Unspecified / Null");
            }

            // Set default values for private parameters
            _hasStartedFadeIn = false;
            _hasCompletedFadeIn = false;
            _hasCompletedFadeOut = false;
            _isReadyForNextScene = false;

            _invisible = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            _visible = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            // Ensure that the image starts off hidden so it can fade in and out in the right order
            splashImage.color = _invisible;

            if (Debug.isDebugBuild)
            {
                Debug.Log("Starting Splash Color: " + splashImage.color);
            }

            // Kick off the coroutine for the fading process
            StartCoroutine(WaitThenStartFadingIn());
        }

        /// <summary>
        /// Update is called once per frame after the first frame
        /// </summary>
        void Update()
        {
            // Use floats as value step when used with timestamp difference and max range value of duration
            float timeToFadeIn = (Time.time - _startTime) / fadeDuration;
            float timeToFadeOut = (Time.time - _fadeInTime) / fadeDuration;

            if (Debug.isDebugBuild)
            {
                Debug.Log("Current Splash Color: " + splashImage.color);
            }

            // Smoothly fade in if image has not been faded in yet
            if (!_hasCompletedFadeIn && _hasStartedFadeIn)
            {
                splashImage.color = new Color(1.0f, 1.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, timeToFadeIn));
            }

            // Smoothly fade out if image has already been faded in and has not faded out yet
            else if (_hasCompletedFadeIn && !_hasCompletedFadeOut && _hasStartedFadeIn)
            {
                splashImage.color = new Color(1.0f, 1.0f, 1.0f, Mathf.SmoothStep(1.0f, 0.0f, timeToFadeOut));
            }

            // Check whether specific fade conditions have been met in the following functions and triggers next action if so
            CheckFadeIn();
            CheckFadeOut();
            CheckLoadNextScene();
        }

        /// <summary>
        /// Function to check if the image has completed fading in and sets appropriate flags and timestamps
        /// </summary>
        void CheckFadeIn()
        {
            if (Equals(_visible, splashImage.color))
            {
                _hasCompletedFadeIn = true;
                _fadeInTime = Time.time;

                if (Debug.isDebugBuild)
                {
                    Debug.Log("Splash screen successfully faded in.");
                }
            }
        }

        /// <summary>
        /// Function to check if the image has completed fading out, sets appropriate flags, and kicks off completion coroutine
        /// </summary>
        void CheckFadeOut()
        {
            if (_hasCompletedFadeIn && !_hasCompletedFadeOut && Equals(_invisible, splashImage.color))
            {
                _hasCompletedFadeOut = true;

                // Kick off the coroutine to wait until we can load the next scene
                StartCoroutine(WaitThenCompleteFadingOut());

                if (Debug.isDebugBuild)
                {
                    Debug.Log("Splash Screen successfully faded out.");
                }
            }
        }

        /// <summary>
        /// Load next scene once splash screen fade process is completed
        /// </summary>
        void CheckLoadNextScene()
        {
            if (_isReadyForNextScene)
            {
                // Get the next scene in the build index list based on the current scene's build index
                Scene currentScene = SceneManager.GetActiveScene();
                int nextSceneBuildIndex = currentScene.buildIndex + 1;

                if (Debug.isDebugBuild)
                {
                    Debug.Log("Current scene " + currentScene.name + " is at build index " + currentScene.buildIndex);
                    Debug.Log("Loading next scene at build index " + nextSceneBuildIndex);
                }

                // Finally, load the next scene when ready
                SceneManager.LoadSceneAsync(nextSceneBuildIndex);
            }
        }

        /// <summary>
        /// Once image fades out completely, briefly wait before doing anything else, purely aesthetic
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitThenCompleteFadingOut()
        {
            yield return new WaitForSeconds(waitBetweenStepsDuration);
            _isReadyForNextScene = true;
        }

        /// <summary>
        /// Briefly wait until starting to fade in the image, purely aesthetic
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitThenStartFadingIn()
        {
            yield return new WaitForSeconds(waitBetweenStepsDuration);
            _hasStartedFadeIn = true;
            _startTime = Time.time;
        }
    }
}