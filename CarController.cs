using UnityEngine;

public class CarController : MonoBehaviour
{
    public float maxSpeed = 5f;           // Viteza maximă a mașinii
    public float acceleration = 2f;       // Accelerația mașinii
    public float brakeForce = 8f;        // Forța de frânare
    public float turnSpeed = 50f;        // Viteza de rotație
    public Rigidbody rb;                 // Rigidbody-ul mașinii

    private float currentSpeed = 0f;     // Viteza curentă
    private bool isBraking = false;
    private bool isUpsideDown = false;   // Verifică dacă mașina este răsturnată

    private AudioSource carEngineAudio;  // Referință la AudioSource-ul motorului

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Preia Rigidbody-ul
        carEngineAudio = GetComponent<AudioSource>(); // Obține AudioSource-ul de pe mașină
        if (carEngineAudio != null)
        {
            carEngineAudio.loop = true;  // Sunetul motorului se va repeta
        }
    }

    void Update()
    {
        if (enabled)
        {
            float moveInput = Input.GetAxis("Vertical");
            float turnInput = Input.GetAxis("Horizontal");

            HandleMovement(moveInput, turnInput);
            HandleBraking(moveInput);
            CheckForUpsideDown();

            // Actualizează sunetul motorului pe baza vitezei
            if (carEngineAudio != null)
            {
                UpdateEngineSound();  // Actualizează sunetul motorului pe baza vitezei
            }
        }
    }

    void HandleMovement(float moveInput, float turnInput)
    {
        // Mers înainte
        if (moveInput > 0 && currentSpeed < maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        // Mers înapoi
        else if (moveInput < 0 && currentSpeed > -maxSpeed)
        {
            currentSpeed -= acceleration * Time.deltaTime;
        }

        // Frânare
        if (moveInput == 0 && currentSpeed > 0)
        {
            currentSpeed -= brakeForce * Time.deltaTime;
        }
        else if (moveInput == 0 && currentSpeed < 0)
        {
            currentSpeed += brakeForce * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);

        // Mișcare pe direcția curentă
        Vector3 move = transform.forward * currentSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + move);

        // Controlul direcției (inversarea controlului la mersul cu spatele)
        if (moveInput < 0) // Dacă mașina merge înapoi
        {
            // Inversăm direcția de rotire
            turnInput = -turnInput;
        }

        // Rotirea pe axa Y (direcțiile stânga/dreapta)
        if (turnInput != 0)
        {
            transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
        }
    }

    void HandleBraking(float moveInput)
    {
        if (moveInput == 0)
        {
            if (currentSpeed > 0)
                currentSpeed -= brakeForce * Time.deltaTime;
            else if (currentSpeed < 0)
                currentSpeed += brakeForce * Time.deltaTime;
        }

        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed);
    }

    void CheckForUpsideDown()
    {
        if (transform.up.y < 0)
        {
            isUpsideDown = true;
        }
        else
        {
            isUpsideDown = false;
        }

        if (isUpsideDown)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0), 1000 * Time.deltaTime);
            rb.velocity = Vector3.zero;
            currentSpeed = 0f;
        }
    }

    // Salvează poziția mașinii
    public void SaveCarPosition()
    {
        PlayerPrefs.SetFloat("CarPosX", transform.position.x);
        PlayerPrefs.SetFloat("CarPosY", transform.position.y);
        PlayerPrefs.SetFloat("CarPosZ", transform.position.z);
        PlayerPrefs.Save();
    }

    // Încărcă poziția mașinii
    public void LoadCarPosition()
    {
        if (PlayerPrefs.HasKey("CarPosX"))
        {
            float carPosX = PlayerPrefs.GetFloat("CarPosX");
            float carPosY = PlayerPrefs.GetFloat("CarPosY");
            float carPosZ = PlayerPrefs.GetFloat("CarPosZ");

            transform.position = new Vector3(carPosX, carPosY, carPosZ);
        }
    }

    // Actualizează sunetul motorului pe baza vitezei
    void UpdateEngineSound()
    {
        // Ajustează pitch-ul sunetului motorului în funcție de viteza curentă
        float pitch = Mathf.Lerp(1f, 2f, Mathf.Abs(currentSpeed) / maxSpeed);
        carEngineAudio.pitch = pitch;

        // Ajustează volumul sunetului motorului pe baza vitezei
        float volume = Mathf.Clamp(Mathf.Abs(currentSpeed) / maxSpeed, 0.2f, 1f);  // Minimul e 0.2, maximul 1
        carEngineAudio.volume = volume;
    }

    // Adaugă un getter pentru a accesa viteza curentă din alte scripturi
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
