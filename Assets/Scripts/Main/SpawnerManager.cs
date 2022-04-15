using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject redCandleSpawner;
    [SerializeField]
    private GameObject blueCandleSpawner;
    
    // Start is called before the first frame update
    void Start()
    {
        // Enable unlocked spawners
        EnableSpawners();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void EnableSpawners()
    {
        if (DataManager.Instance == null) return;
        foreach (var itemDetail in DataManager.Instance.items)
        {
            if (!itemDetail.unlocked) continue;

            switch (itemDetail.name)
            {
                case "candle-red":
                    redCandleSpawner.SetActive(true);
                    break;
                case "candle-blue":
                    blueCandleSpawner.SetActive(true);
                    break;
            }
        }
        
        
    }
    
}
