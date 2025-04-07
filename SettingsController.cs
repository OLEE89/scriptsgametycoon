using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    // Referințe pentru elementele UI
    public Slider volumeSlider;
    public Toggle vibrationToggle;
    public Button resumeGameButton;         // Butonul Resume Game
    public Button backToMainMenuButton;     // Butonul Back To Main Menu
    public AudioSource backgroundMusic;     // Referință la AudioSource-ul care redă muzica de fundal

    void Start()
    {
        // Setări inițiale din PlayerPrefs
        if (volumeSlider != null)
        {
            volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f); // Setează volumul pe baza valorii salvate
            volumeSlider.onValueChanged.AddListener(SetVolume); // Adaugă listener pentru slider
        }

        if (vibrationToggle != null)
        {
            vibrationToggle.isOn = PlayerPrefs.GetInt("Vibration", 1) == 1;
            vibrationToggle.onValueChanged.AddListener(SetVibration); // Adaugă listener pentru toggle-ul de vibrație
        }

        if (resumeGameButton != null)
            resumeGameButton.onClick.AddListener(ResumeGame); // Butonul pentru a relua jocul

        if (backToMainMenuButton != null)
            backToMainMenuButton.onClick.AddListener(BackToMainMenu); // Butonul pentru a merge la meniul principal

        // Dacă există un AudioSource pentru muzica de fundal, setează volumul acestuia pe valoarea sliderului
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = PlayerPrefs.GetFloat("Volume", 1f); // Setează volumul la valoarea salvată
        }
    }

    void SetVolume(float volume)
    {
        // Modifică volumul global al audio-ului
        AudioListener.volume = volume;

        // Dacă ai muzică de fundal, actualizează și volumul ei
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = volume; // Actualizează volumul muzicii de fundal
        }

        // Salvează volumul în PlayerPrefs
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }

    void SetVibration(bool isVibrationOn)
    {
        // Salvează setarea de vibrație
        PlayerPrefs.SetInt("Vibration", isVibrationOn ? 1 : 0);

        // Poți adăuga efecte de vibrație pentru mobile dacă este cazul
    }

    void ResumeGame()
    {
        // Încarcă scena curentă a jocului - modifică dacă ai altă scenă
        SceneManager.LoadScene("dayone");
    }

    void BackToMainMenu()
    {
        // Mergi la meniul principal
        SceneManager.LoadScene("MainMenu");
    }
}
