using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public Dictionary<string, int> items;

    public Order(int itemCount, List<Item> potentialItems)
    {
        items = new Dictionary<string, int>();
        for (int i = 0; i < itemCount; i++)
        {
            string randomItem = potentialItems[Random.Range(0, potentialItems.Count)].GetItemId();
            items.TryGetValue(randomItem, out int currentCount);
            items[randomItem] = currentCount + 1;
        }
    }

}