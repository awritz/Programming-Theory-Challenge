using System;
using System.Collections.Generic;
using System.IO;
using Menu;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int money;
    
    public List<ItemDetails> items;

    public int orderComplexity = 1;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }
    
    [Serializable]
    class SaveData
    {
        public int money;

        public int orderComplexity;
        
        public List<ItemDetails> items;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.money = money;
        data.orderComplexity = orderComplexity;
        data.items = items;
        
        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/data.json", json);
    }

    private void Load()
    {
        string path = Application.persistentDataPath + "/data.json";
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            money = data.money;
            orderComplexity = data.orderComplexity;
            items = data.items;
        }
        else
        {
            money = 0;
            orderComplexity = 1;
        }
        
        if (items == null || items.Count == 0) LoadDefaultItemDetails();
    }


    private void LoadDefaultItemDetails()
    {
        items = new List<ItemDetails>
        {
            new ItemDetails("candle-red", 5, true),
            new ItemDetails("candle-blue", 10, false)
        };
    }



}
