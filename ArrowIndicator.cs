using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{
    public float moveSpeed = 1f; // Viteza de mișcare a săgeții
    public float amplitude = 0.5f; // Amplitudinea mișcării (înălțimea)
    public float frequency = 1f; // Frecvența mișcării (viteza sus-jos)

    private Vector3 initialPosition;

    void Start()
    {
        // Salvează poziția inițială a săgeții
        initialPosition = transform.position;
    }

    void Update()
    {
        // Mișcarea sinusoidală a săgeții
        float newY = initialPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}
