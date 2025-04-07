using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{
    public TMP_Text moneyText;       // Text pentru afișarea banilor
    public TMP_Text levelText;       // Text pentru afișarea nivelului

    private int playerMoney = 0;     // Banii jucătorului
    private int playerLevel = 1;     // Nivelul jucătorului

    private int deliveryCount = 0;   // Contor pentru livrările efectuate
    private int deliveriesPerLevel = 10; // Livrările necesare pentru a avansa la următorul nivel

    void Start()
    {
        // Inițializează afișarea inițială a banilor și nivelului
        UpdateMoneyText();
        UpdateLevelText();
    }

    // Actualizează textul cu banii jucătorului
    public void UpdateMoneyText()
    {
        moneyText.text = "Money: $" + playerMoney;
    }

    // Actualizează textul cu nivelul jucătorului
    public void UpdateLevelText()
    {
        levelText.text = "Level: " + playerLevel;
    }

    // Funcție pentru a adăuga bani
    public void AddMoney(int amount)
    {
        playerMoney += amount;
        UpdateMoneyText();  // Actualizează textul după adăugarea banilor
    }

    // Funcție pentru a adăuga livrări
    public void CompleteDelivery()
    {
        deliveryCount++;
    }
}