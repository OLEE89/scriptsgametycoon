using UnityEngine;

public class DayOneCarController : MonoBehaviour
{
    void Start()
    {
        // Verifică dacă există coordonatele salvate pentru cilindrul "Point"
        if (PlayerPrefs.HasKey("PointPosX"))
        {
            float pointPosX = PlayerPrefs.GetFloat("PointPosX");
            float pointPosY = PlayerPrefs.GetFloat("PointPosY");
            float pointPosZ = PlayerPrefs.GetFloat("PointPosZ");

            // Găsește cilindrul "Point"
            GameObject point = GameObject.Find("Point");

            // Dacă cilindrul "Point" există, setează poziția mașinii la coordonatele sale
            if (point != null)
            {
                Vector3 pointPosition = new Vector3(pointPosX, pointPosY, pointPosZ);
                // Muta mașina la poziția cilindrului "Point"
                FindObjectOfType<CarController>().transform.position = pointPosition;
            }
        }
    }
}
