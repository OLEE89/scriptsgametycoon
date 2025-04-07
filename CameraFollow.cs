using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;      // Obiectul pe care camera trebuie să-l urmărească
    public float distance = 10f;  // Distanța față de obiectul urmărit
    public float height = 5f;     // Înălțimea camerei
    public float smoothSpeed = 0.125f;  // Viteza de interpolare pentru mișcarea camerei

    void LateUpdate()
    {
        if (target == null) return;

        // Calculăm direcția din spatele obiectului urmărit
        Vector3 targetPosition = target.position - target.forward * distance + Vector3.up * height;

        // Aplicăm o interpolare lină pentru a urmări mișcarea obiectului urmărit
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Setează poziția camerei
        transform.position = smoothedPosition;

        // Camera se rotește astfel încât să urmeze rotația obiectului urmărit
        transform.rotation = Quaternion.Euler(0, target.eulerAngles.y, 0);
    }
}
