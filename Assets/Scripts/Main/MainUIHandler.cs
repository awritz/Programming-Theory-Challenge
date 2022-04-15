using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1000)]
public class MainUIHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI fundingText;

    [SerializeField] private TextMeshProUGUI orderTimer;

    [SerializeField] private TextMeshProUGUI orderDisplay;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance == null)
        {
            ReturnToMenu();
            return;
        }
        UpdateFundingText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateFundingText()
    {
        fundingText.text = "Current Funding: $" + DataManager.Instance.money;
    }

    public void UpdateTimerText(float value)
    {
        orderTimer.text = $"Time Remaining for Order: {value}s";
    }

    public void UpdateOrderDisplayText(string text)
    {
        orderDisplay.text = $"Current order:\n{text}";
    }
    
}
