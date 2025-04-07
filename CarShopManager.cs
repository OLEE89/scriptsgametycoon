using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CarShopManager : MonoBehaviour
{
    public GameObject[] cars;
    public TextMeshProUGUI carNameText;
    public TextMeshProUGUI carPriceText;
    public Button buyButton;
    public Button nextButton;
    public Button previousButton;
    public Transform cameraTarget; // CameraRig

    private int currentIndex = 0;
    private int[] carPrices = { 500, 1000, 1500, 800 };

    void Start()
    {
        UpdateCarDisplay();

        nextButton.onClick.AddListener(NextCar);
        previousButton.onClick.AddListener(PreviousCar);
        buyButton.onClick.AddListener(BuyCar);
    }

    void UpdateCarDisplay()
    {
        if (cameraTarget == null)
        {
            Debug.LogError("CameraTarget (CameraRig) nu este setat în inspector!");
            return;
        }

        carNameText.text = "Make: " + cars[currentIndex].name;
        carPriceText.text = "Price: $" + carPrices[currentIndex];

        Transform car = cars[currentIndex].transform;
        Vector3 offset = new Vector3(1.23f, 37.4f, -66.83f);
        cameraTarget.position = car.position + offset;
        cameraTarget.LookAt(car.position + Vector3.up * 1.5f);
    }

    void NextCar()
    {
        currentIndex = (currentIndex + 1) % cars.Length;
        UpdateCarDisplay();
    }

    void PreviousCar()
    {
        currentIndex = (currentIndex - 1 + cars.Length) % cars.Length;
        UpdateCarDisplay();
    }

    void BuyCar()
    {
        // Salvează progresul după cumpărarea mașinii
        SaveSystem.GameData gameData = new SaveSystem.GameData
        {
            playerLevel = 1, // Exemplu de nivel
            playerMoney = 500 - carPrices[currentIndex], // Reducem banii jucătorului
            builtBuildings = new List<string>(), // Poți adăuga clădirile construite aici
        };

        SaveSystem.SaveGame(gameData);  // Salvează progresul

        Debug.Log("Ai cumpărat: " + cars[currentIndex].name + " pentru $" + carPrices[currentIndex]);

        // După cumpărare, poți adăuga logica pentru a reveni la joc
    }
}
