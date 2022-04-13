using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int money;
    
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
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.money = money;
        
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
        }
        else
        {
            money = 0;
        }
    }
    
    
    
}
