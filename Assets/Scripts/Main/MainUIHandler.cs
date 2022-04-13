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

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance == null)
        {
            ReturnToMenu();
            return;
        }

        fundingText.text = "Current Funding: $" + DataManager.Instance.money;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
    
}
