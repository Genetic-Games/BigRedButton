﻿using BigRedButton.DataAccess;
using UnityEngine;
using UnityEngine.UI;

namespace BigRedButton.SceneControllers
{
    public class ButtonGameController : MonoBehaviour
    {

        private bool isCurrentScoreHighScore = false;

        private ulong currentScore = 0;
        private ulong highScore = 0;

        private string currentScoreText = "Current Score: ";
        private string highScoreText = "High Score: ";
        private string newHighScoreText = "New High Score!";

        private string username;

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
        /// Initialize the game state
        /// </summary>
        void Start()
        {
            UpdateTextBox(currentScoreTextBox, currentScoreText + currentScore);
            UpdateTextBox(highScoreTextBox, highScoreText + highScore);
            UpdateTextBox(newHighScoreTextBox, newHighScoreText);
            newHighScoreTextBox.gameObject.SetActive(isCurrentScoreHighScore);

            if (Debug.isDebugBuild)
            {
                Debug.Assert(currentScoreTextBox != null, "Error: Current Score Text Box Unspecified / Null");
                Debug.Assert(highScoreTextBox != null, "Error: High Score Text Box Unspecified / Null");
                Debug.Assert(newHighScoreTextBox != null, "Error: New High Score Text Box Unspecified / Null");
            }
        }

        /// <summary>
        /// Update the game state every frame
        /// </summary>
        void Update()
        {
        }

        /// <summary>
        /// Use to check if the current score is a new high score and set high score and flags if so
        /// </summary>
        /// <param name="score"/>
        private void SetHighScore(ulong score)
        {
            if (!isCurrentScoreHighScore)
            {
                isCurrentScoreHighScore = true;
                newHighScoreTextBox.gameObject.SetActive(isCurrentScoreHighScore);
            }

            highScore = score;
        }

        /// <summary>
        /// Helper function to determine if the current score is the new highest
        /// </summary>
        /// <param name="current"></param>
        /// <param name="highest"></param>
        /// <returns></returns>
        private bool IsNewHighScore(ulong current, ulong highest)
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
            currentScore++;
            UpdateTextBox(currentScoreTextBox, currentScoreText + currentScore);

            // Check if it's a new high score and handle if it is
            if (IsNewHighScore(currentScore, highScore))
            {
                SetHighScore(currentScore);
                UpdateTextBox(highScoreTextBox, highScoreText + highScore);
            }
        }
    }
}