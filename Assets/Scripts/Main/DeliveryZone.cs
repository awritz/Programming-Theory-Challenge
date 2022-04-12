using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryZone : MonoBehaviour
{
    private Customer currentCustomer;

    [SerializeField]
    private GameObject customerPrefab;
    [SerializeField]
    private GameObject customerLocation;

    private List<Item> itemsInZone;

    // Start is called before the first frame update
    void Start()
    {
        itemsInZone = new List<Item>();
        GetNextCustomer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetNextCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, customerLocation.transform.position, customerLocation.transform.rotation);
        currentCustomer = customer.GetComponent<Customer>();
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
