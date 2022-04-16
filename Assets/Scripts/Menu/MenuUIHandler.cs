using System.Collections;
using System.Collections.Generic;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI fundingText;

    [SerializeField]
    private Slider orderComplexitySlider;

    [SerializeField] private TextMeshProUGUI orderComplexityDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        fundingText.text = "Current Funding: $" + DataManager.Instance.money;
        orderComplexitySlider.value = DataManager.Instance.orderComplexity;
        orderComplexityDisplay.text = $"{DataManager.Instance.orderComplexity}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustComplexity()
    {
        DataManager.Instance.orderComplexity = (int) orderComplexitySlider.value;
        orderComplexityDisplay.text = $"{DataManager.Instance.orderComplexity}";
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenShop()
    {
        SceneManager.LoadScene(2);
    }
    
    public void QuitGame()
    {
        DataManager.Instance.Save();
        
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
