using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Menu;
using TMPro;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    private Customer currentCustomer;

    [SerializeField]
    private GameObject customerPrefab;
    [SerializeField]
    private GameObject customerLocation;

    private List<Item> itemsInZone;

    [SerializeField]
    private MainUIHandler mainUIHandler;

    private bool orderIsComplete;
    private bool newOrderIsAvailable = true;
    
    // Start is called before the first frame update
    void Start()
    {
        itemsInZone = new List<Item>();
        GetNextCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        if (orderIsComplete)
        {
            GetNextCustomer();
            return;
        }
        
        
        if (currentCustomer == null || currentCustomer.order == null) return;

        if (newOrderIsAvailable)
        {
            mainUIHandler.UpdateOrderDisplayText(currentCustomer.GetOrderDisplayText());
            newOrderIsAvailable = false;
        }
        
        currentCustomer.DecrementOrderTimer();

        float time = Mathf.Round(currentCustomer.order.timeLimit);
        
        mainUIHandler.UpdateTimerText(time);
        
        if (!currentCustomer.orderIsAvailable)
        {
            // Order has run out of time.
            // Get new customer
            Debug.Log("Order Failed! You have run out of time!");   
            GetNextCustomer();
        }
    }

    private void GetNextCustomer()
    {
        Debug.Log("Getting Next Customer");
        if (currentCustomer != null) Destroy(currentCustomer.gameObject);

        
        GameObject customer = Instantiate(customerPrefab, customerLocation.transform.position, customerLocation.transform.rotation);
        currentCustomer = customer.GetComponent<Customer>();
        orderIsComplete = false;
        newOrderIsAvailable = true;
    }

    public void TurnInOrder()
    {
        Debug.Log("Turning in order.");
        
        // Check items in delivery zone for order.
        // Load into dictionary
        Dictionary<string, int> deliveredItems = new Dictionary<string, int>();
        foreach (var item in itemsInZone)
        {
            deliveredItems.TryGetValue(item.GetItemId(), out int currentCount);
            deliveredItems[item.GetItemId()] = currentCount + 1;
        }

        // Compare to requested items
        var orderedItems = currentCustomer.order.items;
        var equal = false;
        if (deliveredItems.Count == orderedItems.Count) { // Require equal count.
            equal = true;
            foreach (var pair in orderedItems) {
                int value;
                if (deliveredItems.TryGetValue(pair.Key, out value)) {
                    if (value != pair.Value) {
                        equal = false;
                        break;
                    }
                } else {
                    equal = false;
                    break;
                }
            }
        }

        if (equal)
        {
            Debug.Log("Order completed!"); 
            // Clear all items in zone
            foreach (var item in itemsInZone)
            {
                Destroy(item.gameObject);
            }
            itemsInZone.Clear();
            
            // Add money for order
            foreach (var orderedItem in orderedItems)
            {
                ItemDetails itemDetails = DataManager.Instance.items.First(i => i.name.Equals(orderedItem.Key));
                // Total count of item purchased * cost of the item
                DataManager.Instance.money += orderedItem.Value * itemDetails.cost;
            }
            
            // Update money UI
            mainUIHandler.UpdateFundingText();

            orderIsComplete = true;

        }
        else
        {
            Debug.Log("Order is incomplete.");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("Item entered delivery zone");
            Item item = other.gameObject.GetComponent<Item>();
            if (!itemsInZone.Contains(item))
            {
                itemsInZone.Add(item);
                Debug.Log("Added item. Items in Delivery Zone: " + itemsInZone.Count);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            Debug.Log("Item exited delivery zone");
            Item item = other.gameObject.GetComponent<Item>();
            if (itemsInZone.Contains(item))
            {
                itemsInZone.Remove(item);
                Debug.Log("Removed item. Items in Delivery Zone: " + itemsInZone.Count);
            }
        }
    }
    
}
