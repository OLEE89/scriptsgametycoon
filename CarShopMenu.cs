using UnityEngine;
using UnityEngine.SceneManagement;

public class CarShopMenu : MonoBehaviour
{
    // Funcția care salvează scena și poziția cilindrului Point
    public void ExitShop()
    {
        // Salvează scena curentă (CarShop)
        GameManager.SaveScene(); // Salvează scena curentă înainte de a ieși

        // Găsește cilindrul "Point" care definește locul unde vrei să ajungi în DayOne
        GameObject point = GameObject.Find("Point");

        if (point != null)
        {
            // Salvează poziția cilindrului "Point"
            Vector3 pointPosition = point.transform.position;
            PlayerPrefs.SetFloat("PointPosX", pointPosition.x);
            PlayerPrefs.SetFloat("PointPosY", pointPosition.y);
            PlayerPrefs.SetFloat("PointPosZ", pointPosition.z);
            PlayerPrefs.Save();
        }

        // Schimbă scena în dayone
        SceneManager.LoadScene("dayone");
    }
}
