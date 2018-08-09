using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    /// <summary>
    /// From the main menu, load a different scene and transition into it
    /// </summary>
    public void LoadScene(string sceneName)
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
