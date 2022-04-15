using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Order order;
    public bool orderIsAvailable;
    
    [SerializeField]
    private List<Item> potentialItems;

    // Start is called before the first frame update
    void Start()
    {
        List<Item> availableItems = getAvailableItems(potentialItems);
        int itemCount = DataManager.Instance.orderComplexity;
        
        order = new Order(itemCount, availableItems);
        orderIsAvailable = true;

        gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f)));
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
            order.timeLimit -= Time.deltaTime;
            if (order.timeLimit <= 0)
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
