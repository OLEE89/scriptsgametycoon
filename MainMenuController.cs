using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    // Referințe pentru butoane
    public Button buttonstartgame;
    public Button continueGameButton;
    public Button settingsButton;
    public Button loadGameButton;
    public Button quitGameButton;

    // Referințe la salvarea jocului
    private const string gameSavedKey = "GameSaved";  // Cheia pentru a verifica dacă există un joc salvat

    void Start()
    {
        // Asociază funcțiile butoanelor
        buttonstartgame.onClick.AddListener(StartGame);
        continueGameButton.onClick.AddListener(ContinueGame);
        settingsButton.onClick.AddListener(OpenSettings);
        loadGameButton.onClick.AddListener(LoadGame);
        quitGameButton.onClick.AddListener(QuitGame);

        // Verificăm dacă există un joc salvat
        bool gameSaved = PlayerPrefs.GetInt(gameSavedKey, 0) == 1;

        // Dacă nu există un joc salvat, ascundem butonul de "Continue Game"
        continueGameButton.gameObject.SetActive(gameSaved);
    }

    // Funcția pentru Start Game
    public void StartGame()
    {
        // Afișează o întrebare dacă vrea să înceapă de la început sau să continue
        PlayerPrefs.SetInt(gameSavedKey, 1);  // Salvează progresul pentru că jocul a început
        SceneManager.LoadScene("dayone");     // Schimbă scena la primul nivel ("dayone")
    }

    // Funcția pentru Continue Game
    public void ContinueGame()
    {
        // Dacă există un joc salvat, continuă de unde a rămas
        bool gameSaved = PlayerPrefs.GetInt(gameSavedKey, 0) == 1;

        if (gameSaved)
        {
            // Încarcă jocul din fișierul JSON
            SaveSystem.GameData loadedData = SaveSystem.LoadGame();

            if (loadedData != null)
            {
                // Aici poți utiliza datele încărcate pentru a restabili starea jocului
                Debug.Log("Player Level: " + loadedData.playerLevel);
                Debug.Log("Player Money: " + loadedData.playerMoney);

                // Poți chiar să mergi la nivelul corect (e.g., "dayone")
                SceneManager.LoadScene("dayone");  // Modifică cu nivelul corect pe care vrei să-l încarci
            }
        }
    }

    // Funcția pentru Settings
    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");  // Asigură-te că ai o scenă de Settings
    }

    // Funcția pentru Load Game (de obicei folosește un sistem de sloturi pentru încărcare)
    public void LoadGame()
    {
        SceneManager.LoadScene("dayone");  // Sau o scenă corespunzătoare pentru încărcare
    }

    // Funcția pentru Quit Game
    public void QuitGame()
    {
#if UNITY_EDITOR
        // Dacă ești în editorul Unity, oprește simularea jocului
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Dacă jocul este construit (de exemplu, .exe, .apk), închide aplicația
        Application.Quit();
#endif
    }
}
