using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SC_Currency : MonoBehaviour
{
    private int collectible;
    private float money;
    public Text moneyText;
    public Text collectibleText;
    private bool currencyUIBool;
    [SerializeField] private GameObject currencyUIGameObject;

    public void AddMoney(float ammount)
    {
        money += ammount;
        print(money);
    }
    public void AddCollectible(int ammount)
    {
        collectible += ammount;
        print(collectible);
    }

    private void Update()
    {
        moneyText.text = "" + money;
        collectibleText.text = "" + collectible;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currencyUIBool = !currencyUIBool;
            currencyUIGameObject.SetActive(currencyUIBool);
        }
    }
}
