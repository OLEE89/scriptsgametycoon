using UnityEngine;

public class ParcelDelivery : MonoBehaviour
{
    public int packagesToDeliver = 4;  // Numărul de colete de livrat la acest punct
    private DeliveryManager deliveryManager;

    public AudioSource deliveredSound;  // Referință la AudioSource pentru sunetul de livrare

    void Start()
    {
        // Caută automat DeliveryManager în scenă
        deliveryManager = FindObjectOfType<DeliveryManager>();

        if (deliveryManager == null)
        {
            Debug.LogError("Nu am găsit DeliveryManager în scenă!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jucătorul a ajuns la locația de livrare!");

            if (deliveryManager != null)
            {
                deliveryManager.CompleteDelivery(packagesToDeliver);
                gameObject.SetActive(false);
                Debug.Log("Livrarea a fost completată!");

                // Redă sunetul de livrare
                if (deliveredSound != null)
                {
                    deliveredSound.Play();  // Redă sunetul de livrare
                }
            }
            else
            {
                Debug.LogError("deliveryManager este NULL la livrare!");
            }
        }
    }
}
