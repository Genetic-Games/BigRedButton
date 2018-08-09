using UnityEngine;
using UnityEngine.UI;

public class ButtonGameController : MonoBehaviour {

    private uint highScore = 0;
    private string highScoreText = "High Score: ";

    /// <summary>
    /// Requires a text box to display the high score
    /// </summary>
    public Text highScoreTextBox;

	// Use this for initialization
	void Start ()
    {
        highScore = 0;

        if (Debug.isDebugBuild)
        {
            Debug.Assert(highScoreTextBox != null);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        highScoreTextBox.text = highScoreText + highScore;
	}

    /// <summary>
    /// Call this every time the button is pressed
    /// </summary>
    public void IncrementCount()
    {
        highScore++;
    }
}
