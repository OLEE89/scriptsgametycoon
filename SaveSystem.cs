using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    private static string saveFilePath => Path.Combine(Application.persistentDataPath, "save.json");

    [System.Serializable]
    public class GameData
    {
        public int playerLevel;
        public float playerMoney;
        public List<string> builtBuildings;
        public string currentScene;  // Salvăm scena curentă
    }

    // Salvează progresul jocului
    public static void SaveGame(GameData gameData)
    {
        gameData.currentScene = SceneManager.GetActiveScene().name; // Salvăm scena curentă
        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game saved to: " + saveFilePath);
    }

    // Încarcă progresul jocului
    public static GameData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(json);
            Debug.Log("Game loaded!");

            // Încarcă scena salvată
            if (!string.IsNullOrEmpty(loadedData.currentScene))
            {
                SceneManager.LoadScene(loadedData.currentScene);
                Debug.Log("Scene Loaded: " + loadedData.currentScene);
            }

            return loadedData;
        }
        else
        {
            Debug.Log("No save found at: " + saveFilePath);
            return null;
        }
    }
}
