using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public CarController carController;  // Referință la CarController

    // Apelată atunci când vrem să schimbăm scena
    public void ChangeSceneToCarShop()
    {
        // Salvează poziția mașinii
        carController.SaveCarPosition();

        // Schimbă scena
        SceneManager.LoadScene("CarShopScene");
    }
}
