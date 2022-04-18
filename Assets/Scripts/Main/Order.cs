using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public Dictionary<string, int> items;

    // ENCAPSULATION
    public float TimeLimit { get; private set; }

    public Order(int itemCount, List<Item> potentialItems)
    {
        items = new Dictionary<string, int>();
        for (int i = 0; i < itemCount; i++)
        {
            string randomItem = potentialItems[Random.Range(0, potentialItems.Count)].GetItemId();
            items.TryGetValue(randomItem, out int currentCount);
            items[randomItem] = currentCount + 1;
        }

        // Give 30 seconds per item in the order.
        TimeLimit = 10 * itemCount;
    }

    public void DecrementTimeLimit(float value)
    {
        TimeLimit -= value;
    }

}
