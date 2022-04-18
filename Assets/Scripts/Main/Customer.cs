using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Customer : MonoBehaviour
{
    // ENCAPSULATION
    public Order order { get; private set; }
    public bool orderIsAvailable{ get; private set; }
    
    [SerializeField]
    private List<Item> potentialItems;

    [SerializeField] private List<GameObject> characterModels;

    // Start is called before the first frame update
    void Start()
    {
        List<Item> availableItems = getAvailableItems(potentialItems);
        int itemCount = DataManager.Instance.orderComplexity;
        
        order = new Order(itemCount, availableItems);
        orderIsAvailable = true;

        GameObject model = characterModels[Random.Range(0, characterModels.Count)];
        
        Vector3 position = new Vector3(0, -1, 0);
        model.transform.position = position;
        
        Vector3 scale = new Vector3(.75f, .75f, .75f);
        model.transform.localScale = scale;
        
        Instantiate(model, transform, false);
        
        // gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f)));
    }

    private List<Item> getAvailableItems(List<Item> items)
    {
        List<Item> result = new List<Item>();
        foreach (var item in items)
        {
            int count = DataManager.Instance.items.Count(i => i.name == item.GetItemId() && i.unlocked);
            if (count > 0)
            {
                result.Add(item);
            }
        }

        Debug.Log("Available Items for Customer to buy");
        foreach (var item in result)
        {
            Debug.Log($"Item: {item.name} - {item.GetItemId()}");
        }
        
        return result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecrementOrderTimer()
    {
        if (orderIsAvailable)
        {
            order.DecrementTimeLimit(Time.deltaTime);
            if (order.TimeLimit <= 0)
            {
                orderIsAvailable = false;
            }
        }
        
    }

    public string GetOrderDisplayText()
    {
        string result = "";
        
        if (order == null) return result;
        
        foreach (var orderItem in order.items)
        {
            Debug.Log("Order Display: " + orderItem.Key);
            result += orderItem.Key + " x " + orderItem.Value + "\n";
        }

        return result;
    }
}
