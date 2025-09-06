using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This script manages the main menu UI functionality.
/// Attach this script to a GameObject in your main menu scene, such as the Canvas.
/// </summary>
public class MainMenu : MonoBehaviour
{
    
    public void PlayGame()
    {
        // Replace "YourGameSceneName" with the actual name of your main game scene file.
        // For example, if your scene is named "CastleLevel1", you would use that name here.
        SceneManager.LoadScene("DemoScene");
    }

    /// <summary>
    /// Placeholder method for the "Settings" button.
    /// For now, it just prints a message to the console.
    /// Later, you can have this method open a settings panel or a different scene.
    /// </summary>
    public void OpenSettings()
    {
        // This is a good way to test if your button is working before you build the full feature.
        Debug.Log("Settings button clicked! You can implement the settings panel later.");
    }

    /// <summary>
    /// Quits the application.
    /// This will only work in a built version of the game (not in the Unity Editor).
    /// It's good practice to include a way for players to exit the game.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        Application.Quit();
    }
}
