using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Menu;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopUIHandler : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI fundingText;

    [SerializeField] private Button buyBlueCandleButton;

    [SerializeField] private TextMeshProUGUI blueCandleCostText;
    
    [SerializeField] private TextMeshProUGUI blueCandlePurchasedText;

    // Start is called before the first frame update
    void Start()
    {
        fundingText.text = "Current Funding: $" + DataManager.Instance.money;

        if (DataManager.Instance.money < 100)
        {
            buyBlueCandleButton.interactable = false;
        }

        if (DataManager.Instance.items.First(i => i.name.Equals("candle-blue")).unlocked)
        {
            DisableBlueCandlePurchaseButton();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void BuyBlueCandleUpgrade()
    {
        ItemDetails itemDetails = DataManager.Instance.items.First(i => i.name.Equals("candle-blue"));

        DataManager.Instance.money -= 100;
        
        itemDetails.unlocked = true;
        DisableBlueCandlePurchaseButton();
        
        fundingText.text = "Current Funding: $" + DataManager.Instance.money;
    }

    private void DisableBlueCandlePurchaseButton()
    {
        buyBlueCandleButton.gameObject.SetActive(false);
        blueCandleCostText.gameObject.SetActive(false);
        
        blueCandlePurchasedText.gameObject.SetActive(true);
    }

}
