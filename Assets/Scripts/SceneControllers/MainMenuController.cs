using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BigRedButton.SceneControllers
{
    public class MainMenuController : MonoBehaviour
    {
        /// <summary>
        /// Constant to use for player preferences username key
        /// </summary>
        private const string USERNAME_KEY = "USERNAME";

        /// <summary>
        /// Constant specifying the game scene name to transition to when game starts
        /// </summary>
        private const string GAME_SCENE_NAME = "Button Game";

        /// <summary>
        /// Current username specified by the user
        /// </summary>
        private string _username;

        /// <summary>
        /// Requires an input field that the play inputs their username inside
        /// </summary>
        public InputField usernameInputField;

        /// <summary>
        /// Initialize the main menu state on the first frame
        /// </summary>
        protected void Start()
        {
            if (Debug.isDebugBuild)
            {
                Debug.Assert(usernameInputField != null, "ERROR: Username input field is unspecified / null");
            }

            // Load the player's username from their last session if it exists
            _username = PlayerPrefs.GetString(USERNAME_KEY, string.Empty);
            usernameInputField.text = _username;

            // Setup the input to call the SetUsername function when a user is done entering username
            usernameInputField.onEndEdit.AddListener( delegate { SetUsername(usernameInputField.text); });
        }

        /// <summary>
        /// Function to setup and store necessary data and then start the game
        /// </summary>
        public void StartGame()
        {
            SetUsername(_username);
            LoadScene(GAME_SCENE_NAME);
        }

        /// <summary>
        /// Set the username so that the game can access it later
        /// </summary>
        /// <param name="name">Username specified by the user</param>
        public void SetUsername(string name)
        {
            // @TODO - Sanitize the username input either here or when going to the DB
            if (_username != name)
            {
                _username = name;
                PlayerPrefs.SetString(USERNAME_KEY, name);
            }
        }

        /// <summary>
        /// From the main menu, load a different scene and transition into it
        /// </summary>
        private void LoadScene(string sceneName)
        {
            // Get the next scene in the build index list based on the current scene's build index
            Scene currentScene = SceneManager.GetActiveScene();

            if (Debug.isDebugBuild)
            {
                Debug.Log("Current scene: " + currentScene.name);
                Debug.Log("Attempting to load scene name: " + sceneName);
            }

            // Finally, load the next scene when ready
            SceneManager.LoadSceneAsync(sceneName);
        }
    }
}