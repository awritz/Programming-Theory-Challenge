using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Order order;

    [SerializeField]
    private List<Item> potentialItems;
    
    // Start is called before the first frame update
    void Start()
    {
        order = new Order(1, potentialItems);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
