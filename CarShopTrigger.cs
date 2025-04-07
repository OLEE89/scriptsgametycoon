using UnityEngine;
using UnityEngine.SceneManagement;

public class CarShopTrigger : MonoBehaviour
{
    // Se va apela atunci când un obiect intră în zona trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verifică dacă obiectul care intră în trigger are tag-ul "Player"
        if (other.CompareTag("Player"))
        {
            // Afișează un mesaj în consolă pentru debugging
            Debug.Log("Mașina a intrat în trigger! Schimbăm scena...");

            // Încarcă scena "carshopscene"
            SceneManager.LoadScene("carshopscene");
        }
    }
}
