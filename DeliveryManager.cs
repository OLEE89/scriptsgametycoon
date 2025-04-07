using TMPro;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text levelText;
    public TMP_Text textDelivered;

    private int playerMoney = 0;
    private int playerLevel = 1;
    private int deliveryCount = 0;
    private int deliveriesPerLevel = 10;

    private int totalPackages = 0;
    private int maxPackages = 10;
    private bool allDeliveriesCompleted = false;


    void Start()
    {
        UpdateMoneyText();
        UpdateLevelText();
        textDelivered.gameObject.SetActive(false);
    }

    void Update()
    {
        if (deliveryCount >= deliveriesPerLevel)
        {
            playerLevel++;
            deliveryCount = 0;
            deliveriesPerLevel += 10;
            UpdateLevelText();
        }

        if (allDeliveriesCompleted)
        {
            ShowSuccessMessage();
        }
    }

    public void UpdateMoneyText()
    {
        if (moneyText != null)
            moneyText.text = "Money: $" + playerMoney;
    }

    public void UpdateLevelText()
    {
        if (levelText != null)
            levelText.text = "Level: " + playerLevel;
    }

    public void AddMoney(int amount)
    {
        playerMoney += amount;
        UpdateMoneyText();
    }

    public void CompleteDelivery(int packagesDelivered)
    {
        if (totalPackages == 0)
        {
            totalPackages = maxPackages;
        }

        if (totalPackages >= packagesDelivered)
        {
            totalPackages -= packagesDelivered;
            deliveryCount += packagesDelivered;
            AddMoney(packagesDelivered * 800);

            if (deliveryCount >= deliveriesPerLevel)
            {
                allDeliveriesCompleted = true;
            }
        }
    }

    private void ShowSuccessMessage()
    {
        if (textDelivered != null)
        {
            textDelivered.gameObject.SetActive(true);
            textDelivered.text = "You've delivered all the packages and leveled up!";
        }
    }

    public void PickupPackage(int amount)
    {
        if (totalPackages + amount <= maxPackages)
        {
            totalPackages += amount;
            Debug.Log("Total packages after pickup: " + totalPackages);
        }
    }
}
