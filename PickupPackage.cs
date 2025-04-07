using UnityEngine;

public class PickupPackage : MonoBehaviour
{
    public GameObject player;
    public GameObject package;
    public GameObject arrowIndicator;
    public GameObject circleArea;

    private bool isAtPickupZone = false;
    private DeliveryManager deliveryManager;

    public float pickupDistance = 3f;
    public int amountToPickup = 10;

    public AudioSource audioSource;  // Referință la componenta AudioSource

    void Update()
    {
        if (Vector3.Distance(player.transform.position, circleArea.transform.position) < pickupDistance)
        {
            if (!isAtPickupZone)
            {
                isAtPickupZone = true;
                PickupPackageFromZone();  // Ridică pachetul
                HidePickupElements();     // Ascunde elementele de pickup
            }
        }
        else
        {
            if (isAtPickupZone)
            {
                ResetPickupZone();  // Resetează zona de pickup
            }
        }
    }

    void HidePickupElements()
    {
        if (package != null)
        {
            package.SetActive(false);  // Ascunde pachetul
            PlayPickupSound();         // Redă sunetul la ridicarea pachetului
        }
        if (arrowIndicator != null) arrowIndicator.SetActive(false);  // Ascunde săgeata
        if (circleArea != null) circleArea.SetActive(false);  // Ascunde zona de pickup
    }

    void ResetPickupZone()
    {
        isAtPickupZone = false;
    }

    public bool IsPickupDone()
    {
        return isAtPickupZone;
    }

    void PickupPackageFromZone()
    {
        if (deliveryManager != null)
        {
            deliveryManager.PickupPackage(amountToPickup);  // Ridică pachetul
        }
    }

    void PlayPickupSound()
    {
        if (audioSource != null && !audioSource.isPlaying)  // Verificăm dacă sunetul nu este deja redat
        {
            audioSource.Play();  // Redă sunetul doar atunci când ridici pachetul
            Debug.Log("Sunetul a fost redat!");  // Afișăm mesaj de debug pentru a verifica
        }
    }
}
