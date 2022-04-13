using System.Collections;
using System.Collections.Generic;
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
        order = new Order(1, potentialItems);
        orderIsAvailable = true;
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
}
