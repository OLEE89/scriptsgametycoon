using UnityEngine;

public class EnterCar : MonoBehaviour
{
    public GameObject player;
    public GameObject car;
    public WheelRotation WheelRotationScript;
    public CarController carControlScript;
    public Transform carCameraTarget;
    private CameraFollow cameraFollow;
    public GameObject arrowComp;

    public PickupPackage pickupScript;  // Legătură către scriptul PickupPackage

    private AudioSource carEngineAudio;  // Referință la AudioSource pentru motor
    private float carSpeed = 0f;         // Viteza mașinii
    private float maxSpeed = 100f;       // Viteza maximă a mașinii (se poate ajusta)
    private float maxPitch = 2f;         // Pitch-ul maxim (mai înalt când accelerezi)
    private float minPitch = 1f;         // Pitch-ul minim (începutul accelerării)

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollow>();

        // Obține AudioSource-ul de pe mașină
        carEngineAudio = car.GetComponent<AudioSource>();

        // Opriți sunetul la începutul jocului
        if (carEngineAudio != null)
        {
            carEngineAudio.Stop();  // Oprește sunetul dacă este activ
        }
    }

    private void Update()
    {
        // Aici putem obține viteza actuală a mașinii sau accelerația
        if (carControlScript != null)
        {
            carSpeed = carControlScript.GetCurrentSpeed(); // Dacă ai o funcție în CarController care îți dă viteza curentă
        }

        // Actualizează sunetul motorului pe baza vitezei
        if (carEngineAudio != null)
        {
            // Ajustează pitch-ul sunetului pe baza vitezei
            float pitch = Mathf.Lerp(minPitch, maxPitch, carSpeed / maxSpeed);
            carEngineAudio.pitch = pitch;

            // Poți ajusta și volumul pe măsură ce accelerezi
            float volume = Mathf.Clamp(carSpeed / maxSpeed, 0.2f, 1f); // Minimul e 0.2, maximul 1
            carEngineAudio.volume = volume;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // NU face nimic dacă pickup-ul nu a fost făcut
            if (pickupScript != null && !pickupScript.IsPickupDone())
            {
                Debug.Log("Trebuie să ridici pachetul înainte să intri în mașină!");
                return;
            }

            player.SetActive(false);
            carControlScript.enabled = true;
            WheelRotationScript.enabled = true;

            cameraFollow.target = car.transform;

            if (arrowComp != null)
                arrowComp.SetActive(false);

            // Redă sunetul motorului când jucătorul intră în mașină
            if (carEngineAudio != null)
            {
                carEngineAudio.Play();  // Începe redarea sunetului
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            // Oprește sunetul motorului când jucătorul iese din mașină
            if (carEngineAudio != null)
            {
                carEngineAudio.Stop();  // Oprește sunetul motorului
            }
        }
    }
}
