using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static string previousScene;

    void Start()
    {
        // Salvează scena curentă când jocul începe
        previousScene = SceneManager.GetActiveScene().name;
    }

    // Funcția care revine la scena salvată
    public void ResumeGame()
    {
        // Încarcă scena DayOne
        if (!string.IsNullOrEmpty(previousScene) && previousScene != "dayone")
        {
            Debug.Log("Loading Scene: dayone");
            SceneManager.LoadScene("dayone");
        }
        else
        {
            Debug.Log("Already in dayone Scene or no scene saved.");
        }
    }

    // Salvează scena curentă când intri în car shop
    public static void SaveScene()
    {
        previousScene = SceneManager.GetActiveScene().name;
        Debug.Log("Scene Saved: " + previousScene);
    }
}
