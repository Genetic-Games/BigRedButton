using BigRedButton.AudioControllers;
using UnityEngine;
using UnityEngine.UI;

namespace BigRedButton.SceneControllers
{
    public class ButtonGameController : MonoBehaviour
    {

        // @TODO - Comment all of these private variables
        private bool _isCurrentScoreHighScore = false;

        // @TODO - Check to make sure that we don't overflow to negatives somewhere
        private long _currentScore = 0;
        private long _highScore = 0;

        private string _currentScoreText = "Current Score: ";
        private string _highScoreText = "High Score: ";
        private string _newHighScoreText = "New High Score!";

        private string _username;

        /// <summary>
        /// Requires a text box to display the current score
        /// </summary>
        public Text currentScoreTextBox;

        /// <summary>
        /// Requires a text box to display the high score
        /// </summary>
        public Text highScoreTextBox;

        /// <summary>
        /// Requires a text box to display text that a new high score has been reached
        /// </summary>
        public Text newHighScoreTextBox;

        /// <summary>
        /// Requires a sound effect controller to store SFX and play them when needed
        /// </summary>
        public SoundEffectController soundEffectController;

        /// <summary>
        /// Initialize the game state
        /// </summary>
        internal void Start()
        {
            UpdateTextBox(currentScoreTextBox, _currentScoreText + _currentScore);
            UpdateTextBox(highScoreTextBox, _highScoreText + _highScore);
            UpdateTextBox(newHighScoreTextBox, _newHighScoreText);
            newHighScoreTextBox.gameObject.SetActive(_isCurrentScoreHighScore);

            if (Debug.isDebugBuild)
            {
                Debug.Assert(currentScoreTextBox != null, "ERROR: Current Score Text Box Unspecified / Null");
                Debug.Assert(highScoreTextBox != null, "ERROR: High Score Text Box Unspecified / Null");
                Debug.Assert(newHighScoreTextBox != null, "ERROR: New High Score Text Box Unspecified / Null");
                Debug.Assert(soundEffectController != null, "ERROR: Sound Effect Controller Unspecified / Null");
            }
        }

        /// <summary>
        /// Update the game state every frame
        /// </summary>
        internal void Update()
        {
        }

        /// <summary>
        /// Use to check if the current score is a new high score and set high score and flags if so
        /// </summary>
        /// <param name="score"/>
        private void SetHighScore(long score)
        {
            if (!_isCurrentScoreHighScore)
            {
                _isCurrentScoreHighScore = true;
                newHighScoreTextBox.gameObject.SetActive(_isCurrentScoreHighScore);

            }

            _highScore = score;
        }

        /// <summary>
        /// Helper function to determine if the current score is the new highest
        /// </summary>
        /// <param name="current"></param>
        /// <param name="highest"></param>
        /// <returns></returns>
        private bool IsNewHighScore(long current, long highest)
        {
            return current >= highest;
        }

        /// <summary>
        /// Helper function to update a text box with a particular string
        /// </summary>
        /// <param name="box"></param>
        /// <param name="newText"></param>
        private void UpdateTextBox(Text box, string newText)
        {
            box.text = newText;
        }

        /// <summary>
        /// Call this every time the button is pressed
        /// </summary>
        public void IncrementCount()
        {
            _currentScore++;
            UpdateTextBox(currentScoreTextBox, _currentScoreText + _currentScore);

            // Only consider playing a sound if there is not a sound being played and we should play one
            if (!soundEffectController.IsSoundPlaying() && soundEffectController.ShouldRandomSoundPlay())
            {
                soundEffectController.PlayRandomSound();
            }

            // Check if it's a new high score and handle if it is
            if (IsNewHighScore(_currentScore, _highScore))
            {
                SetHighScore(_currentScore);
                UpdateTextBox(highScoreTextBox, _highScoreText + _highScore);
            }
        }
    }
}