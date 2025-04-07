using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    // Referințele pentru meniul de pauză și butoane
    public GameObject pauseMenuPanel; // Panoul care conține butoanele
    public Button pauseButton; // Butonul pentru a deschide/închide meniul de pauză
    public Button settingsButton; // Butonul de setări
    public Button loadGameButton; // Butonul de încărcare joc
    public Button mainMenuButton; // Butonul pentru a reveni la meniul principal
    public Button quitButton; // Butonul de a ieși din joc
    public Button resumeButton; // Butonul de a relua jocul

    void Start()
    {
        // La început, ascunde meniul de pauză
        pauseMenuPanel.SetActive(false);

        // Asociază funcțiile pentru butoane
        pauseButton.onClick.AddListener(TogglePauseMenu);
        settingsButton.onClick.AddListener(OpenSettings);
        loadGameButton.onClick.AddListener(LoadGame);
        mainMenuButton.onClick.AddListener(BackToMainMenu);
        quitButton.onClick.AddListener(QuitGame);
        resumeButton.onClick.AddListener(ResumeGame);
    }

    // Funcția care comută vizibilitatea meniului de pauză
    void TogglePauseMenu()
    {
        bool isActive = pauseMenuPanel.activeSelf;
        pauseMenuPanel.SetActive(!isActive);

        if (pauseMenuPanel.activeSelf)
        {
            Time.timeScale = 0f; // Pune jocul în pauză
        }
        else
        {
            Time.timeScale = 1f; // Reia jocul
        }
    }

    // Funcția pentru a deschide setările
    void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    // Funcția pentru a încărca jocul
    void LoadGame()
    {
        var data = SaveSystem.LoadGame();
        if (data != null)
        {
            Debug.Log("Player Level: " + data.playerLevel);
            Debug.Log("Player Money: " + data.playerMoney);
            // Aplică datele încărcate aici
        }
    }

    // Funcția pentru a reveni la meniul principal
    void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Funcția pentru a ieși din joc
    void QuitGame()
    {
        // Ieși din aplicație
#if UNITY_EDITOR
        // Dacă ești în editorul Unity, oprește jocul (dar nu va închide editorul)
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Dacă jocul este în build, închide aplicația
        Application.Quit();
#endif
    }

    // Funcția pentru a relua jocul
    void ResumeGame()
    {
        // Salvează poziția mașinii înainte de a schimba scena
        FindObjectOfType<CarController>().SaveCarPosition();

        // Închide meniul de pauză și reia jocul
        pauseMenuPanel.SetActive(false); // Ascunde meniul de pauză
        Time.timeScale = 1f; // Reia jocul

        // Schimbă scena în DayOne
        SceneManager.LoadScene("DayOne");

        // După ce scena se încarcă, încarcă poziția mașinii
        StartCoroutine(LoadCarPositionAfterSceneLoad());
    }

    // Corutina pentru a aștepta încărcarea scenei și a restabili poziția mașinii
    private IEnumerator LoadCarPositionAfterSceneLoad()
    {
        // Așteaptă un pic pentru ca scena să se încarce
        yield return new WaitForSeconds(0.1f);

        // După încărcarea scenei, încarcă poziția mașinii
        FindObjectOfType<CarController>().LoadCarPosition();
    }
}
