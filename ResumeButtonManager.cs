using UnityEngine;
using UnityEngine.UI;

public class ResumeButtonManager : MonoBehaviour
{
    public Button resumeButton;

    void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);
    }

    public void ResumeGame()
    {
        FindObjectOfType<GameManager>().ResumeGame(); // Apelăm funcția ResumeGame din GameManager
    }
}
